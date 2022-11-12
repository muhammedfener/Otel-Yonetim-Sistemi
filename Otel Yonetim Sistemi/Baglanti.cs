using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel_Yonetim_Sistemi
{
    public class Baglanti
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
       // string connectionString = $"Server={Properties.Settings.Default.dbip};Database={Properties.Settings.Default.dbname};User Id={Properties.Settings.Default.dbuser};Password={Properties.Settings.Default.dbpass};";

        public Baglanti(string ConnectionString)
        {
            try 
            { 
                this.connection = new SqlConnection(ConnectionString); 
                command = new SqlCommand();
                connection.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Veritabanına Bağlanılamadı! Hata Mesajı: " +ex.Message);
            }
        }

        public SqlDataReader SorguVeriOku(string Command)
        {
            this.command.Connection = connection;
            this.command.CommandText = Command;

            this.reader = this.command.ExecuteReader();

            return reader;
        }

        public SqlDataReader SorguVeriOku(SqlCommand Command)
        {
            this.command.Connection = connection;
            this.command.CommandText = Command.CommandText;

            this.reader = this.command.ExecuteReader();

            return reader;
        }

        public int SorguNonQuery(SqlCommand Command)
        {
            this.command.Connection = this.connection;
            this.command.CommandText = Command.CommandText;

            int DonecekDeger = this.command.ExecuteNonQuery();

            return DonecekDeger;
        }

        public int SorguNonQuery(string Command)
        {
            this.command.Connection = this.connection;
            this.command.CommandText = Command;

            int DonecekDeger = this.command.ExecuteNonQuery();

            return DonecekDeger;
        }

        public decimal SorguScalar(SqlCommand Command)
        {
            this.command.Connection = this.connection;
            this.command.CommandText = Command.CommandText;

            decimal DonecekDeger = (decimal) this.command.ExecuteScalar();

            return DonecekDeger;
        }

        public decimal SorguScalar(string Command)
        {
            this.command.Connection = this.connection;
            this.command.CommandText = Command;

            decimal DonecekDeger = (decimal)this.command.ExecuteScalar();

            return DonecekDeger;
        }
    }
}
