<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseAdminHomePage.aspx.cs" MasterPageFile="~/Areas/CourseAdmin/CourseAdmin.Master" Inherits="CourseRegistrationSystem.Areas.CourseAdmin.CourseAdminIndex" %>

<asp:Content ID="Title" runat="server" contentPlaceHolderID="subTitle">
     Course Admin Home Page
</asp:Content>

<asp:Content ID="Search" runat="server" contentPlaceHolderID="Search">
   
</asp:Content>

<asp:Content ID="Menu" runat="server" contentPlaceHolderID="Menu" >
    <asp:Menu ID="caMenu" runat="server" BackColor="#E3EAEB" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="Small" 
                ForeColor="#666666" Orientation="Horizontal" StaticSubMenuIndent="10px" 
                Height="16px" Width="189px" style="margin-left: 0px">
        <Items>
            <asp:MenuItem Text="Course Admin Home Page" Value="Course Admin Home Page" NavigateUrl="~/Areas/CourseAdmin/CourseAdminHomePage.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Class Management" Value="Class Management" NavigateUrl="~/Areas/CourseAdmin/ClassManagement/ClassList.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Course Management" Value="Course Management" NavigateUrl="~/Areas/CourseAdmin/CourseManagement/CourseList.aspx"></asp:MenuItem>
            <asp:MenuItem Text="Registration Management" Value="Registration Management" NavigateUrl="~/Areas/CourseAdmin/RegistrationManagement/RegistrationList.aspx"></asp:MenuItem>
        </Items>
    </asp:Menu>
</asp:Content>


<asp:Content ID="Content1" runat="server" contentPlaceHolderID="ContentPlaceHolder1">
    
</asp:Content>


