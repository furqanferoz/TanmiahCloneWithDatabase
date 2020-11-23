using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IUpdateItems
    {
        SqlCommand UpdateItemsData(RelatedItemsEditModel relatedItemsEditModel, string type);
    }
}