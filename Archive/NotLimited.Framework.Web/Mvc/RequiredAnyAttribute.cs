using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Mvc
{
    public class RequiredAnyAttribute : RequiredAttribute, IClientValidatable
    {
        public RequiredAnyAttribute(string jsValidationFunction, params string[] additionalProperties)
        {
            JSValidationFunction = jsValidationFunction;
            AdditionalProperties = additionalProperties;
        }

        public string[] AdditionalProperties { get; }
        public string JSValidationFunction { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = base.IsValid(value, validationContext);
            if (result == ValidationResult.Success)
            {
                return result;
            }

            foreach (var propName in AdditionalProperties)
            {
                var prop = validationContext.ObjectType.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (prop == null)
                {
                    continue;
                }

                var propValue = prop.GetValue(validationContext.ObjectInstance);
                if (base.IsValid(propValue))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(FormatErrorMessage(validationContext.MemberName), AdditionalProperties);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new List<ModelClientValidationRule>
                   {
                       new ModelClientValidationRule
                       {
                           ErrorMessage = ErrorMessage,
                           ValidationType = JSValidationFunction
                       }
                   };
        }
    }
}