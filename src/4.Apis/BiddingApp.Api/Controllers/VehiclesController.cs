using BiddingApp.Application.Services.VehicleSevices;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;
using Microsoft.AspNetCore.Mvc;

namespace BiddingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<bool>
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = SystemConstants.ModelStateResponses.ModelStateInvalid
                });
            }
            var response = await _vehicleService.CreateVehicle(request);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBiddingSession([FromQuery] VehicleFilter request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<bool>
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = SystemConstants.ModelStateResponses.ModelStateInvalid
                });
            }
            var response = await _vehicleService.GetAllVehicles(request);
            return Ok(response);
        }
    }
}
