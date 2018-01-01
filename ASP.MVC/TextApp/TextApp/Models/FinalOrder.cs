using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TextApp.EntityFrameworkContext;

namespace TextApp.Models
{
    public partial class FinalOrder
    {
        public TClients client { get; set; }
        public TCars car { get; set; }
        public int TotalPrice { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public bool flag = false;
        public string Text { get; set; }
    }
}