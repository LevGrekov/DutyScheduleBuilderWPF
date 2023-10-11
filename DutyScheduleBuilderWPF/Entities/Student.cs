using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyScheduleBuilderWPF.Entities
{
    //public class Student
    //{
    //    public int Id { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public int Room { get; set; }
    //    public int QueueForDuty { get; set; }
    //    public bool IsOrderly { get; set; }
    //    public int? KitchenId { get; set; }
    //    public Kitchen Kitchen { get; set; }
    //}

    public class Student : BaseNotifyPropertyChanged
    {
        private int id;
        private string name;
        private int room;
        private int dutyNum;
        private bool isOrderly;
        private int? kitchenId;
        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }
        public int Room
        {
            get => room;
            set => SetField(ref room, value);
        }
        public int DutyNum
        {
            get => dutyNum;
            set => SetField(ref dutyNum, value);
        }
        public bool IsOrderly
        {
            get => isOrderly;
            set => SetField(ref isOrderly, value);
        }
        public int? KitchenId
        {
            get => kitchenId;
            set => SetField(ref kitchenId, value);
        }
        private Kitchen kitchen;
        public Kitchen Kitchen
        {
            get { return kitchen; }
            set { SetField(ref kitchen, value); }
        }
    }
}
