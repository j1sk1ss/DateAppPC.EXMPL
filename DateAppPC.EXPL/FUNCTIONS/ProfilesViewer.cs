using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.FUNCTIONS {
    public static class ProfilesViewer {
        private const string DefaultLogo = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\IMAGES\\default.jpg";
        public static Canvas GetProfileCanvas(User user, MainWindow mainWindow) {
            var tempCanvas = new Canvas {
                Width      = 500,
                Height     = 300,
                Children   = {
                    new Image {
                       Source  = new BitmapImage(new Uri(user.ProfileImage ?? DefaultLogo)),
                       Height  = 300,
                       Width   = 300,
                       Margin  = new Thickness(0,0,200,0),
                       Stretch = Stretch.Fill
                    },
                    new Label {
                        FontSize = 20,
                        Margin   = new Thickness(300,0,0,0),
                        Content  = user.Name + ""
                    },
                    new Label {
                        FontSize = 15,
                        Margin   = new Thickness(300,0,0,0),
                        Content  = "\n \nИнтересы:\n" + string.Join(",", user.Interests) + 
                                  "\n \n \n \n О себе:\n" + user.Info
                    },
                    new Line {
                        X1 = 500,
                        X2 = 500,
                        Y1 = 0,
                        Y2 = 300,
                        Stroke = Brushes.Black
                    },
                    new Line {
                        X1 = 0,
                        X2 = 0,
                        Y1 = 0,
                        Y2 = 300,
                        Stroke = Brushes.Black
                    },
                    new Line {
                        X1 = 0,
                        X2 = 500,
                        Y1 = 0,
                        Y2 = 0,
                        Stroke = Brushes.Black
                    },
                    new Line {
                        X1 = 0,
                        X2 = 500,
                        Y1 = 300,
                        Y2 = 300,
                        Stroke = Brushes.Black
                    },
                }
            };
            var tempButton = new Button() {
                Name    = $"button_{user.Name}",
                Height  = 25,
                Width   = 50,
                Margin  = new Thickness(450, 275, 0, 0),
                Content = "ЛАЙК!"
            };
            tempButton.Click += mainWindow.AddToFavorite;
            tempCanvas.Children.Add(tempButton);
            return tempCanvas;
        }
    }
}