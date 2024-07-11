using Customers.Domain.Models;
using LiteDB;

namespace Customers.Domain.Repositories
{
    public interface ISupportRepository { 

        BsonValue InsertSupport(Support support);

        Support GetSupportByCustomerId(int customerId);
    }

}
