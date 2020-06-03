using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace PTW.DBAccess
{
    public sealed class CustomCommand : IDisposable
    {
        private MySqlCommand cmd = null;

        public CustomCommand()
        {
            cmd = new MySqlCommand
            {
                CommandTimeout = 0
            };
        }

        public void Dispose()
        {
            try
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        cmd.Connection.Close();
                    }
                }
                cmd = null;
            }
            catch (MySqlException)
            {
            }
        }

        public System.Data.CommandType CommandType
        {
            get
            {
                return cmd.CommandType;
            }
            set
            {
                cmd.CommandType = value;
            }
        }

        public MySqlParameterCollection CommandParameters
        {
            get
            {
                return cmd.Parameters;
            }
        }

        public String CommandText
        {
            get
            {
                return cmd.CommandText;
            }
            set
            {
                cmd.CommandText = value;
            }
        }

        public DbParameter AddParameterWithValue(String parameterName, Object value)
        {
            MySqlParameter param = new MySqlParameter
            {
                ParameterName = parameterName
            };

            //if (value == null)
            if (IsNullValue(value))
            {
                param.IsNullable = true;
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            return cmd.Parameters.Add(param);
        }

        public void SetParameterWithValue(string parameterName, object value)
        {
            var parameters = this.cmd.Parameters;
            if (parameters.Contains(parameterName))
            {
                parameters[parameterName].Value = value ?? DBNull.Value;
            }
            else
            {
                this.cmd.Parameters.Add(new MySqlParameter(parameterName, value));
            }
        }

        private Boolean IsNullValue(Object value)
        {
            if (value == null)
            {
                return true;
            }

            try
            {
                if (value.GetType().Name.Equals("DateTime"))
                {
                    DateTime currentValue = System.Convert.ToDateTime(value);
                    if (currentValue == DateTime.MinValue)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }

        }

        internal MySqlCommand GetCommand()
        {
            return cmd;
        }

        public DbParameter AddOutParameterWithValue(String parameterName, Object value)
        {
            MySqlParameter param = new MySqlParameter
            {
                ParameterName = parameterName,

                Direction = ParameterDirection.Output
            };

            //if (value == null)
            if (IsNullValue(value))
            {
                param.IsNullable = true;
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            return cmd.Parameters.Add(param); ;
        }

        public DbParameter AddOutParameterWithValue(String parameterName, Object value, string type, int size)
        {
            MySqlParameter param = new MySqlParameter
            {
                ParameterName = parameterName
            };
            if (type == "NVarChar")
            {
                param.Size = size;
            }
            param.Direction = ParameterDirection.Output;

            //if (value == null)
            if (IsNullValue(value))
            {
                param.IsNullable = true;
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            return cmd.Parameters.Add(param); ;
        }

    }
}
