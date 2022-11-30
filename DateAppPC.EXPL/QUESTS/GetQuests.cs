using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.QUESTS {
    public static class GetQuests {
        public static List<Quest> GetQuestList(Sex type) {
            try {
                const string questPath = 
                    "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Data\\Questions.txt";
                var quests = new List<Quest>();

                if (!File.Exists(questPath)) return quests;
                var text = "";
                
                using var streamReader = new StreamReader(questPath);
                text = streamReader.ReadToEnd();
                
                var tempBodyList = text.Split("/").ToList();
                tempBodyList.Remove("");

                foreach (var questBody in tempBodyList) {
                    var tempQuestLines = questBody.Split("\n").ToList();
                    
                    if (tempQuestLines.Count < 8) continue;
                    
                    if (tempQuestLines[1].Contains("ж") switch {
                            true => Sex.Woman,
                            false => Sex.Man
                        } != type) continue;
                    
                    var quest = new Quest {
                        QuestBody = tempQuestLines[3]
                    };

                    for (var i = 4; i < 8; i++) quest.Answers.Add(tempQuestLines[i]);
                    quests.Add(quest);
                }

                return quests;
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
                return new List<Quest>();
            }
        }
    }
}