<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="externalform.aspx.cs" Inherits="WebCounter.Client.externalform" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="jquery-1.6.4.min.js"></script>
    <script type="text/javascript">
        function doPost() {
            $.ajax({
                type: "POST",
                url: "http://localhost:24419/externalFormHandler.ashx?id=355ce215-3a85-4f02-9d95-6708cfe27f63&async=true",
                data: $('#form1').serialize() + "&WebCounterUserID=" + WebCounter.UserID,
                success: function (msg) {
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" ClientIDMode="Static" action="http://localhost:24419/externalFormHandler.ashx?id=355ce215-3a85-4f02-9d95-6708cfe27f63" runat="server">
        <script type="text/javascript">
            var _lfq = _lfq || [];
            (function () {
                var leadforce = document.createElement('script'); leadforce.type = 'text/javascript'; leadforce.async = true;
                leadforce.src = ('https:' == document.location.protocol) ? 'https' : 'http' + '://localhost:24419/WebCounter.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(leadforce, s);
            })();
            _lfq.push(['WebCounter.LG_Counter', 'acada40f-be80-417f-a73b-dcc52de40edd']);
        </script>
        <%--
        <fieldset>
            <legend>Repost</legend>
            <label for="company">Компания:</label><br />
            <input type="text" id="company" name="company" /><br />
            <label for="address">Адрес:</label><br />
            <input type="text" id="address" name="address" /><br />
            <label for="hobby">Хобби:</label><br />
            <input type="text" id="hobby" name="hobby" /><br />
            <label for="country">Страна:</label><br />
            <select id="country" name="country">
                <option value="Украина">Украина</option>
                <option value="Россия">Россия</option>
                <option value="Белоруссия">Белоруссия</option>
            </select><br />
            <input type="submit" />
        </fieldset>
        --%>

        <fieldset>
            <legend>AJAX</legend>
            <label for="fullname">Ф.И.О.:</label><br />
            <input type="text" id="fullname" name="fullname" /><br />
            <label for="email">E-mail:</label><br />
            <input type="text" id="email" name="email" /><br />
            <label for="address">Адрес:</label><br />
            <input type="text" id="address" name="address" /><br />
            <label for="hobby">Хобби:</label><br />
            <input type="text" id="hobby" name="hobby" /><br />
            <label for="country">Страна:</label><br />
            <select id="country" name="country">
                <option value="Украина">Украина</option>
                <option value="Россия">Россия</option>
                <option value="Белоруссия">Белоруссия</option>
            </select><br />
            <input type="submit" onclick="doPost(); return false;" />
        </fieldset>
    </form>
</body>
</html>