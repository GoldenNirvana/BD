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
    public partial class AddHomeTask : Form
    {
        DataBase db = new DataBase();
        int pdId;
        List<string> subj;


        public AddHomeTask(int id, List<string> subj)
        {
            InitializeComponent();
            createColumns();
            pdId = id;
            this.subj = subj;
            updateDgw();
        }

        private void createColumns()
        {
            dataGridView1.Columns.Add("n0", "Предмет");
            dataGridView1.Columns.Add("n1", "Класс");
            dataGridView1.Columns.Add("n2", "Дата и время урока");
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns[3].Visible = false;
        }

        private void updateDgw()
        {
            foreach (string str in subj)
            {
                string qu = $"select Schedule.Id, DateLesson, TimeLesson, SubjectName, ClassName from Schedule \r\njoin Subjects on Schedule.SubjectID = Subjects.ID\r\njoin Classes on ClassID = Classes.Id\r\nwhere SubjectID = {str}";
                db.openConnection();
                SqlCommand command1 = new SqlCommand(qu, db.GetSqlConnection());
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    dataGridView1.Rows.Add(reader1.GetString(3), reader1.GetString(4), reader1.GetString(2), reader1.GetInt32(0));
                }
                reader1.Close();
                db.closeConnection();
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void AddHomeTask_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DataTable dataTable = getIdNewTask(textBox1.Text, db);
                DataRow row = dataTable.Rows[0];
                string newId = row["id"].ToString();
                int i = dataGridView1.CurrentCell.RowIndex;
                addNewHoneTask(dataGridView1.Rows[i].Cells[3].Value.ToString(), newId, db);
            }
            else
            {
                MessageBox.Show("Введите задание.");
                return;
            }
        }

        private static DataTable getIdNewTask(string taskText, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "addTextTask";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter textParam = new SqlParameter
            {
                ParameterName = "@task",
                Value = taskText
            };
            command.Parameters.Add(textParam);

            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        private static DataTable addNewHoneTask(string lessonId, string textId, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "addHomeTask";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter lesson = new SqlParameter
            {
                ParameterName = "@lessonId",
                Value = lessonId
            };

            SqlParameter text = new SqlParameter
            {
                ParameterName = "@textId",
                Value = textId
            };
            command.Parameters.Add(lesson);
            command.Parameters.Add(text);

            adapter.SelectCommand = command;
            adapter.Fill(table);
            MessageBox.Show("Задание выдано.");
            return table;
        }
    }
}
