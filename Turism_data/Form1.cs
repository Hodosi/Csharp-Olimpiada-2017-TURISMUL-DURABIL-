using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace Turism_data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Turism.mdf;Integrated Security=True;Connect Timeout=30;";

        CONNECT conn = new CONNECT();
        private static void sterge()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //---------------------------------------------------------
            SqlCommand cmd = new SqlCommand("Delete from Localitati",con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new SqlCommand("Delete from Imagini",con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new SqlCommand("Delete from Planificari",con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            //--------------------------------------------------------
            ////cmd = new SqlCommand("ALTER TABLE table_name AUTO_INCREMENT = 1", con);
            ////cmd.ExecuteNonQuery();

            //---------------------------------------------------------
            con.Close();
        }

        private static void Initializare()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd;
            StreamReader sr = new StreamReader(Application.StartupPath + @"\..\..\planificari.txt");
            string sir;
            char[] split = { '*' };
            DateTime dt1, dt2;
            con.Open();
            //-----------------------------------------------------------------------------------
            while ((sir = sr.ReadLine()) != null)
            {
                string[] siruri = sir.Split(split);
                //-----------------------------------------------------------------------------
                cmd = new SqlCommand("insert into Localitati(nume) values(@localitate)", con);
                cmd.Parameters.AddWithValue("localitate", siruri[0].Trim());
                cmd.ExecuteNonQuery();
                //------------------------------------------------------------------------------
                cmd = new SqlCommand("select IDLocalitate from Localitati where nume=@nume", con);
                cmd.Parameters.AddWithValue("nume", siruri[0].Trim());
                int idlocalitate = Convert.ToInt32(cmd.ExecuteScalar());
                int nrzile, i;
                //------------------------------------------------------------------------------
                switch (siruri[1].Trim())
                {
                    case "ocazional":
                        string d1 = siruri[2];
                        string d2 = siruri[3];
                        dt1 = Convert.ToDateTime(d1.Trim(), System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));
                        dt2 = Convert.ToDateTime(d2.Trim(), System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));
                        i = 4;
                        while (i < siruri.Length)
                        {
                            cmd = new SqlCommand("insert into Imagini(IDLocalitate,CaleFisier) values(@idlocalitate,@cale)", con);
                            cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                            cmd.Parameters.AddWithValue("cale", siruri[i].Trim());
                            cmd.ExecuteNonQuery();
                            i++;
                        }
                        //-------------------------------------------------------------------------------------------------------
                        cmd = new SqlCommand("insert into Planificari(IDLocalitate,Frecventa,DataStart,DataStop) values(@idlocalitate,@frecventa,@datastart,@datastop)", con);
                        cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                        cmd.Parameters.AddWithValue("frecventa", "ocazional");
                        cmd.Parameters.AddWithValue("datastart", dt1);
                        cmd.Parameters.AddWithValue("datastop", dt2);
                        cmd.ExecuteNonQuery();
                        break;
                    case "anual":
                        nrzile = Convert.ToInt32(siruri[2].Trim());
                        i = 3;
                        while (i < siruri.Length)
                        {
                            cmd = new SqlCommand("insert into Imagini(IDLocalitate,Calefisier) values(@idlocalitate,@cale)", con);
                            cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                            cmd.Parameters.AddWithValue("cale", siruri[i].Trim());
                            cmd.ExecuteNonQuery();
                            i++;
                        }
                        cmd = new SqlCommand("insert into Planificari(IDLocalitate,Frecventa,Ziua) values(@idlocalitate,@frecventa,@ziua)", con);
                        cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                        cmd.Parameters.AddWithValue("frecventa", "anual");
                        cmd.Parameters.AddWithValue("ziua", nrzile);
                        cmd.ExecuteNonQuery();
                        break;
                    case "lunar":
                        nrzile = Convert.ToInt32(siruri[2].Trim());
                        i = 3;
                        while (i < siruri.Length)
                        {
                            cmd = new SqlCommand("insert into Imagini(IDLocalitate,Calefisier) values(@idlocalitate,@cale)", con);
                            cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                            cmd.Parameters.AddWithValue("cale", siruri[i].Trim());
                            cmd.ExecuteNonQuery();
                            i++;
                        }
                        cmd = new SqlCommand("insert into Planificari(IDLocalitate,Frecventa,Ziua) values(@idlocalitate,@frecventa,@ziua)", con);
                        cmd.Parameters.AddWithValue("idlocalitate", idlocalitate);
                        cmd.Parameters.AddWithValue("frecventa", "lunar");
                        cmd.Parameters.AddWithValue("ziua", nrzile);
                        cmd.ExecuteNonQuery();
                        break;
                }
            }
            MessageBox.Show("Done!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sterge();
            Initializare();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string path = folderBrowserDialog1.SelectedPath;
            this.textBox1.Text = path;
            string dpath = Application.StartupPath+"\\Ceva";
            System.IO.Directory.Move(path,dpath);

            //string[] filePaths = Directory.GetFiles(path,"*.jpg");


            ////for (int i = 0; i < filePaths.Length; i++)
            ////{
            //    MemoryStream ms = new MemoryStream();
            //    // string pathhh = filePaths[i];
            //    //Bitmap bmp= new Bitmap(pathhh);
            //   // this.pictureBox1.Image = bmp;
            //    this.pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            //    byte[] img = ms.ToArray();

            //    string query = "insert into Imgsource(Img) values(@img)";

            //    conn.OpenConnection();

            //    SqlCommand command = new SqlCommand(query, conn.GetConnection());

            //    command.Parameters.Add("img", SqlDbType.Image).Value = img;

            //    command.ExecuteNonQuery();

            //    conn.CloseConnection();
            ////}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vizualizare_Excursie ve = new Vizualizare_Excursie();
            ve.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Generare_Poster gp = new Generare_Poster();
            gp.ShowDialog();
        }
    }
}
