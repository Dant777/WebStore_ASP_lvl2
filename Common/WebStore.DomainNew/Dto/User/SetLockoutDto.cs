using System;

namespace WebStore.DomainNew.Dto.User
{
    public class SetLockoutDto
    {
        public Entities.User User { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}