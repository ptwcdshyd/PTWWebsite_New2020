using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Services
{
    public interface INewsEventService
    {
        List<News> GetAllNewsDetails();
        List<News> GetAllEventDetails();


    }
}
