using BiddingApp.Application.Services.BiddingServices;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.BiddingDtos;
using Microsoft.AspNetCore.Mvc;

namespace BiddingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingsController : ControllerBase
    {
        private readonly IBiddingService _biddingService;
        public BiddingsController(IBiddingService biddingService)
        {
            _biddingService = biddingService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBiddingRequest([FromBody] CreateBiddingRequest request)
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
            var response = await _biddingService.CreateBidding(request);
            return Ok(response);
        }
    }
}
