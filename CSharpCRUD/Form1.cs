using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace CSharpCRUD
{
    public partial class Form1 : Form
    {

        // Connectionstring that will connect mysql and .net
        private string connectionString = "Server=127.0.0.1;Database=seminar;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This will handle the loading of the form
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">Event Handler</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            RetrieveContactData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        void RetrieveContactData()
        {
            // 1. Create the connection object
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    // 1.a Open the connection
                    dbConnection.Open();

                    // 2. Build the command query
                    string query = "SELECT * FROM contact";

                    // Create sql command object
                    MySqlCommand sqlCmd = new MySqlCommand(query, dbConnection);

                    // Create a reader for the raw data
                    MySqlDataReader reader = sqlCmd.ExecuteReader();

                    // Clear all the data from the listview first
                    // before loading the data from the database
                    listView1.Items.Clear();

                    while (reader.Read())
                    {
                        // put the read data on the listview
                        listView1.Items.Add(reader["id"].ToString());

                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(reader["firstName"].ToString());
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(reader["lastName"].ToString());
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(reader["middleName"].ToString());
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(reader["bday"].ToString());
                    }

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }


                // 4. Display the error
            }
        }

        void DeleteContactData()
        {
            // 1. Create the connection object
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    // 1.a Open the connection
                    dbConnection.Open();

                    // 2. Build the command query
                    string query = "DELETE FROM contact WHERE id = @id";

                    // Create sql command object
                    MySqlCommand sqlCmd = new MySqlCommand(query, dbConnection);

                    sqlCmd.Parameters.AddWithValue("id", listView1.SelectedItems[0].Text);

                    // Execute the Insert Command
                    sqlCmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }


                // 4. Display the error
            }

            RetrieveContactData();
        }

        void UpdateContactDate()
        {
            // 1. Create the connection object
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    // 1.a Open the connection
                    dbConnection.Open();

                    // 2. Build the command query
                    string query = "UPDATE contact SET firstName = @firstName, lastName = @lastName, middleName = @middleName, bday = @bday WHERE id = @id";


                    // Create sql command object
                    MySqlCommand sqlCmd = new MySqlCommand(query, dbConnection);

                    sqlCmd.Parameters.AddWithValue("firstName", txtFirstName.Text);
                    sqlCmd.Parameters.AddWithValue("lastName", txtLastName.Text);
                    sqlCmd.Parameters.AddWithValue("middleName", txtMiddleName.Text);
                    sqlCmd.Parameters.AddWithValue("bday", txtBday.Text);
                    sqlCmd.Parameters.AddWithValue("id", listView1.SelectedItems[0].Text);

                    // Execute the Insert Command
                    sqlCmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }


                // 4. Display the error
            }

            RetrieveContactData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Create the connection object
            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    // 1.a Open the connection
                    dbConnection.Open();

                    // 2. Build the command query
                    string query = "INSERT INTO contact(firstName, lastName, middleName, bday) VALUES(@firstName, @lastName, @middleName, @bday)";

                    // Create sql command object
                    MySqlCommand sqlCmd = new MySqlCommand(query, dbConnection);

                    sqlCmd.Parameters.AddWithValue("firstName", txtFirstName.Text);
                    sqlCmd.Parameters.AddWithValue("lastName", txtLastName.Text);
                    sqlCmd.Parameters.AddWithValue("middleName", txtMiddleName.Text);
                    sqlCmd.Parameters.AddWithValue("bday", txtBday.Text);

                    // Execute the Insert Command
                    sqlCmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }


                // 4. Display the error
            }

            RetrieveContactData();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            DeleteContactData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteContactData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateContactDate();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            // first name
            txtFirstName.Text = listView1.SelectedItems[0].SubItems[1].Text;

            // last name
            txtLastName.Text = listView1.SelectedItems[0].SubItems[2].Text;

            // middle name
            txtMiddleName.Text = listView1.SelectedItems[0].SubItems[3].Text;

            // bday
            txtBday.Text = listView1.SelectedItems[0].SubItems[4].Text; 
        }
    }
}
