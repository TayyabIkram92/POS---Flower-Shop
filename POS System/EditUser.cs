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
    public partial class EditUser : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public EditUser(string code)
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            c = code;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox6.Text == string.Empty || textBox4.Text == string.Empty || textBox9.Text == string.Empty || comboBox10.Text == string.Empty)
            {
                if (textBox6.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox6, "warning");
                }
                if (textBox4.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox4, "warning");
                }
                if (textBox9.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox9, "warning");
                }
                if (comboBox10.Text == string.Empty)
                {
                    errorProvider1.SetError(comboBox10, "warning");
                }
            }
            else
            {
                int id = 0;
                try
                {
                    con.Open();
                    SqlCommand cmnd = new SqlCommand("Select UserID from Users where Username=@us", con);
                    cmnd.Parameters.AddWithValue("@us", textBox4.Text);
                    SqlDataReader rd = cmnd.ExecuteReader();
                    if (rd.Read())
                    {
                        id = Convert.ToInt32(rd[0].ToString());
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                int branchid = 0;
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("Select BranchID from Branch where BranchName=@BN", con);
                    command.Parameters.AddWithValue("@BN", comboBox10.Text);
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        branchid = Convert.ToInt32(dr[0].ToString());
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
                    SqlCommand cmd = new SqlCommand("update Users set Username=@u,Password=@p,FullName=@f,IsActive=@i,BranchID=@b where UserID=@ui", con);
                    cmd.Parameters.AddWithValue("@u", textBox4.Text);
                    cmd.Parameters.AddWithValue("@p", textBox9.Text);
                    cmd.Parameters.AddWithValue("@f", textBox6.Text);
                    cmd.Parameters.AddWithValue("@i", checkBox3.CheckState);
                    cmd.Parameters.AddWithValue("@b", branchid);
                    cmd.Parameters.AddWithValue("@ui", id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User updated Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void EditUser_Load(object sender, EventArgs e)
        {

            comboBox10.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select BranchName from Branch", con);
                SqlDataReader rdx = cmd.ExecuteReader();
                while (rdx.Read())
                {
                    comboBox10.Items.Add(rdx[0].ToString());
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
                SqlCommand cmnd = new SqlCommand("Select * from Users where UserID=@BC", con);
                cmnd.Parameters.AddWithValue("@BC", c);
                SqlDataReader rd = cmnd.ExecuteReader();
                if (rd.Read())
                {
                    textBox6.Text = rd["FullName"].ToString();
                    textBox4.Text = rd["Username"].ToString();
                    textBox9.Text = rd["Password"].ToString();
                    string state = rd["IsActive"].ToString();
                    if (state == "True")
                    {
                        checkBox3.Checked = true;
                    }
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
