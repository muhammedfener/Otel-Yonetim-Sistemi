using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Otel_Yonetim_Sistemi
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        private void btnAyarKaydet_Click(object sender, EventArgs e)
        {
            if (BaglantiTest())
            {
                string serverName = txtServerIp.Text;
                string username = txtDbUser.Text;
                string password = txtDbPass.Text;
                string dbname = txtDbName.Text;

                try
                {
                    Properties.Settings.Default.dbip = serverName;
                    Properties.Settings.Default.dbuser = username;
                    Properties.Settings.Default.dbpass = password;
                    Properties.Settings.Default.dbname = dbname;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Ayarlar Kaydedildi!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ayarlar Kaydedilemedi! Hata Mesajı: " + ex.Message);

                }
            }
        }

        private void btnBaglantiTest_Click(object sender, EventArgs e)
        {
            if (BaglantiTest())
            {
                MessageBox.Show("Bağlantı Başarılı");
            }
        }

        private bool BaglantiTest()
        {
            string serverName = txtServerIp.Text;
            string username = txtDbUser.Text;
            string password = txtDbPass.Text;
            string dbname = txtDbName.Text;

            string constring = $"Server={serverName};Database={dbname};User Id={username};Password={password};";
            SqlConnection testconnection = new SqlConnection(constring);
            try
            {
                testconnection.Open();
                return true;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bağlantı Hatası!" + hata.ToString(), "HATA!");
                return false;
            }
        }
    }
}
