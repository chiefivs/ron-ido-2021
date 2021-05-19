using System;
using System.Collections.Generic;

namespace Ron.Ido.Common.Logging
{
    [Serializable]
    public class ExceptionInfo
    {
        public Dictionary<object, object> Data { get; set; }
        public ExceptionInfo InnerException { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string Type { get; set; }

        public ExceptionInfo(Exception ex)
        {
            if (ex != null)
            {
                if (ex.Data != null)
                {
                    Data = new Dictionary<object, object>();
                    foreach (object key in ex.Data.Keys)
                    {
                        object val = ex.Data[key];
                        Data.Add(key, val);
                    }
                }

                if (ex.InnerException != null)
                {
                    InnerException = new ExceptionInfo(ex.InnerException);
                }

                Message = ex.Message;
                Source = ex.Source;
                StackTrace = ex.StackTrace;
                Type = ex.GetType().FullName;
            }
        }

        public override string ToString()
        {
            var parts = new List<string>()
            {
                Type,
                $"Message:'{Message}'",
                $"Source:'{Source}'",
                $"StackTrace:'{StackTrace}'"
            };

            if (InnerException != null)
                parts.Add($"InnerException:[{InnerException}]");

            return string.Join(" ", parts);
        }
    }
}
