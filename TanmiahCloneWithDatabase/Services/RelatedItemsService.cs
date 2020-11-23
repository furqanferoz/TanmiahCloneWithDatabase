using SqlItmes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public class RelatedItemsService : IRelatedItemsService
    {
        RelatedItemsEditModel relatedItemsEditModel = new RelatedItemsEditModel();
        DataTable dataTable;
        private IGetRelatedItems _getRelatedItems;

        public RelatedItemsService(IGetRelatedItems getRelatedItems)
        {
            this._getRelatedItems = getRelatedItems;
        }

        public RelatedItemsEditModel FillData(int id)
        {
            dataTable = new DataTable();
            dataTable = this._getRelatedItems.GetRelatedItemsData(id);
            if (dataTable.Rows.Count == 1)
            {
                relatedItemsEditModel.ID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                relatedItemsEditModel.Image = dataTable.Rows[0][1].ToString();
                relatedItemsEditModel.Name = dataTable.Rows[0][2].ToString();
                relatedItemsEditModel.Title = dataTable.Rows[0][3].ToString();
                relatedItemsEditModel.Description = dataTable.Rows[0][4].ToString();
               
                return relatedItemsEditModel;
            }
            return relatedItemsEditModel;
        }
    }
    public class GetRelatedItems : IGetRelatedItems
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        DataTable dataTable;

        public DataTable GetRelatedItemsData(int id)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterRelatedItems", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "SingleSelect");
                sqlCommand.Parameters.AddWithValue("@ID", id);

                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                return dataTable;
            }
        }
        public DataTable GetRelatedItemsData()
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterRelatedItems", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "AllSelect");
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                return dataTable;
            }
        }
    }
    public class CreateItems : ICreateItems
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand CreateHeaderData(RelatedItemsEditModel relatedItemsEditModel)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterRelatedItems", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Insert");
                sqlCommand.Parameters.AddWithValue("@RelatedTitle", relatedItemsEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@RelatedName", relatedItemsEditModel.Name);
                sqlCommand.Parameters.AddWithValue("@RelatedDesc", relatedItemsEditModel.Description);
                sqlCommand.Parameters.AddWithValue("@RelatedImage", relatedItemsEditModel.Image);
                sqlCommand.ExecuteNonQuery();
            }
            return sqlCommand;
        }
    }
    public class UpdateItems : IUpdateItems
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand UpdateItemsData(RelatedItemsEditModel relatedItemsEditModel, string type)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterRelatedItems", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", type);
                sqlCommand.Parameters.AddWithValue("@ID", relatedItemsEditModel.ID);
                sqlCommand.Parameters.AddWithValue("@RelatedTitle", relatedItemsEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@RelatedName", relatedItemsEditModel.Name);
                sqlCommand.Parameters.AddWithValue("@RelatedDesc", relatedItemsEditModel.Description);
                sqlCommand.Parameters.AddWithValue("@RelatedImage", relatedItemsEditModel.Image);
                sqlCommand.ExecuteNonQuery();
                return sqlCommand;
            }
        }
    }
}