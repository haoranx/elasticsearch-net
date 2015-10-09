﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.DeleteRepository
{
	public class DeleteRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await DELETE($"/_snapshot/{repository}")
				.Fluent(c => c.DeleteRepository(repository))
				.Request(c => c.DeleteRepository(new DeleteRepositoryRequest(repository)))
				.FluentAsync(c => c.DeleteRepositoryAsync(repository))
				.RequestAsync(c => c.DeleteRepositoryAsync(new DeleteRepositoryRequest(repository)))
				;
		}
	}
}