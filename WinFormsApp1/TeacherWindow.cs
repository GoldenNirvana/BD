using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    enum RowState
    {
        Existed,
        New,
        Modifed,
        ModifedNew,
        Deleted
    }

    public partial class TeacherWindow : Form
    {
        int index;
        DataBase database = new DataBase();
        private Form f;
        int pdID;
        public TeacherWindow(Form form, int pdID)
        {
            this.pdID = pdID;
            f = form;
            f.Hide();
            InitializeComponent();
            string str = $"select ClassName from Classes";
            database.openConnection();
            SqlCommand command = new SqlCommand(str, database.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string className = reader.GetValue(0).ToString();
                classes.Items.Add(className);
            }
            database.closeConnection();
        }

        private void createColumns()
        {
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns.Add("n1", "Фамилия");
            dataGridView1.Columns.Add("n2", "Имя");
            dataGridView1.Columns.Add("n3", "Отчество");
        }

        private void readSingleRows(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3));
        }

        private void refrestDGW(DataGridView dgw, String indexClass)
        {
            dgw.Rows.Clear();
            string q = $"select Pupils.ID, Namee, Surname, Patronymic from PersonalData join Pupils on PersonalData.ID = Pupils.DataID where ClassID = '{indexClass}'";
            SqlCommand command = new SqlCommand(q, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                readSingleRows(dgw, reader);
            }
            reader.Close();
        }

        private void exit(object sender, EventArgs e)
        {
            f.Show();
            Close();
        }

        private void classes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string q = $"select id from Classes where ClassName = '{classes.Text}'";
            nameBox.Text = "";
            surnameBox.Text = "";
            FatherBox.Text = "";
            averadgeBox.Text = "";
            database.openConnection();
            SqlCommand command = new SqlCommand(q, database.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string indexClass = reader.GetValue(0).ToString();
                database.closeConnection();
                reader.Close();
                refrestDGW(dataGridView1, indexClass);
            }
            database.closeConnection();
            reader.Close();
        }

        private void TeacherWindow_Load(object sender, EventArgs e)
        {
            createColumns();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if (index >= 0 && index < dataGridView1.RowCount - 1)
            {
                DataGridViewRow row = dataGridView1.Rows[index];
                nameBox.Text = row.Cells[1].Value.ToString();
                surnameBox.Text = row.Cells[2].Value.ToString();
                FatherBox.Text = row.Cells[3].Value.ToString();
                updateMarks();
            }
        }

        private void addMark_Click(object sender, EventArgs e)
        {
            CheckAnswersFrom from = new CheckAnswersFrom(pdID, createSubjectAvalible());
            from.Show();
        }

        private void changeMark_Click(object sender, EventArgs e)
        {
            int ind = dataGridView2.CurrentCell.RowIndex;
            if (textBox1.Text != "")
            {
                
                DataGridViewRow row = dataGridView2.Rows[ind];
                string q = $"update HomeTasksAnswers set Grade = '{textBox1.Text}' where ID = {row.Cells[0].Value.ToString()}";
                database.openConnection();
                SqlCommand command = new SqlCommand(q, database.GetSqlConnection());
                command.ExecuteNonQuery();
                database.closeConnection();
                updateMarks();
            }
        }

        private void updateMarks()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("id", "ID");
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns.Add("gr", "Оценка");
            dataGridView2.Columns.Add("date", "Дата обновления");
            DataGridViewRow row = dataGridView1.Rows[index];
            int id = int.Parse(row.Cells[0].Value.ToString());
            string q = $"select ID, Grade, UpdateDate from HomeTasksAnswers where PupilID = '{id.ToString()}'";
            int count = 0;
            double sum = 0.0;
            SqlCommand command1 = new SqlCommand(q, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                
                try
                {
                    sum += reader1.GetInt32(1);
                }
                catch
                {
                    continue;
                }
                count++;
                dataGridView2.Rows.Add(reader1.GetInt32(0), reader1.GetInt32(1), reader1.GetString(2));
            }
            if (count > 0)
            {
                if (sum == 0.0)
                {
                    averadgeBox.Text = "Нет оценок";
                }
                else
                {
                    averadgeBox.Text = (sum / count).ToString();
                }  
            }
            reader1.Close();
            database.closeConnection();
        }

        private void addDZ_Click(object sender, EventArgs e)
        {
            AddHomeTask task = new AddHomeTask(pdID, createSubjectAvalible());
            task.Show();
        }

        private List<string> createSubjectAvalible()
        {
            dataGridView1.Rows.Clear();
            createColumns();
            string q = $"select SubjectID from Specializations\r\njoin Teachers\r\non Teachers.ID = Specializations.TeacherID where Teachers.Id = {pdID}";
            List<string> array = new List<string>();

            database.openConnection();
            SqlCommand command = new SqlCommand(q, database.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                string subjectAvalible = reader.GetValue(0).ToString();
                array.Add(subjectAvalible);
            }
            reader.Close();
            database.closeConnection();
            return array;
        }
    }
}