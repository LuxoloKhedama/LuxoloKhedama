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
    public partial class STUDENT_MANAGEMENT : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb";
        public STUDENT_MANAGEMENT()
        {
            InitializeComponent();
        }
        /*OleDbCommand cmd;
        OleDbConnection conn;*/
        public void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox11.Clear();
            textBox12.Clear();
        }
        private void STUDENT_MANAGEMENT_Load(object sender, EventArgs e)
        {

        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "INSERT INTO STUDENT_INFO (STU_NAME, STU_SURNAME, STU_ID, STU_DOB, STU_GENDER, STU_EMAIL, STU_ADDRESS, STU_CITY, STU_COUNTRY, STUENROLMENT_DATE, STU_GRADE, STU_SUBJECTS, STU_CODE) " +
                               "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@STU_NAME", textBox1.Text);
                    cmd.Parameters.AddWithValue("@STU_SURNAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@STU_ID", textBox3.Text);
                    cmd.Parameters.AddWithValue("@STU_DOB", textBox4.Text);
                    cmd.Parameters.AddWithValue("@STU_GENDER", textBox5.Text);
                    cmd.Parameters.AddWithValue("@STU_EMAIL", textBox6.Text);
                    cmd.Parameters.AddWithValue("@STU_ADDRESS", textBox7.Text);
                    cmd.Parameters.AddWithValue("@STU_CITY", textBox8.Text);
                    cmd.Parameters.AddWithValue("@STU_COUNTRY", textBox9.Text);
                    cmd.Parameters.AddWithValue("@STUENROLMENT_DATE", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@STU_GRADE", textBox11.Text);
                    cmd.Parameters.AddWithValue("@STU_SUBJECTS", textBox12.Text);
                    cmd.Parameters.AddWithValue("@STU_CODE", textBox10.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            Clear();
            MessageBox.Show("Student added successfully!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "UPDATE STUDENT_INFO SET " +
               "STU_NAME = ?, " +
               "STU_SURNAME = ?, " +
               "STU_DOB = ?, " +
               "STU_GENDER = ?, " +
               "STU_EMAIL = ?, " +
               "STU_ADDRESS = ?, " +
               "STU_CITY = ?, " +
               "STU_COUNTRY = ?, " +
               "STU_GRADE = ?, " +
               "STU_SUBJECT = ?, " +
               "STUENROLMENT_DATE = ? " +
               "STU_CODE = ?" +
               "WHERE STU_CODE = ?;";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@STU_NAME", textBox1.Text);
                    cmd.Parameters.AddWithValue("@STU_SURNAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@STU_ID", textBox3.Text);
                    cmd.Parameters.AddWithValue("@STU_DOB", textBox4.Text);
                    cmd.Parameters.AddWithValue("@STU_GENDER", textBox5.Text);
                    cmd.Parameters.AddWithValue("@STU_EMAIL", textBox6.Text);
                    cmd.Parameters.AddWithValue("@STU_ADDRESS", textBox7.Text);
                    cmd.Parameters.AddWithValue("@STU_CITY", textBox8.Text);
                    cmd.Parameters.AddWithValue("@STU_COUNTRY", textBox9.Text);
                    cmd.Parameters.AddWithValue("@STU_GRADE", textBox11.Text);
                    cmd.Parameters.AddWithValue("@STU_SUBJECTS", textBox12.Text);
                    cmd.Parameters.AddWithValue("@STUENROLMENT_DATE", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@STU_CODE", textBox10.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Student updated successfully!");
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM STUDENT_INFO";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind DataTable to DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "DELETE FROM STUDENT_INFO WHERE STU_CODE = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@STU_CODE", textBox1.Text);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student deleted successfully!");
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Student not found.");
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            BACKUP N = new BACKUP();
            N.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            STUDENT_MARKS M = new STUDENT_MARKS();
            M.Show();
        }
    }
}
