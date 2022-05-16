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
    public partial class Customers : Form
    {
        SqlConnection con = new SqlConnection();
        string c;
        public Customers()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            c = "";
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
                    dataGridView1.Rows.Add(rd[0], rd[1], rd[2]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void Customers_Load(object sender, EventArgs e)
        {
            Load_Data();
            dateTimePicker1.Value = DateTime.Now.AddMonths(-5);
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.CustomFormat = "  dd/MM/yyyy";
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
                        dataGridView1.Rows.Add(rd[0], rd[1], rd[2]);
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
                c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (c != "")
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Delete from Customers where CustomerID= @BC", con);
                            cmd.Parameters.AddWithValue("@BC", c);
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
                c = "1";
                NewCustomer n = new NewCustomer(c);
                n.ShowDialog();
            }
            else
            {
                c = "0";
                NewCustomer n = new NewCustomer(c);
                n.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Customers where CustomerID= @BC", con);
                cmd.Parameters.AddWithValue("@BC", c);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    code.Text = dr[1].ToString();
                    ename.Text = dr[2].ToString();
                    uname.Text = dr[3].ToString();
                    address.Text = dr[4].ToString();
                    phone.Text = "(" + dr[5].ToString() + ") - (" + dr[6].ToString() + ")";
                    area.Text = dr[7].ToString();
                    dis.Text = dr[9].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select AreaName from Areas where AreaCode= @a", con);
                cmd.Parameters.AddWithValue("@a", area.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    area.Text = area.Text.ToString() + "-" + dr[0].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            dataGridView2.Rows.Clear();
            Load_Sales();
            Load_Receipts();
            Load_Payments();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Selected to Edit.");
            }
            else
            {
                c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                EditCustomer E = new EditCustomer(c);
                E.ShowDialog();
            }
        }
        public void Load_Sales()
        {
            c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select SaleNo,format(SaleDate,'dd/MM/yyyy') as SaleDate,SaleAmount,SaleDescription from Sale where CustomerCode= @BC", con);
                cmd.Parameters.AddWithValue("@BC", c);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], "Credit Sale", dr[1], dr[2],dr[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        public void Load_Searched_Sales()
        {
            c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select SaleNo,format(SaleDate,'dd/MM/yyyy') as SaleDate,SaleAmount,SaleDescription from Sale where CustomerCode= @BC and SaleNo like '%" + textBox1.Text + "%'", con);
                cmd.Parameters.AddWithValue("@BC", c);
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
        public void Load_Receipts()
        {
            c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select VouncherNo,format(Date,'dd/MM/yyyy') as Date,Amount,Narration from CashReceiptVounchers where CustomerCode= @BC", con);
                cmd.Parameters.AddWithValue("@BC", c);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], "Cash Received", dr[1], dr[2], dr[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        public void Load_Searched_Receipts()
        {
            c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select VouncherNo,format(Date,'dd/MM/yyyy')as Date,Amount,Narration from CashReceiptVounchers where CustomerCode= @BC and VouncherNo like '%" + textBox1.Text + "%'", con);
                cmd.Parameters.AddWithValue("@BC", c);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], "Cash Received", dr[1], dr[2], dr[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        public void Load_Payments()
        {
            c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select VouncherNo,format(Date,'dd/MM/yyyy') as Date,Amount,Narration from CashPaymentVounchers where CustomerCode= @BC", con);
                cmd.Parameters.AddWithValue("@BC", c);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], "Cash Paid", dr[1], dr[2], dr[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        public void Load_Searched_Payments()
        {
            c = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select VouncherNo,format(Date,'dd/MM/yyyy') as Date,Amount,Narration from CashPaymentVounchers where CustomerCode= @BC and VouncherNo like '%" + textBox1.Text+"%'", con);
                cmd.Parameters.AddWithValue("@BC", c);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], "Cash Paid", dr[1], dr[2], dr[3]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (checkBox3.Checked == true)
            {
                Load_Sales();
            }
            if (checkBox2.Checked == true)
            {
                Load_Payments();
            }
            if (checkBox1.Checked == true)
            {
                Load_Receipts();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (checkBox3.Checked == true)
            {
                Load_Sales();
            }
            if (checkBox2.Checked == true)
            {
                Load_Payments();
            }
            if (checkBox1.Checked == true)
            {
                Load_Receipts();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (checkBox3.Checked == true)
            {
                Load_Sales();
            }
            if (checkBox2.Checked == true)
            {
                Load_Payments();
            }
            if (checkBox1.Checked == true)
            {
                Load_Receipts();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                dataGridView2.Rows.Clear();
                Load_Sales();
                Load_Receipts();
                Load_Payments();
            }
            else
            {
                dataGridView2.Rows.Clear();
                if (checkBox3.Checked == true)
                {
                    Load_Searched_Sales();
                }
                if (checkBox2.Checked == true)
                {
                    Load_Searched_Payments();
                }
                if (checkBox1.Checked == true)
                {
                    Load_Searched_Receipts();
                }
            }
        }
    }
}
