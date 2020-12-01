using System;
using System.Data;
using LeoStore.Shared;
using Npgsql;

namespace LeoStore.Infra.StoreContext.DataContext
{
    public class LeoDataContext : IDisposable
    {
        public NpgsqlConnection Connection { get; set; }

        public LeoDataContext()

        {
            Connection = new NpgsqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}