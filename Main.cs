using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phase2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connected connected = new Connected();
            this.Hide();
            connected.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Disconnected Dconnected = new Disconnected();
            this.Hide();
            Dconnected.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void roundButton1_Click(object sender, EventArgs e)
        {

        }

        /*rivate void button4_Click(object sender, EventArgs e)
        {
            ReportView  reportForm = new ReportView();    
            this.Hide();
            reportForm.ShowDialog();
            this.Show();




        }*/
    }
}
