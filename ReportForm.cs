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
    public partial class ReportForm : Form
    {

        CrystalReport1 CR;
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
         CR = new CrystalReport1();
            ReportView.ReportSource = CR;

        }

    }
}
