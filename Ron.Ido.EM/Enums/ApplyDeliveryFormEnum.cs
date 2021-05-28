using Ron.Ido.Common.Attributes;

namespace Ron.Ido.EM.Enums
{
    [TypeScriptModule("enums")]
    public enum ApplyDeliveryFormEnum
    {
        /// <summary>
        /// Забрать лично
        /// </summary>
        SELF = 1,
        /// <summary>
        /// Доставка курьером (за счет заявителя)
        /// </summary>
        COURIER = 2,
        /// <summary>
        /// Доставка по почте (за счет Федеральной службы)
        /// </summary>
        POST = 3
    }
}
