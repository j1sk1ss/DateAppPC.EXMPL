using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.DATA {
    public static class TestAnalyzer {
        private const string CharacterPath   = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Data\\Character.txt";
        private const string TemperamentPath = 
            "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Data\\Temperament.txt";
        public static string GetCharacter(List<string> answers, AnswerType answerType) {
            try {
                var path = answerType switch {
                    AnswerType.Temperament => TemperamentPath,
                    AnswerType.Character   => CharacterPath,

                    _ => TemperamentPath
                };
                if (!File.Exists(path)) return "null";
                
                using var streamReader = new StreamReader(path);
                var tempText = streamReader.ReadToEnd();

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
        public static string GetRole(string answer, Sex type, int quest) {
            return type switch {
                Sex.Man => answer switch {
                    "0" => quest == 7 ? "Подкаблучник" : "Хозяин",
                    "1" => quest == 7 ? "Добытчик"     : "Воин",
                    "2" => quest == 7 ? "Домохозяин"   : "Подарок",
                    "3" => quest == 7 ? "Тиран"        : "Авантюрист",
                    _   => "Куколд"
                },
                Sex.Woman => answer switch {
                    "0" => quest == 7 ? "Мать-курица"         : "Хозяйка",
                    "1" => quest == 7 ? "Мать-кукушка"        : "Воин",
                    "2" => quest == 7 ? "Мать-липучка"        : "Приз",
                    "3" => quest == 7 ? "Мать-интеллектуалка" : "Муза",
                    _   => "Куколдиха"
                },
                _ => "null"
            };
        }
        public enum AnswerType {
            Temperament,
            Character
        }
    }
}