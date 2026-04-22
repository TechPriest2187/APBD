using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
    {
        var query = DataStore.Rooms.AsQueryable();

        if (minCapacity.HasValue) query = query.Where(r => r.Capacity >= minCapacity.Value);
        if (hasProjector.HasValue) query = query.Where(r => r.HasProjector == hasProjector.Value);
        if (activeOnly.HasValue && activeOnly.Value) query = query.Where(r => r.IsActive);

        return Ok(query.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoomsByBuilding([FromRoute] string buildingCode)
    {
        var rooms = DataStore.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(rooms);
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room newRoom)
    {
        newRoom.Id = DataStore.NextRoomId;
        DataStore.Rooms.Add(newRoom);
        
        return CreatedAtAction(nameof(GetRoom), new { id = newRoom.Id }, newRoom);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        room.Name = updatedRoom.Name;
        room.BuildingCode = updatedRoom.BuildingCode;
        room.Floor = updatedRoom.Floor;
        room.Capacity = updatedRoom.Capacity;
        room.HasProjector = updatedRoom.HasProjector;
        room.IsActive = updatedRoom.IsActive;

        return Ok(room);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        bool hasReservations = DataStore.Reservations.Any(r => r.RoomId == id);
        if (hasReservations) return Conflict(new { message = "Cannot delete a room that has reservations." });

        DataStore.Rooms.Remove(room);
        return NoContent();
    }
}