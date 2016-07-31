<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formdisposisiSOSIAL.aspx.cs" Inherits="TestASPNETv0._0.formdisposisiSOSIAL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="margin-left: auto; margin-right: auto; text-align: center">
            <div>
                 <div>
                    <h1><b>Kategori SOSIAL</b></h1>
                        <asp:Label id="LabelKeteranganSOSIAL" Text="Jumlah tweet: " runat="server"/>
                        <asp:Label id="LabelJumlahTweetSOSIAL" Text="" runat="server"/>
                 </div>
            </div>

             <asp:Table ID="DispositionSOSIALTable" runat="server" Width="100%" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid"> 
                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             ID
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             Tweet
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             Keywords
                        </div>    
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>  
        </div>
    </div>
    </form>
</body>
</html>
