using System.IO;
using System.Linq;
using Caliburn.Micro;
using ImageViewer.Extensions;

namespace ImageViewer.Views.DirectoryControl
{
    public class DirectoryViewModel : Screen
    {
        private BindableCollection<DirectoryItem> _items;
        private string _directoryPath;

        public string DirectoryPath
        {
            get { return this._directoryPath; }
            set { this.SetProperty(ref this._directoryPath, value); }
        }


        public BindableCollection<DirectoryItem> Items
        {
            get { return this._items; }
            set { this.SetProperty(ref this._items, value); }
        }

        public DirectoryViewModel()
        {
            this.Items = new BindableCollection<DirectoryItem>();
        }

        public void LoadDirectory(string path)
        {
            if (Directory.Exists(path) == false)
            {
                this.Items = new BindableCollection<DirectoryItem>();
                return;
            }

            var subdirectories = Directory.GetDirectories(path);

            foreach (var currentSubdirectory in subdirectories)
            {
                this.Items.Add(new SubDirectoryItem(currentSubdirectory));
            }

            var files = Directory.GetFiles(path);

            foreach (var currentFile in files.Where(f => Path.GetExtension(f).IsValidImageExtension()))
            {
                this.Items.Add(new FileDirectoryItem(currentFile));
            }
        }
    }
}