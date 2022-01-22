using System;

namespace InMemoryLoggerAndProvider
{
    internal struct BlogPost
    {
        public string Title { get; }
        public string Content { get; }
        public DateTime Date { get; }
        public string PostedBy { get; }
        public string[] Categories { get; }
        public string[] Tags { get; }

        public BlogPost(string title, string content, DateTime date, string postedBy, string[] categories, string[] tags)
        {
            Title = title;
            Content = content;
            Date = date;
            PostedBy = postedBy;
            Categories = categories;
            Tags = tags;
        }
    }
}
