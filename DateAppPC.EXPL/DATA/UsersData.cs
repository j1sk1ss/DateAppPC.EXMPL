using System.Collections.Generic;
using DateAppPC.EXPL.OBJECTS;

namespace DateAppPC.EXPL.DATA {
    public class UsersData {
        public UsersData() {
            Users = new List<User>();
        }
        public List<User> Users { get; set; }
    }
}