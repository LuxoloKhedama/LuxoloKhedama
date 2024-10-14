using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace STUDENT_DMS
{
    public partial class LOGIN : Form
    {

        public LOGIN()
        {
            InitializeComponent();

        }
        OleDbCommand cmd;
        OleDbConnection conn;
        private int attempts = 0;
        private const int maxAttempts = 3;
        public void CLear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void LOGIN_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb");
                cmd = new OleDbCommand();
                cmd.Connection = conn;
            
        }
        public void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                string username = textBox1.Text;
                string password = textBox2.Text;
                string pin = textBox3.Text;

                // Assume ValidateUser is a method to check the database
                if (string.IsNullOrEmpty(pin))
                {
                    MessageBox.Show("PLEASE ENTER YOUR PIN TO LOGIN.");
                    return; // Exit the method if the PIN is empty
                }
                if (ValidateUser(username, password, pin))
                {
                    MessageBox.Show("Login successful!");
                    Clear();
                    this.Hide();
                    BACKUP B = new BACKUP();
                    B.Show();
                    // Proceed to the next form or action
                }
                else
                {
                    attempts++;
                    MessageBox.Show("Invalid login details. Please try again." + -attempts);

                    if (attempts >= maxAttempts)
                    {
                        MessageBox.Show("Maximum attempts reached. The login button will be disabled.");
                        button1.Enabled = false; // Disable the login button
                    }
                    Clear();
                }
                /*string us = textBox1.Text;
                string pw = textBox2.Text;
                string pin = textBox3.Text;
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM LOGIN_ADMIN WHERE A_USERNAME = '" + us + "'and A_PASSWORD = '" + pw + "'"; /*, A_PIN = '" + pin + "'";
                cmd.ExecuteNonQuery();
                OleDbDataReader DR = cmd.ExecuteReader();
                Clear();
                MessageBox.Show("WELCOME ADMIN");*/
            }
            catch (Exception x)
            {
                MessageBox.Show("ERROR : " + x.Message); 
            }
        }
        private bool ValidateUser( string username, string password, string pin)
        {
            // Replace with the path to your Access database
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM LOGIN_ADMIN WHERE A_USERNAME = @A_USERNAME AND A_PASSWORD = @A_PASSWORD AND A_PIN = @A_PIN ";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@S_USERNAME", username);
                        cmd.Parameters.AddWithValue("@S_PASSWORD", password);
                        cmd.Parameters.AddWithValue("@S_PIN", pin);

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0; // Return true if user exists
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                    return false; // Return false if there's an error
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            

        }

        
    }
}
