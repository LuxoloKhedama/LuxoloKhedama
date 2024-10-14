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
    public partial class TEACHER_MANAGEMENT : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb";

        public TEACHER_MANAGEMENT()
        {
            InitializeComponent();
        }
        /*OleDbCommand cmd;
        OleDbConnection conn;*/
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            BACKUP N = new BACKUP();
            N.Show();
        }
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

        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "INSERT INTO TEACHER_INFO (T_NAME, T_SURNAME, T_ID, T_GRADES, T_QUALIFICATION, T_EMAIL, T_CELLNUMBER, T_CLASSNO, T_EMPLOYMENTDATE, T_CODE) " +
                               "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@T_NAME", textBox1.Text);
                    cmd.Parameters.AddWithValue("@T_SURNAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@T_ID", textBox3.Text);
                    cmd.Parameters.AddWithValue("@T_GRADES", textBox4.Text);
                    cmd.Parameters.AddWithValue("@T_QUALIFICATION", textBox5.Text);
                    cmd.Parameters.AddWithValue("@T_EMAIL", textBox6.Text);
                    cmd.Parameters.AddWithValue("@T_CLASSNUMBER", textBox7.Text);
                    cmd.Parameters.AddWithValue("@STU_CLASSNO", textBox8.Text);
                    cmd.Parameters.AddWithValue("@T_EMPLOYMENTDATE", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@T_CODE", textBox9.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            Clear();
            MessageBox.Show("Teacher added successfully!");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "UPDATE TEACHER_INFO SET " +
               "T_NAME = ?, " +
               "T_SURNAME = ?, " +
               "T_ID = ?, " +
               "T_GRADES = ?, " +
               "T_QUALIFICATION = ?, " +
               "T_EMAIL = ?, " +
               "T_CLASSNUMBER = ?, " +
               "T_CLASSNO = ?, " +
               "T_EMPLOYMENTDATE = ?, " +
               "T_CODE = ?" +
               "WHERE T_CODE = ?;";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@T_NAME", textBox1.Text);
                    cmd.Parameters.AddWithValue("@T_SURNAME", textBox2.Text);
                    cmd.Parameters.AddWithValue("@T_ID", textBox3.Text);
                    cmd.Parameters.AddWithValue("@T_GRADES", textBox4.Text);
                    cmd.Parameters.AddWithValue("@T_QUALIFICATION", textBox5.Text);
                    cmd.Parameters.AddWithValue("@T_EMAIL", textBox6.Text);
                    cmd.Parameters.AddWithValue("@T_CLASSNUMBER", textBox7.Text);
                    cmd.Parameters.AddWithValue("@T_CLASSNO", textBox8.Text);
                    cmd.Parameters.AddWithValue("@T_EMPLOYMENTDATE", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@T_CODE", textBox9.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Teacher updated successfully!");
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "DELETE FROM TEACHER_INFO WHERE T_CODE = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@T_CODE", textBox1.Text);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Teacher deleted successfully!");
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Teacher not found.");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM TEACHER_INFO";
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

        private void TEACHER_MANAGEMENT_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
