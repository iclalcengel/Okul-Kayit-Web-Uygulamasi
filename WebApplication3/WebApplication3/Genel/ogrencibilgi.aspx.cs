using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Windows.Forms;

namespace WebApplication3.Genel
{
    public partial class ogrencibilgi : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Öğrenci isimlerini DropDownList'e yükle
                YukleOgrenciIsimleri();
            }
        }

        private void YukleOgrenciIsimleri()
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT OgrenciAd FROM Ogrenciler", baglanti);
                SqlDataReader reader = cmd.ExecuteReader();

                // DropDownList içeriğini temizle
                kayitliOgrenciler.Items.Clear();

                // Veri tabanından alınan öğrenci isimlerini DropDownList'e ekle
                while (reader.Read())
                {
                    string ogrenciAd = reader["OgrenciAd"].ToString();
                    kayitliOgrenciler.Items.Add(ogrenciAd);
                }

                // Veritabanı işlemleri tamamlandıktan sonra bağlantıyı kapatın
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci isimleri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        protected void sil(object sender, EventArgs e)
            
        {
           // Seçilen öğrenci adını al
           string ogrenciAd = kayitliOgrenciler.Value; // veya kayitliOgrenciler.SelectedValue kullanabilirsiniz

           // Seçilen öğrenciyi sil
           sil(ogrenciAd);

           // Öğrenci listesini tekrar yükle
           YukleOgrenciIsimleri();
        }

        

        private void sil(string ogrenciAd)
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE OgrenciAd = @OgrenciAd", baglanti);
                cmd.Parameters.AddWithValue("@OgrenciAd", ogrenciAd);

                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Seçilen öğrenci başarıyla silindi!');", true);
                    // Yenileme işlemi gerekirse burada öğrenci listesinin yenilenmesi yapılabilir.
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Öğrenci silinemedi!');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Öğrenci silinirken bir hata oluştu: " + ex.Message + "');", true);
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        private void ekle(string ogrenciNo, string ogrenciAd, string dogumYeri, DateTime dogumTarihi)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "INSERT INTO Ogrenciler (OgrenciNo, OgrenciAd, DogumYeri, DogumTarihi) VALUES (@OgrenciNo, @OgrenciAd, @DogumYeri, @DogumTarihi)";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("@OgrenciNo", ogrenciNo);
                sqlCommand.Parameters.AddWithValue("@OgrenciAd", ogrenciAd);
                sqlCommand.Parameters.AddWithValue("@DogumYeri", dogumYeri);
                sqlCommand.Parameters.AddWithValue("@DogumTarihi", dogumTarihi);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Öğrenci bilgileri başarıyla kaydedilmiştir!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci eklenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State != System.Data.ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        protected void ekle(object sender, EventArgs e)
        {
            // TextBox'lardan değerleri alarak ekle() metodunu çağırma
            string ogrenciNo = OgrenciNo.Text;
            string ogrenciAd = OgrenciAd.Text;
            string dogumYeri = DogumYeri.Text;
            DateTime dogumTarihi = DateTime.Parse(DogumTarihi.Text); // DateTime tipine dönüştürme işlemi

            ekle(ogrenciNo, ogrenciAd, dogumYeri, dogumTarihi);
        }

        // Güncelleme işlemi için
        private void guncelle(string ogrenciNo, string ogrenciAd, string dogumYeri, DateTime dogumTarihi)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "UPDATE Ogrenciler SET OgrenciAd=@OgrenciAd, DogumYeri=@DogumYeri, DogumTarihi=@DogumTarihi WHERE OgrenciNo=@OgrenciNo";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("@OgrenciNo", ogrenciNo);
                sqlCommand.Parameters.AddWithValue("@OgrenciAd", ogrenciAd);
                sqlCommand.Parameters.AddWithValue("@DogumYeri", dogumYeri);
                sqlCommand.Parameters.AddWithValue("@DogumTarihi", dogumTarihi);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State != System.Data.ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        protected void Guncelle(object sender, EventArgs e)
        {
            // TextBox'lardan değerleri alarak ekle() metodunu çağırma
            string ogrenciNo = OgrenciNo.Text;
            string ogrenciAd = OgrenciAd.Text;
            string dogumYeri = DogumYeri.Text;
            DateTime dogumTarihi = DateTime.Parse(DogumTarihi.Text); // DateTime tipine dönüştürme işlemi

            guncelle(ogrenciNo, ogrenciAd, dogumYeri, dogumTarihi);
        }

        // Diğer yöntemler buraya eklenebilir...
    }
}
