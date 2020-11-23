using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IUpdateCart
    {
        SqlCommand UpdateCartData(CartEditModel CartEditModel, string type);
    }
}