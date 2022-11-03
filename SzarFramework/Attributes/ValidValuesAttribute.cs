using System;

namespace SzarFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class ValidValuesAttribute : Attribute
    {
        public string Value { get; set; }
        public string Description { get; set; }
        
        public ValidValuesAttribute(string value, string description)
        {
            this.Value = value;
            this.Description = description;
        }


    }
}
