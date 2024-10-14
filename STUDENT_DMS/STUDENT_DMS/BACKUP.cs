using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Data.OleDb;

namespace STUDENT_DMS
{
    public partial class BACKUP : Form
    {
        public BACKUP()
        {
            InitializeComponent();
        }
        /*OleDbCommand cmd;
        OleDbConnection conn;*/
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LOGIN L = new LOGIN();
            L.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ATTENDANCE A = new ATTENDANCE();
            A.Show();
        }
        private void BACKUP_Load_1(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Access");
            comboBox1.Items.Add("Excel");
            comboBox1.Items.Add("Text File");
        }
        private void UpdateProgress(int currentIndex, int totalCount)
        {
            // For example, you can log the progress to the console or update a UI element
            MessageBox.Show("Progress: " + currentIndex + 1 + "of " + totalCount);
            // If using a UI, you might want to invoke a method to update a progress bar or label
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string inputFilePath = textBox3.Text;
            string outputFolderPath = textBox4.Text;

            if (string.IsNullOrEmpty(inputFilePath) || string.IsNullOrEmpty(outputFolderPath))
            {
                MessageBox.Show("PLEASE SELECT BOTH AN INPUT FILE AND OUTPUT FOLDER");
                return;
            }

            string outputFilePath = System.IO.Path.Combine(outputFolderPath, "Report.txt"); // Create a report filename
            progressBar2.Value = 0;
            GenerateReport(inputFilePath, outputFilePath);
        }
        private void GenerateReport(string inputFilePath, string outputFilePath)
        {
            /*if (radioButton5.Checked)
            {*/
            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + inputFilePath))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM STUDENT_INFO", conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                int totalRows = dataTable.Rows.Count;
                progressBar2.Maximum = totalRows; // Set the maximum value of the progress bar

                using (StreamWriter wr = new StreamWriter(outputFilePath))
                {
                    int TotalRows = dataTable.Rows.Count;
                    for (int i = 0; i < totalRows; i++)
                    {
                        var itemArrayAsStrings = dataTable.Rows[i].ItemArray.Select(item => item.ToString()).ToArray();
                        wr.WriteLine(string.Join(",", itemArrayAsStrings));
                        progressBar2.Value = i + 1;
                    }
                }
            }
            MessageBox.Show("Report has been successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Access Files|*.accdb|Excel Files|*.xlsx";
                openFileDialog.Title = "Select an Access or Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox3.Text = openFileDialog.FileName; // Set the input file path
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a Folder to Save the Report";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox4.Text = folderBrowserDialog.SelectedPath; // Set the output folder path
                }
            }
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
        private string GetSelectedBackupType()
        {
            if (radioButton1.Checked) return "StudentInfo";
            if (radioButton2.Checked) return "TeacherInfo";
            if (radioButton3.Checked) return "Attendance";
            return null;
        }
        private void BackupAccessData(string inputFilePath, string backupType, string outputFolderPath)
        {
               using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + inputFilePath))
                     {
         
        conn.Open();
        string query;

        switch (backupType)
        {
            case "StudentInfo":
                query = "SELECT * FROM Students";
                break;
            case "TeacherInfo":
                query = "SELECT * FROM Teachers";
                break;
            case "Attendance":
                query = "SELECT * FROM Attendance";
                break;
            default:
                throw new InvalidOperationException("Invalid backup type.");
        }

           using (OleDbCommand cmd = new OleDbCommand(query, conn))
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            string csvFilePath = Path.Combine(outputFolderPath, "{backupType}.csv");
            using (StreamWriter writer = new StreamWriter(csvFilePath))
            {
                // Write headers
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (i > 0) writer.Write(","); // Add a comma before subsequent column names
                    writer.Write(dataTable.Columns[i].ColumnName);
                }
                writer.WriteLine(); // Move to the next line after writing all headers

                // Write data
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i > 0) writer.Write(","); // Add a comma before subsequent values
                        writer.Write(row[i].ToString());
                    }
                    writer.WriteLine(); // Move to the next line after writing all values
                }
               }
             {
            }
           }
         }
        }
        private void BackupExcelData(string inputFilePath, string backupType, string outputFolderPath)
        {
            // Implement Excel backup logic here
            MessageBox.Show("Excel backup logic needs to be implemented.");
        }
        private void BackupTextFileData(string inputFilePath, string backupType, string outputFolderPath)
        {
            string destinationFilePath = Path.Combine(outputFolderPath, Path.GetFileName(inputFilePath));
            File.Copy(inputFilePath, destinationFilePath, true); // Copy the file to the backup location
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the input file path from textBox1
            string inputFilePath = textBox1.Text; // The path of the Access/Excel file
            // Get the selected file type from comboBox1
            string fileType = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
            // Get the output folder path from textBox4
            string outputFolderPath = textBox2.Text; // The folder to save backups
            // Get the selected backup type
            string backupType = GetSelectedBackupType();

        /*string fileType = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;
        string backupType = GetSelectedBackupType();
        string inputFilePath = textBox3.Text; // The path of the Access/Excel file
        string outputFolderPath = textBox4.Text; // The folder to save backups*/

        if (string.IsNullOrEmpty(inputFilePath) || string.IsNullOrEmpty(outputFolderPath) || string.IsNullOrEmpty(backupType))
        {
            MessageBox.Show("Please select a file, a folder, and a backup option.");
            return;
        }

        try
        {
            if (fileType == "Access")
            {
                BackupAccessData(inputFilePath, backupType, outputFolderPath);
            }
            else if (fileType == "Excel")
            {
                BackupExcelData(inputFilePath, backupType, outputFolderPath);
            }
            else if (fileType == "Text File")
            {
                BackupTextFileData(inputFilePath, backupType, outputFolderPath);
            }

            MessageBox.Show("Backup created successfully!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: "+ex.Message);
        }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder to save your files";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyDocuments; // Optional: Set the default folder

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Set the selected path to textBox4 (or any other TextBox you want to use for output folder)
                    textBox4.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Access Files|*.accdb|Excel Files|*.xls;*.xlsx|All Files|*.*"; // Adjust the filter as needed
                openFileDialog.Title = "Select an Input File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName; // Set the selected file path to textBox1
                }
            }
        }

        
    }
        
    }
