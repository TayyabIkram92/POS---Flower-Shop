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

namespace POS_System
{
    public partial class Defaults : Form
    {
        SqlConnection con = new SqlConnection();
        public Defaults()
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "25/06/2020 12:30 pm";
        }

        public void Load_Customer_Data()
        {
            comboBox18.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select CustomerEnglishName from Customers", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox18.Items.Add(rd[0]);
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
                SqlCommand cmd = new SqlCommand("Select * from Defaults", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox18.Text = rd[0].ToString();
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
                SqlCommand cmd = new SqlCommand("Select CustomerEnglishName from Customers where CustomerCode=@c", con);
                cmd.Parameters.AddWithValue("@c",comboBox18.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox18.Text = rd[0].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        public void Load_Supplier_Data()
        {
            comboBox17.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select SupplierEnglishName from Suppliers", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox17.Items.Add(rd[0]);
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
                SqlCommand cmd = new SqlCommand("Select * from Defaults", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox17.Text = rd[1].ToString();
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
                SqlCommand cmd = new SqlCommand("Select SupplierEnglishName from Suppliers where SupplierCode=@c", con);
                cmd.Parameters.AddWithValue("@c", comboBox17.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox17.Text = rd[0].ToString();
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void Defaults_Load(object sender, EventArgs e)
        {
            Load_Customer_Data();
            Load_Supplier_Data();
        }
        private void Save_Default_Customer()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select CustomerCode from Customers where CustomerEnglishName=@c", con);
                cmd.Parameters.AddWithValue("@c", comboBox18.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox18.Text = rd[0].ToString();
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
                SqlCommand cmd = new SqlCommand("update Defaults SET DefaultCustomerCode=@d", con);
                cmd.Parameters.AddWithValue("@d", comboBox18.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Default Customer Updated Successfully.");
                Load_Customer_Data();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void Save_Default_Supplier()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select SupplierCode from Suppliers where SupplierEnglishName=@c", con);
                cmd.Parameters.AddWithValue("@c", comboBox17.Text);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox17.Text = rd[0].ToString();
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
                SqlCommand cmd = new SqlCommand("update Defaults SET DefaultSupplierCode=@d", con);
                cmd.Parameters.AddWithValue("@d", comboBox17.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Default Supplier Updated Successfully.");
                Load_Supplier_Data();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox18.Text == string.Empty)
            {
                errorProvider1.SetError(comboBox18, "warning");
            }
            if (comboBox17.Text == string.Empty)
            {
                errorProvider1.SetError(comboBox17, "warning");
            }
            else
            {
                Save_Default_Customer();
                Save_Default_Supplier();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
