using SimpleCalendar.Abstracts.Dto;
using SimpleCalendar.Abstracts.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalendar.Core.Implementation
{
    public class CalendarService : ICalendarService
    {
        private readonly IOpenWeatherService _openWeatherService;

        public CalendarService(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        public async Task<CalendarData> GetMonthDays(int year, int month)
        {
            //Set if is necessary set the day for weather information
            bool weatherInfoNeeded = DateTime.Now.Month == month && DateTime.Now.Year == year;

            DateTime firstMonthDay = new DateTime(year, month, 1);
            int lastMonthDay = DateTime.DaysInMonth(year, month);

            CalendarData data = new CalendarData() { Weeks = new List<Week>()};
            int weekDay = (int)firstMonthDay.DayOfWeek == 0 ? 7 : (int)firstMonthDay.DayOfWeek;

            Week week = new Week() { Days = new Day[7] };
            for (int i = 1; i <= lastMonthDay; i++)
            {
                if (weekDay > 7)
                {
                    weekDay = 1;
                    data.Weeks.Add(week);
                    week = new Week() { Days = new Day[7] };
                }

                week.Days[weekDay - 1] = new Day() { WeekDay = i };

                if (i == DateTime.Now.Day && weatherInfoNeeded)
                {
                    data.ActualDayWeek = data.Weeks.Count;
                    data.ActualDayWeekPosition = weekDay - 1;
                }

                weekDay++;
            }
            //Add last week
            data.Weeks.Add(week);

            if (weatherInfoNeeded)
            {
                data = await _openWeatherService.GetWeather(data);
            }

            return data;
        }
    }
}
