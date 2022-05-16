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
    public partial class EditItemAttributes : Form
    {
        string c;
        SqlConnection con = new SqlConnection();

        private void EditItemAttributes_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmnd = new SqlCommand("Select * from ItemAttributesGrid where ItemAttributesID=@BC", con);
                cmnd.Parameters.AddWithValue("@BC", c);
                SqlDataReader rd1 = cmnd.ExecuteReader();
                if (rd1.Read())
                {
                    comboBox1.Text = rd1[3].ToString();
                    comboBox10.Text = rd1[2].ToString();
                    textBox4.Text = rd1[1].ToString();
                    textBox9.Text = rd1[4].ToString();
                    textBox1.Text = rd1[5].ToString();
                    textBox2.Text = rd1[6].ToString();
                    textBox3.Text = rd1[7].ToString();
                    textBox5.Text = rd1[8].ToString();
                    textBox6.Text = rd1[9].ToString();
                    textBox7.Text = rd1[10].ToString();
                    textBox8.Text = rd1[11].ToString();
                    textBox10.Text = rd1[12].ToString();
                    textBox11.Text = rd1[13].ToString();
                    string state = rd1[14].ToString();
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

        public EditItemAttributes(string code)
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            c = code;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty || comboBox10.Text == string.Empty)
            {
                if (comboBox10.Text == string.Empty)
                {
                    errorProvider1.SetError(comboBox10, "warning");
                }

                if (comboBox1.Text == string.Empty)
                {
                    errorProvider1.SetError(comboBox1, "warning");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("update ItemAttributes set Attr1=@a1,Attr2=@a2,Attr3=@a3,Attr4=@a4,Attr5=@a5,Attr6=@a6,Attr7=@a7,Attr8=@a8,Attr9=@a9,Attr10=@a10,IsActive=@a where ItemAttributesCode=@c", con);
                    cmd2.Parameters.AddWithValue("@c", textBox4.Text);
                    cmd2.Parameters.AddWithValue("@a1", textBox9.Text);
                    cmd2.Parameters.AddWithValue("@a2", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@a3", textBox2.Text);
                    cmd2.Parameters.AddWithValue("@a4", textBox3.Text);
                    cmd2.Parameters.AddWithValue("@a5", textBox5.Text);
                    cmd2.Parameters.AddWithValue("@a6", textBox6.Text);
                    cmd2.Parameters.AddWithValue("@a7", textBox7.Text);
                    cmd2.Parameters.AddWithValue("@a8", textBox8.Text);
                    cmd2.Parameters.AddWithValue("@a9", textBox10.Text);
                    cmd2.Parameters.AddWithValue("@a10", textBox11.Text);
                    cmd2.Parameters.AddWithValue("@a", checkBox3.CheckState);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item Attributes updated Successfully.");
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
