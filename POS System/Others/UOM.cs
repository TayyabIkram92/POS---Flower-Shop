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
    public partial class UOM : Form
    {
        SqlConnection con = new SqlConnection();
        string code;
        public UOM()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            code = "";
        }

        public void Load_Data()
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from UOM", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void UOM_Load(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Select * from UOM where UOMCode like '%" + textBox6.Text + "%' OR UOMName like '%" + textBox6.Text + "%'", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Data to Delete.");
            }
            else
            {
                code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (code != "")
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Delete from UOM where UOMCode= @BC", con);
                            cmd.Parameters.AddWithValue("@BC", code);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }
                    Load_Data();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                code = "1";
                NewUOM n = new NewUOM(code);
                n.ShowDialog();
            }
            else
            {
                code = "0";
                NewUOM n = new NewUOM(code);
                n.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Selected to Edit.");
            }
            else
            {
                code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                EditUOM E = new EditUOM(code);
                E.ShowDialog();
            }
        }
    }
}
