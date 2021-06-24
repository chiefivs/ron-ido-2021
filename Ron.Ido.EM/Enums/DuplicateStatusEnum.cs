namespace Ron.Ido.EM.Enums
{
    public enum DuplicateStatusEnum
    {
        /// <summary>
        /// Заявление создано
        /// </summary>
        DUPLICATE_CREATED = 1,
        /// <summary>
        /// Заявление принято
        /// </summary>
        DUPLICATE_APPROVED = 2,
        /// <summary>
        /// Подготовка решения
        /// </summary>
        DECISION_PREPARATION = 3,
        /// <summary>
        /// Ожидание оплаты
        /// </summary>
        WAITING_PAYMENT = 4,
        /// <summary>
        /// Готов к выдаче
        /// </summary>
        READY_TO_GIVE = 5,
        /// <summary>
        /// Отказ в выдаче дубликата
        /// </summary>
        DECLINED_TO_GIVE = 6,
        /// <summary>
        /// Выдан
        /// </summary>
        GIVEN = 7
    }
}
