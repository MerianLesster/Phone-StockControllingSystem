using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_ABC_Phone_Company
{
    public partial class StockControllerMenu : Form
    {
        public StockControllerMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            DehiwalaBranch fm = new DehiwalaBranch();
            fm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            GampahaBranch fm = new GampahaBranch();
            fm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login fm = new Login();
            fm.Show();
        }
    }
}
