using System;

namespace licenta.ViewModel
{
    internal class MinOneCorrectAnswerAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}