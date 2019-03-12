using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tthk.ContactsRegistry.Data
{
    public class ContactEasy : IEntityWithTypedId<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Initials { get; set; }
    }

    /*public class Contact : IEntityWithTypedId<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual Email Email { get; set; }
        public string Initials { get; set; }
    }

    public class PhoneNumber
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public string Number { get; set; }
        public ContactType Type { get; set; }
        public Boolean Default { get; set; }
    }

    public class Email
    {
        public Guid Id { get; set; }
        public string EmailAdress { get; set; }
        public ContactType Type { get; set; }
        public Boolean Default { get; set; }
    }*/
}

/*public enum ContactType {
    Home = 1,
    Work = 2,
}*/
