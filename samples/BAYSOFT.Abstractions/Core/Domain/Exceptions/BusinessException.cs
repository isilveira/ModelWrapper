using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BAYSOFT.Abstractions.Core.Domain.Exceptions
{
    public class BusinessException : BaysoftException
    {
        public List<RequestValidationException> RequestExceptions { get; set; }
        public List<EntityValidationException> EntityExceptions { get; set; }
        public List<DomainValidationException> DomainExceptions { get; set; }
        public BusinessException():base(400, 4000000)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(string message) : base(400, 4000000, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(string message, Exception innerException) : base(400, 4000000, message, innerException)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(string message, List<RequestValidationException> requestExceptions) : base(400, 4001000, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(string message, List<EntityValidationException> entityExceptions) : base(400, 4002000, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException();
        }

        public BusinessException(string message, List<DomainValidationException> domainExceptions) : base(400, 4003000, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException(domainExceptions);
        }

        public BusinessException(string message, List<EntityValidationException> entityExceptions, List<DomainValidationException> domainExceptions) : base(400, 4004100, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException(domainExceptions);
        }

        public BusinessException(string message, List<RequestValidationException> requestExceptions, List<DomainValidationException> domainExceptions) : base(400, 4004200, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException();
            InitializeDomainValidationException(domainExceptions);
        }

        public BusinessException(string message, List<RequestValidationException> requestExceptions, List<EntityValidationException> entityExceptions) : base(400, 4004300, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException();
        }

        public BusinessException(string message, List<RequestValidationException> requestExceptions, List<EntityValidationException> entityExceptions, List<DomainValidationException> domainExceptions) : base(400, 4005000, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException(domainExceptions);
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(400, 4000000, info, context)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }
        public BusinessException(int exceptionCode, int exceptionInternalCode) : base(exceptionCode, exceptionInternalCode)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, Exception innerException) : base(exceptionCode, exceptionInternalCode, message, innerException)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, List<RequestValidationException> requestExceptions) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, List<EntityValidationException> entityExceptions) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException();
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, List<DomainValidationException> domainExceptions) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException(domainExceptions);
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, List<EntityValidationException> entityExceptions, List<DomainValidationException> domainExceptions) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException(domainExceptions);
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, List<RequestValidationException> requestExceptions, List<DomainValidationException> domainExceptions) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException();
            InitializeDomainValidationException(domainExceptions);
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, List<RequestValidationException> requestExceptions, List<EntityValidationException> entityExceptions) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException();
        }

        public BusinessException(int exceptionCode, int exceptionInternalCode, string message, List<RequestValidationException> requestExceptions, List<EntityValidationException> entityExceptions, List<DomainValidationException> domainExceptions) : base(exceptionCode, exceptionInternalCode, message)
        {
            InitializeRequestValidationException(requestExceptions);
            InitializeEntityValidationException(entityExceptions);
            InitializeDomainValidationException(domainExceptions);
        }

        protected BusinessException(int exceptionCode, int exceptionInternalCode, SerializationInfo info, StreamingContext context) : base(exceptionCode, exceptionInternalCode, info, context)
        {
            InitializeRequestValidationException();
            InitializeEntityValidationException();
            InitializeDomainValidationException();
        }

        private void InitializeRequestValidationException(List<RequestValidationException> exceptions = null)
        {
            RequestExceptions = new List<RequestValidationException>();
            if (exceptions != null&& exceptions.Count>0)
            {
                RequestExceptions.AddRange(exceptions);
            }
        }

        private void InitializeEntityValidationException(List<EntityValidationException> exceptions = null)
        {
            EntityExceptions = new List<EntityValidationException>();
            if (exceptions != null && exceptions.Count > 0)
            {
                EntityExceptions.AddRange(exceptions);
            }
        }

        private void InitializeDomainValidationException(List<DomainValidationException> exceptions = null)
        {
            DomainExceptions = new List<DomainValidationException>();
            if (exceptions != null && exceptions.Count > 0)
            {
                DomainExceptions.AddRange(exceptions);
            }
        }
    }
}
