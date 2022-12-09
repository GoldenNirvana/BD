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
    public partial class EditUser : Form
    {

        DataBase database = new DataBase();
        DataTable dt;
        int ind;
        public EditUser()
        {
            InitializeComponent();
            createColumns();
            dt = getPersons(database);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["role"].ToString() == "0")
                    dataGridView1.Rows.Add(dt.Rows[i]["Surname"].ToString(), dt.Rows[i]["Namee"].ToString(), "Ученик");
                if (dt.Rows[i]["role"].ToString() == "1")
                    dataGridView1.Rows.Add(dt.Rows[i]["Surname"].ToString(), dt.Rows[i]["Namee"].ToString(), "Преподаватель");
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void createColumns()
        {
            dataGridView1.Columns.Add("n0","Фамилия");
            dataGridView1.Columns.Add("n1","Имя");
            dataGridView1.Columns.Add("n2","Роль");
            dataGridView1.Columns.Add("n3","Отчество");
            dataGridView1.Columns.Add("n4","Телефон");
            dataGridView1.Columns.Add("n5","Логин");
            dataGridView1.Columns.Add("n6","Пароль");
            dataGridView1.Columns.Add("n7", "pdId");
            dataGridView1.Columns.Add("n8", "id");
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updatePerson(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text,
                dt.Rows[ind]["pdID"].ToString(),
                dt.Rows[ind]["id"].ToString(), database); ;
        }

        private static void updatePerson(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "updatePerson";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@Sur",
                Value = s1
            };
            SqlParameter a2 = new SqlParameter
            {
                ParameterName = "@Name",
                Value = s2
            };
            SqlParameter a3 = new SqlParameter
            {
                ParameterName = "@Fat",
                Value = s3
            };
            SqlParameter a4 = new SqlParameter
            {
                ParameterName = "@Ph",
                Value = s4
            };
            SqlParameter a5 = new SqlParameter
            {
                ParameterName = "@log",
                Value = s5
            };
            SqlParameter a6 = new SqlParameter
            {
                ParameterName = "@pwd",
                Value = s6
            };
            SqlParameter a7 = new SqlParameter
            {
                ParameterName = "@pdID",
                Value = s7
            };
            SqlParameter a8 = new SqlParameter
            {
                ParameterName = "@userId",
                Value = s8
            };
            command.Parameters.Add(a1);
            command.Parameters.Add(a2);
            command.Parameters.Add(a3);
            command.Parameters.Add(a4);
            command.Parameters.Add(a5);
            command.Parameters.Add(a6);
            command.Parameters.Add(a7);
            command.Parameters.Add(a8);
            adapter.SelectCommand = command;
            adapter.Fill(table);
        }

        private static DataTable getPersons(DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "getPersons";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                ind = e.RowIndex;
                textBox1.Text = dt.Rows[e.RowIndex]["Surname"].ToString();
                textBox2.Text = dt.Rows[e.RowIndex]["Namee"].ToString();
                textBox3.Text = dt.Rows[e.RowIndex]["Patronymic"].ToString();
                textBox4.Text = dt.Rows[e.RowIndex]["PhoneNumber"].ToString();
                textBox5.Text = dt.Rows[e.RowIndex]["login"].ToString();
                textBox6.Text = dt.Rows[e.RowIndex]["passwd"].ToString();
            }
        }
    }
}
