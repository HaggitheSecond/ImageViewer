using Caliburn.Micro;
using ImageViewer.Extensions;

namespace ImageViewer.Views.DirectoryControl
{
    public abstract class DirectoryItem : PropertyChangedBase
    {
        private string _name;
        private string _path;

        public string Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        public string Path
        {
            get { return this._path; }
            set { this.SetProperty(ref this._path, value); }
        }

        protected DirectoryItem(string path)
        {
            this.Path = path;
            this.Name = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }

    public class FileDirectoryItem : DirectoryItem
    {
        public FileDirectoryItem(string path)
            : base(path)
        {
            
        }
    }

    public class SubDirectoryItem : DirectoryItem
    {
        public SubDirectoryItem(string path)
            : base(path)
        {

        }
    }
}