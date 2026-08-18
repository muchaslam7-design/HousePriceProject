using HousePriceApi.Data;
using HousePriceApi.DTOs;
using HousePriceApi.Models;
using HousePriceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HousePriceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IModelPredictionService _predictionService;

        public PredictionController(ApplicationDbContext context, IModelPredictionService predictionService)
        {
            _context = context;
            _predictionService = predictionService;
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict([FromBody] PredictionRequestDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Python .pkl service call
            decimal predictedPrice = await _predictionService.GetPredictionFromPythonAsync(
                model.TotalArea, model.Bedrooms, model.Latitude, model.Longitude
            );

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";

            var record = new PredictionRecord
            {
                UserId = userId,
                TotalArea = model.TotalArea,
                Bedrooms = model.Bedrooms,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                PredictedPrice = predictedPrice
            };

            _context.PredictionRecords.Add(record);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, predictedPrice, data = record });
        }
    }
}