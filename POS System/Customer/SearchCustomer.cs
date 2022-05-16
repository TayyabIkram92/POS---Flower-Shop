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
    public partial class SearchCustomer : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public SearchCustomer()
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
                SqlCommand cmd = new SqlCommand("Select * from Customers", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    dataGridView1.Rows.Add(rd[1],rd[2], rd[3], rd[7]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
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
                    SqlCommand cmd = new SqlCommand("Select * from Customers where CustomerEnglishName like '%" + textBox6.Text + "%' OR CustomerCode like '%" + textBox6.Text + "%'", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        dataGridView1.Rows.Add(rd[1],rd[2], rd[3], rd[7]);
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

        private void SearchCustomer_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        public string value
        {
            get { return c; }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                this.Close();
            }
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
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(sender,e);
            }
        }
    }
}
