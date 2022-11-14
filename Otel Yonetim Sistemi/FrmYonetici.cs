using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        List<Panel> panels;

        List<int> OdaNumaraListe = new List<int>();
        List<string> CalisanTC = new List<string>();
        Dictionary<int,string> MeslekListesi = new Dictionary<int, string>();

        int eskiOdaNumara;

        public FrmYonetici()
        {
            InitializeComponent();
        }

        private void FrmYonetici_Load(object sender, EventArgs e)
        {
            string connectionString = $"Server={Properties.Settings.Default.dbip};Database={Properties.Settings.Default.dbname};User Id={Properties.Settings.Default.dbuser};Password={Properties.Settings.Default.dbpass};";

            //string connectionString = $"Server=DESKTOP-RN1H7KK\\SQLEXPRESS;Database=BilgiHotel; User Id=MFener; Password=123;";

            //string connectionString = "Server=DESKTOP-RN1H7KK\\SQLEXPRESS;Database=BilgiHotel;Trusted_Connection=True;";

            baglanti = new Baglanti(connectionString);


            panels = new List<Panel> { pnlOdaEkle,pnlCalisanEkle };

            PanelAc(pnlOdaEkle);

            OdaListele();

        }

        private void PanelAc(Panel acilacakPanel)
        {
            foreach(Panel panel in panels)
            {
                panel.Visible = false;
            }
            acilacakPanel.Visible = true;
        }

        private void OdaListele()
        {
            lvwOdaListesi.Items.Clear();
            OdaNumaraListe.Clear();

            SqlDataReader odalar = baglanti.SorguVeriOku("SELECT * FROM odalar");
            while (odalar.Read())
            {
                OdaNumaraListe.Add((int)odalar["odaNumara"]);
                string[] satir = { odalar["odaNumara"].ToString(), odalar["odaKat"].ToString(), odalar["odaKisiSayisi"].ToString(), odalar["odaFiyat"].ToString() };
                var listViewItem = new ListViewItem(satir);

                lvwOdaListesi.Items.Add(listViewItem);
            }
            odalar.Close();
        }

        private void CalisanListele()
        {
            lvwCalisanListesi.Items.Clear();

            SqlDataReader calisanlar = baglanti.SorguVeriOku("SELECT calisanAd,calisanSoyad,calisanTelefon,calisanTCKimlik,calisanAdres,calisanIrtibatTelefon,calisanSaatlikUcret,meslekAd FROM calisanlar JOIN meslekler ON meslekler.meslekID = calisanlar.calisanMeslekID");

            while (calisanlar.Read())
            {
                CalisanTC.Add(calisanlar["calisanTCKimlik"].ToString());
                string[] satir = { calisanlar["calisanAd"].ToString() + " " +calisanlar["calisanSoyad"].ToString(), calisanlar["calisanTCKimlik"].ToString(), calisanlar["calisanTelefon"].ToString(), calisanlar["meslekAd"].ToString(), calisanlar["calisanAdres"].ToString(), calisanlar["calisanSaatlikUcret"].ToString()};
                ListViewItem item = new ListViewItem(satir);
                lvwCalisanListesi.Items.Add(item);
            }
            calisanlar.Close();
        }

        private void btnOdaDuzenle_Click(object sender, EventArgs e)
        {

            if (lvwOdaListesi.SelectedItems.Count != 1)
            {
                MessageBox.Show("Oda Seçin!");
                return;
            }

            VerileriTemizle();
            string odaNumarasi = lvwOdaListesi.SelectedItems[0].SubItems[0].Text;
            SqlDataReader odalar = baglanti.SorguVeriOku($"SELECT * FROM odalar WHERE odaNumara= {odaNumarasi}");

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
                        chkKralOdasi.Checked = true;
                        break;
                }
            }

            odaYataklar.Close();

            eskiOdaNumara = Convert.ToInt32(odaNumarasi);
        }

        private void VerileriTemizle()
        {

            foreach (var item in pnlOdaEkle.Controls)
            {

                if (item is NumericUpDown)
                {
                    NumericUpDown nud = (NumericUpDown)item;
                    nud.Value = 0;
                }

                if (item is CheckedListBox)
                {
                    CheckedListBox ckl = (CheckedListBox)item;
                    for (int i = 0; i < ckl.Items.Count; i++)
                    {
                        ckl.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }

                if (item is RichTextBox)
                {
                    RichTextBox rich = (RichTextBox)item;
                    rich.Text = "";
                }

                if (chkKralOdasi.Checked)
                {
                    chkKralOdasi.CheckState = CheckState.Unchecked;
                }

            }
        }

        private void btnOdaEkle_Click(object sender, EventArgs e)
        {
            if (OdaNumaraListe.Exists(x => x == nudOdaNumara.Value))
            {
                MessageBox.Show("Oda Numarasına Sahip Bir Oda Zaten Var!");
            }
            else
            {
                int odaNumara = (int)nudOdaNumara.Value;
                int odaKat = (int)nudOdaKat.Value;
                int odaKisiSayisi = (int)nudOdaKisi.Value;
                int odaFiyat = (int)nudOdaFiyat.Value;
                string odaAciklama = rtxOdaAciklama.Text;
                int tekKisilikYatakAdet = (int)nudTekKisilikYatak.Value;
                int ciftKisilikYatakAdet = (int)nudCiftKisilikYatak.Value;

                try
                {
                    string commandstring = $"INSERT INTO Odalar (odaNumara,odaKat,odaKisiSayisi,odaFiyat,odaAciklama,odaDoluMu,odaAktifMi) VALUES ({odaNumara},{odaKat},{odaKisiSayisi},{odaFiyat},'{odaAciklama}',0,1)";

                    baglanti.SorguNonQuery(commandstring);

                    foreach (int itemIndices in cklOzellikler.CheckedIndices)
                    {
                        commandstring = $"INSERT INTO odalar_odaOzellik (odaNumara,ozellikID) VALUES({odaNumara},{itemIndices + 1})";
                        baglanti.SorguNonQuery(commandstring);
                    }

                    if (chkKralOdasi.Checked)
                    {
                        commandstring = $"INSERT INTO odalar_odaYataklar (odaNumara,yatakID,yatakAdet) VALUES ({odaNumara},3,1)";
                    }
                    else
                    {
                        commandstring = $"INSERT INTO odalar_odaYataklar (odaNumara,yatakID,yatakAdet) VALUES ({odaNumara},1,{tekKisilikYatakAdet}),({odaNumara},1,{ciftKisilikYatakAdet})";
                    }

                    baglanti.SorguNonQuery(commandstring);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oda Eklenirken Hata Oluştu! Hata Mesajı: " + ex.Message);
                    return;
                }
                OdaListele();
            }
        }

        private void btnOdaDegistir_Click(object sender, EventArgs e)
        {
            int odaNumara = (int)nudOdaNumara.Value;
            int odaKat = (int)nudOdaKat.Value;
            int odaKisiSayisi = (int)nudOdaKisi.Value;
            int odaFiyat = (int)nudOdaFiyat.Value;
            string odaAciklama = rtxOdaAciklama.Text;
            int tekKisilikYatakAdet = (int)nudTekKisilikYatak.Value;
            int ciftKisilikYatakAdet = (int)nudCiftKisilikYatak.Value;

            if (OdaNumaraListe.Exists(x => x == odaNumara) && OdaNumaraListe.Exists(x => x == eskiOdaNumara) && odaNumara != eskiOdaNumara)
            {
                MessageBox.Show("Yeni Oda Numarası Daha Önceden Tanımlı Olamaz!");
                return;
            }

            if (!OdaNumaraListe.Exists(x => x == eskiOdaNumara))
            {
                MessageBox.Show("Oda Düzenleyebilmeniz İçin Odanın Var Olması Gerekir!");
                return;
            }

            if (!OdaNumaraListe.Remove(eskiOdaNumara))
            {
                MessageBox.Show("Eski Oda Silinirken Hata Oluştu!");
                return;
            }

            if (cklOzellikler.CheckedItems.Count == 0)
            {
                MessageBox.Show("En Az Bir Özellik Seçmelisiniz!");
                return;
            }

            try
            {
                string commandstring = $"UPDATE odalar SET odaNumara = {odaNumara}, odaKat={odaKat}, odaKisiSayisi = {odaKisiSayisi}, odaFiyat = {odaFiyat}, odaAciklama ='{odaAciklama}' WHERE odaNumara = {eskiOdaNumara}";
                int id = baglanti.SorguNonQuery(commandstring);

                if (id != 1)
                {
                    MessageBox.Show("Oda Düzenlenirken Hata Oluştu!");
                    return;
                }
                else
                {
                    MessageBox.Show("Oda Düzenlendi!");
                }

                commandstring = $"DELETE FROM odalar_odaYataklar WHERE odaNumara = {eskiOdaNumara}";
                baglanti.SorguNonQuery(commandstring);

                if (chkKralOdasi.Checked)
                {
                    commandstring = $"INSERT INTO odalar_odaYataklar (odaNumara,yatakID,yatakAdet) VALUES ({odaNumara},3,1)";
                }
                else
                {
                    commandstring = $"INSERT INTO odalar_odaYataklar (odaNumara,yatakID,yatakAdet) VALUES({odaNumara},1,{tekKisilikYatakAdet}),({odaNumara},2,{ciftKisilikYatakAdet})";
                }
                baglanti.SorguNonQuery(commandstring);

                commandstring = $"DELETE FROM odalar_odaOzellik WHERE odaNumara ={eskiOdaNumara};";
                baglanti.SorguNonQuery(commandstring);

                List<int> ozellikler = new List<int>();
                foreach (int itemIndices in cklOzellikler.CheckedIndices)
                {
                    ozellikler.Add(itemIndices + 1);
                }

                commandstring = "INSERT INTO odalar_odaOzellik (odaNumara,ozellikID) VALUES";
                int sonozellik = ozellikler.Last();
                foreach (int ozellik in ozellikler)
                {
                    if (ozellik.Equals(sonozellik))
                    {
                        commandstring += $"({odaNumara},{ozellik + 1})";
                    }
                    else
                    {
                        commandstring += $"({odaNumara},{ozellik + 1}),";
                    }
                }
                commandstring += ";";

                baglanti.SorguNonQuery(commandstring);
            }
            catch (Exception hata)
            {
                MessageBox.Show("Oda Düzenlenirken Hata Oluştu! Hata Mesajı: " + hata.Message);
            }

            OdaListele();
        }

        private void FrmYonetici_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            VerileriTemizle();
        }

        private void chkKralOdasi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKralOdasi.Checked)
            {
                nudCiftKisilikYatak.Enabled = false;
                nudTekKisilikYatak.Enabled = false;
            }
            else
            {
                nudCiftKisilikYatak.Enabled = true;
                nudTekKisilikYatak.Enabled = true;
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            btnOdaDuzenle.PerformClick();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            int odaNumara = Convert.ToInt32(lvwOdaListesi.SelectedItems[0].SubItems[0].Text);

            try
            {
                string odaSil = $"DELETE FROM odalar WHERE odaNumara = {odaNumara}";
                string odaOzellikSil = $"DELETE FROM odalar_odaOzellik WHERE odaNumara = {odaNumara}";
                string odaYatakSil = $"DELETE FROM odalar_odaYataklar WHERE odaNumara = {odaNumara}";

                baglanti.SorguNonQuery(odaOzellikSil);
                baglanti.SorguNonQuery(odaYatakSil);
                baglanti.SorguNonQuery(odaSil);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Oda Silinirken Hata Oluştu! Hata Mesajı: "  + ex.Message);
                return;
            }

            MessageBox.Show("Oda Başarıyla Silindi!");
            OdaListele();
        }

        private void odaDuzenleMenu_Click(object sender, EventArgs e)
        {
            PanelAc(pnlOdaEkle);
        }

        private void calisanEkleMenu_Click(object sender, EventArgs e)
        {
            PanelAc(pnlCalisanEkle);
            CalisanListele();
            MeslekListesi.Clear();
            string commandstring = "SELECT meslekID,meslekAd FROM Meslekler";
            SqlDataReader reader = baglanti.SorguVeriOku(commandstring);
            while (reader.Read())
            {
                MeslekListesi.Add((int)reader[0], (string)reader[1]);
            }
            cmbMeslek.DataSource = MeslekListesi.ToList();
            cmbMeslek.ValueMember = "Key";
            cmbMeslek.DisplayMember = "Value";
            reader.Close();
        }


        private void btnCalisanKaydet_Click(object sender, EventArgs e)
        {
            string calisanAd = txtAd.Text;
            string calisanSoyad = txtSoyad.Text;
            string calisanTelefon = txtTel.Text;
            string calisanTCKimlik = txtTC.Text;
            string calisanAdres = rtxAdres.Text;
            string calisanIrtibat = txtIrtibat.Text;
            DateTime iseBaslamaTarih = dtpIseBaslama.Value.Date;
            //DateTime istenAyrilmaTarih = dtpIstenAyrilma.Value.Date;
            int calisanMeslekID = (int) cmbMeslek.SelectedValue;
            decimal calisanSaatlikUcret = nudSaatlikUcret.Value;

            if (CalisanTC.Exists(x => x == calisanTCKimlik))
            {
                MessageBox.Show("Bu TC Kimliğine Sahip Bir Çalışan Zaten Var!");
                return;
            }

            try
            {
                string commandstring = $"INSERT INTO Calisanlar (calisanAd,calisanSoyad,calisanTelefon,calisanTCKimlik,calisanAdres,calisanIrtibatTelefon,calisanIseBaslamaTarihi,calisanMeslekID,calisanSaatlikUcret,calisanAktifMi) VALUES ({calisanAd},{calisanSoyad},{calisanTelefon},{calisanTCKimlik},{calisanAdres},{calisanIrtibat},{iseBaslamaTarih},{calisanMeslekID},{calisanSaatlikUcret},1)";

                baglanti.SorguNonQuery(commandstring);
            }
            catch (Exception hata)
            {

                MessageBox.Show("Çalışan Eklenirken Hata Oluştu! Hata Mesajı: " + hata.Message);
            }
        }
    }
}
