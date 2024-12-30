using BiddingApp.Application.Services.VehicleSevices;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
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
        public async Task<IActionResult> GetAllVehicles([FromQuery] VehicleFilter request)
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

        [HttpGet("vin-code/{vin}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleByVIN(string vin)
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
            var response = await _vehicleService.GetVehicleByVin(vin);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleById(int id)
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
            var response = await _vehicleService.GetVehicleById(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleRequest request)
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
            var response = await _vehicleService.UpdateVehicle(id, request);
            return Ok(response);
        }
    }
}
