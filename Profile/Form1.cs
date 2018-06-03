using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Profile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK) {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            String id = textBoxID.Text;
            String name = textBoxName.Text;
            String descp = textBoxDescrip.Text;

            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms,pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            String insert_SQL = "INSERT INTO profile (id,name,description,image) VALUES (@id,@name,@description,@image)";
            MySqlConnection mySqlConnection = DataConnection.getDBConnection();

            mySqlConnection.Open();

            MySqlCommand command_insert = new MySqlCommand(insert_SQL, mySqlConnection);

            command_insert.CommandText = insert_SQL;
            command_insert.Parameters.AddWithValue("@id", id);
            command_insert.Parameters.AddWithValue("@name", name);
            command_insert.Parameters.AddWithValue("@description", descp);
            command_insert.Parameters.AddWithValue("@image", img);


            if (command_insert.ExecuteNonQuery() == 1) {

                MessageBox.Show("Data Inserted");

            }

            mySqlConnection.Close();
        }

        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            String select_SQL = "SELECT * FROM profile where id= '" + textBoxID.Text + "'";
            MySqlConnection mySqlConnection = DataConnection.getDBConnection();

            mySqlConnection.Open();

            MySqlCommand command_insert = new MySqlCommand(select_SQL, mySqlConnection);
            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command_insert);

            dataAdapter.Fill(dataTable);

            textBoxName.Text = dataTable.Rows[0][1].ToString();
            textBoxDescrip.Text = dataTable.Rows[0][1].ToString();

            byte[] image = (byte[])dataTable.Rows[0][3];
            MemoryStream memoryStream = new MemoryStream(image);

            pictureBox1.Image = Image.FromStream(memoryStream);

            dataAdapter.Dispose();

            mySqlConnection.Close();

        }
    }
}
