namespace Shitter.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class StringIsWhiteSpacesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var input = value as string;
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}