using Audit.SqlServer.Providers;
using SB.Resources;
using System;
using System.Data.SqlClient;

namespace SB.Data
{
    public class AuditProvider : SqlDataProvider
    {
        public AuditProvider()
        {
            ConnectionString = string.Format(Parameters.BaseConn, $"{Singleton.Instance.DbName}_Audit");
            Schema = "dbo";
            TableName = "Event";
            IdColumnName = "EventId";
            JsonColumnName = "Data";
            LastUpdatedDateColumnName = "LastUpdatedDate";
            CrearBbAudit();
        }

        private static void CrearBbAudit()
        {
            if (!Singleton.Instance.Audit) return;
            var connectionString = string.Format(Parameters.BaseConn, Singleton.Instance.DbName);
            var commandText = $"SELECT name FROM master.dbo.sysdatabases WHERE (\'[\' + name + \']\' = N\'SB.{Singleton.Instance.DbName}_Audit\' OR name = N\'SB.{Singleton.Instance.DbName}_Audit\');";
            var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlDataReader reader;
                using (var cmd = new SqlCommand(commandText, conn))
                    reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    commandText = $"CREATE DATABASE [SB.{Singleton.Instance.DbName}_Audit];";
                    using (var cmd = new SqlCommand(commandText, conn))
                        cmd.ExecuteNonQuery();
                    commandText = $"USE [SB.{Singleton.Instance.DbName}_Audit]\r\nCREATE TABLE [Event]\r\n(\r\n\t[EventId] BIGINT IDENTITY(1,1) NOT NULL,\r\n\t[InsertedDate] DATETIME NOT NULL DEFAULT(GETDATE()),\r\n\t[LastUpdatedDate] DATETIME NULL,\r\n\t[Data] NVARCHAR(MAX) NOT NULL,\r\n\tCONSTRAINT PK_Event PRIMARY KEY (EventId)\r\n)";
                    using (var cmd = new SqlCommand(commandText, conn))
                        cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
