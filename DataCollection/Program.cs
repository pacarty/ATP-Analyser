using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DataCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning collecting data");

            List<string> dates = GetWeeks();
            dates.Reverse();

            WriteToFile(dates);

            const int indexToCheck = 2017;
            CheckIndex(dates, indexToCheck);

            Console.WriteLine("Finished collecting data");
        }

        public static void CheckIndex(List<string> dates, int index)
        {
            Console.WriteLine("Date at index " + index + ": " + dates[index]);
            Console.WriteLine("Total weeks: " + dates.Count());
        }

        public static void WriteToFile(List<string> dates)
        {
            const string separator = "xxxSEPARATORxxx";
            StringBuilder sb = new StringBuilder();

            const int startIndex = 0;
            const int endIndex = 30;

            for (int i = startIndex; i < endIndex; i++)
            {
                string s = DoWeek(dates[i]);
                sb.Append(separator);
                sb.Append(dates[i]);
                sb.Append(s);

                Console.WriteLine("Index " + i + " completed");
            }

            string aString = sb.ToString();

            string dPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dPath2 = Path.Combine(dPath, "AtpAnalyser");

            string dPath3 = dPath2 + @"\tennisinfo.txt";


            Directory.CreateDirectory(dPath2);
            
            //File.WriteAllText(dPath3, aString);
            File.AppendAllText(dPath3, aString);
        }

        public static List<string> GetWeeks()
        {
            List<string> dates = new List<string>();

            string info = "";

            string url = "https://www.atptour.com/en/rankings/singles?rankDate=2020-03-16&rankRange=0-100";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            info = sr.ReadToEnd();

            int currentIndex = 0;
            bool cont = true;

            currentIndex = info.IndexOf("dropdown-default-label", currentIndex);

            while (cont)
            {
                currentIndex = info.IndexOf("data-value", currentIndex);
                currentIndex += 12;

                if (!Char.IsDigit(info[currentIndex + 9]))
                {
                    cont = false;
                }
                else
                {
                    StringBuilder theDate = new StringBuilder();

                    for (int i = currentIndex; i < currentIndex + 10; i++)
                    {
                        theDate.Append(info[i]);
                    }

                    dates.Add(theDate.ToString());
                }
            }

            return dates;
        }

        public static string DoWeek(string week)
        {
            string info = "";

            string url = "https://www.atptour.com/en/rankings/singles?rankDate=" + week + "&rankRange=0-100";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            info = sr.ReadToEnd();

            return info;
        }
    }
}
