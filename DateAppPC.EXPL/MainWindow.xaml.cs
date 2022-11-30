using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.DATA;
using DateAppPC.EXPL.DATA_BASE;
using DateAppPC.EXPL.FUNCTIONS;
using DateAppPC.EXPL.OBJECTS;
using DateAppPC.EXPL.WINDOWS;

namespace DateAppPC.EXPL {
    public partial class MainWindow {
        public const string StorageLocation =
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\IMAGES\\";

        public readonly string DefaultLogo = $"{StorageLocation}default.jpg";
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
                
                ShowCard(UsersData.Users);
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        private void ShowCard(IEnumerable<User> users) {
            var list = new List<User>(users);
            
            list.Remove(User);
            ProfilesGrid.Height = 400 * list.Count + 50;
            
            ProfilesGrid.Children.Clear();
            for (var i = 0; i < list.Count; i++) {
                var user = list[i];

                ProfilesGrid.Children.Add(ProfilesViewer.GetProfileCanvas(user, this));
                
                var tempUserTemple = ProfilesGrid.Children[^1] as Canvas;
                tempUserTemple!.VerticalAlignment = VerticalAlignment.Top;
                tempUserTemple!.Margin = new Thickness(0, 100 + i * 400, 0, 0);
            }
        }
        private void AppClosed(object sender, EventArgs e) {
            if (!UsersData.Users.Contains(User)) new Connector().SaveUserData(User);
            else new Connector().ChangeUserData(User);
        }
        public void AddToFavorite(object sender, RoutedEventArgs e) {
            var name = (sender as Button)!.Name;

            foreach (var user in UsersData.Users.Where(user => name.Contains(user.UserId.ToString()))) {
                User.Favorite.Add(user.UserId.ToString());
                break;
            }
        }
        private void ShowFavorites(object sender, RoutedEventArgs e) {
            var sortedUsers = UsersData.Users.Where(user => User.Favorite.Contains(user.UserId.ToString())).ToList();
            ShowCard(sortedUsers);
        }
        private void ShowCharacterMatches(object sender, RoutedEventArgs e) {
            try {
                var sortedList = UsersData.Users.Where(user => Matcher.IsMatch(User, user)).ToList();
                ShowCard(sortedList);
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        private void ShowAll(object sender, RoutedEventArgs e) => ShowCard(UsersData.Users);
    }
}