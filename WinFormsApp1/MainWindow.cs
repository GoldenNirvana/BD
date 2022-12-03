using System.Data;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class MainWindow : Form
    {
        DataBase database = new DataBase();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var pswd = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"select id, login, passwd, role, personalDataId from users where login = '{login}' and passwd = '{pswd}'";
            SqlCommand command = new SqlCommand(query, database.GetSqlConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
              
                MessageBox.Show("Succesfull");

                string query1 = $"select id, login, passwd, role, personalDataId from users where login = '{login}' and passwd = '{pswd}' and role = 0";
                SqlCommand command1 = new SqlCommand(query1, database.GetSqlConnection());
                adapter.SelectCommand = command1;
                table.Clear();
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Вошли как ученик");
                    Hide();
                    Form pupil = new PupilWindow();
                    pupil.Show();
                }

                string query2 = $"select id, login, passwd, role, personalDataId from users where login = '{login}' and passwd = '{pswd}' and role = 1";
                SqlCommand command2 = new SqlCommand(query2, database.GetSqlConnection());
                adapter.SelectCommand = command2;
                table.Clear();
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Вошли как учитель");
                    Hide();
                    Form pupil = new TeacherWindow();
                    pupil.Show();
                }

                string query3 = $"select id, login, passwd, role, personalDataId from users where login = '{login}' and passwd = '{pswd}' and role = 2";
                SqlCommand command3 = new SqlCommand(query3, database.GetSqlConnection());
                adapter.SelectCommand = command3;
                table.Clear();
                adapter.Fill(table);
                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Вошли как секретарь");
                    Hide();
                    Form pupil = new SecWindow();
                    pupil.Show();
                }

                

            }
        }
    }
}