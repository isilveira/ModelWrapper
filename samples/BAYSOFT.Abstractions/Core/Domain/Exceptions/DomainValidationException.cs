using System;
using System.Runtime.Serialization;

namespace BAYSOFT.Abstractions.Core.Domain.Exceptions
{
    public class DomainValidationException : BaysoftException
    {
        public DomainValidationException() : base(400, 4003000)
        {
        }

        public DomainValidationException(string message) : base(400, 4003000, message)
        {
        }

        public DomainValidationException(string message, Exception innerException) : base(400, 4003000, message, innerException)
        {
        }

        protected DomainValidationException(SerializationInfo info, StreamingContext context) : base(400, 4003000, info, context)
        {
        }
        public DomainValidationException(int exceptionCode, int exceptionInternalCode) : base(exceptionCode, exceptionInternalCode)
        {
        }

        public DomainValidationException(int exceptionCode, int exceptionInternalCode, string message) : base(exceptionCode, exceptionInternalCode, message)
        {
        }

        public DomainValidationException(int exceptionCode, int exceptionInternalCode, string message, Exception innerException) : base(exceptionCode, exceptionInternalCode, message, innerException)
        {
        }

        protected DomainValidationException(int exceptionCode, int exceptionInternalCode, SerializationInfo info, StreamingContext context) : base(exceptionCode, exceptionInternalCode, info, context)
        {
        }
    }
}
