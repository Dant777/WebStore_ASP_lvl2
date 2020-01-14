using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DomainNew.Dto.User
{
    public class SetLockoutDto
    {
        public Entities.User User { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }

    }
}
