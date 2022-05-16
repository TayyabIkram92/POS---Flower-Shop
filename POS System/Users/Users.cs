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
    public partial class Users : Form
    {
        public string code;
        SqlConnection con = new SqlConnection();
        public Users()
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
                SqlCommand cmd = new SqlCommand("Select * from Users", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], rd[5]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void Users_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                code = "1";
                NewUser n = new NewUser(code);
                n.ShowDialog();
            }
            else
            {
                code = "0";
                NewUser n = new NewUser(code);
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
                code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                EditUser E = new EditUser(code);
                E.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Data to Delete.");
            }
            else
            {
                code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (code != "")
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Delete from Users where UserID= @BC", con);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
