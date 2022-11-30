using System.IO;
using System.Linq;
using System.Windows;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.DATA {
    public static class Matcher {
        private const string CharacterMatch
            = "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Matching\\CharacterMatch.txt";
        private const string FamilyMatch
            = "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Matching\\FamilyMatch.txt";
        private const string RoleMatch
            = "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Matching\\RoleMatch.txt";
        private const string TemperamentMatch
            = "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\QUESTS\\Matching\\TemperamentMatch.txt";
        
        public static bool IsMatch(User firstUser, User secondUser) {
            return Match(firstUser.Character, secondUser.Character, CharacterMatch) +
                Match(firstUser.Type, secondUser.Type,FamilyMatch) +
                Match(firstUser.Role, secondUser.Role, RoleMatch) +
                Match(firstUser.Temperament, secondUser.Temperament,TemperamentMatch) >= 1;
        }
        private static int Match(string first, string second, string path) {
            if (!File.Exists(path)) return 0;

            var text = "";
            using var streamReader = new StreamReader(path);
            text = streamReader.ReadToEnd();

            var lines = text.Split("\n");

            return lines.Any(line => line == $"{first.Replace("\n", "")}" +
                $" {second.Replace("\n", "")}") ? 1 : 0;
        }
    }
}