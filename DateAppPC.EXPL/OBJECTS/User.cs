using System;
using System.Collections.Generic;

namespace DateAppPC.EXPL.OBJECTS {
    public class User {
        public User()
        {
            Favorite = new List<int>();
            UsersFavorite = new List<int>();
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int? Age { get; set; }
        public Sex Sex { get; set; }
        public string Temperament { get; set; }
        public string Character { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfileImage { get; set; }
        public string Info { get; set; }
        public string Interests { get; set; }
        public List<int> Favorite { get; set; }
        public List<int> UsersFavorite { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public enum Sex {
        Man,
        Woman
    }
}