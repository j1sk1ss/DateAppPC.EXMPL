using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.ANIMATIONS;
using DateAppPC.EXPL.OBJECTS;
using Microsoft.Win32;

namespace DateAppPC.EXPL.WINDOWS {
    public partial class UserInfo : Window {
        public UserInfo(MainWindow mainWindow, User user) {
            InitializeComponent();

            MainWindow    = mainWindow;
            User          = user;
            UserName.Text = user.Name;
            NickName.Text = user.Nick;
            Info.Text     = user.Info;
            
            foreach (var interest in user.Interests) {
                Interests.Text += interest + ", ";
            }
        }
        private MainWindow MainWindow { get; set; }
        private string LinkToPicture { get; set; }
        private User User { get; set; }
        private void ClosedWindow(object sender, EventArgs e) {
            User.Name      = UserName.Text;
            User.Nick      = NickName.Text;
            User.Info      = Info.Text;
            User.Interests = Interests.Text.Replace(" ", "").Split(",").ToList();
            
            var link = $"C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\" +
                       $"DateAppPC.EXPL\\IMAGES\\{UserName.Text}_{NickName.Text}.jpg";
            File.Copy(LinkToPicture, link, true);
            
            User.ProfileImage = link;
            User.DateOfBirth  = Age.DisplayDate;
            MainWindow.User   = User;
            
            MainWindow.UpdateProfile();
        }
        private void PictureChose(object sender, RoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog {
                Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() != true) return;
            
            LinkToPicture         = openFileDialog.FileName;
            PreviewPicture.Source = new BitmapImage(new Uri(LinkToPicture));
        }
    }
}