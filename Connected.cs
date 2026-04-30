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
    public partial class Connected : Form
    {
        OracleConnection conn;
        string ordb = "Data source=orcl; User Id=hr; Password=hr;";

        // 1. ADDED THIS LIST: It will store car IDs in the background
        List<int> retrievedCarIds = new List<int>();

        public Connected()
        {
            InitializeComponent();
        }

        private void Connected_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label21_Click(object sender, EventArgs e) { }

        private void button5_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from cars where brand=:n";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("n", textBox1.Text);

            try
            {
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("This car brand was not found in our database.");
                    return;
                }
                DataView dv = new DataView(dt);
                dv.RowFilter = "STATUS = 'Available'";

                if (dv.Count > 0)
                {
                    dataGridView1.DataSource = dv;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("Car is already Rented.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 2. Clear lists to prevent mixing old and new searches
            retrievedCarIds.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            // 3. Added 'cars.car_id' to the beginning of the SELECT statement
            cmd.CommandText = "select cars.car_id, Model, Car_type, country, city, year from cars, locations where brand=:n and cars.current_location_id=locations.location_id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("n", textBox1.Text);

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // 4. Store the car_id in our background list
                retrievedCarIds.Add(Convert.ToInt32(dr[0]));

                // Add the rest to the comboboxes (shifted +1 because of car_id)
                comboBox1.Items.Add(dr[1]); // Model
                comboBox2.Items.Add(dr[2]); // Car_type
                comboBox4.Items.Add(dr[3]); // Country
                comboBox3.Items.Add(dr[4]); // City
                comboBox5.Items.Add(dr[5]); // Year
            }
            dr.Close();

            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("No cars found matching this brand.");
            }
        }

        // 5. Fixed the nested method syntax error here
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a car model from the drop down list first.");
                return;
            }

            int selectedCarId = retrievedCarIds[comboBox1.SelectedIndex];

            try
            {
                OracleCommand cmd = new OracleCommand("Create_User_And_Booking", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // User Parameters
                cmd.Parameters.Add("p_user_id", int.Parse(textBox5.Text));
                cmd.Parameters.Add("p_fname", textBox6.Text);
                cmd.Parameters.Add("p_lname", textBox7.Text);
                cmd.Parameters.Add("p_email", textBox8.Text);

                // --- THIS IS THE FIX ---
                // Convert the text from the textboxes into numbers before sending
                cmd.Parameters.Add("p_ssn", int.Parse(textBox13.Text));
                cmd.Parameters.Add("p_mobile", int.Parse(textBox14.Text));
                // -------------------------

                cmd.Parameters.Add("p_state", textBox9.Text);
                cmd.Parameters.Add("p_country", textBox15.Text);

                // Booking Parameters
                cmd.Parameters.Add("p_car_id", selectedCarId);
                cmd.Parameters.Add("p_pickup_date", Convert.ToDateTime(textBox10.Text));
                cmd.Parameters.Add("p_return_date", Convert.ToDateTime(textBox11.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Booking created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("A critical error occurred: " + ex.Message);
            }
        }
    }
}