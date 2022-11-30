using System.Windows;
using System.Windows.Controls;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.FUNCTIONS {
    public static class TestViewer {
        public static Grid GetTest(Quest quest) {
            var tempGrid = new Grid {
                Height = 180,
                Width = 720
            };
            for (var i = 0; i < quest.Answers.Count; i++) {
                tempGrid.Children.Add(new RadioButton {
                    Name = $"Answer_{i}",
                    FontSize = 15,
                    Content = quest.Answers[i],
                    Margin = new Thickness(10,20 + i * 40,0,0)
                });
            }
            return tempGrid;
        }
    }
}