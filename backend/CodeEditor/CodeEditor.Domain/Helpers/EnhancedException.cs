using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace CodeEditor.Domain.Helpers
{
    public class EnhancedException : Exception
    {
        public int ErrorCode { get; }

        public string FunctionName { get; set; }

        public List<string>? Arguments { get; set; }

        public EnhancedException(
            string message,
            List<string>? arguments = null,
            [CallerMemberName] string functionName = ""
            ) : base(message) 
        {
            FunctionName = functionName;
            Arguments = arguments;
        }

        public EnhancedException(
            string message,
            int errorCode,
            List<string>? arguments = null,
            [CallerMemberName] string functionName = ""
            ) : base(message)
        {

            ErrorCode = errorCode;
            FunctionName = functionName;
            Arguments = arguments;
        }

        public EnhancedException(
            string message, 
            Exception innerException, 
            List<string>? arguments = null,
            [CallerMemberName] string functionName = "")
            : base(message, innerException) 
        {

            FunctionName = functionName;
            Arguments = arguments;
        }
    }
}
