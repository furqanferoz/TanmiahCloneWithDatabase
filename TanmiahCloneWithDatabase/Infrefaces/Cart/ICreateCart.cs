using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface ICreateCart
    {
        SqlCommand CreateCartData(CartEditModel cartEditModel);
    }
}