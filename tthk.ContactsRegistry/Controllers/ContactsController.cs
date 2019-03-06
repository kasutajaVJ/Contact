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

            IQueryable<ContactEasy> seed = _context.ContactEasies;

            if (term != null)
            {
                if (!string.IsNullOrEmpty(term))
                {
                    seed = seed
                        .Where(x => x.Name.Contains(term)
                        || x.PhoneNumber.Contains(term)
                        || x.Email.Contains(term));

                }
            }

            var list = (await seed.ToArrayAsync())
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    defaultPhoneNumber = x.PhoneNumber,
                    defaultEmail = x.Email
                });

            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {

            IQueryable<ContactEasy> seed = _context.ContactEasies;

                seed = seed
                    .Where(x => x.Id.Equals(id));


            var list = (await seed.ToArrayAsync())
                .Select(x => new {
                    id = x.Id,
                    name = x.Name,
                    initials = x.Initials,
                    phoneNumber = x.PhoneNumber,
                    email = x.Email
                });

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] ContactEasy contact)
        {
            var contactData = await _contactsService.UpdateContact(contact.Id, contact);
            
            return Ok(GetDto(contactData));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] ContactEasy data)
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


    /*
    private ContactDto GetDto(Contact contact)
    {
        var phone = new PhoneNumber
        {

            Number = contact.PhoneNumber.Number,
            Type = contact.PhoneNumber.Type,
            Default = contact.PhoneNumber.Default,

        };

        return new ContactDto
        {
            Id = contact.Id,
            Name = contact.Name,
            PhoneNumber = new PhoneNumber
            {

                Number = contact.PhoneNumber.Number,
                Type = contact.PhoneNumber.Type,
                Default = contact.PhoneNumber.Default,

            },
            Email = new Email
            {

                    EmailAdress = contact.Email.EmailAdress,
                    Type = contact.Email.Type,
                    Default = contact.Email.Default,

            }
        };
    }
*/

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

