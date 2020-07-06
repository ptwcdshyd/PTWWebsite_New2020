using Microsoft.AspNetCore.Http;
using PTW.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PTW.DataAccess.Services
{
    public interface IAboutServices
    {

        List<AboutModel> GetAboutProfile(string Languagecode);

        List<AboutModel> GetProfile();

        List<Language> GetLanguages();

        AboutModel GetProfileByProfileId(int ProfileId,string LanguageCode);

        bool UpdateProfileByProfileId(AboutModel objabout);

        bool AddProfileByProfileId(AboutModel objabout);
    }
}
