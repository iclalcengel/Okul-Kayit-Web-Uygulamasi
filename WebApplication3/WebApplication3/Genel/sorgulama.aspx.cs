using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WebApplication3.Genel
{
    public partial class sorgulama : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Öğrenci isimlerini DropDownList'e yükle
                YukleDersler();
                YukleDogumYeri();
                YukleOgretimUyesi();
            }
        }

        private void YukleDersler()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("SELECT DersID FROM Dersler", baglanti);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // DropDownList içeriğini temizle
                    ders.Items.Clear();

                    // Veri tabanından alınan ders bilgilerini DropDownList'e ekle
                    while (reader.Read())
                    {
                        string dersID = reader["DersID"].ToString();
                        ders.Items.Add(new ListItem(dersID));
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Dersler yüklenirken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        private void YukleDogumYeri()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT DogumYeri FROM Ogrenciler", baglanti);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // DropDownList içeriğini temizle
                    sehir.Items.Clear();

                    // Veri tabanından alınan dogum yeri bilgilerini DropDownList'e ekle
                    while (reader.Read())
                    {
                        string dogumYeri = reader["DogumYeri"].ToString();
                        sehir.Items.Add(new ListItem(dogumYeri));
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Doğum yerleri yüklenirken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        private void YukleOgretimUyesi()
        {
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("SELECT OgretmenAdi FROM Ogretmenler", baglanti);
                SqlDataReader reader = cmd.ExecuteReader();

                // DropDownList içeriğini temizle
                ogretimUyesi.Items.Clear();

                // Veri tabanından alınan öğretmen isimlerini DropDownList'e ekle
                while (reader.Read())
                {
                    string ogretmenAdi = reader["OgretmenAdi"].ToString();
                    ogretimUyesi.Items.Add(ogretmenAdi);
                }

                // Veritabanı işlemleri tamamlandıktan sonra bağlantıyı kapatın
                reader.Close();
                baglanti.Close();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Öğretmen isimleri yüklenirken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        protected void DogumYeriAyniOlanOgrenciler(object sender, EventArgs e)
        {
            try
            {
                string selectedDogumYeri = sehir.Value;
                string query = "SELECT * FROM Ogrenciler WHERE DogumYeri = @DogumYeri";

                using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DogumYeri", selectedDogumYeri);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    resultGrid.DataSource = dataTable;
                    resultGrid.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorgu çalıştırılırken bir hata oluştu: " + ex.Message + "');", true);
            }
        }


        protected void DogumTarihiAyniOlanOgrenciler(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDogumTarihi = DateTime.Parse(dogumTarihi.Value);
                string query = "SELECT * FROM Ogrenciler WHERE DogumTarihi = @DogumTarihi";

                using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DogumTarihi", selectedDogumTarihi);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    resultGrid.DataSource = dataTable;
                    resultGrid.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorgu çalıştırılırken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        protected void SehirdeDogmusVeNotuYuksekOgrenciler(object sender, EventArgs e)
        {
            try
            {
                string selectedSehir = sehir.Value;
                int selectedNotDegeri = int.Parse(notDegeri.Value);
                string query = "SELECT * FROM Ogrenciler O INNER JOIN Notlar N ON O.OgrenciNo = N.OgrenciNo WHERE O.DogumYeri = @DogumYeri AND N.[Not] > @Not";

                using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DogumYeri", selectedSehir);
                    command.Parameters.AddWithValue("@Not", selectedNotDegeri);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    resultGrid.DataSource = dataTable;
                    resultGrid.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorgu çalıştırılırken bir hata oluştu: " + ex.Message + "');", true);
            }
        }


        // ********************************************
        protected void Notu50UstundeAlanOgrenciler(object sender, EventArgs e)
        {
            try
            {
                int dersID = Convert.ToInt32(ders.Value);
                string query = "SELECT * FROM Notlar WHERE DersID = @DersID AND [Not] > 50";

                using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DersID", dersID);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    resultGrid.DataSource = dataTable;
                    resultGrid.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorgu çalıştırılırken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        protected void BasariliOgrenciSayisi(object sender, EventArgs e)
        {
            try
            {
                int secilenDersID = Convert.ToInt32(ders.Value);
                string selectedOgretimUyesi = ogretimUyesi.Value;

                // Bağlantıyı aç
                using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
                {
                    connection.Open();

                    // Seçilen ders ID'sine göre 50'nin altında not alan öğrencilerin sayısını bulma sorgusu
                    string gecenOgrenciSorgu = "SELECT COUNT(*) FROM Notlar WHERE DersID = @DersID AND [Not] >= 50";
                    SqlCommand gecenOgrenciCommand = new SqlCommand(gecenOgrenciSorgu, connection);
                    gecenOgrenciCommand.Parameters.AddWithValue("@DersID", secilenDersID);

                    int gecenOgrenciSayisi = (int)gecenOgrenciCommand.ExecuteScalar();

                    // Seçilen ders ID'sine göre 50'nin üstünde not alan öğrencilerin sayısını bulma sorgusu
                    string kalanOgrenciSorgu = "SELECT COUNT(*) FROM Notlar WHERE DersID = @DersID AND [Not] < 50";
                    SqlCommand kalanOgrenciCommand = new SqlCommand(kalanOgrenciSorgu, connection);
                    kalanOgrenciCommand.Parameters.AddWithValue("@DersID", secilenDersID);

                    int kalanOgrenciSayisi = (int)kalanOgrenciCommand.ExecuteScalar();

                    // Mesaj kutusu olarak geçen ve kalan öğrenci sayılarını göster
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Geçen öğrenci sayısı: " + gecenOgrenciSayisi.ToString() + "\\nKalan öğrenci sayısı: " + kalanOgrenciSayisi.ToString() + "');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorgu çalıştırılırken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        public class Ogrenci
        {
            public int OgrenciNo { get; set; }
            public double OrtalamaNot { get; set; }
            public int AldıgıDersSayisi { get; set; }
        }

        protected List<Ogrenci> OgrenciBasariSiralama()
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();

            try
            {
                using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
                {
                    connection.Open();
                    string sorgu = "SELECT OgrenciNo, AVG([Not]) AS OrtalamaNot, COUNT(*) AS TekrarSayisi FROM Notlar GROUP BY OgrenciNo";
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    SqlDataReader reader = komut.ExecuteReader();

                    while (reader.Read())
                    {
                        Ogrenci ogrenci = new Ogrenci();
                        ogrenci.OgrenciNo = Convert.ToInt32(reader["OgrenciNo"]);
                        ogrenci.OrtalamaNot = Convert.ToDouble(reader["OrtalamaNot"]);
                        ogrenci.AldıgıDersSayisi = Convert.ToInt32(reader["TekrarSayisi"]);
                        ogrenciler.Add(ogrenci);
                    }

                    reader.Close();
                }

                // Ortalama notlara göre öğrencileri sırala
                ogrenciler = ogrenciler.OrderByDescending(x => x.OrtalamaNot).ToList();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sorgu çalıştırılırken bir hata oluştu: " + ex.Message + "');", true);
            }

            return ogrenciler;
        }

        protected void BasariSiralamasi(object sender, EventArgs e)
        {
            List<Ogrenci> siralanmisOgrenciler = OgrenciBasariSiralama();

            // GridView kontrolünü bul
            resultGrid.DataSource = siralanmisOgrenciler;
            resultGrid.DataBind();
        }


    }
}
