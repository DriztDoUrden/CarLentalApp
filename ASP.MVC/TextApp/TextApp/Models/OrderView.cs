using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TextApp.Models
{
    public class OrderView
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
        public string AdditionalInformations { get; set; }
        public int AmountOfOrder { get; set; }
    }
}