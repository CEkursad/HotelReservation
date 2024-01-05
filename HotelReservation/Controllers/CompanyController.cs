using HotelReservation.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly TechCareerDBContext dBContext;

        public CompanyController()
        {
            dBContext = new TechCareerDBContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var companies = dBContext.Companies;
            return Ok(companies);
        }

        [HttpPost]
        public IActionResult Post(Company company)
        {
            dBContext.Companies.Add(company);
            dBContext.SaveChanges();
            return Ok(company);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Company company)
        {
            var updateCompany = dBContext.Companies.FirstOrDefault(x => x.Id == id);
            if (updateCompany == null)
            {
                return NotFound("Sirket bulunamadi.");
            }
            updateCompany.Name = company.Name;
            updateCompany.Address = company.Address;
            dBContext.SaveChanges();
            return Ok(updateCompany);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteCompany = dBContext.Companies.FirstOrDefault(x => x.Id == id);
            if (deleteCompany == null)
            {
                return NotFound("Sirket bulunamadi.");
            }
            dBContext.Companies.Remove(deleteCompany);
            dBContext.SaveChanges();

            return Ok(deleteCompany);
        }

    }
}
