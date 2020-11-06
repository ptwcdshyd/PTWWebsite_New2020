using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Text;
using PTW.DBAccess;
using System.Data;
using LoggerService;

namespace PTW.DataAccess.ServicesImpl
{
    public class UserService : IUserService
    {
        private readonly ILoggerManager _loggerManager;
        public UserService(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }
        public DataTable Authenticate(LoginUser model)
        {
            DataTable result = new DataTable();
            CustomCommand command = null;


            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UserLogin";

                    command.AddParameterWithValue("@UserName", model.UserName);
                    command.AddParameterWithValue("@Password", model.Password);

                    //  Execute command and get values from output parameters.
                    result = DBDataAccess.ExecuteTable(command);

                }

            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return result;

        }
        public void InsertSession(string Sessionid, string UserName)
        {
            CustomCommand command = null;
            try
            {
                // Define the command.
                command = new CustomCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertSession";
                // Assign the parameters.
                command.AddParameterWithValue("@Session", Sessionid);
                command.AddParameterWithValue("@username", UserName);
                //  Execute command and get values from output parameters.
                DBDataAccess.ExecuteNonQuery(command, true);
            }
            catch
            {
                throw;
            }
            finally
            {
                //Dispose the object.
                if (command != null) { command.Dispose(); }
            }
        }
        public List<Region> RetrieveRegionData()
        {
            CustomCommand command = null;
            Main main = new Main();
            try
            {
                using (command = new CustomCommand())
                {

                    main.regions = new List<Region>();

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Region";
                    //  Execute command and get values from output parameters.
                    DataTable result = DBDataAccess.ExecuteTable(command);
                    if (result != null && result.Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                Region obj1 = new Region();
                                obj1.RegionCode = "0";
                                obj1.Name = "Please select region";
                                main.regions.Add(obj1);
                            }
                            Region obj = new Region();
                            obj.RegionCode = result.Rows[i]["RegionCode"].ToString();
                            obj.Name = result.Rows[i]["Name"].ToString();
                            main.regions.Add(obj);
                        }

                    }



                }
                
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
            return main.regions;


        }
        public List<Country> RetrieveCountryData(string regioncode)
        {
            CustomCommand command = null;
            Main main = new Main();
            try
            {
                using (command = new CustomCommand())
                {

                    main.countries = new List<Country>();

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Country";
                    command.AddParameterWithValue("@regionCode", regioncode);
                    //  Execute command and get values from output parameters.
                    DataTable result = DBDataAccess.ExecuteTable(command);
                    if (result != null && result.Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                Country obj1 = new Country();
                                obj1.CountryCode = "0";
                                obj1.Name = "Please select Country";
                                main.countries.Add(obj1);
                            }
                            Country obj = new Country();
                            obj.CountryCode = result.Rows[i]["CountryCode"].ToString();
                            obj.Name = result.Rows[i]["Name"].ToString();
                            main.countries.Add(obj);
                        }

                    }



                }
                
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return main.countries;
        }

        public List<Citys> RetrieveCityData(string countrycode)
        {
            CustomCommand command = null;
            Main main = new Main();
            try
            {
                using (command = new CustomCommand())
                {

                    main.cities = new List<Citys>();

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveCitys";
                    command.AddParameterWithValue("@countryCode", countrycode);
                    //  Execute command and get values from output parameters.
                    DataTable result = DBDataAccess.ExecuteTable(command);
                    if (result != null && result.Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                Citys obj1 = new Citys();
                                obj1.Citycode = "0";
                                obj1.Name = "Please select City";
                                main.cities.Add(obj1);
                            }
                            Citys obj = new Citys();
                            obj.Citycode = result.Rows[i]["City"].ToString();
                            obj.Name = result.Rows[i]["Name"].ToString();
                            main.cities.Add(obj);
                        }

                    }



                }
                
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return main.cities;
        }
        public void InsertUserlog(string username, string sessionid)
        {
            CustomCommand command = null;

            try
            {
                using (command = new CustomCommand())
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "InsertUserlog";
                    command.AddParameterWithValue("@username", username);
                    command.AddParameterWithValue("@sessionid", sessionid);
                    //  Execute command and get values from output parameters.
                    DBDataAccess.ExecuteNonQuery(command, false);

                }

            }

            catch { throw; }

            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }
        public List<LocationDetails> RetrieveAllLocations()
        {
            CustomCommand command = null;
            List<LocationDetails> locationslist = new List<LocationDetails>();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveAllLocations";

                    DataTable result = DBDataAccess.ExecuteTable(command);

                    

                    if (result != null && result.Rows.Count > 0)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            LocationDetails obj = new LocationDetails();
                            obj.ID = Convert.ToInt32(result.Rows[i]["ID"]);
                            obj.Region = result.Rows[i]["Region"].ToString();
                            obj.Country = result.Rows[i]["Country"].ToString();
                            obj.Location = result.Rows[i]["Location"].ToString();
                            obj.Address = result.Rows[i]["Address"].ToString();
                            //obj.GoogleMapHeading = result.Rows[i]["GoogleMapHeading"].ToString();
                            //obj.GoogleMap = result.Rows[i]["GoogleMap"].ToString();
                            obj.Language = result.Rows[i]["LanguageCode"].ToString();
                            locationslist.Add(obj);

                        }
                    }
                }
            }
            catch { throw; }
            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return locationslist;
        }
        public LocationDetails GetLocationById(int locationId)
        {
            CustomCommand command = null;
            LocationDetails obj = new LocationDetails();
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "RetrieveLocationById";
                    command.AddParameterWithValue("@LocationId", locationId);
                    DataTable result = DBDataAccess.ExecuteTable(command);

                    

                    if (result != null && result.Rows.Count > 0)
                    {                        
                        obj.ID = Convert.ToInt32(result.Rows[0]["ID"]);
                        obj.Region = result.Rows[0]["Region"].ToString();
                        obj.Country = result.Rows[0]["Country"].ToString();
                        obj.Location = result.Rows[0]["Location"].ToString();
                        obj.Address = result.Rows[0]["Address"].ToString();
                        //obj.GoogleMapHeading = result.Rows[0]["GoogleMapHeading"].ToString();
                        obj.GoogleMap = result.Rows[0]["GoogleMap"].ToString();
                        obj.IsActive = Convert.ToBoolean(result.Rows[0]["IsActive"]);

                    }
                }
            }

            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return obj;
        }

       public int UpdateLocation(LocationDetails loc)
        {
            CustomCommand command = null;
            int resultcode = 0;
            try
            {
                using (command = new CustomCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateLocation";
                    command.AddParameterWithValue("@region", loc.Region);
                    command.AddParameterWithValue("@country", loc.Country);
                    command.AddParameterWithValue("@location", loc.Location);
                    command.AddParameterWithValue("@address", loc.Address);
                    command.AddParameterWithValue("@googleMap", loc.GoogleMap);
                    command.AddParameterWithValue("@id", loc.ID);
                    command.AddParameterWithValue("@isactive", loc.IsActive);
                    resultcode=DBDataAccess.ExecuteNonQuery(command,false);
                    
                }

                
            }
            catch { throw; }


            finally
            {
                if (command != null) command.Dispose();
                command = null;
            }
            return resultcode;
        }
            
    }

}
