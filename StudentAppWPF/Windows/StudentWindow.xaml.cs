using StudentAppWPF.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentAppWPF.Windows
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        StudentMapDataContext context;

        public StudentWindow()
        {
            InitializeComponent();

            context = new StudentMapDataContext();
        }

        void GetStudents()
        {
            //var data = from s in context.Students select s;
            var data = context.Students.ToList();
            StudentsDataGrid.ItemsSource = new ObservableCollection<Student>(data);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetStudents();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var row = new Student();
            row.StudentId = int.Parse(TextBoxID.Text);
            row.Age = int.Parse(TextBoxAge.Text);
            row.FirstName = TextBoxFN.Text;
            row.LastName = TextBoxLN.Text;
            row.AvgGrade = int.Parse(TextBoxGrade.Text);
            row.BirthNumber = TextBoxBN.Text;

            context.Students.InsertOnSubmit(row);
            context.SubmitChanges();

            MessageBox.Show("Added");

            GetStudents();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var row = StudentsDataGrid.SelectedItem as Student;
            if (row != null)
            {
                row.Age = int.Parse(TextBoxAge.Text);
                row.FirstName = TextBoxFN.Text;
                row.LastName = TextBoxLN.Text;
                row.AvgGrade = int.Parse(TextBoxGrade.Text);
                row.BirthNumber = TextBoxBN.Text;

                MessageBox.Show("Saved");
                context.SubmitChanges();

                GetStudents();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this employee?", "Deleting Records", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                var row = StudentsDataGrid.SelectedItem as Student;

                if (row != null)
                {
                    context.Students.DeleteOnSubmit(row);

                    MessageBox.Show("Deleted");
                    context.SubmitChanges();

                    GetStudents();
                }
            }
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = StudentsDataGrid.SelectedItem as Student;

            if (row != null)
            {
                TextBoxID.Text = row.StudentId.ToString();
                TextBoxAge.Text = row.Age.ToString();
                TextBoxFN.Text = row.FirstName;
                TextBoxLN.Text = row.LastName;
                TextBoxGrade.Text = row.AvgGrade.ToString();
                TextBoxBN.Text = row.BirthNumber;
            }
        }
    }
}
