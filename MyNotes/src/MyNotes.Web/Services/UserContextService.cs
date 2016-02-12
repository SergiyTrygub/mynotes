using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Services
{
    public interface IUserContextService
    {
        Guid UserId { get; }
    }

    public class FakeUserContextService : IUserContextService
    {
        public FakeUserContextService(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
