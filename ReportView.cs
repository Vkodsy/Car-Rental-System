using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace Phase2
{
    public partial class ReportView : Form
    {
        CrystalReport1 CR;
        public ReportView()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            CR = new CrystalReport1();
            crystalReportViewer1.ReportSource = CR;

            foreach (ParameterDiscreteValue pf in CR.ParameterFields[0].DefaultValues)
            {
               comboBox1.Items.Add(pf.Value);
            }
            foreach (ParameterDiscreteValue pf in CR.ParameterFields[1].DefaultValues)
            {
                comboBox2.Items.Add(pf.Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CR.SetParameterValue(0, comboBox1.Text.ToString());
            CR.SetParameterValue(1, comboBox2.Text.ToString());
            crystalReportViewer1.ReportSource = CR;
        }
    }
}
