using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface ICreateHeader
    {
        SqlCommand CreateHeaderData(HeaderCartEditModel headerCartEditModel);
    }
}