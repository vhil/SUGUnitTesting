using Sitecore.Data;

namespace Sug.Ut.Navigation
{
    public static class Templates
    {
        public static class NavigationRoot 
        {
            public static ID TemplateId  = new ID("{4753C2DB-B490-4C2C-AD11-944EC7E4A2D9}");
        }

        public static class Navigation
        {
            public static ID TemplateId  = new ID("{F16CDC22-CF07-4688-B864-5825FA424C0E}");

            public static class FieldNames
            {
                public const string NavigationTitle = "Navigation Title";
                public const string HideFromNavigation = "Hide From Navigation";
            }
        }
    }
}