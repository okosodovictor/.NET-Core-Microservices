using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events.EventImpl
{
    public class ActivityCreated : IAuthenticatedEvent
    {
        public Guid UserId { get; set; }
        public Guid Id { get; }
        public string Name { get;}
        public string Category { get;}
        public string Description { get; }
        public DateTime CreatedAt { get;}

        protected ActivityCreated()
        {

        }

        public ActivityCreated(Guid userId, Guid id, string name, string category, string description, DateTime createdAt)
        {
            UserId = userId;
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
