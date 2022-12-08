using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class CheckAnswersFrom : Form
    {
        DataBase database = new DataBase();
        int pdID;
        List<string> subjs;

        public CheckAnswersFrom(int id, List<string> s)
        {
            pdID = id;
            subjs = s;
            InitializeComponent();
            createColumns();    
        }

        private void createColumns()
        {
            dataGridView1.Columns.Add("n0", "Предмет");
            dataGridView1.Columns.Add("n1", "Класс");
            dataGridView1.Columns.Add("n2", "ФИО");
            dataGridView1.Columns.Add("n3", "Ответ");
            dataGridView1.Columns.Add("n4", "Дата загрузки");
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns[5].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int i = dataGridView1.CurrentCell.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[i];
                string pId = row.Cells[5].Value.ToString();
                string q =  $"update HomeTasksAnswers set Grade = {textBox1.Text} where ID = {pId}";
                database.closeConnection();
                database.openConnection();
                SqlCommand command = new SqlCommand(q, database.GetSqlConnection());
                command.ExecuteNonQuery();
                database.closeConnection();
                updateScr();
            }
            else
            {
                MessageBox.Show("Введите оценку");
            }
        }

        private void CheckAnswersFrom_Load(object sender, EventArgs e)
        {
            updateScr();
        }


        private void updateScr()
        {
            int c = 0;
            foreach (string str in subjs)
            {
                string qu = $"select SubjectName, Namee, Surname, Answer, Classes.ClassName, HomeTasksAnswers.UpdateDate, HomeTasksAnswers.ID from Schedule\r\njoin HomeTasks on HomeTasks.LessonID = Schedule.Id\r\njoin HomeTasksAnswers on HomeTaskID = HomeTasks.ID\r\njoin Pupils on Pupils.DataID = PupilID\r\njoin PersonalData on PersonalData.ID = Pupils.DataID\r\njoin Subjects on Subjects.ID = SubjectID\r\njoin Classes on Classes.Id = Schedule.ClassID\r\nwhere Grade is Null and SubjectID = {str}";
                database.openConnection();
                SqlCommand command1 = new SqlCommand(qu, database.GetSqlConnection());
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    c++;
                    dataGridView1.Rows.Add(reader1.GetString(0), reader1.GetString(1) + ' ' + reader1.GetString(2), reader1.GetString(3), reader1.GetString(4), reader1.GetString(5), reader1.GetInt32(6));
                }
                reader1.Close();
                database.closeConnection();
            }
            if (c == 0)
            {
                MessageBox.Show("Нет выполненных заданий для проверки");
                Close();
                return;
            }
        }
    }
}
