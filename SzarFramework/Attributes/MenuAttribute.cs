using System;

namespace SzarFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class MenuAttribute : Attribute
    {
        public string menuUid;
        public string description;

        public MenuAttribute(string menuUid, string description)
        {
            this.menuUid = menuUid;
            this.description = description;
        }
    }
}
