using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetReservations([FromQuery] DateOnly? date, [FromQuery] string? status, [FromQuery] int? roomId)
    {
        var query = DataStore.Reservations.AsQueryable();

        if (date.HasValue) query = query.Where(r => r.Date == date.Value);
        if (!string.IsNullOrEmpty(status)) query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        if (roomId.HasValue) query = query.Where(r => r.RoomId == roomId.Value);

        return Ok(query.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation newReservation)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == newReservation.RoomId);
        if (room == null) return NotFound(new { message = "Room does not exist." });
        if (!room.IsActive) return BadRequest(new { message = "Cannot reserve an inactive room." });

        bool isOverlapping = DataStore.Reservations.Any(r =>
            r.RoomId == newReservation.RoomId &&
            r.Date == newReservation.Date &&
            r.StartTime < newReservation.EndTime && 
            r.EndTime > newReservation.StartTime);

        if (isOverlapping) return Conflict(new { message = "Reservation overlaps with an existing one." });

        newReservation.Id = DataStore.NextReservationId;
        DataStore.Reservations.Add(newReservation);

        return CreatedAtAction(nameof(GetReservation), new { id = newReservation.Id }, newReservation);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedData)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null) return NotFound();

        bool isOverlapping = DataStore.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedData.RoomId &&
            r.Date == updatedData.Date &&
            r.StartTime < updatedData.EndTime &&
            r.EndTime > updatedData.StartTime);

        if (isOverlapping) return Conflict(new { message = "Reservation overlaps with an existing one." });

        reservation.RoomId = updatedData.RoomId;
        reservation.OrganizerName = updatedData.OrganizerName;
        reservation.Topic = updatedData.Topic;
        reservation.Date = updatedData.Date;
        reservation.StartTime = updatedData.StartTime;
        reservation.EndTime = updatedData.EndTime;
        reservation.Status = updatedData.Status;

        return Ok(reservation);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null) return NotFound();

        DataStore.Reservations.Remove(reservation);
        return NoContent();
    }
}