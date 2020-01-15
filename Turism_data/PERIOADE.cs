using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Turism_data
{
    class PERIOADE
    {
        CONNECT conn = new CONNECT();

        public int get_nr_Perioade()
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT * FROM Perioade";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows.Count;
        }

        public string getPerioadaPerioadei(int idv)
        {
            SqlCommand command = new SqlCommand();
            string time;
            string query = "SELECT Frecventa FROM Perioade where IDPerioade=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("id", SqlDbType.Int).Value = idv;

            conn.OpenConnection();

            time = command.ExecuteScalar().ToString();

            conn.CloseConnection();

            return time;
        }
        public DateTime getDateStartPerioade(int idv)
        {
            SqlCommand command = new SqlCommand();
            DateTime time = new DateTime();
            string query = "SELECT DataStart FROM Perioade where IDPerioade=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("id", SqlDbType.Int).Value = idv;

            conn.OpenConnection();

            time = Convert.ToDateTime(command.ExecuteScalar().ToString());

            conn.CloseConnection();

            return time;
        }
        public DateTime getDateStopPerioade(int idv)
        {
            SqlCommand command = new SqlCommand();
            DateTime time = new DateTime();
            string query = "SELECT DataStop FROM Perioade where IDPerioade=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("id", SqlDbType.Int).Value = idv;

            conn.OpenConnection();

            time = Convert.ToDateTime(command.ExecuteScalar().ToString());

            conn.CloseConnection();

            return time;
        }
        public string getLocPerioade(int idv)
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Nume FROM Perioade";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows[idv][0].ToString();
        }
        public void InsertItinerariu(string loc, DateTime start_vizita)
        {
            SqlCommand command = new SqlCommand();
            string query = "INSERT INTO Itinerariu(Localitate,Data) Values(@l,@s)";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("l", SqlDbType.VarChar).Value = loc;
            command.Parameters.Add("s", SqlDbType.DateTime).Value = start_vizita;

            conn.OpenConnection();

            command.ExecuteNonQuery();

            conn.CloseConnection();
        }

        public DataTable getItinerariu()
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Localitate,Data FROM Itinerariu";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public DataTable OrderItinerariu()
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Localitate,Data FROM Itinerariu ORDER BY Data ASC";
            //string query = "ALTER Itinerariu ORDER BY Data ASC";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            //conn.OpenConnection();

            //command.ExecuteNonQuery();

            //conn.CloseConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
    }
}
