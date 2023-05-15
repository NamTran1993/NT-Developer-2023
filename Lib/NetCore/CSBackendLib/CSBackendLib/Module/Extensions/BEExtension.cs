using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;

namespace BEBackendLib.Module.Extensions
{
    public static class BEExtension
    {
        #region String => Int; Int => String
        public static int BEToInt32(this string obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static long BEToLong(this string obj)
        {
            try
            {
                return long.Parse(obj.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string BEToString(this int obj)
        {
            try
            {
                return obj.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int[]? BEToArrayInt32(this string[] obj)
        {
            try
            {
                return Array.ConvertAll(obj, s => int.Parse(s));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string[]? BEToArrayString(this int[] obj)
        {
            try
            {
                return Array.ConvertAll(obj, s => s.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region JSON
        public static string BEObjectToJson(this object obj)
        {
            try
            {
                if (obj is not null)
                    return JsonConvert.SerializeObject(obj);
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T? BEJsonToObject<T>(this string obj)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion



        #region XML
        public static T? BEXMLToObject<T>(this string xml)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (var stringReader = new StringReader(xml))
                {
                    return (T?)xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static string BEObjectToXML(this object obj)
        {
            string res = string.Empty;
            try
            {
                if (obj is not null)
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    var sb = new StringBuilder();
                    using (TextWriter writer = new StringWriter(sb))
                    {
                        serializer.Serialize(writer, obj);
                    }

                    res = sb.ToString();
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region Distinct
        public static IEnumerable<T> BEDistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            try
            {
                return items.GroupBy(property).Select(x => x.First());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region IMAGE
        public static byte[]? BEConvertbase64ToByteArray(this string base64)
        {
            try
            {
                if (!string.IsNullOrEmpty(base64))
                    return Convert.FromBase64String(base64);
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region DIFF
        public static IEnumerable<T> BEDequeue<T>(this Queue<T> queues, int count)
        {
            for (int i = 0; i < count && queues.Count > 0; i++)
            {
                yield return queues.Dequeue();
            }
        }

        public static string BERemoveSpaces(this string obj)
        {
            string res = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    obj = obj.Trim();
                    res = obj.Replace(" ", "");
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

