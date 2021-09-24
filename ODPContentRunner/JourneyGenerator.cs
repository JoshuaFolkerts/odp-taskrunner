using ODP.Services;
using ODP.Services.Models;
using System;
using System.Collections.Generic;

namespace ODPContentRunner
{
    public class JourneyGenerator : IJourneyGenerator
    {
        private readonly IContentGeneratorService contentGeneratorService;

        public JourneyGenerator(IContentGeneratorService contentGeneratorService)
        {
            this.contentGeneratorService = contentGeneratorService;
        }

        public List<ODPGeneric> GenerateCMSScript(Customer customer, DateTime startDateTime)
        {
            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);
            var list = new List<ODPGeneric>
            {
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(3), "search", "optimization", new Data() { Page = "/search", Title = "Search" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(4), "bestbetselected", "best bet selected", new Data() { Page = "/services", Title = "Service" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "PageCategories", "Services", new Data() { Page = "/services", Title = "Services" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(6), "Experiment-Decision","ServicesEngagement - LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(7), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "Experiment-Conversion","ServicesEngagement - LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(9), "AddToMailingList","Services Demo", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(10), "email","Services Customer Stories", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(10), "email","Internal - Demo Sign Up", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" })
            };

            return list;
        }

        public List<ODPGeneric> GenerateAssociationCMSScript(Customer customer, DateTime startDateTime)
        {
            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);
            var list = new List<ODPGeneric>
            {
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(3), "pageview", new Data() { Page = "/2021Conference", Title = "Conference 2021" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(4), "Experiment-Decision","ConferenceRegistrationStrategy - DiscountOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(6), "Experiment-Conversion","ConferenceRegistrationStrategy - DiscountOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(7), "AddToMailingList","Newsletter", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "email","Conference Promotion With Content Recs", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(9), "email","Conference Sign-Up Email", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" })
            };
            return list;
        }

        public List<ODPGeneric> GenerateCommerceScript(Customer customer, DateTime startDateTime)
        {
            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);
            var list = new List<ODPGeneric>
            {
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(3), "pageview", "PDP", new Data() { Page = "/en/fashion/mens/mens-shoes/p-39813617/", Title = "Jodpur Boot" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(4), "Experiment-Decision","AddToCartPromotionStrategy - Shipping", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateProductEvent(vuid, startDateTime.AddMinutes(6),"add-to-cart", "Jodpur Boot", "SKU-39813617"),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(7), "Experiment-Conversion","AddToCartPromotionStrategy - Shipping", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "email","Abandoned Card Email", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateOrderEvent(vuid, startDateTime.AddMinutes(9), "purchase",
                    new Order() {
                        Discount=50.00,
                        OrderId = "ABC123",
                        Shipping = 5.00,
                        CouponCode = "5Off",
                        Subtotal = 1130.00,
                        Tax = 100.00,
                        Total = 1235.00,
                        Items = new List<Item>() {
                            new Item() {
                                ProductId="SKU-39813617",
                                Price = 540.00,
                                Subtotal =1180.00,
                                Quantity = 2,
                                Discount=0.00
                            }
                        }
                    }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(9).AddSeconds(1), "AddToMailingList","Newsletter", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(9).AddSeconds(2), "email","transcription-receipt", new Data())
            };
            return list;
        }

        public List<ODPGeneric> GenerateCommerceMultiDayScript(Customer customer, DateTime startDateTime)
        {
            var maxDate = startDateTime.AddDays(3);
            if (maxDate > DateTime.Now)
            {
                while (maxDate > DateTime.Now)
                {
                    maxDate = maxDate.AddHours(-1);
                }
            }

            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);
            var list = new List<ODPGeneric>
            {
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(3), "pageview", "PDP", new Data() { Page = "/en/fashion/mens/mens-shoes/p-39813617/", Title = "Jodpur Boot" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(4), "Experiment-Decision","AddToCartPromotionStrategy - Shipping", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateProductEvent(vuid, startDateTime.AddMinutes(6),"add-to-cart", "Jodpur Boot", "SKU-39813617"),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(7), "Experiment-Conversion","AddToCartPromotionStrategy - Shipping", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "email","Abandoned Card Email", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateOrderEvent(vuid, maxDate, "purchase",
                    new Order() {
                        Discount=50.00,
                        OrderId = "ABC123",
                        Shipping = 5.00,
                        CouponCode = "5Off",
                        Subtotal = 1130.00,
                        Tax = 100.00,
                        Total = 1235.00,
                        Items = new List<Item>() {
                            new Item() {
                                ProductId="SKU-39813617",
                                Price = 540.00,
                                Subtotal =1180.00,
                                Quantity = 2,
                                Discount=0.00
                            }
                        }
                    }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, maxDate.AddSeconds(1), "AddToMailingList","Newsletter", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, maxDate.AddSeconds(2), "email","transcription-receipt", new Data() {  })
            };
            return list;
        }
    }
}