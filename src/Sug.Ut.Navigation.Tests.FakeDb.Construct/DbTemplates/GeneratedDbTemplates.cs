using global::Sitecore.Data;
using global::Sitecore.FakeDb;
using global::Sitecore.FakeDb.Construct;

#region Navigation Root

namespace Sug.Ut.Navigation.Tests.FakeDb.Construct.DbTemplates
{
	/// <summary>
	/// Generated FakeDb constructable object for Navigation Root template (Template ID: {4753C2DB-B490-4C2C-AD11-944EC7E4A2D9})
	/// </summary>
	/// <seealso cref="Sitecore.FakeDb.Construct.ConstructableDbTemplate" />
	public partial class NavigationRootDbTemplate : ConstructableDbTemplate
	{
		public override void ConstructDb(Db db)
		{
			var template = new DbTemplate(TemplateName, TemplateId)
		    {
		        BaseIDs = new ID[]
		        {
				},
		    };


            db.Add(template);
		}

		public static ID TemplateId = new ID("{4753C2DB-B490-4C2C-AD11-944EC7E4A2D9}");
		public const string TemplateName = "Navigation Root";

		public static class FieldNames
		{
		}

		public static class FieldIds
		{
		}
	}
}

#endregion
#region Navigation

namespace Sug.Ut.Navigation.Tests.FakeDb.Construct.DbTemplates
{
	/// <summary>
	/// Generated FakeDb constructable object for Navigation template (Template ID: {F16CDC22-CF07-4688-B864-5825FA424C0E})
	/// </summary>
	/// <seealso cref="Sitecore.FakeDb.Construct.ConstructableDbTemplate" />
	public partial class NavigationDbTemplate : ConstructableDbTemplate
	{
		public override void ConstructDb(Db db)
		{
			var template = new DbTemplate(TemplateName, TemplateId)
		    {
		        BaseIDs = new ID[]
		        {
				},
		    };

			template.Fields.Add(new DbField(FieldNames.HideFromNavigation, FieldIds.HideFromNavigation));
			template.Fields.Add(new DbField(FieldNames.NavigationTitle, FieldIds.NavigationTitle));

            db.Add(template);
		}

		public static ID TemplateId = new ID("{F16CDC22-CF07-4688-B864-5825FA424C0E}");
		public const string TemplateName = "Navigation";

		public static class FieldNames
		{
			public const string HideFromNavigation = "Hide From Navigation";
			public const string NavigationTitle = "Navigation Title";
		}

		public static class FieldIds
		{
			public static ID HideFromNavigation = new ID("{3DB7E755-F283-43E1-BA89-084FC73B272A}");
			public static ID NavigationTitle = new ID("{3E805864-4A2B-4394-A2A4-99D1CF97ACA8}");
		}
	}
}

#endregion
