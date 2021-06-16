using Ron.Ido.Common.Attributes;

namespace Ron.Ido.BM.Models.OData
{
    [TypeScriptModule("odata")]
    public class ODataOption
    {
        public object Value { get; set; }
        public string Text { get; set; }
        public object Parent { get; set; }

        public ODataOption() { }

        public ODataOption(string text, object value)
        {
            Text = text;
            Value = value;
        }
    }
}
