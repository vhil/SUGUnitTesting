using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitecore.FakeDb.Construct
{
	public static class FakeDbConstructFactory
	{
		public static Db ConstructDbFromExecutingAssembly()
		{
			return ConstructDbFromAssembly(Assembly.GetExecutingAssembly());
		}

		public static Db ConstructDbFromAssembly(Assembly assembly)
		{
			var constructables = assembly.GetTypes()
				.Where(x => typeof(ConstructableDbTemplate).IsAssignableFrom(x))
				.ToArray();

		    var templates = constructables.Select(type => (ConstructableDbTemplate) Activator.CreateInstance(type, null));

			return ConstructDb(templates);
		}

        public static Db ConstructDb(IEnumerable<ConstructableDbTemplate> constructables)
        {
            var db = new Db();

            foreach (var template in constructables)
            {
                template.ConstructDb(db);
            }

            return db;
        }
    }
}