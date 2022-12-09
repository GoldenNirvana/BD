using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ChangeSc : Form
    {
        DataBase dataBase = new DataBase();
        List<int> classesId = new List<int>();
        List<int> roomsId = new List<int>();
        List<int> teacherID = new List<int>();
        List<int> subjectsId = new List<int>();
        public ChangeSc()
        {
            InitializeComponent();
            getClasses();
            getRooms();
            getSubjets();
            getTeachers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != ""
                && textBox1.Text != "" && textBox2.Text != "")
            {
                insertLesson(classesId[comboBox1.SelectedIndex].ToString(), roomsId[comboBox2.SelectedIndex].ToString(), subjectsId[comboBox4.SelectedIndex].ToString(),
                    textBox1.Text.ToString(), teacherID[comboBox3.SelectedIndex].ToString(), textBox2.Text.ToString(), dataBase);
                MessageBox.Show("Добавлено в расписание");
                return;
            }
            MessageBox.Show("Неверные параметры");
        }

        private void getClasses()
        {
            string str = $"select ClassName, Id from Classes";
            dataBase.openConnection();
            SqlCommand command = new SqlCommand(str, dataBase.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string className = reader.GetValue(0).ToString();
                classesId.Add(int.Parse(reader.GetValue(1).ToString()));
                comboBox1.Items.Add(className);
            }
            reader.Close();
            dataBase.closeConnection();
        }

        private void getRooms()
        {
            string str = $"select Number, Id from ClassRoom";
            dataBase.openConnection();
            SqlCommand command = new SqlCommand(str, dataBase.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string className = reader.GetValue(0).ToString();
                roomsId.Add(int.Parse(reader.GetValue(1).ToString()));
                comboBox2.Items.Add(className);
            }
            reader.Close();
            dataBase.closeConnection();
        }

        private void getTeachers()
        {
            string str = $"select Surname, Teachers.ID from PersonalData\r\njoin Teachers on Teachers.DataID = PersonalData.ID";
            dataBase.openConnection();
            SqlCommand command = new SqlCommand(str, dataBase.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string className = reader.GetValue(0).ToString();
                teacherID.Add(int.Parse(reader.GetValue(1).ToString()));
                comboBox3.Items.Add(className);
            }
            reader.Close();
            dataBase.closeConnection();
        }

        private void getSubjets()
        {
            string str = $"select SubjectName, ID from Subjects";
            dataBase.openConnection();
            SqlCommand command = new SqlCommand(str, dataBase.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string className = reader.GetValue(0).ToString();
                subjectsId.Add(int.Parse(reader.GetValue(1).ToString()));
                comboBox4.Items.Add(className);
            }
            reader.Close();
            dataBase.closeConnection();
        }

        private void insertLesson(string s1, string s2, string s3, string s4, string s5, string s6, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "insertLesson";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@classId",
                Value = s1
            };
            SqlParameter a2 = new SqlParameter
            {
                ParameterName = "@classRoomId",
                Value = s2
            };
            SqlParameter a3 = new SqlParameter
            {
                ParameterName = "@subjectId",
                Value = s3
            };
            SqlParameter a4 = new SqlParameter
            {
                ParameterName = "@date",
                Value = s4
            };
            SqlParameter a5 = new SqlParameter
            {
                ParameterName = "@teacherId",
                Value = s5
            };
            SqlParameter a6 = new SqlParameter
            {
                ParameterName = "@time",
                Value = s6
            };
            command.Parameters.Add(a1);
            command.Parameters.Add(a2);
            command.Parameters.Add(a3);
            command.Parameters.Add(a4);
            command.Parameters.Add(a5);
            command.Parameters.Add(a6);
            adapter.SelectCommand = command;
            adapter.Fill(table);  
        }

        private void ChangeSc_Load(object sender, EventArgs e)
        {

        }
    }
}
