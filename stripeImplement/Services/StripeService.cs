using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using stripeImplement.Helper;
using stripeImplement.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StripeException = Stripe.StripeException;

namespace stripeImplement.Services
{


    public class StripeService : IStripeService
    {
        string stripeSecretKey = "paste_your_stripe_secret_key_here";
        //private readonly StripeDTO _stripeSettings;
        public StripeService()
        {
        }
        public Task<string> CustomerCreate(string name, string email, string? token)
        {
            return StripeHelper.AssginTokenToCustomer(name, email,token,stripeSecretKey);

        }

        public Task<string> CreateProduct(string name, string description)
        {
            return StripeHelper.CreateProduct(name,description,stripeSecretKey);
            
        }

        public Task<string> CreatePrice(string productId, long amount,string name)
        {
            return StripeHelper.CreatePrice(productId, amount,stripeSecretKey,name);
        }

        public Task<Subscription> CreateSubscription(string customterId, string priceId)
        {
            return StripeHelper.CreateSubscription(customterId, priceId,stripeSecretKey);
        }
    }


}
