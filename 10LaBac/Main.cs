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

namespace _10LaBac
{
    public partial class Main : Form
    {
        string connectionString = @"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Initial Catalog=UserRegistrationDB;Integrated Security=True";
        Instructiuni ins = new Instructiuni();
        bool vazut = false;
        /// <summary>
        /// codul asta de mai jos face posibila mutarea ferestrei pe ecran
        /// </summary>


        public Main()
        {
            InitializeComponent();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

      

        private void Main_Load(object sender, EventArgs e)
        {
           
            ins.Hide();
            ///aici selectam userul logat din baza de date folosindu-ne de un sql statement
            status1.Hide();
                
            
            ///folosindu-ne de using retragem din baza de date informatia de care avem nevoie
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SelectUser", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                if (dtbl.Rows.Count > 0)
                {
                    //Do your stuff here.
                    label2.Text = dtbl.Rows[0][1].ToString();
                }
               
               
            }

            

            ///label 8 - media notelor romana
            /// label 9 ultima nota romana
            /// label 10 - media ultimelor note mate
            /// label 11 - ultima nota info
            /// label 12 - media ultimelor note mate
            /// label 13  ultima nota mate

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            status1.Hide();
            if (!vazut)
            {
                ins.Show();
                vazut = true;
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string commandText = "UPDATE tblUser SET logged_in = 'false' WHERE logged_in = 'true'";
            ///aici declaram conexiunea si o folosim
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(commandText, sqlcon))
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }


            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            status1.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string commandText = "UPDATE tblUser SET logged_in = 'false' WHERE logged_in = 'true'";
            ///aici declaram conexiunea si o folosim
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(commandText, sqlcon))
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            string commandText = "UPDATE tblUser SET logged_in = 'false' WHERE logged_in = 'true'";
            ///aici declaram conexiunea si o folosim
            using(SqlConnection sqlcon = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(commandText, sqlcon))
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }

            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();

        }

        private void Teste_Click(object sender, EventArgs e)
        {
            status1.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            status1.Show();
        }

        private void quizzes1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            string commandText = "UPDATE tblUser SET logged_in = 'false' WHERE logged_in = 'true'";
            ///aici declaram conexiunea si o folosim
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(commandText, sqlcon))
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }

            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();

        }

        bool drag = false;
        Point start_point = new Point(0, 0);
        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if(drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);

            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);

            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void status1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Romana ro = new Romana();
            this.Hide();
            ro.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Matematica m = new Matematica();
            this.Hide();
            m.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Informatica info = new Informatica();
            this.Hide();
            info.ShowDialog();
        }

        private void instructiuni1_Load(object sender, EventArgs e)
        {

        }
    }
}
