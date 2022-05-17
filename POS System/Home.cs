using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;

namespace POS_System
{
    public partial class Home : Form
    {
        SqlConnection con = new SqlConnection();
        SqlConnection con2 = new SqlConnection();
        public Home()
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            Connection c2 = new Connection();
            con2.ConnectionString = c2.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }
        public void Load_Data()
        {
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.CustomFormat = "  dd/MM/yyyy";
            string saleno = "";
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from StockOutInformation where SaleDate between @a and @b", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[6],dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add("All");
            comboBox1.Text = "All";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemCatName from ItemCategory", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            //    string saleno = "";
            //    dataGridView1.Rows.Clear();
            //    try
            //    {
            //        con.Open();
            //        SqlCommand cmd = new SqlCommand("select SaleNo from Sale where SaleDate between @a and @b", con);
            //        cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
            //        cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
            //        SqlDataReader dr = cmd.ExecuteReader();
            //        while (dr.Read())
            //        {
            //            saleno = dr[0].ToString();
            //            con2.Open();
            //            SqlCommand cmd2 = new SqlCommand("select * from SalesDetail where SaleNo=@a", con2);
            //            cmd2.Parameters.AddWithValue("@a", saleno);
            //            SqlDataReader dr2 = cmd2.ExecuteReader();
            //            while (dr2.Read())
            //            {
            //                dataGridView1.Rows.Add(dr2[1], dr2[2], dr2[3], dr2[4], dr2[5]);
            //            }
            //            con2.Close();
            //        }
            //        con.Close();
            //    }
            //    catch (Exception exp)
            //    {
            //        MessageBox.Show(exp.ToString());
            //    }
            dataGridView2.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Item where ItemQty<Reorder", con);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    String Des = "";
                    if (rd[5] != "")
                    {
                        Des += rd[5].ToString();
                    }
                    if (rd[11] != "")
                    {
                        Des += ", " + rd[11].ToString();
                    }
                    if (rd[12] != "")
                    {
                        Des += ", " + rd[12].ToString();
                    }
                    if (rd[13] != "")
                    {
                        Des += ", " + rd[13].ToString();
                    }
                    if (rd[14] != "")
                    {
                        Des += ", " + rd[14].ToString();
                    }
                    if (rd[15] != "")
                    {
                        Des += ", " + rd[15].ToString();
                    }
                    if (rd[16] != "")
                    {
                        Des += ", " + rd[16].ToString();
                    }
                    dataGridView2.Rows.Add(Des, rd[10]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        public void Calculate_Profit()
        {
            float profit = 0;
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                profit+=float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            label4.Text = profit.ToString();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            Load_Data();
            Calculate_Profit();
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load_Data();
            Calculate_Profit();
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from StockOutInfo", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into StockOutInfo values (@a,@b,@c,@d,@e,@f)", con);
                    cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[i].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[5].Value);
                    cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[6].Value);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            SaleReportViewer s = new SaleReportViewer();
            CrystalReports.CrystalReport4 rpt = new CrystalReports.CrystalReport4();
            SqlDataAdapter sda = new SqlDataAdapter("select * from StockOutInfo", con);
            sda.SelectCommand.Parameters.AddWithValue("@a", dateTimePicker1.Value);
            sda.SelectCommand.Parameters.AddWithValue("@b", dateTimePicker2.Value);
            DataSet ds = new DataSet();
            sda.Fill(ds, "StockOutInfo");

            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No data Found", "CrystalReportWithOracle");
                return;
            }
            rpt.SetDataSource(ds);
            TextObject dates = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text2"];
            dates.Text = dateTimePicker2.Value.ToShortDateString();
            s.crystalReportViewer1.ReportSource = rpt;
            s.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from StockOutInformation where SaleDate between @a and @b", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[6], dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            comboBox1_SelectedIndexChanged(sender,e);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from StockOutInformation where SaleDate between @a and @b", con);
                cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[6], dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("All");
            comboBox2.Text = "All";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemSubCatName from ItemSubCatGrid where ItemCatName=@a", con);
                cmd.Parameters.AddWithValue("@a", comboBox1.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr[0].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            if (comboBox1.Text == "All")
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from StockOutInformation where SaleDate between @a and @b", con);
                    cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[6], dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
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
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from StockOutInformation where SaleDate between @a and @b and ItemCatName=@c", con);
                    cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@c", comboBox1.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[6], dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All")
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from StockOutInformation where SaleDate between @a and @b", con);
                    cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[6], dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
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
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from StockOutInformation where SaleDate between @a and @b and ItemCatName=@c and ItemSubCatName=@d", con);
                    cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@b", dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@c", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@d", comboBox2.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[6], dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
