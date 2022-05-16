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
    public partial class NewItemSubCategory : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        string itemcatid;
        public NewItemSubCategory(string code)
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            itemcatid = "";
            c = code;
        }
        public void Load_Data()
        {
            comboBox10.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ItemCatName from ItemCategory", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox10.Items.Add(rd[0]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void NewItemSubCategory_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = string.Empty;

            //if (c != "1")
            //{
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select max(ItemSubCatID) from ItemSubCategory", con);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                if (rd1.Read())
                {
                    if (rd1[0] != DBNull.Value)
                    {
                        int num = Convert.ToInt32(rd1[0].ToString());
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
            try
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Select ItemCatID from ItemCategory where ItemCatName=@n", con);
                cmd2.Parameters.AddWithValue("@n", comboBox10.Text);
                SqlDataReader rd2 = cmd2.ExecuteReader();
                if (rd2.Read())
                {
                    itemcatid = rd2[0].ToString();
                    textBox4.Text += rd2[0].ToString();
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
            if(comboBox10.Text==string.Empty || textBox9.Text == string.Empty)
            {
                if (textBox9.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox9, "warning");
                }

                if (comboBox10.Text == string.Empty)
                {
                    errorProvider1.SetError(comboBox10, "warning");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Insert into ItemSubCategory(ItemCatID,ItemSubCatCode,ItemSubCatName,IsActive) values(@i,@c,@n,@a)", con);
                    cmd2.Parameters.AddWithValue("@i", itemcatid);
                    cmd2.Parameters.AddWithValue("@c", textBox4.Text);
                    cmd2.Parameters.AddWithValue("@n", textBox9.Text);
                    cmd2.Parameters.AddWithValue("@a", checkBox3.CheckState);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New Sub Category Item Added Successfully.");
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
