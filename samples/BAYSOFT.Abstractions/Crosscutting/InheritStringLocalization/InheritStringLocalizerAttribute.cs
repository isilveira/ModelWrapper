using System;

namespace BAYSOFT.Abstractions.Crosscutting.InheritStringLocalization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class InheritStringLocalizerAttribute : Attribute
    {
        public InheritStringLocalizerAttribute(Type inheritFrom)
        {
            InheritFrom = inheritFrom ?? throw new ArgumentNullException(nameof(inheritFrom));
        }

        public Type InheritFrom { get; private set; }
        public int Priority { get; set; }
    }
}
