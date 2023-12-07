<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="details.aspx.cs" Inherits="ThreeTierTemplate.details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_container" runat="server">
       <asp:HiddenField ID="del_stud" runat="server" />
        <div class="home_container">
        <div class="left">
            <br />
            <asp:DropDownList ID="student_delete_dd" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Deleted</asp:ListItem>

            </asp:DropDownList>
                  <asp:TextBox CssClass="border-2 border-black" placeholder="Enter name" ID="name_search" runat="server" onkeyup="showsearch()"/>
                  <asp:Button CssClass="bg-amber-400 p-1 circle-1 rounded-md" Text="Search Student" runat="server" OnClick="search_stud_button_Click" />
            <div id="dt">
                <br />
                <asp:GridView ID="students_grid"  runat="server" AutoGenerateColumns="false" OnRowCommand="students_grid_RowCommand" GridLines="None" CellPadding="4" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Student_ID" HeaderText="Student_ID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="Phone_No" HeaderText="Phone_No" />
                        <asp:BoundField DataField="Sex" HeaderText="Sex" />
                        <asp:BoundField DataField="Age" HeaderText="Age" />
                        <asp:BoundField DataField="Reg_Date" HeaderText="Reg_Date" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:ImageButton CssClass="w-[20px]" ID="btnEdit" CommandName="EditRow" ImageUrl="./Images/editIconedt.jpg" CommandArgument='<%#Eval("Student_ID")%>' runat="server" />
                                <asp:ImageButton CssClass="w-[22px]" ID="btnDelete" CommandName="DeleteRow" OnClientClick="return check_del()" CommandArgument='<%#Eval("Student_ID")%>' ImageUrl="./Images/deleteIcon1.jpg" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </div>
           </div>
              <div class="right">
                  <br />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="name_search" ErrorMessage="Please Enter character"
                  ValidationExpression="^[a-zA-Z]+$"
                  ForeColor="Red"    
                  ></asp:RegularExpressionValidator>
                  <br />
                  <asp:GridView ID="grid_student_namemacth" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                      <AlternatingRowStyle BackColor="White" />
                      <EditRowStyle BackColor="#7C6F57" />
                      <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                      <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                      <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                      <RowStyle BackColor="#E3EAEB" />
                      <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                      <SortedAscendingCellStyle BackColor="#F8FAFA" />
                      <SortedAscendingHeaderStyle BackColor="#246B61" />
                      <SortedDescendingCellStyle BackColor="#D4DFE1" />
                      <SortedDescendingHeaderStyle BackColor="#15524A" />
                  </asp:GridView>
                  <asp:Label ID="lb_no_result" runat="server" Text="" Visible="False"></asp:Label>
              </div>
            </div>
</asp:Content>
