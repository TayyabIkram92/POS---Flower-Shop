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

namespace POS_System
{
    public partial class CustomerLedger : Form
    {
        int x = 0;
        SqlConnection con = new SqlConnection();
        public CustomerLedger()
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }
        private void CustomerLedger_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.CustomFormat = "  dd/MM/yyyy";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            x = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select CustomerCode,VouncherNo,Date as Date_,Narration,Amount as Debit,0 as Credit from CashPaymentVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, VouncherNo, Date as Date_, Narration, 0 as Debit, Amount as Credit from CashReceiptVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, SaleAmount as Debit, 0 as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, 0 as Debit, ReceivedAmount as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c order by Date_", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    x += int.Parse(dr[4].ToString());
                    x -= int.Parse(dr[5].ToString());
                    label4.Text = x.ToString();
                    dataGridView1.Rows.Add(dr[1], dr[2], dr[3], dr[4], dr[5],x);
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
                SqlCommand cmd = new SqlCommand("select CustomerUrduName,CustomerBalance,CustomerEnglishName from Customers where CustomerCode=@a", con);
                cmd.Parameters.AddWithValue("@a", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uname.Text = dr[0].ToString();
                    label5.Text = dr[1].ToString();
                    int a = int.Parse(label4.Text);
                    int b = int.Parse(label5.Text);
                    int res = a - b;
                    label6.Text = res.ToString();
                    label7.Text = dr[2].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            x = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select CustomerCode,VouncherNo,Date as Date_,Narration,Amount as Debit,0 as Credit from CashPaymentVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, VouncherNo, Date as Date_, Narration, 0 as Debit, Amount as Credit from CashReceiptVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, SaleAmount as Debit, 0 as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, 0 as Debit, ReceivedAmount as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c order by Date_", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    x += int.Parse(dr[4].ToString());
                    x -= int.Parse(dr[5].ToString());
                    label4.Text = x.ToString();
                    dataGridView1.Rows.Add(dr[1], dr[2], dr[3], dr[4], dr[5], x);
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
                SqlCommand cmd = new SqlCommand("select CustomerUrduName,CustomerBalance,CustomerEnglishName from Customers where CustomerCode=@a", con);
                cmd.Parameters.AddWithValue("@a", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uname.Text = dr[0].ToString();
                    label5.Text = dr[1].ToString();
                    int a = int.Parse(label4.Text);
                    int b = int.Parse(label5.Text);
                    int res = a - b;
                    label6.Text = res.ToString();
                    label7.Text = dr[2].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void txtcn_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            x = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select CustomerCode,VouncherNo,Date as Date_,Narration,Amount as Debit,0 as Credit from CashPaymentVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, VouncherNo, Date as Date_, Narration, 0 as Debit, Amount as Credit from CashReceiptVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, SaleAmount as Debit, 0 as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, 0 as Debit, ReceivedAmount as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c order by Date_", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    x += int.Parse(dr[4].ToString());
                    x -= int.Parse(dr[5].ToString());
                    label4.Text = x.ToString();
                    dataGridView1.Rows.Add(dr[1], dr[2], dr[3], dr[4], dr[5], x);
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
                SqlCommand cmd = new SqlCommand("select CustomerUrduName,CustomerBalance,CustomerEnglishName from Customers where CustomerCode=@a", con);
                cmd.Parameters.AddWithValue("@a",txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uname.Text = dr[0].ToString();
                    label5.Text = dr[1].ToString();
                    int a = int.Parse(label4.Text);
                    int b = int.Parse(label5.Text);
                    int res = a - b;
                    label6.Text = res.ToString();
                    label7.Text = dr[2].ToString();
                }
                con.Close();
            }catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchCustomer c = new SearchCustomer();
            c.ShowDialog();
            txtcn.Text = c.value;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtcn.Text == string.Empty)
            {
                errorProvider1.SetError(txtcn, "warning");
            }
            else
            {
                SaleReportViewer s = new SaleReportViewer();
                CrystalReports.CrystalReport3 rpt = new CrystalReports.CrystalReport3();
                SqlDataAdapter sda = new SqlDataAdapter("select CustomerCode,VouncherNo,Date as Date_,Narration,Amount as Debit,0 as Credit from CashPaymentVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, VouncherNo, Date as Date_, Narration, 0 as Debit, Amount as Credit from CashReceiptVounchers where Date between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, SaleAmount as Debit, 0 as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c UNION select CustomerCode, 'SAL-'+SaleNo as SaleNo, SaleDate as Date_, SaleDescription, 0 as Debit, ReceivedAmount as Credit from Sale where SaleDate between @a and @b and CustomerCode=@c order by Date_", con);
                sda.SelectCommand.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                sda.SelectCommand.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                sda.SelectCommand.Parameters.AddWithValue("@c", txtcn.Text);
                DataSet ds = new DataSet();
                sda.Fill(ds, "CustomerLedger");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No data Found", "CrystalReportWithOracle");
                    return;
                }
                rpt.SetDataSource(ds);
                TextObject dates = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text7"];
                dates.Text = "From " + dateTimePicker1.Value.ToShortDateString() + " Upto " + dateTimePicker2.Value.ToShortDateString();
                TextObject name = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text10"];
                name.Text = txtcn.Text + " - " + label7.Text;
                TextObject b1 = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text12"];
                b1.Text = label6.Text;
                TextObject b2 = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text14"];
                b2.Text = label5.Text;
                s.crystalReportViewer1.ReportSource = rpt;
                s.Show();
            }
        }
    }
}
