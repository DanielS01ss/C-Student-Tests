using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Text;
using System.Text.RegularExpressions;
//using System.Linq;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace _10LaBac
{
    public partial class Sign_IN : UserControl
    {
        int score = 0;
        bool validEmail = false;
        string connectionString = @"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Initial Catalog=UserRegistrationDB;Integrated Security=True";    
        /// programul de validare email

        static string Encrypt(string value)
        {
            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);

            }
        }

        public bool IsValid(string emailaddress)
        {
            if (emailaddress == "")
                return false;
                bool isEmail = Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }

        public Sign_IN()
        {
            InitializeComponent();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }
   
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            score = 0;
            label8.Text = "";
            textBox4.PasswordChar = '*';
            String password = textBox4.Text;
            int minLength = 10;

            string specialChars = "#?!,-'/`_*$@%^&";
           
            if (password.Length >= minLength)
            {
                score++;
            }
            if (password.Any(char.IsUpper))
            {
                score++;
            }
            if (password.Any(char.IsLower))
            {
                score++;
            }
            if (password.Any(char.IsDigit))
            {
                score++;
            }
            
            


            bool caractereSpeciale = false;
            ///aici verificam daca functia contine caractere speciale
            foreach (char c in password)
            {
                if (specialChars.Contains(c))
                {
                    caractereSpeciale = true;
                    break;
                }
            }
            if (caractereSpeciale)
            {
                score++;
            }
            switch (score)
            {
                case 5:
             
                    label8.Text = "Extremly strong password";
                    label8.ForeColor = System.Drawing.Color.Lime;
                    break;
                case 4:
                case 3:
                    label8.Text = "This is a strong password";
                    label8.ForeColor = System.Drawing.Color.Green;
                    
                    break;
                case 2:
                    label8.Text = "This is an average password";
                    label8.ForeColor = System.Drawing.Color.Orange;
                    
                    break;
                case 1:
                    label8.Text ="This is a weak password";
                    label8.ForeColor = System.Drawing.Color.Yellow;
                    
                    break;
            
                    
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void Sign_IN_Load(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
         
        

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void Signup()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                string pass = textBox4.Text;
                float def = 0;
                pass = Encrypt(pass);
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("UserAdd",sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Nume", textBox1.Text);
                sqlCmd.Parameters.AddWithValue("@Prenume", textBox2.Text);
                sqlCmd.Parameters.AddWithValue("@Scoala", textBox5.Text);
                sqlCmd.Parameters.AddWithValue("@Email", textBox3.Text);
                sqlCmd.Parameters.AddWithValue("@Parola", pass);
                sqlCmd.Parameters.AddWithValue("@media_mate", def);
                sqlCmd.Parameters.AddWithValue("@ultima_mate", def);
                sqlCmd.Parameters.AddWithValue("@medie_rom", def);
                sqlCmd.Parameters.AddWithValue("@ultima_rom", def);
                sqlCmd.Parameters.AddWithValue("@medie_info", def);
                sqlCmd.Parameters.AddWithValue("@ultima_info", def);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Registration Successfull!!");
                this.Hide();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            String password = textBox4.Text;
  
           
            bool signUp = false;
            
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text)|| String.IsNullOrEmpty(textBox3.Text)
                || String.IsNullOrEmpty(textBox4.Text)|| String.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Completati toate campurile!", "Eroare Inregistrare",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ///verificam emailul si parola
            else
            {
              
                if(score>3 && validEmail)
                {

                    signUp = true;
                }
                else
                {
                    MessageBox.Show("Datele introduse nu sunt valide!!!", "Eroare logare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if(signUp)
            {
                Signup();
            } 
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            String email = textBox3.Text;
            if(IsValid(email))
            {
                label7.Text = "Valid Email Adress";
                label7.ForeColor = System.Drawing.Color.Lime;
                validEmail = true;

            }
            else
            {
                label7.Text = "Invalid email adress";
                label7.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
