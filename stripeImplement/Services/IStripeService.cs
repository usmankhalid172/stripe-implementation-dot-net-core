using Stripe;

namespace stripeImplement.Services
{
    public interface IStripeService
    {
        Task<string> CreateProduct(string name, string description);
        Task<string> CreatePrice(string productId, long amount, string name);
        Task<string> CustomerCreate(string name, string email, string? token);
        Task<Subscription> CreateSubscription(string customterId, string priceId);
    }
}
