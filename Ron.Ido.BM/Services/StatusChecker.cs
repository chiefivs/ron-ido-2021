using Ron.Ido.BM.Extensions;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using System;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public interface IStatusChecker
    {
        /// <summary>
        /// Содержит ошибки
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        ContainErrorsEnum ContainsErrors(Apply apply, string pars);
        /// <summary>
        /// Возврат заявителю
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool IsReturn(Apply apply, string pars);
        /// <summary>
        /// Содержит дубликат
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool ContainsDouble(Apply apply, string pars);
        /// <summary>
        /// Дослали полный пакет
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool FullPackageSent(Apply apply, string pars);
        /// <summary>
        /// Процедура?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool IsProcedure(Apply apply, string pars);
        /// <summary>
        /// Требуется запрос?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool RequestRequired(Apply apply, string pars);
        /// <summary>
        /// Получен ответ?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool ResponseReceived(Apply apply, string pars);
        /// <summary>
        /// В экспертизе найдены ошибки?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool ErrorsInExpertise(Apply apply, string pars);
        /// <summary>
        /// Государственная услуга?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool IsPublicService(Apply apply, string pars);
        /// <summary>
        /// Ошибки в экспертном заключении?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool ErrorsInExpertOpinion(Apply apply, string pars);
        /// <summary>
        /// результат-свидетельство?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool IsResultCertificate(Apply apply, string pars);
        /// <summary>
        /// Оплата поступила?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool IsPaymentReceived(Apply apply, string pars);
        /// <summary>
        /// Электронное свидетельство?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool DigitalCertificate(Apply apply, string pars);
        /// <summary>
        /// Форма приёма
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        ApplyFormEnum AdmissionForm(Apply apply, string pars);
        /// <summary>
        /// Полный комплект?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        bool IsFullSet(Apply apply, string pars);
        /// <summary>
        /// Способ получения
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <param name="pars">Дополнительные параметры</param>
        /// <returns>результат проверки</returns>
        ReceiveMethodEnum Transport(Apply apply, string pars);
    }

        public class StatusChecker : IStatusChecker
        {
            public StatusChecker(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            AppDbContext _dbContext;
            public ApplyFormEnum AdmissionForm(Apply apply, string pars)
            {
                if ( apply.DeliveryForm.Name.In("self", "courier") )
                    return ApplyFormEnum.Personal;

                if ( apply.DeliveryForm.Name == "federal mail" )
                    return ApplyFormEnum.Mail;

                return ApplyFormEnum.Online;
            }

            public bool ContainsDouble(Apply apply, string pars)
            {
                return _dbContext.Applies.Any(z => z.CreatorEmail == apply.CreatorEmail && z.Id != apply.Id);
            }

            public ContainErrorsEnum ContainsErrors(Apply apply, string pars)
            {
                return ContainErrorsEnum.No;
                //throw new NotImplementedException();

            }

            public bool DigitalCertificate(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool ErrorsInExpertise(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool ErrorsInExpertOpinion(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool FullPackageSent(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool IsFullSet(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool IsPaymentReceived(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool IsProcedure(Apply apply, string pars)
            {
                return !apply.ForInfoLetter;
            }

            public bool IsPublicService(Apply apply, string pars)
            {
                return !string.IsNullOrEmpty(apply.EpguCode);
            }

            public bool IsResultCertificate(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool IsReturn(Apply apply, string pars)
            {
                return apply.ReturnOriginalsForm != null;
            }

            public bool RequestRequired(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public bool ResponseReceived(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }

            public ReceiveMethodEnum Transport(Apply apply, string pars)
            {
                throw new NotImplementedException();
            }
        }

}
