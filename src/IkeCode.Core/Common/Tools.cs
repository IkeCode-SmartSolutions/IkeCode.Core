using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Xml;

namespace IkeCode.Core
{
    public static class Tools
    {
        #region Private Methods

        private static bool ParseCheck(this object value)
        {
            return string.IsNullOrEmpty(Convert.ToString(value));
        }

        #endregion

        #region Public Methods

        public static string ToString(this object value)
        {
            return value.ToString(string.Empty);
        }
        
        public static string ToString(this object value, string defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            string result;
            try
            {
                result = value.ToString();
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static byte ToByte(this object value)
        {
            return value.ToByte(0);
        }
        
        public static byte ToByte(this object value, byte defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            byte result;
            try
            {
                result = Convert.ToByte(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static short ToInt16(this object value)
        {
            return value.ToInt16(0);
        }
        
        public static short ToInt16(this object value, short defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            short result;
            try
            {
                result = Convert.ToInt16(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static int ToInt32(this object value)
        {
            return value.ToInt32(0);
        }
        
        public static int ToInt32(this object value, int defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            int result;
            try
            {
                result = Convert.ToInt32(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static long ToInt64(this object value)
        {
            return value.ToInt64(0L);
        }
        
        public static long ToInt64(this object value, long defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            long result;
            try
            {
                result = Convert.ToInt64(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static decimal ToDecimal(this object value)
        {
            return value.ToDecimal(0m);
        }
        
        public static decimal ToDecimal(this object value, decimal defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            decimal result;
            try
            {
                result = Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static decimal ToDecimal(this object value, string cultureName)
        {
            return value.ToDecimal(cultureName, 0m);
        }
        
        public static decimal ToDecimal(this object value, string cultureName, decimal defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            decimal result;
            try
            {
                result = Convert.ToDecimal(value, new CultureInfo(cultureName));
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static decimal ToDecimal(this object value, CultureInfo cultureInfo)
        {
            return value.ToDecimal(cultureInfo, 0m);
        }
        
        public static decimal ToDecimal(this object value, CultureInfo cultureInfo, decimal defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            decimal result;
            try
            {
                result = Convert.ToDecimal(value, cultureInfo);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static float ToFloat(this object value)
        {
            return value.ToFloat(0f);
        }
        
        public static float ToFloat(this object value, float defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            float result;
            try
            {
                result = Convert.ToSingle(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static float ToFloat(this object value, CultureInfo cultureInfo)
        {
            return value.ToFloat(cultureInfo, 0f);
        }
        
        public static float ToFloat(this object value, CultureInfo cultureInfo, float defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            float result;
            try
            {
                result = Convert.ToSingle(value, cultureInfo);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static float ToFloat(this object value, string cultureName)
        {
            return value.ToFloat(cultureName, 0f);
        }
        
        public static float ToFloat(this object value, string cultureName, float defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            float result;
            try
            {
                result = Convert.ToSingle(value, new CultureInfo(cultureName));
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static double ToDouble(this object value)
        {
            return value.ToDouble(0.0);
        }
        
        public static double ToDouble(this object value, double defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            double result;
            try
            {
                result = Convert.ToDouble(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static double ToDouble(this object value, string cultureName)
        {
            return value.ToDouble(cultureName, 0.0);
        }
        
        public static double ToDouble(this object value, string cultureName, double defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            double result;
            try
            {
                result = Convert.ToDouble(value, new CultureInfo(cultureName));
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static double ToDouble(this object value, CultureInfo cultureInfo)
        {
            return value.ToDouble(cultureInfo, 0.0);
        }
        
        public static double ToDouble(this object value, CultureInfo cultureInfo, double defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            double result;
            try
            {
                result = Convert.ToDouble(value, cultureInfo);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static bool ToBoolean(this object value)
        {
            return value.ToBoolean(false);
        }
        
        public static bool ToBoolean(this object value, bool defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            if (value.Equals("1"))
            {
                return true;
            }
            bool result;
            try
            {
                result = Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static DateTime ToDateTime(this object value)
        {
            return value.ToDateTime(DateTime.MinValue);
        }
        
        public static DateTime ToDateTime(this object value, DateTime defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            DateTime result;
            try
            {
                result = Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static DateTime ToDateTime(this object value, string cultureName)
        {
            return value.ToDateTime(cultureName, DateTime.MinValue);
        }
        
        public static DateTime ToDateTime(this object value, string cultureName, DateTime defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            DateTime result;
            try
            {
                result = Convert.ToDateTime(value, new CultureInfo(cultureName));
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static DateTime ToDateTime(this object value, CultureInfo cultureInfo)
        {
            return value.ToDateTime(cultureInfo, DateTime.MinValue);
        }
        
        public static DateTime ToDateTime(this object value, CultureInfo cultureInfo, DateTime defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            DateTime result;
            try
            {
                result = Convert.ToDateTime(value, cultureInfo);
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }
        
        public static TimeSpan ToTimeSpan(this object value)
        {
            return value.ToTimeSpan(TimeSpan.MinValue);
        }
        
        public static TimeSpan ToTimeSpan(this object value, TimeSpan defaultValue)
        {
            if (value.ParseCheck())
            {
                return defaultValue;
            }
            TimeSpan result;
            try
            {
                if (value is string)
                {
                    result = TimeSpan.Parse((string)value);
                }
                else
                {
                    result = TimeSpan.FromSeconds(value.ToDouble());
                }
            }
            catch (Exception)
            {
                result = defaultValue;
            }
            return result;
        }

        //public static string ToJsonString(this XmlDocument obj)
        //{
        //    return JsonConvert.SerializeXmlNode(obj);
        //}
        
        public static T FromJsonString<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
   
        #endregion
    }
}