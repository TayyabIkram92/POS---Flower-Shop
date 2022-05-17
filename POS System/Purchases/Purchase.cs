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
    public partial class Purchase : Form
    {
        float complete, part;
        SqlConnection con = new SqlConnection();
        float num1, num2, num3, res;

        private void button2_Click(object sender, EventArgs e)
        {
            SearchSupplier c = new SearchSupplier();
            c.ShowDialog();
            txtcn.Text = c.value;
        }

        private void txtcn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select SupplierUrduName,SupplierBalance from Suppliers where SupplierCode=@e", con);
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
            if (dataGridView1.Rows.Count < 0 || textBox1.Text == string.Empty || textBox2.Text == string.Empty)
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
                        if (textBox3.Text == string.Empty)
                        {
                            textBox3.Text = "Credit Purchase";
                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into Purchase(PurchaseNo,SupplierCode,PurchaseTime,PurchaseDate,PurchaseAmount,PreviousAmount,TotalAmount,ReceivedAmount,RemainingAmount,PurchaseDescription,PurchaaseRemarks,SystemNotes) values(@a,@b,@c,@d,@f,@g,@h,@i,@j,@k,@l,@m)", con);
                        cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                        cmd.Parameters.AddWithValue("@b", txtcn.Text);
                        cmd.Parameters.AddWithValue("@c", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
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
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Purchaserate = num1 / num2;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into saledataset values (@a,@b,@c,@d,@e)", con);
                            cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[i].Cells[10].Value);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[12].Value);
                            cmd.Parameters.AddWithValue("@d", Purchaserate.ToString("0.0"));
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[8].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                        try
                        {
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Salerate1 = num1 / num2;
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Salerate2 = num1 / num2;
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Purchaserate = num1 / num2;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Update Items set SaleRate1=@a,SaleRate2=@b,PurchaseRate=@c,ItemQty+=@d,CompletePackQty=@f where ItemCode=@e", con);
                            cmd.Parameters.AddWithValue("@a", Salerate1);
                            cmd.Parameters.AddWithValue("@b", Salerate2);
                            cmd.Parameters.AddWithValue("@c", Purchaserate);
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[0].Value);
                            cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[7].Value);
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
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into PurchaseDetail(PurchaseNo,ItemDescription,Rate1,Rate2,Qty,NetAmount,PurchaseRate,ItemUrduName,ItemPartUrdu,PackQty) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)", con);
                            cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[1].Value);
                            cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[2].Value);
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[3].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[8].Value);
                            cmd.Parameters.AddWithValue("@g", dataGridView1.Rows[i].Cells[4].Value);
                            cmd.Parameters.AddWithValue("@h", dataGridView1.Rows[i].Cells[10].Value);
                            cmd.Parameters.AddWithValue("@i", dataGridView1.Rows[i].Cells[12].Value);
                            cmd.Parameters.AddWithValue("@j", dataGridView1.Rows[i].Cells[7].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Suppliers set SupplierBalance=@a where SupplierCode=@c", con);
                    cmd.Parameters.AddWithValue("@a", label6.Text);
                    cmd.Parameters.AddWithValue("@c", txtcn.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Purchase Info Saved Successfully");
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
                if (button3.Text == "Save and Print")
                {
                    try
                    {
                        if (textBox3.Text == string.Empty)
                        {
                            textBox3.Text = "Credit Purchase";
                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into Purchase(PurchaseNo,SupplierCode,PurchaseTime,PurchaseDate,PurchaseAmount,PreviousAmount,TotalAmount,ReceivedAmount,RemainingAmount,PurchaseDescription,PurchaaseRemarks,SystemNotes) values(@a,@b,@c,@d,@f,@g,@h,@i,@j,@k,@l,@m)", con);
                        cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                        cmd.Parameters.AddWithValue("@b", txtcn.Text);
                        cmd.Parameters.AddWithValue("@c", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
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
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Purchaserate = num1 / num2;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into saledataset values (@a,@b,@c,@d,@e)", con);
                            cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[i].Cells[10].Value);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[12].Value);
                            cmd.Parameters.AddWithValue("@d", Purchaserate.ToString("0.0"));
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[8].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                        try
                        {
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Salerate1 = num1 / num2;
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Salerate2 = num1 / num2;
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Purchaserate = num1 / num2;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Update Items set SaleRate1=@a,SaleRate2=@b,PurchaseRate=@c,ItemQty+=@d,CompletePackQty=@f where ItemCode=@e", con);
                            cmd.Parameters.AddWithValue("@a", Salerate1);
                            cmd.Parameters.AddWithValue("@b", Salerate2);
                            cmd.Parameters.AddWithValue("@c", Purchaserate);
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[0].Value);
                            cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[7].Value);
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
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into PurchaseDetail(PurchaseNo,ItemDescription,Rate1,Rate2,Qty,NetAmount,PurchaseRate,ItemUrduName,ItemPartUrdu,PackQty) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)", con);
                            cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[1].Value);
                            cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[2].Value);
                            cmd.Parameters.AddWithValue("@d", dataGridView1.Rows[i].Cells[3].Value);
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@f", dataGridView1.Rows[i].Cells[8].Value);
                            cmd.Parameters.AddWithValue("@g", dataGridView1.Rows[i].Cells[4].Value);
                            cmd.Parameters.AddWithValue("@h", dataGridView1.Rows[i].Cells[10].Value);
                            cmd.Parameters.AddWithValue("@i", dataGridView1.Rows[i].Cells[12].Value);
                            cmd.Parameters.AddWithValue("@j", dataGridView1.Rows[i].Cells[7].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update Suppliers set SupplierBalance=@a where SupplierCode=@c", con);
                        cmd.Parameters.AddWithValue("@a", label6.Text);
                        cmd.Parameters.AddWithValue("@c", txtcn.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Purchase Info Saved Successfully");
                        //SaleReportViewer s = new SaleReportViewer();
                        CrystalReports.CrystalReport1 rpt = new CrystalReports.CrystalReport1();
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
                        saleno.Text = txtsaleno.Text + " : ";
                        TextObject date = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text6"];
                        date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        TextObject time = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text7"];
                        time.Text = " " + DateTime.Now.ToString("hh:mm tt");
                        TextObject customercode = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text8"];
                        customercode.Text = txtcn.Text;
                        TextObject customername = (TextObject)rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text12"];
                        customername.Text = uname.Text;
                        TextObject mojoda = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text16"];
                        mojoda.Text = label11.Text;
                        TextObject sabka = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text15"];
                        sabka.Text = lblpb.Text;
                        TextObject totalraqm = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text18"];
                        totalraqm.Text = label7.Text;
                        TextObject wasool = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text20"];
                        wasool.Text = textBox2.Text;
                        TextObject bakayaraqm = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text22"];
                        bakayaraqm.Text = label6.Text;
                        rpt.PrintOptions.PrinterName = "BlackCopper 80mm Series";
                        rpt.PrintToPrinter(1, false, 0, 0);
                        //s.crystalReportViewer1.ReportSource = rpt;
                        button5_Click(sender, e);
                        //s.Show();
                        //button5_Click(sender, e);
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
                            num1 = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            num2 = float.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            float Purchaserate = num1 / num2;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into saledataset values (@a,@b,@c,@d,@e)", con);
                            cmd.Parameters.AddWithValue("@a", dataGridView1.Rows[i].Cells[10].Value);
                            cmd.Parameters.AddWithValue("@b", dataGridView1.Rows[i].Cells[6].Value);
                            cmd.Parameters.AddWithValue("@c", dataGridView1.Rows[i].Cells[12].Value);
                            cmd.Parameters.AddWithValue("@d", Purchaserate.ToString("0.0"));
                            cmd.Parameters.AddWithValue("@e", dataGridView1.Rows[i].Cells[8].Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }//SaleReportViewer s = new SaleReportViewer();
                    CrystalReports.CrystalReport1 rpt = new CrystalReports.CrystalReport1();
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
                    saleno.Text = txtsaleno.Text + " : ";
                    TextObject date = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text6"];
                    date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    TextObject time = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text7"];
                    time.Text = " " + DateTime.Now.ToString("hh:mm tt");
                    TextObject customercode = (TextObject)rpt.ReportDefinition.Sections["Section1"].ReportObjects["Text8"];
                    customercode.Text = txtcn.Text;
                    TextObject customername = (TextObject)rpt.ReportDefinition.Sections["Section2"].ReportObjects["Text12"];
                    customername.Text = uname.Text;
                    TextObject mojoda = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text16"];
                    mojoda.Text = label11.Text;
                    TextObject sabka = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text15"];
                    sabka.Text = lblpb.Text;
                    TextObject totalraqm = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text18"];
                    totalraqm.Text = label7.Text;
                    TextObject wasool = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text20"];
                    wasool.Text = textBox2.Text;
                    TextObject bakayaraqm = (TextObject)rpt.ReportDefinition.Sections["Section4"].ReportObjects["Text22"];
                    bakayaraqm.Text = label6.Text;
                    rpt.PrintOptions.PrinterName = "BlackCopper 80mm Series";
                    rpt.PrintToPrinter(1, false, 0, 0);
                    //s.crystalReportViewer1.ReportSource = rpt;
                    button5_Click(sender, e);
                    //s.Show();
                    //button5_Click(sender, e);
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Select();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
                    {
                        num1 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                        num2 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                        num3 = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                        res = num1 / num3;
                        res = res * num2;
                        res = res + 0.5F;
                        dataGridView1.Rows[e.RowIndex].Cells[8].Value = (int)res;

                        float totalamount = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                        {
                            totalamount += float.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                        }
                        label11.Text = totalamount.ToString();

                        label7.Text = (float.Parse(label11.Text.ToString()) + float.Parse(lblpb.Text.ToString())).ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchItems s = new SearchItems();
                s.ShowDialog();
                textBox1.Text = s.itemcode;
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[6];
                    dataGridView1.BeginEdit(true);
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[6];
                dataGridView1.BeginEdit(true);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemEnglishName,SaleRate1*CompletePackQty as Rate1,SaleRate2*CompletePackQty as Rate2,PurchaseRate*CompletePackQty as PRate,PurchaseRate,ItemUrduName,CompleteNameUrdu,PartNameUrdu,CompletePackQty from Items where ItemCode=@e", con);
                cmd.Parameters.AddWithValue("@e", textBox1.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dataGridView1.Rows.Add(textBox1.Text, dr[0], dr[1], dr[2], dr[3], "0", "0", dr[8], "0", dr[4], dr[5], dr[6], dr[7]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PurchaseReceipts s = new PurchaseReceipts();
            s.ShowDialog();
            txtsaleno.Text = s.value;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Purchase where PurchaseNo=@a", con);
                cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtcn.Text = dr[2].ToString();
                    label11.Text = dr[3].ToString();
                    lblpb.Text = dr[4].ToString();
                    label7.Text = dr[5].ToString();
                    textBox2.Text = dr[6].ToString();
                    label6.Text = dr[7].ToString();
                    textBox3.Text = dr[8].ToString();
                    textBox4.Text = dr[9].ToString();
                    textBox5.Text = dr[10].ToString();
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
                SqlCommand cmd = new SqlCommand("select * from PurchaseDetail where PurchaseNo=@a", con);
                cmd.Parameters.AddWithValue("@a", txtsaleno.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(0,dr[1],dr[2],dr[3],dr[6],0,dr[4],dr[9],dr[5],0,dr[7],0,dr[8]);
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

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Purchase()
        {
            InitializeComponent();
            num1 = num2 = res = num3 = 0;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            complete = part = 0;
        }

        public void Load_Data()
        {
            bool flag = false;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select PurchaseNo from Purchase", con);
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
                    SqlCommand cmd = new SqlCommand("Select max(PurchaseNo) from Purchase", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        int num = Convert.ToInt32(rd[0].ToString());
                        num++;
                        txtsaleno.Text = num.ToString();
                    }
                    con.Close();
                }
                catch (Exception)
                {
                    //MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                txtsaleno.Text = "10000001";
            }

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Defaults", con);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtcn.Text = rd[1].ToString();
                }
                con.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show(exp.ToString());
            }
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select SupplierUrduName,SupplierBalance from Suppliers where SupplierCode=@e", con);
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
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            Load_Data();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "  dd/MM/yyyy";
            textBox1.Select();
        }
    }
}
