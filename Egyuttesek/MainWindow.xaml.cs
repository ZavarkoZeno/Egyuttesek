using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Egyuttesek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public string connectionString = "server=localhost;user=root;database=vizsga;password=;";

        public void LoadData()
        {
            var conn = new MySqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM albumok";

            var cmd = new MySqlCommand(sql, conn);

            var adapter = new MySqlDataAdapter(cmd);

            var dt = new System.Data.DataTable();

            adapter.Fill(dt);

            dataGrid1.ItemsSource = dt.DefaultView;

            conn.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGrid1.SelectedItem as System.Data.DataRowView;

            var conn = new MySqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT COUNT(*) FROM `zeneszek` WHERE `egyuttes`= @egyuttes";

            var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@egyuttes", row["egyuttes"]);

            MessageBox.Show(cmd.ExecuteScalar().ToString());

            conn.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var conn = new MySqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT `album` FROM `albumok` ORDER BY `hossz` DESC LIMIT 1";

            var cmd = new MySqlCommand(sql, conn);

            MessageBox.Show(cmd.ExecuteScalar().ToString());

            conn.Close();
        }
    }
}
