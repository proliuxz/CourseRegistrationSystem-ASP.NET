<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassDetail.aspx.cs" MasterPageFile="~/Areas/CourseAdmin/CourseAdmin.Master"  Inherits="CourseRegistrationSystem.Areas.CourseAdmin.ClassManagement.ClassDetail"  Title="Class Detail"%>
<asp:Content ID="Content1" runat="server" contentPlaceHolderID="ContentPlaceHolder1">
 <div>
    <asp:Table ID="DataTable" runat="server">
        <asp:TableRow >
            <asp:TableCell>
                <asp:Label runat="server" Text="Class ID"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label runat="server" ID="lb_CourseCode"></asp:Label>
                <asp:TextBox runat="server" ID ="tb_ClassID"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                  <asp:Label runat="server" ID ="L_ErrMsgID" ForeColor="#ff0000"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Course"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddt_Course" runat="server"  OnSelectedIndexChanged="ddt_Course_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Start Date"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                 <asp:Calendar ID="Start_Date" runat="server" BackColor="White"
                      BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                      DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                      ForeColor="#003399" Height="200px" Width="220px" OnSelectionChanged="Start_Date_SelectionChanged">
                 <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                 <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                 <OtherMonthDayStyle ForeColor="#999999" />
                 <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                 <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                 <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                 <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                 <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="End Date"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                 <asp:Calendar ID ="End_Date" runat="server" sBackColor="White"
                      BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                      DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                      ForeColor="#003399" Height="200px" Width="220px" OnSelectionChanged="End_Date_SelectionChanged">
                 <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                 <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                 <OtherMonthDayStyle ForeColor="#999999" />
                 <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                 <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                 <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                 <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                 <WeekendDayStyle BackColor="#CCCCFF" /></asp:Calendar>
            </asp:TableCell>
            <asp:TableCell>
                  <asp:Label runat="server" ID ="L_ErrMsgDate" ForeColor="#ff0000"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Register Status"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddl_RegisterStatus" runat="server" >
                    <asp:ListItem Text="Closed"></asp:ListItem>
                    <asp:ListItem Text="Open"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Class Status"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddt_ClassStatus" runat="server" >
                    <asp:ListItem Text="Pending"></asp:ListItem>
                    <asp:ListItem Text="Confirm"></asp:ListItem>
                    <asp:ListItem Text ="Cancel"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableFooterRow>
            <asp:TableCell>
                <asp:Button runat="server" ID="btn_Submit" Text= "Submit" OnClick="btn_Submit_Click"/>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button runat="server" ID="btn_Cancel" Text="Cancel" OnClick="btn_Cancel_Click" />
            </asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
    </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Search">

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="subTitle">
    Class Detail
</asp:Content>