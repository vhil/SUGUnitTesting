using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Templates;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;
using Sug.Ut.SitecoreExtensions.Services;
using Version = Sitecore.Data.Version;

namespace Sug.Ut.SitecoreExtensions.Tests.Services
{
    [TestFixture]
    public class ItemExtensionsServiceTests
    {
        protected IServiceProvider ServiceProvider;
        protected IItemExtensionsService ItemExtensions;
        protected Item TemplateItem;
        protected Item Item;
        protected ID BaseTemplateId;
        protected ID ItemId;
        protected Database Database;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.Database = Substitute.For<Database>();
            this.BaseTemplateId = ID.NewID;
            this.ItemId = ID.NewID;

            var language = Substitute.For<Language>();
            language.Name.Returns("en");
            
            this.TemplateItem = Substitute.For<Item>(
                this.BaseTemplateId, 
                new ItemData(
                    new ItemDefinition(this.BaseTemplateId, "Base Template", ID.NewID, ID.NewID), 
                    language, 
                    Version.Parse(1), 
                    new FieldList()
                ), 
                this.Database);

            this.Item = Substitute.For<Item>(
                this.ItemId, 
                new ItemData(
                    new ItemDefinition(this.ItemId, "Item Of Base Template", this.BaseTemplateId, ID.NewID), 
                    language, 
                    Version.Parse(1), 
                    new FieldList()
                ), 
                this.Database);

            var template = new Template.Builder("Base Template", this.BaseTemplateId, new TemplateCollection()).Template;

            var templateManager = Substitute.For<BaseTemplateManager>();
            templateManager.GetTemplate(this.BaseTemplateId, this.Database).Returns(template);
            templateManager.GetTemplate(this.Item).Returns(template);

            this.ServiceProvider = Substitute.For<IServiceProvider>();
            this.ServiceProvider.GetService(typeof (BaseTemplateManager)).Returns(templateManager);
            ServiceLocator.SetServiceProvider(this.ServiceProvider);

            var templateRecords = Substitute.For<TemplateRecords>(this.Database);

            this.Database.Templates.Returns(templateRecords);

            Sitecore.Context.Database = this.Database;
            Sitecore.Context.Language = language;

            this.ItemExtensions = new ItemExtensionsService();
        }

        [Test]
        public void IsDerived_ItemIsOfGivenTemplate_ReturnsTrue()
        {
            // setup

            // execute

            var isDerived = this.ItemExtensions.IsDerived(this.Item, this.BaseTemplateId);

            // assert

            isDerived.Should().BeTrue();
        }
    }
}
