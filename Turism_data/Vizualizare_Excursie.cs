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
    public partial class Vizualizare_Excursie : Form
    {
        public Vizualizare_Excursie()
        {
            InitializeComponent();
        }

        PLANIFICARI plan = new PLANIFICARI();
        PERIOADE per = new PERIOADE();
        ITINERARIU iti = new ITINERARIU();
        LOCALITATE loccc = new LOCALITATE();
        static public int[] nr_imagini_localitate=new int[100];
        private void Vizualizare_Excursie_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'turismDataSet.Planificari' table. You can move, or remove it, as needed.
            //this.planificariTableAdapter.Fill(this.turismDataSet.Planificari);

            this.dataGridView1.DataSource = plan.getPlanificari();
            //-----------------------------------------------------
            int nr_loc = loccc.getNrLoc();
            for (int i = 2; i <= nr_loc+1 ; i++)
            {
                nr_imagini_localitate[i] = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime start_excursie = dateTimePicker1.Value.Date;
            DateTime stop_excursie = dateTimePicker2.Value.Date;
            DateTime start_vizita, stop_vizita;
            DateTime d_start, d_stop;
            DateTime d_ziua;
            string loc;
            int ziua;
            int[] zilele_lunii = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int nr_plan = plan.get_nr_Planificari();
            for (int i = 1; i <= nr_plan; i++)
            {
                string periodic = plan.getPerioada(i);
                //MessageBox.Show(periodic);
                loc = plan.getLocPlanificari(i);
                switch (periodic.Trim())
                {
                    case "ocazional":
                        //MessageBox.Show(loc);
                        d_start = plan.getDateStart(i);
                        d_stop = plan.getDateStop(i);
                        //if (DateTime.Compare(d_start,start_excursie)>=0 && d_start.Date <= stop_excursie.Date)
                        if ((d_start.Date <= stop_excursie.Date && d_start >= start_excursie) || (d_stop >= start_excursie && d_stop <= stop_excursie))
                        {
                            if (d_start.Date >= start_excursie.Date)
                                start_vizita = d_start;
                            else
                                start_vizita = start_excursie;
                            if (d_stop.Date <= stop_excursie)
                                stop_vizita = d_stop;
                            else
                                stop_vizita = stop_excursie;
                            plan.InsertPerioade(loc, start_vizita, stop_vizita, "ocazional");
                            this.dataGridView2.DataSource = plan.getPerioade();
                        }
                        break;
                    case "anual":
                        ziua = plan.getDateDay(i);
                        int luna = 0;
                        while (ziua > zilele_lunii[luna])
                        {
                            ziua = ziua - zilele_lunii[luna];
                            luna++;
                        }
                        d_ziua = new DateTime(2017, luna + 1, ziua);
                        //MessageBox.Show(d_ziua.ToString());
                        if (d_ziua.Date >= start_excursie.Date && d_ziua.Date <= stop_excursie.Date)
                        {
                            plan.InsertPerioade(loc, d_ziua, d_ziua, "anual");
                            this.dataGridView2.DataSource = plan.getPerioade();
                        }
                        break;
                    case "lunar":
                        ziua = plan.getDateDay(i);
                        for (int lun = 1; lun <= 12; lun++)
                        {
                            if (zilele_lunii[lun - 1] >= ziua)
                            {
                                d_ziua = new DateTime(2017, lun, ziua);
                                //MessageBox.Show(d_ziua.ToString());
                                if (d_ziua.Date >= start_excursie.Date && d_ziua.Date <= stop_excursie.Date)
                                {
                                    plan.InsertPerioade(loc, d_ziua, d_ziua, "lunar");
                                    this.dataGridView2.DataSource = plan.getPerioade();
                                }
                            }
                        }
                        break;
                }
            }

            //-------------------------------------------------
            int nr_perioade = per.get_nr_Perioade();
            for (int i = 0; i < nr_perioade; i++)
            {
                loc = per.getLocPerioade(i);
                string periodic = per.getPerioadaPerioadei(i);
                if (periodic == "ocazional")
                {
                    d_start = per.getDateStartPerioade(i);
                    d_stop = per.getDateStopPerioade(i);
                    while (d_start <= d_stop)
                    {
                        per.InsertItinerariu(loc, d_start);
                        d_start = d_start.AddDays(1);
                    }
                   // per.InsertItinerariu(loc, d_start);
                    //per.InsertItinerariu(loc, d_stop);
                    this.dataGridView3.DataSource = per.getItinerariu();
                }
                else
                {
                    d_ziua = per.getDateStopPerioade(i);
                    per.InsertItinerariu(loc, d_ziua);
                    this.dataGridView3.DataSource = per.getItinerariu();
                }
            }
            //------------------------------------
            //per.OrderItinerariu();
            this.dataGridView3.DataSource = per.OrderItinerariu();
            //----------Update Iti----------------------------
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = iti.getItiNr();
            if (this.timer1.Enabled == false)
            {
                timer1.Enabled = true;
                this.button_Start.Text = "Stop";
            }
            else
            {
                this.timer1.Enabled = false;
                this.button_Start.Text = "Start";
            }
        }

        //string i = this.comboBox_Imagine.SelectedValue.ToString();
        //string fn = Application.StartupPath + "\\Data\\IMG\\Imagini\\" + i;
        //this.pictureBox1.Image = Image.FromFile(fn);

        private void timer1_Tick(object sender, EventArgs e)
        {
            int curent = Program.loc_curent;
            //MessageBox.Show(curent.ToString());
            int nr_iti = iti.getItiNr();
            // int add = 100 / nr_iti;
            if (curent < nr_iti)
            {
                //MessageBox.Show(curent.ToString());
                this.progressBar1.Value += 1;
                string loc = iti.getItiLoc(curent);
                DateTime date = iti.getItiDate(curent);
                this.label_Loc.Text = loc;
                this.label_Data.Text = date.Date.ToString();
                Program.loc_curent += 1;
                //-----------img------------------------------
                int id_loc = loccc.getIdLoc(loc);
                string i = nr_imagini_localitate[id_loc].ToString();
                string fn = Application.StartupPath + "\\Data\\IMG\\Imagini\\" + loc + i +".jpg";
                this.pictureBox1.Image = Image.FromFile(fn);

                //--------------------------------------------------
                int nr_img_loc = loccc.getNrImg(id_loc);
                if (nr_imagini_localitate[id_loc] == nr_img_loc)
                {
                    nr_imagini_localitate[id_loc] = 1;
                }
                else
                    nr_imagini_localitate[id_loc]++;

            }
            else
            {
                this.timer1.Enabled = false;
            }
        }
    }
}
