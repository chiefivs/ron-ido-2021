using Ron.Ido.Common.Attributes;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public enum ODataFilterTypeEnum
    {
        Equals,
        NotEquals,
        LessThan,
        GreatThan,
        LessThanOrEqual,
        GreatThanOrEqual,
        In,
        BetweenNone,
        BetweenLeft,
        BetweenRight,
        BetweenAll,
        Starts,
        Contains
    }
}
