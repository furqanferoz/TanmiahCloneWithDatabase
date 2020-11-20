using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IUpdateBreadCrumb
    {
        SqlCommand UpdateBreadCrumbData(BreadCrumbModel breadCrumbModel, string type);
    }
}