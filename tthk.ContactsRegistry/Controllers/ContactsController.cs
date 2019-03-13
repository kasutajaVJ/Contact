using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tthk.ContactsRegistry.Data;

namespace tthk.ContactsRegistry.Controllers
{
    public class ContactsController : ApiControllerBase
    {
        private readonly ContactsContext _context;
        private readonly IContactsService _contactsService;

        public ContactsController(
            ContactsContext context,
            IContactsService contactsService,
            ILogger<ContactsController> logger)
        {
            _context = context;
            _contactsService = contactsService;
            logger.LogDebug("List items controller created");
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts([FromQuery] string term)
        {

            IQueryable<Contact> seed = _context.Contacts;

            
                if (!string.IsNullOrEmpty(term))
                {
                    seed = seed
                        .Where(x => x.Name.Contains(term)
                        || x.PhoneNumbers.Select(number => number.Number.Contains(term) && number.IsDefault).FirstOrDefault()
                        || x.Emails.Select(email => email.Email.Contains(term) && email.IsDefault).FirstOrDefault());

                }
            

            var list = await seed
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    defaultPhoneNumber = x.PhoneNumbers.OrderByDescending(number => number.IsDefault).FirstOrDefault().Number,
                    defaultEmail = x.Emails.OrderByDescending(email => email.IsDefault).FirstOrDefault().Email
                }).ToArrayAsync();

            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {

            IQueryable<Contact> seed = _context.Contacts;

            seed = seed
                .Where(x => x.Id.Equals(id));


            var list = await seed
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    initials = x.Initials,
                    phoneNumbers = 
                new
                {
                    number = x.PhoneNumbers.Select(n => n.Number),
                    isDefault = x.PhoneNumbers.Select(n => n.IsDefault),
                    type = x.PhoneNumbers.Select(n => n.Type),
                },
                    emails = 
                        new {
                            email = x.Emails.Select(n => n.Email),
                            isDefault = x.Emails.Select(n => n.IsDefault),
                            type = x.Emails.Select(n => n.Type),
                        }
                }).ToArrayAsync();

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] Contact contact)
        {
            var contactData = await _contactsService.UpdateContact(contact.Id, contact);

            return Ok(new
            {
                id = contact.Id,
                name = contact.Name,
                defaultPhoneNumber = contact.PhoneNumbers.FirstOrDefault(phone => phone.IsDefault)?.Number,
                defaultEmail = contact.Emails.FirstOrDefault(email => email.IsDefault)?.Email
            });
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] Contact data)
        {

            _context.Add(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contactsService.DeleteItem(id);
            return Ok(true);
        }

        private ContactShowDto GetDto(ContactEasy contact)
        {
            return new ContactShowDto
            {
                Id = contact.Id,
                Name = contact.Name,
                DefaultPhoneNumber = contact.PhoneNumber,
                DefaultEmail = contact.Email,

            };

        }

    }





    public class ContactShowDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DefaultPhoneNumber { get; set; }
        public string DefaultEmail { get; set; }
    }

    public class SearchData<T> : SearchData
    {

        public T Dto { get; set; }
    }

    public class SearchData
    {
        public string SearchTerm { get; set; }
    }
}

