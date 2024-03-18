using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WebApplication3.Models;

namespace WebApplication3.Genel
{
    public partial class ogretimuye : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True");


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Öğretmen isimlerini DropDownList'e yükle
                YukleOgretmenIsimleri();
            }
        }

        private void YukleOgretmenIsimleri()
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT OgretmenAdi FROM Ogretmenler", baglanti);
                SqlDataReader reader = cmd.ExecuteReader();

                // DropDownList içeriğini temizle
                kayitliOgretmenler.Items.Clear();

                // Veri tabanından alınan öğretmen isimlerini DropDownList'e ekle
                while (reader.Read())
                {
                    string ogretmenAdi = reader["OgretmenAdi"].ToString();
                    kayitliOgretmenler.Items.Add(ogretmenAdi);
                }

                // Veritabanı işlemleri tamamlandıktan sonra bağlantıyı kapatın
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğretmen isimleri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        protected void sil(object sender, EventArgs e)

        {
            // Seçilen öğretmen adını al
            string ogretmenAdi = kayitliOgretmenler.Value; 

            // Seçilen öğretmen sil
            sil(ogretmenAdi);

            // Öğretmen listesini tekrar yükle
            YukleOgretmenIsimleri();
        }



        private void sil(string ogretmenAdi)
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Ogretmenler WHERE OgretmenAdi = @OgretmenAdi", baglanti);
                cmd.Parameters.AddWithValue("@OgretmenAdi", ogretmenAdi);

                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Seçilen öğretmen başarıyla silindi!');", true);
                    // Yenileme işlemi gerekirse burada öğrenci listesinin yenilenmesi yapılabilir.
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Öğretmen silinemedi!');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Öğretmen silinirken bir hata oluştu: " + ex.Message + "');", true);
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        private void ekle(string ogretmenID, string ogretmenAdi)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "INSERT INTO Ogretmenler (OgretmenID, OgretmenAdi) VALUES (@OgretmenID, @OgretmenAdi)";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("@OgretmenID", ogretmenID);
                sqlCommand.Parameters.AddWithValue("@OgretmenAdi", ogretmenAdi);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Öğretmen bilgileri başarıyla kaydedilmiştir!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğretmen eklenirken bir hata oluştu: " + ex.Message);
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
            string ogretmenID = OgretmenID.Text;
            string ogretmenAdi = OgretmenAdi.Text;

            ekle(ogretmenID, ogretmenAdi);
        }

        private void guncelle(string ogretmenID, string ogretmenAdi)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "UPDATE Ogretmenler SET OgretmenAdi=@OgretmenAdi WHERE OgretmenID=@OgretmenID";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("@OgretmenID", ogretmenID);
                sqlCommand.Parameters.AddWithValue("@OgretmenAdi", ogretmenAdi);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Öğretmen bilgileri başarıyla kaydedilmiştir!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğretmen eklenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State != System.Data.ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        protected void guncelle(object sender, EventArgs e)
        {
            // TextBox'lardan değerleri alarak ekle() metodunu çağırma
            string ogretmenID = OgretmenID.Text;
            string ogretmenAdi = OgretmenAdi.Text;

            guncelle(ogretmenID, ogretmenAdi);
        }


    }
}
