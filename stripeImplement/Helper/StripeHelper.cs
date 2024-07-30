using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StripeException = Stripe.StripeException;

using Stripe;
using Stripe.Climate;

namespace stripeImplement.Helper
{

    public static class StripeHelper
    {

        #region Create Customer And Assign Token To Customer
        public async static Task<string> AssginTokenToCustomer(string name, string email, string? token, string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;

            var options = new CustomerCreateOptions
            {
                Name = name,
                Email = email,
                Source = token ?? "tok_visa",
            };

            var service = new CustomerService();
            var customer = await service.CreateAsync(options);

            return customer.Id;
        }
        #endregion

        #region Update Card
        public async static Task<string> UpdateCard(string customerId, string token, string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;

            var options = new CustomerUpdateOptions { Source = token };
            var service = new CustomerService();
            var customer = service.Update(customerId, options);

            return customer.Id;
        }
        #endregion

        #region Subscription
        public static Subscription GetSubscription(string subscriptionId, string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;

            var service = new SubscriptionService();
            Subscription subscription = service.Get(subscriptionId);

            return subscription;
        }

        public async static Task<Subscription> CreateSubscription(string customerId, string priceId, string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;
            var options = new SubscriptionCreateOptions
            {
                Customer = customerId,
                Items = new List<SubscriptionItemOptions>
                        {
                            new SubscriptionItemOptions { Price = priceId },
                        },
            };

            var service = new SubscriptionService();
            var subscription = await service.CreateAsync(options);

            return subscription;
        }

        public async static Task<string> CancelSubscription(string subscriptionId, string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;

            var service = new SubscriptionService();
            var subscription = await service.CancelAsync(subscriptionId);
            return subscription.Id;
        }

        public async static Task<Subscription> UpdateSubscription(string subscriptionId, string priceId, string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;

            var service = new SubscriptionService();
            Subscription subscription = service.Get(subscriptionId);

            var items = new List<SubscriptionItemOptions> {
                new SubscriptionItemOptions {
                    Id = subscription.Items.Data[0].Id,
                    Price = priceId,
                },
            };

            var options = new SubscriptionUpdateOptions
            {
                CancelAtPeriodEnd = false,
                ProrationBehavior = "none",
                Items = items,
                //TransferData = new SubscriptionTransferDataOptions
                //{
                //    Destination = AdminStripeAccount.MembershipAccount,
                //},
            };
            subscription = await service.UpdateAsync(subscriptionId, options);

            return subscription;
        }
        public async static Task<string> CreatePrice(string? subscriptionId, long amount, string apiKey,string name)
        {
            StripeConfiguration.ApiKey = apiKey;
           
            var options = new PriceCreateOptions
            {
                Currency = "usd",
                UnitAmount = amount * 100,
                Recurring = new PriceRecurringOptions { Interval = "month" },
                ProductData = new PriceProductDataOptions { Name = name },
            };
            var service = new PriceService();
            var priceObj = service.Create(options);
            return priceObj.Id;
        }
        public async static Task<string> CreateProduct(string productName, string description, string apiKey)
        {
            StripeConfiguration.ApiKey = apiKey;

            var options = new ProductCreateOptions
            {
                Name = productName,
                Description = description,
            };
            var service = new Stripe.ProductService();
            var priceObj = service.Create(options);
            return priceObj.Id;
        }

        #endregion
    }
}


