using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace SixNet_StringUtils
{
    public static class Utils
    {
        public static bool CheckForSQL(string s)
        {
            if (s.ToUpper().Contains("DROP TABLE")) return true;
            if (s.ToUpper().Contains("SELECT *")) return true;
            if (s.ToUpper().Contains("DELETE FROM")) return true;
            return false;
        }

        public static string ToSQL(string s)
        {
            string t = s;
            t = t.Replace("'", "''");
            return t;
        }

        public static string FromSQL(string s)
        {
            string t = s;
            t = t.Replace("''", "'");
            return t;
        }

        public static string SPC(int spaces)
        {
            string s = "";
            for (int i = 0; i < spaces; i++)
            {
                s = s + " ";
            }
            return s;
        }

        public static string Repeat(char ch,int spaces)
        {
            string s = "";
            for (int i = 0; i < spaces; i++)
            {
                s = s + ch;
            }
            return s;
        }

        public static string Center(string stringtocenter, int columns)
        {
            string s = "";
            int center = columns/2;
            int stringlen = stringtocenter.Length;
            s = SPC(center - (stringlen / 2)) + stringtocenter + SPC(columns - (stringlen / 2));
            if (s.Length > columns) s = s.Substring(0, columns);
            return s;
        }
        public static string Clip(string stringtoclip, int maxlen, bool padend)
        {
            if (stringtoclip == null) stringtoclip = "";
            string s = "";
            //ALready clipped?
            if (stringtoclip.Length <= maxlen)
            {
                if (!padend)
                {
                    s = stringtoclip;
                }
                else
                {
                    s = stringtoclip + SPC(maxlen - stringtoclip.Length);
                }
            }
            else
            {
                    s = stringtoclip.Substring(0, maxlen);
            }
            //Double check length
            if (s.Length > maxlen) s = s.Substring(0, maxlen);
            return s;
        }

        public static List<string> Split(string str, int chunkSize)
        {
            int index = 0;
            List<String> ss = new List<String>();
            if (str.Length <= chunkSize)
            {
                ss.Add(str);
                return ss;
            }
            else
            {
                while (index < str.Length)
                {
                    string t = str.Substring(index, chunkSize);
                    ss.Add(t);
                    index += chunkSize;
                    if (index + chunkSize > str.Length)
                    {
                        string u = str.Substring(index, str.Length - index);
                        ss.Add(u);
                        index = str.Length;
                    }
                    
                }
            }
            return ss;

        }

        public static string Reassemble(string[] parts, int low, int high)
        {
            string result = "";
            for (int i = low; i <= high; i++)
            {
                result = result + parts[i] + ((i < high)?" ":"");
            }
            return result;
        }

        public static String UTF8ByteArrayToString(Byte[] ArrBytes)
        { return new UTF8Encoding().GetString(ArrBytes); }

        public static Byte[] StringToUTF8ByteArray(String XmlString)
        { return new UTF8Encoding().GetBytes(XmlString); }

        public static string SerializeToXmlString<T>(T ToSerialize)
        {
            string xmlstream = String.Empty;

            using (MemoryStream memstream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlTextWriter xmlWriter = new XmlTextWriter(memstream, Encoding.UTF8);

                xmlSerializer.Serialize(xmlWriter, ToSerialize);
                xmlstream = UTF8ByteArrayToString(((MemoryStream)xmlWriter.BaseStream).ToArray());
            }

            return xmlstream;
        }

        public static T DeserializeXmlString<T>(string XmlString)
        {
            T tempObject = default(T);

            using (MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(XmlString)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                tempObject = (T)xs.Deserialize(memoryStream);
            }

            return tempObject;
        } 


    }
}
