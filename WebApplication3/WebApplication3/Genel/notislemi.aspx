<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notislemi.aspx.cs" Inherits="WebApplication3.Genel.notislemi" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Not İşlemleri</title>
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
        input[type="text"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
        }
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
    </style>
</head>
<body>
  <form id="notForm" runat="server">
     <div class="container">
        <h2>Not İşlemleri</h2>
        <!-- Kayıtlı öğrencilerin seçim alanı -->
        <label for="kayitliOgrenciler">Kayıtlı Öğrenciler:</label>
        <select id="ogrenciler" runat="server"></select>

        <!-- Kayıtlı dersler listesi -->
        <label for="kayitliDersler">Kayıtlı Dersler:</label>
        <select id="kayitliDersler" runat="server" style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px; box-sizing: border-box;"></select>

        <!-- Not giriş alanı -->
        <label for="Not">Not:</label>
        <asp:TextBox ID="Not" runat="server" CssClass="form-control" required="required"></asp:TextBox>

        <!-- Not kaydet butonu -->
        <button type="submit" class="btn btn-primary" runat="server" onserverclick="notukaydet" id="notKaydet">Not Kaydet</button>

        <!-- Not Güncelle -->
        <button type="submit" class="btn btn-primary" runat="server" onserverclick="notuguncelle" id="notGuncelle">Notu Güncelle</button>

        <!-- Not sil butonu -->
        <button type="submit" class="btn btn-primary" runat="server" onserverclick="notusil" id="notSil">Notu Sil</button>

        
    </div>
</form>
</body>
</html>
