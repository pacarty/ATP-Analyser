using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace TenFinal.Data
{
    public class PlayerInit
    {
        public static void Init(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
            {
                return;
            }

            AddExamplePlayer(context);

            List<string> weekInfo = GetList();

            int startIndex = 0;
            int endIndex = weekInfo.Count;

            for (int i = startIndex; i < endIndex; i++)
            {
                DoWeek(weekInfo[i], context);
            }
        }

        public static List<string> GetList()
        {
            const string separator = "xxxSEPARATORxxx";

            string dPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dPath2 = Path.Combine(dPath, "AtpAnalyser");
            string dPath3 = dPath2 + @"\tennisinfo.txt";

            string allInfo = File.ReadAllText(dPath3);

            List<string> allWeeks = new List<string>();

            allWeeks = allInfo.Split(separator).ToList();
            allWeeks.RemoveAt(0);

            return allWeeks;
        }

        public static void AddExamplePlayer(DataContext context)
        {

            context.Players.Add(new Player
            {
                PlayerId = "exampleId",
                Name = "Example Player"
            });

            context.RankDates.Add(new RankDate
            {
                RankingNumber = 1,
                Date = DateTime.Parse("1970-01-01"),
                PlayerId = "exampleId"
            });

            context.RankDates.Add(new RankDate
            {
                RankingNumber = 1,
                Date = DateTime.Parse("2070-12-31"),
                PlayerId = "exampleId"
            });

            context.SaveChanges();
        }

        public static void DoWeek(string week, DataContext context)
        {
            string info = week;

            StringBuilder currentDate = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                currentDate.Append(info[i]);
            }

            StringBuilder rankStringBuilder = new StringBuilder();
            StringBuilder codeStringBuilder = new StringBuilder();
            StringBuilder nameStringBuilder = new StringBuilder();

            int currentIndex = 0;
            bool cont = true;

            while (cont)
            {
                currentIndex = info.IndexOf("rank-cell", currentIndex);

                if (currentIndex == -1)
                {
                    cont = false;
                }
                else
                {
                    currentIndex += 11;

                    while (!info[currentIndex].Equals('<'))
                    {
                        if (Char.IsDigit(info[currentIndex]))
                        {
                            rankStringBuilder.Append(info[currentIndex]);
                        }
                        currentIndex++;
                    }

                    int playerRanking = int.Parse(rankStringBuilder.ToString());
                    rankStringBuilder.Clear();

                    currentIndex = info.IndexOf("/overview", currentIndex) - 1;

                    while (!info[currentIndex].Equals('/'))
                    {
                        codeStringBuilder.Append(info[currentIndex]);
                        currentIndex--;
                    }

                    char[] charArray = codeStringBuilder.ToString().ToCharArray();
                    Array.Reverse(charArray);

                    string pCode = new string(charArray);
                    codeStringBuilder.Clear();

                    currentIndex = info.IndexOf("label", currentIndex) + 7;

                    while (!info[currentIndex].Equals('"'))
                    {
                        nameStringBuilder.Append(info[currentIndex]);
                        currentIndex++;
                    }

                    string playerName = nameStringBuilder.ToString();
                    nameStringBuilder.Clear();

                    if (context.Players.Find(pCode) == null)
                    {
                        context.Players.Add(new Player
                        {
                            PlayerId = pCode,
                            Name = playerName
                        });
                    }

                    context.RankDates.Add(new RankDate
                    {
                        RankingNumber = playerRanking,
                        Date = DateTime.Parse(currentDate.ToString()),
                        PlayerId = pCode
                    });

                    context.SaveChanges();

                }
            }

        }
    }
}
