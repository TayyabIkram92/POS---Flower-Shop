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
    public partial class MainWindow : Form
    {
        SqlConnection con = new SqlConnection();
        public MainWindow()
        {
            InitializeComponent();
            Connection c = new Connection();
            con.ConnectionString = c.connections();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }
        public MainWindow(string s)
        {
            InitializeComponent();
            label1.Text = s;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
        }

        private void defaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach(Form f in Application.OpenForms)
            {
                if (f.Text == "Defaults")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Defaults d = new Defaults();
                d.MdiParent = this;
                d.WindowState = FormWindowState.Maximized;
                d.Dock = DockStyle.Fill;
                d.Show();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }

        private void branchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Branches")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Branches b = new Branches();
                b.MdiParent = this;
                b.WindowState = FormWindowState.Maximized;
                b.Dock = DockStyle.Fill;
                b.Show();
            }
        }

        private void configurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Configuration Settings")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                DynamicallyConnectionString d = new DynamicallyConnectionString();
                d.MdiParent = this;
                d.WindowState = FormWindowState.Maximized;
                d.Dock = DockStyle.Fill;
                d.Show();
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string date=DateTime.Now.ToString();
            date=date.Replace('/','_');
            date = date.Replace(' ','_');
            date = date.Replace(':', '_');
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    SqlCommand cmd = new SqlCommand("BACKUP DATABASE Shop TO DISK = @d ", con);
                    cmd.Parameters.AddWithValue("@d", f.SelectedPath + "\\Shop_" + date + ".bak");
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Backup Successful");
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Backup Files(*.bak)|*.bak";
            if (f.ShowDialog() == DialogResult.OK)
            {
                try {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("RESTORE DATABASE Shop FROM DISK = @d ", con);
                    cmd.Parameters.AddWithValue("@d", f.FileName);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Restore Successful");
                }
                catch(Exception exp)
                {
                    MessageBox.Show("Restore Failed.\nContact Developer.\n\n"+exp.ToString());
                }
            }
        }

        private void clearDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You don't have Rights to Delete Database.");
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Users")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Users u = new Users();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void userRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You don't have Rights to use that functionality.");
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ChangePassword")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                ChangePassword u = new ChangePassword(label1.Text);
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Already Registered.");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Close();
        }

        private void itemCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ItemCategory")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                ItemCategory u = new ItemCategory();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void itemSubCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ItemSubCategory")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                ItemSubCategory u = new ItemSubCategory();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void itemModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //For Tiles POS
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ItemAttributes")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                ItemAttributes u = new ItemAttributes();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
            //For Karyana POS
            /*bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ItemModel")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                ItemModel u = new ItemModel();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }*/
        }

        private void unitOfMeasuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "UOM")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                UOM u = new UOM();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void stockTransferNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You don't have Rights to use that functionality.");
        }

        private void stockAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You don't have Rights to use that functionality.");
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Item")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Item u = new Item();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Customers")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Customers u = new Customers();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            //foreach (Form f in Application.OpenForms)
            //{
            //    if (f.Text == "Sale")
            //    {
            //        IsOpen = true;
            //        f.Focus();
            //        break;
            //    }
            //}
            if (IsOpen == false)
            {
                Sales u = new Sales();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }
        private void cashPaymentVouncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "CashPaymentVouncher")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                CashPaymentVouncher u = new CashPaymentVouncher();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void cashReceiptVouncherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "CashReceiptVouncher")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                CashReceiptVouncher u = new CashReceiptVouncher();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Suppliers")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Suppliers u = new Suppliers();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void areaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Areas")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Areas u = new Areas();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }
        
        private void purchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        //    bool IsOpen = false;
        //    foreach (Form f in Application.OpenForms)
        //    {
        //        if (f.Text == "Purchase")
        //        {
        //            IsOpen = true;
        //            f.Focus();
        //            break;
        //        }
        //    }
        //    if (IsOpen == false)
        //    {
        //        Purchase u = new Purchase();
        //        u.MdiParent = this;
        //        u.WindowState = FormWindowState.Maximized;
        //        u.Dock = DockStyle.Fill;
        //        u.Show();
        //    }
        }

        private void grossProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "GrossProfit")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                GrossProfit u = new GrossProfit();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void byCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "ProfitByCustomer")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                ProfitByCustomer u = new ProfitByCustomer();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void customerLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "CustomerLedger")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                CustomerLedger u = new CustomerLedger();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void supplierLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "SupplierLedger")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                SupplierLedger u = new SupplierLedger();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void accountReceivableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "AccountReceivable")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                AccountReceivable u = new AccountReceivable();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void accountPayableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "AccountPayable")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                AccountPayable u = new AccountPayable();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleReportViewer s = new SaleReportViewer();
            CrystalReports.CrystalReport6 rpt = new CrystalReports.CrystalReport6();
            SqlDataAdapter sda = new SqlDataAdapter("select * from ItemsInfo", con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "ItemsInfo");

            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No data Found", "CrystalReportWithOracle");
                return;
            }
            rpt.SetDataSource(ds);
            s.crystalReportViewer1.ReportSource = rpt;
            s.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleReportViewer s = new SaleReportViewer();
            CrystalReports.CrystalReport7 rpt = new CrystalReports.CrystalReport7();
            SqlDataAdapter sda = new SqlDataAdapter("Select dbo.Customers.CustomerID, dbo.Customers.CustomerCode, dbo.Customers.CustomerEnglishName, dbo.Customers.CustomerUrduName, dbo.Customers.CustomerAddress, dbo.Customers.CustomerPhone1, dbo.Customers.CustomerPhone2, dbo.Areas.AreaName as CustomerArea, dbo.Customers.CreditLimit, dbo.Customers.Discount, dbo.Customers.SaleRate, dbo.Customers.CustomerBalance, dbo.Customers.AccountOpeningDate from dbo.Customers INNER JOIN dbo.Areas ON dbo.Customers.CustomerArea = dbo.Areas.AreaCode", con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Customers");

            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No data Found", "CrystalReportWithOracle");
                return;
            }
            rpt.SetDataSource(ds);
            s.crystalReportViewer1.ReportSource = rpt;
            s.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleReportViewer s = new SaleReportViewer();
            CrystalReports.CrystalReport8 rpt = new CrystalReports.CrystalReport8();
            SqlDataAdapter sda = new SqlDataAdapter("Select dbo.Suppliers.SupplierID, dbo.Suppliers.SupplierCode, dbo.Suppliers.SupplierEnglishName, dbo.Suppliers.SupplierUrduName, dbo.Suppliers.SupplierAddress, dbo.Suppliers.SupplierPhone1, dbo.Suppliers.SupplierPhone2, dbo.Areas.AreaName as SupplierArea, dbo.Suppliers.SupplierBalance, dbo.Suppliers.AccountOpeningDate from dbo.Suppliers INNER JOIN dbo.Areas ON dbo.Suppliers.SupplierArea = dbo.Areas.AreaCode", con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Suppliers");

            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No data Found", "CrystalReportWithOracle");
                return;
            }
            rpt.SetDataSource(ds);
            s.crystalReportViewer1.ReportSource = rpt;
            s.Show();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Pasword")
                {
                    IsOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (IsOpen == false)
            {
                Pasword u = new Pasword();
                u.ShowDialog();
            }
        }

        private void preSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsOpen = false;
            //foreach (Form f in Application.OpenForms)
            //{
            //    if (f.Text == "Sale")
            //    {
            //        IsOpen = true;
            //        f.Focus();
            //        break;
            //    }
            //}
            if (IsOpen == false)
            {
                PreSale u = new PreSale();
                u.MdiParent = this;
                u.WindowState = FormWindowState.Maximized;
                u.Dock = DockStyle.Fill;
                u.Show();
            }
        }
    }
}
