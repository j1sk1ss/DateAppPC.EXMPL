using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.OBJECTS;
using Microsoft.Win32;

namespace DateAppPC.EXPL.WINDOWS {
    public partial class UserInfo : Window {
        public UserInfo(MainWindow mainWindow, User user) {
            InitializeComponent();
            MainWindow = mainWindow;
            User = user;
            
            UserName.Text = user.Name;
            NickName.Text = user.Nick;

            foreach (var interest in user.Interests) {
                Interests.Text += interest + ", ";
            }

            Info.Text = user.Info;
        }
        private MainWindow MainWindow { get; set; }
        private string LinkToPicture { get; set; }
        private User User { get; set; }
        private void ClosedWindow(object sender, EventArgs e) {
            try {
                MainWindow.ProfileImage.Source = new BitmapImage();
                
                var tempLink = "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL" +
                               $"\\DateAppPC.EXPL\\IMAGES\\{User.Name}_{User.Nick}.jpg";
                if (File.Exists(tempLink)) {
                    File.Delete(tempLink);
                }

                User.Name = UserName.Text;
                User.Nick = NickName.Text;
                User.Info = Info.Text;

                var temp = Interests.Text.Replace(" ", "").Split(",");
                User.Interests.Clear();
                foreach (var interest in temp) {
                    User.Interests.Add(interest);
                }

                var link = $"C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\" +
                           $"DateAppPC.EXPL\\IMAGES\\{UserName.Text}_{NickName.Text}.jpg";
                File.Copy(LinkToPicture, link, true);
                User.ProfileImage = link;
                User.DateOfBirth  = Age.DisplayDate;
                
                MainWindow.User = User;
                MainWindow.UpdateProfile();
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
                throw;
            }
        }

        private void PictureChose(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            
            if (openFileDialog.ShowDialog() == true) {
                LinkToPicture = openFileDialog.FileName;
            }
        }
    }
}