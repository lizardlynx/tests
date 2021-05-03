using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using IIG.DatabaseConnectionUtils;

namespace IIG.CoSFE.DatabaseUtils
{
    public class FlagpoleDatabaseUtils : DatabaseConnection
    {
        public FlagpoleDatabaseUtils(NameValueCollection appSettings) : base(appSettings)
        {
        }

        public FlagpoleDatabaseUtils(string server, string database, bool isTrusted, string login = null,
            string password = null, int connectionTimeOut = 15) : base(server, database, isTrusted, login, password,
            connectionTimeOut)
        {
        }

        public bool AddFlag(string flagView, bool flagValue)
        {
            if (!ConnectToDatabase())
                return false;

            bool res;

            using (var cmd = new SqlCommand("[dbo].[AddFlag]", Connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FlagView", SqlDbType.VarChar).Value = flagView;
                    cmd.Parameters.Add("@FlagValue", SqlDbType.Bit).Value = flagValue;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    res = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    res = false;
                }
                finally
                {
                    Connection.Close();
                }
            }

            return res;
        }

        public bool GetFlag(int flagId, out string flagView, out bool? flagValue)
        {
            flagView = null;
            flagValue = null;
            var dt = GetDataTableBySql($"EXECUTE [dbo].[GetFlag] {flagId}");
            if (dt == null || dt.Rows.Count != 1)
                return false;
            var itemArray = dt.Rows[0].ItemArray;
            flagView = itemArray[1] as string;
            flagValue = itemArray[2] as bool?;
            return true;
        }
    }
}