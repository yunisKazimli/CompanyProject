using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CompanyProject
{
    static class SqlConnector
    {
        public static SqlConnection sqlc;

        public static SqlCommand command;

        public static void Init(string serverParameter)
        {
            sqlc = new SqlConnection(serverParameter);
        }

        public static DataTable ExQuery(string commandText)
        {
            DataTable dt = new DataTable();

            sqlc.Open();
            command = sqlc.CreateCommand();
            command.CommandText = commandText;

            dt.Load(command.ExecuteReader());

            sqlc.Close();

            return dt;
        }

        public static void ExNonQuery(string commandText)
        {
            sqlc.Open();
            command = sqlc.CreateCommand();
            command.CommandText = commandText;
            command.ExecuteNonQuery();
            sqlc.Close();
        }
    }
}
