using HotelReservation.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly TechCareerDBContext dBContext;

        public RoomController()
        {
            dBContext = new TechCareerDBContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rooms = dBContext.Rooms;
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult Post(Room room)
        {
            dBContext.Rooms.Add(room);
            dBContext.SaveChanges();
            return Ok(room);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Room room)
        {
            var updateRoom = dBContext.Rooms.FirstOrDefault(x => x.Id == id);
            if (updateRoom == null)
            {
                return NotFound("Oda bulunamadi.");
            }
            updateRoom.Name = room.Name;
            dBContext.SaveChanges();
            return Ok(updateRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteRoom = dBContext.Rooms.FirstOrDefault(x => x.Id == id);
            if (deleteRoom == null)
            {
                return NotFound("Oda bulunamadi.");
            }
            dBContext.Rooms.Remove(deleteRoom);
            dBContext.SaveChanges();
            return Ok(deleteRoom);
        }
    }
}
