using System;
using System.ComponentModel.DataAnnotations;

namespace NotLimited.Framework.Common.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinNumberAttribute : ValidationAttribute
    {
        private const string DefaultError = "'{0}' should be at least {1}.";

        public MinNumberAttribute(long minValue) : base(DefaultError)
        {
            MinValue = minValue;
        }

        public long MinValue { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            long number;
            try
            {
                number = Convert.ToInt64(value);
            }
            catch
            {
                return false;
            }

            return number >= MinValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(ErrorMessageString, name);
        }
    }
}