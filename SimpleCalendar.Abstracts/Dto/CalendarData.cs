using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCalendar.Abstracts.Dto
{
    public class CalendarData
    {
        public List<Week> Weeks { get; set; }
        public int ActualDayWeek { get; set; }
        public int ActualDayWeekPosition { get; set; }
    }

    public class Week
    {
        public Day[] Days { get; set; }
    }

    public class Day
    {
        public int WeekDay { get; set; }
        public string Weather { get; set; }
    }
}
