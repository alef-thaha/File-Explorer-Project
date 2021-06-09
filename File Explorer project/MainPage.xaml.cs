using File_Explorer_project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace File_Explorer_project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Folders> Picturefiles;
        private ObservableCollection<Folders> Pictureitems;
        private ObservableCollection<Folders> Musicfiles;
        private ObservableCollection<Folders> Musicitems = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> VideoFiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Objectfiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Pickfiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Pickfolders = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Documentfiles = new ObservableCollection<Folders>();
        public StorageFolder picturesFolder;
        public MainPage()
        {
         
            this.InitializeComponent();
            Picturefiles = new ObservableCollection<Folders>();
            Pictureitems = new ObservableCollection<Folders>();
            Musicfiles = new ObservableCollection<Folders>();
            
        }

        private  async void PicturesButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsSplitview.IsPaneOpen = false;
            ListTextblock.Text = "Pictures";
            FoldersListview.ItemsSource = Picturefiles;
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            

            IReadOnlyList<IStorageItem> itemsList = await picturesFolder.GetItemsAsync();

            foreach (var item in itemsList)
            {
                if (Picturefiles.Count < itemsList.Count )
                {
                    Windows.Storage.FileProperties.BasicProperties basicProperties =  await item.GetBasicPropertiesAsync();
                    string fileSize = string.Format("{0:n0}", basicProperties.Size);
                    Picturefiles.Add(new Folders { Foldername = item.Name,FolderCreatedDate=item.DateCreated, Folderpath=item.Path ,FolderModifiedDate=basicProperties.DateModified ,Foldersize=fileSize});
                }
                
            }
        }

        private async  void FoldersListview_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            var data = (Folders)e.ClickedItem;
            if(data.Foldername == "Saved Pictures")
            {
                ListTextblock.Text = data.Foldername;
                FoldersListview.ItemsSource = Pictureitems;
                StorageFolder PictureFolder = KnownFolders.SavedPictures;
                IReadOnlyList<IStorageItem> itemlist = await PictureFolder.GetItemsAsync();
                foreach (var Item in itemlist)
                {
                    if(Pictureitems.Count< itemlist.Count)
                    {
                        Windows.Storage.FileProperties.BasicProperties basicProperties = await Item.GetBasicPropertiesAsync();
                        string fileSize = string.Format("{0:n0}", basicProperties.Size);
                        Pictureitems.Add(new Folders { Foldername = Item.Name, Folderpath = Item.Path, FolderCreatedDate = Item.DateCreated,Foldersize=fileSize,FolderModifiedDate=basicProperties.DateModified });
                    }
                }
            }
            else
            {
                DetailsTextblock.Text = data.Foldername + " " + "Properties";
                DetailsSplitview.IsPaneOpen = !DetailsSplitview.IsPaneOpen;
                Filenametext.Text = data.Foldername;
                Filepathtext.Text = data.Folderpath;
                Filedatetext.Text = data.FolderCreatedDate.ToString();
                FileModifieddatetext.Text = data.FolderModifiedDate.ToString();
                Filesizetext.Text = data.Foldersize;
            }
          
        

            
        }

        private async void MusicButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsSplitview.IsPaneOpen = false;
            ListTextblock.Text = "Music";
            FoldersListview.ItemsSource = Musicfiles;
            StorageFolder picturesFolder = KnownFolders.MusicLibrary;
            

            IReadOnlyList<IStorageItem> itemsList = await picturesFolder.GetItemsAsync();

            foreach (var item in itemsList)
            {
                if (Musicfiles.Count < itemsList.Count )
                {
                    Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                    string fileSize = string.Format("{0:n0}", basicProperties.Size);
                    Musicfiles.Add(new Folders { Foldername = item.Name ,FolderCreatedDate=item.DateCreated,Folderpath=item.Path,FolderModifiedDate=basicProperties.DateModified, Foldersize=fileSize });
                }
                
            }
        }

        private async  void VideoButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsSplitview.IsPaneOpen = false;
            ListTextblock.Text = "Videos";
            FoldersListview.ItemsSource = VideoFiles;
            StorageFolder videofolder = KnownFolders.VideosLibrary;
            IReadOnlyList<IStorageItem> itemlist = await videofolder.GetItemsAsync();
            foreach (var item in itemlist)
            { 
                    if (VideoFiles.Count < itemlist.Count)
                    {
                     Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                     string fileSize = string.Format("{0:n0}", basicProperties.Size);
                    VideoFiles.Add(new Folders { Foldername = item.Name ,Folderpath=item.Path ,FolderCreatedDate=item.DateCreated,FolderModifiedDate=basicProperties.DateModified,Foldersize=fileSize});
                    }
                
                
            }
        }

        private async  void ObjectButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsSplitview.IsPaneOpen = false;
            ListTextblock.Text = "3D objects";
            FoldersListview.ItemsSource = Objectfiles;
            StorageFolder videofolder = KnownFolders.Objects3D;
            IReadOnlyList<IStorageItem> itemlist = await videofolder.GetItemsAsync();
            foreach (var item in itemlist)
            {
                if (Objectfiles.Count < itemlist.Count)
                {
                    Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                    string fileSize = string.Format("{0:n0}", basicProperties.Size);
                    Objectfiles.Add(new Folders { Foldername = item.Name,Folderpath=item.Path,FolderCreatedDate=item.DateCreated,Foldersize=fileSize,FolderModifiedDate=basicProperties.DateModified });
                }


            }
        }

        private async  void DocumentsButton_Click(object sender, RoutedEventArgs e)
        {

            ListTextblock.Text = "Documents";

            FoldersListview.ItemsSource = Documentfiles;
            StorageFolder Documentfolder = KnownFolders.DocumentsLibrary;
            IReadOnlyList<IStorageItem> itemlist = await Documentfolder.GetItemsAsync();
            foreach (var item in itemlist)
            {
                if (Documentfiles.Count < itemlist.Count)
                {
                    Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                    string filesize = string.Format("{0:n0}", basicProperties.Size);
                    Documentfiles.Add(new Folders { Foldername = item.Name ,Folderpath=item.Path,FolderCreatedDate=item.DateCreated,FolderModifiedDate=basicProperties.DateModified, Foldersize=filesize});
                }
            }
          
        }

        

        private void SplitviewcancelButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsSplitview.IsPaneOpen = false;
            DetailsTextblock.Text = "";
        }

        private void SplitviewcancelButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            SplitviewcancelButton.Background = new SolidColorBrush(Colors.White);
        }

        private void SplitviewcancelButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private async  void FileButton_Click(object sender, RoutedEventArgs e)
        {
            FoldersListview.ItemsSource = Pickfiles;
            ListTextblock.Text = "Picked files";
            var picker =  new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".wav");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".reg");
            picker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                Windows.Storage.FileProperties.BasicProperties basicProperties = await file.GetBasicPropertiesAsync();
                string filesize = string.Format("{0:n}", basicProperties.Size);
                Pickfiles.Add(new Folders { Foldername = file.Name, FolderCreatedDate = file.DateCreated, Folderpath = file.Path, FolderModifiedDate = basicProperties.DateModified, Foldersize = filesize });
            }
        }

        private async  void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            FoldersListview.ItemsSource = Pickfolders;
            ListTextblock.Text = "Picked folders";
            var Folderpicker = new Windows.Storage.Pickers.FolderPicker();
            Folderpicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            Folderpicker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder folder = await Folderpicker.PickSingleFolderAsync();
            if (folder != null)
            {
                Windows.Storage.FileProperties.BasicProperties basicProperties = await folder.GetBasicPropertiesAsync();
                string filesize = string.Format("{0:n}", basicProperties.Size);
               Pickfolders.Add(new Folders { Foldername = folder.Name, FolderCreatedDate = folder.DateCreated, Folderpath = folder.Path,FolderModifiedDate=basicProperties.DateModified,Foldersize=filesize });
            }
        }
    }
}
