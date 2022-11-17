using System;
using System.Data.SqlClient;
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
            //string connectionString = $"Server=DESKTOP-RN1H7KK\\SQLEXPRESS;Database=BilgiHotel; User Id=MFener; Password=1234;";
            //string connectionString = "Server=DESKTOP-RN1H7KK\\SQLEXPRESS;Database=BilgiHotel;Trusted_Connection=True;";
            string kullaniciAd = txtKullaniciAd.Text;
            string kullaniciSifre = txtSifre.Text;

            Baglanti baglanti = new Baglanti(connectionString);
            string Command = $"SELECT * FROM kullanicilar WHERE kullaniciAdi = '{kullaniciAd}' AND kullaniciSifre = '{kullaniciSifre}'";
            SqlDataReader reader = baglanti.SorguVeriOku(Command);

            if (!reader.HasRows)
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre!");
            }
            else
            {
                while (reader.Read())
                {

                    if (reader[5] == DBNull.Value)
                    {
                        FrmYonetici yoneticiForm = new FrmYonetici();
                        yoneticiForm.Show();
                        this.Hide();
                        return;
                    }

                    if (reader.IsDBNull(6))
                    {
                        FrmResepsiyon resepsiyonForm = new FrmResepsiyon();
                        resepsiyonForm.Show();
                        this.Hide();
                        return;
                    }
                    MessageBox.Show("Hesabınız İçin Yetkilendirme Yapılmamış!");
                }
            }
        }
    }
}
