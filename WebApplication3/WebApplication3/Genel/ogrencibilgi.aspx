<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ogrencibilgi.aspx.cs" Inherits="WebApplication3.Genel.ogrencibilgi" %>

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
            width: 50%;
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
        input[type="text"],
        input[type="date"],
        select {
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
        button + button {
            margin-top: 10px;
        }
        .ogrenci-listesi {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="ogrenciForm" runat="server">
        <div class="container">
            <h2>Öğrenci Bilgileri</h2>
            <label for="OgrenciNo">Öğrenci No:</label>
            <asp:TextBox ID="OgrenciNo" runat="server" CssClass="form-control" required="required"></asp:TextBox>
            <label for="OgrenciAd">Öğrenci Adı:</label>
            <asp:TextBox ID="OgrenciAd" runat="server" CssClass="form-control" required="required"></asp:TextBox>
            <label for="DogumYeri">Doğum Yeri:</label>
            <asp:TextBox ID="DogumYeri" runat="server" CssClass="form-control" required="required"></asp:TextBox>
            <label for="DogumTarihi">Doğum Tarihi:</label>
            <asp:TextBox ID="DogumTarihi" runat="server" CssClass="form-control" required="required" type="date"></asp:TextBox>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="ekle" id="butonEkle">Ekle</button>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="Guncelle">Güncelle</button>
            <div class="ogrenci-listesi">
                <label for="kayitliOgrenciler">Kayıtlı Öğrenciler:</label>
                <select id="kayitliOgrenciler" runat="server" style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px; box-sizing: border-box;"></select>
                <button type="submit" class="btn btn-primary" runat="server" onserverclick="sil" id="butonSil">Seçileni Sil</button>
            </div>
        </div>
    </form>
</body>
</html>
