using SimpleCalendar.Abstracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalendar.Abstracts.Interface
{
    public interface ICalendarService
    {
        /// <summary>
        /// Generates the correct days and position (Monday, tuesday, etc..) for the given
        /// year and month
        /// </summary>
        /// <param name="year">Desire Year</param>
        /// <param name="month">Desire Month</param>
        /// <returns></returns>
        Task<CalendarData> GetMonthDays(int year, int month);
    }
}
