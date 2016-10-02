using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParentsBankWebApp_Group.Models
{
    public class vldDateNotFuture : ValidationAttribute
    {
        //The transaction date cannot be in the future
        //The transaction date cannot be before the current year
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateValue = (DateTime)value;
            if (dateValue > DateTime.Today)
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }
    }
}