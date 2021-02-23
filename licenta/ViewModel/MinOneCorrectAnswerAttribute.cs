using System;
using System.ComponentModel.DataAnnotations;

namespace licenta.ViewModel
{
    internal class MinOneCorrectAnswerAttribute : ValidationAttribute
    {
        public string ErrorMessage { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}