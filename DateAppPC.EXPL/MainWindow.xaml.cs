using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.DATA;
using DateAppPC.EXPL.FUNCTIONS;
using DateAppPC.EXPL.OBJECTS;
using DateAppPC.EXPL.WINDOWS;
using Newtonsoft.Json;

namespace DateAppPC.EXPL {
    public partial class MainWindow : Window {
        private const string DataBaseLocation = 
                        "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\DATA\\DataBase.json";
        private const string DefaultLogo = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\IMAGES\\default.jpg";
        public MainWindow() {
            InitializeComponent();
            UsersData = new UsersData();
            User      = new User();
            
            if (!File.Exists(DataBaseLocation)) return;
            
            UsersData = JsonConvert.DeserializeObject<UsersData>(File.ReadAllText(DataBaseLocation));
        }
        public UsersData UsersData { get; set; }
        public User User { get; set; }
        private void Enter(object sender, RoutedEventArgs e) {
            foreach (var user in UsersData.Users.Where(user => user.Login == Login.Text)) {
                if (user.Password != Password.Text) {
                    MessageBox.Show("Неверный пароль!");
                    return;
                }
                User = user;
                UsersData.Users.Remove(user);
                UpdateProfile();
                CloseRegistration();
                return;
            }
            var k = MessageBox.Show("Аккаунт не зарегистрирован!", 
                "Регистрация", MessageBoxButton.OKCancel);
            if (k != MessageBoxResult.OK) return;
            
            User = new User() {
                Password = Password.Text,
                Login = Login.Text
            };
            UsersData.Users.Add(User);
            CloseRegistration();
        }
        public void Like()
        {
            
        }
        public void UpdateProfile() {
            ProfileImage.Source = new BitmapImage(new Uri(User.ProfileImage ?? DefaultLogo));
            UserName.Content    = User.Name;
            NickName.Content    = User.Nick;
            Age.Content         = (DateTime.Now.Year - User.DateOfBirth.Year).ToString();
        }
        private void SetProfile(object sender, RoutedEventArgs e) { 
           UsersData.Users.Remove(User);
           new UserInfo(this, User).Show();
        }
        private void CloseRegistration() {
            Registration.Visibility   = Visibility.Hidden;
            Profile.Visibility        = Visibility.Visible;
            ScrollProfiles.Visibility = Visibility.Visible;
            Filters.Visibility        = Visibility.Visible;

            try {
                var profilesViewer = new ProfilesViewer();
                ProfilesGrid.Height = 400 * UsersData.Users.Count;
                for (var i = 0; i < UsersData.Users.Count; i++) {
                    var user = UsersData.Users[i];
                    if (user == User) continue;
                    ProfilesGrid.Children.Add(profilesViewer.ShowProfile(user, this));
                    (ProfilesGrid.Children[^1] as Canvas)!.VerticalAlignment = VerticalAlignment.Top;
                    (ProfilesGrid.Children[^1] as Canvas)!.Margin = new Thickness(0,50 + i * 400,0,0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                throw;
            }

        }
        private void AppClosed(object sender, EventArgs e) {
            UsersData.Users.Add(User);
            File.WriteAllText(DataBaseLocation, JsonConvert.SerializeObject(UsersData));
        }

        public void Like(object sender, RoutedEventArgs e)
        {
            
        }
    }
}