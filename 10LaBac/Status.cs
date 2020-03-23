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

            ///acuma verificam mediile si pune imaginile

            ///1 Romana --> label 8 medie max
            /// --> label 9 medie min

            int actual_R, maxR, actualM, maxM, actualI, maxI;

            actual_R = Int32.Parse(label9.Text);
            maxR = Int32.Parse(label8.Text);

            actualI = Int32.Parse(label11.Text);
            maxI = Int32.Parse(label10.Text);

            actualM = Int32.Parse(label13.Text);
            maxM = Int32.Parse(label12.Text);


            if (Int32.Parse(label8.Text) == 0)
            {
                label8.Text = "Nu s-au efectuat teste inca";
                label9.Text = "Nu s-au efectuat teste inca";
            }
            else
            {

                if (actual_R == maxR)
                {
                    if (actual_R >= 7)
                    {
                        pictureBox1.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\happy.png";
                    }
                    else
                    {
                        pictureBox1.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\sad.png";
                    }

                }
                else
                {
                    if(actual_R<maxR)
                    {
                        pictureBox1.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\jos.png";
                    }

                }


            }
            if(Int32.Parse(label10.Text)==0)
            {
                label10.Text = "Nu s-au efectuat teste inca";
                label11.Text = "Nu s-au efectuat teste inca";
            }
            else
            {

                if (actualI == maxI)
                {
                    if (actualI >= 7)
                    {
                        pictureBox3.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\happy.png";
                    }
                    else
                    {
                        pictureBox3.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\sad.png";
                    }

                }
                else
                {
                    if (actualI < maxI)
                    {
                        pictureBox3.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\jos.png";
                    }

                }


            }


            if (Int32.Parse(label12.Text) == 0)
            {
                label12.Text = "Nu s-au efectuat teste inca";
                label13.Text = "Nu s-au efectuat teste inca";
            }
            else
            {

                if (actualM == maxM)
                {
                    if (actualM >= 7)
                    {
                        pictureBox2.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\happy.png";
                    }
                    else
                    {
                        pictureBox2.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\sad.png";
                    }

                }
                else
                {
                    if (actualM < maxM)
                    {
                        pictureBox2.ImageLocation = @"C:\Users\stanc\Desktop\atestat\poze_app\jos.png";
                    }

                }


            }


            ///if-ul pentru imagini






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

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
