using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class TokenStore : StoreBase, ITokenStore
	{
		public TokenStore(ISessionSource sessionSource) : base(sessionSource)
		{
		}

		public IToken CreateNewInstance()
		{
			return new Token();
		}

		public Task<IdentityResult> AddAsync(IToken token, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				session.Store(token);
				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> RemoveAsync(string token, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var tok = session.Load<Token>(token);
				if (tok == null)
					return new IdentityResult(false);

				session.Delete(tok);

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> UpdateAsync(IToken token, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var tok = session.Load<Token>(token.Id);
				if (tok == null)
					return new IdentityResult(false);

				tok.ValidUntilUtc = token.ValidUntilUtc;
				tok.Value = token.Value;

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IToken> FindAsync(string id, bool onlyIfValid, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var tok = session.Load<Token>(id);
				if (tok == null)
					return null;

				if (onlyIfValid)
					return (IToken)(tok.ValidUntilUtc < DateTime.UtcNow ? tok : null);

				return tok;
			}, cancellationToken);
		}
	}
}