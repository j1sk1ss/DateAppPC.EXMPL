using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.DATA {
    public static class TestAnalyzer {
        private const string CharacterPath = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Character.txt";
        private const string TemperamentPath = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Temperament.txt";
        public static string GetCharacter(List<string> answers, AnswerType answerType) {
            try {
                var path = answerType switch {
                    AnswerType.Temperament => TemperamentPath,
                    AnswerType.Character   => CharacterPath,

                    _ => TemperamentPath
                };
                if (!File.Exists(path)) return "null";

                var tempText = "";
                using var streamReader = new StreamReader(path);
                tempText = streamReader.ReadToEnd();

                var tempLines = tempText.Split("\n");
                foreach (var line in tempLines) {
                    var symbols = line.Split(" ").ToList().GetRange(answerType switch {
                        AnswerType.Temperament => 1,
                        AnswerType.Character   => 0,
                        
                        _ => 1
                    }, answers.Count);
                    
                    if (string.Join("", symbols.ToArray()) == string.Join("", answers.ToArray())) 
                        return line.Split(" ")[^1].Remove(line.Split(" ")[^1].Length - 1);
                }
            
                return "null";
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
                return "null";
            }
        }
        public static string GetRole(string seven, Sex type) {
            return type switch {
                Sex.Man => seven switch {
                    "0" => "Подкаблучник",
                    "1" => "Добытчик",
                    "2" => "Домохозяин",
                    "3" => "Тиран",
                    _   => "Куколд"
                },
                Sex.Woman => seven switch {
                    "0" => "Мать-курица",
                    "1" => "Мать-кукушка",
                    "2" => "Мать-липучка",
                    "3" => "Мать-интеллектуалка",
                    _   => "Куколдиха"
                },
                _ => "null"
            };
        }

        public static string GetType(string eight, Sex type) {
            return type switch {
                Sex.Man => eight switch {
                    "0" => "Хозяин",
                    "1" => "Воин",
                    "2" => "Подарок",
                    "3" => "Авантюрист",
                    _   => "Куколд"
                },
                Sex.Woman => eight switch {
                    "0" => "Хозяйка",
                    "1" => "Воин",
                    "2" => "Приз",
                    "3" => "Муза",
                    _   => "Куколдиха"
                },
                _ => "null"
            };
        }
        public enum AnswerType {
            Temperament,
            Character
        }
        public static string GetWish() => "ВИСХ";
    }
}