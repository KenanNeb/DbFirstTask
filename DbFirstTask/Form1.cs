using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DbFirstTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("Authors");
            comboBox1.Items.Add("Press");
            comboBox1.Items.Add("Category");
            comboBox1.Items.Add("Theme");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            var options = optionsBuilder.UseSqlServer("Data Source=DESKTOP-7HUNHTF;Integrated Security=True;ApplicationIntent=ReadWrite; Initial Catalog=Library;").Options;

            using (LibraryContext context = new LibraryContext(options))
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    comboBox2.Items.Clear();
                    var authors = context.Authors.ToList();
                    foreach (var author in authors)
                        comboBox2.Items.Add(author.FirstName + " " + author.LastName);
                }

                else if (comboBox1.SelectedIndex == 1)
                {
                    comboBox2.Items.Clear();
                    var presses = context.Presses.ToList();
                    foreach (var press in presses)
                        comboBox2.Items.Add(press.Name);
                }

                else if (comboBox1.SelectedIndex == 2)
                {
                    comboBox2.Items.Clear();
                    var categories = context.Categories.ToList();
                    foreach (var category in categories)
                        comboBox2.Items.Add(category.Name);
                }

                else if (comboBox1.SelectedIndex == 3)
                {
                    comboBox2.Items.Clear();
                    var themes = context.Themes.ToList();
                    foreach (var theme in themes)
                        comboBox2.Items.Add(theme.Name);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new("Data Source=DESKTOP-7HUNHTF;Integrated Security=True;ApplicationIntent=ReadWrite; Initial Catalog=Library;"))
            {
                conn.Open();

                if (comboBox1.SelectedIndex == 0)
                {
                    listView1.Items.Clear();
                    SqlCommand cmd = new SqlCommand(@"SELECT Books.Name, Authors.FirstName, Authors.LastName FROM BOOKS 
                                                      JOIN Authors ON Id_Author = Authors.Id ", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    do
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                                listView1.Items.Add($"{reader[i]} ");
                        }
                    } while (reader.NextResult());
                }

                else if (comboBox1.SelectedIndex == 1)
                {
                    listView1.Items.Clear();
                    SqlCommand cmd = new SqlCommand(@"SELECT Books.Name, Press.Name FROM BOOKS 
                                                      JOIN Press ON Id_Press = Press.Id ", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    do
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                                listView1.Items.Add($"{reader[i]} ");
                        }
                    } while (reader.NextResult());
                }

                else if (comboBox1.SelectedIndex == 2)
                {
                    listView1.Items.Clear();
                    SqlCommand cmd = new SqlCommand(@"SELECT Books.Name, Categories.Name FROM BOOKS 
                                                      JOIN Categories ON Id_Category = Categories.Id ", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    do
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                                listView1.Items.Add($"{reader[i]} ");
                        }
                    } while (reader.NextResult());
                }

                else if (comboBox1.SelectedIndex == 3)
                {
                    listView1.Items.Clear();
                    SqlCommand cmd = new SqlCommand(@"SELECT Books.Name, Themes.Name FROM BOOKS 
                                                      JOIN Themes ON Id_Themes = Themes.Id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    do
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                                listView1.Items.Add($"{reader[i]} ");
                        }
                    } while (reader.NextResult());
                }
            }

        }
    }
}