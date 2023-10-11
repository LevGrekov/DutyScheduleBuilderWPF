using DutyScheduleBuilderWPF.Entities;
using MathNet.Numerics.Distributions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DutyScheduleBuilderWPF.ViewModels
{
    class MainWindowViewModel : BaseNotifyPropertyChanged
    {
        public ObservableCollection<Student> Students { get; set; }

        private Student student;
        public int? kitchenId => Students[0].KitchenId;
        public Student selectedStudent
        {
            get => student;
            set
            {
                SetField(ref student, value);
            }

        }

        public BaseCommand AddCommand { get; set; }
        public BaseCommand DelCommand { get; set; }


        public MainWindowViewModel()
        {
            AddCommand = new BaseCommand(_ => true, _ => Add());
            DelCommand = new BaseCommand(_ => true, _ => Del());
        }

        private void Add()
        {
            Students.Add(selectedStudent);
            using (var db = new ApplicationContext())
            {
                db.Students.Add(selectedStudent);
                db.SaveChanges();
            }
        }

        private void Del()
        {
            using (var db = new ApplicationContext())
            {
                var studentToDelete = db.Students.SingleOrDefault(s => s.Id == selectedStudent.Id);

                if (studentToDelete != null)
                {
                    foreach (var student in Students)
                    {
                        if (student.DutyNum > selectedStudent.DutyNum)
                        {
                            student.DutyNum--;
                        }
                    }
                    foreach (var student in db.Students.Where(s => s.KitchenId == kitchenId))
                    {
                        if (student.DutyNum > selectedStudent.DutyNum)
                        {
                            student.DutyNum--;
                        }
                    }
                    Students.Remove(selectedStudent);
                    db.Students.Remove(studentToDelete);

                    db.SaveChanges();
                }
            }

            
        }
    }
}
