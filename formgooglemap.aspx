<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formgooglemap.aspx.cs" Inherits="TestASPNETv0._0.formgooglemap" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>   

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Location</title>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=places"></script>  
    <script src="GoogleAPIScript.js" type="text/javascript"></script>  
    <link href="MapStyleSheet.css" rel="stylesheet" type="text/css" />  
</head>
<body>
     <h5 style="color:White;">  
        Article for C# corner</h5>  
    <input id="txtsearch" class="apply" type="text" placeholder="Enter Search Place e.g C# Corner Noida" />  
    <div id="divloadMap"></div>   

    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
