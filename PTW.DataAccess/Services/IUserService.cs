using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PTW.DataAccess.Services
{
   public interface IUserService
    {
        DataTable Authenticate(LoginUser model);
       void InsertSession(string Sessionid, string UserName);
        List<Region> RetrieveRegionData();
        List<Country> RetrieveCountryData(string regioncode);
        List<Citys> RetrieveCityData(string countrycode);
    }
}
