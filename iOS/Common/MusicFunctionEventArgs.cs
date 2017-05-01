using System;
using ProjectMato.iOS.Model;

namespace ProjectMato.iOS.Common
{
    public class MusicFunctionEventArgs : EventArgs
    {
        public MusicFunctionEventArgs(IBasicInfo musicInfo, MenuCellInfo menuCellInfo)
        {
            this.MusicInfo = musicInfo;
            this.MenuCellInfo = menuCellInfo;
        }
        public IBasicInfo MusicInfo { get; set; }
        public MenuCellInfo MenuCellInfo { get; set; }
    }
}