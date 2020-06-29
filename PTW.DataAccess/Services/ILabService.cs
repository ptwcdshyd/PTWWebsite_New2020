﻿using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTW.DataAccess.Services
{
    public interface ILabService
    {
        List<Labs> GetAllLabsDetails(string LanguageCode);

        List<Labs> GetAllLatestInsights(string LanguageCode);

        List<Labs> GetSlider();
    }
}
