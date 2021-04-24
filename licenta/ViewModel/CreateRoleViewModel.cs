using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licenta.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Numele rolului")]
        public string RoleName { get; set; }
    }
}
