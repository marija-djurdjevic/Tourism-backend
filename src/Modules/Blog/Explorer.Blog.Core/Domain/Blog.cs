using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Blog.Core.Domain
{
    public enum BlogStatus
    {
        Draft,
        Published,
        Closed,
        Active,
        Famous
    }

    //[Table("Blogs", Schema = "blog")] 

    public class Blogs: Entity
    {
        //[ForeignKey("User")]
        public long AuthorId { get; private set; }
        //public List<Comment>? Comments { get; private set; }
        public List<Vote>? Votes { get; private set; }
        //public User User { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string? Image { get; private set; }
        public BlogStatus Status { get; private set; }

        public Blogs(long authorId, string title, string description, DateTime creationDate, BlogStatus status, string? image = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            AuthorId = authorId;
            Title = title;
            Description = description;
            CreationDate = creationDate;
            Image = image;
            Status = status;
        }
        public Blogs(long authorId, List<Vote>? votes, string title, string description, DateTime creationDate, BlogStatus status, string? image = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid title.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description.");
            AuthorId = authorId;
            Votes = votes;
            Title = title;
            Description = description;
            CreationDate = creationDate;
            Image = image;
            Status = status;
        }

        public void AddVote(Vote newVote)
        {
            if (Votes == null)
                Votes = new List<Vote>();

            var vote = Votes.FirstOrDefault(v => v?.AuthorId == newVote.AuthorId);

            if (vote == null)
                Votes.Add(vote);
            else
            {
                vote.CreationDate = newVote.CreationDate;
                vote.Value = newVote.Value;
            }

            CheckStatus();
        }

        private void CheckStatus()
        {
            int badVotes = 0;
            int goodVotes = 0;
            foreach (Vote vote in Votes)
            {
                if (vote.Value == false) { badVotes++; }
                else { goodVotes++; }
            }
            int votes = goodVotes - badVotes;

            if (votes < -10) { Status = BlogStatus.Closed; }
            else if (votes > 100) { Status = BlogStatus.Active; }
            else if (votes > 500) { Status = BlogStatus.Famous; }

        }

        public void RemoveVote(long authorId)
        {
            if (Votes == null || !Votes.Any())
                throw new InvalidOperationException("No votes available to remove.");

            var vote = Votes.FirstOrDefault(v => v.AuthorId == authorId);

            if (vote == null)
                throw new ArgumentException($"Vote by author with ID {authorId} does not exist.");

            Votes.Remove(vote);
        }

        public List<Vote> GetAllVotes()
        {
            return Votes ?? new List<Vote>();
        }
    }
}
