<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Web.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/login.css" rel="stylesheet" />
    <script src="scripts/jquery.min.js"></script>
    <script type="text/javascript" src="js/login.js"></script>
</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
        <div class="pagewrap">
            <div class="main">
                <div class="header">
                </div>
                <div class="content">
                    <div class="con_left"></div>
                    <div class="con_right">
                        <div class="con_r_top">
                            <a href="javascript:;" class="right" style="color: rgb(51, 51, 51); border-bottom-width: 2px; border-bottom-style: solid; border-bottom-color: rgb(46, 85, 142);">用户登录</a>
                        </div>
                        <ul>
                            <li class="con_r_right" style="display: block;">
                                <div class="user">
                                    <div>
                                        <span class="user-icon"></span>
                                        <input type="text" id="username" name="username" placeholder="　输入账号" value="" runat="server" />
                                    </div>

                                    <div>
                                        <span class="mima-icon"></span>
                                        <input type="password" id="password" name="password" placeholder="　输入密码" value="" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <asp:Button Id="btn_Login" type="submit" runat="server" Text="登 录" CssClass="loginbtn" OnClick="btn_cs_Click"></asp:Button>
                            </li>
                        </ul>

                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
