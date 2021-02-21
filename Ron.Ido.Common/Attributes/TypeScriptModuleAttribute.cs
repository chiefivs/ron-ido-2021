using System;

namespace Ron.Ido.Common.Attributes
{
    public class TypeScriptModuleAttribute: Attribute
    {
        public string Name { get; private set; }

        public TypeScriptModuleAttribute(string name)
        {
            Name = name;
        }
    }
}
