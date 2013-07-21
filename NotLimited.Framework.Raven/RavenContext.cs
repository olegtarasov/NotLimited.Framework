using Raven.Client;
using Raven.Client.Document;

namespace NotLimited.Framework.Raven
{
	public class RavenContext
	{
		private readonly IDocumentStore _documentStore;

		public RavenContext(string url, string dbName)
		{
			_documentStore = new DocumentStore
			{
				Url = url,
				DefaultDatabase = dbName
			};
			_documentStore.Conventions.DefaultQueryingConsistency = ConsistencyOptions.QueryYourWrites;
			_documentStore.Initialize();
		}

		public IDocumentSession OpenSession()
		{
			return _documentStore.OpenSession();
		}
	}
}