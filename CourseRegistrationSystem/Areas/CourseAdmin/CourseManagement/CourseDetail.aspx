<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseDetail.aspx.cs" MasterPageFile="~/Areas/CourseAdmin/CourseAdmin.Master"  Inherits="CourseRegistrationSystem.Areas.CourseAdmin.ClassManagement.CourseDetail" %>

<asp:Content ID="Content1" runat="server" contentPlaceHolderID="ContentPlaceHolder1">
    

    <div>
        <asp:Panel ID="Panel3" runat="server" >
            <asp:Label ID="LblMessage" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" Height="566px" Width ="768px">

            <asp:Table ID="Table1" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top" Width="15%">
                        <asp:label runat="server">Code:</asp:label>
                    </asp:TableCell>
                    <asp:TableCell Width="85%">
                        <asp:TextBox ID="TxtCourseCode" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValid_Code" runat="server" Text="*" 
                            ControlToValidate="TxtCourseCode" ForeColor="Red" 
                            ErrorMessage="Please Enter" ValidationGroup="ValidGroup"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="HidTimestamp" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:label runat="server">Title:</asp:label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtCourseTitle" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValid_Title" runat="server" Text="*" 
                            ControlToValidate="TxtCourseTitle" ForeColor="Red" 
                            ErrorMessage="Please Enter" ValidationGroup="ValidGroup"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        Category:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="DropDownCategory" runat="server" Width="250px"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                 <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        Description:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtDescription" runat="server" TextMode="multiline" Rows="5" Width="500px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:label runat="server">Fee:</asp:label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TxtFee" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValid_Fee" runat="server" Text="*" 
                            ControlToValidate="TxtFee" ForeColor="Red" 
                            ErrorMessage="Please Enter" ValidationGroup="ValidGroup"></asp:RequiredFieldValidator>
                        <asp:RangeValidator id="RagValid_Fee" runat="server" 
                            ErrorMessage="Please input number in range(0-100,000)" ControlToValidate="TxtFee" ForeColor="Red"
                            MaximumValue="100000" MinimumValue="0" Type="Double" ValidationGroup="ValidGroup" />
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:label runat="server">Duration:</asp:label>
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="TxtNumberOfDays" runat="server" Width="250px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValid_Duration" runat="server" Text="*" 
                            ControlToValidate="TxtNumberOfDays" ForeColor="Red" 
                            ErrorMessage="Please Enter" ValidationGroup="ValidGroup"></asp:RequiredFieldValidator>
                        <asp:RangeValidator id="RagValid_Duration" runat="server" 
                            ErrorMessage="Please input integer in range(1-1000)" ControlToValidate="TxtNumberOfDays" ForeColor="Red"
                            MaximumValue="1000" MinimumValue="1" Type="Integer" ValidationGroup="ValidGroup" />
                    </asp:TableCell></asp:TableRow><asp:TableRow Height="100px">
                    <asp:TableCell VerticalAlign="Top">
                        Instructors:
                    </asp:TableCell><asp:TableCell>
                        <asp:Panel ID="Panel2" runat="server" Height="100px" ScrollBars="Vertical" BorderWidth="1px" Width="500px">
                            <asp:CheckBoxList ID="ChkBoxListInstructors" runat="server" >
                                <asp:listitem text="checkbox1"/> 
                                <asp:listitem text="checkbox2"/> 
                                <asp:listitem text="checkbox3"/> 
                                <asp:listitem text="checkbox4"/> 
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        Enabled:
                    </asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="ChkBoxEnabled" runat="server" Checked="true" />
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        CreateDate:
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="TxtCreateDate" runat="server" Enabled="false" Width="250px"></asp:TextBox>
                    </asp:TableCell></asp:TableRow></asp:Table><asp:Panel ID="BtnPanel" runat="server" >
                <asp:Panel ID="NewPanel" runat="server" HorizontalAlign="Center">
                    <asp:Button runat="server" Text="Create" OnClick="BtnCreate_Click" Width="80px" ValidationGroup="ValidGroup" />
                    <asp:Button runat="server" Text="Back" OnClick="BtnBack_Click" Width="80px" />
                </asp:Panel>
                <asp:Panel ID="EditPanel" runat="server" HorizontalAlign="Center">
                    <asp:Button runat="server" Text="Edit" ID="BtnEdit" OnClick="BtnEdit_Click" Width="80px" ValidationGroup="ValidGroup" />
                    <asp:Button runat="server" Text="Back" OnClick="BtnBack_Click"  Width="80px"/>
                </asp:Panel>
                <asp:Panel ID="ViewPanel" runat="server" HorizontalAlign="Center">
                    <asp:Button runat="server" Text="Back" OnClick="BtnBack_Click" Width="80px" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    
    </div>
  
</asp:Content>

<asp:Content ContentPlaceHolderID="subTitle" runat="server">
    Course Detail
</asp:Content>