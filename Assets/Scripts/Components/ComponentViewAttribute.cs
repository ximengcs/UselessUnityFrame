
using System;

namespace TestGame
{
    public class ComponentViewAttribute : Attribute
    {
        public Type Type { get; }

        public ComponentViewAttribute(Type type)
        {
            Type = type;
        }
    }
}
