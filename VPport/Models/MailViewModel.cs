using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VPport.Models
{
    public class MailViewModel
    {
        [Required(ErrorMessage = "Feltet må ikke være tomt")]
        [EmailAddress(ErrorMessage = "Du skal indtaste en gyldig email")]
        public string MailEmail { get; set; }

        [Required(ErrorMessage = "Feltet må ikke være tomt")]
        [MinLength(8, ErrorMessage = "Skal indholde mindst 8 tal")]
        public string MailPhone { get; set; }

        [Required(ErrorMessage = "Feltet må ikke være tomt")]
        [MinLength(10, ErrorMessage = "Skal indholde mindst 10 tegn")]
        public string MailMessage { get; set; }

        [Required(ErrorMessage = "Feltet må ikke være tomt")]
        [MinLength(2, ErrorMessage = "Skal indholde mindst 2 tegn")]
        public string MailName { get; set; }
    }
}