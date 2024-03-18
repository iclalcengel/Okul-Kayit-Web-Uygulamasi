<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sorgulama.aspx.cs" Inherits="WebApplication3.Genel.sorgulama" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Öğrenci Bilgileri</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        .container {
            width: 80%;
            margin: auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h2 {
            text-align: center;
        }
        label {
            display: block;
            margin-bottom: 5px;
        }
        input[type="text"], select, input[type="date"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
        }
        button {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            background-color: #4caf50;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }
        button:hover {
            background-color: #45a049;
        }
        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .gridview th, .gridview td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        .gridview th {
            background-color: #4CAF50;
            color: white;
        }
        .gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="notForm" runat="server">
        <div class="container">
            <h2>Sorgulama İşlemleri</h2>
            
            <!-- Öğrenci bilgileri giriş alanları -->
            <label for="dogumYeri">Doğum Yeri:</label>
            <select id="sehir" runat="server"></select>

            <label for="dogumTarihi">Doğum Tarihi:</label>
            <input type="date" id="dogumTarihi" runat="server"/>

            <label for="notDegeri">Not Değeri:</label>
            <input type="text" id="notDegeri" runat="server"/>

            <label for="ders">Ders:</label>
            <select id="ders" runat="server"></select>

            <label for="ogretimUyesi">Öğretim Üyesi:</label>
            <select id="ogretimUyesi" runat="server"></select>

            <!-- Butonlar -->
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="DogumYeriAyniOlanOgrenciler">Doğum Yeri Aynı Olan Öğrenciler</button>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="DogumTarihiAyniOlanOgrenciler">Doğum Tarihi Aynı Olan Öğrenciler</button>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="SehirdeDogmusVeNotuYuksekOgrenciler">Seçilen Şehirde Doğmuş ve Notu Girilen Değerden Yüksek Öğrenciler</button>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="Notu50UstundeAlanOgrenciler">Notu 50'nin Üstünde Olan Öğrenciler</button>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="BasariliOgrenciSayisi">Seçilen Öğretim Üyesinin Seçilen Dersinden Geçen-Kalan Öğrenci Sayısı</button>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="BasariSiralamasi">Başarı Sıralaması</button>
           
            <!-- Sonuçları göstermek için GridView -->
            <asp:GridView ID="resultGrid" runat="server" AutoGenerateColumns="True" CssClass="gridview"></asp:GridView>
        </div>
    </form>
</body>
</html>
