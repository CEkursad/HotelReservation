using HotelReservation.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly TechCareerDBContext dBContext;

        public ReservationController()
        {
            dBContext = new TechCareerDBContext();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var reservations = dBContext.Reservations.Include(x => x.Room).Include(x => x.Client).ToList();
            return Ok(reservations);
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var reservation = dBContext.Reservations.Include(x => x.Room).Include(x => x.Client).FirstOrDefault(x => x.Id == id);
            if (reservation == null)
            {
                return NotFound("Rezervasyon bulunamadi.");
            }
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult Post(Reservation reservation)
        {
            if (reservation.ReservationStartDate <= reservation.AddDate || reservation.ReservationEndDate <= reservation.ReservationStartDate)
            {
                return NotFound("Rezervasyon baslangic tarihi AddDate tarihinden buyuk olamaz veya Rezervasyon bitis tarihi Baslangic tarihinden buyuk olamaz..");
            }

            Client client = dBContext.Clients.FirstOrDefault(x => x.Id == reservation.ClientId);
            Room room = dBContext.Rooms.FirstOrDefault(x => x.Id == reservation.RoomId);

            if (client == null)
            {
                return NotFound("Musteri bulunamadi.");
            }
            reservation.Client = client;
            reservation.ClientId = client.Id;

            if (room == null)
            {
                return NotFound("Oda bulunamadi.");
            }
            reservation.Room = room;
            reservation.RoomId = room.Id;

            dBContext.Reservations.Add(reservation);
            dBContext.SaveChanges();

            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Reservation reservation)
        {
            var updateReservation = dBContext.Reservations.FirstOrDefault(x => x.Id == id);
            if (updateReservation == null)
            {
                return NotFound("Rezervasyon bulunamadi.");
            }
            else if (updateReservation.ReservationStartDate < updateReservation.AddDate || updateReservation.ReservationEndDate < updateReservation.ReservationStartDate)
            {
                return NotFound("Rezervasyon baslangıc tarihi Eklenme tarihinden buyuk olamaz veya Rezervasyon bitis tarihi Rezervasyon Baslangic tarihinden buyuk olamaz..");
            }
            updateReservation.ClientId = reservation.ClientId;
            updateReservation.RoomId = reservation.RoomId;
            updateReservation.ReservationStartDate = reservation.ReservationStartDate;
            updateReservation.ReservationEndDate = reservation.ReservationEndDate;
            dBContext.SaveChanges();
            return Ok(updateReservation);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteReservation = dBContext.Reservations.FirstOrDefault(x => x.Id == id);
            if (deleteReservation == null)
            {
                return NotFound("Rezervasyon bulunamadi.");
            }
            dBContext.Reservations.Remove(deleteReservation);
            dBContext.SaveChanges();
            return Ok(deleteReservation);
        }
    }
}
