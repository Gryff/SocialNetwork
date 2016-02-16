using System.Collections.Generic;

namespace SocialNetwork.Core
{
    public class User
    {
        public string Name { get; set; }

        public Timeline Timeline { get; } = new Timeline();

        public List<User> Following = new List<User>();

        public User(string name)
        {
            this.Name = name;
        }
    }
}
