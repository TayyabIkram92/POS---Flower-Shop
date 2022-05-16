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
    public partial class NewUser : Form
    {
        SqlConnection con = new SqlConnection();
        string c;
        public NewUser(string code)
        {
            InitializeComponent();
            c = code;
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
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
                bool flag = false;
                try
                {
                    con.Open();
                    SqlCommand cmnd = new SqlCommand("Select Username from Users where Username=@us", con);
                    cmnd.Parameters.AddWithValue("@us", textBox4.Text);
                    SqlDataReader rd = cmnd.ExecuteReader();
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
                    MessageBox.Show("Username Already Taken.\nTry Another Username.");
                }
                else
                {
                    int branchid=0;
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
                        SqlCommand cmd = new SqlCommand("Insert into Users (Username,Password,FullName,IsActive,BranchID) Values(@u,@p,@f,@i,@b)", con);
                        cmd.Parameters.AddWithValue("@u", textBox4.Text);
                        cmd.Parameters.AddWithValue("@p", textBox9.Text);
                        cmd.Parameters.AddWithValue("@f", textBox6.Text);
                        cmd.Parameters.AddWithValue("@i", checkBox3.CheckState);
                        cmd.Parameters.AddWithValue("@b", branchid);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("New User added Successfully.");
                        this.Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                }
            }
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            comboBox10.Items.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select BranchName from Branch", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox10.Items.Add(rd[0].ToString());
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
