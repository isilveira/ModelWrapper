using System;
using System.Dynamic;

namespace ModelWrapper.Binders
{
    /// <summary>
    /// Class that binds properties from request into WrapRequest keeping a reference of the source
    /// </summary>
    public class WrapRequestMemberBinder : SetMemberBinder
    {
        /// <summary>
        /// Bind source
        /// </summary>
        public WrapPropertySource Source { get; set; }
        /// <summary>
        /// WrapRequestMemberBinder constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="source">Property source binder</param>
        /// <param name="ignoreCase">Indication if case must be ignored</param>
        public WrapRequestMemberBinder(
            string name,
            WrapPropertySource source,
            bool ignoreCase
        ) : base(name, ignoreCase)
        {
            Source = source;
        }
        /// <summary>
        /// WrapRequestMemberBinder constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="ignoreCase">Indication if case must be ignored</param>
        public WrapRequestMemberBinder(
            string name,
            bool ignoreCase
        ) : base(name, ignoreCase)
        {
            Source = WrapPropertySource.FromBody;
        }
        /// <summary>
        /// Method FallbackSetMember not implemented
        /// </summary>
        /// <param name="target">DynamicMetaObject target</param>
        /// <param name="value">DynamicMetaObject value</param>
        /// <param name="errorSuggestion">DynamicMetaObject errorSuggestion</param>
        /// <returns>DynamicMetaObject</returns>
        public override DynamicMetaObject FallbackSetMember(
            DynamicMetaObject target,
            DynamicMetaObject value,
            DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
