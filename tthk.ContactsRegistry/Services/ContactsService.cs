using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tthk.ContactsRegistry;
using tthk.ContactsRegistry.Data;

namespace tthk.ContactsRegistry.Services
{
    public class ContactsService : IContactsService
    {
        public ContactsService(ContactsContext context)
        {
            this._context = context;
        }

        private readonly ContactsContext _context;
        public async Task DeleteItem(Guid id)
        {
            var item = await _context.Contacts.FindAsync(id);
            await DeleteItem(item);
        }

        public async Task DeleteItem(Contact item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Contact> UpdateContact(Guid id, Contact contact)
        {
            var contactUpdate = await _context.Contacts
                .FirstOrDefaultAsync(x => x.Id == id);

            
            contactUpdate.Name = contact.Name;
            contactUpdate.Emails = contact.Emails;
            contactUpdate.PhoneNumbers = contact.PhoneNumbers;
            contactUpdate.Initials = contact.Initials;
            

            await _context.SaveChangesAsync();

            return contactUpdate;
        }
    }
}

public interface IContactsService
{
    Task DeleteItem(Guid id);
    Task<Contact> UpdateContact(Guid id, Contact contact);
}
