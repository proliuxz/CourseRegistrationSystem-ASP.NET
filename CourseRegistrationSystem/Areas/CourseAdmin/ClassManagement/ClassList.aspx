<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassList.aspx.cs" MasterPageFile="~/Areas/CourseAdmin/CourseAdmin.Master" Inherits="CourseRegistrationSystem.Areas.CourseAdmin.ClassManagement.ClassList"  Title="Class List"%>
<asp:Content ID="Content1" runat="server" contentPlaceHolderID="ContentPlaceHolder1">
    
    <script type="text/javascript">

        var modalShow = function () {
            $('#myModal').on('hide.bs.modal', function (e) {
                var d1 = $("*[id$=Keep_Pending]").val();
                var d2 = document.getElementById(d1);
                console.log(d1);
                d2.click();
            })
            $('#myModal').modal('show');
            console.log("call already");
        }

    </script>

    

<div class="modal fade"  id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title" id="myModalLabel">Confirm Class</h4>
      </div>
      <div class="modal-body">
        <asp:Label runat="server" ID="lbl_confirmMsg"></asp:Label>
        <p>Do you want to confirm the class?</p>
      </div>
      <asp:HiddenField ID="hd_classid" runat="server" />
      <div class="modal-footer">
         <asp:button ID="Confirm" runat="server" OnClick="Confirm_Click" Text="Confirm Class"></asp:button>
         <asp:button ID="Cancel" runat="server" OnClick="Cancel_Click" Text="Cancel Class"></asp:button>
         <asp:button ID="Keep_Pending" runat="server" OnClick="Close_Click" Text="Keep Pending"></asp:button>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

<div>
    <asp:Panel ID="SelectPanel" runat="server" Width="1141px">
            <asp:Table ID="SelectTable" runat="server">
                <asp:TableRow>
                    <asp:TableCell Width="20%">
                        <label>
                            Select Course:
                        </label>
                    </asp:TableCell>
                    <asp:TableCell Width="80%">
                        <asp:DropDownList ID="DropDownCourse" runat="server" Width="220px"
                            OnSelectedIndexChanged="DropDownCourse_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem>Select All</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>

                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Record:
                    </asp:TableCell>
                    <asp:TableCell>
                        <label id="LblRecNo" runat="server"></label>
                    </asp:TableCell>
                    <asp:TableCell Width="20%" HorizontalAlign="Right">
                        <asp:Button ID="BtnCreate" runat="server" Text="Create Class" OnClick="BTNCREATE_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>

         <asp:Panel ID="Panel1" runat="server" >
            <asp:GridView ID="GridView1" runat="server" Width="1141px" CellPadding="4" ForeColor="#333333" 
                AllowPaging="True" 
                AutoGenerateColumns="False"
                AllowSorting="True"
                
                OnRowCommand="GridView1_RowCommand"
                OnRowDataBound="GridView1_RowDataBound"
                OnPageIndexChanging="GridView1_PageIndexChanging" 
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged">

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
                        <ControlStyle Width="30px" />
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
       
                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            Class ID
                        </HeaderTemplate>
                        <ItemTemplate >
                            <asp:LinkButton runat="server" Text='<%# Eval("ClassId") %>' style="display:block;text-align:left"
                                CommandArgument='<%#Eval("ClassId") %>' OnClick="BTNViewDetail_Click" ></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>

                    
                    <asp:BoundField DataField="Course.CourseTitle" HeaderText="Course" ControlStyle-Width="300px" ItemStyle-Width="300px" >
                    <ControlStyle Width="300px" />
                    <ItemStyle Width="300px" HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            Reg Num
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                        <ItemTemplate >
                            <asp:LinkButton runat="server" ID="RegNum" style="display:block;text-align:left"
                                CommandArgument='<%#Eval("ClassId") %>' OnClick="RegNum_Click" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" ControlStyle-Width="100px" ItemStyle-Width="100px" DataFormatString="{0:dd-MMM-yyyy}" >
                    <ControlStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="EndDate" HeaderText="End Date" ControlStyle-Width="100px" ItemStyle-Width="100px" DataFormatString="{0:dd-MMM-yyyy}" >
                    <ControlStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="isOpenForRegister" HeaderText="Open For Register" ControlStyle-Width="100px" ItemStyle-Width="100px" >
                    <ControlStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Status" HeaderText="Status" ControlStyle-Width="100px" ItemStyle-Width="100px">
                    <ControlStyle Width="100px" />
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            Opeation
                        </HeaderTemplate>
                        <ItemTemplate >
                            <asp:Button ID="BtnEdit" runat="server" Font-Size="9pt" Text="Edit" 
                                OnClick="BTNEDIT_Click" 
                                CommandArgument='<%# Bind("ClassId") %>' Width="50px" />

                           

                            <asp:Button ID="BtnDel" runat="server" Font-Size="9pt" Text="Delete" 
                                OnClick="BTNDELETE_Click" 
                                CommandArgument='<%# Bind("ClassId") %>' 
                                OnClientClick='javascript:return confirm("This record will be deleted?");'
                                Width="50px" />

                             <asp:Button ID="BtnCheckRegNum" runat="server" Font-Size="9" Text="Change Status"
                                 OnClick="BtnCheckRegNum_Click"
                                 CommandArgument='<%# Bind("ClassId") %>' Width="108px" />

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Search" runat="server">

</asp:Content>
<asp:Content ContentPlaceHolderID="subTitle" runat ="server">
    Class List
</asp:Content>


