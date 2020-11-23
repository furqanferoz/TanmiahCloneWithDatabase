using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IUpdateIngredient
    {
        SqlCommand UpdateIngredientData(IngredientEditModel ingredientEditModel, string type);
    }
}