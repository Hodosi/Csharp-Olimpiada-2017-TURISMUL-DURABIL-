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
    public partial class Generare_Poster : Form
    {
        public Generare_Poster()
        {
            InitializeComponent();
        }

        LOCALITATE loc = new LOCALITATE();

        private void Generare_Poster_Load(object sender, EventArgs e)
        {
            this.comboBox_Localitate.DataSource = loc.getLocalitati();
            this.comboBox_Localitate.DisplayMember = "Nume";
            this.comboBox_Localitate.ValueMember = "IDLocalitate";
            //----------------------------------------------------------
            int idLoc = Convert.ToInt32(this.comboBox_Localitate.SelectedValue.ToString());
            this.comboBox_Imagine.DataSource = loc.getImgLocalitate(idLoc);
            this.comboBox_Imagine.DisplayMember = "CaleFisier";
            this.comboBox_Imagine.ValueMember = "CaleFisier";
            ///-------------------------------------------------------------
            string fn = Application.StartupPath + "\\Data\\IMG\\Imagini\\" + this.comboBox_Imagine.Text;
            this.pictureBox1.Image = Image.FromFile(fn);
            ////-----------------------------------------------------------
        }

        private void comboBox_Localitate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox_Localitate.DisplayMember = "Nume";
            this.comboBox_Localitate.ValueMember = "IDLocalitate";
            //--------------------------------------------------------------------------
            int idLoc = Convert.ToInt32(this.comboBox_Localitate.SelectedValue.ToString());
            this.comboBox_Imagine.DataSource = loc.getImgLocalitate(idLoc);
            this.comboBox_Imagine.DisplayMember = "CaleFisier";
            this.comboBox_Imagine.ValueMember = "CaleFisier";
            //----------------------------------------------------------------------------
        }

        private void comboBox_Imagine_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox_Imagine.DisplayMember = "CaleFisier";
            this.comboBox_Imagine.ValueMember = "CaleFisier";
            //----------------------------------------------------------------------
            string i = this.comboBox_Imagine.SelectedValue.ToString();
            string fn = Application.StartupPath + "\\Data\\IMG\\Imagini\\" + i;
            this.pictureBox1.Image = Image.FromFile(fn);
            ////-----------------------------------------------------------
        }

        private void button_Adauga_Click(object sender, EventArgs e)
        {
            int nr = listBox1.Items.Count;
            if (nr < 10)
                this.listBox1.Items.Add(this.comboBox_Imagine.SelectedValue.ToString());
            else
                MessageBox.Show("Lista ii plina", "Error", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.png)|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //this.comboBox_Imagine.DisplayMember = "CaleFisier";
                //this.comboBox_Imagine.ValueMember = "CaleFisier";
                ////----------------------------------------------------------------------

                ////----------------------------------------------------------------------
                //string i = this.comboBox_Imagine.SelectedValue.ToString();
                //string fn = Application.StartupPath + "\\Data\\IMG\\Imagini\\" + i;

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    string img_name = this.listBox1.Items[i].ToString();
                    string fn = Application.StartupPath + "\\Data\\IMG\\Imagini\\" + img_name;
                    string df = sfd.FileName + img_name ;
                    Image.FromFile(fn).Save(df);
                }
                //pictureBox1.Image.Save(sfd.FileName);
            }

            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "(*.png)|*.png";
            //sfd.RestoreDirectory = true;
            //sfd.DefaultExt = ".png";
            ////this.saveFileDialog1.ShowDialog();

            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    Stream filestream = sfd.OpenFile();
            //    StreamWriter sw = new StreamWriter(filestream);

            //    //sw.Write(this.textBox_titlu.Text);
            //    //----------------------------------------------------
            //    this.comboBox_Imagine.DisplayMember = "CaleFisier";
            //    this.comboBox_Imagine.ValueMember = "CaleFisier";
            //    //----------------------------------------------------------------------

            //    //----------------------------------------------------------------------
            //    string i = this.comboBox_Imagine.SelectedValue.ToString();
            //    string fn = Application.StartupPath + "\\Data\\IMG\\Imagini\\" + i;
            //    sw.Write(Image.FromFile(fn));
            //    ////-----------------------------------------------------------

            //    sw.Close();
            //    filestream.Close();
            //}



        }
    }
}
