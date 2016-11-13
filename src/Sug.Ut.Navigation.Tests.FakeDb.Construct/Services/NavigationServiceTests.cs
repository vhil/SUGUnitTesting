using System.Linq;
using System.Reflection;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.FakeDb;
using Sitecore.FakeDb.Construct;
using Sug.Ut.Navigation.Services;
using Sug.Ut.Navigation.Tests.FakeDb.Construct.DbTemplates;
using Sug.Ut.SitecoreExtensions.Services;

namespace Sug.Ut.Navigation.Tests.FakeDb.Construct.Services
{
	[TestFixture]
	public class NavigationServiceTests
	{
		protected ID NonNavigationTemplateId;
		protected INavigationService NavigationService;
		protected BaseLinkManager LinkManager;

		[OneTimeSetUp]
		public void Setup()
		{
			this.NonNavigationTemplateId = ID.NewID;
			this.LinkManager = Substitute.For<BaseLinkManager>();
			this.NavigationService = new NavigationService(this.LinkManager, new ItemExtensionsService());
		}

		[Test]
		public void GetPrimaryNavigation_MixedTree_ReturnsOnlyNavigableItems()
		{
			using (var db = new FakeDbConstructFactory().ConstructDbFromAssembly(Assembly.GetExecutingAssembly()))
			{
				var currentItemId = ID.NewID;
				var nonNavItemId = ID.NewID;
				// setup
				var navRootItem = new DbItem("root", ID.NewID, NavigationRootDbTemplate.TemplateId);

				var navPage1 = new DbItem("page 1", ID.NewID, NavigationDbTemplate.TemplateId);
				navRootItem.Children.Add(navPage1);

				var nonNavItem = new DbItem("non nav", nonNavItemId, this.NonNavigationTemplateId);
				navRootItem.Children.Add(nonNavItem);

				var navPage2 = new DbItem("page 2", currentItemId, NavigationDbTemplate.TemplateId);
				navRootItem.Children.Add(navPage2);

				db.Add(navRootItem);

				var currentItem = db.GetItem(currentItemId);

				// act
				var primaryNavigation = this.NavigationService.GetPrimaryNavigation(currentItem);

				// assert
				primaryNavigation.Should().NotBeNull();
				primaryNavigation.Should().NotBeEmpty();
				primaryNavigation.Should().NotContain(x => x.ItemId == nonNavItemId);
			}
		}

		[Test]
		public void GetPrimaryNavigation_NormalTree_CurrentItemShouldBeActive()
		{
			using (var db = new FakeDbConstructFactory().ConstructDbFromAssembly(Assembly.GetExecutingAssembly()))
			{
				var currentItemId = ID.NewID;
				// setup
				var navRootItem = new DbItem("root", ID.NewID, Templates.NavigationRoot.TemplateId);

				var navPage1 = new DbItem("page 1", ID.NewID, NavigationDbTemplate.TemplateId);
				navRootItem.Children.Add(navPage1);

				var navPage2 = new DbItem("page 2", currentItemId, NavigationDbTemplate.TemplateId);
				navRootItem.Children.Add(navPage2);

				db.Add(navRootItem);

				var currentItem = db.GetItem(currentItemId);

				// act
				var primaryNavigation = this.NavigationService.GetPrimaryNavigation(currentItem);

				// assert
				primaryNavigation.Should().NotBeNull();
				primaryNavigation.Should().NotBeEmpty();
				primaryNavigation.Should().Contain(x => x.ItemId == currentItemId);
				primaryNavigation.Single(x => x.ItemId == currentItemId).IsActive.Should().BeTrue();
				primaryNavigation.Where(x => x.ItemId != currentItemId).Select(x => x.IsActive.Should().BeFalse());
			}
		}
	}
}
