using System.Collections.Generic;

namespace DateAppPC.EXPL.OBJECTS {
    public class Quest {
        public Quest() {
            Answers = new List<string>();
        }
        public string QuestBody { get; init; }
        public List<string> Answers { get; }
    }
}