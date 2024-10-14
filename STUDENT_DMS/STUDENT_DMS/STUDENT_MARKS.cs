using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO.Compression;
using System.IO;


namespace STUDENT_DMS
{
    public partial class STUDENT_MARKS : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb";// Adjust this path
        public STUDENT_MARKS()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new[] { "GRADE8", "GRADE9", "GRADE10", "GRADE11", "GRADE12" });
        }
        /*OleDbCommand cmd;
        OleDbConnection conn;*/
        private void STUDENT_MARKS_Load(object sender, EventArgs e)
        {
            /*comboBox1.Items.Add("Grade 8");
            comboBox1.Items.Add("Grade 9");
            comboBox1.Items.Add("Grade 10");
            comboBox1.Items.Add("Grade 11");
            comboBox1.Items.Add("Grade 12");*/
        }
        /*private void GenerateReport(string inputFilePath, string outputFilePath)
        {
            /*if (radioButton5.Checked)
            {
            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + inputFilePath))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM GRADE8 ", conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                int totalRows = dataTable.Rows.Count;
                progressBar1.Maximum = totalRows; // Set the maximum value of the progress bar

                using (StreamWriter wr = new StreamWriter(outputFilePath))
                {
                    int TotalRows = dataTable.Rows.Count;
                    for (int i = 0; i < totalRows; i++)
                    {
                        var itemArrayAsStrings = dataTable.Rows[i].ItemArray.Select(item => item.ToString()).ToArray();
                        wr.WriteLine(string.Join(",", itemArrayAsStrings));
                        progressBar1.Value = i + 1;
                    }
                }
            }
            MessageBox.Show("Report has been successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            STUDENT_MANAGEMENT B = new STUDENT_MANAGEMENT();
            B.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string inputFilePath = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb"; // Adjust this path
            string outputFilePath = @"C:/Users/L1G1/Documents/Visual Studio 2008/Projects/output.csv"; // Adjust this path

            string selectedGrade = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
            string tableName;
            try
            {
                if (selectedGrade != null &&
                    (selectedGrade == "GRADE8" || selectedGrade == "G9" || selectedGrade == "G10" || selectedGrade == "G11" || selectedGrade == "G12"))
                {
                    using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb" + inputFilePath))
                    {
                        switch (selectedGrade)
                        {
                            case "GRADE8":
                                tableName = "GRADE8";
                                MessageBox.Show("Table Grade 8");
                                break;
                            case "G9":
                                tableName = "G9";
                                MessageBox.Show("Table Grade 9");

                                break;
                            case "G10":
                                tableName = "G10";
                                MessageBox.Show("Table Grade 10");

                                break;
                            case "G11":
                                tableName = "G11";
                                MessageBox.Show("Table Grade 11");

                                break;
                            case "G12":
                                tableName = "G12";
                                MessageBox.Show("Table Grade 12");

                                break;
                            default:
                                MessageBox.Show("Please select a valid grade.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                        }
                        conn.Open();
                        tableName = "GRADE8";
                        //string query = "SELECT * FROM GRADE8 OR GRADE9 OR GRADE10 OR GRADE11 OR GRADE12 = '" + comboBox1 + "'"; // Use the determined table name
                        string query = "SELECT * FROM "+tableName+" ";
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        int totalRows = dataTable.Rows.Count;
                        progressBar1.Maximum = totalRows; // Set the maximum value of the progress bar

                        using (StreamWriter wr = new StreamWriter(outputFilePath))
                        {
                            for (int i = 0; i < totalRows; i++)
                            {
                                var itemArrayAsStrings = dataTable.Rows[i].ItemArray.Select(item => item.ToString()).ToArray();
                                wr.WriteLine(string.Join(",", itemArrayAsStrings));
                                progressBar1.Value = i + 1;
                            }
                        }
                    }

                    MessageBox.Show("Report has been successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Please select a valid grade.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception x)
            {
                MessageBox.Show("error :" + x.Message);
            }
        }
        private void SearchInTable(string tableName, string searchTerm)
        {
        using (OleDbConnection conn = new OleDbConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM "+tableName+" WHERE G8_NAME = '"+textBox1.Text+"'"; 
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; // Bind the data to the DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string selectedTable = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
            string searchTerm = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(selectedTable) && !string.IsNullOrEmpty(searchTerm))
            {
                SearchInTable(selectedTable, searchTerm);
            }
            else
            {
                MessageBox.Show("Please select a table and enter a search term.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string accessPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs"; // Adjust based on your Office version
            string databasePath = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\L1G1\Documents\STUDENT_DMS.accdb"; // Adjust this path

            try
            {
                System.Diagnostics.Process.Start(accessPath, databasePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening database: " + ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadTableData(string tableName)
        {
             using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
            try
            {
                conn.Open();
                string query = "SELECT * FROM "+tableName+""; // Use square brackets for table names
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
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if an item is selected in the ComboBox
            string selectedTable = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;

             if (!string.IsNullOrEmpty(selectedTable))
             {
                LoadTableData(selectedTable);
             }
             else
             {
                MessageBox.Show("Please select a table from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

