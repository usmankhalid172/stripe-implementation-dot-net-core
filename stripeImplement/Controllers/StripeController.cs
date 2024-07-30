using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using stripeImplement.Services;

namespace stripeImplement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        readonly IStripeService _stripeService;
        public StripeController(IStripeService invoiceService)
        {
            _stripeService = invoiceService;
        }
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(string name, string description)
        {
            return Ok(await _stripeService.CreateProduct(name,description));
        }
        [HttpPost("createPrice")]
        public async Task<IActionResult> CreatePrice(string? productId, long amount, string name)
        {
            return Ok(await _stripeService.CreatePrice(productId,amount, name));
        }
        [HttpPost("customerCreate")]
        public async Task<IActionResult> CustomerCreate(string name, string email, string? token)
        {
            return Ok(await _stripeService.CustomerCreate(name,email,token));
        }
        [HttpPost("createSubscription")]
        public async Task<IActionResult> CreateSubscription(string customterId, string priceId)
        {
            return Ok(await _stripeService.CreateSubscription(customterId, priceId));
        }
    }
}
