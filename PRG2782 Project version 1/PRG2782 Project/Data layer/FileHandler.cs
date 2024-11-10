using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
// Not sure if this import is needed yet, but keeping it for now
using System.Text.RegularExpressions;

namespace PRG2782_Project.Data_layer
{
    public class FileHandler
    {
        private string studentsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.txt");
        private string summaryFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "summary.txt");
        private string basePath; // Not actually used, but could be a base directory for future expansion

        
        public FileHandler()
        {
            
            if (!File.Exists(studentsFile))
            {
                // file creation could be improved here, but for now just ensuring it exists
                File.WriteAllText(studentsFile, "");
            }
            if (!File.Exists(summaryFile))
            {
                File.WriteAllText(summaryFile, "");
            }
        }

        public List<string[]> ReadStudents()
        {
            var studentsData = new List<string[]>();
            try
            {
                foreach (var line in File.ReadAllLines(studentsFile))
                {
                    studentsData.Add(line.Split(',')); 
                }
            }
            catch (IOException ex)
            {
                // Simple error log; may need to replace with a full logging system
                Console.WriteLine("File read error: " + ex.Message);
            }
            return studentsData;
        }

        public void RemoveStudentById(string studId)
        {
            try
            {
                // Read all lines from the students file
                var lines = File.ReadAllLines(studentsFile).ToList();

                // Remove the line that starts with the student ID
                lines = lines.Where(line => !line.StartsWith(studId + ",")).ToList();

                // Write the updated list back to the file
                File.WriteAllLines(studentsFile, lines);

                Console.WriteLine("Student successfully removed."); // Optional console feedback
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting student: " + ex.Message); // Error handling
            }
        }

        public void GenerateSummary(out int total, out double avgAge)
        {
            // Using a List here instead of an array for future expandability
            total = 0; avgAge = 0.0;
            var studentAges = new List<int>();

            try
            {
                foreach (var line in File.ReadAllLines(studentsFile))
                {
                    // TODO: Consider more rigorous data validation here if format changes
                    var fields = line.Split(',');
                    if (fields.Length > 2)
                    {
                        studentAges.Add(int.Parse(fields[2]));
                        total++;
                    }
                }
                avgAge = studentAges.Count > 0 ? studentAges.Average() : 0;
                File.WriteAllText(summaryFile, $"Total Students: {total}\nAvg Age: {avgAge:F1}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Summary calculation issue: " + ex.Message); // quick log for any failures
            }
        }
    }
}
