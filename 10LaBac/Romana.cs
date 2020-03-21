using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace _10LaBac
{
    public partial class Romana : Form
    {
        int corecte = 0, gresite = 0;
        private int totalSeconds,totalMinutes;
        Main mn = new Main();
        Instructiuni ins = new Instructiuni();
        bool isActive;


        public Romana()
        {

            InitializeComponent();
            label4.Text = corecte.ToString();
            label7.Text = gresite.ToString();


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ///daca deabea s-a incarcat panoul atunci cand apasa pe buton ascunde instructiunile
            ///si initializeaza timpul dar si i-a intrebarile din baza de date si le pune in array pe raspunsuri
            ///ca sa inceapa quiz-ul

            label4.Text = corecte.ToString();
            label7.Text = gresite.ToString();
            ins.Hide();
          
        }

        void start()
        {
            totalMinutes = 40;
            totalSeconds = (totalMinutes * 60);
            this.timer1.Enabled = true;

        }

        
        void populare()
        {
            string connString = @"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string comm = "SELECT * FROM UserRegistrationDB.dbo.TestRom";
            SqlConnection sqlConn = new SqlConnection(connString);
            SqlDataAdapter sqlDA = new SqlDataAdapter(comm, sqlConn);
            DataTable dtRaspunsuri = new DataTable();
            sqlDA.Fill(dtRaspunsuri);
            for(int i=0; i<dtRaspunsuri.Rows.Count;i++)
            label1.Text = dtRaspunsuri.Rows[0]["Enunt"].ToString();


        }

     
        private void Romana_Load(object sender, EventArgs e)
        {
            ///daca am ascuns instructiunile atunci poate incepe testul
            start();
            ///bun acuma cand se incarca ferestrea vom popula matricile cu valori
            populare();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(totalSeconds>0)
            {
                totalSeconds--;
                int minutes = totalSeconds / 60;
                int seconds = totalSeconds - (minutes * 60);
                this.label8.Text = minutes.ToString() + ":" + seconds.ToString();
            }
            else
            {
                this.timer1.Stop();
                MessageBox.Show("Timpul a expirat!!!");
                this.Hide();
                mn.Show();
            }
        }

        private void instructiuni1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Esti sigur ca vrei sa renunti? Haide stiu ca poti!",
                      "Oprire Test", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    this.Hide();
                    mn.Show();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
