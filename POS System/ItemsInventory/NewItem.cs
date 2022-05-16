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
    public partial class NewItem : Form
    {
        string c,itemcatid,itemsubcatid,itemmodelid,uomid;
        float num1, num2, res;
        SqlConnection con = new SqlConnection();
        public NewItem(string code)
        {
            InitializeComponent();
            itemcatid = itemsubcatid = itemmodelid = uomid = "";
            c = code;
            num2 = num1 = res = 0;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }


        public void Load_Data()
        {
            if (c != "1")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select max(ItemCode) from Items", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        int num = Convert.ToInt32(rd[0].ToString());
                        num++;
                        txtcode.Text = num.ToString();
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
                txtcode.Text = "200001";
            }

            cbocategory.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ItemCatName from ItemCategory", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cbocategory.Items.Add(rd[0]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            cbouom.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select UOMName from UOM", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cbouom.Items.Add(rd[0]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void NewItem_Load(object sender, EventArgs e)
        {
            Load_Data();
            //InputLanguage i = InputLanguage.CurrentInputLanguage;
            //MessageBox.Show(i.Culture.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
            }
        }

        private void cbosubcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbomodel.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ItemModelName from ItemModelGrid where ItemSubCatName=@n", con);
                cmd.Parameters.AddWithValue("@n", cbosubcategory.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cbomodel.Items.Add(rd[0]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void txtsale1_TextChanged(object sender, EventArgs e)
        {
            if (txtsale1.Text != string.Empty)
            {
                num1 = float.Parse(txtpackqty.Text);
                num2 = float.Parse(txtsale1.Text);
                res = num2 / num1;
                txtsr1.Text = res.ToString();
            }
        }

        private void txtsale2_TextChanged(object sender, EventArgs e)
        {
            if (txtsale2.Text != string.Empty)
            {
                num1 = float.Parse(txtpackqty.Text);
                num2 = float.Parse(txtsale2.Text);
                res = num2 / num1;
                txtsr2.Text = res.ToString();
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

        private void txtsale2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtreorder_KeyPress(object sender, KeyPressEventArgs e)
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
            if(cbocategory.Text==string.Empty || cbosubcategory.Text==string.Empty || cbomodel.Text==string.Empty || txtcode.Text==string.Empty || txtEname.Text==string.Empty || txtUname.Text==string.Empty || txtpackqty.Text==string.Empty || txtcomplete.Text==string.Empty || txtpart.Text==string.Empty || txtpr.Text==string.Empty || txtsr1.Text==string.Empty || txtsr2.Text==string.Empty || cbouom.Text == string.Empty)
            {
                if (cbocategory.Text == string.Empty)
                {
                    errorProvider1.SetError(cbocategory, "warning");
                }
                if (cbosubcategory.Text == string.Empty)
                {
                    errorProvider1.SetError(cbosubcategory, "warning");
                }
                if (cbomodel.Text == string.Empty)
                {
                    errorProvider1.SetError(cbomodel, "warning");
                }
                if (txtcode.Text == string.Empty)
                {
                    errorProvider1.SetError(txtcode, "warning");
                }
                if (txtEname.Text == string.Empty)
                {
                    errorProvider1.SetError(txtEname, "warning");
                }
                if (txtUname.Text == string.Empty)
                {
                    errorProvider1.SetError(txtUname, "warning");
                }
                if (txtpackqty.Text == string.Empty)
                {
                    errorProvider1.SetError(txtpackqty, "warning");
                }
                if (txtcomplete.Text == string.Empty)
                {
                    errorProvider1.SetError(txtcomplete, "warning");
                }
                if (txtpart.Text == string.Empty)
                {
                    errorProvider1.SetError(txtpart, "warning");
                }
                if (txtpr.Text == string.Empty)
                {
                    errorProvider1.SetError(txtpr, "warning");
                }
                if (txtsr1.Text == string.Empty)
                {
                    errorProvider1.SetError(txtsr1, "warning");
                }
                if (txtsr2.Text == string.Empty)
                {
                    errorProvider1.SetError(txtsr2, "warning");
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
                //get model ID
                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Select ItemModelID from ItemModel where ItemModelName=@n", con);
                    cmd2.Parameters.AddWithValue("@n", cbomodel.Text);
                    SqlDataReader rd2 = cmd2.ExecuteReader();
                    if (rd2.Read())
                    {
                        itemmodelid = rd2[0].ToString();
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
                    Image img = pictureBox1.Image;
                    byte[] byteImg = ImageToByteArray(img);
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("Insert into Items(ItemCatID,ItemSubCatID,ItemModelID,ItemCode,ItemEnglishName,ItemUrduName,IncludeCategories,IsActive,ItemImage,PurchaseRate,SaleRate1,SaleRate2,CompletePackQty,CompleteNameUrdu,PartNameUrdu,UOMID,ReOrderLevel,IsTaxable,SystemNotes,ItemQty) values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p,@q,@r,@s,@t)", con);
                    cmd2.Parameters.AddWithValue("@a", itemcatid);
                    cmd2.Parameters.AddWithValue("@b", itemsubcatid);
                    cmd2.Parameters.AddWithValue("@c", itemmodelid);
                    cmd2.Parameters.AddWithValue("@d", txtcode.Text);
                    cmd2.Parameters.AddWithValue("@e", txtEname.Text);
                    cmd2.Parameters.AddWithValue("@f", txtUname.Text);
                    cmd2.Parameters.AddWithValue("@g", ckincludecategories.CheckState);
                    cmd2.Parameters.AddWithValue("@h", ckactive.CheckState);
                    cmd2.Parameters.AddWithValue("@i", byteImg);
                    cmd2.Parameters.AddWithValue("@j", txtpr.Text);
                    cmd2.Parameters.AddWithValue("@k", txtsr1.Text);
                    cmd2.Parameters.AddWithValue("@l", txtsr2.Text);
                    cmd2.Parameters.AddWithValue("@m", txtpackqty.Text);
                    cmd2.Parameters.AddWithValue("@n", txtcomplete.Text);
                    cmd2.Parameters.AddWithValue("@o", txtpart.Text);
                    cmd2.Parameters.AddWithValue("@p", uomid);
                    cmd2.Parameters.AddWithValue("@q", txtreorder.Text);
                    cmd2.Parameters.AddWithValue("@r", cktaxable.CheckState);
                    cmd2.Parameters.AddWithValue("@s", txtsys.Text);
                    cmd2.Parameters.AddWithValue("@t", 0);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("New Item Added Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void txtUname_Enter(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("ur-PK");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }

        private void txtUname_Leave(object sender, EventArgs e)
        {
            CultureInfo en = new CultureInfo("en-GB");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(en);
        }

        private void txtcomplete_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcomplete_Enter(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("ur-PK");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }

        private void txtcomplete_Leave(object sender, EventArgs e)
        {

            CultureInfo ur = new CultureInfo("en-GB");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }

        private void txtpart_Enter(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("ur-PK");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }

        private void txtpart_Leave(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("en-GB");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }

        public byte[] ImageToByteArray(Image img)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtpackqty_TextChanged(object sender, EventArgs e)
        {
            //txtpurchaserate.Text = "";
            //txtsale1.Text = "";
            //txtsale2.Text = "";
            //txtpr.Text = "";
            //txtsr1.Text = "";
            //txtsr2.Text = "";
            if (txtpackqty.Text == string.Empty)
            {
                txtpurchaserate.ReadOnly = true;
                txtsale1.ReadOnly = true;
                txtsale2.ReadOnly = true;
            }
            else
            {
                txtpurchaserate.ReadOnly = false;
                txtsale1.ReadOnly = false;
                txtsale2.ReadOnly = false;
            }
        }

        private void txtpurchaserate_TextChanged(object sender, EventArgs e)
        {
            if (txtpurchaserate.Text != string.Empty)
            {
                num1 = float.Parse(txtpackqty.Text);
                num2 = float.Parse(txtpurchaserate.Text);
                res = num2 / num1;
                txtpr.Text = res.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.iconfinder_Shop_379396;
        }

        private void cbocategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbosubcategory.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ItemSubCatName from ItemSubCatGrid where ItemCatName=@n", con);
                cmd.Parameters.AddWithValue("@n",cbocategory.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cbosubcategory.Items.Add(rd[0]);
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
