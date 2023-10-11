using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyScheduleBuilderWPF.Entities
{
    public class Kitchen
    {
        public int Id { get; set; }
        public int FloorId { get; set; }

        public Floor Floor { get; set; }
        public ObservableCollection<Student> Students { get; set; }

        public Kitchen()
        {
            Students = new ObservableCollection<Student>();
        }
    }
}
