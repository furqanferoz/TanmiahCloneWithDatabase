using System.Data.SqlClient;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface ICreateIngredient
    {
        SqlCommand CreateIngredientData(IngredientEditModel ingredientEditModel);
    }
}