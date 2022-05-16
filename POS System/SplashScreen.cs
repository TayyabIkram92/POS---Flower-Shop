using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Net;

namespace POS_System
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Width += 6;
                if (pictureBox1.Width >= 470)
                {
                    timer1.Stop();
                    Login frm = new Login();
                    frm.Show();
                    this.Hide();
                    //string id = ID();
                    //if (id == "(Murshad-PC)---(00:25:D3:DC:45:E0)")
                    //{
                    //    Login frm = new Login();
                    //    frm.Show();
                    //    this.Hide();
                    //}
                    //else if(id== "(Murshad-PC)---()")
                    //{
                    //    Login frm = new Login();
                    //    frm.Show();
                    //    this.Hide();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Contact Developer\n03058480350\n");
                    //}
                }
            }catch(Exception)
            {
                return;
            }
        }

        private string ID()
        {
            //ManagementObjectCollection mbsList = null;
            //ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            //mbsList = mbs.Get();
            //string id = "";
            //foreach (ManagementObject mo in mbsList)
            //{
            //    id = mo["ProcessorID"].ToString();
            //}
            //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            //ManagementObjectCollection moc = mos.Get();
            //string motherBoard = "";
            //foreach (ManagementObject mo in moc)
            //{
            //    motherBoard = (string)mo["SerialNumber"];
            //}
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            string MACAddress = String.Empty;

            foreach (ManagementObject mo in moc)
            {
                if (MACAddress == String.Empty)
                { // only return MAC Address from first card
                    if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }
            string HostName = Dns.GetHostName().ToString();
            string myUniqueID = "("+HostName+")---("+MACAddress+")";
            return myUniqueID;
        }
    }
}
