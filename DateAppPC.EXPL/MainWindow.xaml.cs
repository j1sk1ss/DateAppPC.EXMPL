using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.DATA;
using DateAppPC.EXPL.DATA_BASE;
using DateAppPC.EXPL.FUNCTIONS;
using DateAppPC.EXPL.OBJECTS;
using DateAppPC.EXPL.WINDOWS;
using Newtonsoft.Json;

namespace DateAppPC.EXPL {
    public partial class MainWindow : Window {
        private const string DefaultLogo = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\IMAGES\\default.jpg";
        public MainWindow() {
            InitializeComponent();
            UsersData = new UsersData {
                Users = new Connector().GetUsersData()
            };
            User      = new User();
        }
        public UsersData UsersData { get; }
        public User User { get; set; }
        private void Enter(object sender, RoutedEventArgs e) {
            foreach (var user in UsersData.Users.Where(user => user.Login == Login.Text)) {
                if (user.Password != Password.Text) {
                    MessageBox.Show("Неверный пароль!");
                    return;
                }
                User = user;
                UpdateProfile();
                CloseRegistration();
                return;
            }
            var k = MessageBox.Show("Аккаунт не зарегистрирован!", 
                "Регистрация?", MessageBoxButton.OKCancel);
            if (k != MessageBoxResult.OK) return;
            
            User = new User {
                Password = Password.Text,
                Login    = Login.Text
            };
            CloseRegistration();
        }
        public void UpdateProfile() {
            ProfileImage.Source = new BitmapImage(new Uri(User.ProfileImage ?? DefaultLogo));
            UserName.Content    = User.Name;
            NickName.Content    = User.Nick;
            Age.Content         = (DateTime.Now.Year - User.DateOfBirth.Year).ToString();
        }
        private void SetProfile(object sender, RoutedEventArgs e) {
            new UserInfo(this, User).Show();
        }
        private void CloseRegistration() {
            try {
                Registration.Visibility   = Visibility.Hidden;
                Profile.Visibility        = Visibility.Visible;
                ScrollProfiles.Visibility = Visibility.Visible;
                Filters.Visibility        = Visibility.Visible;
            
                ProfilesGrid.Height = 200 * UsersData.Users.Count;
                for (var i = 0; i < UsersData.Users.Count; i++) {
                    var user = UsersData.Users[i];
                    if (user == User) continue;
                    ProfilesGrid.Children.Add(ProfilesViewer.GetProfileCanvas(user, this));
                
                    var tempUserTemple = ProfilesGrid.Children[^1] as Canvas;
                    tempUserTemple!.VerticalAlignment = VerticalAlignment.Top;
                    tempUserTemple!.Margin = new Thickness(0, i * 400, 0, 0);
                }
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        private void AppClosed(object sender, EventArgs e) {
            if (!UsersData.Users.Contains(User)) new Connector().SaveUserData(User);
            else new Connector().ChangeUserData(User);
        }
        public void AddToFavorite(object sender, RoutedEventArgs e) {
            var name = (sender as Button)!.Name;
            foreach (var user in UsersData.Users.Where(user => user.Name == name)) {
                user.UsersFavorite.Add(User.UserId);
                User.Favorite.Add(user.UserId);
            }
        }
    }
}