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
    public partial class ChangePassword : Form
    {
        SqlConnection con = new SqlConnection();
        public ChangePassword(string s)
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            textBox6.Text = s;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }
        void clear()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox9.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox6.Text == string.Empty || textBox4.Text == string.Empty || textBox9.Text == string.Empty || textBox1.Text == string.Empty)
            {
                if (textBox6.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox6, "warning");
                }
                if (textBox4.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox4, "warning");
                }
                if (textBox9.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox9, "warning");
                }
                if (textBox1.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox1, "warning");
                }
            }
            else
            {
                string pass = "";

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select Password from Users where Username=@u", con);
                    cmd.Parameters.AddWithValue("@u", textBox6.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        pass = rd[0].ToString();
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }

                if (pass != textBox4.Text)
                {
                    MessageBox.Show("Invalid Old Password");
                }
                else
                {
                    if (textBox9.Text == textBox1.Text)
                    {

                        try
                        {
                            con.Open();
                            SqlCommand cmnd = new SqlCommand("update Users set Password=@p where Username=@u", con);
                            cmnd.Parameters.AddWithValue("@p", textBox1.Text);
                            cmnd.Parameters.AddWithValue("@u", textBox6.Text);
                            cmnd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Password Changed Successfully.");
                            clear();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("New Password and Confirm Password do not match.");
                    }
                }
            }
        }
    }
}
