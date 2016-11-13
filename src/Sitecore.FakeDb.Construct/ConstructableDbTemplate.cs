using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.FakeDb.Construct
{
	public abstract class ConstructableDbTemplate
	{
		public abstract void ConstructDb(Db db);
	}
}
