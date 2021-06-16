using File_Explorer_project.Model;
using Microsoft.Toolkit.Uwp.Helpers;
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
using FileAttributes = System.IO.FileAttributes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace File_Explorer_project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Folders> Picturefiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Listitems = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Musicfiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> VideoFiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Objectfiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Pickfiles = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Pickfolders = new ObservableCollection<Folders>();
        private ObservableCollection<Folders> Documentfiles = new ObservableCollection<Folders>();
        public StorageFolder picturesFolder;
       
        public string path = "C:\\Users\\"+ApplicationData.Current.LocalFolder.Path.Split("\\")[2]+"\\";
        List<string> Files = new List<string>();
        public MainPage()
        {
         
            this.InitializeComponent();
            
        }

        private   void PicturesButton_Click(object sender, RoutedEventArgs e)
        {
            ListGrid.Background = new SolidColorBrush(Colors.White);
            Detailstackpanel.Background = new SolidColorBrush(Colors.Transparent);
            ListTextblock.Text = "Pictures";
            
            if(path!= "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\")
            {
                path = "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\";
            }

            //Add the selected Button folder to the path

            path += ListTextblock.Text;

            //Get all Files and Folders in the Pictures Library  

            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            GiveItems(picturesFolder);
            Double windowwidth = this.ActualWidth;
            
            Visible_collapsed();

            
        }
       
        private async  void FoldersListview_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            var data = (Folders)e.ClickedItem;
            string folder = data.Foldername;

            // Add the selected folder to the path 

            path +="\\" + folder;
            ListTextblock.Text = data.Foldername;
            if (Files.Contains(folder))
            {
                Double windowwidth = this.ActualWidth;
                if (windowwidth < 600)
                {
                    DetailViewBack.Visibility = Visibility.Visible;
                }
                Detailstackpanel.Visibility = Visibility.Visible;
                Detailstackpanel.Background = new SolidColorBrush(Colors.White);
                Detailstackpanel.BorderThickness = new Thickness(2, 0, 0, 0);
                DetailsTextblock.Visibility = Visibility.Visible;
                DetailsTextblock.Text = data.Foldername + " " + "Properties";
                Filename.Visibility = Visibility.Visible;
                Filedate.Visibility = Visibility.Visible;
                Filepath.Visibility = Visibility.Visible;
                Filemodifieddate.Visibility = Visibility.Visible;
                Filesize.Visibility = Visibility.Visible;
                Filenametext.Visibility = Visibility.Visible;
                Filedatetext.Visibility = Visibility.Visible;
                Filepathtext.Visibility = Visibility.Visible;
                FileModifieddatetext.Visibility = Visibility.Visible;
                Filesizetext.Visibility = Visibility.Visible;
                Filenametext.Text = data.Foldername;
                Filepathtext.Text = data.Folderpath;
                Filedatetext.Text = data.FolderCreatedDate.ToString();
                FileModifieddatetext.Text = data.FolderModifiedDate.ToString();
                Filesizetext.Text = data.Foldersize + "Bytes";
                path = path.Replace("\\" + data.Foldername, "");
            }
            else 
            {
                    Listitems.Clear();
                    StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(path);
                    
                    IReadOnlyList<IStorageItem> itemlist = await storageFolder.GetItemsAsync();
                    foreach (var Item in itemlist)
                    {
                        if (Listitems.Count < itemlist.Count )
                        { 
                            Windows.Storage.FileProperties.BasicProperties basicProperties = await Item.GetBasicPropertiesAsync();
                            string fileSize = string.Format("{0:n0}", basicProperties.Size);
                        Listitems.Add(new Folders { Foldername = Item.Name, Folderpath = Item.Path, FolderCreatedDate = Item.DateCreated, Foldersize = fileSize, FolderModifiedDate = basicProperties.DateModified });
                        }
                        if(Item is StorageFile)
                        {
                            Files.Add(Item.Name);
                        }

                    }
                FoldersListview.ItemsSource = Listitems;
            }
                     
        }


        private  void MusicButton_Click(object sender, RoutedEventArgs e)
        {
            ListGrid.Background = new SolidColorBrush(Colors.White);
            Detailstackpanel.Background = new SolidColorBrush(Colors.Transparent);
            ListTextblock.Text = "Music";
          
            if (path != "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\")
            {
                path = "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\";
            }
            path += ListTextblock.Text;
            //Get all Files and Folders in the Music library

            StorageFolder musicFolder = KnownFolders.MusicLibrary;
            GiveItems(musicFolder);
            DetailsTextblock.Text = "";
            Visible_collapsed();
            Double windowwidth = this.ActualWidth;
            
        }

        private   void VideoButton_Click(object sender, RoutedEventArgs e)
        {
            ListGrid.Background = new SolidColorBrush(Colors.White);
            Detailstackpanel.Background = new SolidColorBrush(Colors.Transparent);
            ListTextblock.Text = "Videos";
            if (path != "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\")
            {
                path = "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\";
            }
            path += ListTextblock.Text;

            //Get all Files and Folders in the Videos Library

            StorageFolder videofolder = KnownFolders.VideosLibrary;
            GiveItems(videofolder);
            DetailsTextblock.Text = "";
            Visible_collapsed();
            
            
        }

        private   void ObjectButton_Click(object sender, RoutedEventArgs e)
        {
            ListGrid.Background = new SolidColorBrush(Colors.White);
            Detailstackpanel.Background = new SolidColorBrush(Colors.Transparent);
            ListTextblock.Text = "3D Objects";
           
            if (path != "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\")
            {
                path = "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\";
            }
            path += ListTextblock.Text;

            //Get all Files and Folders in the 3D Objects Library

            StorageFolder videofolder = KnownFolders.Objects3D;
            GiveItems(videofolder);
            DetailsTextblock.Text = "";
            Visible_collapsed();
            Double windowwidth = this.ActualWidth;
            
        }

        private   void DocumentsButton_Click(object sender, RoutedEventArgs e)
        {
            ListGrid.Background = new SolidColorBrush(Colors.White);
            Detailstackpanel.Background = new SolidColorBrush(Colors.Transparent);
            ListTextblock.Text = "Documents";
            
            if (path != "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\")
            {
                path = "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] + "\\";
            }
            path += ListTextblock.Text;

            //Get all Files and Folders in the Documents Library

            StorageFolder Documentfolder = KnownFolders.DocumentsLibrary;
            GiveItems(Documentfolder);
            DetailsTextblock.Text = "";
            Visible_collapsed();
            Double windowwidth = this.ActualWidth;
            
        }
        private async void  GiveItems(StorageFolder folder)
        {
            Documentfiles.Clear();
            IReadOnlyList<IStorageItem> itemlist = await folder.GetItemsAsync();

            foreach (var item in itemlist)
            {
                if (Documentfiles.Count < itemlist.Count)
                {
                    Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                    string filesize = string.Format("{0:n0}", basicProperties.Size);
                    Documentfiles.Add(new Folders { Foldername = item.Name, Folderpath = item.Path, FolderCreatedDate = item.DateCreated, FolderModifiedDate = basicProperties.DateModified, Foldersize = filesize });
                }
                if (item is StorageFile)
                {
                    Files.Add(item.Name);
                }
            }
            FoldersListview.ItemsSource = Documentfiles;
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
            path = file.Path;
            if (file != null)
            {
                Files.Add(file.Name);
                Windows.Storage.FileProperties.BasicProperties basicProperties = await file.GetBasicPropertiesAsync();
                string filesize = string.Format("{0:n}", basicProperties.Size);
                Pickfiles.Add(new Folders { Foldername = file.Name, FolderCreatedDate = file.DateCreated, Folderpath = file.Path, FolderModifiedDate = basicProperties.DateModified, Foldersize = filesize });
            }
            DetailsTextblock.Text = "";
            Visible_collapsed();
            ListGrid.Background = new SolidColorBrush(Colors.White);
            Detailstackpanel.Background = new SolidColorBrush(Colors.Transparent);
            Double windowwidth = this.ActualWidth;
          
        }

        private async  void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            FoldersListview.ItemsSource = Pickfolders;
            path = "C:\\Users\\" + ApplicationData.Current.LocalFolder.Path.Split("\\")[2] ;
            var Folderpicker = new Windows.Storage.Pickers.FolderPicker();
            Folderpicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            Folderpicker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder folder = await Folderpicker.PickSingleFolderAsync();
            if (folder != null)
            {
                Windows.Storage.FileProperties.BasicProperties basicProperties = await folder.GetBasicPropertiesAsync();
                string filesize = string.Format("{0:n}", basicProperties.Size);
               Pickfolders.Add(new Folders { Foldername = folder.Name, FolderCreatedDate = folder.DateCreated, Folderpath = folder.Path,FolderModifiedDate=basicProperties.DateModified,Foldersize=filesize });
                ListTextblock.Text = folder.Name;
            }
            else
            {
                return;
            }
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
             if( await picturesFolder.TryGetItemAsync(folder.Name) != null)
            {
                path += "\\" + "Pictures";
            }
            else if(await KnownFolders.MusicLibrary.TryGetItemAsync(folder.Name) != null)
            {
                path += "\\" + "Music";
            }
            else if(await KnownFolders.DocumentsLibrary.TryGetItemAsync(folder.Name) != null)
            {
                path += "\\" + "Documents";
            }
            else if (await KnownFolders.VideosLibrary.TryGetItemAsync(folder.Name) != null)
            {
                path += "\\" + "Videos";
            }
            else if (await KnownFolders.Objects3D.TryGetItemAsync(folder.Name) != null)
            {
                path += "\\" + "3D Objects";
            }
            DetailsTextblock.Text = "";
            Visible_collapsed();
            ListGrid.Background = new SolidColorBrush(Colors.White);
            Detailstackpanel.Background = new SolidColorBrush(Colors.Transparent);
            Double windowwidth = this.ActualWidth;
           
        }

        private void Detailstackpanel_Loaded(object sender, RoutedEventArgs e)
        { 
            
        }

        private void Visible_collapsed()
        {
            Detailstackpanel.Visibility = Visibility.Collapsed;
            Filename.Visibility = Visibility.Collapsed;
            Filedate.Visibility = Visibility.Collapsed;
            Filepath.Visibility = Visibility.Collapsed;
            Filemodifieddate.Visibility = Visibility.Collapsed;
            Filesize.Visibility = Visibility.Collapsed;
            Filenametext.Visibility = Visibility.Collapsed;
            Filedatetext.Visibility = Visibility.Collapsed;
            Filepathtext.Visibility = Visibility.Collapsed;
            FileModifieddatetext.Visibility = Visibility.Collapsed;
            Filesizetext.Visibility = Visibility.Collapsed;
            DetailsTextblock.Visibility = Visibility.Collapsed;
        }

        private async  void BackButton_Click(object sender, RoutedEventArgs e)
        {
           
            path = path.Replace("\\" + ListTextblock.Text, "");
            string[] arr = path.Split("\\");
            if(arr.Length-1 <= 2)
            {
                path += "\\" + ListTextblock.Text;
                return;
            }
            else
            {
                ListTextblock.Text = arr[arr.Length - 1];
            }
            Listitems.Clear();
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(path);

            IReadOnlyList<IStorageItem> itemlist = await storageFolder.GetItemsAsync();
            foreach (var Item in itemlist)
            {
                if (Listitems.Count < itemlist.Count)
                {
                    Windows.Storage.FileProperties.BasicProperties basicProperties = await Item.GetBasicPropertiesAsync();
                    string fileSize = string.Format("{0:n0}", basicProperties.Size);
                    Listitems.Add(new Folders { Foldername = Item.Name, Folderpath = Item.Path, FolderCreatedDate = Item.DateCreated, Foldersize = fileSize, FolderModifiedDate = basicProperties.DateModified });
                }

            }
            FoldersListview.ItemsSource = Listitems;

            Visible_collapsed();
            
        }

        
        

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {

            Visible_collapsed();
        }

        private void BackButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            BackButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void BackButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            BackButton.Background = new SolidColorBrush(Colors.Transparent);
        }


        private void PicturesButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            PicturesButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void MusicButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            MusicButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void VideoButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            VideoButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void DocumentsButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            DocumentsButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void FileButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            FileButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void FolderButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            FolderButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void ObjectButton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            ObjectButton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private async  void DetailViewBack_Click(object sender, RoutedEventArgs e)
        {
            path = path.Replace("\\" + ListTextblock.Text, "");
            string[] arr = path.Split("\\");
            ListTextblock.Text = arr[arr.Length - 1];
            Listitems.Clear();
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(path);

            IReadOnlyList<IStorageItem> itemlist = await storageFolder.GetItemsAsync();
            foreach (var Item in itemlist)
            {
                if (Listitems.Count < itemlist.Count)
                {
                    Windows.Storage.FileProperties.BasicProperties basicProperties = await Item.GetBasicPropertiesAsync();
                    string fileSize = string.Format("{0:n0}", basicProperties.Size);
                    Listitems.Add(new Folders { Foldername = Item.Name, Folderpath = Item.Path, FolderCreatedDate = Item.DateCreated, Foldersize = fileSize, FolderModifiedDate = basicProperties.DateModified });
                }

            }
            FoldersListview.ItemsSource = Listitems;

            Visible_collapsed();
            DetailViewBack.Visibility = Visibility.Collapsed;
        }

        private void Hamburgerbutton_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Hamburgerbutton.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void DetailViewBack_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            DetailViewBack.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
