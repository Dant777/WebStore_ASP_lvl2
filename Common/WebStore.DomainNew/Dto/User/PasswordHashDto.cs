using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.DomainNew.Dto.User
{
    public class PasswordHashDto
    {
        public Entities.User User { get; set; }
        public string Hash { get; set; }

    }
}
