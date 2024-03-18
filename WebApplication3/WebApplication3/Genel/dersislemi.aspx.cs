using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebApplication3.Genel
{
    public partial class dersislemi : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ders isimlerini DropDownList'e yükle
                YukleDersIsimleri();
            }
        }

        private void YukleDersIsimleri()
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT DersAdi FROM Dersler", baglanti);
                SqlDataReader reader = cmd.ExecuteReader();

                // DropDownList içeriğini temizle
                kayitliDersler.Items.Clear();

                // Veri tabanından alınan ders isimlerini DropDownList'e ekle
                while (reader.Read())
                {
                    string dersAdi = reader["DersAdi"].ToString();
                    kayitliDersler.Items.Add(dersAdi);
                }

                // Veritabanı işlemleri tamamlandıktan sonra bağlantıyı kapatın
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ders isimleri yüklenirken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        protected void sil(object sender, EventArgs e)
        {
            // Seçilen ders adını al
            string dersAdi = kayitliDersler.Value;

            // Seçilen dersi sil
            sil(dersAdi);

            // Ders listesini tekrar yükle
            YukleDersIsimleri();
        }

        private void sil(string dersAdi)
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Dersler WHERE DersAdi = @DersAdi", baglanti);
                cmd.Parameters.AddWithValue("@DersAdi", dersAdi);

                int affectedRows = cmd.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Seçilen ders başarıyla silindi!');", true);
                    // Yenileme işlemi gerekirse burada ders listesinin yenilenmesi yapılabilir.
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ders silinemedi!');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ders silinirken bir hata oluştu: " + ex.Message + "');", true);
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        private void ekle(string dersID, string dersAdi, string ogretmenID)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "INSERT INTO Dersler (DersID, DersAdi, OgretmenID) VALUES (@DersID, @DersAdi, @OgretmenID)";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("@DersID", dersID);
                sqlCommand.Parameters.AddWithValue("@DersAdi", dersAdi);
                sqlCommand.Parameters.AddWithValue("@OgretmenID", ogretmenID);

                sqlCommand.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ders başarıyla eklendi!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ders eklenirken bir hata oluştu: " + ex.Message + "');", true);
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        protected void ekle(object sender, EventArgs e)
        {
            // TextBox'lardan değerleri alarak EkleDers metodunu çağırma
            string dersID = DersID.Text;
            string dersAdi = DersAdi.Text;
            string ogretmenID = OgretmenID.Text;

            ekle(dersID, dersAdi, ogretmenID);

            // Ders listesini tekrar yükle
            YukleDersIsimleri();
        }

        private void guncelle(string dersID, string dersAdi, string ogretmenID)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "UPDATE Dersler SET DersID = @DersID, OgretmenID = @OgretmenID WHERE DersAdi = @DersAdi";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("DersID", dersID);
                sqlCommand.Parameters.AddWithValue("@OgretmenID", ogretmenID);
                sqlCommand.Parameters.AddWithValue("@DersAdi", dersAdi);

                sqlCommand.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ders başarıyla güncellendi!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Ders güncellenirken bir hata oluştu: " + ex.Message + "');", true);
            }
            finally
            {
                if (baglanti.State != ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        protected void guncelle(object sender, EventArgs e)
        {
            // TextBox'lardan değerleri alarak GuncelleDers metodunu çağırma
            string dersID = DersID.Text;
            string dersAdi = DersAdi.Text;
            string ogretmenID = OgretmenID.Text;

            guncelle(dersID, dersAdi, ogretmenID);

            // Ders listesini tekrar yükle
            YukleDersIsimleri();
        }
    }
}
