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
using System.Configuration;


namespace POS_System
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection();
        public Login()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.user_24;
            panel1.BackColor = Color.DeepSkyBlue;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.padlock_4_24__1_;
            panel2.BackColor = Color.DeepSkyBlue;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==string.Empty || textBox2.Text == string.Empty)
            {
                if (textBox1.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox1, "warning");
                }
                if (textBox2.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox2, "warning");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select Username,Password from Users where Username=@u and Password=@p", con);
                    cmd.Parameters.AddWithValue("@u", textBox1.Text);
                    cmd.Parameters.AddWithValue("@p", textBox2.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        MainWindow frm = new MainWindow(textBox1.Text.ToString());
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password");
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
