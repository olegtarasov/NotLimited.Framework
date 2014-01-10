using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.MongoDb
{
    public class UserStore<TUser, TKey> : IUserStore<TUser, TKey> where TUser : IUser<TKey>
    {
	    public void Dispose()
	    {
		    throw new NotImplementedException();
	    }

	    public Task CreateAsync(TUser user)
	    {
		    throw new NotImplementedException();
	    }

	    public Task UpdateAsync(TUser user)
	    {
		    throw new NotImplementedException();
	    }

	    public Task DeleteAsync(TUser user)
	    {
		    throw new NotImplementedException();
	    }

	    public Task<TUser> FindByIdAsync(TKey userId)
	    {
		    throw new NotImplementedException();
	    }

	    public Task<TUser> FindByNameAsync(string userName)
	    {
		    throw new NotImplementedException();
	    }
    }
}
