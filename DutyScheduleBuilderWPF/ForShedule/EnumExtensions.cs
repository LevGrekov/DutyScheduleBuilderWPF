using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DutyScheduleBuilderWPF
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
    public enum Month
    {
        [Description("Январь")]
        January = 1,
        [Description("Февраль")]
        February,
        [Description("Март")]
        March,
        [Description("Апрель")]
        April,
        [Description("Май")]
        May,
        [Description("Июнь")]
        June,
        [Description("Июль")]
        July,
        [Description("Август")]
        August,
        [Description("Сентябрь")]
        September,
        [Description("Октябрь")]
        October,
        [Description("Ноябрь")]
        November,
        [Description("Декабрь")]
        December
    }

    public enum WeekDay
    {
        [Description("Воскресенье")]
        Sunday = 0,
        [Description("Понедельник")]
        Monday,
        [Description("Вторник")]
        Tuesday,
        [Description("Среда")]
        Wednesday,
        [Description("Четверг")]
        Thursday,
        [Description("Пятница")]
        Friday,
        [Description("Суббота")]
        Saturday,
    }
}
