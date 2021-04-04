using Store.Core.Domain.Entities.Default;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Validations.EntityValidations.Default
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
        }
    }
}
