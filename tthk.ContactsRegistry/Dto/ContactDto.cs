using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tthk.ContactsRegistry
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }
        public string Initials { get; set; }
    }

public class PhoneNumber
{
    public string Number { get; set; }
    public ContactType Type { get; set; }
    public Boolean Default { get; set; }
}
    public class Email
    {
        public string EmailAdress { get; set; }
        public ContactType Type { get; set; }
        public Boolean Default { get; set; }
    }
}
