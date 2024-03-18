using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication3.Genel
{
    public partial class notislemi : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Sayfa ilk yüklendiğinde öğrencileri ve dersleri yükle
                YukleOgrenciler();
                YukleDersler();
            }
        }

        private void YukleOgrenciler()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("SELECT OgrenciNo FROM Ogrenciler", baglanti);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // DropDownList içeriğini temizle
                    ogrenciler.Items.Clear();

                    // Veri tabanından alınan öğrenci bilgilerini DropDownList'e ekle
                    while (reader.Read())
                    {
                        string ogrenciNo = reader["OgrenciNo"].ToString();
                        ogrenciler.Items.Add(new ListItem(ogrenciNo));
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Öğrenciler yüklenirken bir hata oluştu: " + ex.Message + "');", true);
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
                    kayitliDersler.Items.Clear();

                    // Veri tabanından alınan ders bilgilerini DropDownList'e ekle
                    while (reader.Read())
                    {
                        string dersID = reader["DersID"].ToString();
                        kayitliDersler.Items.Add(new ListItem(dersID));
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Dersler yüklenirken bir hata oluştu: " + ex.Message + "');", true);
            }
        }

        protected void notukaydet(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=ICLAL\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand comm = new SqlCommand("Insert into Notlar values(@OgrenciNo, @DersID, @Not)", baglanti);
                    comm.Parameters.AddWithValue("@OgrenciNo", ogrenciler.Value);
                    comm.Parameters.AddWithValue("@DersID", kayitliDersler.Value);
                    comm.Parameters.AddWithValue("@Not", int.Parse(Not.Text));

                    comm.ExecuteNonQuery();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Not başarıyla kaydedilmiştir!');", true);
                YukleOgrenciler();
                YukleDersler();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Not kaydedilirken bir hata oluştu: " + ex.Message + "');", true);
            }
        }
        private void notuguncelle(string ogrenciNo, string dersID, int not)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "UPDATE Notlar SET [Not]=@Not WHERE OgrenciNo=@OgrenciNo AND DersID=@DersID";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("@OgrenciNo", ogrenciNo);
                sqlCommand.Parameters.AddWithValue("@DersID", dersID);
                sqlCommand.Parameters.AddWithValue("@Not", not);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Not bilgileri başarıyla güncellendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State != System.Data.ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        protected void notuguncelle(object sender, EventArgs e)
        {
            // TextBox'lardan değerleri alarak ekle() metodunu çağırma
            string ogrenciNo = ogrenciler.Value;
            string dersID = kayitliDersler.Value;
            int not = int.Parse(Not.Text);

            notuguncelle(ogrenciNo, dersID, not);
        }


        private void notusil(string ogrenciNo, string dersID)
        {
            try
            {
                baglanti.Open();
                string sqlcumlesi = "DELETE FROM Notlar WHERE OgrenciNo=@OgrenciNo AND DersID=@DersID";
                SqlCommand sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

                sqlCommand.Parameters.AddWithValue("@OgrenciNo", ogrenciNo);
                sqlCommand.Parameters.AddWithValue("@DersID", dersID);

                int affectedRows = sqlCommand.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Not başarıyla silindi!");
                }
                else
                {
                    MessageBox.Show("Silinecek not bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not silinirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (baglanti.State != System.Data.ConnectionState.Closed)
                    baglanti.Close();
            }
        }

        protected void notusil(object sender, EventArgs e)
        {
            // TextBox'lardan değerleri alarak NotuSil() metodunu çağırma
            string ogrenciNo = ogrenciler.Value;
            string dersID = kayitliDersler.Value;

            notusil(ogrenciNo, dersID);
        }


    }
}
