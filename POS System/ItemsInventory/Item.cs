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
    public partial class Item : Form
    {
        SqlConnection con = new SqlConnection();
        string code;
        public Item()
        {
            InitializeComponent();
            Connection cn = new Connection();
            con.ConnectionString = cn.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            code = "";
        }
        public void Load_Data()
        {
            dataGridView1.Rows.Clear();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from ItemGrid", con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    String Des = "";
                    if (rd[13] != "")
                    {
                        Des += rd[13].ToString();
                    }
                    if (rd[14] != "")
                    {
                        Des += ", "+rd[14].ToString();
                    }
                    if (rd[15] != "")
                    {
                        Des += ", " + rd[15].ToString();
                    }
                    if (rd[16] != "")
                    {
                        Des += ", " + rd[16].ToString();
                    }
                    if (rd[17] != "")
                    {
                        Des += ", " + rd[17].ToString();
                    }
                    if (rd[18] != "")
                    {
                        Des += ", " + rd[18].ToString();
                    }
                    dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], Des,rd[5],rd[6],rd[7],rd[9],rd[10],rd[12]);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add("All");
            comboBox1.Text = "All";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemCatName from ItemCategory", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            label3.Text = dataGridView1.Rows.Count.ToString();
            float worth = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()) >= 0)
                {
                    worth = worth + (float.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString()) * float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()));
                }
            }
            label4.Text = worth.ToString();
        }

        private void Item_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == string.Empty)
            {
                Load_Data();
            }
            else
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from ItemGrid where ItemName like '%" + textBox6.Text + "%' OR ItemCatName like '%" + textBox6.Text + "%' OR ItemSubCatName like '%" + textBox6.Text + "%' OR ItemCode like '%" + textBox6.Text + "%'", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        String Des = "";
                        if (rd[13] != "")
                        {
                            Des += rd[13].ToString();
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
                        if (rd[17] != "")
                        {
                            Des += ", " + rd[17].ToString();
                        }
                        if (rd[18] != "")
                        {
                            Des += ", " + rd[18].ToString();
                        }
                        dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], Des, rd[5], rd[6], rd[7], rd[9], rd[10], rd[12]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Data to Delete.");
            }
            else
            {
                code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
                if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (code != "")
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Delete from Item where ItemCode= @BC", con);
                            cmd.Parameters.AddWithValue("@BC", code);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.ToString());
                        }
                    }
                    Load_Data();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                code = "1";
                NewItems n = new NewItems(code);
                n.ShowDialog();
            }
            else
            {
                code = "0";
                NewItems n = new NewItems(code);
                n.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Selected to Edit.");
            }
            else
            {
                code = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
                EditItems E = new EditItems(code);
                E.ShowDialog();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("All");
            comboBox2.Text = "All";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select ItemSubCatName from ItemSubCatGrid where ItemCatName=@a", con);
                cmd.Parameters.AddWithValue("@a", comboBox1.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr[0].ToString());
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            if (comboBox1.Text == "All")
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from ItemGrid", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        String Des = "";
                        if (rd[13] != "")
                        {
                            Des += rd[13].ToString();
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
                        if (rd[17] != "")
                        {
                            Des += ", " + rd[17].ToString();
                        }
                        if (rd[18] != "")
                        {
                            Des += ", " + rd[18].ToString();
                        }
                        dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], Des, rd[5], rd[6], rd[7], rd[9], rd[10], rd[12]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                label3.Text = dataGridView1.Rows.Count.ToString();
                float worth = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()) >= 0)
                    {
                        worth = worth + (float.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString()) * float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()));
                    }
                }
                label4.Text = worth.ToString();
            }
            else
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from ItemGrid where ItemCatName='"+comboBox1.Text+"'", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        String Des = "";
                        if (rd[13] != "")
                        {
                            Des += rd[13].ToString();
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
                        if (rd[17] != "")
                        {
                            Des += ", " + rd[17].ToString();
                        }
                        if (rd[18] != "")
                        {
                            Des += ", " + rd[18].ToString();
                        }
                        dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], Des, rd[5], rd[6], rd[7], rd[9], rd[10], rd[12]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                label3.Text = dataGridView1.Rows.Count.ToString();
                float worth = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()) >= 0)
                    {
                        worth = worth + (float.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString()) * float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()));
                    }
                }
                label4.Text = worth.ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All")
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from ItemGrid", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        String Des = "";
                        if (rd[13] != "")
                        {
                            Des += rd[13].ToString();
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
                        if (rd[17] != "")
                        {
                            Des += ", " + rd[17].ToString();
                        }
                        if (rd[18] != "")
                        {
                            Des += ", " + rd[18].ToString();
                        }
                        dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], Des, rd[5], rd[6], rd[7], rd[9], rd[10], rd[12]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                label3.Text = dataGridView1.Rows.Count.ToString();
                float worth = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()) >= 0)
                    {
                        worth = worth + (float.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString()) * float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()));
                    }
                }
                label4.Text = worth.ToString();
            }
            else
            {
                dataGridView1.Rows.Clear();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from ItemGrid where ItemCatName='" + comboBox1.Text + "' and ItemSubCatName='"+comboBox2.Text+"'", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        String Des = "";
                        if (rd[13] != "")
                        {
                            Des += rd[13].ToString();
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
                        if (rd[17] != "")
                        {
                            Des += ", " + rd[17].ToString();
                        }
                        if (rd[18] != "")
                        {
                            Des += ", " + rd[18].ToString();
                        }
                        dataGridView1.Rows.Add(rd[0], rd[1], rd[2], rd[3], rd[4], Des, rd[5], rd[6], rd[7], rd[9], rd[10], rd[12]);
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
                label3.Text = dataGridView1.Rows.Count.ToString();
                float worth = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()) >= 0)
                    {
                        worth = worth + (float.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString()) * float.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString()));
                    }
                }
                label4.Text = worth.ToString();
            }
        }
    }
}
