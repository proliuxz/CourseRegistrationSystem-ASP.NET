<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationList.aspx.cs" MasterPageFile="~/Areas/CourseAdmin/CourseAdmin.Master" Inherits="CourseRegistrationSystem.Areas.CourseAdmin.RegistrationManagement.RegistrationList" %>

<asp:Content ID="Content1" runat="server" contentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <asp:Panel ID="SelectPanel" runat="server" Width="1250px">
            <asp:Table ID="SelectTable" runat="server">
                <asp:TableRow>
                    <asp:TableCell Width="10%">
                        <label>
                            Category:
                        </label>
                    </asp:TableCell>
                    <asp:TableCell Width="25%">
                        <asp:DropDownList ID="DropDownCategory" runat="server" Width="220px"
                            OnSelectedIndexChanged="DropDownCategory_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem>Select All</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Width="10%">
                        <label>
                            Course:
                        </label>
                    </asp:TableCell>
                    <asp:TableCell Width="25%">
                        <asp:DropDownList ID="DropDownCourse" runat="server" Width="220px"
                            OnSelectedIndexChanged="DropDownCourse_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem>Select All</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell Width="10%">
                        <label>
                            ClassId:
                        </label>
                    </asp:TableCell>
                    <asp:TableCell Width="20%">
                        <asp:DropDownList ID="DropDownClass" runat="server" Width="220px"
                            OnSelectedIndexChanged="DropDownClass_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem>Select All</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>

                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <label>
                            ParticipantName:
                        </label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtParticipantName" runat="server" Width="220px">
                        </asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <label>
                            ParticipantIdNumber:
                        </label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtParticipantIdNumber" runat="server" Width="220px">
                        </asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <label>
                            ParticipantCompanyName:
                        </label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtParticipantCompanyName" runat="server" Width="220px">
                        </asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right">
                        <asp:Button ID="BtnReset" runat="server" Text="Reset" Width ="70px" OnClick="BtnReset_Click"/>
                        <asp:Button ID="BtnSearch" runat="server" Text="Search" Width ="70px" OnClick="BtnSearch_Click"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell Width="30%">
                        Record:
                    </asp:TableCell>
                    <asp:TableCell Width="70%">
                        <label id="LblRecNo" runat="server"></label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>

        <asp:Panel ID="Panel1" runat="server" >
            <asp:GridView ID="GridView1" runat="server" Width="1250px" CellPadding="4" ForeColor="#333333" 
                AllowPaging="True" 
                AutoGenerateColumns="False"
                AllowSorting="True"
                
                OnRowCommand="GridView1_RowCommand"
                OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">

                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30px" />
                
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Height="30px" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                <Columns>
                    <asp:TemplateField HeaderText="No." ControlStyle-Width="30px" ItemStyle-Width="30px">
                        <ItemTemplate>
                        <asp:Label ID="LblRecordNo" runat="server" Text='<%# Container.DataItemIndex+1%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="CourseClass.Course.Category.CategoryName" HeaderText="Category" >
                        <ControlStyle Width="200px" />
                        <ItemStyle Width="200px" />
                    </asp:BoundField>
                    
                    <asp:TemplateField HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            CourseTitle
                        </HeaderTemplate>
                        <ItemTemplate >
                            <asp:LinkButton runat="server" Text='<%# Eval("CourseClass.Course.CourseTitle") %>' style="display:block;text-align:left"
                                CommandArgument='<%#Eval("CourseClass.Course.CourseCode") %>' OnClick="BTNVIEW_Click" ></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="CourseClass.ClassId" HeaderText="ClassId" ControlStyle-Width="100px" ItemStyle-Width="100px" >
                        <ControlStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CourseClass.StartDate" HeaderText="StartDate" ControlStyle-Width="100px" ItemStyle-Width="100px" 
                        DataFormatString="{0:dd-MMM-yyyy}" >
                        <ControlStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CourseClass.EndDate" HeaderText="EndDate" ControlStyle-Width="100px" ItemStyle-Width="100px" 
                        DataFormatString="{0:dd-MMM-yyyy}" >
                        <ControlStyle Width="100px" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
               
                    <asp:BoundField DataField="Participant.Fullname" HeaderText="Participant" ControlStyle-Width="100px" ItemStyle-Width="100px" >
                        <ControlStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>

                        <asp:BoundField DataField="Status" HeaderText="Status" ControlStyle-Width="100px" ItemStyle-Width="100px" >
                        <ControlStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                        
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            Opeation
                        </HeaderTemplate>

                        <ItemTemplate >
                            <asp:Button ID="BtnConfirm" runat="server" Font-Size="9pt" Text="Confirm" 
                                OnClick="BTNCONFIRM_Click" 
                                CommandArgument='<%# Bind("RegistrationId") %>' Width="60px" />
                            <asp:Button ID="BtnCancel" runat="server" Font-Size="9pt" Text="Cancel" 
                                OnClick="BTNCANCEL_Click" 
                                CommandArgument='<%# Bind("RegistrationId") %>' 
                                OnClientClick='javascript:return confirm("This registration will be canceled?");'
                                Width="60px" />
                            <asp:Button ID="BtnDetail" runat="server" Font-Size="9pt" Text="Detail" 
                                OnClick="BTNDETAIL_Click" 
                                CommandArgument='<%# Bind("RegistrationId") %>' Width="60px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="subTitle" runat ="server">
    Registration List
</asp:Content>