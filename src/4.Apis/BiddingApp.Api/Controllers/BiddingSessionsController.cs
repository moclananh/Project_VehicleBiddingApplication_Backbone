using BiddingApp.Application.Services.BiddingSessionServices;
using BiddingApp.BuildingBlock.Utilities;
using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using Microsoft.AspNetCore.Mvc;

namespace BiddingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingSessionsController : ControllerBase
    {
        private readonly IBiddingSessionService _biddingSessionService;
        public BiddingSessionsController(IBiddingSessionService biddingSessionService)
        {
            _biddingSessionService = biddingSessionService;
        }

        [HttpPost("/api/sessions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBiddingSession([FromBody] CreateBiddingSessionRequest request)
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
            var response = await _biddingSessionService.CreateBiddingSession(request);
            return Ok(response);
        }

        [HttpGet("/api/sessions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBiddingSession([FromQuery] BiddingSessionFilter request) //Admin role == admin ? (request.IsActive = null) : true
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
            var response = await _biddingSessionService.GetAllBiddingSessions(request);
            return Ok(response);
        }

        [HttpGet("/api/users/{userId:guid}/sessions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBiddingSessionsWithUserState(Guid userId, [FromQuery] UserBiddingSessionFilter request)
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
            var response = await _biddingSessionService.GetAllBiddingSessionsWithUserState(userId, request);
            return Ok(response);
        }

        [HttpGet("/api/sessions/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBiddingSessionById(Guid id)
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
            var response = await _biddingSessionService.GetBiddingSessionById(id);
            return Ok(response);
        }

        [HttpGet("/api/users/{userId:guid}/sessions/{sessionId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBiddingSessionByIdWithUserState(Guid userId, Guid sessionId)
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
            var response = await _biddingSessionService.GetBiddingSessionByIdWithUserState(userId, sessionId);
            return Ok(response);
        }

        [HttpPut("/api/sessions/{id:guid}/close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CloseBiddingSession(Guid id)  //called when end session (this is manual option)
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
            var response = await _biddingSessionService.CloseBiddingSession(id);
            return Ok(response);
        }

        [HttpPut("/api/sessions/{id:guid}/disable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DisableBiddingSession(Guid id)
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
            var response = await _biddingSessionService.DisableBiddingSession(id);
            return Ok(response);
        }  
    }
}
