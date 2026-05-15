using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr16_v1_Savitsin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Student> studList = new List<Student>();
        private ICollectionView _view;
        public MainWindow()
        {
            InitializeComponent();

            CollectionViewSource groupedSource = (CollectionViewSource)this.FindResource("GroupedItems");
            if (groupedSource != null)
            {
                groupedSource.Source = studList;
                _view = groupedSource.View;
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e) //Кнопка добавить в список
        {
            string input = txtInput.Text.Trim();
            string groupText = comboBoxGroup.Text;
            string groupSpeciality = comboBoxSpeciality.Text;
            string groupSubject = comboBoxSubject.Text;
            if (!int.TryParse(comboBoxGrade.Text, out int grade))
            {
                grade = 3;
            }
            lst_box.FontSize = 20;
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(groupText) && !string.IsNullOrEmpty(groupSpeciality))
            {
                Student student = new Student("", "", "", "", 0);
                student.Name = input;
                student.Group = groupText;
                student.Speciality = groupSpeciality;
                student.Subject = groupSubject;
                student.Grade = grade;

                var list = from temp in studList where temp.Name.ToLower() == input.ToLower() && temp.Group.ToLower() == groupText.ToLower() 
                           && temp.Speciality.ToLower() == groupSpeciality.ToLower() && temp.Subject.ToLower() == groupSubject.ToLower() && temp.Grade == grade
                           select temp;

                if (list.Count() == 0)
                {
                    studList.Add(student);

                    if (_view != null)
                    {
                        _view.Refresh(); 
                    }

                    txtInput.Clear();
                    comboBoxGroup.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Такой студент уже есть", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Delete_lst(object sender, RoutedEventArgs e) //Кнопка очистить список
        {
            studList.Clear();
            _view?.Refresh();
        }
        private void Sort_Click(object sender, RoutedEventArgs e) //Кнопка сортировать список
        {
            if (_view == null) return;

            _view.SortDescriptions.Clear();

            if (comboBoxSort.SelectedIndex == 0)
            {
                _view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }
            else if (comboBoxSort.SelectedIndex == 1)
            {
                _view.SortDescriptions.Add(new SortDescription("Group", ListSortDirection.Ascending));
            }

            _view.Refresh();
        }
        private void Filter_Click(object sender, RoutedEventArgs e) //Кнопка фильтрация списка
        {
            if (_view == null) return;

            if (comboboxFilter.SelectedIndex == 0)
            {
                _view.Filter = item =>
                {
                    Student student = item as Student;
                    return student != null && (student.Name.StartsWith("А") || student.Name.StartsWith("а"));
                };
            }
            else if (comboboxFilter.SelectedIndex == 1)
            {
                _view.Filter = item =>
                {
                    Student student = item as Student;
                    return student != null && (student.Group.StartsWith("П") || student.Group.StartsWith("п"));
                };
            }
            else
            {
                _view.Filter = null;  
            }

            _view.Refresh();
        }
        private void DelSelect_Click(object sender, RoutedEventArgs e) // Кнопка удалить выбранный элемент
        {
            if (lst_box.SelectedItem != null)
            {
                Student selectedStudent = lst_box.SelectedItem as Student;
                if (selectedStudent != null)
                {
                    studList.Remove(selectedStudent);
                    _view?.Refresh();  
                }
            }
        }

        private void Category_Click(object sender, RoutedEventArgs e) //Категоризация специальностей
        {
            CollectionViewSource groupedSource = (CollectionViewSource)this.FindResource("GroupedItems");

            if (groupedSource != null && groupedSource.View != null)
            {
                ICollectionView view = groupedSource.View;

                if (view.GroupDescriptions.Count == 0)
                {
                    view.GroupDescriptions.Add(new PropertyGroupDescription("Speciality"));
                }
                else
                {
                    view.GroupDescriptions.Clear();
                }
            }

            _view.Refresh();
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e) //Сбросить фильтр
        {
            if (_view != null)
            {
                _view.Filter = null; 
                _view.Refresh();
                comboboxFilter.SelectedIndex = -1;
            }
        }
    }
}
