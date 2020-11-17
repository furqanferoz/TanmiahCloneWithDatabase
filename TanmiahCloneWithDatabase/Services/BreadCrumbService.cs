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
    public class BreadCrumbService
    {
        GetBreadCrumb GetBreadCrumb = new GetBreadCrumb();
        DataTable dataTable;
        BreadCrumbModel breadCrumbModel;
        public BreadCrumbModel FillData(int id)
        {
            dataTable = new DataTable();
            breadCrumbModel = new BreadCrumbModel();

            dataTable =GetBreadCrumb.GetBreadCrumbData(id);
            if (dataTable.Rows.Count == 1)
            {
                breadCrumbModel.ID = Convert.ToInt32(dataTable.Rows[0][0]);
                breadCrumbModel.MainLink = dataTable.Rows[0][1].ToString();
                breadCrumbModel.Link = dataTable.Rows[0][2].ToString();
                breadCrumbModel.SubLink = dataTable.Rows[0][3].ToString();
                return breadCrumbModel;
            }
            return breadCrumbModel;
        }
    }

    public class GetBreadCrumb
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        DataTable dataTable;
        public DataTable GetBreadCrumbData(int ID)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterBreadCrumb", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Select");
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                return dataTable;
            }
        }
        public class UpdateBreadCrumb
        {
            SqlCommand sqlCommand;
            SqlConnection sqlConnection;

            public SqlCommand UpdateBreadCrumbData(BreadCrumbModel breadCrumbModel, string type)
            {
                using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("SpMasterBreadCrumb", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StatementType", type);
                    sqlCommand.Parameters.AddWithValue("@ID", breadCrumbModel.ID);
                    sqlCommand.Parameters.AddWithValue("@MainLink", breadCrumbModel.MainLink);
                    sqlCommand.Parameters.AddWithValue("@Link", breadCrumbModel.Link);
                    sqlCommand.Parameters.AddWithValue("@SubLink", breadCrumbModel.SubLink);
                    
                    sqlCommand.ExecuteNonQuery();
                    return sqlCommand;
                }
            }
        }
        public class CreateBreadCrumb
        {
            SqlCommand sqlCommand;
            SqlConnection sqlConnection;

            public SqlCommand CreateBreadCrumbData(BreadCrumbModel breadCrumbModel)
            {
                using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("SpMasterBreadCrumb", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@StatementType", "Insert");
                    sqlCommand.Parameters.AddWithValue("@MainLink", breadCrumbModel.MainLink);
                    sqlCommand.Parameters.AddWithValue("@Link", breadCrumbModel.Link);
                    sqlCommand.Parameters.AddWithValue("@SubLink", breadCrumbModel.SubLink);
                    sqlCommand.ExecuteNonQuery();
                }
                return sqlCommand;
            }
        }
    }
}