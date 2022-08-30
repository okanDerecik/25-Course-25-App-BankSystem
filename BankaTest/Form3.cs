using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BankaTest
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-K0C08G8;Initial Catalog=DbBankaTest;Integrated Security=True");

        private void BtnKaydet_Click(object sender, EventArgs e)
        {

            //Kişiler Tablosuna Ekleme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKISILER(AD,SOYAD,TC,TELEFON,HESAPNO,SIFRE) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)",baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3",MskTc.Text);
            komut.Parameters.AddWithValue("@P4", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P5", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@P6", TxtSifre.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Bilgileri Sisteme Kaydedildi.");

            // Hesap Tablosuna Ekleme
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Insert into TBLHESAP (HESAPNO,BAKIYE) values (@s1,@s2)", baglanti);
            cmd.Parameters.AddWithValue("@s1", MskHesapNo.Text);
            cmd.Parameters.AddWithValue("@s2", TxtBakiye.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hesabınıza Para Aktarımı Yapıldı");
        }

        private void BtnHesapNo_Click(object sender, EventArgs e)
        {
            int arastir;
            Random rastgele = new Random();
            int sayi = rastgele.Next(100000, 1000000);
            MskHesapNo.Text = sayi.ToString();

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * From TBLHESAP", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                arastir = Convert.ToInt32(dr[0]);
                if(arastir == sayi)
                {
                    MskHesapNo.Text = sayi.ToString();
                    MessageBox.Show("Hesap Numarası Daha Önce Atanmış");
                }
            }
            baglanti.Close();
        }
    }
}
