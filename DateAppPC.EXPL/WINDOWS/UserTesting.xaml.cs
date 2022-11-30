using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DateAppPC.EXPL.DATA;
using DateAppPC.EXPL.FUNCTIONS;
using DateAppPC.EXPL.OBJECTS;
using DateAppPC.EXPL.QUESTS;

namespace DateAppPC.EXPL.WINDOWS {
    public partial class UserTesting : Window {
        public UserTesting(User user) {
            InitializeComponent();
            User      = user;
            Questions = new List<Quest>();
            Answers   = new List<string>();
        }
        private User User { get; set; }
        private List<Quest> Questions { get; set; }
        private List<string> Answers { get; set; }
        private void StartTest(object sender, RoutedEventArgs e) {
            try {
                User.Surname    = Surname.Text;
                User.Name       = Name.Text;
                User.Patronymic = Patronymic.Text;
                User.Age        = int.Parse(Age.Text);
                
                User.Sex = Sex.Text switch {
                    "Мужчина" => OBJECTS.Sex.Man,
                    "Женщина" => OBJECTS.Sex.Woman,
                    _ => OBJECTS.Sex.Man
                };
                Questions = GetQuests.GetQuestList(User.Sex);

                RegistrationForm.Visibility = Visibility.Hidden;
                NextQuest(null, null);
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
                throw;
            }
        }
        
        private int _questPosition;
        private void NextQuest(object sender, RoutedEventArgs routedEventArgs) {
            try {
                
                if (AnswerGrid.Children[^1].GetType() == typeof(Grid)) {
                    foreach (var answer in (AnswerGrid.Children[^1] as Grid)!.Children) {
                        if (answer.GetType() != typeof(RadioButton)) continue;
                        var button = answer as RadioButton;
                        
                        if (button!.IsChecked != true) continue;
                        Answers.Add(button.Name[^1].ToString());

                        break;
                    }
                }
                
                if (_questPosition > Questions.Count - 1) {
                    User.Temperament =
                        TestAnalyzer.GetCharacter(Answers.GetRange(0, 4), TestAnalyzer.AnswerType.Temperament);
                    User.Character   =
                        TestAnalyzer.GetCharacter(Answers.GetRange(5, 2), TestAnalyzer.AnswerType.Character);
                    User.Role        = TestAnalyzer.GetRole(Answers[6], User.Sex);
                    User.Type        = TestAnalyzer.GetType(Answers[7], User.Sex);
                    
                    MessageBox.Show($"Тест пройден: \n" +
                                    $"Темперамент: {User.Temperament}" +
                                    $"\nТип характера: {User.Character}" +
                                    $"\nВаша роль: {User.Role}" +
                                    $"\nВаша роль: {User.Type}");
                    Close();
                    return;
                }
                
                Quest.Content = Questions[_questPosition].QuestBody;
                AnswerGrid.Children.Clear();
                AnswerGrid.Children.Add(TestViewer.GetTest(Questions[_questPosition++]));
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
                throw;
            }
        }
    }
}