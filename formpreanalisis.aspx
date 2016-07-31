<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formpreanalisis.aspx.cs" Inherits="TestASPNETv0._0.formpreanalisis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form preanalisis</title>
    <link href="newCSS/CSSmaster.css" rel="stylesheet"/>
</head>
<body>

    <form id="form3" runat="server">

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <div>

        <div>
            <b>Jenis algoritma:</b>
             <asp:Label id="LabelJenisAlgo" Text="" runat="server"/>
        </div>

        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Table ID="TokenTable" runat="server" Width="100%" BorderColor="Black" BorderWidth="1" ForeColor="Black" GridLines="Both" BorderStyle="Solid"> 
                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             Nama dinas
                        </div>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="<%# maxTokenAmount %>">
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                             Token
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>  

            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <asp:Button ID="StartAnalysisButton" align="center" runat="server" Text="Start Analysis" PostBackUrl="~/formanalisis.aspx" />
            </div>
        </div>
    </div>

    </form>

</body>
</html>
