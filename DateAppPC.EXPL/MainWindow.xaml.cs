using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.DATA;
using DateAppPC.EXPL.OBJECTS;
using DateAppPC.EXPL.WINDOWS;
using Newtonsoft.Json;

namespace DateAppPC.EXPL {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            UsersData = new UsersData();
            if (!File.Exists("C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\DATA\\DataBase.json")) return;
            UsersData = JsonConvert.DeserializeObject<UsersData>(File.ReadAllText("C:\\Users\\j1sk1ss\\" +
                "RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\DATA\\DataBase.json"));
            User = new User();
        }
        private UsersData UsersData { get; set; }
        public User User { get; set; }
        private void Enter(object sender, RoutedEventArgs e) {
            foreach (var user in UsersData.Users.Where(user => user.Login == Login.Text)) {
                if (user.Password != Password.Text) MessageBox.Show("Неверный пароль!");
                User = user;
                UsersData.Users.Remove(user);
                CloseRegistration();
                UpdateProfile();
                return;
            }
            var k =MessageBox.Show("Аккаунт не зарегистрирован!", "Регистрация", MessageBoxButton.OKCancel);
            if (k != MessageBoxResult.OK) return;
            
            User = new User() {
                Password = Password.Text,
                Login = Login.Text
            };
            UsersData.Users.Add(User);
            
            CloseRegistration();
        }
        public void UpdateProfile() {
            ProfileImage.Source = new BitmapImage(new Uri(User.ProfileImage));
            UserName.Content = User.Name;
            NickName.Content = User.Nick;
            Age.Content = (DateTime.Now.Year - User.DateOfBirth.Year).ToString();
        }
        private void SetProfile(object sender, RoutedEventArgs e) { 
           UsersData.Users.Remove(User);
           new UserInfo(this, User).Show();
        }
        private void CloseRegistration() {
            Registration.Visibility = Visibility.Hidden;
            Profile.Visibility = Visibility.Visible;
            ScrollProfiles.Visibility = Visibility.Visible;
            Filters.Visibility = Visibility.Visible;
        }
        private void AppClosed(object sender, EventArgs e) {
            UsersData.Users.Add(User);
            File.WriteAllText("C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\" +
                              "DateAppPC.EXPL\\DATA\\DataBase.json", JsonConvert.SerializeObject(UsersData));
        }
    }
}