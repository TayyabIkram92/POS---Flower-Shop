using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace POS_System
{
    public partial class Pasword : Form
    {
        SqlConnection con = new SqlConnection();
        public Pasword()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        private void button7_Click(object sender, EventArgs e)
        {
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
                            Home h = new Home();
                            h.ShowDialog();
                            this.Close();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangePasword c = new ChangePasword();
            c.ShowDialog();
        }
    }
}
