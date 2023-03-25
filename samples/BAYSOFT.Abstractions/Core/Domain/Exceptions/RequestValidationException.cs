using System;
using System.Runtime.Serialization;

namespace BAYSOFT.Abstractions.Core.Domain.Exceptions
{
    public class RequestValidationException : BaysoftException
    {
        public string SourceProperty { get; set; }
        public RequestValidationException(): base(400, 4001000)
        {
        }

        public RequestValidationException(string message) : base(400, 4001000, message)
        {
        }

        public RequestValidationException(string message, Exception innerException) : base(400, 4001000, message, innerException)
        {
        }

        protected RequestValidationException(SerializationInfo info, StreamingContext context) : base(400, 4001000, info, context)
        {
        }
        public RequestValidationException(string sourceProperty, string message) : base(400, 4001000, message)
        {
            SourceProperty = sourceProperty;
        }
        public RequestValidationException(int exceptionCode, int exceptionInternalCode) : base(exceptionCode, exceptionInternalCode)
        {
        }

        public RequestValidationException(int exceptionCode, int exceptionInternalCode, string message) : base(exceptionCode, exceptionInternalCode, message)
        {
        }

        public RequestValidationException(int exceptionCode, int exceptionInternalCode, string message, Exception innerException) : base(exceptionCode, exceptionInternalCode, message, innerException)
        {
        }

        protected RequestValidationException(int exceptionCode, int exceptionInternalCode, SerializationInfo info, StreamingContext context) : base(exceptionCode, exceptionInternalCode, info, context)
        {
        }
        public RequestValidationException(int exceptionCode, int exceptionInternalCode, string sourceProperty, string message) : base(exceptionCode, exceptionInternalCode, message)
        {
            SourceProperty = sourceProperty;
        }
    }
}
