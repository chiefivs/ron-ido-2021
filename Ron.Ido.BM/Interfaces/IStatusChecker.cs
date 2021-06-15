using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;

namespace Ron.Ido.BM.Interfaces
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

}
