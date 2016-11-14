using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitecore.FakeDb.Construct
{
	public class FakeDbConstructFactory
	{
		public virtual Db ConstructDbFromExecutingAssembly()
		{
			return this.ConstructDbFromAssembly(Assembly.GetExecutingAssembly());
		}

		public virtual Db ConstructDbFromAssembly(Assembly assembly)
		{
			var constructables = assembly.GetTypes()
				.Where(x => typeof(ConstructableDbTemplate).IsAssignableFrom(x))
				.ToArray();

		    var templates = constructables.Select(type => (ConstructableDbTemplate) Activator.CreateInstance(type, null));

			return this.ConstructDb(templates);
		}

        public virtual Db ConstructDb(IEnumerable<ConstructableDbTemplate> constructables)
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