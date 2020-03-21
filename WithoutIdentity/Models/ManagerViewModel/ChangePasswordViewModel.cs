using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WithoutIdentity.Models.ManagerViewModel
{
    public class ChangePasswordViewModel
    {
        [Required, DataType(dataType:DataType.Password), Display(Name ="Senha Atual")]
        public string OldPassword { get; set; }

        [Required, DataType(dataType: DataType.Password), Display(Name = "Nova Senha")]
        [StringLength(15,MinimumLength =4)]
        public string NewPassword { get; set; }

        [DataType(dataType: DataType.Password), Display(Name = "Confirmar Nova Senha")]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
