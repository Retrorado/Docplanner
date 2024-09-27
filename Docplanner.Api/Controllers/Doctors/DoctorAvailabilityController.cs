using Docplanner.Contracts.Commands.Doctors.Availability;
using Docplanner.Contracts.Queries.Doctors.Availability;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocplannerApi.Controllers.Doctors;

[ApiController]
[Route("api/v1/doctors/availability")]
public class DoctorAvailabilityController(IMediator mediator) : ControllerBase
{
    [HttpGet("weekly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<DoctorWeeklyAvailabilityDto> GetWeeklyAvailability(
        [FromQuery] DateTime startDate,
        CancellationToken cancellationToken)
    {
        var query = new GetDoctorWeeklyAvailability(startDate);
        return await mediator.Send(query, cancellationToken);
    }

    [HttpPost("takeSlot")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> TakeSlot([FromBody] TakeDoctorSlot command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}