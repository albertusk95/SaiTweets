<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formanalisis.aspx.cs" Inherits="TestASPNETv0._0.formanalisis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form analisis</title>
    <link href="newCSS/CSSmaster.css" rel="stylesheet"/>
</head>
<body>
     <ul>
        <li><a href="Dashboard.aspx">Dashboard</a></li>
        <li><a href="formstart.aspx">Home</a></li>
        <li><a href="formperihal.aspx">Perihal</a></li>
        <li style="float:right"><a class="active" href="formabout.aspx">About</a></li>
    </ul>

    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
        <div  style="margin-left: auto; margin-right: auto; text-align: center">
            <div>
                 <div>
                    <h2><b>Jenis algoritma:</b>
                        <asp:Label id="LabelJenisAlgoAnalisis" Text="" runat="server"/>
                    </h2>
                 </div>
            </div>

            <h1><b>Hasil analisis</b></h1><br />
            <h2><b>Sifat: umum</b></h2><br /> 

             <asp:Table ID="DispositionTable" CssClass="MainDISPTable" runat="server" Width="100%" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid"> 
                <asp:TableRow ID="TupleDispositionTable">
                    <asp:TableCell ID="kolomNamaDinasDispositionTable">
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             Nama dinas
                        </div>
                    </asp:TableCell>
                    <asp:TableCell ID="kolomBanyakTweetDispositionTable">
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             Banyak tweet terdisposisi
                        </div>
                    </asp:TableCell>
                    <asp:TableCell ID="kolomInfoDetailDispositionTable">
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             Info detail
                        </div>    
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>  

        </div>
    </div>
    </form>
</body>
</html>
