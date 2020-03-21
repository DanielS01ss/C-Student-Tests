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
using System.Security.Cryptography;

namespace _10LaBac
{
    public partial class Form1 : Form
    {
       Sign_IN sg_in = new Sign_IN();
        string connectionString = @"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Initial Catalog=UserRegistrationDB;Integrated Security=True";

        public string myVal;

       public string MyVal
        {
            get { return myVal; }
            set { myVal = value; }

        } 

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cand se incarca form-ul ascundem celelate user controls
            sign_IN1.Hide();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.TabStop = false;

        }

        //butonul de Sign In din primul pannel
        private void button2_Click(object sender, EventArgs e)
        {
        sign_IN1.Show();          
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
          
           

           
        }

     
        private void textBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        ///butonul de Back to login din al doilea pannel
        private void sign_IN1_Load(object sender, EventArgs e)
        {
            
        }

        private void sign_IN1_Click(object sender, EventArgs e)
        {
         
        }

        static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);

            }
        }
     
        ///Asta e butonul de login!!
        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBox2.Text;
            pass = Encrypt(pass);
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Initial Catalog=UserRegistrationDB;Integrated Security=True");
            SqlDataAdapter sqa = new SqlDataAdapter("Select count(*) From tblUser where Email ='"+textBox1.Text+"' and Parola ='"+pass+"'",con);
            DataTable dt = new DataTable();
            sqa.Fill(dt);
            

               
            
                if (dt.Rows[0][0].ToString() == "1")
                {
                    ///aici facem comanda care sa puna logged in = true pentru userul logat ca apoi sa stim care s-a logat
                    string commandText = "UPDATE tblUser SET logged_in = 'true' where Email ='" + textBox1.Text + "' and Parola ='" + pass + "'";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }



                    Main mn = new Main();

                    this.Hide();
                    mn.ShowDialog();

                }
               

            
            else
            {
                MessageBox.Show("Email or password is incorrect!! Please try again!");
            }



        }

        private void sign_IN1_Load_1(object sender, EventArgs e)
        {

        }

        private void sign_IN1_Load_2(object sender, EventArgs e)
        {
          
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
          
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
             
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void sign_IN1_Load_3(object sender, EventArgs e)
        {

        }


        /// aceste eventuri sunt folosite pentru a face window-ul sa se poate misca pe ecran
        /// chiar daca am setat border style la none
        bool drag = false;
        Point start_point = new Point(0, 0);
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);

            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);

            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }
    }
}
