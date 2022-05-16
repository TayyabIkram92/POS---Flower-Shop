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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text.RegularExpressions;

namespace POS_System
{
    public partial class CashPaymentVouncher : Form
    {
        SqlConnection con = new SqlConnection();
        public CashPaymentVouncher()
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        public void Load_Data()
        {
            bool flag = false;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select VouncherNo from CashPaymentVounchers", con);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    flag = true;
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            if (flag == true)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select max(VouncherNo) from CashPaymentVounchers", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        string vn = Regex.Replace(rd[0].ToString(), "[^0-9.]", "");
                        double num = Convert.ToDouble(vn);
                        num++;
                        txtsaleno.Text = "CPV-"+num.ToString();
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                txtsaleno.Text = "CPV-10000001";
            }
        }
        private void CashPaymentVouncher_Load(object sender, EventArgs e)
        {
            Load_Data();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            SearchCustomer c = new SearchCustomer();
            c.ShowDialog();
            txtcn.Text = c.value;
        }

        private void txtcn_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select CustomerUrduName,CustomerBalance,CustomerEnglishName from Customers where CustomerCode=@e", con);
                cmd.Parameters.AddWithValue("@e", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uname.Text = dr[0].ToString();
                    if (dr[1].ToString() == string.Empty)
                    {
                        label5.Text = "0";
                    }
                    else
                    {
                        label5.Text = dr[1].ToString();
                    }
                    dataGridView2.Rows.Add(txtcn.Text,dr[2],"0","Cash Paid");

                    if (dataGridView2.Rows.Count > 0)
                    {
                        dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2];
                        dataGridView2.BeginEdit(true);
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtcn.Text = "";
            uname.Text = "------------";
            dataGridView2.Rows.Clear();
            label5.Text = "0";
            textBox4.Text = "";
            Load_Data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == string.Empty)
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (String.IsNullOrEmpty(dataGridView2.Rows[0].Cells[3].Value as String))
                    {
                        dataGridView2.Rows[0].Cells[3].Value = "Cash Paid";
                    }
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into CashPaymentVounchers values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k)", con);
                        cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                        cmd.Parameters.AddWithValue("@b", txtcn.Text);
                        cmd.Parameters.AddWithValue("@c", dataGridView2.Rows[0].Cells[1].Value.ToString());
                        cmd.Parameters.AddWithValue("@d", dataGridView2.Rows[0].Cells[2].Value.ToString());
                        cmd.Parameters.AddWithValue("@e", dataGridView2.Rows[0].Cells[3].Value.ToString());
                        cmd.Parameters.AddWithValue("@f", textBox1.Text);
                        cmd.Parameters.AddWithValue("@g", textBox2.Text);
                        cmd.Parameters.AddWithValue("@h", textBox3.Text);
                        cmd.Parameters.AddWithValue("@i", "0");
                        cmd.Parameters.AddWithValue("@j", "0");
                        cmd.Parameters.AddWithValue("@k", dateTimePicker1.Value);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }

                    try
                    {
                        if (dataGridView2.Rows[0].Cells[3].Value.ToString() != "salary")
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("update Customers set CustomerBalance+=@a where CustomerCode=@b", con);
                            cmd.Parameters.AddWithValue("@a", dataGridView2.Rows[0].Cells[2].Value.ToString());
                            cmd.Parameters.AddWithValue("@b", txtcn.Text);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        MessageBox.Show("Cash Payment Vouncher Saved Successfully.");
                        button5_Click(sender, e);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                }
                else
                {
                    errorProvider1.SetError(label5, "warning");
                }
            }
            if (txtcn.Text == string.Empty)
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (String.IsNullOrEmpty(dataGridView2.Rows[0].Cells[3].Value as String))
                    {
                        dataGridView2.Rows[0].Cells[3].Value = "Cash Paid";
                    }
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into CashPaymentVounchers values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k)", con);
                        cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                        cmd.Parameters.AddWithValue("@b", "0");
                        cmd.Parameters.AddWithValue("@c", "0");
                        cmd.Parameters.AddWithValue("@d", dataGridView2.Rows[0].Cells[2].Value.ToString());
                        cmd.Parameters.AddWithValue("@e", dataGridView2.Rows[0].Cells[3].Value.ToString());
                        cmd.Parameters.AddWithValue("@f", textBox1.Text);
                        cmd.Parameters.AddWithValue("@g", textBox2.Text);
                        cmd.Parameters.AddWithValue("@h", textBox3.Text);
                        cmd.Parameters.AddWithValue("@i", textBox4.Text);
                        cmd.Parameters.AddWithValue("@j", dataGridView2.Rows[0].Cells[1].Value.ToString());
                        cmd.Parameters.AddWithValue("@k", dateTimePicker1.Value);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }

                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update Suppliers set SupplierBalance-=@a where SupplierCode=@b", con);
                        cmd.Parameters.AddWithValue("@a", dataGridView2.Rows[0].Cells[2].Value.ToString());
                        cmd.Parameters.AddWithValue("@b", textBox4.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Cash Payment Vouncher Saved Successfully.");
                        button5_Click(sender, e);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                }
                else
                {
                    errorProvider1.SetError(label5, "warning");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtcn.Text = "";
            SearchSupplier c = new SearchSupplier();
            c.ShowDialog();
            textBox4.Text = c.value;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select SupplierUrduName,SupplierBalance,SupplierEnglishName from Suppliers where SupplierCode=@e", con);
                cmd.Parameters.AddWithValue("@e", textBox4.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uname.Text = dr[0].ToString();
                    if (dr[1].ToString() == string.Empty)
                    {
                        label5.Text = "0";
                    }
                    else
                    {
                        label5.Text = dr[1].ToString();
                    }
                    dataGridView2.Rows.Add(txtcn.Text, dr[2], "0", "Cash Paid");

                    if (dataGridView2.Rows.Count > 0)
                    {
                        dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2];
                        dataGridView2.BeginEdit(true);
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
