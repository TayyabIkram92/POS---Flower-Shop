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
    public partial class EditBranch : Form
    {
        string c;
        SqlConnection con = new SqlConnection();
        public EditBranch(string code)
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            c = code;
        }

        public byte[] ImageToByteArray(Image img)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
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
                Image img = pictureBox1.Image;
                byte[] byteImg = ImageToByteArray(img);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Branch set BranchName=@BranchName,Address=@Address,Phone1=@Phone1,Phone2=@Phone2,Email=@Email,Website=@Website,NTNNo=@NTNNo,GSTNo=@GSTNo,Logo=@Logo,IsMainBranch=@IsMainBranch where BranchCode=@BranchCode", con);
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
                    MessageBox.Show("Branch updated Successfully.");
                    this.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void EditBranch_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmnd = new SqlCommand("Select * from Branch where BranchCode=@BC", con);
                cmnd.Parameters.AddWithValue("@BC", c);
                SqlDataReader rd = cmnd.ExecuteReader();
                if (rd.Read())
                {
                    textBox2.Text = rd["BranchCode"].ToString();
                    textBox3.Text = rd["BranchName"].ToString();
                    textBox1.Text = rd["Address"].ToString();
                    textBox6.Text = rd["Phone1"].ToString();
                    textBox7.Text = rd["Phone2"].ToString();
                    textBox4.Text = rd["Email"].ToString();
                    textBox5.Text = rd["Website"].ToString();
                    textBox9.Text = rd["NTNNo"].ToString();
                    textBox8.Text = rd["GSTNo"].ToString();
                    System.IO.MemoryStream ms = new System.IO.MemoryStream((byte[])rd["Logo"]);
                    pictureBox1.Image = new Bitmap(ms);
                    string state = rd["IsMainBranch"].ToString();
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
