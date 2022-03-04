using System;
using System.Collections.Generic;

namespace Lesson_1
{
    public class ValuesHolder
    {
        private SortedList<DateTime, WeatherForecast> _values = new SortedList<DateTime, WeatherForecast>();
        public SortedList<DateTime, WeatherForecast> Values
        { 
            get => _values; 
            set => _values = value; 
        }
    }
}