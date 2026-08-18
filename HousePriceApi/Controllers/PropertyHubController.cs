using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HousePriceApi.Controllers
{
    [ApiController]
    [Route("api/property-hub")]
    public class PropertyHubController : ControllerBase
    {
        private readonly RealEstateHubService _hubService;

        public PropertyHubController(RealEstateHubService hubService)
        {
            _hubService = hubService;
        }

        // 1. Future Value Trend Predictor
        [HttpPost("predict-trend")]
        public async Task<IActionResult> PredictTrend([FromBody] object data)
        {
            string modelUrl = "http://127.0.0.1:5000/predict-trend";
            var result = await _hubService.CallAIModelAsync(modelUrl, data);
            return Content(result, "application/json");
        }

        // 2. Safety Score Predictor
        [HttpPost("predict-safety")]
        public async Task<IActionResult> PredictSafety([FromBody] object data)
        {
            string modelUrl = "http://127.0.0.1:5000/predict-safety";
            var result = await _hubService.CallAIModelAsync(modelUrl, data);
            return Content(result, "application/json");
        }
    }
}