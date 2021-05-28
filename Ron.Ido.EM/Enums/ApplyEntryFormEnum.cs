using Ron.Ido.Common.Attributes;

namespace Ron.Ido.EM.Enums
{
    [TypeScriptModule("enums")]
    public enum ApplyEntryFormEnum
    {
        SELF = 1,

        /// <summary>
        /// по почте
        /// </summary>
        MAIL = 2,
        /// <summary>
        /// личный кабинет
        /// </summary>
        CABINET = 3,
        /// <summary>
        /// ЕПГУ
        /// </summary>
        EPGU = 4,
        /// <summary>
        /// он-лайн
        /// </summary>
        ONLINE = 5
    }
}
