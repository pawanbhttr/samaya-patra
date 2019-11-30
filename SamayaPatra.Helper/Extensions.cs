using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamayaPatra.Helper
{
    public static class Extensions
    {
        public static bool IsNull(this object _obj)
        {
            try
            {
                return string.IsNullOrWhiteSpace(_obj.ToText());
            }
            catch
            {
                return true;
            }
        }
        public static string ToText(this object _obj)
        {
            try
            {
                return _obj.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int ToInt32(this object _obj)
        {
            
            try
            {
                return Convert.ToInt32(_obj.ToText());
            }
            catch
            {
                return 0;
            }
        }

        public static bool ToBoolean(this object _obj)
        {

            try
            {
                return Convert.ToBoolean(_obj);
            }
            catch
            {
                return false;
            }
        }

        public static decimal ToDecimal(this object _obj)
        {

            try
            {
                return Convert.ToDecimal(_obj.ToText());
            }
            catch
            {
                return 0;
            }
        }

        public static double ToDouble(this object _obj)
        {

            try
            {
                return Convert.ToDouble(_obj.ToText());
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime ToDateTime(this object _obj)
        {

            try
            {
                return Convert.ToDateTime(_obj.ToText());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static string ToJson(this object _obj)
        {
            try
            {
                return JsonConvert.SerializeObject(_obj);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
