using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Profile
{
    public class DataConnection
    {
        static MySqlConnection mySqlConnection;

        public static MySqlConnection getDBConnection()
        {

            String conStr = "server=localhost;user id=root;password=;persistsecurityinfo=True;port=3306;database=profile_test_db;SslMode=none";

            if (DataConnection.mySqlConnection == null)
            {

                mySqlConnection = new MySqlConnection(connectionString: conStr);
                //mySqlConnection.Open();

            }

            return mySqlConnection;
        }

    }
}
