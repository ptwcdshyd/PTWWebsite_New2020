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

			//sqlbuilder.UserID = UtilityHelper.Decrypt(sqlbuilder.UserID);

			//sqlbuilder.Password = UtilityHelper.Decrypt(sqlbuilder.Password);

			//sqlbuilder.Server = UtilityHelper.Decrypt(sqlbuilder.Server);

			return sqlbuilder.ToString();

		}

	}
}
