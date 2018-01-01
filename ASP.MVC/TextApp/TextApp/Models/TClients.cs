using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TextApp.Models
{
    public partial class TClients
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"),MaxLength(50, ErrorMessage ="Maksimum length is 50"),Required (ErrorMessage ="This field is reguired")]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Surname { get; set; }
        [RegularExpression("^([0-9]{9})|(([0-9]{3}-){2}[0-9]{3})$", ErrorMessage ="Invalid format")]
        public string Telephone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Password { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Function { get; set; }
        public string Avatar { get; set; }

    }
}