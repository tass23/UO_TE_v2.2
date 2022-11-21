using System;
using System.Text;
using Server;

namespace Server.TimeSystem
{
    public class Support
    {
        #region Enums

        private enum m_MonthList
        {
			Janurary = 1,
			February = 2,
			March = 3,
			April = 4,
			May = 5,
			June = 6,
			July = 7,
			August = 8,
			September = 9,
			October = 10,
			November = 11,
			December = 12
        };

        #endregion

        #region Private Variables

        public const char CodeChar = '$';
        private const char m_ValueChar = '-';

        private const string m_YearCode = "yr";
        private const string m_MonthCode = "mo";
        private const string m_DayCode = "da";
        private const string m_HourCode = "hr";
        private const string m_MinuteCode = "mn";
        private const string m_MoonPhaseCode = "mp";
        private const string m_AmPmCode = "ap";
        private const string m_NthCode = "nth";
        private const string m_SpaceCode = "";

        #endregion

        #region Get Information

        public static object GetValue(object o, string value)
        {
            if (value != null)
            {
                if (value.IndexOf('.') > -1)
                {
                    try
                    {
                        o = Convert.ToDouble(value);
                    }
                    catch { }
                }
                else if (value.ToLower() == "true" || value.ToLower() == "false")
                {
                    try
                    {
                        o = Convert.ToBoolean(value);
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        o = Convert.ToInt32(value);
                    }
                    catch { }
                }
            }

            return o;
        }

        public static string GetTimeFormat(string timeFormat, int minute, int hour, int day, int month, int year, string moonPhaseName)
        {
            bool formatCode = false;

            StringBuilder sb = new StringBuilder();
            StringBuilder formatter = new StringBuilder();

            for (int i = 0; i < timeFormat.Length; i++)
            {
                char c = timeFormat[i];

                if (formatCode && c == CodeChar)
                {
                    formatCode = false;

                    string formattedValue = String.Format("{0}{1}{2}", CodeChar, formatter.ToString(), CodeChar);

                    string[] formatterSplit = formatter.ToString().Split(m_ValueChar);

                    string code = formatterSplit[0];
                    string format = String.Empty;

                    if (formatterSplit.Length == 2)
                    {
                        format = formatterSplit[1];
                    }

                    switch (code)
                    {
                        case m_YearCode:
                            {
                                string theYear = year.ToString();

                                object o = format;

                                o = GetValue(o, format);

                                if (format != String.Empty)
                                {
                                    if (o is int)
                                    {
                                        int value = (int)o;

                                        if (value <= theYear.Length)
                                        {
                                            int difference = theYear.Length - value;

                                            formattedValue = theYear.Remove(0, difference);
                                        }
                                        else
                                        {
                                            formattedValue = theYear;
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theYear;
                                }

                                break;
                            }
                        case m_MonthCode:
                            {
                                string theMonth = month.ToString();

                                object o = format;

                                o = GetValue(o, format);

                                if (format != String.Empty)
                                {
                                    if (o is int)
                                    {
                                        int value = (int)o;

                                        int totalMonths = Enum.GetNames(typeof(m_MonthList)).Length;

                                        if (System.UseRealTime)
                                        {
                                            switch (value)
                                            {
                                                case 0:
                                                    {
                                                        formattedValue = DateTime.Now.ToString("MMMM");

                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        formattedValue = DateTime.Now.ToString("MMM");

                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            if (System.MonthsPerYear == totalMonths)
                                            {
                                                switch (value)
                                                {
                                                    case 0:
                                                        {
                                                            formattedValue = ((m_MonthList)month).ToString();

                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            string monthValue = ((m_MonthList)month).ToString();

                                                            formattedValue = monthValue.Remove(3, monthValue.Length - 3);

                                                            break;
                                                        }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theMonth;
                                }

                                break;
                            }
                        case m_DayCode:
                            {
                                string theDay = day.ToString();

                                object o = format;

                                o = GetValue(o, format);

                                if (format != String.Empty)
                                {
                                    if (o is int)
                                    {
                                        int value = (int)o;

                                        if (value >= 2)
                                        {
                                            StringBuilder daySB = new StringBuilder();

                                            for (int j = theDay.Length; j < value; j++)
                                            {
                                                daySB.Append("0");
                                            }

                                            daySB.Append(theDay);

                                            formattedValue = daySB.ToString();
                                        }
                                        else
                                        {
                                            formattedValue = theDay;
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theDay;
                                }

                                break;
                            }
                        case m_HourCode:
                            {
                                string theHour = hour.ToString();

                                object o = format;

                                o = GetValue(o, format);

                                if (format != String.Empty)
                                {
                                    if (o is int)
                                    {
                                        int value = (int)o;

                                        if (value >= 2)
                                        {
                                            StringBuilder hourSB = new StringBuilder();

                                            for (int j = theHour.Length; j < value; j++)
                                            {
                                                hourSB.Append("0");
                                            }

                                            hourSB.Append(theHour);

                                            formattedValue = hourSB.ToString();
                                        }
                                        else
                                        {
                                            formattedValue = theHour;
                                        }
                                    }
                                    else if (o is string)
                                    {
                                        string value = (string)o;

                                        if (value == m_AmPmCode)
                                        {
                                            int totalHours = System.HoursPerDay;

                                            int changePoint = Convert.ToInt32((double)totalHours / 2.0);

                                            if (hour <= changePoint)
                                            {
                                                if (hour == 0)
                                                {
                                                    formattedValue = changePoint.ToString();
                                                }
                                                else
                                                {
                                                    formattedValue = hour.ToString();
                                                }
                                            }
                                            else
                                            {
                                                formattedValue = Convert.ToString(hour - changePoint);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theHour;
                                }

                                break;
                            }
                        case m_MinuteCode:
                            {
                                string theMinute = minute.ToString();

                                object o = format;

                                o = GetValue(o, format);

                                if (format != String.Empty)
                                {
                                    if (o is int)
                                    {
                                        int value = (int)o;

                                        if (value >= 2)
                                        {
                                            StringBuilder minuteSB = new StringBuilder();

                                            for (int j = theMinute.Length; j < value; j++)
                                            {
                                                minuteSB.Append("0");
                                            }

                                            minuteSB.Append(theMinute);

                                            formattedValue = minuteSB.ToString();
                                        }
                                        else
                                        {
                                            formattedValue = theMinute;
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theMinute;
                                }

                                break;
                            }
                        case m_MoonPhaseCode:
                            {
                                if (format == String.Empty)
                                {
                                    formattedValue = moonPhaseName;
                                }

                                break;
                            }
                        case m_AmPmCode:
                            {
                                if (format == String.Empty)
                                {
                                    int totalHours = System.HoursPerDay;

                                    int changePoint = Convert.ToInt32((double)totalHours / 2.0);

                                    if (hour < changePoint)
                                    {
                                        formattedValue = "AM";
                                    }
                                    else
                                    {
                                        formattedValue = "PM";
                                    }
                                }

                                break;
                            }
                        case m_NthCode:
                            {
                                if (format != String.Empty)
                                {
                                    int value = 0;

                                    switch (format)
                                    {
                                        case "d":
                                            {
                                                value = day;

                                                break;
                                            }
                                        case "m":
                                            {
                                                value = month;

                                                break;
                                            }
                                    }

                                    switch (value)
                                    {
                                        case 11:
                                            {
                                                formattedValue = "th";

                                                break;
                                            }
                                        case 12:
                                            {
                                                formattedValue = "th";

                                                break;
                                            }
                                        case 13:
                                            {
                                                formattedValue = "th";

                                                break;
                                            }
                                        default:
                                            {
                                                string nth = value.ToString();

                                                if (nth.EndsWith("1"))
                                                {
                                                    formattedValue = "st";
                                                }
                                                else if (nth.EndsWith("2"))
                                                {
                                                    formattedValue = "nd";
                                                }
                                                else if (nth.EndsWith("3"))
                                                {
                                                    formattedValue = "rd";
                                                }
                                                else if (value != 0)
                                                {
                                                    formattedValue = "th";
                                                }

                                                break;
                                            }
                                    }
                                }

                                break;
                            }
                        case m_SpaceCode:
                            {
                                formattedValue = " ";

                                break;
                            }
                    }

                    sb.Append(formattedValue);

                    formatter = new StringBuilder();
                }
                else if (!formatCode && c == CodeChar)
                {
                    formatCode = true;
                }

                if (!formatCode)
                {
                    if (c != CodeChar)
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    if (c != CodeChar)
                    {
                        formatter.Append(c);
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Message Formatting

        public static string ErrorMessageFormatter(System.Variable variable, object o, string minValue, string maxValue)
        {
            string value = null;

            if (o is int)
            {
                value = Convert.ToString((int)o);
            }
            else if (o is double)
            {
                value = Convert.ToString((double)o);
            }

            return String.Format("The specified value {0} for [{1}] must be within range from {2} to {3}.", value, variable, minValue, maxValue);
        }

        public static string ErrorMessageFormatter(System.Variable variable, object o, string minValue, string maxValue, Type typeExpected)
        {
            string typeName = "null";

            if (o != null)
            {
                typeName = o.GetType().Name;
            }

            return String.Format("[{0}] cannot have a value of {1} type.  Please specify a value of {2} type between {3} and {4}.", variable, typeName, typeExpected.Name, minValue, maxValue);
        }

        public static string ErrorMessageFormatter(System.Variable variable, object o, string minValueLowRange, string maxValueLowRange, string minValueHighRange, string maxValueHighRange)
        {
            string value = null;

            if (o is int)
            {
                value = Convert.ToString((int)o);
            }
            else if (o is double)
            {
                value = Convert.ToString((double)o);
            }

            return String.Format("The specified value {0} for [{1}] must be within range from {2} to {3} or {4} to {5}.", value, variable, minValueLowRange, maxValueLowRange, minValueHighRange, maxValueHighRange);
        }

        public static string ErrorMessageFormatter(System.Variable variable, object o, Type typeExpected)
        {
            string typeName = "null";

            if (o != null)
            {
                typeName = o.GetType().Name;
            }

            return String.Format("[{0}] cannot have a value of {1} type.  Please specify a value of {2} type.", variable, typeName, typeExpected.Name);
        }

        public static string VariableMessageFormatter(string variableName, string value)
        {
            return VariableMessageFormatter(variableName, value, false);
        }

        public static string VariableMessageFormatter(string variableName, string value, bool append)
        {
            if (append)
            {
                return String.Format("[{0}] += {1}", variableName, value);
            }
            else
            {
                return String.Format("[{0}] = {1}", variableName, value);
            }
        }

        #endregion
    }
}