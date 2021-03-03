using licenta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace licenta.ViewModel
{
    internal class MinOneCorrectAnswerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool ok = false;
            var answers = (List<Answer>)value;
            foreach(var answer in answers)
            {
                if (answer.IsCorrect)
                    ok = true;
            }
             return (ok)
                ? ValidationResult.Success
                : new ValidationResult("Ar trebuie sa ai cel putin un răspuns corect.");
        }
    }
}