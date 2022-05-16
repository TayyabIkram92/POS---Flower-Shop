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
    public partial class Branches : Form
    {
        public string code;
        SqlConnection con = new SqlConnection();
        public Branches()
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            code = "";
        }
        public void Load_Data()
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Branch", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], rd[5], rd[6], rd[7], rd[8], rd[9], rd[10], rd[11]);
                }
                con.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void Branches_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count==0) {
                MessageBox.Show("No data to delete.");
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
                            SqlCommand cmd = new SqlCommand("Delete from Branch where BranchCode= @BC", con);
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
            NewBranch b = new NewBranch();
            b.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("No Data Selected to Edit.");
            }
            else
            {
                code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                EditBranch E = new EditBranch(code);
                E.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
