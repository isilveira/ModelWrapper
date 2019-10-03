using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ModelWrapper.Binders
{
    public class WrapRequestMemberBinder : SetMemberBinder
    {
        public WrapPropertySource Source { get; set; }

        public WrapRequestMemberBinder(string name, WrapPropertySource source, bool ignoreCase) : base(name, ignoreCase)
        {
            Source = source;
        }

        public WrapRequestMemberBinder(string name, bool ignoreCase) : base(name, ignoreCase)
        {
            Source = WrapPropertySource.FromBody;
        }

        public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
