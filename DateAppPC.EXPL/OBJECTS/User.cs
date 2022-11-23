using System;
using System.Collections.Generic;

namespace DateAppPC.EXPL.OBJECTS
{
    public class User
    {
        public User() {
            Interests = new List<string>();
            Chosen = new List<User>();
            UsersChose = new List<User>();
        }
        public string Name { get; set; }
        public string Nick { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfileImage { get; set; }
        public string Info { get; set; }
        public List<string> Interests { get; set; }
        
        public List<User> Chosen { get; set; }
        public List<User> UsersChose { get; set; }
        
        public string Login { get; set; }
        public string Password { get; set; }
    }
}