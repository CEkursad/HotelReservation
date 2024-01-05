using HotelReservation.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly TechCareerDBContext dBContext;

        public ClientController()
        {
            dBContext = new TechCareerDBContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clients = dBContext.Clients.Include(x => x.Company);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = dBContext.Clients.Include(x => x.Company).FirstOrDefault(x => x.Id == id);

            if (client == null)                
                return NotFound("id bulunamadi");
           
            else           
                return Ok(client);
        }

        [HttpPost]
        public IActionResult Post(Client client)
        {
            Company company = dBContext.Companies.FirstOrDefault(x => x.Id == client.CompanyId);
            if (company == null)
            {
                return NotFound("Sirket mevcut degil");
            }
            client.Company = company;
            client.CompanyId = company.Id;

            dBContext.Clients.Add(client);
            dBContext.SaveChanges();

            return Ok(client);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Client client)
        {
            var updateClient = dBContext.Clients.FirstOrDefault(x => x.Id == id);
            if (updateClient == null)
            {
                return NotFound("Musteri bulunamadi.");
            }
            updateClient.Name = client.Name;
            updateClient.Surname = client.Surname;
            updateClient.BirthDate = client.BirthDate;
            updateClient.Address = client.Address;
            updateClient.EMail = client.EMail;
            updateClient.CompanyId = client.CompanyId;
            dBContext.SaveChanges();
            return Ok(updateClient);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteClient = dBContext.Clients.FirstOrDefault(x => x.Id == id);
            if (deleteClient == null)
            {
                return NotFound("Musteri bulunamadi.");
            }
            dBContext.Clients.Remove(deleteClient);
            dBContext.SaveChanges();

            return Ok(deleteClient);
        }
    }
}
