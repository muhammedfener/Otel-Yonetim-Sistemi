﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Otel_Yonetim_Sistemi
{
    public partial class FrmYonetici : Form
    {
        Baglanti baglanti;
        List<Panel> panels;

        List<int> OdaNumaraListe = new List<int>();
        List<string> CalisanTC = new List<string>();
        Dictionary<int, string> MeslekListesi = new Dictionary<int, string>();
        Dictionary<int, string> KullaniciCalisanListesi = new Dictionary<int, string>();
        Dictionary<int, string> KullaniciYoneticiListesi = new Dictionary<int, string>();
        List<string> KullaniciAdListesi = new List<string>();
        int eskiOdaNumara;
        string eskiTC;

        public FrmYonetici()
        {
            InitializeComponent();
        }

        private void FrmYonetici_Load(object sender, EventArgs e)
        {
            //string connectionString = $"Server={Properties.Settings.Default.dbip};Database={Properties.Settings.Default.dbname};User Id={Properties.Settings.Default.dbuser};Password={Properties.Settings.Default.dbpass};";

            //string connectionString = $"Server=DESKTOP-RN1H7KK\\SQLEXPRESS;Database=BilgiHotel; User Id=MFener; Password=123;";

            string connectionString = "Server=DESKTOP-RN1H7KK\\SQLEXPRESS;Database=BilgiHotel;Trusted_Connection=True;";

            baglanti = new Baglanti(connectionString);


            panels = new List<Panel> { pnlOdaEkle, pnlCalisanEkle, pnlKullaniciEkleDuzenle };

            PanelAc(pnlOdaEkle);

            OdaListele();

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

        private void PanelAc(Panel acilacakPanel)
        {
            foreach (Panel panel in panels)
            {
                panel.Visible = false;
            }
            acilacakPanel.Visible = true;
        }

        private void FrmYonetici_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region Oda İşlemleri

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

        private void btnOdaSec_Click(object sender, EventArgs e)
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

        private void btnOdaDuzenle_Click(object sender, EventArgs e)
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

        private void btnOdaTemizle_Click(object sender, EventArgs e)
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
            btnOdaSec.PerformClick();
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
                MessageBox.Show("Oda Silinirken Hata Oluştu! Hata Mesajı: " + ex.Message);
                return;
            }

            MessageBox.Show("Oda Başarıyla Silindi!");
            OdaListele();
        }

        private void odaDuzenleMenu_Click(object sender, EventArgs e)
        {
            PanelAc(pnlOdaEkle);
        }
        #endregion

        #region Çalışan İşlemleri
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCalisanSec.PerformClick();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string calisanTC = lvwCalisanListesi.SelectedItems[0].SubItems[1].Text;

            try
            {
                string commandString = $"UPDATE calisanlar SET calisanAktifMi = 0 WHERE calisanTCKimlik = '{calisanTC}'";

                baglanti.SorguNonQuery(commandString);

                MessageBox.Show("Çalışan Başarıyla Silindi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Çalışan Silinirken Hata Oluştu! Hata Mesajı: " + ex.Message);
                return;
            }

            CalisanListele();
        }

        private void CalisanListele()
        {
            lvwCalisanListesi.Items.Clear();

            SqlDataReader calisanlar = baglanti.SorguVeriOku("SELECT calisanAd,calisanSoyad,calisanTelefon,calisanTCKimlik,calisanAdres,calisanIrtibatTelefon,calisanSaatlikUcret,meslekAd FROM calisanlar JOIN meslekler ON meslekler.meslekID = calisanlar.calisanMeslekID");

            while (calisanlar.Read())
            {
                CalisanTC.Add(calisanlar["calisanTCKimlik"].ToString());
                string[] satir = { calisanlar["calisanAd"].ToString() + " " + calisanlar["calisanSoyad"].ToString(), calisanlar["calisanTCKimlik"].ToString(), calisanlar["calisanTelefon"].ToString(), calisanlar["meslekAd"].ToString(), calisanlar["calisanAdres"].ToString(), calisanlar["calisanSaatlikUcret"].ToString() };
                ListViewItem item = new ListViewItem(satir);
                lvwCalisanListesi.Items.Add(item);
            }
            calisanlar.Close();
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
            int calisanMeslekID = (int)cmbMeslek.SelectedValue;
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

                MessageBox.Show("Çalışan Eklendi!");
            }
            catch (Exception hata)
            {

                MessageBox.Show("Çalışan Eklenirken Hata Oluştu! Hata Mesajı: " + hata.Message);

            }
        }

        private void btnCalisanDuzenle_Click(object sender, EventArgs e)
        {
            if (CalisanTC.Exists(x => x == txtTC.Text) && txtTC.Text == eskiTC)
            {
                MessageBox.Show("Değiştirmek İstediğiniz Çalışan Zaten Mevcut!");
            }

            if (!CalisanTC.Exists(x => x == txtTC.Text) && eskiTC != txtTC.Text)
            {
                MessageBox.Show("Düzenlemek İstediğiniz Çalışan Mevcut Değil!");
            }

            string calisanAd = txtAd.Text;
            string calisanSoyad = txtSoyad.Text;
            string calisanTelefon = txtTel.Text;
            string calisanTCKimlik = txtTC.Text;
            string calisanAdres = rtxAdres.Text;
            string calisanIrtibat = txtIrtibat.Text;
            DateTime iseBaslamaTarih = dtpIseBaslama.Value.Date;
            DateTime istenAyrilmaTarihi = dtpIstenAyrilma.Value.Date;
            int calisanMeslekID = (int)cmbMeslek.SelectedValue;
            decimal calisanSaatlikUcret = nudSaatlikUcret.Value;
            string commandString = $"UPDATE Calisanlar SET calisanAd = '{calisanAd}', calisanSoyad = '{calisanSoyad}'";
        }

        private void btnCalisanSec_Click(object sender, EventArgs e)
        {
            if (lvwCalisanListesi.SelectedItems.Count != 1)
            {
                MessageBox.Show("Bir Çalışan Seçmelisiniz!");
                return;
            }

            string calisanTC = lvwCalisanListesi.SelectedItems[0].SubItems[1].Text;

            string commandString = $"SELECT * FROM Calisanlar WHERE calisanTCKimlik = '{calisanTC}'";

            SqlDataReader calisan = baglanti.SorguVeriOku(commandString);

            while (calisan.Read())
            {
                txtAd.Text = calisan[1].ToString();
                txtSoyad.Text = calisan[2].ToString();
                txtTel.Text = calisan[3].ToString();
                txtTC.Text = calisan[4].ToString();
                rtxAdres.Text = calisan[5].ToString();
                txtIrtibat.Text = calisan[6].ToString();
                dtpIseBaslama.Value = calisan.GetDateTime(7);
                dtpIstenAyrilma.Value = calisan.IsDBNull(8) ? DateTime.Now : calisan.GetDateTime(8);
                cmbMeslek.SelectedIndex = calisan.GetInt32(9) - 1;
                nudSaatlikUcret.Value = calisan.GetDecimal(10);
            }
            calisan.Close();

            eskiTC = calisanTC;
        }

        #endregion

        #region Kullanıcı İşlemleri

        private void kullaniciEkleDuzenleMenu_Click(object sender, EventArgs e)
        {
            PanelAc(pnlKullaniciEkleDuzenle);
            KullaniciListele();
            KullanicilarComboboxDoldur();
        }

        private void KullaniciListele()
        {
            lvwKullaniciListe.Items.Clear();
            string commandString = $"SELECT kullaniciAdi,kullaniciSifre,kullaniciMail,kullaniciKayitTarihi,(calisanAd + ' ' + calisanSoyad) as CalisanAdSoyad,(yoneticiAd + ' ' + yoneticiSoyad) as YoneticiAdSoyad FROM kullanicilar LEFT JOIN calisanlar ON calisanlar.calisanID = kullanicilar.kullaniciCalisanID LEFT JOIN yoneticiler ON yoneticiler.yoneticiID = kullanicilar.kullaniciYoneticiID";
            SqlDataReader reader = baglanti.SorguVeriOku(commandString);

            while (reader.Read())
            {
                KullaniciAdListesi.Add(reader.GetString(0));
                string[] satir = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.IsDBNull(4) ? " " : reader.GetString(4), reader.IsDBNull(5) ? " " : reader.GetString(5) };

                var listViewItem = new ListViewItem(satir);

                lvwKullaniciListe.Items.Add(listViewItem);
            }
            reader.Close();
        }

        private void KullanicilarComboboxDoldur()
        {
            KullaniciCalisanListesi.Clear();
            KullaniciYoneticiListesi.Clear();
            string commandString = "SELECT calisanID, (calisanAd + ' ' + calisanSoyad) as CalisanAdSoyad FROM calisanlar";

            SqlDataReader reader = baglanti.SorguVeriOku(commandString);
            while (reader.Read())
            {
                KullaniciCalisanListesi.Add(reader.GetInt32(0), reader.GetString(1));
            }
            reader.Close();

            cmbKullaniciCalisan.DataSource = KullaniciCalisanListesi.ToList();
            cmbKullaniciCalisan.ValueMember = "Key";
            cmbKullaniciCalisan.DisplayMember = "Value";

            commandString = "SELECT yoneticiID, (yoneticiAd + ' ' + yoneticiSoyad) as YoneticiAdSoyad FROM yoneticiler";
            reader = baglanti.SorguVeriOku(commandString);
            while (reader.Read())
            {
                KullaniciYoneticiListesi.Add(reader.GetInt32(0), reader.GetString(1));
            }
            reader.Close();

            cmbKullaniciYonetici.DataSource = KullaniciYoneticiListesi.ToList();
            cmbKullaniciYonetici.ValueMember = "Key";
            cmbKullaniciYonetici.DisplayMember = "Value";


            cmbKullaniciCalisan.SelectedIndex = -1;
            cmbKullaniciYonetici.SelectedIndex = -1;
        }

        private void btnKullaniciSec_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = lvwKullaniciListe.SelectedItems[0].SubItems[0].Text;
            string commandString = $"SELECT * FROM kullanicilar WHERE kullaniciAdi = '{kullaniciAdi}'";

            SqlDataReader reader = baglanti.SorguVeriOku(commandString);
            while (reader.Read())
            {
                txtKullaniciAd.Text = reader.GetString(1);
                txtKullaniciSifre.Text = reader.GetString(2);
                txtKullaniciMail.Text = reader.GetString(3);
                if (reader.IsDBNull(5))
                {
                    cmbKullaniciYonetici.SelectedValue = reader.GetInt32(6);
                    cmbKullaniciCalisan.SelectedIndex = -1;
                }
                else
                {
                    cmbKullaniciCalisan.SelectedValue = reader.GetInt32(5);
                    cmbKullaniciYonetici.SelectedIndex = -1;
                }
            }
            reader.Close();
        }

        private void düzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnKullaniciSec.PerformClick();
        }

        private void btnKullaniciEkle_Click(object sender, EventArgs e)
        {
            if (cmbKullaniciCalisan.SelectedIndex != -1 && cmbKullaniciYonetici.SelectedIndex != -1)
            {
                MessageBox.Show("Kullanıcı Hem Yönetici Hem Çalışan Olamaz!");
                cmbKullaniciYonetici.SelectedIndex = -1;
                cmbKullaniciCalisan.SelectedIndex = -1;
                return;
            }
            if (KullaniciAdListesi.Exists(x => x == txtKullaniciAd.Text))
            {
                MessageBox.Show("Bu Kullanıcı Adına Sahip Bir Üye Zaten Var!");
                return;
            }

            string kullaniciAd = txtKullaniciAd.Text;
            string kullaniciSifre = txtKullaniciSifre.Text;
            string kullaniciMail = txtKullaniciMail.Text;
            DateTime kayitTarihi = DateTime.Now;
            /*int calisanID = cmbKullaniciCalisan.SelectedIndex == -1 ? null  : cmbKullaniciCalisan.SelectedIndex;
            int yoneticiID = cmbKullaniciYonetici.SelectedIndex == -1 ? null : cmbKullaniciYonetici.SelectedIndex;
            */
            SqlCommand commandString = new SqlCommand("INSERT INTO kullanicilar (kullaniciAdi, kullaniciSifre, kullaniciMail, kullaniciKayitTarihi, kullaniciCalisanID, kullaniciYoneticiID, kullaniciAktifMi) VALUES(@kullaniciAd, @kullaniciSifre, @kullaniciMail, @kayitTarihi,@calisanID,@yoneticiID,1)");
            commandString.Parameters.AddWithValue("@kullaniciAd", kullaniciAd);
            commandString.Parameters.AddWithValue("@kullaniciSifre", kullaniciSifre);
            commandString.Parameters.AddWithValue("@kullaniciMail", kullaniciMail);
            commandString.Parameters.AddWithValue("@kayitTarihi", kayitTarihi);
            commandString.Parameters.AddWithValue("@calisanID", cmbKullaniciCalisan.SelectedIndex == -1 ? DBNull.Value.ToString() : cmbKullaniciCalisan.SelectedIndex.ToString());
            commandString.Parameters.AddWithValue("@yoneticiID", cmbKullaniciYonetici.SelectedIndex == -1 ? DBNull.Value.ToString() : cmbKullaniciYonetici.SelectedIndex.ToString());

            baglanti.SorguNonQuery(commandString);

            //string commandString = $"INSERT INTO kullanicilar (kullaniciAdi,kullaniciSifre,kullaniciMail,kullaniciKayitTarihi,kullaniciCalisanID,kullaniciYoneticiID,kullaniciAktifMi) VALUES ('{kullaniciAd}','{kullaniciSifre}','{kullaniciMail}','{kayitTarihi}',{calisanID},{yoneticiID},1)";

            KullaniciListele();
        }
        #endregion
    }
}
