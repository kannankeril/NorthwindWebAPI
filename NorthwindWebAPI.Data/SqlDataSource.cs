using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NorthwindWebAPI.Data
{
    public class SqlDataSource
    {
        public string ConnectionString { get; set; }

        public SqlDataSource()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["NorthwindDbConnection"].ConnectionString;
        }

        public virtual SqlConnection CreateConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        protected virtual IDataReader ExecuteReader(string commandText, SqlParameter[] parameters = null
                                            , CommandType commandType = CommandType.StoredProcedure
                                            , SqlConnection connection = null)
        {
            bool isNewConnection = false;
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                connection = CreateConnection();
                isNewConnection = true;
            }

            try
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var commandBehavior = isNewConnection ? CommandBehavior.CloseConnection : CommandBehavior.Default;
                return command.ExecuteReader(commandBehavior);
            }
            catch (Exception)
            {
                if (isNewConnection && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                throw;
            }
        }

        protected virtual int ExecuteNonQuery(string commandText, SqlParameter[] parameters = null
                                            , CommandType commandType = CommandType.StoredProcedure
                                            , SqlConnection connection = null
                                            , SqlTransaction transaction = null)
        {
            bool isLocalConnection = false;
            try
            {
                SqlCommand sqlCommand = BuildSqlCommand(commandText, parameters, commandType, ref connection, transaction, ref isLocalConnection);

                return sqlCommand.ExecuteNonQuery();
            }
            finally
            {   
                if (isLocalConnection && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        protected virtual object ExecuteScalar(string commandText, SqlParameter[] parameters = null
                                            , CommandType commandType = CommandType.StoredProcedure
                                            , SqlConnection connection = null
                                            , SqlTransaction transaction = null)
        {
            bool isLocalConnection = false;
            try
            {
                SqlCommand sqlCommand = BuildSqlCommand(commandText, parameters, commandType, ref connection, transaction
                                                        , ref isLocalConnection);

                return sqlCommand.ExecuteScalar();
            }
            finally
            {
                if (isLocalConnection && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }


        private SqlCommand BuildSqlCommand(string commandText, SqlParameter[] parameters, CommandType commandType
                                            , ref SqlConnection connection
                                            , SqlTransaction transaction
                                            , ref bool isLocalConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(commandText);
            sqlCommand.CommandType = commandType;
            if (parameters != null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }

            if (connection == null || connection.State == ConnectionState.Closed)
            {
                connection = CreateConnection();
                isLocalConnection = true;
            }

            sqlCommand.Connection = connection;

            if (transaction != null)
                sqlCommand.Transaction = transaction;
            return sqlCommand;
        }
    }
}
