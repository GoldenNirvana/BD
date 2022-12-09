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
    public partial class SecWindow : Form
    {
        DataBase database = new DataBase();
        Form form;
        string pdId;

        public SecWindow(Form form, string pdId)
        {
            this.form = form;
            this.pdId = pdId;
            form.Hide();
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            form.Show();
            Close();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            NewUser user = new NewUser();
            user.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void SecWindow_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("n0", "Фамилия");
            dataGridView1.Columns.Add("n1", "Остаток средств");
            dataGridView1.Columns.Add("n1", "Tелефон");
            DataTable dt = getPupilsForTable(database);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i]["Surname"].ToString(), dt.Rows[i]["CountOfMoney"].ToString(), dt.Rows[i]["PhoneNumber"].ToString());
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private DataTable getPupilsForTable(DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "getPupilsLittleMoney";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
