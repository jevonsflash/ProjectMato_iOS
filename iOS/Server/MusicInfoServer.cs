using MediaPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMato.iOS.Common;
using UIKit;
using Xamarin.Forms;
using XLabs;
using Foundation;
using Microsoft.International.Converters.PinYinConverter;
using System.Text.RegularExpressions;
using GameKit;
using ProjectMato.iOS.Model;

namespace ProjectMato.iOS.Server
{
    public class MusicInfoServer
    {

        private const int MyFavouriteIndex = 1;
        private static MusicInfoServer current;

        List<ArtistInfo> _artistInforesult;

        List<AlbumInfo> _albumInforesult;

        List<MusicInfo> _musicInfosresult;

        /// <summary>
        /// 当前实例
        /// </summary>
        public static MusicInfoServer Current
        {
            get
            {

                if (current == null)
                {
                    current = new MusicInfoServer();
                }
                return current;
            }

        }

        private MusicInfoServer()
        {
            DatabaseManager.Current.Connect();
        }

        ~MusicInfoServer()
        {
            DatabaseManager.Current.Disconnect();
        }

        private MPMediaQuery _mediaQuery;

        public MPMediaQuery MediaQuery
        {
            get
            {
                if (_mediaQuery == null)
                {
                    _mediaQuery = new MPMediaQuery();
                }
                return _mediaQuery;
            }
        }
        /// <summary>
        /// 获取分组包装好的MusicInfo集合
        /// </summary>
        /// <returns></returns>
        public AlphaGroupedObservableCollection<MusicInfo> GetAlphaGroupedMusicInfo()
        {
            AlphaGroupedObservableCollection<MusicInfo> result = new AlphaGroupedObservableCollection<MusicInfo>();
            var list = GetMusicInfos();
            list.ForEach(c =>
            {
                result.Add(c, c.GroupHeader);

            });
            result.Root = result.Root.Where(c => c.HasItems).ToList();
            return result;


        }
        /// <summary>
        /// 获取分组包装好的ArtistInfo集合
        /// </summary>
        /// <returns></returns>
        public AlphaGroupedObservableCollection<ArtistInfo> GetAlphaGroupedArtistInfo()
        {
            AlphaGroupedObservableCollection<ArtistInfo> result = new AlphaGroupedObservableCollection<ArtistInfo>();
            var list = GetArtistInfos();
            list.ForEach(c =>
            {
                result.Add(c, c.GroupHeader);

            });
            result.Root = result.Root.Where(c => c.HasItems).ToList();
            return result;


        }

        /// <summary>
        /// 获取分组包装好的AlbumInfo集合
        /// </summary>
        /// <returns></returns>
        public AlphaGroupedObservableCollection<AlbumInfo> GetAlphaGroupedAlbumInfo()
        {
            AlphaGroupedObservableCollection<AlbumInfo> result = new AlphaGroupedObservableCollection<AlbumInfo>();
            var list = GetAlbumInfos();
            list.ForEach(c =>
            {
                result.Add(c, c.GroupHeader);

            });
            result.Root = result.Root.Where(c => c.HasItems).ToList();
            return result;


        }

        private bool MediaLibraryAuthorization()
        {
            var result = false;
            var status = MPMediaLibrary.AuthorizationStatus;
            switch (status)
            {
                case MPMediaLibraryAuthorizationStatus.Authorized:
                    result = true;
                    break;
                case MPMediaLibraryAuthorizationStatus.NotDetermined:
                    MPMediaLibrary.RequestAuthorization((c) =>
                    {
                        result = c == MPMediaLibraryAuthorizationStatus.Authorized;
                    });
                    break;
                case MPMediaLibraryAuthorizationStatus.Denied:
                case MPMediaLibraryAuthorizationStatus.Restricted:
                    result = false;
                    break;
            }
            return result;
            ;
        }

        /// <summary>
        /// 获取MusicInfo集合
        /// </summary>
        /// <returns></returns>
        public List<MusicInfo> GetMusicInfos()
        {
            if (_musicInfosresult == null && MediaLibraryAuthorization())
            {


                _musicInfosresult = (from item in MediaQuery.Items
                                     where item.MediaType == MPMediaType.Music
                                     select new MusicInfo()
                                     {
                                         Id = Guid.NewGuid().ToString("N"),
                                         Title = item.Title,
                                         Url = item.AssetURL.ToString(),
                                         AlbumTitle = item.AlbumTitle,
                                         Artist = item.Artist,
                                         AlbumArt = GetAlbumArtSource(item),
                                         GroupHeader = GetGroupHeader(item.Title),
                                         IsFavourite = GetIsMyFavouriteContains(item.Title)
                                     }).ToList();
            }
            return _musicInfosresult;

        }

        /// <summary>
        /// 获取AlbumInfo集合
        /// </summary>
        /// <returns></returns>

        public List<AlbumInfo> GetAlbumInfos()
        {

            if (_albumInforesult == null && MediaLibraryAuthorization())
            {


                _albumInforesult = (from item in MediaQuery.Items
                                    where item.MediaType == MPMediaType.Music
                                    group item by item.AlbumTitle
                                into c
                                    select new AlbumInfo()
                                    {
                                        Title = c.Key,
                                        GroupHeader = GetGroupHeader(c.Key),

                                        AlbumArt = GetAlbumArtSource(c.FirstOrDefault()),
                                        Musics = c.Select(d => new MusicInfo()
                                        {
                                            Id = Guid.NewGuid().ToString("N"),
                                            Title = d.Title,
                                            Url = d.AssetURL.ToString(),
                                            AlbumTitle = d.AlbumTitle,
                                            Artist = d.Artist,
                                            AlbumArt = GetAlbumArtSource(d),
                                            IsFavourite = GetIsMyFavouriteContains(d.Title)
                                        }).ToList()

                                    }).ToList();
            }
            return _albumInforesult;

        }

        /// <summary>
        /// 获取ArtistInfo集合
        /// </summary>
        /// <returns></returns>
        public List<ArtistInfo> GetArtistInfos()
        {
            if (_artistInforesult == null && MediaLibraryAuthorization())
            {


                _artistInforesult = (from item in MediaQuery.Items
                                     where item.MediaType == MPMediaType.Music
                                     group item by item.Artist
                              into c
                                     select new ArtistInfo()
                                     {
                                         Title = c.Key,
                                         GroupHeader = GetGroupHeader(c.Key),
                                         Musics = c.Select(d => new MusicInfo()
                                         {
                                             Id = Guid.NewGuid().ToString("N"),
                                             Title = d.Title,
                                             Url = d.AssetURL.ToString(),
                                             AlbumTitle = d.AlbumTitle,
                                             Artist = d.Artist,
                                             AlbumArt = GetAlbumArtSource(d),
                                             IsFavourite = GetIsMyFavouriteContains(d.Title)
                                         }).ToList()

                                     }).ToList();
            }

            return _artistInforesult;
        }

        /// <summary>
        /// 将MusicInfo插入到列队
        /// </summary>
        /// <param name="musicInfo"></param>
        /// <returns></returns>
        public bool CreateQueueEntry(MusicInfo musicInfo)
        {
            var entry = new QueueEntryTable(musicInfo.Title, 0);
            var result = DatabaseManager.Current.AddQueueEntryTable(entry);
            return result > 0;
        }

        /// <summary>
        /// 将MusicInfo集合插入到列队
        /// </summary>
        /// <param name="musicInfos"></param>
        /// <returns></returns>
        public bool CreateQueueEntrys(List<MusicInfo> musicInfos)
        {
            //var rankValue = 0;
            var entrys = musicInfos.Select(c => new QueueEntryTable(c.Title, 0));
            var result = DatabaseManager.Current.AddQueueEntryTables(entrys);
            return result > 0;
        }

        /// <summary>
        /// 从列队中读取MusicInfo
        /// </summary>
        /// <returns></returns>
        public List<MusicInfo> GetQueueEntry()
        {
            var queueEntrys = DatabaseManager.Current.FetchQueueEntryTables();
            var musicInfos
                = GetMusicInfos();
            var result =
                from musicInfo in musicInfos
                join queueEntryTable in queueEntrys
                    on musicInfo.Title equals queueEntryTable.MusicTitle
                orderby queueEntryTable.QueueEntryId
                select musicInfo;
            return result.ToList();

        }

        /// <summary>
        /// 从列队中删除指定MusicInfo
        /// </summary>
        /// <param name="musicInfo"></param>
        /// <returns></returns>
        public bool DeleteMusicInfoFormQueueEntry(MusicInfo musicInfo)
        {
            var entry =
                DatabaseManager.Current.FetchQueueEntryTables().FirstOrDefault(c => c.MusicTitle == musicInfo.Title);
            var result = DatabaseManager.Current.DeleteQueueItem(entry.QueueEntryId);
            return result > 0;
        }

        /// <summary>
        /// 交换列队中两个MusicInfo的位置
        /// </summary>
        /// <param name="oldMusicInfo"></param>
        /// <param name="newMusicInfo"></param>
        public void ReorderQueue(MusicInfo oldMusicInfo, MusicInfo newMusicInfo)
        {
            DatabaseManager.Current.InterchangeQueueEntry(oldMusicInfo.Title, newMusicInfo.Title);
        }

        /// <summary>
        /// 从列队中清除所有MusicInfo
        /// </summary>
        public void ClearQueue()
        {
            DatabaseManager.Current.ClearQueue();
        }

        /// <summary>
        /// 将MusicInfo插入到歌单
        /// </summary>
        /// <param name="musicInfo"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool CreatePlaylistEntry(MusicInfo musicInfo, int playlistId)
        {
            var entry = new PlaylistEntryTable(playlistId, musicInfo.Title, 0);
            var result = DatabaseManager.Current.AddPlaylistEntry(entry);
            return result > 0;
        }

        /// <summary>
        /// 将MusicInfo集合插入到歌单
        /// </summary>
        /// <param name="musicCollectionInfo"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool CreatePlaylistEntrys(MusicCollectionInfo musicCollectionInfo, int playlistId)
        {
            var entrys = musicCollectionInfo.Musics.Select(c => new PlaylistEntryTable(playlistId, c.Title, 0));
            var result = DatabaseManager.Current.AddPlaylistEntryTables(entrys);
            return result > 0;
        }

        /// <summary>
        /// 从歌单中删除MusicInfo根据指定的Title
        /// </summary>
        /// <param name="musicTitle"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool DeletePlaylistEntry(string musicTitle, int playlistId)
        {
            var entry = DatabaseManager.Current.FetchPlaylistEntriesForPlaylistAndSongTitle(playlistId, musicTitle).FirstOrDefault();
            if (entry != null)
            {
                var result = DatabaseManager.Current.DeletePlaylistEntry(entry.PlaylistEntryId);
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从歌单中删除MusicInfo
        /// </summary>
        /// <param name="musicInfo"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool DeletePlaylistEntry(MusicInfo musicInfo, int playlistId)
        {
            return DeletePlaylistEntry(musicInfo.Title, playlistId);
        }


        /// <summary>
        /// 将MusicInfo插入到“我最喜爱”歌单
        /// </summary>
        /// <param name="musicInfo"></param>
        /// <returns></returns>
        public bool CreatePlaylistEntryToMyFavourite(MusicInfo musicInfo)
        {
            return CreatePlaylistEntry(musicInfo, MyFavouriteIndex);

        }

        /// <summary>
        /// 从“我最喜爱”中删除MusicInfo
        /// </summary>
        /// <param name="musicInfo"></param>
        /// <returns></returns>
        public bool DeletePlaylistEntryFromMyFavourite(MusicInfo musicInfo)
        {
            return DeletePlaylistEntry(musicInfo, MyFavouriteIndex);
        }

        /// <summary>
        /// 从歌单中读取MusicInfo
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public List<MusicInfo> GetPlaylistEntry(int playlistId)
        {
            var currentPlaylistEntrie = DatabaseManager.Current.FetchPlaylistEntriesForPlaylist(playlistId);
            var result = GetMusicInfos().Where(c => currentPlaylistEntrie.Select(p => p.MusicTitle).Contains(c.Title)).ToList();
            return result;

        }

        /// <summary>
        /// 从“我最喜爱”中读取MusicInfo
        /// </summary>
        /// <returns></returns>
        public List<MusicInfo> GetPlaylistEntryFormMyFavourite()
        {
            return GetPlaylistEntry(MyFavouriteIndex);
        }

        /// <summary>
        /// 返回一个值表明一个Title是否包含在某个歌单中
        /// </summary>
        /// <param name="musicTitle">music标题</param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool GetIsPlaylistContains(string musicTitle, int playlistId)
        {
            var result = false;
            var playlistEntryList = DatabaseManager.Current.FetchPlaylistEntriesForPlaylist(playlistId);
            result = playlistEntryList.Any(c => c.MusicTitle == musicTitle);
            return result;

        }

        /// <summary>
        ///  返回一个值表明一个MusicInfo是否包含在某个歌单中
        /// </summary>
        /// <param name="musicInfo">musicInfo对象</param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool GetIsPlaylistContains(MusicInfo musicInfo, int playlistId)
        {
            var result = false;
            result = DatabaseManager.Current.FetchPlaylistEntriesForPlaylist(playlistId).Any(c => c.MusicTitle == musicInfo.Title);
            return result;

        }

        /// <summary>
        /// 返回一个值表明一个Title是否包含在"我最喜爱"中
        /// </summary>
        /// <param name="musicTitle">music标题</param>
        /// <returns></returns>
        public bool GetIsMyFavouriteContains(string musicTitle)
        {
            return GetIsPlaylistContains(musicTitle, MyFavouriteIndex);

        }

        /// <summary>
        ///  返回一个值表明一个MusicInfo是否包含在"我最喜爱"中
        /// </summary>
        /// <param name="musicInfo">musicInfo对象</param>
        /// <returns></returns>
        public bool GetIsMyFavouriteContains(MusicInfo musicInfo)
        {
            return GetIsPlaylistContains(musicInfo, 0);

        }

        /// <summary>
        /// 交换某歌单中两个MusicInfo的位置
        /// </summary>
        /// <param name="oldMusicInfo"></param>
        /// <param name="newMusicInfo"></param>
        /// <param name="playlistId"></param>
        public void ReorderPlaylist(MusicInfo oldMusicInfo, MusicInfo newMusicInfo, int playlistId)
        {
            DatabaseManager.Current.InterchangePlaylistEntry(oldMusicInfo.Title, newMusicInfo.Title, playlistId);
        }
        /// <summary>
        /// 交换"我最喜爱"中两个MusicInfo的位置
        /// </summary>
        /// <param name="oldMusicInfo"></param>
        /// <param name="newMusicInfo"></param>
        public void ReorderMyFavourite(MusicInfo oldMusicInfo, MusicInfo newMusicInfo)
        {
            ReorderPlaylist(oldMusicInfo, newMusicInfo, MyFavouriteIndex);
        }
        /// <summary>
        /// 获取Playlist
        /// </summary>
        /// <returns></returns>
        public List<PlaylistTable> GetPlaylist()
        {
            return DatabaseManager.Current.FetchPlaylists();
        }
        /// <summary>
        /// 获取PlaylistInfo
        /// </summary>
        /// <returns></returns>
        public List<PlaylistInfo> GetPlaylistInfo()
        {
            var playlistInfo = GetPlaylist().Select(c => new PlaylistInfo()
            {
                PlaylistId = c.PlaylistId,
                GroupHeader = GetGroupHeader(c.Name),
                Title = c.Name,
                IsHidden = c.IsHidden,
                IsRemovable = c.IsRemovable,
                Musics = GetPlaylistEntry(c.PlaylistId)
            }).ToList();
            return playlistInfo;
        }

        /// <summary>
        /// 创建Playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public bool CreatePlaylist(PlaylistTable playlist)
        {
            var result = DatabaseManager.Current.AddPlaylist(playlist);
            return result > 0;
        }

        /// <summary>
        /// 根据Id删除Playlist
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool DeletePlaylist(int playlistId)
        {
            var result = DatabaseManager.Current.DeletePlaylist(playlistId);
            return result > 0;
        }

        /// <summary>
        /// 根据PlaylistInfo删除Playlist
        /// </summary>
        /// <param name="playlistInfo"></param>
        /// <returns></returns>
        public bool DeletePlaylist(PlaylistInfo playlistInfo)
        {
            return DeletePlaylist(playlistInfo.PlaylistId);

        }
        /// <summary>
        /// 获取一个字符串的标题头
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private string GetGroupHeader(string title)
        {
            string result = string.Empty;
            if (title != string.Empty)
            {


                if (Regex.IsMatch(title, @"^[\u4e00-\u9fa5]+$"))
                {
                    try
                    {
                        var chinese = new ChineseChar(title.First());
                        result = chinese.Pinyins[0].Substring(0, 1);
                    }
                    catch (Exception ex)
                    {
                        return string.Empty;
                    }

                }
                else
                {
                    result = title.Substring(0, 1);
                }
            }
            return result;

        }

        /// <summary>
        /// 获取专辑封面Source
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private ImageSource GetAlbumArtSource(MPMediaItem item)
        {
            var _MPMediaItemArtwork = item.Artwork;
            if (_MPMediaItemArtwork != null)
            {


                var _UIImage = _MPMediaItemArtwork.ImageWithSize(new CoreGraphics.CGSize(200, 200));
                return ImageSource.FromStream(() => _UIImage.AsPNG().AsStream());
            }
            else
            {
                return null;
            }
        }


    }
}
