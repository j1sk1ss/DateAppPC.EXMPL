using System;
using System.Collections.Generic;

namespace DateAppPC.EXPL.OBJECTS {
    public class User {
        public User() {
            Favorite     = new List<string>();
            UserId       = new Random().Next();
            Age          = 1;
            ProfileImage = 
                "C:\\Users\\j1sk1ss\\RiderProjects\\DateAppPC.EXPL\\DateAppPC.EXPL\\IMAGES\\default.jpg";
        }
        public int UserId { get; init; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int? Age { get; set; }
        public Sex Sex { get; set; }
        public string Temperament { get; set; }
        public string Character { get; set; }
        public string Role { get; set; }
        public string Type { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfileImage { get; set; }
        public string Info { get; set; }
        public string Interests { get; set; }
        public List<string> Favorite { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }
    }
    public enum Sex {
        Man,
        Woman
    }
}