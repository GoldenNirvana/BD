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
    }
}
