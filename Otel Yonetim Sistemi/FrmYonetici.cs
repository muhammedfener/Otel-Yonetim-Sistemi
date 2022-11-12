using System;
using System.Collections;
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

        List<string> OdaNumaraListe = new List<string>();
        string eskiOdaNumara;
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

            OdaListele();
            
        }
        private void OdaListele()
        {
            lvwOdaListesi.Items.Clear();
            SqlDataReader odalar = baglanti.SorguVeriOku("SELECT * FROM odalar");
            while (odalar.Read())
            {
                OdaNumaraListe.Add(odalar["odaNumara"].ToString());
                string[] satir = { odalar["odaNumara"].ToString(), odalar["odaKat"].ToString(), odalar["odaKisiSayisi"].ToString(), odalar["odaFiyat"].ToString() };
                var listViewItem = new ListViewItem(satir);

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
                VerileriTemizle();
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

                eskiOdaNumara = odaNumarasi;
            }
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
                    for(int i = 0; i < ckl.Items.Count; i++)
                    {
                        ckl.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }

                if(item is RichTextBox)
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
            if(OdaNumaraListe.Exists(x => x == nudOdaNumara.Value.ToString()))
            {
                MessageBox.Show("Oda Numarasına Sahip Bir Oda Zaten Var!");
            }
            else 
            {
                string odaNumara = nudOdaNumara.Value.ToString();
                string odaKat = nudOdaKat.Value.ToString();
                string odaKisiSayisi = nudOdaKisi.Value.ToString();
                string odaFiyat = nudOdaFiyat.Value.ToString();
                string odaAciklama = rtxOdaAciklama.Text;

                string commandstring = "INSERT INTO Odalar (odaNumara,odaKat,odaKisiSayisi,odaFiyat,odaAciklama,odaDoluMu,odaAktifMi) VALUES (@odaNumara,@odaKat,@odaKisiSayisi,@odaFiyat,@odaAciklama,0,1);";
                SqlCommand sorgu = new SqlCommand(commandstring);
                sorgu.Parameters.AddWithValue("@odaNumara",odaNumara);
                sorgu.Parameters.AddWithValue("@odaKat", odaKat);
                sorgu.Parameters.AddWithValue("@odaKisiSayisi", odaKisiSayisi);
                sorgu.Parameters.AddWithValue("@odaFiyat", odaFiyat);
                sorgu.Parameters.AddWithValue("@odaAciklama", odaAciklama);

                try
                {
                    int id = (int)baglanti.SorguScalar(sorgu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oda Eklenirken Hata Oluştu! Hata Mesajı: " + ex.Message);
                }


                foreach (int itemIndices in cklOzellikler.CheckedIndices)
                {

                    commandstring = $"INSERT INTO odalar_odaOzellik (odaNumara,ozellikID) VALUES({odaNumara},{itemIndices+1})";


                }
            }
        }

        private void btnOdaDegistir_Click(object sender, EventArgs e)
        {  
            int odaNumara = (int) nudOdaNumara.Value;
            int odaKat = (int) nudOdaKat.Value;
            int odaKisiSayisi = (int) nudOdaKisi.Value;
            int odaFiyat = (int) nudOdaFiyat.Value;
            string odaAciklama = rtxOdaAciklama.Text;
            int tekKisilikYatakAdet = (int) nudTekKisilikYatak.Value;
            int ciftKisilikYatakAdet = (int) nudCiftKisilikYatak.Value;

            if (OdaNumaraListe.Exists(x => x == nudOdaNumara.Value.ToString()))
            {
                if(cklOzellikler.CheckedItems.Count == 0)
                {
                    MessageBox.Show("En Az Bir Özellik Seçmelisiniz!");
                    return;
                }

                string commandstring = $"UPDATE odalar SET odaNumara = {odaNumara}, odaKat= {odaKat}, odaKisiSayisi = {odaKisiSayisi}, odaFiyat = {odaFiyat}, odaAciklama = '{odaAciklama}' WHERE odaNumara = {Convert.ToInt32(eskiOdaNumara)}";

                //MessageBox.Show(commandstring);

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

                OdaListele();
                //Yatakları sil
                commandstring = $"DELETE FROM odalar_odaYataklar WHERE odaNumara = {odaNumara};";
                //Yatakları ekle

                baglanti.SorguNonQuery(commandstring);

                commandstring = $"INSERT INTO odalar_odaYataklar (odaNumara,yatakID,yatakAdet) VALUES({odaNumara},1,{tekKisilikYatakAdet}),({odaNumara},2,{ciftKisilikYatakAdet});";

                //commandstring = "UPDATE odalar_odaYataklar SET odaNumara = {odaNumara}, yatakAdet ={tekKisilikYatakAdet} WHERE odaNumara = {eskiOdaNumara};";

                baglanti.SorguNonQuery(commandstring);

                //commandstring = "UPDATE odalar_odaYataklar SET odaNumara = {odaNumara}, yatakAdet ={tekKisilikYatakAdet} WHERE odaNumara = {eskiOdaNumara} AND yatakID = 2;";

                //Oda Özelliklerini Sil
                commandstring = "DELETE FROM odalar_odaOzellikler WHERE odaNumara = {eskiOdaNumara};";

                baglanti.SorguNonQuery(commandstring);

                //Oda Özelliklerini Ekle
                ArrayList ozellikler = new ArrayList();

                foreach(int itemIndices in cklOzellikler.CheckedIndices)
                {
                    ozellikler.Add(itemIndices + 1);
                }

                commandstring = "INSERT INTO odalar_odaOzellikler (odaNumara,ozellikID) VALUES";
                foreach(int ozellik in ozellikler)
                {
                    commandstring += $"({odaNumara},{ozellik}),";
                }
                commandstring += ";";

                baglanti.SorguNonQuery(commandstring);

            }
        }

        private void FrmYonetici_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
