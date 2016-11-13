using FluentAssertions;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.FakeDb;
using Sug.Ut.SitecoreExtensions.Services;

namespace Sug.Ut.SitecoreExtensions.Tests.FakeDb.Services
{
	[TestFixture]
	public class ItemExtensionsServiceTests
	{
		protected IItemExtensionsService ItemExtensions;
		protected ID BaseTemplateId;
		protected ID ItemTemplateID;
		protected ID AnotherTemplateId;
		protected DbItem Item;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.BaseTemplateId = ID.NewID;
			this.ItemTemplateID = ID.NewID;
			this.AnotherTemplateId = ID.NewID;

			this.ItemExtensions = new ItemExtensionsService();
		}

		[Test]
		public void IsDerived_ItemIsOfGivenTemplate_ReturnsTrue()
		{
			using (var db = this.ConstructDb())
			{
				// setup
				db.Add(this.Item = new DbItem("page", ID.NewID, this.BaseTemplateId));

				var item = db.GetItem(this.Item.ID);

				// act
				var isDerived = this.ItemExtensions.IsDerived(item, this.BaseTemplateId);

				// assert
				isDerived.Should().BeTrue();
			}
		}

		[Test]
		public void IsDerived_ItemInheritsBaseTemplate_ReturnsTrue()
		{
			using (var db = this.ConstructDb())
			{
				// setup
				db.Add(this.Item = new DbItem("page", ID.NewID, this.ItemTemplateID));

				var item = db.GetItem(this.Item.ID);

				// act
				var isDerived = this.ItemExtensions.IsDerived(item, this.BaseTemplateId);

				// assert
				isDerived.Should().BeTrue();
			}
		}

		[Test]
		public void IsDerived_ItemDoesNotInheritBaseTemplate_ReturnsFalse()
		{
			using (var db = this.ConstructDb())
			{
				// setup
				db.Add(this.Item = new DbItem("page", ID.NewID, this.AnotherTemplateId));

				var item = db.GetItem(this.Item.ID);

				// act
				var isDerived = this.ItemExtensions.IsDerived(item, this.BaseTemplateId);

				// assert
				isDerived.Should().BeFalse();
			}
		}

		private Db ConstructDb()
		{
			var db = new Db
			{
				new DbTemplate("base template", this.BaseTemplateId),
				new DbTemplate("another template", this.AnotherTemplateId),
				new DbTemplate("page template", this.ItemTemplateID)
				{
					BaseIDs = new [] {this.BaseTemplateId}
				}
			};

			return db;
		}
	}
}
