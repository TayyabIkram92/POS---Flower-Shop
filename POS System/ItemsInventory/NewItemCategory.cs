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
    public partial class NewItemCategory : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public NewItemCategory(string code)
        {
            InitializeComponent();
            c = code;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        public void Load_Data()
        {
            //if (c != "1")
            //{
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select max(ItemCatID) from ItemCategory", con);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd[0] != DBNull.Value)
                    {
                        int num = Convert.ToInt32(rd[0].ToString());
                        num++;
                        textBox4.Text = num.ToString();
                    }
                    else
                    {
                        textBox4.Text = "1";
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            //}
            //else
            //{
            //    textBox4.Text = "1";
            //}
        }
        private void NewItemCategory_Load(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("insert into ItemCategory(ItemCatCode,ItemCatName,IsActive) values(@c,@n,@i)", con);
                    cmd.Parameters.AddWithValue("@c", textBox4.Text);
                    cmd.Parameters.AddWithValue("@n", textBox9.Text);
                    cmd.Parameters.AddWithValue("@i", checkBox3.CheckState);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New Item Category added Successfully.");
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
