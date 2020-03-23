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
using System.IO;


namespace _10LaBac
{

    ///  functie care amesteca raspunsurile
    

    public partial class Matematica : Form
    {
        int corecte = 0, gresite = 0;
        private int totalSeconds,totalMinutes;
        Main mn = new Main();
        Instructiuni ins = new Instructiuni();
        bool isActive;
        int[] val = new int[55];
        string ras_corect = "";
        string[] answers = new string[4];
        int p = 0;
        double nota;
        double medie_mate;

        string connString = @"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// acuma vom avea o variabila in care vom stoca raspunsul
        /// ales de utilizator
        /// </summary>

        string ras_ales = "";

        public Matematica()
        {

            InitializeComponent();
            label4.Text = corecte.ToString();
            label7.Text = gresite.ToString();
            ///ascundem label 2 unde se afla mesajul de felicitare pentru user
            ///mesajul basic
            label2.Visible = false;
            //ascundem si nota si restul
            //aici avem :
            /// nota (default)
            label3.Visible = false;
            ///nota efectiva
            label6.Visible = false;
            ///mesaj generat de mine
            label10.Visible = false;


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        ///aici e click eventul pentru urmatoarea intrebare
        ///functia ASTA ARE NEVOIE DE EDIT LA MATE =)))
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ras_corect = ras_corect.Trim();
            ras_ales = raspuns.Text.Trim();
            ///prima data verificam daca s-a ales o intrebare

            ///inainte de a valida inputul trebuie sa il prelucram o leaca
            ras_ales = ras_ales.ToLower();


           if(ras_ales.Length == 0)
            {
                MessageBox.Show("Alege un raspuns!");
            }
            else
            {
                ///prima data trbuie sa verificam raspunsul
                if(ras_ales == ras_corect )
                {
                    corecte++;
                }
                else
                {
                    gresite++;
                }

                ////updatam raspunsurile corecte si gresite
                label4.Text = corecte.ToString();
                label7.Text = gresite.ToString();
                ///apoi trebuie sa facem toate raspunsurile albastre

                ///daca mai exista raspunsuri updatam
                ///daca nu afisam rezultatele finale
                
                if(p == 8)
                {
                    this.timer1.Enabled = false;

                    label1.Visible = false;
                    raspuns.Visible = false;
                    ///ascundem label 2 unde se afla mesajul de felicitare pentru user
                    ///mesajul basic
                    label2.Visible = true;
                    //ascundem si nota si restul
                    //aici avem :
                    /// nota (default)
                    label3.Visible = true;
                    ///nota efectiva
                    label6.Visible = true;
                    ///mesaj generat de mine
                    label10.Visible = true;
                    pictureBox2.Visible = false;


                    ///si acuma calculam nota
                    nota = corecte+2;
                    label6.Text = nota.ToString();

                    nota = Convert.ToDouble(nota);

                    if(nota <= 10 && nota >9)
                    {
                        label10.Text = "BRAVO! ESTI EXPERT ;)";
                    }
                    else if(nota >=7 && nota<=9)
                    {
                        label10.Text = "BRAVO! POTI MAI MULT ;)";
                    }
                    else if(nota>=5 && nota<7)
                    {
                        label10.Text = "Mai ai de lucru";
                    }
                    else
                    {
                        label10.Text = "Mai exerseaza si revino";
                    }

                    ///+ salvare in baza de date!!!!

                    ///desc
                    ///deschidem o conexiune si salvam in baza de date =))
                    ///

                    ////prima data extragem cea mai mare nota la romana
                    string comm;
                    comm = "SELECT * FROM UserRegistrationDB.dbo.tblUser WHERE logged_in = 'true'";


                    SqlConnection sqlConn = new SqlConnection(connString);
                    SqlDataAdapter sqlDA = new SqlDataAdapter(comm, sqlConn);
                    DataTable dtUser = new DataTable();
                    sqlDA.Fill(dtUser);
                    medie_mate = Double.Parse(dtUser.Rows[0]["medie_mate"].ToString());
                    
                    if(nota>medie_mate)
                    {
                        ///stocam nota asta ca nota maxima
                        ///daca nu lasam asa 
                        ///oricum de stocat stocam nota ca ultima nota

                        comm = "UPDATE UserRegistrationDB.dbo.tblUser SET ultima_mate = @mate, medie_mate=@medie  WHERE logged_in = 'true'; ";

                        using (SqlConnection sqlCon = new SqlConnection(connString))
                        {
                            sqlCon.Open();
                            SqlCommand sqlCmd = new SqlCommand(comm, sqlCon);
                            sqlCmd.Parameters.AddWithValue("@mate", nota);
                            sqlCmd.Parameters.AddWithValue("@medie", nota);
                            sqlCmd.ExecuteNonQuery();

                        }


                    }
                    else
                    {
                        ///daca nu o introducem doar ca ultima nota
                        comm = "UPDATE UserRegistrationDB.dbo.tblUser SET ultima_mate = @mate  WHERE logged_in='true';";

                        using (SqlConnection sqlCon = new SqlConnection(connString))
                        {
                            sqlCon.Open();
                            SqlCommand sqlCmd = new SqlCommand(comm, sqlCon);
                            sqlCmd.Parameters.AddWithValue("@mate", nota);
                            sqlCmd.ExecuteNonQuery();
                        }


                    }
                    


                        p++;
                }
                else if(p==9)
                {
                    this.Hide();
                    mn.Show();
                }  
                else
                {

                    raspuns.Text = "";
                    ras_ales = "";
                    ///dupa ce am facut asta trebuie sa generam alte raspunsuri
                    populare();
                }
              

            }

        }

        void start()
        {
            totalMinutes = 50;
            totalSeconds = (totalMinutes * 60);
            this.timer1.Enabled = true;

        }

        
        void populare()
        {
            ///extragem 
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand sqlCmd = new SqlCommand("UserRegistrationDB.dbo.ReadImage",conn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "UserRegistrationDB.dbo.ReadImage";
            sqlCmd.Parameters.Add("@imgId", val[p]);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ras_corect = dt.Rows[0]["raspuns_corect"].ToString();
                    MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["Image"]);
                    pictureBox2.Image = Image.FromStream(ms);
                    ///pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    ///pictureBox2.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


            p++;
        }


        /// <summary>
        /// aici generam indici random de care vom avea nevoie ulterior
        /// 
        /// </summary>
        public Random a = new Random();
        
        int MyNumber,k=0;
        void generare_arr()
        {
            ///vom genera in MyNumber valoare de care avem nevoie
            /// dar chestia asta o facem intr-un for deoarece avem nevoie de mai multe
            ///valori
            for(int i=0;i<8;i++)
            {
                MyNumber = a.Next(0, 15);
                while(val.Contains(MyNumber))
                {
                    MyNumber = a.Next(0,15);
                }
                val[k++] = MyNumber;
            }
            ///la terminarea while-ului avem toate valorile necesare

        }  

     
        private void Romana_Load(object sender, EventArgs e)
        {
            raspuns.MaxLength = 1;
            ////prima data vom genera un array de indici random pe seama carora se vor prelua valori din baza de date
            ///functia de generare este buna
            generare_arr();
            ///functia asta de start se ocupa de cronometru
            ///avand in vedere ca merge la fel la toate testele si asta este buna
            start();
            ///bun acuma cand se incarca ferestrea vom popula matricile cu valori
            ////edit
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

      

      

      
     

        ///astea sunt ca sa fac windowul sa se miste


        bool drag = false;
        Point start_point = new Point(0, 0);


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        
    
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }

        }

        private void Romana_MouseMove(object sender, MouseEventArgs e)
        {
            if(drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void Romana_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void Romana_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            drag = false;

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
