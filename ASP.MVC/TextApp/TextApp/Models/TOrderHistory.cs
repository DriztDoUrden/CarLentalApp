using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TextApp.Models
{
    public class TOrderHistory
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public int Car_id { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
        public string AdditionalInformations { get; set; }
        public int AmountOfOrder { get; set; }
    }
}