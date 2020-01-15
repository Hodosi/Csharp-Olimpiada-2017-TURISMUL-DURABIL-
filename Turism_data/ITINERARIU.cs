using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Turism_data
{
    class ITINERARIU
    {
        CONNECT conn = new CONNECT();
        //public DataTable OrderItinerariu()
        //{
        //    SqlCommand command = new SqlCommand();
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    DataTable table = new DataTable();
        //    string query = "SELECT Localitate,Data FROM Itinerariu ORDER BY Data ASC";
        //    //string query = "ALTER Itinerariu ORDER BY Data ASC";

        //    command.CommandText = query;
        //    command.Connection = conn.GetConnection();

        //    //conn.OpenConnection();

        //    //command.ExecuteNonQuery();

        //    //conn.CloseConnection();

        //    adapter.SelectCommand = command;
        //    adapter.Fill(table);

        //    return table;
        //}

        public string getItiLoc(int idv)
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Localitate,Data FROM Itinerariu ORDER BY Data ASC";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows[idv][0].ToString();
        }
        public DateTime getItiDate(int idv)
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Localitate,Data FROM Itinerariu ORDER BY Data ASC";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return Convert.ToDateTime(table.Rows[idv][1].ToString());
        }
        public int getItiNr()
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT * FROM Itinerariu";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows.Count;
        }
    }
}
