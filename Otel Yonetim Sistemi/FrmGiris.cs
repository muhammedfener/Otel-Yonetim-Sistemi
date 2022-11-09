using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_Yonetim_Sistemi
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            FrmAyarlar frmAyarlar = new FrmAyarlar();
            frmAyarlar.Show();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string connectionString = $"Server={Properties.Settings.Default.dbip};Database={Properties.Settings.Default.dbname};User Id={Properties.Settings.Default.dbuser};Password={Properties.Settings.Default.dbpass};";

            Baglanti baglanti = new Baglanti(connectionString);
            string Command = "SELECT kullaniciAdi,kullaniciSifre FROM Kullanicilar";
            SqlDataReader reader = baglanti.SorguVeriOku(Command);

            List<string> kullaniciadlari = new List<string>();
            List<string> kullanicisifreler = new List<string>();

            while (reader.Read())
            {
                kullaniciadlari.Add((string)reader["kullaniciAdi"]);
                kullanicisifreler.Add((string)reader["kullaniciSifre"]);
            }
            
            reader.Close();

            string girilenkullaniciad = txtKullaniciAd.Text;

            if(kullaniciadlari.Exists(x => x == girilenkullaniciad))
            {

            }
        }
    }
}
