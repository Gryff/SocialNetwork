namespace SocialNetwork.Core
{
    public class User
    {
        public string Name { get; set; }

        public Timeline Timeline { get; } = new Timeline();

        public User(string name)
        {
            this.Name = name;
        }

    }
}
