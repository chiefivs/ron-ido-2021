using System.Text.Json;

namespace Ron.Ido.Common.Logging
{
    public class LogItem
    {
        public string Type { get; private set; }
        public string Message { get; set; }
        public ExceptionInfo Exception { get; set; }

        public LogItem()
        {
            Type = GetType().FullName;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, GetType());
        }
    }
}
