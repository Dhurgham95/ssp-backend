﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ViewModels
{
    public class SuperAdminLoginViewModel
    {
        [Required]
        public string SuperAdminCode { get; set; }
        [Required]
        public string SuperAdminPassword { get; set; }
    }
}
