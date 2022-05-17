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
using System.Globalization;

namespace POS_System
{
    public partial class AccountReceivable : Form
    {
        SqlConnection con = new SqlConnection();
        public AccountReceivable()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        private void Load_Area()
        {
            try
            {
                cboarea.Items.Clear();
                cboarea.Items.Add("All");
                cboarea.Text = "All";
                con.Open();
                SqlCommand cmd = new SqlCommand("Select AreaCode,AreaName from Areas", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cboarea.Items.Add(rd[0].ToString() + "-" + rd[1].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void AccountReceivable_Load(object sender, EventArgs e)
        {
            Load_Area();
        }
        private void Load_Records()
        {
            if (cboarea.Text == "All")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select CustomerUrduName,CustomerBalance from Customers where CustomerBalance>0", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0], "Customer", dr[1]);
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
                    SqlCommand cmd = new SqlCommand("Select SupplierUrduName,SupplierBalance from Suppliers where SupplierBalance<0", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0], "Supplier", dr[1]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }
        private void Load_Specific_Records()
        {
            if (cboarea.Text != "All")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select CustomerUrduName,CustomerBalance from Customers where CustomerBalance>0 and CustomerArea=@a", con);
                    cmd.Parameters.AddWithValue("@a", float.Parse(cboarea.Text.Substring(0, cboarea.Text.IndexOf("-")).ToString()));
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0], "Customer", dr[1]);
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
                    SqlCommand cmd = new SqlCommand("Select SupplierUrduName,SupplierBalance from Suppliers where SupplierBalance<0 and SupplierArea=@a", con);
                    cmd.Parameters.AddWithValue("@a", float.Parse(cboarea.Text.Substring(0, cboarea.Text.IndexOf("-")).ToString()));
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0], "Supplier", dr[1]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }
        private void cboarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Load_Records();
            Load_Specific_Records();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaleReportViewer s = new SaleReportViewer();
            CrystalReports.CrystalReport5 rpt = new CrystalReports.CrystalReport5();
            if (cboarea.Text == "All")
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select CustomerUrduName, CustomerBalance from Customers where CustomerBalance > 0 UNION Select SupplierUrduName,SupplierBalance from Suppliers where SupplierBalance<0", con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "Customers");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No data Found", "CrystalReportWithOracle");
                    return;
                }
                rpt.SetDataSource(ds);
                s.crystalReportViewer1.ReportSource = rpt;
                s.Show();
            }
            else
            {
                float x = float.Parse(cboarea.Text.Substring(0, cboarea.Text.IndexOf("-")).ToString());
                SqlDataAdapter sda = new SqlDataAdapter("Select CustomerUrduName, CustomerBalance from Customers where CustomerBalance > 0 and CustomerArea='"+x+ "' UNION Select SupplierUrduName,SupplierBalance from Suppliers where SupplierBalance<0 and SupplierArea='" + x + "'", con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "Customers");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No data Found", "CrystalReportWithOracle");
                    return;
                }
                rpt.SetDataSource(ds);
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
