using Sitecore.Data;

namespace Sug.Ut.PageContent
{
    public static class Templates
    {
        public static class PageTitle
        {
            public static ID TemplateId = new ID("{3F6DDFA2-C79B-4C4A-86D4-696E76303C92}");

            public static class FieldNames
            {
                public const string PageTitle = "Page Title";
            }
        }

        public static class PageImage
        {
            public static ID TemplateId = new ID("{B986636B-C1A3-438A-A478-B26FFD3A8217}");

            public static class FieldNames
            {
                public const string PageImage = "Page Image";
            }
        }

        public static class PageBody
        {
            public static ID TemplateId = new ID("{478F623F-E2CE-4EA8-B8E7-081E98A0AA22}");

            public static class FieldNames
            {
                public const string PageBody = "Page Body";
            }
        }
    }
}