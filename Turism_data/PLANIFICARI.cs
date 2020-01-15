using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Turism_data
{
    class PLANIFICARI
    {
        CONNECT conn = new CONNECT();

        public DataTable getPerioade()
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Nume,DataStart,DataStop,Frecventa FROM Perioade";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        public DataTable getPlanificari()
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Localitati.Nume,Planificari.Frecventa,Planificari.DataStart,Planificari.DataStop,Planificari.Ziua FROM Planificari INNER JOIN Localitati ON Planificari.IDLocalitate=Localitati.IDLocalitate";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public string getLocPlanificari(int idv)
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT Localitati.Nume,Planificari.Frecventa,Planificari.DataStart,Planificari.DataStop,Planificari.Ziua FROM Planificari INNER JOIN Localitati ON Planificari.IDLocalitate=Localitati.IDLocalitate";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows[idv - 1][0].ToString();
        }
        public int get_nr_Planificari()
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = "SELECT * FROM Planificari";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows.Count;
        }

        public string getPerioada(int idv)
        {
            SqlCommand command = new SqlCommand();
            string time;
            string query = "SELECT Frecventa FROM Planificari where IDVizita=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("id", SqlDbType.Int).Value = idv;

            conn.OpenConnection();

            time = command.ExecuteScalar().ToString();

            conn.CloseConnection();

            return time;
        }

        public DateTime getDateStart(int idv)
        {
            SqlCommand command = new SqlCommand();
            DateTime time = new DateTime();
            string query = "SELECT DataStart FROM Planificari where IDVizita=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("id", SqlDbType.Int).Value = idv;

            conn.OpenConnection();

            time = Convert.ToDateTime(command.ExecuteScalar().ToString());

            conn.CloseConnection();

            return time;
        }

        public DateTime getDateStop(int idv)
        {
            SqlCommand command = new SqlCommand();
            DateTime time = new DateTime();
            string query = "SELECT DataStop FROM Planificari where IDVizita=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("id", SqlDbType.Int).Value = idv;

            conn.OpenConnection();

            time = Convert.ToDateTime(command.ExecuteScalar().ToString());

            conn.CloseConnection();

            return time;
        }

        public int getDateDay(int idv)
        {
            SqlCommand command = new SqlCommand();
            int time;
            string query = "SELECT Ziua FROM Planificari where IDVizita=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("id", SqlDbType.Int).Value = idv;

            conn.OpenConnection();

            time = Convert.ToInt32(command.ExecuteScalar().ToString());

            conn.CloseConnection();

            return time;
        }

        public void InsertPerioade(string loc, DateTime start_vizita,DateTime stop_vizita,string frecventa)
        {
            SqlCommand command = new SqlCommand();
            string query = "INSERT INTO Perioade(Nume,DataStart,DataStop,Frecventa) Values(@l,@s,@ds,@f)";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("l", SqlDbType.VarChar).Value = loc;
            command.Parameters.Add("s", SqlDbType.DateTime).Value = start_vizita;
            command.Parameters.Add("ds", SqlDbType.DateTime).Value = stop_vizita;
            command.Parameters.Add("f", SqlDbType.VarChar).Value = frecventa;

            conn.OpenConnection();

            command.ExecuteNonQuery();

            conn.CloseConnection();
        }
    }
}
