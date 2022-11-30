using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.OBJECTS;
using Microsoft.Win32;

namespace DateAppPC.EXPL.WINDOWS {
    public partial class UserInfo {
        public UserInfo(MainWindow mainWindow, User user) {
            InitializeComponent();

            MainWindow     = mainWindow;
            User           = user;
            UserName.Text  = user.Name;
            NickName.Text  = user.Nick;
            
            Info.Document.Blocks.Add(new Paragraph(new Run(user.Info))); 
            Interests.Document.Blocks.Add(new Paragraph(new Run(user.Interests))); 
        }
        private string LinkToPicture { get; set; }        
        private MainWindow MainWindow { get; }
        private User User { get; }
        private void ClosedWindow(object sender, EventArgs e) {
            if (MainWindow.UsersData.Users.Any(user => user.Nick == NickName.Text)) {
                MessageBox.Show("Никнейм уже занят!");
                return;
            }
            
            User.Name      = UserName.Text;
            User.Nick      = NickName.Text;
            User.Info      = StringFromRichTextBox(Info);
            User.Interests = StringFromRichTextBox(Interests);
            User.Age       = DateTime.Now.Year - Age.DisplayDate.Year;

            string link;
            if (LinkToPicture != null)
            {
                link = $"{MainWindow.StorageLocation}{UserName.Text}_{NickName.Text}.jpg";
                File.Copy(LinkToPicture, link, true);
            }
            else link = MainWindow.DefaultLogo;
            
            User.ProfileImage = link;
            User.DateOfBirth  = Age.DisplayDate;
            MainWindow.User   = User;
            
            MainWindow.UpdateProfile();
            Close();
        }
        private void PictureChose(object sender, RoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog {
                Filter           = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*",
                FilterIndex      = 2,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() != true) return;
            
            LinkToPicture         = openFileDialog.FileName;
            PreviewPicture.Source = new BitmapImage(new Uri(LinkToPicture));
        }
        string StringFromRichTextBox(RichTextBox rtb) {
            TextRange textRange = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd
            );
            return textRange.Text;
        }
        private void ExtendedTest(object sender, RoutedEventArgs e) {
            new UserTesting(User).Show();
            Close();
        }
    }
}