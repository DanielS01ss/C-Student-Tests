using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _10LaBac
{
    public partial class Status : UserControl
    {
        public Status()
        {
            InitializeComponent();
        }

        private void Status_Load(object sender, EventArgs e)
        {
            DataTable dtbl = new DataTable();
            string connectionString = @"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Initial Catalog=UserRegistrationDB;Integrated Security=True";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SelectUser", sqlCon);
                
                sqlDa.Fill(dtbl);
                label2.Text = dtbl.Rows[0][1].ToString();

            }

            ///here is a problem =))

            ///label 8 - media notelor romana
            label8.Text = dtbl.Rows[0][4].ToString();
            /// label 9 ultima nota romana
            label9.Text = dtbl.Rows[0][5].ToString();
            /// label 10 - media ultimelor note info
            label10.Text = dtbl.Rows[0][6].ToString();
            /// label 11 - ultima nota info
            label11.Text = dtbl.Rows[0][7].ToString();
            /// label 12 - media ultimelor note mate
            label12.Text = dtbl.Rows[0][2].ToString();
            /// label 13  ultima nota mate
            label13.Text = dtbl.Rows[0][3].ToString();



        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Romana_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
