<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formstart.aspx.cs" Inherits="TestASPNETv0._0.formstart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form awal</title>
    <link href="newCSS/CSSmaster.css" rel="stylesheet"/>
</head>
<body>
       
        <ul>
            <li><a href="Dashboard.aspx">Dashboard</a></li>
            <li><a href="formstart.aspx">Home</a></li>
            <li><a href="formperihal.aspx">Perihal</a></li>
            <li style="float:right"><a class="active" href="formabout.aspx">About</a></li>
        </ul>
        
        <div style="margin-left: auto; margin-right: auto;">

            <form id="form1" runat="server">
            
            <!-- 
            <div id="namaApps" style="color: ghostwhite; text-align: center">
                <b>Simple Tweet Analyzer</b>
            </div>
            -->

            <br />
            <br />
            <br />

            <asp:Table ID="TweetTable" runat="server" Width="100%"> 
                
                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-right: auto; margin-right: auto; text-align:right;">
                            <b><asp:Label ID="Label1" Text="Keyword pencarian" runat="server"></asp:Label></b>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align:center;">
                            <asp:TextBox ID="tipetweet" runat="server"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-right: auto; margin-right: auto; text-align:right;">
                            <b><asp:Label ID="Label2" Text="Keyword Dinas PDAM" runat="server"></asp:Label></b>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align:center;">
                            <asp:TextBox ID="keywordPDAM" runat="server"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-right: auto; margin-right: auto; text-align:right;">
                            <b><asp:Label ID="Label3" Text="Keyword Dinas sosial" runat="server"></asp:Label></b>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell> 
                        <div style="margin-left: auto; margin-right: auto; text-align:center;">  
                            <asp:TextBox ID="keywordSOSIAL" runat="server"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-right: auto; margin-right: auto; text-align:right;">
                            <b><asp:Label ID="Label4" Text="Keyword Dinas kebersihan" runat="server"></asp:Label></b>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align:center;">
                            <asp:TextBox ID="keywordKEBERSIHAN" runat="server"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-right: auto; margin-right: auto; text-align:right;">
                            <b><asp:Label ID="Label5" Text="Keyword Dinas pertamanan" runat="server"></asp:Label></b>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align:center;">
                            <asp:TextBox ID="keywordPERTAMANAN" runat="server"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <div style="margin-right: auto; margin-right: auto; text-align:right;">
                            <b><asp:Label ID="Label6" Text="Keyword Dinkominfo" runat="server"></asp:Label></b>
                        </div>
                    </asp:TableCell>
                    <asp:TableCell>
                        <div style="margin-left: auto; margin-right: auto; text-align:center;">
                            <asp:TextBox ID="keywordKOMINFO" runat="server"></asp:TextBox>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
        </asp:Table>

        <asp:Table ID="TableOpsiAction" runat="server" Width="100%"> 
             <asp:TableRow>

                 <asp:TableCell>
                    <div style="margin-right: auto; margin-right: auto; text-align:left;">
                        <b><asp:Label ID="LabelJenisTampilanTweet" Text="Jenis tampilan tweet:" runat="server"></asp:Label></b><br />
                    </div>
                 </asp:TableCell>
                 
                 <asp:TableCell>
                     <div style="margin-right: auto; margin-right: auto; text-align:left;">
                        <b><asp:Label ID="LabelJenisBahasa" Text="Bahasa dalam tweet:" runat="server"></asp:Label></b><br />
                    </div>
                 </asp:TableCell>
                 
                 <asp:TableCell>
                     <div style="margin-right: auto; margin-right: auto; text-align:left;">
                        <b><asp:Label ID="LabelJenisAlgoritma" Text="Jenis algoritma:" runat="server"></asp:Label></b><br />
                     </>
                 </asp:TableCell>

             </asp:TableRow>

             <asp:TableRow>

                 <asp:TableCell>
                    <div style="margin-right: auto; margin-right: auto; text-align:left;">
                        <asp:RadioButtonList ID="optTampilanTweet" runat="server">
                                            <asp:ListItem Selected="True">HTML</asp:ListItem>
                                            <asp:ListItem>Plain text</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                 </asp:TableCell>
                 <asp:TableCell>
                     <div style="margin-right: auto; margin-right: auto; text-align:left;">
                         <asp:RadioButtonList ID="optBahasaTweet" runat="server">
                                            <asp:ListItem Selected="True">Bahasa Indonesia</asp:ListItem>
                                            <asp:ListItem>Basa Sunda</asp:ListItem>
                                            <asp:ListItem>English</asp:ListItem>
                         </asp:RadioButtonList>
                     </div>
                 </asp:TableCell>
                 <asp:TableCell>
                     <div style="margin-right: auto; margin-right: auto; text-align:left;">
                         <asp:RadioButtonList ID="optAlgoritma" runat="server">
                                            <asp:ListItem Selected="True">KMP</asp:ListItem>
                                            <asp:ListItem>Boyer-Moore</asp:ListItem>
                         </asp:RadioButtonList>
                     </div>
                 </asp:TableCell>

             </asp:TableRow>
        </asp:Table>
        
        <br />
        <br />
        <br />

        <asp:Table ID="TableActionButton" runat="server" Width="100%"> 
             <asp:TableRow>
                  
                <asp:TableCell>
                    <div style="margin-right: auto; margin-right: auto; text-align:right;">
                        <asp:Button ID="Button1" Text="ExtractTwitter" runat="server" OnClick="ExtractTweet_Click" />
                    </div>
                </asp:TableCell>
                <asp:TableCell>
                    <div style="margin-right: auto; margin-right: auto; text-align:left;">
                        <asp:Button ID="Button2" Text="Analisis" runat="server" OnClick="Analisis_Click" />
                    </div>
                </asp:TableCell>
               
            </asp:TableRow>
               
        </asp:Table>

            </form>

        </div>
 
</body>
</html>
