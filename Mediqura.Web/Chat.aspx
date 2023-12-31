﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Mediqura.Web.Chat" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <link type="text/css" rel="stylesheet" href="css/ChatStyle.css" />
    <link rel="stylesheet" href="/css/JQueryUI/themes/base/jquery.ui.all.css" />

    <link href="DashboardCss/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="DashboardCss/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" />
    <link href="DashboardCss/custom.min.css" rel="stylesheet" />
    <link href="Styles/medistyles.css" rel="stylesheet" />
    <link href="Styles/wickedpicker.min.css" rel="stylesheet" />

   <!--Script references. -->

    <!--Reference the jQuery library. -->
    <script src='<%= this.ResolveClientUrl("DashboardCss/jquery.min.js") %>'></script>

    <script src="/Scripts/ui/jquery.ui.core.js"></script>
    <script src="/Scripts/ui/jquery.ui.widget.js"></script>
    <script src="/Scripts/ui/jquery.ui.mouse.js"></script>
    <script src="/Scripts/ui/jquery.ui.draggable.js"></script>
    <script src="/Scripts/ui/jquery.ui.resizable.js"></script>

    <script src='<%= this.ResolveClientUrl("DashboardCss/bootstrap.min.js") %>'></script>
    <script src='<%= this.ResolveClientUrl("DashboardCss/bootstrap-progressbar.min.js") %>'></script>
    <script src='<%= this.ResolveClientUrl("DashboardCss/moment.min.js") %>'></script>
    <script src='<%= this.ResolveClientUrl("DashboardCss/daterangepicker.js") %>'></script>
    <script src='<%= this.ResolveClientUrl("DashboardCss/custom.js") %>'></script>
    <script src='<%= this.ResolveClientUrl("Scripts/wickedpicker.min.js") %>'></script>


    <!--Reference the SignalR library. -->
    <script src="/Scripts/jquery.signalR-1.0.0.js"></script>

    <!--Reference the autogenerated SignalR hub script. -->
    <script src="/signalr/hubs"></script>

    <script type="text/javascript">

        $(function () {

            setScreen(false);

            // Declare a proxy to reference the hub. 
            var chatHub = $.connection.chatHub;

            registerClientMethods(chatHub);

            // Start Hub
            $.connection.hub.start().done(function () {

                registerEvents(chatHub)

            });

        });

        function setScreen(isLogin) {

            if (!isLogin) {

                $("#divChat").hide();
                $("#divLogin").show();
            }
            else {

                $("#divChat").show();
                $("#divLogin").hide();
            }

        }

        function registerEvents(chatHub) {

            $("#btnStartChat").click(function () {

                var name = $("#txtNickName").val();
                if (name.length > 0) {
                    chatHub.server.connect(name);
                }
                else {
                    alert("Please enter name");
                }

            });


            $('#btnSendMsg').click(function () {

                var msg = $("#txtMessage").val();
                if (msg.length > 0) {

                    var userName = $('#hdUserName').val();
                    chatHub.server.sendMessageToAll(userName, msg);
                    $("#txtMessage").val('');
                }
            });


            $("#txtNickName").keypress(function (e) {
                if (e.which == 13) {
                    $("#btnStartChat").click();
                }
            });

            $("#txtMessage").keypress(function (e) {
                if (e.which == 13) {
                    $('#btnSendMsg').click();
                }
            });


        }

        function registerClientMethods(chatHub) {

            // Calls when user successfully logged in
            chatHub.client.onConnected = function (id, userName, allUsers, messages) {

                setScreen(true);

                $('#hdId').val(id);
                $('#hdUserName').val(userName);
                $('#spanUser').html(userName);

                // Add All Users
                for (i = 0; i < allUsers.length; i++) {

                    AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName);
                }

                // Add Existing Messages
                for (i = 0; i < messages.length; i++) {

                    AddMessage(messages[i].UserName, messages[i].Message);
                }


            }

            // On New User Connected
            chatHub.client.onNewUserConnected = function (id, name) {

                AddUser(chatHub, id, name);
            }


            // On User Disconnected
            chatHub.client.onUserDisconnected = function (id, userName) {

                $('#' + id).remove();

                var ctrId = 'private_' + id;
                $('#' + ctrId).remove();


                var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');

                $(disc).hide();
                $('#divusers').prepend(disc);
                $(disc).fadeIn(200).delay(2000).fadeOut(200);

            }

            chatHub.client.messageReceived = function (userName, message) {

                AddMessage(userName, message);
            }


            chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message) {

                var ctrId = 'private_' + windowId;


                if ($('#' + ctrId).length == 0) {

                    createPrivateChatWindow(chatHub, windowId, ctrId, fromUserName);

                }

                $('#' + ctrId).find('#divMessage').append('<div class="message"><span class="userName">' + fromUserName + '</span>: ' + message + '</div>');

                // set scrollbar
                var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
                $('#' + ctrId).find('#divMessage').scrollTop(height);

            }

        }

        function AddUser(chatHub, id, name) {

            var userId = $('#hdId').val();

            var code = "";

            if (userId == id) {

                code = $('<div class="loginUser">' + name + "</div>");

            }
            else {

                code = $('<a id="' + id + '" class="user" >' + name + '<a>');

                $(code).dblclick(function () {

                    var id = $(this).attr('id');

                    if (userId != id)
                        OpenPrivateChatWindow(chatHub, id, name);

                });
            }

            $("#divusers").append(code);

        }

        function AddMessage(userName, message) {
            $('#divChatWindow').append('<div class="message"><span class="userName">' + userName + '</span>: ' + message + '</div>');

            var height = $('#divChatWindow')[0].scrollHeight;
            $('#divChatWindow').scrollTop(height);
        }

        function OpenPrivateChatWindow(chatHub, id, userName) {

            var ctrId = 'private_' + id;

            if ($('#' + ctrId).length > 0) return;

            createPrivateChatWindow(chatHub, id, ctrId, userName);

        }

        function createPrivateChatWindow(chatHub, userId, ctrId, userName) {

            var div = '<div id="' + ctrId + '" class="ui-widget-content draggable private-chat" rel="0">' +
               ' <div class="header">'+
                   ' <div  style="float:right;">'+
                         '  <i id="imgDelete" class="fa fa-remove"  style="cursor:pointer;"></i>'+
                       ' </div>'+
                   '<span class="selText" rel="0">' + userName + '</span>' +
              '  </div>'+
              '  <div class="row">'+
                  '  <div id="divMessage" class="messageArea">' +
                        
                '    </div>'+
                    '<div class="messageBar col-sm-12">'+
                        '<div class="input-group input-grp-chat">'+
                            '<textarea class="chatmsgbox" id="txtPrivateMessage" rows="1" placeholder="Type your message..."></textarea>' +
                           ' <span class="input-group-btn btn-group-chat">'+
                              '  <i style="font-size: 20px; margin-right: 10px;" class="fa fa-send" id="btnSendMessage"></i>' +
                           ' </span>'+
                       ' </div>'+
                   ' </div>'+
                '</div>'+
           ' </div>'
            var $div = $(div);

            // DELETE BUTTON IMAGE
            $div.find('#imgDelete').click(function () {
                $('#' + ctrId).remove();
            });

            // Send Button event
            $div.find("#btnSendMessage").click(function () {

                $textBox = $div.find("#txtPrivateMessage");
                var msg = $textBox.val();
                if (msg.length > 0) {

                    chatHub.server.sendPrivateMessage(userId, msg);
                    $textBox.val('');
                }
            });

            // Text Box event
            $div.find("#txtPrivateMessage").keypress(function (e) {
                if (e.which == 13) {
                    $div.find("#btnSendMessage").click();
                }
            });

            AddDivToContainer($div);

        }

        function AddDivToContainer($div) {
            $('#divContainer').prepend($div);

            $div.draggable({

                handle: ".header",
                stop: function () {

                }
            });

        }
        $(function () {

            $("#openChat").click(function () {

                $("#divChat").show();
                $("#openChat").hide();
                $("#closeChat").show();

            });

            $("#closeChat").click(function () {

                $("#divChat").hide();
                $("#openChat").show();
                $("#closeChat").hide();

            });
        })
    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <div id="divContainer">
            <div id="divLogin" class="login">
                <div>
                    Your Name:<br />
                    <input id="txtNickName" type="text" class="textBox" />
                </div>
                <div id="divButton">
                    <input id="btnStartChat" type="button" class="submitButton" value="Start Chat" />
                </div>
            </div>


            <div id="divChat" class="chatRoom">
                <div class="title">
                    Mediqura Chat Room [<span id='spanUser'></span>]
                </div>
                <div class="row">
                    <div class="content col-sm-12">
                        <div id="divChatWindow" class="chatWindow col-sm-8">
                        </div>
                         <div id="divusers" class="col-sm-4">
                             
                        </div>
                    </div>
                    <div class="messageBar col-sm-12">
                        <div class="input-group input-grp-chat">
                            <span class="input-group-btn btn-group-chat">
                                <asp:LinkButton runat="server" CssClass="fa fa-paperclip btnAttachment" ID="btnAttachment"></asp:LinkButton>
                            </span>
                            <textarea class="chatmsgbox" id="txtMessage" rows="1" placeholder="Type your message..."></textarea>
                            <span class="input-group-btn btn-group-chat">
                                <i style="font-size: 20px; margin-right: 10px;" class="fa fa-send" id="btnSendMsg"></i>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <input id="hdId" type="hidden" />
            <input id="hdUserName" type="hidden" />
        </div>
        <footer>
            <div class="pull-right color-foot">
                &nbsp; <a href="#" id="openChat"><span class="fa fa-paper-plane"></span>Chat</a><a href="#" id="closeChat" style="display: none"><span class="fa fa-paper-plane"></span>Chat</a>
            </div>
            <div class="pull-right color-foot">
                Powered By <a style="color: yellow" href="http://www.mobimp.com">Mobimp Services Pvt Ltd</a>
            </div>
            <div class="pull-left color-foot pad-lft">
                Copyright <a style="color: white">&copy; 2017,  All Rights Reserved</a>
            </div>
            <div class="clearfix"></div>
        </footer>
    </form>
</body>
</html>
