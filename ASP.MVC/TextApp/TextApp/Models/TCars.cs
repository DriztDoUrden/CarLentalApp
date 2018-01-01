using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TextApp.Models
{
    public class TCars
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Mark { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Model { get; set; }
        [Range(1,99999,ErrorMessage = "Value for {0} must be between {1} and {2}."), Required(ErrorMessage = "This field is reguired")]
        public Nullable<int> Price { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(150, ErrorMessage = "Maksimum length is 50")]
        public string ImageSource { get; set; }
        [Range(1, 2100, ErrorMessage = "Value for {0} must be between {1} and {2}."), Required(ErrorMessage = "This field is reguired")]
        public Nullable<int> Year { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Engine { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2"), MaxLength(50, ErrorMessage = "Maksimum length is 50"), Required(ErrorMessage = "This field is reguired")]
        public string Transmission { get; set; }
        [Range(1, 20, ErrorMessage = "Value for {0} must be between {1} and {2}."), Required(ErrorMessage = "This field is reguired")]
        public Nullable<int> Airbag { get; set; }
        public Nullable<bool> IsAvaiable { get; set; }
        [Range(1, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}."), Required(ErrorMessage = "This field is reguired")]
        public Nullable<int> NumberOf { get; set; }
    }
}