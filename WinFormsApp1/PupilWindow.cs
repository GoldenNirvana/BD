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
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class PupilWindow : Form
    {
        DataBase dataBase= new DataBase();
        int Id;
        int classId;

        public PupilWindow(int id, int classId)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Id = id;
            this.classId = classId;
            InitializeComponent();
            createColumnsForMarks();
            createColumnsForTasks();
            fillTableForTasks();
            fillTableForMarks();
        }

        private void createColumnsForMarks()
        {
            dataGridView2.Columns.Add("n0", "Предмет");
            dataGridView2.Columns.Add("n1", "Оценка");
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
            dataGridView2.Rows.Clear();
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

        private void PupilWindow_Load(object sender, EventArgs e)
        {
          
        }

        private void checkMarks_Click(object sender, EventArgs e)
        {
            
        }

        private void addAnswer_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            string taskId = dataGridView1.Rows[i].Cells[2].Value.ToString();
            addNewHomeTaskAnswer(textBox1.Text, Id.ToString(), taskId, dataBase);
            fillTableForTasks();
        }

        private void raiting_Click(object sender, EventArgs e)
        {

        }

        private void addMonye_Click(object sender, EventArgs e)
        {

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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
