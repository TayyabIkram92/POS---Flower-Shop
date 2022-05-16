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
    public partial class NewBranch : Form
    {
        SqlConnection con = new SqlConnection();
        public NewBranch()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }
        public byte[] ImageToByteArray(Image img)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || comboBox10.Text == string.Empty)
            {
                if (textBox2.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox2, "warning");
                }
                if (textBox3.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox3, "warning");
                }
                if (textBox1.Text == string.Empty)
                {
                    errorProvider1.SetError(textBox1, "warning");
                }
                if (comboBox10.Text == string.Empty)
                {
                    errorProvider1.SetError(comboBox10, "warning");
                }
            }
            else
            {
                bool flag = false;
                Image img = pictureBox1.Image;
                byte[] byteImg = ImageToByteArray(img);
                try
                {
                    con.Open();
                    SqlCommand cmnd = new SqlCommand("Select BranchCode from Branch where BranchCode=@BC", con);
                    cmnd.Parameters.AddWithValue("@BC", textBox2.Text);
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
                    MessageBox.Show("Branch Code Already Taken.\nTry Another Branch Code.");
                }
                else
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into Branch (BranchCode,BranchName,Address,Phone1,Phone2,Email,Website,NTNNo,GSTNo,Logo,IsMainBranch) Values(@BranchCode,@BranchName,@Address,@Phone1,@Phone2,@Email,@Website,@NTNNo,@GSTNo,@Logo,@IsMainBranch)", con);
                        cmd.Parameters.AddWithValue("@BranchCode", textBox2.Text);
                        cmd.Parameters.AddWithValue("@BranchName", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Address", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Phone1", textBox6.Text);
                        cmd.Parameters.AddWithValue("@Phone2", textBox7.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Website", textBox5.Text);
                        cmd.Parameters.AddWithValue("@NTNNo", textBox9.Text);
                        cmd.Parameters.AddWithValue("@GSTNo", textBox8.Text);
                        cmd.Parameters.AddWithValue("@Logo", byteImg);
                        cmd.Parameters.AddWithValue("@IsMainBranch", checkBox3.CheckState);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("New Branch added Successfully.");
                        this.Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.iconfinder_Shop_379396;
        }

        private void NewBranch_Load(object sender, EventArgs e)
        {

        }
    }
}
