using DutyScheduleBuilderWPF.Entities;
using DutyScheduleBuilderWPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

namespace DutyScheduleBuilderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateFloorsAndKitchens();
            ChangeDataInGrid(202);
            using (var db = new ApplicationContext())
            {
                if (db.Students.Where(s => s.KitchenId == 202).Count() == 0)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        db.Students.Add(new Student { Name = i.ToString() + "SDASdsa", KitchenId = 202, DutyNum = i, IsOrderly = false, Room = 202 });
                    }
                    db.SaveChanges();
                }
                
            }
        }

        private void GenerateFloorsAndKitchens()
        {
            if (myMenu != null)
            {
                MenuItem Main = new MenuItem();
                Main.Header = "Этажи";

                using(var db = new ApplicationContext())
                {
                    var floorsWithKitchens = db.Floors.Include(f => f.Kitchens).ToList();
                    foreach (var floor in floorsWithKitchens)
                    {
                        // Создаем элемент меню для этажа
                        MenuItem floorMenuItem = new MenuItem();
                        floorMenuItem.Header = $"Этаж №{floor.Id}";

                        foreach (var kitchen in floor.Kitchens)
                        {
                            // Создаем элемент меню для кухни на этаже
                            MenuItem kitchenMenuItem = new MenuItem();
                            kitchenMenuItem.Header = $"{kitchen.Id}a";
                            kitchenMenuItem.Click += (sender, e) => ChangeDataInGrid(kitchen.Id);
                            floorMenuItem.Items.Add(kitchenMenuItem);
                        }

                        // Добавляем элемент меню для этажа в Menu
                        Main.Items.Add(floorMenuItem);
                    }
                    myMenu.Items.Add(Main);
                }
            }
        }

        private void ChangeDataInGrid(int kitchenId)
        {
            using (var db = new ApplicationContext())
            {
                //var studentsInKitchen = db.Students
                //.Where(s => s.KitchenId == kitchenId);

                //if (studentsInKitchen != null)
                //{
                //    var studentList = new ObservableCollection<Student>(studentsInKitchen);
                //    MainWindowViewModel? viewModel = this.DataContext as MainWindowViewModel;

                //    if (viewModel != null)
                //    {
                //        viewModel.Students = studentList;
                //        dataGrid.ItemsSource = viewModel.Students;
                //    }
                //}
                Expression<Func<Student, bool>> filterExpression = s => s.KitchenId == kitchenId;
                dataGrid.ItemsSource = db.GetFilteredStudents(filterExpression);

                db.Students.Local.ToObservableCollection();

            }
        }

        private void GenerateSchedule_Click(object sender, RoutedEventArgs e)
        {
            //using (var db = new ApplicationContext())
            //{
            //    DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            //    MainWindowViewModel? viewModel = this.DataContext as MainWindowViewModel;
            //    var kitchenWithStudents = db.Kitchens
            //        .Include(k => k.Students)
            //        .FirstOrDefault(k => k.Id == viewModel.kitchenId);
            //    var a = new Schedule(kitchenWithStudents, today, WeekDay.Wednesday, 1);
            //    a.SaveFile();
            //}
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext()) { db.SaveChanges(); }
        }

        private void dataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            //// Создайте новый объект для новой записи
            //Student newStudent = new Student();
            //MainWindowViewModel? viewModel = this.DataContext as MainWindowViewModel;
            //// Установите значения полей, которые хотите заполнить по умолчанию
            //newStudent.Name = null;
            //newStudent.KitchenId = viewModel.kitchenId ; // Пример значения по умолчанию
            //newStudent.Room = 0;
            //newStudent.DutyNum = 10; // Пример значения по умолчанию


            //// Присвойте новую запись объекту e.NewItem
            //e.NewItem = newStudent;
        }
    }
}
