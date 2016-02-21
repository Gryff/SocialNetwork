using System;

namespace SocialNetwork.Core
{
    public class Post
    {
        public string Content { get; set; }
        public DateTime PostedDateTime { get; set; }

        public Post() { }

        public Post(string content)
        {
            this.Content = content;
            this.PostedDateTime = DateTime.UtcNow;
        }

        public string ToOutputFormat()
        {
            string timeAgo = this.GetPostTimeSuffix(DateTime.UtcNow - this.PostedDateTime);

            return $"{this.Content} ({timeAgo})";
        }

        private string GetPostTimeSuffix(TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds < 60)
                return $"{Math.Round(timeSpan.TotalSeconds)} " + 
                    $"second{this.GetPlural(timeSpan.TotalSeconds)} ago";

            if(timeSpan.TotalMinutes < 60)
                return $"{Math.Round(timeSpan.TotalMinutes)} " +
                    $"minute{this.GetPlural(timeSpan.TotalMinutes)} ago";

            if (timeSpan.TotalHours < 24)
                return $"{Math.Round(timeSpan.TotalHours)} " +
                       $"hour{this.GetPlural(timeSpan.TotalHours)} ago";

            else
                return $"{Math.Round(timeSpan.TotalDays)} " +
                       $"day{this.GetPlural(timeSpan.TotalDays)} ago";
        }

        private string GetPlural(double number) => Math.Round(number) == 1 ? "" : "s";
    }
}
