using SimpleCalendar.Abstracts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalendar.Abstracts.Interface
{
    public interface IOpenWeatherService
    {
        /// <summary>
        /// Get the weather for the next 5 days.
        /// </summary>
        /// <param name="calendarData">Calendar month days</param>
        /// <returns>Fullfill Calendar month</returns>
        Task<CalendarData> GetWeather(CalendarData calendarData);
    }
}
