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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-K0C08G8;Initial Catalog=DbBankaTest;Integrated Security=True");

        private void Form4_Load(object sender, EventArgs e)
        {
            // Hesaba Giriş

            SqlDataAdapter da = new SqlDataAdapter("Select (AD + ' '+ SOYAD) as 'GONDEREN',TUTAR from TBLHAREKET inner join TBLKISILER on TBLHAREKET.GONDEREN = TBLKISILER.HESAPNO", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            // Hesaptan ÇIKIŞ
            SqlDataAdapter da2 = new SqlDataAdapter("Select (AD + ' '+ SOYAD) as 'ALICI',TUTAR from TBLHAREKET inner join TBLKISILER on TBLHAREKET.ALICI = TBLKISILER.HESAPNO", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
    }
}
