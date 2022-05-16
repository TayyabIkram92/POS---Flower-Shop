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
    public partial class EditItems : Form
    {
        string c, itemcatid, itemsubcatid, itemmodelid, uomid;
        float num1, num2, res;

        private void txtpurchaserate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtsale1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtpackqty_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtpurchaserate.Text == string.Empty || txtsale1.Text == string.Empty || txtpackqty.Text == string.Empty || cbocategory.Text == string.Empty || cbosubcategory.Text == string.Empty || txtcode.Text == string.Empty || txtEname.Text == string.Empty || cbouom.Text == string.Empty)
            {
                if (txtpurchaserate.Text == string.Empty)
                {
                    errorProvider1.SetError(txtpurchaserate, "warning");
                }
                if (txtsale1.Text == string.Empty)
                {
                    errorProvider1.SetError(txtsale1, "warning");
                }
                if (cbocategory.Text == string.Empty)
                {
                    errorProvider1.SetError(cbocategory, "warning");
                }
                if (cbosubcategory.Text == string.Empty)
                {
                    errorProvider1.SetError(cbosubcategory, "warning");
                }
                if (txtcode.Text == string.Empty)
                {
                    errorProvider1.SetError(txtcode, "warning");
                }
                if (txtEname.Text == string.Empty)
                {
                    errorProvider1.SetError(txtEname, "warning");
                }
                if (txtpackqty.Text == string.Empty)
                {
                    errorProvider1.SetError(txtpackqty, "warning");
                }
                if (cbouom.Text == string.Empty)
                {
                    errorProvider1.SetError(cbouom, "warning");
                }
            }
            else
            {
                //get category ID
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Select ItemCatID from ItemCategory where ItemCatName=@n", con);
                    cmd2.Parameters.AddWithValue("@n", cbocategory.Text);
                    SqlDataReader rd2 = cmd2.ExecuteReader();
                    if (rd2.Read())
                    {
                        itemcatid = rd2[0].ToString();
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                //get sub category ID
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Select ItemSubCatID from ItemSubCategory where ItemSubCatName=@n", con);
                    cmd2.Parameters.AddWithValue("@n", cbosubcategory.Text);
                    SqlDataReader rd2 = cmd2.ExecuteReader();
                    if (rd2.Read())
                    {
                        itemsubcatid = rd2[0].ToString();
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                //get UOM ID
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Select UOMID from UOM where UOMName=@n", con);
                    cmd2.Parameters.AddWithValue("@n", cbouom.Text);
                    SqlDataReader rd2 = cmd2.ExecuteReader();
                    if (rd2.Read())
                    {
                        uomid = rd2[0].ToString();
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                //save data
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Update Item set ItemCatID=@a,ItemSubCatID=@b,ItemAttributesID=@c,ItemName=@e,IsActive=@f,PurchaseRate=@g,SaleRate=@h,UOMID=@i,ItemQty=@j,Attr5=@k,Attr6=@l,Attr7=@m,Attr8=@n,Attr9=@o,Attr10=@p,Reorder=@q,ProductCode=@r where ItemCode=@d", con);
                    cmd2.Parameters.AddWithValue("@a", itemcatid);
                    cmd2.Parameters.AddWithValue("@b", itemsubcatid);
                    cmd2.Parameters.AddWithValue("@c", 0);
                    cmd2.Parameters.AddWithValue("@d", txtcode.Text);
                    cmd2.Parameters.AddWithValue("@e", txtEname.Text);
                    cmd2.Parameters.AddWithValue("@f", ckactive.CheckState);
                    cmd2.Parameters.AddWithValue("@g", txtpurchaserate.Text);
                    cmd2.Parameters.AddWithValue("@h", txtsale1.Text);
                    cmd2.Parameters.AddWithValue("@i", uomid);
                    cmd2.Parameters.AddWithValue("@j", txtpackqty.Text);
                    cmd2.Parameters.AddWithValue("@k", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@l", textBox2.Text);
                    cmd2.Parameters.AddWithValue("@m", textBox3.Text);
                    cmd2.Parameters.AddWithValue("@n", textBox4.Text);
                    cmd2.Parameters.AddWithValue("@o", textBox5.Text);
                    cmd2.Parameters.AddWithValue("@p", textBox6.Text);
                    cmd2.Parameters.AddWithValue("@q", textBox7.Text);
                    cmd2.Parameters.AddWithValue("@r", textBox8.Text);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item Updated Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SqlConnection con = new SqlConnection();
        public EditItems(string code)
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            c = code;
            itemcatid = itemsubcatid = itemmodelid = uomid = "";
            num2 = num1 = res = 0;
        }

        public void Load_Data()
        {
            try
            {
                con.Open();
                SqlCommand cmnd = new SqlCommand("Select * from Item where ItemCode=@BC", con);
                cmnd.Parameters.AddWithValue("@BC", c);
                SqlDataReader rd = cmnd.ExecuteReader();
                if (rd.Read())
                {
                    textBox7.Text = rd[17].ToString();
                    textBox8.Text = rd[18].ToString();
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
                SqlCommand cmnd = new SqlCommand("Select * from ItemGrid where ItemCode=@BC", con);
                cmnd.Parameters.AddWithValue("@BC", c);
                SqlDataReader rd = cmnd.ExecuteReader();
                if (rd.Read())
                {
                    cbocategory.Items.Add(rd[5].ToString());
                    cbosubcategory.Items.Add(rd[6].ToString());
                    txtcode.Text = rd[3].ToString();
                    txtEname.Text = rd[4].ToString();
                    cbocategory.Text = rd[5].ToString();
                    cbosubcategory.Text = rd[6].ToString();
                    txtpurchaserate.Text = rd[9].ToString();
                    txtsale1.Text = rd[10].ToString();
                    cbouom.Items.Add(rd[11].ToString());
                    uomid = rd[11].ToString();
                    txtpackqty.Text = rd[12].ToString();
                    string state2 = rd[8].ToString();
                    if (state2 == "True")
                    {
                        ckactive.Checked = true;
                    }
                    textBox1.Text = rd[13].ToString();
                    textBox2.Text = rd[14].ToString();
                    textBox3.Text = rd[15].ToString();
                    textBox4.Text = rd[16].ToString();
                    textBox5.Text = rd[17].ToString();
                    textBox6.Text = rd[18].ToString();
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
                SqlCommand cmd = new SqlCommand("Select Attr5,Attr6,Attr7,Attr8,Attr9,Attr10 from ItemAttributesGrid where ItemCatName=@n and ItemSubCatName=@o", con);
                cmd.Parameters.AddWithValue("@n", cbocategory.Text);
                cmd.Parameters.AddWithValue("@o", cbosubcategory.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    if (rd[0] != "")
                    {
                        label1.Text = rd[0].ToString();
                        label1.Visible = true;
                        textBox1.Visible = true;
                    }
                    if (rd[1] != "")
                    {
                        label6.Text = rd[1].ToString();
                        label6.Visible = true;
                        textBox2.Visible = true;
                    }
                    if (rd[2] != "")
                    {
                        label8.Text = rd[2].ToString();
                        label8.Visible = true;
                        textBox3.Visible = true;
                    }
                    if (rd[3] != "")
                    {
                        label9.Text = rd[3].ToString();
                        label9.Visible = true;
                        textBox4.Visible = true;
                    }
                    if (rd[4] != "")
                    {
                        label12.Text = rd[4].ToString();
                        label12.Visible = true;
                        textBox5.Visible = true;
                    }
                    if (rd[5] != "")
                    {
                        label13.Text = rd[5].ToString();
                        label13.Visible = true;
                        textBox6.Visible = true;
                    }
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
                SqlCommand cmd = new SqlCommand("Select UOMName from UOM where UOMID=@u", con);
                cmd.Parameters.AddWithValue("@u", uomid);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cbouom.Text = rd[0].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void EditItems_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
    }
}
