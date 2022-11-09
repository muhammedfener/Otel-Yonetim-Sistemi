using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel_Yonetim_Sistemi
{
    public partial class FrmResepsiyon : Form
    {
        List<TableLayoutPanel> gorunumler = new List<TableLayoutPanel>();

        public FrmResepsiyon()
        {
            InitializeComponent();
            gorunumler.Add(tpnlOdaDurum);
            gorunumler.Add(tpnlRezervasyon);
            gorunumler.Add(tpnlKullaniciIslemler);
            tableLayoutAc();
        }

        private void tableLayoutAc(TableLayoutPanel tpnlobj = null)
        {
            foreach (TableLayoutPanel gorunum in gorunumler)
            {
                gorunum.Visible = false;
            }
            if(tpnlobj != null)
            {
                tpnlobj.Visible = true;
            }
        }

        private void ms_OtelDurum_Click(object sender, EventArgs e)
        {
            tableLayoutAc(tpnlOdaDurum);
        }

        private void ms_YeniRezervasyon_Click(object sender, EventArgs e)
        {
            tableLayoutAc(tpnlRezervasyon);
        }

        private void ms_KullaniciSifreDegis_Click(object sender, EventArgs e)
        {
            tableLayoutAc(tpnlKullaniciIslemler);
        }

        private void ms_CikisYap_Click(object sender, EventArgs e)
        {

        }

        private void SifreGizleGoster(object sender, EventArgs e)
        {
            TextBox sifrekutulari = (TextBox) sender;
            sifrekutulari.PasswordChar = sifrekutulari.Focused == true ? '\0' : '*';
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
