using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.IntegrationTests
{
	[CollectionDefinition("DatabaseCollection")]
	public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
	{
	}
}
