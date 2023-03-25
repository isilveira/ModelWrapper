using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAYSOFT.Abstractions.Core.Domain.Entities
{
    public class DomainEntity<TIdType> : DomainEntity
    {
        public TIdType Id { get; set; }
    }

    public class DomainEntity
    {
        public void Update(DomainEntity updatedEntity)
        {
            this.GetType().GetProperties().ToList().ForEach(property => property.SetValue(this, updatedEntity.GetType().GetProperty(property.Name).GetValue(updatedEntity)));
        }
    }
}
