using Newtonsoft.Json.Linq;
using SimpleCalendar.Abstracts.Dto;
using SimpleCalendar.Abstracts.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalendar.Core.Implementation
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherService()
        {
            _httpClient = new HttpClient();
        }

        #region Public Methods
        public async Task<CalendarData> GetWeather(CalendarData calendarData)
        {
            List<string> weatherData = await Get();

            if (weatherData.Count == 0)
            {
                return calendarData;
            }

            calendarData = FillWeatherInfo(calendarData, weatherData);

            return calendarData;
        }

        #endregion

        #region Private Methods

        private async Task<List<string>> Get()
        {
            var response = await _httpClient.GetAsync("https://api.openweathermap.org/data/2.5/forecast?id=3128759&units=metric&appid=23231431abc9c8d0960d84bd35baca5d");

            if (!response.IsSuccessStatusCode)
            {
                return new List<string>();
            }

            JToken jsonResponse = JToken.Parse(await response.Content.ReadAsStringAsync());

            return ParseResult(jsonResponse["list"]);
        }

        private List<string> ParseResult(JToken openWeatherResponse)
        {
            var dateWeatherInformation = new List<string>();
            openWeatherResponse = openWeatherResponse.First;
            int day = DateTime.Now.Day;

            while (openWeatherResponse != null)
            {
                int responseDay = DateTime.Parse(openWeatherResponse["dt_txt"].ToString()).Day;

                if (responseDay == day || responseDay == day + 1)
                {
                    dateWeatherInformation.Add(FormatInfoFromResponse(openWeatherResponse));
                    // Necesary if API is called after 9 PM
                    day = responseDay;
                    day++;
                }

                openWeatherResponse = openWeatherResponse.Next;
            }

            return dateWeatherInformation;
        }


        private string FormatInfoFromResponse(JToken weatherInfo)
        {
            string weatherInfoFormated = "";

            weatherInfoFormated = weatherInfo["weather"].First["main"].ToString();
            weatherInfoFormated += $"({(int)weatherInfo["main"]["temp"]}ºC)";

            return weatherInfoFormated;
        }

        private CalendarData FillWeatherInfo(CalendarData calendarData, List<string> info)
        {
            int week = calendarData.ActualDayWeek;
            int weekPosition = calendarData.ActualDayWeekPosition;

            foreach (string weatherInfo in info)
            {
                if (weekPosition == 7)
                {
                    if (week >= calendarData.Weeks.Count - 1)
                    {
                        break;
                    }

                    weekPosition = 0;
                    week++;
                }

                calendarData.Weeks[week].Days[weekPosition].Weather = weatherInfo;

                weekPosition++;
            }

            return calendarData;
        }
        #endregion
    }
}
