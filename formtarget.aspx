<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formtarget.aspx.cs" Inherits="TestASPNETv0._0.formtarget" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form ekstraksi tweet</title>
</head>
<body>
  
        <form id="form2" runat="server">

        <div>
        <b>Extracted tweets</b><br />
        <b>Keyword pencarian:</b><asp:TextBox ID="namelab" runat="server" Text=""></asp:TextBox><br />
        <b>Jenis tampilan tweet:</b><asp:Label id="LabelJenisTampilanTweet" Text="" runat="server"/><br />
        <b>Bahasa tweet:</b><asp:Label id="LabelBahasaTweet" Text="" runat="server"/><br />
        </div>
        
        <div  style="margin-left: auto; margin-right: auto; text-align: center">
        <h1><b>Hasil ekstraksi tweet</b></h1> 
        </div>
        
        <div>
            <asp:Button ID="ButtonBack" Text="Back" runat="server" PostBackUrl="~/formstart.aspx" />
        </div>

        <div>
        <asp:Table ID="TweetTable" runat="server" Width="100%" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid"> 
            <asp:TableRow>
                <asp:TableCell>
                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                        ID
                    </div>
                </asp:TableCell>
                <asp:TableCell>
                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                         Tweets
                    </div>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>  
        </div>

        </form>

</body>
</html>
