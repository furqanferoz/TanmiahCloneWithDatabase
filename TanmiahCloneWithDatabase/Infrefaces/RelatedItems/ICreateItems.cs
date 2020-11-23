using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface ICreateItems
    {
        SqlCommand CreateHeaderData(RelatedItemsEditModel relatedItemsEditModel);
    }
}