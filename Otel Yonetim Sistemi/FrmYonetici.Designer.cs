namespace Otel_Yonetim_Sistemi
{
    partial class FrmYonetici
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.odaDuzenleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.calisanEkleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.kullaniciEkleDuzenleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CalisanDuzenleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlOdaEkle = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.nudOdaNumara = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudOdaKat = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudOdaKisi = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudOdaFiyat = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudTekKisilikYatak = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudCiftKisilikYatak = new System.Windows.Forms.NumericUpDown();
            this.cklOzellikler = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOdaEkle = new System.Windows.Forms.Button();
            this.lvwOdaListesi = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOdaDuzenle = new System.Windows.Forms.Button();
            this.btnOdaDegistir = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.rtxOdaAciklama = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.pnlOdaEkle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaNumara)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaKat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaKisi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaFiyat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTekKisilikYatak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCiftKisilikYatak)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.odaDuzenleMenu,
            this.calisanEkleMenu,
            this.CalisanDuzenleMenu,
            this.kullaniciEkleDuzenleMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // odaDuzenleMenu
            // 
            this.odaDuzenleMenu.Name = "odaDuzenleMenu";
            this.odaDuzenleMenu.Size = new System.Drawing.Size(117, 20);
            this.odaDuzenleMenu.Text = "Oda Ekle&&Duzenle";
            // 
            // calisanEkleMenu
            // 
            this.calisanEkleMenu.Name = "calisanEkleMenu";
            this.calisanEkleMenu.Size = new System.Drawing.Size(81, 20);
            this.calisanEkleMenu.Text = "Çalışan Ekle";
            // 
            // kullaniciEkleDuzenleMenu
            // 
            this.kullaniciEkleDuzenleMenu.Name = "kullaniciEkleDuzenleMenu";
            this.kullaniciEkleDuzenleMenu.Size = new System.Drawing.Size(140, 20);
            this.kullaniciEkleDuzenleMenu.Text = "Kullanıcı Ekle&&Düzenle";
            // 
            // CalisanDuzenleMenu
            // 
            this.CalisanDuzenleMenu.Name = "CalisanDuzenleMenu";
            this.CalisanDuzenleMenu.Size = new System.Drawing.Size(102, 20);
            this.CalisanDuzenleMenu.Text = "Çalışan Düzenle";
            // 
            // pnlOdaEkle
            // 
            this.pnlOdaEkle.Controls.Add(this.rtxOdaAciklama);
            this.pnlOdaEkle.Controls.Add(this.btnOdaDuzenle);
            this.pnlOdaEkle.Controls.Add(this.lvwOdaListesi);
            this.pnlOdaEkle.Controls.Add(this.btnOdaDegistir);
            this.pnlOdaEkle.Controls.Add(this.btnOdaEkle);
            this.pnlOdaEkle.Controls.Add(this.cklOzellikler);
            this.pnlOdaEkle.Controls.Add(this.nudCiftKisilikYatak);
            this.pnlOdaEkle.Controls.Add(this.nudTekKisilikYatak);
            this.pnlOdaEkle.Controls.Add(this.nudOdaFiyat);
            this.pnlOdaEkle.Controls.Add(this.label8);
            this.pnlOdaEkle.Controls.Add(this.label7);
            this.pnlOdaEkle.Controls.Add(this.label6);
            this.pnlOdaEkle.Controls.Add(this.nudOdaKisi);
            this.pnlOdaEkle.Controls.Add(this.label5);
            this.pnlOdaEkle.Controls.Add(this.nudOdaKat);
            this.pnlOdaEkle.Controls.Add(this.label4);
            this.pnlOdaEkle.Controls.Add(this.label3);
            this.pnlOdaEkle.Controls.Add(this.label2);
            this.pnlOdaEkle.Controls.Add(this.nudOdaNumara);
            this.pnlOdaEkle.Controls.Add(this.label1);
            this.pnlOdaEkle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOdaEkle.Location = new System.Drawing.Point(0, 24);
            this.pnlOdaEkle.Name = "pnlOdaEkle";
            this.pnlOdaEkle.Size = new System.Drawing.Size(800, 426);
            this.pnlOdaEkle.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oda Numara: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudOdaNumara
            // 
            this.nudOdaNumara.Location = new System.Drawing.Point(162, 29);
            this.nudOdaNumara.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudOdaNumara.Name = "nudOdaNumara";
            this.nudOdaNumara.Size = new System.Drawing.Size(120, 20);
            this.nudOdaNumara.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Oda Kat: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudOdaKat
            // 
            this.nudOdaKat.Location = new System.Drawing.Point(162, 55);
            this.nudOdaKat.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudOdaKat.Name = "nudOdaKat";
            this.nudOdaKat.Size = new System.Drawing.Size(120, 20);
            this.nudOdaKat.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Oda Kişi Sayısı: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudOdaKisi
            // 
            this.nudOdaKisi.Location = new System.Drawing.Point(162, 81);
            this.nudOdaKisi.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudOdaKisi.Name = "nudOdaKisi";
            this.nudOdaKisi.Size = new System.Drawing.Size(120, 20);
            this.nudOdaKisi.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Oda Fiyat: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudOdaFiyat
            // 
            this.nudOdaFiyat.DecimalPlaces = 2;
            this.nudOdaFiyat.Location = new System.Drawing.Point(162, 107);
            this.nudOdaFiyat.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudOdaFiyat.Name = "nudOdaFiyat";
            this.nudOdaFiyat.Size = new System.Drawing.Size(120, 20);
            this.nudOdaFiyat.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Tek Kişilik Yatak Sayısı: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudTekKisilikYatak
            // 
            this.nudTekKisilikYatak.Location = new System.Drawing.Point(162, 133);
            this.nudTekKisilikYatak.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTekKisilikYatak.Name = "nudTekKisilikYatak";
            this.nudTekKisilikYatak.Size = new System.Drawing.Size(120, 20);
            this.nudTekKisilikYatak.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Çift Kişilik Yatak Sayısı: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudCiftKisilikYatak
            // 
            this.nudCiftKisilikYatak.Location = new System.Drawing.Point(162, 159);
            this.nudCiftKisilikYatak.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCiftKisilikYatak.Name = "nudCiftKisilikYatak";
            this.nudCiftKisilikYatak.Size = new System.Drawing.Size(120, 20);
            this.nudCiftKisilikYatak.TabIndex = 3;
            // 
            // cklOzellikler
            // 
            this.cklOzellikler.CheckOnClick = true;
            this.cklOzellikler.FormattingEnabled = true;
            this.cklOzellikler.Items.AddRange(new object[] {
            "Saç Kurutma Makinesi",
            "Minibar",
            "TV",
            "Kablosuz İnternet",
            "Klima",
            "Balkon"});
            this.cklOzellikler.Location = new System.Drawing.Point(128, 185);
            this.cklOzellikler.Name = "cklOzellikler";
            this.cklOzellikler.Size = new System.Drawing.Size(154, 94);
            this.cklOzellikler.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Oda Özellikleri: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOdaEkle
            // 
            this.btnOdaEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOdaEkle.Location = new System.Drawing.Point(188, 384);
            this.btnOdaEkle.Name = "btnOdaEkle";
            this.btnOdaEkle.Size = new System.Drawing.Size(94, 30);
            this.btnOdaEkle.TabIndex = 5;
            this.btnOdaEkle.Text = "Oda Ekle";
            this.btnOdaEkle.UseVisualStyleBackColor = true;
            this.btnOdaEkle.Click += new System.EventHandler(this.btnOdaEkle_Click);
            // 
            // lvwOdaListesi
            // 
            this.lvwOdaListesi.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvwOdaListesi.FullRowSelect = true;
            this.lvwOdaListesi.HideSelection = false;
            this.lvwOdaListesi.Location = new System.Drawing.Point(458, 29);
            this.lvwOdaListesi.Name = "lvwOdaListesi";
            this.lvwOdaListesi.Size = new System.Drawing.Size(330, 230);
            this.lvwOdaListesi.TabIndex = 6;
            this.lvwOdaListesi.UseCompatibleStateImageBehavior = false;
            this.lvwOdaListesi.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Oda Numarası";
            this.columnHeader1.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Oda Katı";
            this.columnHeader2.Width = 72;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Oda Kişi Sayısı";
            this.columnHeader3.Width = 93;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Oda Fiyat";
            this.columnHeader4.Width = 75;
            // 
            // btnOdaDuzenle
            // 
            this.btnOdaDuzenle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOdaDuzenle.Location = new System.Drawing.Point(691, 265);
            this.btnOdaDuzenle.Name = "btnOdaDuzenle";
            this.btnOdaDuzenle.Size = new System.Drawing.Size(97, 31);
            this.btnOdaDuzenle.TabIndex = 7;
            this.btnOdaDuzenle.Text = "Oda Düzenle";
            this.btnOdaDuzenle.UseVisualStyleBackColor = true;
            this.btnOdaDuzenle.Click += new System.EventHandler(this.btnOdaDuzenle_Click);
            // 
            // btnOdaDegistir
            // 
            this.btnOdaDegistir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOdaDegistir.Location = new System.Drawing.Point(63, 384);
            this.btnOdaDegistir.Name = "btnOdaDegistir";
            this.btnOdaDegistir.Size = new System.Drawing.Size(119, 30);
            this.btnOdaDegistir.TabIndex = 5;
            this.btnOdaDegistir.Text = "Odayı Değiştir";
            this.btnOdaDegistir.UseVisualStyleBackColor = true;
            this.btnOdaDegistir.Click += new System.EventHandler(this.btnOdaDegistir_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 288);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Oda Özellikleri: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtxOdaAciklama
            // 
            this.rtxOdaAciklama.Location = new System.Drawing.Point(128, 285);
            this.rtxOdaAciklama.Name = "rtxOdaAciklama";
            this.rtxOdaAciklama.Size = new System.Drawing.Size(154, 93);
            this.rtxOdaAciklama.TabIndex = 8;
            this.rtxOdaAciklama.Text = "";
            // 
            // FrmYonetici
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlOdaEkle);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmYonetici";
            this.Text = "FrmYonetici";
            this.Load += new System.EventHandler(this.FrmYonetici_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlOdaEkle.ResumeLayout(false);
            this.pnlOdaEkle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaNumara)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaKat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaKisi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOdaFiyat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTekKisilikYatak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCiftKisilikYatak)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem odaDuzenleMenu;
        private System.Windows.Forms.ToolStripMenuItem calisanEkleMenu;
        private System.Windows.Forms.ToolStripMenuItem CalisanDuzenleMenu;
        private System.Windows.Forms.ToolStripMenuItem kullaniciEkleDuzenleMenu;
        private System.Windows.Forms.Panel pnlOdaEkle;
        private System.Windows.Forms.Button btnOdaDuzenle;
        private System.Windows.Forms.ListView lvwOdaListesi;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnOdaEkle;
        private System.Windows.Forms.CheckedListBox cklOzellikler;
        private System.Windows.Forms.NumericUpDown nudCiftKisilikYatak;
        private System.Windows.Forms.NumericUpDown nudTekKisilikYatak;
        private System.Windows.Forms.NumericUpDown nudOdaFiyat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudOdaKisi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudOdaKat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudOdaNumara;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOdaDegistir;
        private System.Windows.Forms.RichTextBox rtxOdaAciklama;
        private System.Windows.Forms.Label label8;
    }
}