using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TextApp.EntityFrameworkContext;
using TextApp.Models;

namespace TextApp
{
    public class Dane
    {
        public List<TCars> cars { get; set; }
        public List<TClients> clients { get; set; }


        public Dane()
        {
            this.cars = new List<TCars>();
            this.clients = new List<TClients>();
        }
    }
}