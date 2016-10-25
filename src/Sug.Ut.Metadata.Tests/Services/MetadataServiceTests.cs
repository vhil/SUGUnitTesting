using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sug.Ut.Metadata.Services;
using Sug.Ut.SitecoreExtensions.Services;
using Version = Sitecore.Data.Version;

namespace Sug.Ut.Metadata.Tests.Services
{
	[TestFixture]
	public class MetadataServiceTests
	{
		protected IMetadataService MetadataService;
		protected IItemExtensionsService ItemExtensions;
		protected Database Database;
		protected Language Language;
		protected Item WebsiteMetadataItem;
		protected Item MetadataItem;

		[OneTimeSetUp]
		public void Setup()
		{
			this.ItemExtensions = Substitute.For<IItemExtensionsService>();
			this.Database = Substitute.For<Database>();
			this.Language = Substitute.For<Language>();
			this.MetadataService = new MetadataService(this.ItemExtensions);

			var pageItemId = ID.NewID;
			this.MetadataItem = Substitute.For<Item>(
				pageItemId,
				new ItemData(
					new ItemDefinition(pageItemId, "Page Item", Templates.Metadata.TemplateId, ID.NewID),
					this.Language,
					Version.Parse(1),
					new FieldList()
				),
				this.Database);

			this.MetadataItem[Templates.Metadata.FieldNames.MetaTitle].Returns("Page Meta Title");
			this.MetadataItem[Templates.Metadata.FieldNames.MetaDescription].Returns("Page Meta Description");
			this.MetadataItem[Templates.Metadata.FieldNames.MetaKeywords].Returns("Page Meta Keywords");

			var rootItemId = ID.NewID;
			this.WebsiteMetadataItem = Substitute.For<Item>(
				rootItemId,
				new ItemData(
					new ItemDefinition(rootItemId, "Website Metadata", Templates.WebsiteMetadata.TemplateId, ID.NewID),
					this.Language,
					Version.Parse(1),
					new FieldList()
				),
				this.Database);

			this.WebsiteMetadataItem[Templates.WebsiteMetadata.FieldNames.MetaTitle].Returns("Site Meta Title");
		}

		[Test]
		public void GetPageMetadata_NullParameter_ThrowsArgumentNullException()
		{
			// setup
			Item renderingItem = null;

			// act
			Action action = () => this.MetadataService.GetPageMetadata(renderingItem);

			// assert
			action.ShouldThrow<ArgumentNullException>();
		}

		[Test]
		public void GetPageMetadata_CurrentItemIsChildPage_ReturnsValidFieldValues()
		{
			// setup
			this.ItemExtensions.GetAncestorOrSelfOfTemplate(this.MetadataItem, Templates.WebsiteMetadata.TemplateId).Returns(this.WebsiteMetadataItem);
			this.ItemExtensions.IsDerived(this.WebsiteMetadataItem, Templates.WebsiteMetadata.TemplateId).Returns(true);
			this.ItemExtensions.IsDerived(this.MetadataItem, Templates.Metadata.TemplateId).Returns(true);

			// act
			var result = this.MetadataService.GetPageMetadata(this.MetadataItem);

			// assert
			result.SiteTitle.Should().BeEquivalentTo("Site Meta Title");
			result.MetaTitle.Should().BeEquivalentTo("Page Meta Title");
			result.MetaDescription.Should().BeEquivalentTo("Page Meta Description");
			result.MetaKeywords.Should().BeEquivalentTo("Page Meta Keywords");
		}

		[Test]
		public void GetPageMetadata_CurrentItemIsNotChildPage_ReturnsEmptyPageMetaValules()
		{
			// setup
			this.ItemExtensions.GetAncestorOrSelfOfTemplate(this.MetadataItem, Templates.WebsiteMetadata.TemplateId).Returns(this.WebsiteMetadataItem);
			this.ItemExtensions.IsDerived(this.WebsiteMetadataItem, Templates.WebsiteMetadata.TemplateId).Returns(true);
			this.ItemExtensions.IsDerived(this.MetadataItem, Templates.Metadata.TemplateId).Returns(false);
			
			// act
			var result = this.MetadataService.GetPageMetadata(this.MetadataItem);

			// assert
			result.SiteTitle.Should().BeEquivalentTo("Site Meta Title");
			result.MetaTitle.Should().BeNullOrEmpty();
			result.MetaDescription.Should().BeNullOrEmpty();
			result.MetaKeywords.Should().BeNullOrEmpty();
		}

		[Test]
		public void GetPageMetadata_NoRootWebsiteMetadataItem_ReturnsEmptySiteMetaTitle()
		{
			// setup
			this.ItemExtensions.GetAncestorOrSelfOfTemplate(this.MetadataItem, Templates.WebsiteMetadata.TemplateId).Returns(default(Item));
			this.ItemExtensions.IsDerived(this.MetadataItem, Templates.Metadata.TemplateId).Returns(true);

			// act
			var result = this.MetadataService.GetPageMetadata(this.MetadataItem);

			// assert
			result.SiteTitle.Should().BeNullOrEmpty();
			result.MetaTitle.Should().BeEquivalentTo("Page Meta Title");
			result.MetaDescription.Should().BeEquivalentTo("Page Meta Description");
			result.MetaKeywords.Should().BeEquivalentTo("Page Meta Keywords");
		}
	}
}
