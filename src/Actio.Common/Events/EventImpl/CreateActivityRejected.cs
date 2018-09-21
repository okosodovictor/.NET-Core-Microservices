using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events.EventImpl
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public Guid UserId { get;}
        public string Name { get;}
        public string Reason { get; }
        public string Code { get; }

        public CreateActivityRejected(Guid id, Guid userId, string name, string reason, string code)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Reason = reason;
            Code = code;
        }
    }
}
