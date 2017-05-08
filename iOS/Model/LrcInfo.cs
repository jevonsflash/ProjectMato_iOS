using System;
using System.Collections.Generic;

namespace ProjectMato.iOS.Model
{
    public class LrcInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 歌曲
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 专辑
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 歌词作者
        /// </summary>
        public string LrcBy { get; set; }
        /// <summary>
        /// 偏移量
        /// </summary>
        public string Offset { get; set; }

        /// <summary>
        /// 歌词
        /// </summary>
        public List<LrcWord> LrcWords = new List<LrcWord>();
    }

    public class LrcWord
    {
        public LrcWord()
        {
            Time = default(TimeSpan);
            Content = string.Empty;
            IsCurrent = false;
        }

        public int LrcWordId { get; set; }
        public TimeSpan Time { get; set; }
        public string Content { get; set; }
        public bool IsCurrent { get; set; }
    }
}
