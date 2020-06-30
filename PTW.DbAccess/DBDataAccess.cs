using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;

namespace PTW.DBAccess
{
    public abstract class DBDataAccess
    {
        private static IConfiguration Configuration { get; set; }
        private static string ConnectionString { get; set; } //To Read ConnectionString from appsettings.json file  
        private static string GetConnectionString()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string connectionString = Configuration["ConnectionStrings:PTWWebsiteConnection"];

            // Create instance.
            MySqlConnectionStringBuilder sqlbuilder = new MySqlConnectionStringBuilder(connectionString);

            sqlbuilder.UserID = UtilityHelper.Decrypt(sqlbuilder.UserID);

            sqlbuilder.Password = UtilityHelper.Decrypt(sqlbuilder.Password);

            sqlbuilder.Server = UtilityHelper.Decrypt(sqlbuilder.Server);

            return sqlbuilder.ToString();

        }
        public static bool ExecuteTransaction(CustomCommand command)
        {
            try
            {
                //To implement connection String logic
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = command.GetCommand())
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = trans;
                                cmd.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        catch (Exception err)
                        {
                            trans.Rollback();
                            throw err;
                        }
                    }
                }
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static bool ExecuteTransaction(CustomCommand[] commandList)
        {
            try
            {
                //To implement connection String logic
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    using (MySqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (CustomCommand Command in commandList)
                            {
                                using (MySqlCommand cmd = Command.GetCommand())
                                {
                                    cmd.Connection = conn;
                                    cmd.Transaction = trans;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            trans.Commit();
                        }
                        catch (Exception err)
                        {
                            trans.Rollback();
                            throw err;
                        }
                    }
                }
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static DataTable ExecuteTable(CustomCommand command)
        {
            try
            {
                DataTable dt = new DataTable();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    using (MySqlCommand cmd = command.GetCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Connection = conn;
                        conn.Open();

                        MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                        adap.Fill(dt);
                        //MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        //dt.Load(dr);

                        //dr.Close();
                        conn.Close();
                    }
                }

                return dt;
            }
            catch (Exception err)
            {
                throw err;
                //return null;
            }
        }

        public static DataSet ExecuteDataSet(CustomCommand command)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    using (MySqlCommand cmd = command.GetCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Connection = conn;
                        conn.Open();

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(ds);
                        conn.Close();
                    }
                }

                return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public static Object ExecuteScalar(CustomCommand command)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    using (MySqlCommand cmd = command.GetCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Connection = conn;

                        conn.Open();

                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception er)
            {
                //TempLogErrorMessage(command, er);
                return er.Message;
            }
        }
        public static Int32 ExecuteNonQuery(CustomCommand command, bool throwOnExceptions)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    using (MySqlCommand cmd = command.GetCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                //TempLogErrorMessage(command, ex);
                if (throwOnExceptions) throw;
                return Int32.MinValue;
            }
        }

        public static Object GetOutParameterValue(CustomCommand command, String parameterName)
        {
            try
            {
                MySqlCommand cmd = command.GetCommand();
                Object obj = cmd.Parameters[parameterName].Value;
                return obj;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //private static void TempLogErrorMessage(CustomCommand command, Exception ex)
        //{
        //    ApplicationCustomErrorEvent errorEvent = new ApplicationCustomErrorEvent("DB: " + ex.Message, null, "", ex);
        //    errorEvent.CustomEventDetails += " DBQuery Details; CommandText:" + command.CommandText + ";\r\n";
        //    errorEvent.CustomEventDetails += " CommandType:" + command.CommandType + ";\r\n";
        //    if (command.GetCommand().Parameters.Count > 0)
        //    {
        //        foreach (MySqlParameter param in command.GetCommand().Parameters)
        //        {
        //            errorEvent.CustomEventDetails += string.Format("Parameter: Name: {0}; Value: {1}, DBType: {2};", param.ParameterName, param.Value, param.SqlDbType.ToString());
        //        }
        //    }
        //    errorEvent.Raise();
        //}

        public static IDataReader ExecuteDataReader(CustomCommand command)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(GetConnectionString());

                MySqlCommand cmd = command.GetCommand();
                cmd.Connection = conn;
                conn.Open();

                IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

    }
}

