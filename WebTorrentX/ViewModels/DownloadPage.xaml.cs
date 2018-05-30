﻿using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WebTorrentX.Controllers;
using WebTorrentX.Models;
using System;
using System.Diagnostics;

namespace WebTorrentX.ViewModels
{

    public sealed partial class DownloadPage : Page
    {

        private FileSystemWatcher watcher;        

        internal ObservableCollection<Torrent> TorrentSource
        {
            get { return App.downloadController.Torrents; }
        }

        public DownloadPage()
        {
            InitializeComponent();
            App.downloadController = new DownloadController();
            App.downloadController.Error += (sender, message) =>
            {
                MessageBox.Show(message, "WebTorrentX", MessageBoxButton.OK);
            };
            DataContext = this;
            TorrentListView.ItemsSource = TorrentSource;
            if (Application.Current.Properties["openfile"] != null)
            {
                LoadTorrent((string)Application.Current.Properties["openfile"]);
            }
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.WatchForNewFiles)
            {
                watcher = new FileSystemWatcher();
                watcher.Path = Properties.Settings.Default.WatchFolder;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Filter = "*.torrent";
                watcher.Created += Watcher_Created;
                watcher.EnableRaisingEvents = true;
            }
            else if (watcher != null)
                watcher.Dispose();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                App.downloadController.LoadTorrent(e.FullPath);
            });
        }        

        public void LoadTorrent(string filename)
        {
            App.downloadController.LoadTorrent(filename);
        }

        public void PauseAll()
        {
            foreach (var torrent in App.downloadController.Torrents)
                torrent.IsDownloading = false;
        }

        public void ResumeAll()
        {
            foreach (var torrent in App.downloadController.Torrents)
                torrent.IsDownloading = true;
        }

        private void RemoveTorrentButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Remove this torrent?", "WebTorrentX", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if ((sender as Button).Tag is Torrent)
                {
                    ((sender as Button).Tag as Torrent).Remove();
                    App.downloadController.Torrents.Remove((sender as Button).Tag as Torrent);
                }
            }

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Torrent)
            {
                string path = Path.Combine(((sender as Button).Tag as Torrent).DownloadPath, ((sender as Button).Tag as Torrent).Name);
                if (Directory.Exists(path))
                {
                    string[] extensions = { ".mkv", ".flv", ".f4p", ".f4a", ".f4v", "f4b", ".avi", ".wmv", ".mp4", ".m4p", ".m4v", ".mpg", ".mpeg", ".m2v", ".3gp" };
                    foreach (string file in Directory.GetFiles(path))
                    {
                        if (extensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
                        {
                            Application.Current.Properties["filename"] = file;
                            break;
                        }
                    }
                }
                else
                {
                    Application.Current.Properties["filename"] = Path.Combine(((sender as Button).Tag as Torrent).DownloadPath, ((sender as Button).Tag as Torrent).Name);
                }
                NavigationService.Navigate(new PlayerPage());
            }                
        }

        private void TorrentListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < files.Length; i++)
                    LoadTorrent(files[i]);
            }
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag is Torrent)
            {
                var torrent = (sender as Button).Tag as Torrent;
                if (Directory.Exists(torrent.DownloadPath))
                {
                    Process.Start("explorer.exe", torrent.DownloadPath);
                }
            }
        }
    }
}
