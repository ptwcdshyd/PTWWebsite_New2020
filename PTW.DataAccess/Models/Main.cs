using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Models
{
  public class Main
    {
        public List<Region> regions { set; get; }
        public List<Country> countries { set; get; }
        public List<Citys> cities { set; get; }
    }
    public class Country
    {
        public string CountryCode { get; set; }
        public string Name { get; set; }
    }
    public class Citys
    {
        public string Citycode { get; set; }
        public string Name { get; set; }
    }
}
