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
    public partial class SearchItems : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public SearchItems()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }
        public void Load_Data()
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ItemCode,ItemCatName,ItemSubCatName,ItemName,Attr5,Attr6,Attr7,Attr8,Attr9,Attr10,ItemQty,SaleRate,PurchaseRate from ItemGrid", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    String Des = "";
                    if (rd[3] != "")
                    {
                        Des += rd[3].ToString();
                    }
                    if (rd[4] != "")
                    {
                        Des += ", " + rd[4].ToString();
                    }
                    if (rd[5] != "")
                    {
                        Des += ", " + rd[5].ToString();
                    }
                    if (rd[6] != "")
                    {
                        Des += ", " + rd[6].ToString();
                    }
                    if (rd[7] != "")
                    {
                        Des += ", " + rd[7].ToString();
                    }
                    if (rd[8] != "")
                    {
                        Des += ", " + rd[8].ToString();
                    }
                    if (rd[9] != "")
                    {
                        Des += ", " + rd[9].ToString();
                    }
                    dataGridView1.Rows.Add(rd[0], Des, rd[1], rd[2], rd[10], rd[11], rd[12]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add("All");
            comboBox1.Text = "All";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemCatName from ItemCategory", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        public string itemcode
        {
            get { return c; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchItems_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == string.Empty)
            {
                Load_Data();
            }
            else
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select ItemCode,ItemCatName,ItemSubCatName,ItemName,Attr5,Attr6,Attr7,Attr8,Attr9,Attr10,ItemQty,SaleRate,PurchaseRate from ItemGrid where ItemName like '%" + textBox6.Text + "%' OR ItemCatName like '%" + textBox6.Text + "%' OR ItemSubCatName like '%" + textBox6.Text + "%' OR ItemCode like '%" + textBox6.Text + "%'", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        String Des = "";
                        if (rd[3] != "")
                        {
                            Des += rd[3].ToString();
                        }
                        if (rd[4] != "")
                        {
                            Des += ", " + rd[4].ToString();
                        }
                        if (rd[5] != "")
                        {
                            Des += ", " + rd[5].ToString();
                        }
                        if (rd[6] != "")
                        {
                            Des += ", " + rd[6].ToString();
                        }
                        if (rd[7] != "")
                        {
                            Des += ", " + rd[7].ToString();
                        }
                        if (rd[8] != "")
                        {
                            Des += ", " + rd[8].ToString();
                        }
                        if (rd[9] != "")
                        {
                            Des += ", " + rd[9].ToString();
                        }
                        dataGridView1.Rows.Add(rd[0], Des, rd[1], rd[2], rd[10],rd[11],rd[12]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
                c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                this.Close();
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                this.Close();
            }
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                button1_Click(sender, e);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                button1_Click(sender, e);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemSubCatName from ItemSubCatGrid where ItemCatName=@a", con);
                cmd.Parameters.AddWithValue("@a", comboBox1.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr[0].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ItemCode,ItemCatName,ItemSubCatName,ItemName,Attr5,Attr6,Attr7,Attr8,Attr9,Attr10,ItemQty,SaleRate,PurchaseRate from ItemGrid where ItemCatName = @a and ItemSubCatName = @b", con);
                cmd.Parameters.AddWithValue("@a", comboBox1.Text);
                cmd.Parameters.AddWithValue("@b", comboBox2.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    String Des = "";
                    if (rd[3] != "")
                    {
                        Des += rd[3].ToString();
                    }
                    if (rd[4] != "")
                    {
                        Des += ", " + rd[4].ToString();
                    }
                    if (rd[5] != "")
                    {
                        Des += ", " + rd[5].ToString();
                    }
                    if (rd[6] != "")
                    {
                        Des += ", " + rd[6].ToString();
                    }
                    if (rd[7] != "")
                    {
                        Des += ", " + rd[7].ToString();
                    }
                    if (rd[8] != "")
                    {
                        Des += ", " + rd[8].ToString();
                    }
                    if (rd[9] != "")
                    {
                        Des += ", " + rd[9].ToString();
                    }
                    dataGridView1.Rows.Add(rd[0], Des, rd[1], rd[2], rd[10], rd[11], rd[12]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load_Data();
        }
    }
}
