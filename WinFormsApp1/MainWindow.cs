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
            string query = $"select id, login, passwd, role, personalDataId from users where login = '{login}' and passwd = '{pswd}'";
            database.openConnection();
            SqlCommand command = new SqlCommand(query, database.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                object role = reader.GetValue(3);
                int r = int.Parse(role.ToString());
                if (r == 0)
                {
                    Form pupil = new PupilWindow();
                    pupil.Show();
                }
                if (r == 1)
                {
                    Form pupil = new TeacherWindow(this);
                    pupil.Show();
                }
                if (r == 2)
                {
                    Form pupil = new SecWindow(this);
                    pupil.Show();
                }
            }
            database.closeConnection();
        }
    }
}