﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ProjectMato.iOS.Model;

namespace ProjectMato.iOS.Helper
{
    public class LRCSer
    {
        public static Gecime_Lyric GecimeLyricDeserializer(string jsonStr)
        {
            Gecime_Lyric result = new Gecime_Lyric();
            if (!string.IsNullOrEmpty(jsonStr))
            {
                result = JsonConvert.DeserializeObject<Gecime_Lyric>(jsonStr);
                return result;
            }
            else
            {
                return null;
            }
        }

        public static Gecime_Artist GecimeArtistDeserializer(string jsonStr)
        {
            Gecime_Artist result = new Gecime_Artist();
            if (!string.IsNullOrEmpty(jsonStr))
            {
                result = JsonConvert.DeserializeObject<Gecime_Artist>(jsonStr);
                return result;
            }
            else
            {
                return null;
            }
        }

        public static LRCItem InitLrc(string lrcStr)
        {
            LRCItem lrc = new LRCItem();
            //if (lrcStr.StartsWith("[ti:"))
            //{
            //    lrc.Title = SplitInfo(lrcStr);
            //}
            //else if (lrcStr.StartsWith("[ar:"))
            //{
            //    lrc.Artist = SplitInfo(lrcStr);
            //}
            //else if (lrcStr.StartsWith("[al:"))
            //{
            //    lrc.Album = SplitInfo(lrcStr);
            //}
            //else if (lrcStr.StartsWith("[by:"))
            //{
            //    lrc.LrcBy = SplitInfo(lrcStr);
            //}
            //else if (lrcStr.StartsWith("[offset:"))
            //{
            //    lrc.Offset = SplitInfo(lrcStr);
            //}
            //else
            //{
            //    Regex regex = new Regex(@"\[([0-9.:]*)\]+(.*)", RegexOptions.Compiled);
            //    MatchCollection mc = regex.Matches(lrcStr);
            //    double time = TimeSpan.Parse("00:" + mc[0].Groups[1].Value).TotalSeconds;
            //    string word = mc[0].Groups[2].Value;
            //    lrc.LrcWord.Add(time, word);
            //}

            lrcStr = lrcStr.Replace("\r", "\n");
            String[] av = lrcStr.Split('\n');
            int i;
            for (i = 0; i < av.GetLength(0); i++)
            {
                if (av[i] != "")
                {
                    MatchCollection matchTime = Regex.Matches(av[i], @"(?<=\[)\d{2}:\d{2}\.\d{2}(?=\])");
                    Match matchContent = Regex.Match(av[i], @"(?<=\])(?!\[).*");

                    if (!string.IsNullOrEmpty(matchContent.ToString()))
                    {
                        var id = 0;
                        foreach (var item in matchTime)
                        {
                            System.Diagnostics.Debug.WriteLine(item.ToString());
                            try
                            {
                                lrc.LrcWords.Add(new LrcWord()
                                {
                                    LrcWordId = id++,
                                    Time = TimeSpan.Parse(item.ToString().Split('.')[0]),
                                    //Time = Math.Round(stringToInterval(item.ToString()), 1),
                                    Content = matchContent.ToString()
                                });

                            }
                            catch (Exception)
                            {

                                lrc.LrcWords.Add(new LrcWord()
                                {
                                    LrcWordId = id++,
                                    Time = default(TimeSpan),
                                    //Time = Math.Round(stringToInterval(item.ToString()), 1),
                                    Content = matchContent.ToString()
                                });
                            }
                        }
                    }
                    lrc.LrcWords = (from a in lrc.LrcWords
                                    orderby a.Time ascending
                                    select a).ToList();

                }
            }



            return lrc;
        }



        /// <summary>
        /// 处理信息(私有方法)
        /// </summary>
        /// <param name="line"></param>
        /// <returns>返回基础信息</returns>
        public static string SplitInfo(string line)
        {
            return line.Substring(line.IndexOf(":") + 1).TrimEnd(']');
        }
        /// <summary>
        /// String转换Interval
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static double stringToInterval(String str)
        {
            try
            {
                double min = double.Parse(str.Split(':').GetValue(0).ToString());
                double sec = double.Parse(str.Split(':').GetValue(1).ToString());
                return min * 60.0 + sec;
            }
            catch
            {
                return uint.MaxValue;
            }
        }

        /// <summary>
        /// interval转换String
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        private static String intervalToString(double interval)
        {
            int min;
            float sec;
            min = (int)interval / 60;
            sec = (float)(interval - (float)min * 60.0);
            String smin = String.Format("{0:d2}", min);
            String ssec;
            ssec = String.Format("{0:00.0}", sec);
            return smin + ":" + ssec;
        }

    }
}
