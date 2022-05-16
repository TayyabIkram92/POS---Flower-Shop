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
    public partial class EditSupplier : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public EditSupplier(string code)
        {
            InitializeComponent();
            c = code;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        public void Load_Data()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Suppliers where SupplierCode=@c", con);
                cmd.Parameters.AddWithValue("@c", c);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtcode.Text = rd[1].ToString();
                    txtename.Text = rd[2].ToString();
                    txtuname.Text = rd[3].ToString();
                    txtaddress.Text = rd[4].ToString();
                    txtp1.Text = rd[5].ToString();
                    txtp2.Text = rd[6].ToString();
                    cboarea.Text = rd[7].ToString();
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
                cmd.Parameters.AddWithValue("@a", cboarea.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cboarea.Text = cboarea.Text.ToString() + "-" + dr[0].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void Load_Area()
        {
            try
            {
                cboarea.Items.Clear();
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
        private void EditSupplier_Load(object sender, EventArgs e)
        {
            Load_Data();
            Load_Area();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtename.Text == string.Empty || txtaddress.Text == string.Empty || cboarea.Text == string.Empty)
            {
                if (txtename.Text == string.Empty)
                {
                    errorProvider1.SetError(txtename, "warning");
                }
                if (txtaddress.Text == string.Empty)
                {
                    errorProvider1.SetError(txtaddress, "warning");
                }
                if (cboarea.Text == string.Empty)
                {
                    errorProvider1.SetError(cboarea, "warning");
                }
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Suppliers set SupplierEnglishName=@b,SupplierUrduName=@c,SupplierAddress=@d,SupplierPhone1=@e,SupplierPhone2=@f,SupplierArea=@g where SupplierCode=@a", con);
                    cmd.Parameters.AddWithValue("@a", txtcode.Text);
                    cmd.Parameters.AddWithValue("@b", txtename.Text);
                    cmd.Parameters.AddWithValue("@c", txtuname.Text);
                    cmd.Parameters.AddWithValue("@d", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@e", txtp1.Text);
                    cmd.Parameters.AddWithValue("@f", txtp2.Text);
                    float Area_ = float.Parse(cboarea.Text.Substring(0, cboarea.Text.IndexOf("-")).ToString());
                    cmd.Parameters.AddWithValue("@g", Area_);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Supplier updated Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void txtuname_Enter(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("ur-PK");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }

        private void txtuname_Leave(object sender, EventArgs e)
        {
            CultureInfo ur = new CultureInfo("en-GB");
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(ur);
        }
    }
}
