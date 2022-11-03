using System;

namespace SzarFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class FormAttribute : Attribute
    {
        public string formUid;
        public string description;

        public FormAttribute(string formUid, string description)
        {
            this.formUid = formUid;
            this.description = description;
        }

    }
}
