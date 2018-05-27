﻿using System;
using System.IO;
using System.Linq;
using Ragnar;
using System.Collections.ObjectModel;
using WebTorrentX.Models;

namespace WebTorrentX.Controllers
{
    internal sealed class DownloadController : IDisposable
    {

        private const int minPort = 2000;
        private const int maxPort = 2500;
        private readonly string sesStateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".ses_state");
        private readonly string torrentDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".torrents");


        private Session session;

        public ObservableCollection<Torrent> Torrents { get; private set; }

        public event EventHandler<string> Error;

        public DownloadController()
        {
            session = new Session();
            LoadSessionState();            
            Torrents = new ObservableCollection<Torrent>();
            LoadActiveTorrents();
        }

        private void LoadActiveTorrents()
        {
            if (Directory.Exists(torrentDir))
            {
                var files = Directory.GetFiles(torrentDir);
                foreach (var file in files)
                {
                    var addParams = new AddTorrentParams
                    {
                        TorrentInfo = new TorrentInfo(file)
                    };
                    var torrent = Torrent.Create(addParams, session);
                    Torrents.Add(torrent);
                }
            }
        }

        private void LoadSessionState()
        {
            if (File.Exists(sesStateFile))
            {
                session.LoadState(File.ReadAllBytes(sesStateFile));
            }
            else session.ListenOn(minPort, maxPort);
        }

        private void SaveSessionState()
        {
            if (Torrents.Count > 0) File.WriteAllBytes(sesStateFile, session.SaveState());               
        }

        public void LoadTorrent(string filename)
        {
            if (!Directory.Exists(torrentDir))
                Directory.CreateDirectory(torrentDir);
            try
            {
                TorrentInfo info = new TorrentInfo(filename);
                File.Copy(filename, Path.Combine(torrentDir, info.Name + ".torrent"), true);
                var result = from t in Torrents where t.InfoHash.ToHex() == info.InfoHash select t;
                if (result == null || result.Count() == 0)
                {
                    var addParams = new AddTorrentParams
                    {
                        SavePath = Properties.Settings.Default.Location,
                        TorrentInfo = new TorrentInfo(filename)
                    };
                    addParams.TorrentInfo = new TorrentInfo(filename);
                    var torrent = Torrent.Create(addParams, session);
                    Torrents.Add(torrent);
                }
                else
                {
                    Error?.Invoke(this, "This torrent already exists");
                }
            }
            catch
            {
                Error?.Invoke(this, "Error parsing file");
            }   
        }

        public void Dispose()
        {
            foreach (var torrent in Torrents)
                torrent.Dispose();
            SaveSessionState();
        }
    }
}
