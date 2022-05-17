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

namespace POS_System
{
    public partial class Sales : Form
    {
        float complete, part;
        SqlConnection con = new SqlConnection();
        float num1, num2, num3, res;

        private void Sales_Load(object sender, EventArgs e)
        {
            Load_Data();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
            textBox1.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchCustomer c = new SearchCustomer();
            c.ShowDialog();
            txtcn.Text = c.value;
        }

        private void txtcn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select CustomerUrduName,CustomerBalance from Customers where CustomerCode=@e", con);
                cmd.Parameters.AddWithValue("@e", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uname.Text = dr[0].ToString();
                    if (dr[1].ToString() == string.Empty)
                    {
                        lblpb.Text = "0";
                    }
                    else
                    {
                        lblpb.Text = dr[1].ToString();
                    }
                }
                con.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(exp.ToString());
            }

            label7.Text = (float.Parse(label11.Text.ToString()) + float.Parse(lblpb.Text.ToString())).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtsaleno.Text = "";
            txtcn.Text = "";
            uname.Text = "-------------";
            lblpb.Text = "0";
            label7.Text = "0";
            textBox1.Text = "";
            label8.Text = "0";
            dataGridView1.Rows.Clear();
            label11.Text = "0";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label6.Text = "0";
            button4.Visible = true;
            button3.Text = "Save and Print";
            button3.Width = 153;
            Load_Data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label11.Text != "0")
            {
                listBox1.Items.Insert(0, label11.Text);
            }
            if (dataGridView1.Rows.Count < 0 || textBox2.Text == string.Empty)
            {
                if (dataGridView1.Rows.Count < 0)
                {
                    errorProvider1.SetError(textBox1, "warning");
                }
                if (textBox2.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox2, "warning");
                }
                if (txtcn.Text == string.Empty)
                {
                    errorProvider1.SetError(txtcn, "warning");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Customers set CustomerBalance=@a where CustomerCode=@c", con);
                    cmd.Parameters.AddWithValue("@a", label6.Text);
                    cmd.Parameters.AddWithValue("@c", txtcn.Text);
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
                    SqlCommand cmd = new SqlCommand("delete from saledataset", con);
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
                        SqlCommand cmd = new SqlCommand("Insert into saledataset values (@a,@b,@c,@d,@e)", con);
                        cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@c", "");
                        cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[4].Value);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                    try
                    {
                        res = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update Item set ItemQty-=@a where ItemCode=@b", con);
                        cmd.Parameters.AddWithValue("@a", res);
                        cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[7].Value);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into SalesDetail(SaleNo,Profit,ItemDescription,Rate,Qty,NetAmount,PurchaseRate,ItemName,ItemCode) values(@a,@b,@c,@d,@e,@f,@g,@h,@i)", con);
                        cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                        cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[0].Value);
                        cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[1].Value);
                        cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[2].Value);
                        cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[3].Value);
                        cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[4].Value);
                        cmd.Parameters.AddWithValue("@g", dataGridView1.Rows[i].Cells[5].Value);
                        cmd.Parameters.AddWithValue("@h", dataGridView1.Rows[i].Cells[6].Value);
                        cmd.Parameters.AddWithValue("@i", dataGridView1.Rows[i].Cells[7].Value);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                }
                try
                {
                    if (textBox3.Text == string.Empty)
                    {
                        textBox3.Text = "Credit Sale";
                    }
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Sale(SaleNo,CustomerCode,SaleTime,SaleDate,SaleTotalProfit,SaleAmount,PreviousAmount,TotalAmount,ReceivedAmount,RemainingAmount,SaleDescription,SaleRemarks,SystemNotes) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m)", con);
                    cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                    cmd.Parameters.AddWithValue("@b", txtcn.Text);
                    cmd.Parameters.AddWithValue("@c", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@e", label8.Text);
                    cmd.Parameters.AddWithValue("@f", label11.Text);
                    cmd.Parameters.AddWithValue("@g", lblpb.Text);
                    cmd.Parameters.AddWithValue("@h", label7.Text);
                    cmd.Parameters.AddWithValue("@i", textBox2.Text);
                    cmd.Parameters.AddWithValue("@j", label6.Text);
                    cmd.Parameters.AddWithValue("@k", textBox3.Text);
                    cmd.Parameters.AddWithValue("@l", textBox4.Text);
                    cmd.Parameters.AddWithValue("@m", textBox5.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Sale Info Saved Successfully.");
                    button5_Click(sender, e);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                label6.Text = (float.Parse(label7.Text) - float.Parse(textBox2.Text)).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (label11.Text != "0")
            {
                listBox1.Items.Insert(0, label11.Text);
            }
            if (dataGridView1.Rows.Count < 0 || textBox2.Text == string.Empty)
            {
                if (dataGridView1.Rows.Count < 0)
                {
                    errorProvider1.SetError(textBox1, "warning");
                }
                if (textBox2.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox2, "warning");
                }
                if (txtcn.Text == string.Empty)
                {
                    errorProvider1.SetError(txtcn, "warning");
                }
            }
            else
            {
                if (checkBox2.Checked == true) {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("delete from saledataset", con);
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
                            SqlCommand cmd = new SqlCommand("Insert into saledataset values (@a,@b,@c,@d,@e)", con);
                            cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[i].Cells[1].Value);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[3].Value);
                            cmd.Parameters.AddWithValue("@c", "");
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[2].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[4].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into SalesDetail(SaleNo,Profit,ItemDescription,Rate,Qty,NetAmount,PurchaseRate,ItemName,ItemCode) values(@a,@b,@c,@d,@e,@f,@g,@h,@i)", con);
                            cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[0].Value);
                            cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[1].Value);
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[2].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[3].Value);
                            cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[4].Value);
                            cmd.Parameters.AddWithValue("@g", dataGridView1.Rows[i].Cells[5].Value);
                            cmd.Parameters.AddWithValue("@h", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@i", dataGridView1.Rows[i].Cells[7].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }
                    try
                    {
                        if (textBox3.Text == string.Empty)
                        {
                            textBox3.Text = "Pre Sale";
                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into Sale(SaleNo,CustomerCode,SaleTime,SaleDate,SaleTotalProfit,SaleAmount,PreviousAmount,TotalAmount,ReceivedAmount,RemainingAmount,SaleDescription,SaleRemarks,SystemNotes) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m)", con);
                        cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                        cmd.Parameters.AddWithValue("@b", txtcn.Text);
                        cmd.Parameters.AddWithValue("@c", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@e", label8.Text);
                        cmd.Parameters.AddWithValue("@f", label11.Text);
                        cmd.Parameters.AddWithValue("@g", lblpb.Text);
                        cmd.Parameters.AddWithValue("@h", label7.Text);
                        cmd.Parameters.AddWithValue("@i", textBox2.Text);
                        cmd.Parameters.AddWithValue("@j", label6.Text);
                        cmd.Parameters.AddWithValue("@k", textBox3.Text);
                        cmd.Parameters.AddWithValue("@l", textBox4.Text);
                        cmd.Parameters.AddWithValue("@m", textBox5.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Sale Info Saved Successfully.");
                        //button5_Click(sender, e);
                        int nag = dataGridView1.Rows.Count;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "0")
                            {
                                nag--;
                            }
                        }
                        SaleReportViewer sz = new SaleReportViewer();
                        CrystalReports.TilesSaleReport rptz = new CrystalReports.TilesSaleReport();
                        SqlDataAdapter sdaz = new SqlDataAdapter("select * from saledataset", con);
                        DataSet dsz = new DataSet();
                        sdaz.Fill(dsz, "saledataset");

                        if (dsz.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("No data Found", "CrystalReportWithOracle");
                            return;
                        }
                        rptz.SetDataSource(dsz);
                        TextObject r_info = (TextObject)rptz.ReportDefinition.Sections["Section1"].ReportObjects["Text2"];
                        r_info.Text = "Pre Sale Receipt";
                        TextObject salenoz = (TextObject)rptz.ReportDefinition.Sections["Section1"].ReportObjects["Text4"];
                        salenoz.Text = txtsaleno.Text;
                        TextObject datez = (TextObject)rptz.ReportDefinition.Sections["Section1"].ReportObjects["Text6"];
                        datez.Text = DateTime.Now.ToShortDateString();
                        TextObject timez = (TextObject)rptz.ReportDefinition.Sections["Section1"].ReportObjects["Text7"];
                        timez.Text = " " + DateTime.Now.ToString("hh:mm tt");
                        TextObject customercodez = (TextObject)rptz.ReportDefinition.Sections["Section1"].ReportObjects["Text8"];
                        customercodez.Text = txtcn.Text;
                        TextObject customernamez = (TextObject)rptz.ReportDefinition.Sections["Section2"].ReportObjects["Text12"];
                        customernamez.Text = uname.Text;
                        TextObject mojodaz = (TextObject)rptz.ReportDefinition.Sections["Section4"].ReportObjects["Text16"];
                        mojodaz.Text = label11.Text;
                        //TextObject total_nag = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text21"];
                        //total_nag.Text = nag.ToString();
                        TextObject sabkaz = (TextObject)rptz.ReportDefinition.Sections["Section4"].ReportObjects["Text15"];
                        sabkaz.Text = lblpb.Text;
                        TextObject totalraqmz = (TextObject)rptz.ReportDefinition.Sections["Section4"].ReportObjects["Text18"];
                        totalraqmz.Text = label7.Text;
                        TextObject wasoolz = (TextObject)rptz.ReportDefinition.Sections["Section4"].ReportObjects["Text20"];
                        wasoolz.Text = textBox2.Text;
                        TextObject bakayaraqmz = (TextObject)rptz.ReportDefinition.Sections["Section4"].ReportObjects["Text22"];
                        bakayaraqmz.Text = label6.Text;
                        //rpt.PrintOptions.PrinterName = "BlackCopper 80mm Series";
                        //rpt.PrintToPrinter(1, false, 0, 0);
                        sz.crystalReportViewer1.ReportSource = rptz;
                        button5_Click(sender, e);
                        sz.Show();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                
            }
                else
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update Customers set CustomerBalance=@a where CustomerCode=@c", con);
                        cmd.Parameters.AddWithValue("@a", label6.Text);
                        cmd.Parameters.AddWithValue("@c", txtcn.Text);
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
                        SqlCommand cmd = new SqlCommand("delete from saledataset", con);
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
                            SqlCommand cmd = new SqlCommand("Insert into saledataset values (@a,@b,@c,@d,@e)", con);
                            cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[i].Cells[1].Value);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[3].Value);
                            cmd.Parameters.AddWithValue("@c", "");
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[2].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[4].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                        try
                        {
                            res = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Update Item set ItemQty-=@a where ItemCode=@b", con);
                            cmd.Parameters.AddWithValue("@a", res);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[7].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into SalesDetail(SaleNo,Profit,ItemDescription,Rate,Qty,NetAmount,PurchaseRate,ItemName,ItemCode) values(@a,@b,@c,@d,@e,@f,@g,@h,@i)", con);
                            cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[0].Value);
                            cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[1].Value);
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[2].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[3].Value);
                            cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[4].Value);
                            cmd.Parameters.AddWithValue("@g", dataGridView1.Rows[i].Cells[5].Value);
                            cmd.Parameters.AddWithValue("@h", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@i", dataGridView1.Rows[i].Cells[7].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }
                    try
                    {
                        if (textBox3.Text == string.Empty)
                        {
                            textBox3.Text = "Credit Sale";
                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into Sale(SaleNo,CustomerCode,SaleTime,SaleDate,SaleTotalProfit,SaleAmount,PreviousAmount,TotalAmount,ReceivedAmount,RemainingAmount,SaleDescription,SaleRemarks,SystemNotes) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m)", con);
                        cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                        cmd.Parameters.AddWithValue("@b", txtcn.Text);
                        cmd.Parameters.AddWithValue("@c", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@e", label8.Text);
                        cmd.Parameters.AddWithValue("@f", label11.Text);
                        cmd.Parameters.AddWithValue("@g", lblpb.Text);
                        cmd.Parameters.AddWithValue("@h", label7.Text);
                        cmd.Parameters.AddWithValue("@i", textBox2.Text);
                        cmd.Parameters.AddWithValue("@j", label6.Text);
                        cmd.Parameters.AddWithValue("@k", textBox3.Text);
                        cmd.Parameters.AddWithValue("@l", textBox4.Text);
                        cmd.Parameters.AddWithValue("@m", textBox5.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Sale Info Saved Successfully.");
                        //button5_Click(sender, e);
                        int nag = dataGridView1.Rows.Count;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "0")
                            {
                                nag--;
                            }
                        }
                        SaleReportViewer s = new SaleReportViewer();
                        CrystalReports.TilesSaleReport rpt = new CrystalReports.TilesSaleReport();
                        SqlDataAdapter sda = new SqlDataAdapter("select * from saledataset", con);
                        DataSet ds = new DataSet();
                        sda.Fill(ds, "saledataset");

                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("No data Found", "CrystalReportWithOracle");
                            return;
                        }
                        rpt.SetDataSource(ds);
                        TextObject saleno = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text4"];
                        saleno.Text = txtsaleno.Text;
                        TextObject date = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text6"];
                        date.Text = DateTime.Now.ToShortDateString();
                        TextObject time = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text7"];
                        time.Text = " " + DateTime.Now.ToString("hh:mm tt");
                        TextObject customercode = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text8"];
                        customercode.Text = txtcn.Text;
                        TextObject customername = (TextObject)rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text12"];
                        customername.Text = uname.Text;
                        TextObject mojoda = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text16"];
                        mojoda.Text = label11.Text;
                        //TextObject total_nag = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text21"];
                        //total_nag.Text = nag.ToString();
                        TextObject sabka = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text15"];
                        sabka.Text = lblpb.Text;
                        TextObject totalraqm = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text18"];
                        totalraqm.Text = label7.Text;
                        TextObject wasool = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text20"];
                        wasool.Text = textBox2.Text;
                        TextObject bakayaraqm = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text22"];
                        bakayaraqm.Text = label6.Text;
                        //rpt.PrintOptions.PrinterName = "BlackCopper 80mm Series";
                        //rpt.PrintToPrinter(1, false, 0, 0);
                        s.crystalReportViewer1.ReportSource = rpt;
                        button5_Click(sender, e);
                        s.Show();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Select();
            }
            if (e.Control && e.KeyCode == Keys.P)
            {
                button3_Click(sender, e);
            }
            if (e.KeyCode == Keys.Delete)
            {
                num1 = float.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[4].Value.ToString());
                num2 = float.Parse(label11.Text.ToString());
                res = num2 - num1;
                label11.Text = res.ToString();
                num2 = float.Parse(label7.Text.ToString());
                res = num2 - num1;
                label7.Text = res.ToString();
            }
        }

        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                button3_Click(sender, e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                button3_Click(sender, e);
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Select();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                button3_Click(sender, e);
            }
        }

        private void txtcn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                button3_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaleReceipts s = new SaleReceipts();
            s.ShowDialog();
            txtsaleno.Text = s.value;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Sale where SaleNo=@a", con);
                cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtcn.Text = dr[2].ToString();
                    label8.Text = dr[3].ToString();
                    label11.Text = dr[4].ToString();
                    lblpb.Text = dr[5].ToString();
                    label7.Text = dr[6].ToString();
                    textBox2.Text = dr[7].ToString();
                    label6.Text = dr[8].ToString();
                    textBox3.Text = dr[9].ToString();
                    textBox4.Text = dr[10].ToString();
                    textBox5.Text = dr[11].ToString();
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
                SqlCommand cmd = new SqlCommand("select * from SalesDetail where SaleNo=@a", con);
                cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            button4.Visible = false;
            button3.Text = "Print";
            button3.Width = 90;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (e.ColumnIndex == 3)
                    {
                        bool flag = false;
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select ItemQty from Item where ItemCode=@a", con);
                            cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                if (float.Parse(dr[0].ToString()) < float.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()))
                                {
                                    flag = true;
                                }
                            }
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                        if (flag == false)
                        {
                            num1 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                            res = (num1 * num2) + 0.5F;
                            dataGridView1.Rows[e.RowIndex].Cells[4].Value = (int)res;

                            num1 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                            num3 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                            res = num1 - (num2 * num3);
                            dataGridView1.Rows[e.RowIndex].Cells[0].Value = res;

                            float profit = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                profit += float.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            }
                            label8.Text = profit.ToString();

                            float totalamount = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                totalamount += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            }
                            label11.Text = totalamount.ToString();

                            label7.Text = (float.Parse(label11.Text.ToString()) + float.Parse(lblpb.Text.ToString())).ToString();
                        }
                        else {
                            MessageBox.Show("Not Enough Products");
                        }
                    }
                    if (e.ColumnIndex == 2)
                    {
                        num1 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                        complete = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                        if (num1 < complete)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[2].Value = complete;
                        }
                        else
                        {
                            num1 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                            res = (num1 * num2) + 0.5F;
                            dataGridView1.Rows[e.RowIndex].Cells[4].Value = (int)res;

                            num1 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                            num3 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                            res = num1 - (num2 * num3);
                            dataGridView1.Rows[e.RowIndex].Cells[0].Value = res;

                            float profit = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                profit += float.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            }
                            label8.Text = profit.ToString();

                            float totalamount = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                totalamount += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            }
                            label11.Text = totalamount.ToString();

                            label7.Text = (float.Parse(label11.Text.ToString()) + float.Parse(lblpb.Text.ToString())).ToString();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            //if (textBox1.Text != string.Empty)
            //{
            //    if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value.ToString() != "0" || dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value.ToString() != "0")
            //    {
            //        SearchItems s = new SearchItems();
            //        s.ShowDialog();
            //        textBox1.Text = s.itemcode;
            //        if (dataGridView1.Rows.Count > 0)
            //        {
            //            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3];
            //            dataGridView1.BeginEdit(true);
            //        }
            //    }
            //}
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                button3_Click(sender, e);
            }
            //if (dataGridView1.Rows.Count < 1)
            //{
            if (e.KeyCode == Keys.Enter)
            {
                SearchItems s = new SearchItems();
                s.ShowDialog();
                textBox1.Text = s.itemcode;
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3];
                    dataGridView1.BeginEdit(true);
                }
            }
            //}
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3];
                dataGridView1.BeginEdit(true);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string cat = "";
            string subcat = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemCatName from ItemGrid where ItemCode=@e", con);
                cmd.Parameters.AddWithValue("@e", textBox1.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    cat = rd[0].ToString();
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
                SqlCommand cmd = new SqlCommand("select ItemSubCatName from ItemGrid where ItemCode=@e", con);
                cmd.Parameters.AddWithValue("@e", textBox1.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    subcat = rd[0].ToString();
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
                SqlCommand cmd = new SqlCommand("select * from Item where ItemCode=@e", con);
                cmd.Parameters.AddWithValue("@e", textBox1.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    String Des = cat+", "+subcat+", ";
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
                    dataGridView1.Rows.Add("0", Des, rd[8], "0", "0",rd[7],rd[5],rd[4]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != 10))
            {
                e.Handled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DateTime date, time;
        public Sales()
        {
            InitializeComponent();
            num1 = num2 = res = num3 = 0;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            complete = part = 0;
            date = dateTimePicker1.Value;
            time = dateTimePicker1.Value;
        }
        public void Load_Data()
        {
            int count = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Sales")
                {
                    count++;
                }
            }
            bool flag = false;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select SaleNo from Sale", con);
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
                    SqlCommand cmd = new SqlCommand("Select max(SaleNo) from Sale", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        int num = Convert.ToInt32(rd[0].ToString());
                        num += count;
                        txtsaleno.Text = num.ToString();
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
                int num = 10000000 + count;
                txtsaleno.Text = num.ToString();
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Defaults", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    txtcn.Text = rd[0].ToString();
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
                SqlCommand cmd = new SqlCommand("select CustomerUrduName,CustomerBalance from Customers where CustomerCode=@e", con);
                cmd.Parameters.AddWithValue("@e", txtcn.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uname.Text = dr[0].ToString();
                    if (dr[1].ToString() == string.Empty)
                    {
                        lblpb.Text = "0";
                    }
                    else
                    {
                        lblpb.Text = dr[1].ToString();
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
    }
}
