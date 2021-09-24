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
                this.contentGeneratorService.GeneratePageViewEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(3), "search", "optimization", new Data() { Page = "/search", Title = "Search" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(4), "bestbetselected", "best bet selected", new Data() { Page = "/services", Title = "Service" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "PageCategories", "Services", new Data() { Page = "/services", Title = "Services" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(6), "Experiment-Decision","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(7), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "Experiment-Conversion","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
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
                this.contentGeneratorService.GeneratePageViewEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "search", "optimization", new Data() { Page = "/search", Title = "Search" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "bestbetselected", "best bet selected", new Data() { Page = "/services", Title = "Service" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(12), "PageCategories", "Services", new Data() { Page = "/services", Title = "Services" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(14), "Experiment-Decision","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(14), "Experiment-Conversion","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "AddToMailingList","Services Demo", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "email","Services Customer Stories", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "email","Internal - Demo Sign Up", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" })
            };
            return list;
        }

        public List<ODPGeneric> GenerateCommerceScript(Customer customer, DateTime startDateTime)
        {
            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);
            var list = new List<ODPGeneric>
            {
                this.contentGeneratorService.GeneratePageViewEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "search", "optimization", new Data() { Page = "/search", Title = "Search" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "bestbetselected", "best bet selected", new Data() { Page = "/services", Title = "Service" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(12), "PageCategories", "Services", new Data() { Page = "/services", Title = "Services" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(14), "Experiment-Decision","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(14), "Experiment-Conversion","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "AddToMailingList","Services Demo", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "email","Services Customer Stories", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "email","Internal - Demo Sign Up", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" })
            };
            return list;
        }

        public List<ODPGeneric> GenerateCommerceMultiDayScript(Customer customer, DateTime startDateTime)
        {
            var vuid = Identifier.AddVuid(customer.Attributes.Vuid);
            var list = new List<ODPGeneric>
            {
                this.contentGeneratorService.GeneratePageViewEvent(vuid, startDateTime.AddMinutes(2), "pageview", new Data() { Page = "/en", Title = "Home" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(5), "search", "optimization", new Data() { Page = "/search", Title = "Search" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(8), "bestbetselected", "best bet selected", new Data() { Page = "/services", Title = "Service" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(12), "PageCategories", "Services", new Data() { Page = "/services", Title = "Services" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(14), "Experiment-Decision","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "Submit Form","Find Out More", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(14), "Experiment-Conversion","ServicesEngagement-LimitedTimeOffer", new Data() {  Title = "Demo Site", Page="/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "AddToMailingList","Services Demo", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "email","Services Customer Stories", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" }),
                this.contentGeneratorService.GenerateGenericEvent(vuid, startDateTime.AddMinutes(20), "email","Internal - Demo Sign Up", new Data() {  Title = "Demo Site", Page = "/en/demo-scenarios/DemoSite/" })
            };
            return list;
        }
    }
}