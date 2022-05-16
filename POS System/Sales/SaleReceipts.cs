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
    public partial class SaleReceipts : Form
    {
        SqlConnection con = new SqlConnection();
        string x;
        public SaleReceipts()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            x = "";
        }

        public void Load_Sales()
        {
            dataGridView2.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select SaleNo,format(SaleDate,'dd/MM/yyyy') as SaleDate,SaleAmount,SaleDescription from Sale where SaleDate between @a and @b", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], "Credit Sale", dr[1], dr[2], dr[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void SaleReceipts_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-1);
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.CustomFormat = "  dd/MM/yyyy";
            Load_Sales();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Load_Sales();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Load_Sales();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select SaleNo,format(SaleDate,'dd/MM/yyyy') as SaleDate,SaleAmount,SaleDescription from Sale where SaleDate between @a and @b and SaleNo like '%"+textBox6.Text+"%'", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], "Credit Sale", dr[1], dr[2], dr[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        public string value
        {
            get { return x; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                x = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
                this.Close();
            }
        }
    }
}
