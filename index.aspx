<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Worles_Po_Status.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missing Wolres PO</title>
    <link rel="icon" href="images/ANU LOGO.jpg" type="image/jpg">
    <style type="text/css">
        *
        {
            padding:0px;
            margin:0px;               
            font-family:Segoe UI Historic; 
        }
        .logo img
        {
           width:40px;  
            line-height:80px;
            margin:5px;
        }
        .navbar
        {
            background-color:#3390FF;
            width:100%;
            padding:0px;
            margin:0px;   
            height:48px;  
            box-shadow:2px 2px 4px;       
        }
        .logo
        {
            float:left;    
        }
        .softwareName
        {
            line-height:50px;
            color:White;
        }
        .datashower
        {
            width:50%;
            margin:auto;
            max-height:450px;
            overflow-x:hidden;
            overflow-y:auto;
            border-radius:5px;
            border:2px solid black;
        }
        .DataView
        {
            text-align:center;
        }
        .Version
        {
            color:White;
            margin-top:-35px;
            float:right;
            margin-right:50px;
            font-weight:bold;
        }
        .footer
        {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: #3390ff;
            color: white;
            text-align: center;
            height:50px;
            line-height:50px;
            font-weight:bold;
            border-top-left-radius: 4px;
            border-top-right-radius:4px;
        }
        .checkBox
        {
            border:2px solid black;   
            width:150px; 
            text-align:center;
            height:30px;
            line-height:30px;
            border-radius:5px;
            margin-top:15px;
            font-weight:bold;
            background:#ffd700;
            color:#000000;
            margin-bottom:10px;
        }
        .links a
        {  
            text-decoration:none;
        }
        .emptyDataRow
        {
            text-align:center;
            font-size:20px;
            color:Red;    
        }
    </style>
</head>
<body>
     <div class="navbar">
        <div class="logo">
            <img src="images/ANU LOGO.jpg" alt="Alternate Text" />
        </div>
        <div class="softwareName">
            <center>
                <h2>
                    Missing Worles PO Status
                </h2>
            </center>
        </div>
         <div class="Version">
             <p>
                Version: 1.0
             </p>
         </div>
    </div>
    <form id="form1" runat="server">

    <center>
        <div class="checkBox">
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Last 10 Days" 
                AutoPostBack="true" oncheckedchanged="CheckBox1_CheckedChanged"/>
        </div>
    </center>


    <div class="datashower">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  CssClass="DataView" style="width:100%;" AlternatingRowStyle-BackColor="#CCCCCC" HeaderStyle-BackColor="#3390FF" HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="none" ShowHeaderWhenEmpty="true" EmptyDataText="No Missing Worles PO Record Available...!" EmptyDataRowStyle-ForeColor="Red" Font-Bold="False" EmptyDataRowStyle-CssClass="emptyDataRow">
      
            <Columns>
                <asp:HyperLinkField 
                    HeaderText="Missing"
                    DataNavigateUrlFields="Missing"
                    DataNavigateUrlFormatString="https://mail.google.com/mail/u/0/#search/PO{0}"
                    DataTextField="Missing"
                    HeaderStyle-Width="50%" 
                    Target="_blank"
                    ItemStyle-CssClass="links" />
                <asp:BoundField HeaderText="Last Worles PO Date" DataField="Date" HeaderStyle-Width="50%" />
            </Columns>  
        </asp:GridView>
    </div>
    </form>

    <div class="footer">
        <p style="line-height:50px;">
            Anu Worles Solutions &#169; 2020
        </p>
    </div>
</body>
</html>
