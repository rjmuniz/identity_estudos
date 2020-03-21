﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WithoutIdentity.Models.ManagerViewModel
{
    public class IndexViewModel
    {
        public string UserName { get; set; }
        public bool IsEmailConfirmed{ get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone, Display(Name ="Número do telefone")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
