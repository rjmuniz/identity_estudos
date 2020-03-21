using System;
using System.ComponentModel.DataAnnotations;

namespace WithoutIdentity.Models.AccountViewModel{
    public class RegisterViewModel{
        [Required, EmailAddress, Display(Name="E-mail")]
        public string Email{get;set;}

        [Required, DataType(DataType.Password), Display(Name="Senha")]
        [StringLength(15, ErrorMessage ="O campo {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength=4)]
        public string Password { get;set;}
        
        [Required, DataType(DataType.Password), Display(Name="Confirmar Senha")]
        [Compare("Password", ErrorMessage="As Senhas devem ser iguais")]
        public string ConfirmPassword { get;set;}
         
    }

}