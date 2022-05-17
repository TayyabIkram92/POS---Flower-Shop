using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace POS_System
{
    public partial class ChangePasword : Form
    {
        SqlConnection con = new SqlConnection();
        public ChangePasword()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                con.Open();
                SqlCommand cmnd = new SqlCommand("Select pass from Password", con);
                SqlDataReader rd = cmnd.ExecuteReader();
                if (rd.Read())
                {
                    if (textBox9.Text != String.Empty)
                    {
                        if (rd[0].ToString() == textBox9.Text.ToString())
                        {
                            flag = true;
                        }
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            if (flag==true)
            {
                try
                {
                    con.Open();
                    SqlCommand cmnd = new SqlCommand("update Password set pass=@a", con);
                    cmnd.Parameters.AddWithValue("@a", textBox1.Text);
                    cmnd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Password updated Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                MessageBox.Show("Old Password Incorrect");
            }
        }
    }
}
