using System.Collections.Generic;
using System.Windows;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.WINDOWS {
    public partial class UserTesting : Window {
        public UserTesting(User user) {
            InitializeComponent();
            User = user;
        }
        private User User { get; set; }
        private List<Quest> Questions { get; set; }
        private void StartTest(object sender, RoutedEventArgs e) {
            User.Surname    = Surname.Text;
            User.Name       = Name.Text;
            User.Patronymic = Patronymic.Text;
            User.Age        = int.Parse(Age.Text);
            
            RegistrationForm.Visibility = Visibility.Hidden;
        }

        private void NextQuest() {
            
        }
    }
}