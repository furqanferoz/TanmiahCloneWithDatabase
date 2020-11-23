using System.Data;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IGetCart
    {
        DataTable GetCartData(int ID);
    }
}