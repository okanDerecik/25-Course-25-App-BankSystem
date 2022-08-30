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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-K0C08G8;Initial Catalog=DbBankaTest;Integrated Security=True");

        public string hesap;
        private void Form2_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = hesap;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLKISILER where HESAPNO=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1", hesap);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
                LblTc.Text = dr[3].ToString();
                LblTelefon.Text = dr[4].ToString();
            }
            baglanti.Close();
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            // Gönderilen Hesabın Para ARtışı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLHESAP set bakıye=bakıye+@p1 where hesapno=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1",decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@p2",MskHesapNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            //Gönderen Hesabın Para Azalışı
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Update tBLHESAP set bakıye=bakıye-@k1 where hesapno=@k2", baglanti);
            komut2.Parameters.AddWithValue("@k1", decimal.Parse(TxtTutar.Text));
            komut2.Parameters.AddWithValue("@k2",hesap);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İşlem Gerçekleşti");

            //Hareket Tablosuna İşlem Ekleme
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("Insert into TBLHAREKET (GONDEREN,ALICI,TUTAR) values (@s1,@s2,@s3)",baglanti);
            komut3.Parameters.AddWithValue("@s1",LblHesapNo.Text);
            komut3.Parameters.AddWithValue("@s2",MskHesapNo.Text);
            komut3.Parameters.AddWithValue("@s3",decimal.Parse(TxtTutar.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 fr = new Form4();
            fr.Show();
        }
    }
}
