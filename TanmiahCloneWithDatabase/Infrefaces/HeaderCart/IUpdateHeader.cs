using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IUpdateHeader
    {
        SqlCommand UpdateHeaderData(HeaderCartEditModel headerCartEditModel, string type);
    }
}