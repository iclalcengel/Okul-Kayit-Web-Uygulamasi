//document.addEventListener("DOMContentLoaded", function () {
//    var baglanti = new SqlConnection("Data Source=ICLAL\\SQLEXPRESS;Initial Catalog=okulkayit;Integrated Security=True");

// function ekle() {
//        try {
//            baglanti.open();
//            var sqlcumlesi = "INSERT INTO Ogrenciler (OgrenciNo, OgrenciAd, DogumYeri, DogumTarihi) VALUES (@OgrenciNo, @OgrenciAd, @DogumYeri, @DogumTarihi)";
//            var sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

//            sqlCommand.Parameters.AddWithValue("@OgrenciNo", document.getElementById("ogrenciNo").value);
//            sqlCommand.Parameters.AddWithValue("@OgrenciAd", document.getElementById("ogrenciAd").value);
//            sqlCommand.Parameters.AddWithValue("@DogumYeri", document.getElementById("dogumYeri").value);
//            sqlCommand.Parameters.AddWithValue("@DogumTarihi", document.getElementById("dogumTarihi").value);

//            sqlCommand.ExecuteNonQuery();
//            alert("Öğrenci bilgileri başarıyla kaydedilmiştir!");
//            document.getElementById("ogrenciNo").value = "";
//            document.getElementById("ogrenciAd").value = "";
//            document.getElementById("dogumYeri").value = "";
//        } catch (ex) {
//            alert("Öğrenci eklenirken bir hata oluştu: " + ex.message);
//        } finally {
//            if (baglanti.state !== ConnectionState.closed)
//                baglanti.close();
//        }
//    };

//  function guncelle() {
//        try {
//            baglanti.open();
//            var sqlcumlesi = "UPDATE Ogrenciler SET OgrenciAd = @OgrenciAd, DogumYeri = @DogumYeri, DogumTarihi = @DogumTarihi WHERE OgrenciNo = @OgrenciNo";
//            var sqlCommand = new SqlCommand(sqlcumlesi, baglanti);

//            sqlCommand.Parameters.AddWithValue("@OgrenciAd", document.getElementById("ogrenciAd").value);
//            sqlCommand.Parameters.AddWithValue("@DogumYeri", document.getElementById("dogumYeri").value);
//            sqlCommand.Parameters.AddWithValue("@DogumTarihi", document.getElementById("dogumTarihi").value);
//            sqlCommand.Parameters.AddWithValue("@OgrenciNo", document.getElementById("ogrenciNo").value);

//            var affectedRows = sqlCommand.ExecuteNonQuery();
//            if (affectedRows > 0) {
//                alert("Öğrenci bilgileri başarıyla güncellendi!");
//            } else {
//                alert("Güncellenecek öğrenci bulunamadı!");
//            }
//        } catch (ex) {
//            alert("Öğrenci güncellenirken bir hata oluştu: " + ex.message);
//        } finally {
//            if (baglanti.state !== ConnectionState.closed)
//                baglanti.close();
//        }
//    };

//  function secileniSil() {
//        try {
//            var ogrenciAd = document.getElementById("kayitliOgrenciler").value;

//            baglanti.open();
//            var cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE OgrenciAd = @OgrenciAd", baglanti);
//            cmd.Parameters.AddWithValue("@OgrenciAd", ogrenciAd);

//            var affectedRows = cmd.ExecuteNonQuery();
//            baglanti.close();

//            if (affectedRows > 0) {
//                alert("Seçilen öğrenci başarıyla silindi!");
//                // Yenileme işlemi gerekirse burada comboBox1'in yenilenmesi yapılabilir.
//            } else {
//                alert("Öğrenci silinemedi!");
//            }
//        } catch (ex) {
//            alert("Öğrenci silinirken bir hata oluştu: " + ex.message);
//        } finally {
//            if (baglanti.state !== ConnectionState.closed)
//                baglanti.close();
//        }
//    };
//});
