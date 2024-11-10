using PRG2782_Project.Data_layer;
using System;
using System.Windows.Forms;
using System.Linq;

// Placeholder import, not actively used but might be later on
using System.Text.RegularExpressions;

namespace PRG2782_Project
{
    public partial class Form1 : Form
    {
        private FileHandler fileHandler = new FileHandler(); 
        public Form1()
        {
            InitializeComponent();
            LoadStudentsIntoList();
        }
        private void LoadStudentsIntoList()
        {
            lstStudents.Items.Clear();
            var studentList = fileHandler.ReadStudents();

            foreach (var stud in studentList)
            {
                // TODO: Add error checking for any malformed student data here
                lstStudents.Items.Add($"ID: {stud[0]}, Name: {stud[1]}, Age: {stud[2]}, Course: {stud[3]}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim(); 

            if (!string.IsNullOrEmpty(studentId))
            {
                FileHandler fileHandler = new FileHandler();
                fileHandler.RemoveStudentById(studentId); 
                LoadStudentsIntoList();
                MessageBox.Show("Student removed successfully."); 
            }
            else
            {
                MessageBox.Show("Please enter a valid student ID."); 
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            fileHandler.GenerateSummary(out int total, out double avgAge);

            label1.Text = "Total Students: " + total;
            label2.Text = "Average Age: " + avgAge.ToString("F1");
            MessageBox.Show("Summary created & saved to summary.txt.");
        }

        private void lstStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstStudents.Items.Clear();
            var studentList = fileHandler.ReadStudents();

            foreach (var stud in studentList)
            {
                // TODO: Add error checking for any malformed student data here
                lstStudents.Items.Add($"ID: {stud[0]}, Name: {stud[1]}, Age: {stud[2]}, Course: {stud[3]}");
            }

        }

    }
}
