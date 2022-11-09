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
    public partial class FrmYonetici : Form
    {
        Baglanti baglanti;

        public FrmYonetici()
        {
            InitializeComponent();
        }

        private void FrmYonetici_Load(object sender, EventArgs e)
        {
            string connectionString = $"Server={Properties.Settings.Default.dbip};Database={Properties.Settings.Default.dbname};User Id={Properties.Settings.Default.dbuser};Password={Properties.Settings.Default.dbpass};";
            
            baglanti = new Baglanti(connectionString);

            SqlDataReader odalar = baglanti.SorguVeriOku("SELECT * FROM odalar");
            while (odalar.Read())
            {
                string[] row = { odalar["odaNumara"].ToString(),odalar["odaKat"].ToString(), odalar["odaKisiSayisi"].ToString(), odalar["odaFiyat"].ToString() };
                var listViewItem = new ListViewItem(row);

                lvwOdaListesi.Items.Add(listViewItem);
            }
            odalar.Close();
            
        }
        
        private void btnOdaDuzenle_Click(object sender, EventArgs e)
        {
            if(lvwOdaListesi.SelectedItems.Count != 1)
            {
                MessageBox.Show("Oda Seçin!");
            }
            else
            {
                string odaNumarasi = lvwOdaListesi.SelectedItems[0].SubItems[0].Text;
                SqlDataReader odalar = baglanti.SorguVeriOku($"SELECT * FROM odalar WHERE odaNumara = {odaNumarasi}");

                while (odalar.Read())
                {
                    nudOdaNumara.Value = Convert.ToDecimal(odalar["odaNumara"]);
                    nudOdaKat.Value = Convert.ToDecimal(odalar["odaKat"]);
                    nudOdaKisi.Value = Convert.ToDecimal(odalar["odaKisiSayisi"]);
                    nudOdaFiyat.Value = Convert.ToDecimal(odalar["odaFiyat"]);
                    rtxOdaAciklama.Text = (string)odalar["odaAciklama"];
                }
               
                odalar.Close();

                SqlDataReader odaOzellikler = baglanti.SorguVeriOku($"SELECT * FROM odalar_odaOzellik WHERE odaNumara = {odaNumarasi}");
                while (odaOzellikler.Read())
                {
                    switch ((int)odaOzellikler["ozellikID"])
                    {
                        case 1:
                            cklOzellikler.SetItemChecked(0, true);
                            break;
                        case 2:
                            cklOzellikler.SetItemChecked(1, true);
                            break;
                        case 3:
                            cklOzellikler.SetItemChecked(2, true);
                            break;
                        case 4:
                            cklOzellikler.SetItemChecked(3, true);
                            break;
                        case 5:
                            cklOzellikler.SetItemChecked(4, true);
                            break;
                        case 6:
                            cklOzellikler.SetItemChecked(5, true);
                            break;
                        default:
                            break;
                    }
                }
                odaOzellikler.Close();

                SqlDataReader odaYataklar = baglanti.SorguVeriOku($"SELECT * FROM odalar_odaYataklar WHERE odaNumara = {odaNumarasi}");

                NuDTemizle();


                while (odaYataklar.Read())
                {
                    switch ((int)odaYataklar["yatakID"])
                    {
                        case 1:
                            nudTekKisilikYatak.Value = Convert.ToDecimal(odaYataklar["yatakAdet"]);
                            break;
                        case 2:
                            nudCiftKisilikYatak.Value = Convert.ToDecimal(odaYataklar["yatakAdet"]);
                            break;
                        case 3:
                            nudTekKisilikYatak.Value = Convert.ToDecimal(odaYataklar["yatakAdet"]);
                            break;
                    }
                }

                odaYataklar.Close();

            }
        }

        private void NuDTemizle()
        {
           
            foreach (var item in pnlOdaEkle.Controls)
            {
                if (item is NumericUpDown)
                {
                   NumericUpDown nud = (NumericUpDown)item;
                    nud.Value=0;

                }
               
            }
        }

        private void btnOdaEkle_Click(object sender, EventArgs e)
        {
            string commandstring = "INSERT INTO Odalar (odaNumara,odaKat,odaKisiSayisi,odaFiyat,odaAciklama,odaDoluMu,odaAktifMi) VALUES (@odaNumara,@odaKat,@odaKisiSayisi,@odaFiyat,@odaAciklama,0,1); SELECT SCOPE_IDENTITY();";

            baglanti.SorguScalar(commandstring);
        }

        private void btnOdaDegistir_Click(object sender, EventArgs e)
        {

        }
    }
}
