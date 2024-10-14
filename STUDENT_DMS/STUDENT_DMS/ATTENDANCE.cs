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
    public partial class ATTENDANCE : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb";

        public ATTENDANCE()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            BACKUP T = new BACKUP();
            T.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM ATTENDANCE_STUDENTS";
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

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            STUDENT_MANAGEMENT N = new STUDENT_MANAGEMENT();
            N.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM ATTENDANCE_TEACHER";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind DataTable to DataGridView
                    dataGridView2.DataSource = dataTable;
                }
            }
        }
        private void LoadTableData()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM ATTENDANCE_TEACHER"; // Adjust to your table name
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Bind the data to the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadTableData2()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM ATTENDANCE_STUDENTS"; // Adjust to your table name
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Bind the data to the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Assuming STU_CODE is in the first column; adjust as needed
                string stuCode = dataGridView2.SelectedRows[0].Cells["ATT_NAME"].Value.ToString();

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "DELETE FROM ATTENDANCE_TEACHER WHERE ATT_NAME = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ATT_NAME", stuCode);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Teacher deleted successfully!");

                            // Refresh the DataGridView to reflect changes
                            LoadTableData();
                        }
                        else
                        {
                            MessageBox.Show("Teacher not found not found.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*using (OleDbConnection conn = new OleDbConnection(connectionString))
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
        }*/

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
        {
            // Get the selected row
            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

            // Get the ID from the third column (assumed to be STU_CODE)
            string newStuCode = selectedRow.Cells[2].Value.ToString(); // Adjust the index based on your DataGridView structure
            string oldStuCode = selectedRow.Cells["ATT_ID_NUMBER"].Value.ToString(); // Assuming the original ID is stored here

            // Update the rest of the student information (if needed)
            string newName = selectedRow.Cells["ATT_NAME"].Value.ToString();
            string newSurname = selectedRow.Cells["ATT_SURNAME"].Value.ToString();
            // Get other fields as needed...

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "UPDATE ATTENDANCE_TEACHER SET " +
                    "ATT_NAME = ?, " +
                    "ATT_SURNAME = ?, " +
                    "ATT_ID_NUMBER = ? " +
                    "WHERE ATT_ID_NUMBER = ?;";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ATT_NAME", newName);
                    cmd.Parameters.AddWithValue("@ATT_SURNAME", newSurname);
                    cmd.Parameters.AddWithValue("@ATT_ID_NUMBER", newStuCode); // New ID
                    cmd.Parameters.AddWithValue("@ATT_ID_NUMBER", oldStuCode); // Old ID for WHERE clause

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Teacher updated successfully!");
            LoadTableData(); // Refresh the DataGridView to show updated data
        }
        else
        {
            MessageBox.Show("Please select a row to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming STU_CODE is in the first column; adjust as needed
                string stuCode = dataGridView1.SelectedRows[0].Cells["AT_NAME"].Value.ToString();

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "DELETE FROM ATTENDANCE_STUDENTS WHERE AT_NAME = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AT_NAME", stuCode);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student deleted successfully!");

                            // Refresh the DataGridView to reflect changes
                            LoadTableData();
                        }
                        else
                        {
                            MessageBox.Show("Student not found not found.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Get the ID from the third column (assumed to be STU_CODE)
                string newStuCode = selectedRow.Cells[2].Value.ToString(); // Adjust the index based on your DataGridView structure
                string oldStuCode = selectedRow.Cells["AT_ID_NUMBER"].Value.ToString(); // Assuming the original ID is stored here

                // Update the rest of the student information (if needed)
                string newName = selectedRow.Cells["AT_NAME"].Value.ToString();
                string newSurname = selectedRow.Cells["AT_SURNAME"].Value.ToString();
                // Get other fields as needed...

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "UPDATE ATTENDANCE_STUDENTS SET " +
                        "AT_NAME = ?, " +
                        "AT_SURNAME = ?, " +
                        "AT_ID_NUMBER = ? " +
                        "WHERE AT_ID_NUMBER = ?;";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AT_NAME", newName);
                        cmd.Parameters.AddWithValue("@AT_SURNAME", newSurname);
                        cmd.Parameters.AddWithValue("@AT_ID_NUMBER", newStuCode); // New ID
                        cmd.Parameters.AddWithValue("@AT_ID_NUMBER", oldStuCode); // Old ID for WHERE clause

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Student updated successfully!");
                LoadTableData2(); // Refresh the DataGridView to show updated data
            }
            else
            {
                MessageBox.Show("Please select a row to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string searchValue = textBox2.Text;
            SearchTeachers(searchValue);
        }
        private void SearchTeachers(string searchValue)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ATTENDANCE_TEACHER WHERE ATT_NAME LIKE ?";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", "%" + searchValue + "%");
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable teacherData = new DataTable();
                    adapter.Fill(teacherData);
                    dataGridView2.DataSource = teacherData;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;
            SearchStudents(searchValue);
        }
        private void SearchStudents(string searchValue)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ATTENDANCE_STUDENTS WHERE AT_NAME LIKE ?";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", "%" + searchValue + "%");
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataTable teacherData = new DataTable();
                    adapter.Fill(teacherData);
                    dataGridView1.DataSource = teacherData;
                }
            }
        }
        }
    }
