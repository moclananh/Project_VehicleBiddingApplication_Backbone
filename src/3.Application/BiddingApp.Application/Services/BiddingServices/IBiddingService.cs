using BiddingApp.Domain.Models;
using BiddingApp.Infrastructure.Dtos.BiddingDtos;

namespace BiddingApp.Application.Services.BiddingServices
{
    public interface IBiddingService
    {
        Task<ApiResponse<bool>> CreateBidding(CreateBiddingRequest request);
    }
}
