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
        /// <returns>результат проверки</returns>
        ContainErrorsEnum ContainsErrors(Apply apply);
        /// <summary>
        /// Возврат заявителю
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool IsReturn(Apply apply);
        /// <summary>
        /// Содержит дубликат
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool ContainsDouble(Apply apply);
        /// <summary>
        /// Дослали полный пакет
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool FullPackageSent(Apply apply);
        /// <summary>
        /// Процедура?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool IsProcedure(Apply apply);
        /// <summary>
        /// Требуется запрос?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool RequestRequired(Apply apply);
        /// <summary>
        /// Получен ответ?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool ResponseReceived(Apply apply);
        /// <summary>
        /// В экспертизе найдены ошибки?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool ErrorsInExpertise(Apply apply);
        /// <summary>
        /// Государственная услуга?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool IsPublicService(Apply apply);
        /// <summary>
        /// Ошибки в экспертном заключении?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool ErrorsInExpertOpinion(Apply apply);
        /// <summary>
        /// результат-свидетельство?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool IsResultCertificate(Apply apply);
        /// <summary>
        /// Оплата поступила?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool IsPaymentReceived(Apply apply);
        /// <summary>
        /// Электронное свидетельство?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool DigitalCertificate(Apply apply);
        /// <summary>
        /// Форма приёма
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        ApplyFormEnum AdmissionForm(Apply apply);
        /// <summary>
        /// Полный комплект?
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        bool IsFullSet(Apply apply);
        /// <summary>
        /// Способ получения
        /// </summary>
        /// <param name="apply">Заявка</param>
        /// <returns>результат проверки</returns>
        ReceiveMethodEnum Transport(Apply apply);
    }

        public class StatusChecker : IStatusChecker
        {
            public StatusChecker(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            AppDbContext _dbContext;
            public ApplyFormEnum AdmissionForm(Apply apply)
            {
                if ( apply.DeliveryForm.Name.In("self", "courier") )
                    return ApplyFormEnum.Personal;

                if ( apply.DeliveryForm.Name == "federal mail" )
                    return ApplyFormEnum.Mail;

                return ApplyFormEnum.Online;
            }

            public bool ContainsDouble(Apply apply)
            {
                return _dbContext.Applies.Any(z => z.CreatorEmail == apply.CreatorEmail && z.Id != apply.Id);
            }

            public ContainErrorsEnum ContainsErrors(Apply apply)
            {
                return ContainErrorsEnum.No;
                //throw new NotImplementedException();

            }

            public bool DigitalCertificate(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool ErrorsInExpertise(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool ErrorsInExpertOpinion(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool FullPackageSent(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool IsFullSet(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool IsPaymentReceived(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool IsProcedure(Apply apply)
            {
                return !apply.ForInfoLetter;
            }

            public bool IsPublicService(Apply apply)
            {
                return !string.IsNullOrEmpty(apply.EpguCode);
            }

            public bool IsResultCertificate(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool IsReturn(Apply apply)
            {
                return apply.ReturnOriginalsForm != null;
            }

            public bool RequestRequired(Apply apply)
            {
                throw new NotImplementedException();
            }

            public bool ResponseReceived(Apply apply)
            {
                throw new NotImplementedException();
            }

            public ReceiveMethodEnum Transport(Apply apply)
            {
                throw new NotImplementedException();
            }
        }

}
