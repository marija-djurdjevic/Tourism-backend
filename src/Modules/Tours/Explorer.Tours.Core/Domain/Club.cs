using AutoMapper.Execution;
using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Club : Entity
    {
        public int OwnerId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ImageId {  get; private set; }
        public List<int> MemberIds { get; private set;}
        public List<int> InvitationIds { get; private set;}
        public List<int> RequestIds { get; private set;}

        public Club(string name, string description, int imageId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Desc.");
            //if (string.IsNullOrWhiteSpace(imageId)) throw new ArgumentException("Invalid Image.");
            Name = name;
            Description = description;
            ImageId = imageId;
            MemberIds = new List<int>();
            InvitationIds = new List<int>();
            RequestIds = new List<int>();
        }

        // Add a new member
        public void AddMember(int userId)
        {
            if (!MemberIds.Contains(userId))
                MemberIds.Add(userId);
        }

        // Remove a member
        public void RemoveMember(int userId)
        {
            MemberIds.Remove(userId);
        }

        // Invite a user
        public void InviteUser(int userId)
        {
            if (!InvitationIds.Contains(userId))
            {
                InvitationIds.Add(userId);
            }
        }

        // Accept an invitation
        public void AcceptInvitation(int userId)
        {
            if (InvitationIds.Contains(userId))
            {
                InvitationIds.Remove(userId);
                AddMember(userId);
            }
        }

        // Reject an invitation
        public void RejectInvitation(int userId)
        {
            InvitationIds.Remove(userId);
        }

        public void RequestJoin(int userId)
        {
            if (!RequestIds.Contains(userId))
            {
                RequestIds.Add(userId);
            }
        }

        public void AcceptRequest(int userId)
        {
            if (!MemberIds.Contains(userId))
            {
                RequestIds.Remove(userId);
                MemberIds.Add(userId);
            }
            if (InvitationIds.Contains(userId))
            {
                InvitationIds.Remove(userId);
            }
        }

        public void DenyRequest(int userId)
        {
            RequestIds.Remove(userId);
        }
    }
}
