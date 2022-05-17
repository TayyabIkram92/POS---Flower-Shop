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
    public partial class ProfitByCustomer : Form
    {
        SqlConnection con = new SqlConnection();
        public ProfitByCustomer()
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        private void ProfitByCustomer_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.CustomFormat = "  dd/MM/yyyy";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select SaleNo,format(SaleDate,'dd/MM/yyyy'),CustomerDesciption,TotalSale,TotalCost,GrossProfit from GrossProfitReport where SaleDate between @a and @b and SUBSTRING(CustomerDesciption,0,CHARINDEX('-',CustomerDesciption,0))=@c", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
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
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select SaleNo,format(SaleDate,'dd/MM/yyyy'),CustomerDesciption,TotalSale,TotalCost,GrossProfit from GrossProfitReport where SaleDate between @a and @b and SUBSTRING(CustomerDesciption,0,CHARINDEX('-',CustomerDesciption,0))=@c", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
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
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select SaleNo,format(SaleDate,'dd/MM/yyyy'),CustomerDesciption,TotalSale,TotalCost,GrossProfit from GrossProfitReport where SaleDate between @a and @b and SUBSTRING(CustomerDesciption,0,CHARINDEX('-',CustomerDesciption,0))=@c", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@c", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
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
            SearchCustomer c = new SearchCustomer();
            c.ShowDialog();
            txtcn.Text = c.value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtcn.Text == string.Empty)
            {
                errorProvider1.SetError(txtcn,"warning");
            }
            else
            {
                SaleReportViewer s = new SaleReportViewer();
                CrystalReports.CrystalReport2 rpt = new CrystalReports.CrystalReport2();
                SqlDataAdapter sda = new SqlDataAdapter("select SaleNo,format(SaleDate,'dd/MM/yyyy'),CustomerDesciption,TotalSale,TotalCost,GrossProfit from GrossProfitReport where SaleDate between @a and @b and SUBSTRING(CustomerDesciption,0,CHARINDEX('-',CustomerDesciption,0))=@c", con);
                sda.SelectCommand.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                sda.SelectCommand.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                sda.SelectCommand.Parameters.AddWithValue("@c", txtcn.Text);
                DataSet ds = new DataSet();
                sda.Fill(ds, "GrossProfitReport");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No data Found", "CrystalReportWithOracle");
                    return;
                }
                rpt.SetDataSource(ds);
                TextObject dates = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text3"];
                dates.Text = "From " + dateTimePicker1.Value.ToShortDateString() + " Upto " + dateTimePicker2.Value.ToShortDateString();
                s.crystalReportViewer1.ReportSource = rpt;
                s.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
