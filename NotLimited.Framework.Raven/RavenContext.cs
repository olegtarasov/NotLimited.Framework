using Raven.Client;
using Raven.Client.Document;

namespace NotLimited.Framework.Raven
{
	public class RavenContext
	{
		private readonly IDocumentStore _documentStore;

		public RavenContext(string connectionStringName)
		{
			_documentStore = new DocumentStore
			{
				ConnectionStringName = connectionStringName
			};
			_documentStore.Conventions.DefaultQueryingConsistency = ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite;
			_documentStore.Initialize();
		}

		public IDocumentSession OpenSession()
		{
			return _documentStore.OpenSession();
		}
	}
}