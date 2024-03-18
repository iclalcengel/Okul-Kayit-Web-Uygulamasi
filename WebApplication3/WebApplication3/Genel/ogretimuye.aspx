<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ogretimuye.aspx.cs" Inherits="WebApplication3.Genel.ogretimuye" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Öğretmen İşlemleri</title>
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
  <form id="ogretmenForm" runat="server">
     <div class="container">
        <h2>Öğretmen İşlemleri</h2>
        <label for="OgretmenID">Öğretmen ID:</label>
        <asp:TextBox ID="OgretmenID" runat="server" CssClass="form-control" required="required"></asp:TextBox>
        <label for="OgretmenAdi">Öğretmen Adı:</label>
        <asp:TextBox ID="OgretmenAdi" runat="server" CssClass="form-control" required="required"></asp:TextBox>

        <!-- Öğretmen ekle butonu -->
        <button type="submit" class="btn btn-primary" runat="server" onserverclick="ekle" id="ogretmenEkle">Öğretmen Ekle</button>

        <!-- Öğretmen güncelle butonu -->
        <button type="submit" class="btn btn-primary" runat="server" onserverclick="guncelle" id="ogretmenGuncelle">Öğretmen Güncelle</button>

      

        <!-- Kayıtlı öğretmenler listesi -->
        <label for="kayitliOgretmenler">Kayıtlı Öğretmenler:</label>
        <select id="kayitliOgretmenler" runat="server" style="width: 100%; padding: 10px; border: 1px solid #ccc; border-radius: 5px; box-sizing: border-box;"></select>
        <!-- Öğretmen sil butonu -->
       
        <button type="submit" class="btn btn-primary" runat="server" onserverclick="sil" id="ogretmenSil">Öğretmen Sil</button>
        
    </div>
</form>
</body>
</html>
