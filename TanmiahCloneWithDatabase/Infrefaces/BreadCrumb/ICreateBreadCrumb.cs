using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface ICreateBreadCrumb
    {
        SqlCommand CreateBreadCrumbData(BreadCrumbModel breadCrumbModel);
    }
}