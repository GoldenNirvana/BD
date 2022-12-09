using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class PupilWindow : Form
    {
        bool isAllMarks = false;

        DataBase dataBase= new DataBase();
        int Id;
        int classId;

        public PupilWindow(int id, int classId)
        {
            Id = id;
            this.classId = classId;
            InitializeComponent();
            createColumnsForMarks();
            createColumnsForTasks();
            fillTableForTasks();
            fillTableForMarks();
            textBox2.Text = getCountOfMoney("0", Id.ToString(), dataBase);
            if (SqlMoney.Parse(textBox2.Text) < 100)
            {
                textBox2.BackColor = Color.Red;
            }
            if (SqlMoney.Parse(textBox2.Text) > 100)
            {
                textBox2.BackColor = Color.Green;
            }
        }

        private void createColumnsForMarks()
        {
            dataGridView2.Columns.Add("n0", "Предмет");
            dataGridView2.Columns.Add("n1", "Оценка");
        }

        private void createColumnsForRaiting()
        {
            dataGridView2.Columns.Add("n0", "Фамилия Имя");
            dataGridView2.Columns.Add("n1", "Средний балл");
        }

        private void createColumnsForTasks()
        {
            dataGridView1.Columns.Add("n0", "Предмет");
            dataGridView1.Columns.Add("n2", "Задание");
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns[2].Visible = false;
        }

        private void fillTableForTasks()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataBase.openConnection();
            string qu = $"select distinct SubjectName, TaskText, HomeTasks.ID from HomeTasks\r\njoin Schedule on HomeTasks.LessonID = Schedule.Id\r\njoin TaskTexts on TaskID = TaskTexts.ID\r\njoin Classes on Classes.ID = ClassID\r\njoin Subjects on SubjectID = Subjects.ID\r\nwhere ClassID = {classId} and not exists \r\n(select * from HomeTasksAnswers\r\nwhere PupilID = {Id} and HomeTasksAnswers.HomeTaskID = HomeTasks.ID)\r\ngroup by SubjectName, TaskText, HomeTasks.ID";
            SqlCommand command1 = new SqlCommand(qu, dataBase.GetSqlConnection());
            SqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                dataGridView1.Rows.Add(reader1.GetString(0), reader1.GetString(1), reader1.GetInt32(2));
            }
            reader1.Close();
            dataBase.closeConnection();
        }

        private void fillTableForMarks()
        {
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataBase.openConnection();
            string qu = $"select Grade, SubjectName from HomeTasksAnswers\r\njoin HomeTasks on HomeTasksAnswers.HomeTaskID = HomeTasks.ID\r\njoin Schedule on HomeTasks.LessonID = Schedule.Id\r\njoin Subjects on Schedule.SubjectID = Subjects.ID\r\nwhere PupilID = {Id} and Grade is not Null\r\ngroup by Grade, SubjectName";
            SqlCommand command1 = new SqlCommand(qu, dataBase.GetSqlConnection());
            SqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                dataGridView2.Rows.Add(reader1.GetInt32(0), reader1.GetString(1));
            }
            reader1.Close();
            dataBase.closeConnection();
        }

        private void checkMarks_Click(object sender, EventArgs e)
        {
            if (!isAllMarks)
            {
                return;
            }
            isAllMarks = false;
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            createColumnsForMarks();
            fillTableForMarks();
        }

        private void addAnswer_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            string taskId = dataGridView1.Rows[i].Cells[2].Value.ToString();
            addNewHomeTaskAnswer(textBox1.Text, Id.ToString(), taskId, dataBase);
            fillTableForTasks();
        }


        private void fillTableForRaiting()
        {
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable pupils = getPeopleInClass(dataBase);
            for (int i = 0; i < pupils.Rows.Count; i++)
            {
                string currPupId = pupils.Rows[i]["ID"].ToString();
                DataTable dataTable = getAvg(currPupId, dataBase);
                dataGridView2.Rows.Add(dataTable.Rows[0]["Namee"].ToString() + " " + dataTable.Rows[0]["Surname"].ToString(), dataTable.Rows[0]["av"].ToString());
            }
        }
        private void raiting_Click(object sender, EventArgs e)
        {
            if (isAllMarks)
            {
                return;
            }
            isAllMarks = true;
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            createColumnsForRaiting();
            fillTableForRaiting();
        }
        
        private static DataTable addNewHomeTaskAnswer(string ans, string pupId, string hometaskid, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "addHomeTaskAnswer";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter an = new SqlParameter
            {
                ParameterName = "@ans",
                Value = ans
            };

            SqlParameter puid  = new SqlParameter
            {
                ParameterName = "@pupId",
                Value = pupId
            };

            SqlParameter task = new SqlParameter
            {
                ParameterName = "@hometaskid",
                Value = hometaskid
            };

            SqlParameter date = new SqlParameter
            {
                ParameterName = "@date",
                Value = "2018-09-15"
            };
            command.Parameters.Add(an);
            command.Parameters.Add(puid);
            command.Parameters.Add(task);
            command.Parameters.Add(date);

            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        private DataTable getPeopleInClass(DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "getPupilsIdInClass";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter an = new SqlParameter
            {
                ParameterName = "@classId",
                Value = classId
            };       
            command.Parameters.Add(an);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        private DataTable getAvg(string pupId, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "getAvg";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter an = new SqlParameter
            {
                ParameterName = "@pupilId",
                Value = pupId
            };
            command.Parameters.Add(an);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                getCountOfMoney(textBox3.Text, Id.ToString(), dataBase);
            }
            textBox2.Text = getCountOfMoney("0", Id.ToString(), dataBase);
            if (SqlMoney.Parse(textBox2.Text) > 100)
            {
                textBox2.BackColor = Color.Green;
            }
            if (SqlMoney.Parse(textBox2.Text) < 100)
            {
                textBox2.BackColor = Color.Red;
            }
        }

        private string getCountOfMoney(string mon,string id ,DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "updateMoney";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@mon",
                Value = mon
            };
            SqlParameter a2 = new SqlParameter
            {
                ParameterName = "@id",
                Value = id
            };
            command.Parameters.Add(a1);
            command.Parameters.Add(a2);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table.Rows[0]["CountOfMoney"].ToString();
        }


    }
}
