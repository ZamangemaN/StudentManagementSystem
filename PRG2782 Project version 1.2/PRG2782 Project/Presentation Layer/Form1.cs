using PRG2782_Project.Data_layer;
using System;
using System.Windows.Forms;
using System.Linq;

// Placeholder import, not actively used but might be later on
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
            dataGridViewStudents.Rows.Clear(); // Clear existing rows
            var studentList = fileHandler.ReadStudents();

            foreach (var stud in studentList)
            {
                dataGridViewStudents.Rows.Add(stud[0], stud[1], stud[2], stud[3]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                string studentId = dataGridViewStudents.SelectedRows[0].Cells["ID"].Value.ToString();
                fileHandler.RemoveStudentById(studentId);
                LoadStudentsIntoList(); 
                MessageBox.Show("Student removed successfully.");
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            fileHandler.GenerateSummary(out int total, out double avgAge);

            label1.Text = "Total Students: " + total;
            label2.Text = "Average Age: " + avgAge.ToString("F1");
            MessageBox.Show("Summary created & saved to summary.txt.");
        }

       

       

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                int id = int.Parse(dataGridViewStudents.SelectedRows[0].Cells["ID"].Value.ToString());
                string name = txtName.Text.Trim();
                int age = int.Parse(txtAge.Text.Trim());
                string course = txtCourse.Text.Trim();

                // Update the student
                fileHandler.AddOrUpdateStudent(id, name, age, course);
                LoadStudentsIntoList(); // Refresh the DataGridView
                MessageBox.Show("Student updated successfully.");
            }
            else
            {
                MessageBox.Show("Please select a student to update.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            int age = int.Parse(txtAge.Text.Trim());
            string course = txtCourse.Text.Trim();

            // Get the earliest available ID
            int id = fileHandler.GetEarliestAvailableId();

            // Add new student
            fileHandler.AddOrUpdateStudent(id, name, age, course);
            LoadStudentsIntoList(); // Refresh the DataGridView
            MessageBox.Show("Student added successfully.");
        }

        private void Student_Click(object sender, EventArgs e)
        {

        }
    }
}
