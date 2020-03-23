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

    ///  functie care amesteca raspunsurile
    

    public partial class Romana : Form
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
        double medie_rom;

        string connString = @"Data Source=DESKTOP-VDSNNJN\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// acuma vom avea o variabila in care vom stoca raspunsul
        /// ales de utilizator
        /// </summary>

        string ras_ales = "";

        public Romana()
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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
           ///prima data verificam daca s-a ales o intrebare
           if(ras_ales.Length ==0)
            {
                MessageBox.Show("Alege un raspuns!");
            }
            else
            {
                ///prima data trbuie sa verificam raspunsul
                if(ras_ales == ras_corect)
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
                
                if(p == 18)
                {
                    this.timer1.Enabled = false;

                    label1.Visible = false;
                    ///ascundem tot ce tine de intrebari
                    panel2.Visible = false;
                    A.Visible = false;
                    R1T.Visible = false;
                    panel3.Visible = false;
                    R2T.Visible = false;
                    B.Visible = false;
                    panel4.Visible = false;
                    R3T.Visible = false;
                    C.Visible = false;
                    panel5.Visible = false;
                    R4T.Visible = false;
                    D.Visible = false;

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



                    ///si acuma calculam nota
                    nota = (corecte /2)+1;
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
                        label10.Text = "Mai exerseaza si revino ;)";
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
                    medie_rom = Double.Parse(dtUser.Rows[0]["medie_rom"].ToString());
                    
                    if(nota>medie_rom)
                    {
                        ///stocam nota asta ca nota maxima
                        ///daca nu lasam asa 
                        ///oricum de stocat stocam nota ca ultima nota

                        comm = "UPDATE UserRegistrationDB.dbo.tblUser SET medie_rom = @medie, ultima_rom = @rom  WHERE logged_in='true';";

                        using (SqlConnection sqlCon = new SqlConnection(connString))
                        {
                            sqlCon.Open();
                            SqlCommand sqlCmd = new SqlCommand(comm, sqlCon);
                            sqlCmd.Parameters.AddWithValue("@rom", nota);
                            sqlCmd.Parameters.AddWithValue("@medie", nota);
                            sqlCmd.ExecuteNonQuery();

                        }


                    }
                    else
                    {
                        ///daca nu o introducem doar ca ultima nota
                        comm = "UPDATE UserRegistrationDB.dbo.tblUser SET ultima_rom = @rom  WHERE logged_in='true';";

                        using (SqlConnection sqlCon = new SqlConnection(connString))
                        {
                            sqlCon.Open();
                            SqlCommand sqlCmd = new SqlCommand(comm, sqlCon);
                            sqlCmd.Parameters.AddWithValue("@rom", nota);
                            sqlCmd.ExecuteNonQuery();
                        }


                    }
                    


                        p++;
                }
                else if(p==19)
                {
                    this.Hide();
                    mn.Show();
                }
                else
                {

                    /// PRIMUL RASPUNS -->PANNEL 2
                    /// ADICA A
                    panel2.BackColor = Color.RoyalBlue;
                    A.BackColor = Color.RoyalBlue;
                    R1T.BackColor = Color.RoyalBlue;
                    ///AL DOILEA RASPUNS --> PANNEL 3
                    ///adica B
                    panel3.BackColor = Color.RoyalBlue;
                    R2T.BackColor = Color.RoyalBlue;
                    B.BackColor = Color.RoyalBlue;
                    ///AL TREILEA RASPUNS --> PANNEL 4
                    ///adica C
                    panel4.BackColor = Color.RoyalBlue;
                    R3T.BackColor = Color.RoyalBlue;
                    C.BackColor = Color.RoyalBlue;
                    ///AL PATRULEA --> PANNEL 5
                    ///adica D
                    panel5.BackColor = Color.RoyalBlue;
                    R4T.BackColor = Color.RoyalBlue;
                    D.BackColor = Color.RoyalBlue;
                    ///si trebuie sa resetam raspunsul la  " "
                    ras_ales = "";
                    ///dupa ce am facut asta trebuie sa generam alte raspunsuri
                    populare();
                }
              

            }

        }

        void start()
        {
            totalMinutes = 5;
            totalSeconds = (totalMinutes * 60);
            this.timer1.Enabled = true;

        }

        
        void populare()
        {
            ///extragem 

          
            string comm = "SELECT * FROM UserRegistrationDB.dbo.TestRo";
            SqlConnection sqlConn = new SqlConnection(connString);
            SqlDataAdapter sqlDA = new SqlDataAdapter(comm, sqlConn);
            DataTable dtRaspunsuri = new DataTable();
            sqlDA.Fill(dtRaspunsuri);
            ///for(int i=0; i<dtRaspunsuri.Rows.Count;i++)
            ///vom extrage raspunsurile bazat pe un i
            
            label1.Text = dtRaspunsuri.Rows[val[p]]["Enunt"].ToString().Trim();
            ///stocam raspunsul corect pentru a-l verifica ulterior
            ras_corect = dtRaspunsuri.Rows[val[p]]["raspuns_corect"].ToString().Trim();
            ///stocam restul raspunsurilor
            answers[0] = dtRaspunsuri.Rows[val[p]]["raspuns_corect"].ToString().Trim(); 
            answers[1] = dtRaspunsuri.Rows[val[p]]["raspuns_gresit1"].ToString().Trim();
            answers[2] = dtRaspunsuri.Rows[val[p]]["raspuns_gresit2"].ToString().Trim();
            answers[3] = dtRaspunsuri.Rows[val[p]]["raspuns_gresit3"].ToString().Trim();
            ///amestecam raspunsurile
            answers.Shuffle<string>();
            answers.Shuffle<string>();
            //dupa ce le-am amestecat le dam pe butoane
            R1T.Text = answers[0];
            R2T.Text = answers[1];
            R3T.Text = answers[2];
            R4T.Text = answers[3];

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
            for(int i=0;i<18;i++)
            {
                MyNumber = a.Next(0, 30);
                while(val.Contains(MyNumber))
                {
                    MyNumber = a.Next(0,30);
                }
                val[k++] = MyNumber;
            }
            ///la terminarea while-ului avem toate valorile necesare

        }

     
        private void Romana_Load(object sender, EventArgs e)
        {
            ///prima data vom genera un array de indici random pe seama carora se vor prelua valori din baza de date
            generare_arr();
            ///functia asta de start se ocupa de cronometru
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            ///userul a apasat pe A deci ras_ales primeste valoare labelului
            ras_ales = R1T.Text;

            /// PRIMUL RASPUNS -->PANNEL 2
            /// ADICA A
            panel2.BackColor = Color.Yellow;
            A.BackColor = Color.Yellow;
            R1T.BackColor = Color.Yellow;
            ///AL DOILEA RASPUNS --> PANNEL 3
            ///adica B
            panel3.BackColor = Color.RoyalBlue;
            R2T.BackColor = Color.RoyalBlue;
            B.BackColor = Color.RoyalBlue;
            ///AL TREILEA RASPUNS --> PANNEL 4
            ///adica C
            panel4.BackColor = Color.RoyalBlue;
            R3T.BackColor = Color.RoyalBlue;
            C.BackColor = Color.RoyalBlue;
            ///AL PATRULEA --> PANNEL 5
            ///adica D
            panel5.BackColor = Color.RoyalBlue;
            R4T.BackColor = Color.RoyalBlue;
            D.BackColor = Color.RoyalBlue;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            ras_ales = R2T.Text;
            /// PRIMUL RASPUNS -->PANNEL 2
            /// ADICA A
            panel2.BackColor = Color.RoyalBlue;
            A.BackColor = Color.RoyalBlue;
            R1T.BackColor = Color.RoyalBlue;
            ///AL DOILEA RASPUNS --> PANNEL 3
            ///adica B
            panel3.BackColor = Color.Yellow;
            R2T.BackColor = Color.Yellow;
            B.BackColor = Color.Yellow;
            ///AL TREILEA RASPUNS --> PANNEL 4
            ///adica C
            panel4.BackColor = Color.RoyalBlue;
            R3T.BackColor = Color.RoyalBlue;
            C.BackColor = Color.RoyalBlue;
            ///AL PATRULEA --> PANNEL 5
            ///adica D
            panel5.BackColor = Color.RoyalBlue;
            R4T.BackColor = Color.RoyalBlue;
            D.BackColor = Color.RoyalBlue;

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            ras_ales = R3T.Text;

            /// PRIMUL RASPUNS -->PANNEL 2
            /// ADICA A
            panel2.BackColor = Color.RoyalBlue;
            A.BackColor = Color.RoyalBlue;
            R1T.BackColor = Color.RoyalBlue;
            ///AL DOILEA RASPUNS --> PANNEL 3
            ///adica B
            panel3.BackColor = Color.RoyalBlue;
            R2T.BackColor = Color.RoyalBlue;
            B.BackColor = Color.RoyalBlue;
            ///AL TREILEA RASPUNS --> PANNEL 4
            ///adica C
            panel4.BackColor = Color.Yellow;
            R3T.BackColor = Color.Yellow;
            C.BackColor = Color.Yellow;
            ///AL PATRULEA --> PANNEL 5
            ///adica D
            panel5.BackColor = Color.RoyalBlue;
            R4T.BackColor = Color.RoyalBlue;
            D.BackColor = Color.RoyalBlue;
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

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            drag = false;

        }

        private void panel5_MouseClick(object sender, MouseEventArgs e)
        {
            ras_ales = R4T.Text;

            /// PRIMUL RASPUNS -->PANNEL 2
            /// ADICA A
            panel2.BackColor = Color.RoyalBlue;
            A.BackColor = Color.RoyalBlue;
            R1T.BackColor = Color.RoyalBlue;
            ///AL DOILEA RASPUNS --> PANNEL 3
            ///adica B
            panel3.BackColor = Color.RoyalBlue;
            R2T.BackColor = Color.RoyalBlue;
            B.BackColor = Color.RoyalBlue;
            ///AL TREILEA RASPUNS --> PANNEL 4
            ///adica C
            panel4.BackColor = Color.RoyalBlue;
            R3T.BackColor = Color.RoyalBlue;
            C.BackColor = Color.RoyalBlue;
            ///AL PATRULEA --> PANNEL 5
            ///adica D
            panel5.BackColor = Color.Yellow;
            R4T.BackColor = Color.Yellow;
            D.BackColor = Color.Yellow;
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

    public static class MSSystemExtenstions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this T[] array)
        {
            rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n);
                n--;
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

}
