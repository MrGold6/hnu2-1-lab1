using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<Student> students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
        }
        public class SessionResult
        {
            public string name;
            public int mark;
            public SessionResult() { }

            public SessionResult(string name, int mark)
            {
                this.name = name;
                this.mark = mark;
            }

            public SessionResult(string serializedObject)
            {
                string[] parts = serializedObject.Split(',');
                name = parts[0];
                mark = int.Parse(parts[1]);
            }
            public override string ToString()
            {
                return $"{name}: {mark}";
            }

        };
        
        public class Student
        {
            public string name;
            public string surname;
            public int course;
            public string group;
            public List<SessionResult> sessionResult = new List<SessionResult>();

            public Student() { }

            public Student(string name, string surname, int course, string group) 
            { 
                this.name = name;
                this.surname = surname;
                this.course = course;
                this.group = group;
            }

            public Student(string serializedObject)
            {
                string[] parts = serializedObject.Split(',');
                name = parts[0];
                surname = parts[1];
                course = int.Parse(parts[2]);
                group = parts[3];
            }
            public override string ToString()
            {
                return $"{name},{surname},{course},{group}";
            }

        };

        bool compare(SessionResult a, SessionResult b)
        {
             if (a.mark<b.mark)
                return true;
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("students.txt"))
            {
                // Write each object to the file
                foreach (Student obj in students)
                {
                    writer.WriteLine(obj.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("students.txt"))
            {
                richTextBox1.Text = "Файл не існує";
                return;
            }

            List<Student> objects = new List<Student>();

            using (StreamReader reader = new StreamReader("students.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    objects.Add(new Student(line));
                }
            }

            Random random = new Random();
            int randomNumber;

            foreach (Student student in objects)
            {
                randomNumber = random.Next(1, 6);
                student.sessionResult.Add(new SessionResult("math", randomNumber));
               
                randomNumber = random.Next(1, 6);
                student.sessionResult.Add(new SessionResult("lang", randomNumber));
               
                randomNumber = random.Next(1, 6);
                student.sessionResult.Add(new SessionResult("science", randomNumber));
                
                randomNumber = random.Next(1, 6);
                student.sessionResult.Add(new SessionResult("programing", randomNumber));
            }

            getCountOfAPlus(objects);

            richTextBox1.Text += "\r\n";

            foreach (Student student in objects)
            {
                richTextBox1.Text += student.ToString() + "\r\n";
                richTextBox1.Text += "Session result:" + "\r\n";
                foreach (SessionResult sessionResult in student.sessionResult)
                {
                    richTextBox1.Text += sessionResult.ToString() + "\r\n";
                }
            }


            
        }

        void getCountOfAPlus(List<Student> students)
        {
            int[] course = { 0, 0, 0, 0};

          
          
            foreach (Student student in students)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (student.sessionResult[j].mark == 5)
                    {
                        course[student.course - 1]++;
                    }
                }
            }
            

            int max = 0;
            for (int i = 0; i < 4;)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (max == course[j])
                    {
                        i++;
                        richTextBox1.Text+= "Course: " + (j + 1) + "\r\n";
                        richTextBox1.Text += "Count of A+: " + course[j] + "\r\n";
                    }
                }
                max++;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            students.Add(
                new Student(
                    textBox1.Text,
                    textBox2.Text,
                    Decimal.ToInt32(numericUpDown1.Value),
                    textBox3.Text
                    )
                );
        }
    }

}
