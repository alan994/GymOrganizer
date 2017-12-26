using System;
using System.Data.SqlClient;

namespace Logger
{
    public class LoggerService
    {
        private readonly DatabaseLoggerSettings settings;

        public LoggerService(DatabaseLoggerSettings settings)
        {
            this.settings = settings;
        }

        public void Write(string message, int level, string exceptionMsg, int errorCode, string source, int eventId, string tenantGuid, string userId)
        {
            string connectionString = this.settings.ConnectionString;

            var queryString = $@"insert into {this.settings.TableName} ([Message], [TimeStamp], [Level], [Exception], [ErrorCode], [Source], [Tenant], [UserId]) 
                                    values(@msg, @timestamp, @level, @exception, @errorCode, @source, @tenant, @userId)";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@msg", message);
                command.Parameters.AddWithValue("@timestamp", DateTime.UtcNow);
                command.Parameters.AddWithValue("@level", level);
                command.Parameters.AddWithValue("@source", source);

                if (string.IsNullOrEmpty(userId))
                {
                    command.Parameters.AddWithValue("@userId", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@userId", userId);
                }

                if (string.IsNullOrEmpty(tenantGuid))
                {
                    command.Parameters.AddWithValue("@tenant", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@tenant", tenantGuid);
                }
                if (string.IsNullOrEmpty(exceptionMsg))
                {
                    command.Parameters.AddWithValue("@exception", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@exception", exceptionMsg);
                }

                if (errorCode == int.MinValue)
                {
                    command.Parameters.AddWithValue("@errorCode", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@errorCode", errorCode);
                }

                try
                {
                    connection.Open();
                    int effectedrows = command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
