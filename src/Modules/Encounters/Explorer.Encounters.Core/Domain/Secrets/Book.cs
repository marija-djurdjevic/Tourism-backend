using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Secrets
{
    public class Book : Entity
    {

        public string Title { get; private set; }
        public int AdminId { get; private set; }
        public int PageNum { get; private set; }

        public string BookColour { get; private set; }

        public Book() { }

        public Book(string title, int adminId, int pageNum, string bookColour)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Invalid Title.");

            Title = title;
            AdminId = adminId;
            PageNum = pageNum;
            BookColour = bookColour;
        }

    }
}
