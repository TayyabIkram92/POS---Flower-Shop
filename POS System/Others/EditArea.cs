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
    public partial class EditArea : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public EditArea(string code)
        {
            c = code;
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            
        }

        private void EditArea_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmnd = new SqlCommand("Select * from Areas where AreaID=@BC", con);
                cmnd.Parameters.AddWithValue("@BC", c);
                SqlDataReader rd = cmnd.ExecuteReader();
                if (rd.Read())
                {
                    textBox4.Text = rd[1].ToString();
                    textBox9.Text = rd[2].ToString();
                    string state = rd[3].ToString();
                    if (state == "True")
                    {
                        checkBox3.Checked = true;
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
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
                    SqlCommand cmd = new SqlCommand("update Areas set AreaName=@n,IsActive=@i where AreaCode=@c", con);
                    cmd.Parameters.AddWithValue("@c", textBox4.Text);
                    cmd.Parameters.AddWithValue("@n", textBox9.Text);
                    cmd.Parameters.AddWithValue("@i", checkBox3.CheckState);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Area updated Successfully.");
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
