using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.FUNCTIONS {
    public class ProfilesViewer {
        private const string DefaultLogo = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\IMAGES\\default.jpg";
        public Canvas ShowProfile(User user, MainWindow mainWindow) {
            var tempCanvas = new Canvas() {
                Background = Brushes.Beige,
                Width      = 500,
                Height     = 300,
                Children   = {
                    new Image() {
                       Source  = new BitmapImage(new Uri(user.ProfileImage ?? DefaultLogo)),
                       Height  = 300,
                       Width   = 300,
                       Margin  = new Thickness(0,0,200,0),
                       Stretch = Stretch.Fill
                    },
                    new Label() {
                        FontSize = 20,
                        Margin   = new Thickness(300,0,0,0),
                        Content  = user.Name + ""
                    },
                    new Label() {
                        FontSize = 15,
                        Margin   = new Thickness(300,0,0,0),
                        Content  = "\n \nИнтересы:\n" + string.Join(",", user.Interests) + 
                                  "\n \n \n \n О себе:\n" + user.Info
                    }
                }
            };
            var tempButton = new Button() {
                Name    = user.Name,
                Height  = 25,
                Width   = 50,
                Margin  = new Thickness(450, 275, 0, 0),
                Content = "ЛАЙК!"
            };
            tempButton.Click += mainWindow.Like;
            tempCanvas.Children.Add(tempButton);
            return tempCanvas;
        }
    }
}