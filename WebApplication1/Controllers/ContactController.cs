using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataDbcontext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactDbcontext dbcontext;

        public ContactController(ContactDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }





        [HttpGet]

        public async Task<IActionResult> GetContact()
        {
            var Contact = await dbcontext.contacts.ToListAsync();
            return Ok(Contact);

        }




        [HttpGet("namesAndphonenumber")]
        public async Task<IActionResult> GetNamesAndPhonenumber()
        {
            // Get a list of objects with only "name" and "number" fields
            var namesAndphonenumber = await dbcontext.contacts.Select(contact => new
            {
                contact.Name,
                contact.PhoneNumber
            }).ToListAsync();

            return Ok(namesAndphonenumber);
        }




        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> Getcontact(Guid id)
        {
            var Contact = await dbcontext.contacts.FindAsync(id);
            if (Contact != null)
            {
                return Ok(Contact);
            }
            return NotFound();

        }






        [HttpPost]

        public async Task<IActionResult> Add(AddContact addcontact)
        {
            var Contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addcontact.Name,
                PhoneNumber = addcontact.PhoneNumber
            };

            await dbcontext.contacts.AddAsync(Contact);
            await dbcontext.SaveChangesAsync();
            return Ok(Contact);

        }







        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateContact(Guid id, UpdateContact updatecontact)
        {
            var Contact = await dbcontext.contacts.FindAsync(id);

            if (Contact != null) 
            {
          
                Contact.Name = updatecontact.Name;
                Contact.PhoneNumber = updatecontact.PhoneNumber;

                
                await dbcontext.SaveChangesAsync();
                return Ok(Contact);

            }

            return NotFound();

        }







        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await dbcontext.contacts.FindAsync(id);

            if (contact != null)
            {
                dbcontext.Remove(contact);
                await dbcontext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }








    }
}
