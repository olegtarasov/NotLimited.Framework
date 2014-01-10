using Microsoft.AspNet.Identity.EntityFramework;
using NotLimited.Framework.Identity.MongoDb;

namespace NotLimited.Framework.WebTest.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : MongoUserBase<string>
    {
    }
}