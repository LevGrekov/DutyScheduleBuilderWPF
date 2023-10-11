using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyScheduleBuilderWPF.Entities
{
    public class Floor
    {
        public int Id { get; set; }
        public ICollection<Kitchen> Kitchens { get; set; }
        public Floor()
        {
            // Инициализация коллекции Kitchens
            Kitchens = new List<Kitchen>();
        }

    }
}
