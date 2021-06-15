using MediatR;
using Ron.Ido.BM.Events;
using Ron.Ido.BM.Extensions.Entities;
using Ron.Ido.BM.Interfaces;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class ApplyStatusService : IDependency
    {
        protected AppDbContext _appDbContext;
        protected IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IStatusChecker _checker;
        public static ConcurrentDictionary<long, ApplyStatus> _prefetch = new ConcurrentDictionary<long, ApplyStatus>();

        public const string ApplyNotFound = "Заявка не найдена";
        public const string StatusNotFound = "Статус не найден";
        public const string NotAllowed = "Нельзя установить этот статус";
        public const string AlreadyInStatus = "Статус не меняется";
        public const string Miscondition = "Неподходящие условия";
        public const string NoHistory = "Статус не менялся";

        public ApplyStatusService(AppDbContext appDbContext, IMediator mediator, IStatusChecker checker, IIdentityService identityService) 
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
            _checker = checker;
            _identityService = identityService;
        }

        /// <summary>
        /// Проверяет на наличие заявок в указанном статусе
        /// </summary>
        /// <param name="applyStatusId"></param>
        /// <returns>результат проверки</returns>
        public bool DenyDelete(long applyStatusId)
        {

            return _appDbContext.Applies.Any(a => a.StatusId == applyStatusId) || _appDbContext.ApplyStatusHistories.Any(ash=>ash.PrevStatusId == applyStatusId || ash.StatusId == applyStatusId);
        }

        /// <summary>
        /// Откатиться к предыдущему статусу
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public string RevertStatus(long applyId, string pars)
        {
            var apply = _appDbContext.Applies.Find(applyId);
            if ( apply == null )
                return ApplyNotFound;
            var changes = _appDbContext.ApplyStatusHistories
                .Where(css => css.ApplyId == applyId)
                .OrderByDescending(cr => cr.ChangeTime);

            var last = changes.FirstOrDefault();

            if ( !(last?.PrevStatusId.HasValue ?? false))
                return NoHistory;

            apply.StatusId = last.PrevStatusId.Value;
            var newDossier = new Dossier { Apply = apply };
            _appDbContext.Applies.Update(apply);
            _appDbContext.Dossiers.Add(newDossier);
            _appDbContext.SaveChanges();
            _mediator.Publish(new ApplyStatusRollbackEvent(newDossier, pars));
            return string.Empty;
        }

        public string SetStatus(long applyId, long statusId, string pars = null)
        {
            #region Post-Payment processor
            string PostPayment(string pars, Apply apply, ApplyStatusEnum newStatusEnum)
            {
                if ( _checker.DigitalCertificate(apply, pars) )
                {
                    switch ( _checker.AdmissionForm(apply, pars) )
                    {
                        case ApplyFormEnum.Mail:
                            if ( newStatusEnum == ApplyStatusEnum.GIVEN_DOCS_IN_NIC )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        case ApplyFormEnum.Online:
                            if ( _checker.IsFullSet(apply, pars) )
                                if ( newStatusEnum == ApplyStatusEnum.WAIT_DOCUMENTS )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;
                            else
                                if ( newStatusEnum == ApplyStatusEnum.GIVEN_DOCS_IN_NIC )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        case ApplyFormEnum.Personal:
                            if ( newStatusEnum == ApplyStatusEnum.GIVEN )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        default:
                            return Miscondition;

                    }
                }
                else // Нет, архив
                {
                    switch ( _checker.AdmissionForm(apply, pars) )
                    {
                        case ApplyFormEnum.Online:
                            if ( _checker.IsFullSet(apply, pars) )
                                if ( newStatusEnum == ApplyStatusEnum.WAIT_DOCUMENTS )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;
                            else
                                if ( newStatusEnum == ApplyStatusEnum.READY_TO_GIVE )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        case ApplyFormEnum.Mail:
                        case ApplyFormEnum.Personal:
                            if ( newStatusEnum == ApplyStatusEnum.READY_TO_GIVE )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        default:
                            return Miscondition;
                    }

                }
            }
            #endregion

            var apply = _appDbContext.Applies.Find(applyId);
            if ( apply == null )
                return ApplyNotFound;

            var currentStatus = apply.Status ?? _prefetch.GetOrAdd(apply.StatusId, key => _appDbContext.ApplyStatuses.Find(key));
            if ( currentStatus == null )
                return StatusNotFound;

            var newStatus = _prefetch.GetOrAdd(statusId, key => _appDbContext.ApplyStatuses.Find(key));
            if ( newStatus == null )
                return StatusNotFound;

            //if ( currentStatus.Id == newStatus.Id ) // Сам в себя
            //    return Yes(apply, statusId, pars);

            if ( !currentStatus.CanJumpTo(newStatus) )
                return NotAllowed;

            var newStatusEnum = newStatus.EnumStatus();

            // Закрыть можно из любого статуса
            if ( newStatusEnum == ApplyStatusEnum.DELETED )
                return Yes(apply, statusId, pars);

            if ( !string.IsNullOrEmpty(currentStatus.StatusEnumValue ))
                switch( currentStatus.EnumStatus()  )
                {
                    #region  Не проверено
                    case ApplyStatusEnum.NO_VALIDATED:
                        {
                            var ceResult = _checker.ContainsErrors(apply, pars);
                            if ( ceResult == ContainErrorsEnum.Email )
                                if ( newStatusEnum == ApplyStatusEnum.UNDERMANNED )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;

                            if ( ceResult == ContainErrorsEnum.Online )
                                if ( newStatusEnum == ApplyStatusEnum.HAS_ERRORS )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;

                            // ContainErrorsEnum.No
                            if (_checker.ContainsDouble(apply, pars))
                                if ( newStatusEnum == ApplyStatusEnum.SECOND_REQUEST)
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;

                            if ( newStatusEnum == ApplyStatusEnum.APPROVED )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        }
                    #endregion

                    #region Некомплект
                    case ApplyStatusEnum.UNDERMANNED:
                        {
                            if ( _checker.IsReturn(apply, pars) )
                                if ( newStatusEnum == ApplyStatusEnum.DELETED )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;

                            // Не возврат
                            if ( _checker.FullPackageSent(apply, pars))
                                if ( newStatusEnum == ApplyStatusEnum.APPROVED )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;

                            if ( newStatusEnum == ApplyStatusEnum.UNDERMANNED )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        }
                    #endregion

                    #region Содержит ошибки
                    case ApplyStatusEnum.HAS_ERRORS:
                        if ( newStatusEnum == ApplyStatusEnum.FIXED_BY_USER )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;
                    #endregion

                    #region Внесены изменения
                    case ApplyStatusEnum.FIXED_BY_USER:
                        {
                            if ( _checker.ContainsDouble(apply, pars) )
                                if ( newStatusEnum == ApplyStatusEnum.SECOND_REQUEST )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;

                            if ( newStatusEnum == ApplyStatusEnum.APPROVED )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        }
                    #endregion

                    #region Заявление принято
                    case ApplyStatusEnum.APPROVED:
                        if ( newStatusEnum == ApplyStatusEnum.ON_EXPERTIZE )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;
                    #endregion

                    #region Предварительная экспертиза
                    case ApplyStatusEnum.ON_EXPERTIZE:
                        if (_checker.IsProcedure(apply, pars))
                            if ( newStatusEnum == ApplyStatusEnum.ON_RESEARCH )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                        if ( newStatusEnum == ApplyStatusEnum.INFO_LETTER )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;
                    #endregion

                    #region Информационное письмо
                    case ApplyStatusEnum.INFO_LETTER:
                        if ( newStatusEnum == ApplyStatusEnum.ON_RESEARCH_END )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;
                    #endregion

                    #region На исследовании
                    case ApplyStatusEnum.ON_RESEARCH:
                        if ( !_checker.RequestRequired(apply, pars))
                            if ( newStatusEnum == ApplyStatusEnum.ON_RESEARCH_END )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                        if ( newStatusEnum == ApplyStatusEnum.SUSPENDED )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;

                    #endregion

                    #region Направлен запрос
                    case ApplyStatusEnum.SUSPENDED:
                        if ( _checker.ResponseReceived(apply, pars))
                            if ( newStatusEnum == ApplyStatusEnum.ON_RESEARCH_END )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                        if ( newStatusEnum == ApplyStatusEnum.SECOND_REQUEST )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;

                    #endregion

                    #region Направлен повторный запрос
                    case ApplyStatusEnum.SECOND_REQUEST:

                        if ( newStatusEnum == ApplyStatusEnum.ON_RESEARCH_END)
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;

                    #endregion

                    #region Рассмотрение завершено
                    case ApplyStatusEnum.ON_RESEARCH_END:

                        if ( _checker.ErrorsInExpertise(apply, pars) )
                            if ( newStatusEnum == ApplyStatusEnum.ON_RESEARCH )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                        if ( !_checker.IsPublicService(apply, pars) )
                            if ( newStatusEnum == ApplyStatusEnum.READY_TO_GIVE_INFOLETTER ) // Готов к выдаче, информационное письмо
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        else
                            if ( newStatusEnum == ApplyStatusEnum.DECISION_PREPARATION ) 
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                    #endregion

                    #region Готов к выдаче, информационное письмо
                    case ApplyStatusEnum.READY_TO_GIVE_INFOLETTER:

                        if ( newStatusEnum == ApplyStatusEnum.INFOLETTER_GIVEN )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;

                    #endregion

                    #region Подготовка решения
                    case ApplyStatusEnum.DECISION_PREPARATION:

                        if ( newStatusEnum == ApplyStatusEnum.DECISION_ACT )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;

                    #endregion

                    #region Подготовка акта
                    case ApplyStatusEnum.DECISION_ACT:

                        if ( newStatusEnum == ApplyStatusEnum.SIGNING_POSTED )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;

                    #endregion

                    #region На визировании
                    case ApplyStatusEnum.SIGNING_POSTED:

                        if ( _checker.ErrorsInExpertOpinion(apply, pars) )
                            if ( newStatusEnum == ApplyStatusEnum.DECISION_PREPARATION )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                        if ( !_checker.IsResultCertificate(apply, pars) )
                            if ( newStatusEnum == ApplyStatusEnum.REFUSAL_READY_TO_GIVE )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                        if ( !_checker.IsPaymentReceived(apply, pars) )
                            if ( newStatusEnum == ApplyStatusEnum.WAIT_PAYMENT )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;

                        #region PostPayment
                        return PostPayment(pars, apply, newStatusEnum);
                    #endregion

                    #endregion

                    #region Ожидание оплаты
                    case ApplyStatusEnum.WAIT_PAYMENT:
                        return PostPayment(pars, apply, newStatusEnum);
                    #endregion

                    #region Ожидание документов
                    case ApplyStatusEnum.WAIT_DOCUMENTS:
                        if ( _checker.DigitalCertificate(apply, pars) )
                            if ( newStatusEnum == ApplyStatusEnum.READY_TO_GIVE )
                                return Yes(apply, statusId, pars);
                            else
                                return Miscondition;
                        else
                            if ( newStatusEnum == ApplyStatusEnum.GIVEN_DOCS_IN_NIC )
                            return Yes(apply, statusId, pars);
                        else
                            return Miscondition;

                    #endregion

                    #region Готов к выдаче
                    case ApplyStatusEnum.READY_TO_GIVE:
                    case ApplyStatusEnum.GIVEN_DOCS_IN_NIC:
                        switch ( _checker.Transport(apply, pars) ) {
                            case ReceiveMethodEnum.Email:
                                if ( newStatusEnum == ApplyStatusEnum.PREPARE_TO_GIVE )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;
                            case ReceiveMethodEnum.Personal:
                                if ( newStatusEnum == ApplyStatusEnum.GIVEN )
                                    return Yes(apply, statusId, pars);
                                else
                                    return Miscondition;
                            default:
                                    return Miscondition;
                        }
                    #endregion

                    #region Варианты без доп. проверок
                    default:
                        return Yes(apply, statusId, pars);
                    #endregion
                }

            return Yes(apply, statusId, pars);
        }

        private string Yes(Apply apply, long status, string pars)
        {
            var prevRecord = _appDbContext.ApplyStatusHistories
                .Where(css => css.ApplyId == apply.Id)
                .OrderByDescending(cr => cr.ChangeTime)
                .FirstOrDefault();
            if ( prevRecord != null )
            {
                prevRecord.EndTime = DateTime.UtcNow;
                _appDbContext.ApplyStatusHistories.Update(prevRecord);
            }
            var historyRecord = new ApplyStatusHistory { Apply = apply, PrevStatus = apply.Status, StatusId = status, ChangeTime = DateTime.UtcNow, UserId = _identityService?.Identity?.Id };
            _appDbContext.ApplyStatusHistories.Add(historyRecord);
            apply.StatusId = status;
            var newDossier = new Dossier { Apply = apply };
            _appDbContext.Applies.Update(apply);
            _appDbContext.Dossiers.Add(newDossier);
            _appDbContext.SaveChanges();
            _mediator.Publish(new ApplyStatusChangedEvent(newDossier, pars));
            return string.Empty;
        }
    }

    public enum ContainErrorsEnum
    {
        No,
        Email,
        Online
    }
    public enum ApplyFormEnum
    {
        Personal,
        Mail,
        Online
    }
    public enum ReceiveMethodEnum
    {
        Personal,
        Email
    }
}
