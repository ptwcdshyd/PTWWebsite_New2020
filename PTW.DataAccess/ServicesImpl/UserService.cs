using PTW.DataAccess.Models;
using PTW.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Text;
using PTW.DBAccess;
using System.Data;

namespace PTW.DataAccess.ServicesImpl
{
    public class UserService : IUserService
    {
        public DataTable Authenticate(LoginUser model)
        {

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
                    DataTable result = DBDataAccess.ExecuteTable(command);


                    return result;

                }

            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;



            }

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
                return main.regions;
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }



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
                return main.countries;
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
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
                return main.cities;
            }




            catch { throw; }



            finally
            {
                if (command != null) command.Dispose();
                command = null;

            }
        }

    }

}
