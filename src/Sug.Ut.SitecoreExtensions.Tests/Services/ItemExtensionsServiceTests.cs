using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Sitecore.Abstractions;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Engines;
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
        protected Database Database;
	    protected Language Language;
	    protected TemplateEngine TemplateEngine;
	    protected BaseTemplateManager TemplateManager;

		[OneTimeSetUp]
        public void SetUp()
        {
            this.Database = Substitute.For<Database>();
			this.Language = Substitute.For<Language>();

			this.TemplateEngine = Substitute.For<TemplateEngine>(this.Database);
			this.TemplateManager = Substitute.For<BaseTemplateManager>();
			this.ServiceProvider = Substitute.For<IServiceProvider>();

			this.ServiceProvider.GetService(typeof(BaseTemplateManager)).Returns(this.TemplateManager);
			ServiceLocator.SetServiceProvider(this.ServiceProvider);

			var templateRecords = Substitute.For<TemplateRecords>(this.Database);

			this.Database.Templates.Returns(templateRecords);

			Sitecore.Context.Database = this.Database;
			Sitecore.Context.Language = this.Language;

			this.ItemExtensions = new ItemExtensionsService();
        }

		[Test]
		public void IsDerived_ItemIsNull_DoesNotThrowException()
		{
			// setup
			Item item = null;

			// act
			Action action = () => this.ItemExtensions.IsDerived(item, ID.NewID);

			// assert
			action.ShouldNotThrow<ArgumentNullException>();
		}

		[Test]
		public void IsDerived_ItemIsNull_ReturnsFalse()
		{
			// setup
			Item item = null;

			// act
			var result = this.ItemExtensions.IsDerived(item, ID.NewID);

			// assert
			result.Should().BeFalse();
		}

		[Test]
        public void IsDerived_ItemIsOfGivenTemplate_ReturnsTrue()
        {
			// setup
			#region Setup

			var baseTemplateId = ID.NewID;
			var itemId = ID.NewID;

			var item = Substitute.For<Item>(
				itemId,
				new ItemData(
					new ItemDefinition(itemId, "Item Of Template", baseTemplateId, ID.NewID),
					this.Language,
					Version.Parse(1),
					new FieldList()
				),
				this.Database);

			var template = new Template.Builder("Base Template", baseTemplateId, new TemplateCollection()).Template;

			this.TemplateManager.GetTemplate(baseTemplateId, this.Database).Returns(template);
			this.TemplateManager.GetTemplate(item).Returns(template);

			#endregion

			// act
			var isDerived = this.ItemExtensions.IsDerived(item, baseTemplateId);

            // assert
			isDerived.Should().BeTrue();
        }

		[Test]
		public void IsDerived_ItemInheritsBaseTemplate_ReturnsTrue()
		{
			// setup
			#region Setup

			var itemTemplateId = ID.NewID;
			var baseTemplateId = ID.NewID;
			var itemId = ID.NewID;

			var item = Substitute.For<Item>(
				itemId,
				new ItemData(
					new ItemDefinition(itemId, "Item Of Base Template", itemTemplateId, ID.Null),
					this.Language,
					Version.Parse(1),
					new FieldList()
				),
				this.Database);

			var baseTemplate = new Template.Builder("Base Template", baseTemplateId, this.TemplateEngine).Template;
			var itemTemplateItem = new Template.Builder("Item Template", itemTemplateId, this.TemplateEngine);
			itemTemplateItem.SetBaseIDs(baseTemplateId.ToString());
			var itemTemplate = itemTemplateItem.Template;

			this.TemplateEngine.GetTemplate(itemTemplateId).Returns(itemTemplate);
			this.TemplateEngine.GetTemplate(baseTemplateId).Returns(baseTemplate);

			this.TemplateManager.GetTemplate(itemTemplateId, this.Database).Returns(itemTemplate);
			this.TemplateManager.GetTemplate(baseTemplateId, this.Database).Returns(baseTemplate);
			this.TemplateManager.GetTemplate(item).Returns(itemTemplate);

			#endregion

			// act
			var isDerived = this.ItemExtensions.IsDerived(item, baseTemplateId);

			// assert
			isDerived.Should().BeTrue();
		}

		[Test]
		public void IsDerived_ItemIsNotOfBaseTemplate_ReturnsTrue()
		{
			// setup
			#region Setup

			var itemTemplateId = ID.NewID;
			var baseTemplateId = ID.NewID;
			var itemId = ID.NewID;

			var item = Substitute.For<Item>(
				itemId,
				new ItemData(
					new ItemDefinition(itemId, "Item Of Template", ID.NewID, ID.NewID),
					this.Language,
					Version.Parse(1),
					new FieldList()
				),
				this.Database);

			var template = new Template.Builder("Base Template", baseTemplateId, new TemplateCollection()).Template;

			var baseTemplate = new Template.Builder("Base Template", baseTemplateId, this.TemplateEngine).Template;
			var itemTemplate = new Template.Builder("Item Template", itemTemplateId, this.TemplateEngine).Template;

			this.TemplateEngine.GetTemplate(itemTemplateId).Returns(itemTemplate);
			this.TemplateEngine.GetTemplate(baseTemplateId).Returns(baseTemplate);

			this.TemplateManager.GetTemplate(itemTemplateId, this.Database).Returns(itemTemplate);
			this.TemplateManager.GetTemplate(baseTemplateId, this.Database).Returns(baseTemplate);
			this.TemplateManager.GetTemplate(item).Returns(itemTemplate);

			#endregion

			// act
			var isDerived = this.ItemExtensions.IsDerived(item, baseTemplateId);

			// assert
			isDerived.Should().BeFalse();
		}

	    [Test]
	    public void GetAncestorOrSelfOfTemplate_NullItem_ThrowsArgumentNullException()
	    {
			// setup
		    Item item = null;

			// act
		    Action action = () => this.ItemExtensions.GetAncestorOrSelfOfTemplate(item, ID.NewID);

			// assert
		    action.ShouldThrow<ArgumentNullException>();
	    }
    }
}
