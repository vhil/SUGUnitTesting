using Sitecore.Data;

namespace Sug.Ut.Metadata
{
    public static class Templates
    {
        public static class Metadata
        {
            public static ID TemplateId = new ID("{312ED441-85C0-4ED2-9798-83153EDB248B}");

            public static class FieldNames
            {
                public const string MetaTitle = "Meta Title";
                public const string MetaDescription = "Meta Description";
                public const string MetaKeywords = "Meta Keywords";
            }
        }

        public static class WebsiteMetadata
        {
            public static ID TemplateId = new ID("{1C41706F-C660-418E-A39B-28D2599A08CF}");

            public static class FieldNames
            {
                public const string MetaTitle = "Meta Title";
            }
        }
    }
}