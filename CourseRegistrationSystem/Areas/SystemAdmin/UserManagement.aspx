<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Areas/SystemAdmin/SystemAdmin.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="CourseRegistrationSystem.Areas.SystemAdmin.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="subTitle" runat="server">User Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Path" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Search" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
    </style>
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:GridView ID="UserGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="UserObjectDataSource" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" DataKeyNames="Id" OnRowCommand="UserGridView_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" >
<HeaderStyle CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                            <asp:BoundField DataField="PasswordHash" HeaderText="PasswordHash" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" >
<HeaderStyle CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />
                            <asp:TemplateField HeaderText="DisableAccount">
                                <ItemTemplate>
                                    <asp:CheckBox ID="DisableAccount" runat="server" Checked='<%#determineIsDisableCheckState(Eval("PasswordHash"))%>' OnCheckedChanged="DisableAccount_CheckedChanged"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="ResetPassword" runat="server" CommandName="ResetPasswordCmd" CommandArgument='<%# Eval("Id") %>'>Reset Password</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="UserObjectDataSource" runat="server" DeleteMethod="DeleteUser" InsertMethod="CreateUser" SelectMethod="getAllUsers" TypeName="CourseRegistrationSystem.Areas.SystemAdmin.UserManagement" UpdateMethod="UpdateUser">
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Id" Type="String" />
                            <asp:Parameter Name="userName" Type="String" />
                            <asp:Parameter Name="PasswordHash" Type="String" />
                            <asp:Parameter Name="email" Type="String" />
                            <asp:Parameter Name="PhoneNumber" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Id" Type="String" />
                            <asp:Parameter Name="userName" Type="String" />
                            <asp:Parameter Name="PasswordHash" Type="String" />
                            <asp:Parameter Name="email" Type="String" />
                            <asp:Parameter Name="PhoneNumber" Type="String" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                </td>
                <td>
                    <asp:ObjectDataSource ID="RolesObjectDataSource" runat="server" SelectMethod="getAllRoles" TypeName="CourseRegistrationSystem.Areas.SystemAdmin.UserManagement"></asp:ObjectDataSource>
                    <asp:DetailsView ID="UserDetailsView" runat="server" AutoGenerateRows="False" DataSourceID="UserObjectDataSource" Height="50px" Width="125px" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                        <EditRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <Fields>
                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" >
<HeaderStyle CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />
                            <asp:TemplateField HeaderText="RoleName">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="RolesRadioButtonList" runat="server" AutoPostBack="True" DataSourceID="RolesObjectDataSource" CausesValidation="True" OnSelectedIndexChanged="RolesRadioButtonList_SelectedIndexChanged">
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowInsertButton="True" InsertText="Create" NewText="Create New User" />
                        </Fields>
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    </asp:DetailsView>
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>