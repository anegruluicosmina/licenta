using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.Models
{
    internal class NrOfWrongAnsweresQst: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var category = (Category)validationContext.ObjectInstance;
            return (category.NumberOfQuestions > category.NumberOfWrongQuestions)
               ? ValidationResult.Success
               : new ValidationResult("Numărul de întrebari dintr-un test ar trebui să fie mai mare decât numărul de întrebări la care se poate greși.");
        }
    }
}
