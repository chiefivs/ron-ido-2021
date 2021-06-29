namespace Ron.Ido.EM.Enums
{
    public enum ApplyStatusEnum
    {

        /// <summary>
        /// не проверена
        /// </summary>
        NO_VALIDATED = 1,

        /// <summary>
        /// содержит ошибки
        /// </summary>
        HAS_ERRORS = 2,

        /// <summary>
        /// некомплект
        /// </summary>
        UNDERMANNED = 3,

        /// <summary>
        /// внесены изменения заявителем
        /// </summary>
        FIXED_BY_USER = 4,

        /// <summary>
        /// одобрена модератором, заявление принято
        /// </summary>
        APPROVED = 5,

        /// <summary>
        /// на исследовании, назначается после предварительной экспертизы
        /// </summary>
        ON_RESEARCH = 10,

        /// <summary>
        /// рассмотрение завершено
        /// </summary>
        ON_RESEARCH_END = 11,

        /// <summary>
        /// предварительная экспертиза, 
        /// проводится предварительная экспертиза назначается при назначении эксперту
        /// или передаче от специалиста по запросам к эксперту
        /// </summary>
        ON_EXPERTIZE = 12,

        /// <summary>
        /// приостановлено, (условно-завершенные) назначается при передаче от эксперта специалисту по запросам
        /// </summary>
        SUSPENDED = 13,

        /// <summary>
        /// подготовка решения, по завершении при передаче от эксперта менеджеру
        /// </summary>
        DECISION_PREPARATION = 14,

        /// <summary>
        /// подготовка акта
        /// </summary>
        DECISION_ACT = 15,

        /// <summary>
        /// визирование документы переданы, ожидают получения
        /// </summary>
        SIGNING_POSTED = 16,

        /// <summary>
        /// информационное письмо
        /// </summary>
        INFO_LETTER = 17,

        /// <summary>
        /// повторный запрос
        /// </summary>
        SECOND_REQUEST = 18,

        /// <summary>
        /// Готов к выдаче, информационное письмо (новый)
        /// </summary>
        READY_TO_GIVE_INFOLETTER = 22,

        /// <summary>
        /// готов к выдаче
        /// </summary>
        READY_TO_GIVE = 23,

        /// <summary>
        /// подготовка к выдаче
        /// </summary>
        PREPARE_TO_GIVE = 24,

        /// <summary>
        /// ожидание оплаты
        /// </summary>
        WAIT_PAYMENT = 32,

        /// <summary>
        /// ожидание документов
        /// </summary>
        WAIT_DOCUMENTS = 33,

        /// <summary>
        /// Отказ готов к выдаче (новый)
        /// </summary>
        REFUSAL_READY_TO_GIVE = 36,

        /// <summary>
        /// Выдано, документы в ГЭЦ (новый)
        /// </summary>
        GIVEN_DOCS_IN_NIC = 39,

        /// <summary>
        /// выдано информационное письмо (новый)
        /// </summary>
        INFOLETTER_GIVEN = 40,
        /// <summary>
        /// выдан
        /// </summary>
        GIVEN = 41,


        /// <summary>
        /// дело прекращено
        /// </summary>
        DELETED = 99
    }
}
