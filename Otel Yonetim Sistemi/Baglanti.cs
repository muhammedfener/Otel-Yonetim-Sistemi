using System;
using System.Data;
using System.Data.SqlClient;

namespace Otel_Yonetim_Sistemi
{
    public class Baglanti
    {
        public SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public string connectionString = $"Server={Properties.Settings.Default.dbip};Database={Properties.Settings.Default.dbname};User Id={Properties.Settings.Default.dbuser};Password={Properties.Settings.Default.dbpass};";
        //public string connectionString = "Server=DESKTOP-RN1H7KK\\SQLEXPRESS;Database=BilgiHotel;Trusted_Connection=True;";
        public Baglanti(string ConnectionString = null)
        {
            try
            {
                if(ConnectionString != null)
                {
                    this.connection = new SqlConnection(ConnectionString);
                }
                else
                {
                    this.connection = new SqlConnection(connectionString);
                }
                command = new SqlCommand();
                connection.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Veritabanına Bağlanılamadı! Hata Mesajı: " + ex.Message);
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
            Command.Connection = this.connection;

            this.reader = Command.ExecuteReader();

            return reader;
        }

        public int SorguNonQuery(SqlCommand Command)
        {

            Command.Connection = this.connection;

            int DonecekDeger = Command.ExecuteNonQuery();

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

            Command.Connection = this.connection;

            decimal DonecekDeger = (decimal)Command.ExecuteScalar();

            return DonecekDeger;
        }

        public decimal SorguScalar(string Command)
        {
            this.command.Connection = this.connection;
            this.command.CommandText = Command;

            decimal DonecekDeger = (decimal)this.command.ExecuteScalar();

            return DonecekDeger;
        }

        public SqlDataAdapter DataAdapterDondur(SqlCommand Command)
        {
            Command.Connection = this.connection;

            SqlDataAdapter da = new SqlDataAdapter(Command);

            /*DataSet dt = new DataSet();

            da.Fill(dt);*/

            return da;
        }
    }
}
