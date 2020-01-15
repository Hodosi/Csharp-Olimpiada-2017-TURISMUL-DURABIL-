using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Turism_data
{
    class LOCALITATE
    {
        CONNECT conn = new CONNECT();
        public DataTable getLocalitati()
        {
            SqlCommand command = new SqlCommand();
            string query = "select * from Localitati ";
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public DataTable getImgLocalitate(int idLoc)
        {
            SqlCommand command = new SqlCommand();
            string query = "select CaleFisier from Imagini where IDLocalitate=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.Add("id", SqlDbType.Int).Value = idLoc;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public DataTable getImg(int idImg)
        {
            SqlCommand command = new SqlCommand();
            string query = "select CaleFisier from Imagini where IDImagine=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.Add("id", SqlDbType.Int).Value = idImg;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public int getIdLoc(string loc)
        {
            SqlCommand command = new SqlCommand();
            string query = "select IDLocalitate from Localitati where Nume=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.Add("id", SqlDbType.VarChar).Value = loc;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return Convert.ToInt32(table.Rows[0][0].ToString());
        }
        public int getNrImg(int loc)
        {
            SqlCommand command = new SqlCommand();
            string query = "select CaleFisier from Imagini where IDLocalitate=@id";

            command.CommandText = query;
            command.Connection = conn.GetConnection();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            command.Parameters.Add("id", SqlDbType.VarChar).Value = loc;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows.Count;
        }

        public int getNrLoc()
        {
            SqlCommand command = new SqlCommand();
            string query = "select * from Localitati";

            command.CommandText = query;
            command.Connection = conn.GetConnection();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //command.Parameters.Add("id", SqlDbType.VarChar).Value = loc;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows.Count;
        }
    }
}
