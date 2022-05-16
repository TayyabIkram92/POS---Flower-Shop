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
    public partial class NewUOM : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public NewUOM(string code)
        {
            InitializeComponent();
            c = code;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        public void Load_Data()
        {
            if (c != "1")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select max(UOMID) from UOM", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        int num = Convert.ToInt32(rd[0].ToString());
                        num++;
                        textBox4.Text = num.ToString();
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                textBox4.Text = "1";
            }
        }
        private void NewUOM_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == string.Empty)
            {
                errorProvider1.SetError(textBox9, "warning");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UOM(UOMCode,UOMName,IsActive) values(@c,@n,@i)", con);
                    cmd.Parameters.AddWithValue("@c", textBox4.Text);
                    cmd.Parameters.AddWithValue("@n", textBox9.Text);
                    cmd.Parameters.AddWithValue("@i", checkBox3.CheckState);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New Unit of Measures added Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }
    }
}
