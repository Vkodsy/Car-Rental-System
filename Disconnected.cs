using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Phase2
{
    public partial class Disconnected : Form
    {
        string conn = "Data source=orcl; User Id=hr; Password=hr;";
        string cmd = "";
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;
        public Disconnected()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string col = comboBox1.Text == "ALL" ? "*" : comboBox1.Text;
            string cmd = textBox1.Text == ""
                ? $"SELECT {col} FROM USERS ORDER BY USER_ID"
                : $"SELECT {col} FROM USERS WHERE USER_ID = :id ORDER BY USER_ID";
            
            adapter = new OracleDataAdapter(cmd, conn);

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                adapter.SelectCommand.Parameters.Add("id", textBox1.Text);
            }

            ds = new DataSet();
            adapter.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else {
                MessageBox.Show("User doesn't exist!");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Disconnected_Load(object sender, EventArgs e)
        {

            cmd = @"SELECT COLUMN_NAME 
        FROM USER_TAB_COLUMNS 
        WHERE TABLE_NAME = 'USERS'
        ORDER BY COLUMN_ID";

            OracleDataAdapter adapter = new OracleDataAdapter(cmd, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            DataRow row = dt.NewRow();
            row["COLUMN_NAME"] = "ALL";
            dt.Rows.InsertAt(row, 0);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "COLUMN_NAME";
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }
    }
}
