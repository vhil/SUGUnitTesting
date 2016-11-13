using System;
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
			var db = new Db();

			var constructables = assembly.GetTypes()
				.Where(x => typeof(ConstructableDbTemplate).IsAssignableFrom(x))
				.ToArray();

			foreach (var type in constructables)
			{
				var methodInfo = type.GetMethod("ConstructDb");

				if (methodInfo != null)
				{
					var classInstance = Activator.CreateInstance(type, null);
					methodInfo.Invoke(classInstance, new object[] { db });
				}
			}

			return db;
		}
	}
}
