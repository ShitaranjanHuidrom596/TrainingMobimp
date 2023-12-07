<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ThreeTierTemplate.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://cdn.tailwindcss.com"></script>

      <style>
        body{
            font-family:'Nirmala UI';
            margin:0;
        }
        #nav{
            font-family:'Nirmala UI';
            font-weight:bold;
            background-color:lightgray;
            display:flex;
            width:100%;
            height:100px;
        }
        #nav div{
            margin:5px;
            font-size:1.2rem;
            cursor:pointer;
        }
        #nav a{
            text-decoration:none;
            color:black;
        }
        #form1{
            margin-left:10px;
        }
        #namedd{
            width:40%;
            height:40px;
        }
        #delete_tb{
            width:100px;
            height:20px;
        }
        #students_grid{
            transition:height 1.5s width 1.5s ease-in-out;
            border:1px solid black;
            box-shadow:2px 2px 2px gray;
            padding:5px;
            cursor:pointer;
        }
        #student_delete_dd{
            height:30px;
            margin-bottom:4px;
            border-radius:2px;
        }
        .home_container{
            display:flex;
        }
        .right{
            margin-left:50px;
        }
        #name_search{
            height:30px;
            border-radius:5px;
        }
        #search_stud_button{
            color:#fff;
            font-weight:bold;
            height:35px;
            display:none;
            border-radius:5px;
            background:linear-gradient(to right, #312828, #000004);
        }
    </style>

    <title></title>
  </head>
<body>
     <div id="nav">
        <div><a href="index.aspx">Register</a></div>
        <div><a href="home.aspx">Home</a></div>
    </div>

    <div id="delete_modal" class="h-[300px] w-[400px] bg-amber-400 absolute inset-0 hidden m-auto">
        <div class="relative h-[100%] text-center">
            <p class="text-[20px]">Are you sure want to delete this student:<span id="stud_name"></span></p>
            <button class="absolute bottom-0 right-0 mb-5 mr-5 bg-cyan-500 p-2 rounded-md" onclick="delete_stud_yes()">Yes</button>
            <button class="absolute bottom-0 right-20 mb-5 bg-cyan-500 p-2 rounded-md" onclick="delete_stud_no()">No</button>
        </div>
    </div>


    <form id="form1" runat="server">
        <asp:HiddenField ID="del_stud" runat="server" />
        <div class="home_container">
        <div class="left">
            <h1> All the Students are listed here</h1>
            <asp:DropDownList ID="student_delete_dd" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Deleted</asp:ListItem>

            </asp:DropDownList>
            <div id="dt">
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
                  <h1>Search Students</h1>
                  <asp:TextBox CssClass="border-2 border-black" placeholder="Enter name" ID="name_search" runat="server" onkeyup="showsearch()"/>
                  <asp:Button CssClass="p-1" Text="Search Student" ID="search_stud_button" runat="server" OnClick="search_stud_button_Click" />
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
          </form>

    <script>
        var delete_id = "";
   
        const validate = () => {
            console.log("Halt");
        }

        const check_del = () => {
            if (confirm("Are you sure want to delete this student")) {
                return true;
            } else {
                return false;
            }
        }


        const showsearch=() => {
            var search_txt_box = $('#name_search');
            if (search_txt_box.val() != "") {
                $('#search_stud_button').css({ "display": "block" });
            }
            else {
                $('#search_stud_button').css({ "display": "none" });
            }
        }

        const delete_stud_yes = (e) => {
            //$('#delete_modal').css({ "display": "none" });
            $('#form1').submit();
            return true;
        }

        const delete_stud_no = (e) => {
            $('#delete_modal').css({ "display": "none" });
            return false;
        }

    </script>
</body>
</html>
