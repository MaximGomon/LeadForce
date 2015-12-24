<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCode.aspx.cs" Inherits="WebCounter.AdminPanel.FormCode" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="<%# ResolveUrl("~/App_Themes/Default/AdminPanel.css") %>" type="text/css" enableviewstate="false" />
    <script src="<%#ResolveUrl("~/Scripts/jquery-1.5.2.min.js")%>" type="text/javascript"></script>
    <script src="<%#ResolveUrl("~/Scripts/jquery.corner.js")%>" type="text/javascript"></script>
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            var selectedIcon = '';
            $(document).ready(function () {
                UpdateFormCode();
            });

            function ShowFloatingButtonIconRadWindow() { $find('<%= rwFloatingButtonIcon.ClientID %>').show(); }
            function CloseFloatingButtonIconRadWindow() {
                selectedIcon = '';
                $find('<%= rwFloatingButtonIcon.ClientID %>').close();
            }
            function SelectFloatingButtonIconRadWindow() {
                $('#aSelectIcon').text(selectedIcon);
                $('#aSelectIcon').attr('rel', selectedIcon);
                UpdateFormCode();
                selectedIcon = '';
                $find('<%= rwFloatingButtonIcon.ClientID %>').close();
            }

            function ClientItemSelected(sender, args) {
                selectedIcon = args.get_item().get_name();
            }
            function tsFormMode_ClientTabSelected(sender, eventArgs) {
                var tab = eventArgs.get_tab();
                var rblFormMode = $("#rblFormMode input:radio");
                for (var i = 0; i < rblFormMode.length; i++) {
                    if (rblFormMode[i].value == tab.get_value()) {
                        rblFormMode[i].checked = true;
                        break;
                    }
                }
                UpdateFormCode();
                return false;
            }
        </script>
    </telerik:RadCodeBlock>
    <style>
        html, body { height: auto; }
        form { height: auto; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnablePageMethods="true" />

        <div class="form-code-container">
        	<telerik:RadTabStrip ID="tsFormMode" OnClientTabSelected="tsFormMode_ClientTabSelected" SelectedIndex="0" runat="server">
            </telerik:RadTabStrip>
	        <div class="row" style="display:none">
		        <label>Режим:</label>
		        <asp:RadioButtonList runat="server" ID="rblFormMode" ClientIDMode="Static" CssClass="list" RepeatLayout="Flow" onclick="UpdateFormCode()"/>
	        </div>
            <br />
            <div class="row">
                <label>Параметр:</label>
                <asp:TextBox ID="txtParameter" ClientIDMode="Static" onkeyup="UpdateFormCode()" CssClass="input-text" runat="server" />
            </div>
            <div id="autocall" style="float: left; width: 450px">
	            <h3>Кому отражать</h3>
                <div class="row">
		            <label>Категория посетителя:</label>
                    <asp:RadioButtonList runat="server" ID="rblContactCategory" ClientIDMode="Static" CssClass="list radiobuttonlist-horizontal" RepeatLayout="Flow" RepeatDirection="Horizontal" onclick="UpdateFormCode()" />
	            </div>
                <div class="row">
		            <label>Отражать с визита N1:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtShowFromVisit" AutoCompleteType="Disabled" ClientIDMode="Static" onkeyup="UpdateFormCode()" EmptyMessage=""  MinValue="1" CssClass="input-text" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                    </telerik:RadNumericTextBox>
                </div>
                <div class="row">
		            <label>Отражать через N2:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtThrough" AutoCompleteType="Disabled" ClientIDMode="Static" onkeyup="UpdateFormCode()" EmptyMessage="" MinValue="1" CssClass="input-text" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                    </telerik:RadNumericTextBox>
                </div>
                <div class="row">
		            <label>Период:</label>
		            <asp:RadioButtonList runat="server" ID="rblPeriod" ClientIDMode="Static" onclick="UpdateFormCode()" CssClass="list radiobuttonlist-horizontal" RepeatLayout="Flow" RepeatDirection="Horizontal" />
	            </div>
            </div>
            <div id="floatingbutton" style="float: left; width: 450px">
                <h3>Кнопка вызова</h3>
                <div class="row">
                    <label>Название:</label>
                    <asp:TextBox ID="txtFloatingButtonName" ClientIDMode="Static" onkeyup="UpdateFormCode()" CssClass="input-text" runat="server" />
                </div>
                <div class="row">
                    <label>Иконка:</label>
                    <a id="aSelectIcon" href="javascript:;" rel="" onclick="ShowFloatingButtonIconRadWindow()">Выберите иконку</a>
                    <telerik:RadWindow ID="rwFloatingButtonIcon" Title="Выберите иконку" Width="713px" Height="390px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" runat="server">
                        <ContentTemplate>
                            <telerik:RadFileExplorer ID="rfeFloatingButtonIcon" OnClientItemSelected="ClientItemSelected" Height="300px" EnableAsyncUpload="True" EnableCreateNewFolder="False" Skin="Windows7" VisibleControls="ContextMenus,Grid,Toolbar" runat="server" />
                            <br />
                            <div class="buttons">
                                <a id="btnSelectIcon" href="javascript:;" onclick="SelectFloatingButtonIconRadWindow()" style="margin: 0 10px;" class="btn"><em>&nbsp;</em><span>Выбрать</span></a>
                                <a href="javascript:;" class="cancel" onclick="CloseFloatingButtonIconRadWindow()">Отмена</a>                                
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </div>
                <div class="row row-color-picker clearfix">
                    <label>Цвет фона:</label>
                    <telerik:RadColorPicker ShowIcon="true" ID="rcpFloatingButtonBackground" OnClientColorChange="UpdateFormCode" runat="server" PaletteModes="All" />
                </div>
                <div class="row">
                    <label>Часть экрана:</label>
                    <asp:DropDownList ID="ddlFloatingButtonPosition" ClientIDMode="Static" onchange="UpdateFormCode()" CssClass="select-text" runat="server" />
                </div>
                <div class="row">
                    <label>Отступ от края:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtFloatingButtonMargin" AutoCompleteType="Disabled" ClientIDMode="Static" onkeyup="UpdateFormCode()" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                    </telerik:RadNumericTextBox>
                </div>
            </div>
            
            <div id="clear1" class="clear"></div>
            <div id="popupform" style="float: left; width: 450px">
                <h3>Всплывающая форма</h3>
                <div id="rowShowDelayPopup" class="row">
		            <label>Задержка появления:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtPopupDelayAppear" AutoCompleteType="Disabled" ClientIDMode="Static" onkeyup="UpdateFormCode()" EmptyMessage="" MinValue="1" CssClass="input-text" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                    </telerik:RadNumericTextBox>
	            </div>
                <div class="row">
		            <label>Эффект появления:</label>
                    <asp:DropDownList ID="ddlPopupEffectAppear" ClientIDMode="Static" CssClass="select-text" onchange="UpdateFormCode()" runat="server" />
                </div>
                <div class="row row-popup-align">
		            <label>Место появления:</label>
                    <asp:RadioButtonList runat="server" ID="rblPopupAlign" ClientIDMode="Static" CssClass="list" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="3" onclick="UpdateFormCode()" />
                </div>
            </div>            
            <div id="clear2" class="clear"></div>
            <div id="closecall" style="float: left; width: 450px">
                <h3>Вызов на закрытие</h3>
                <div class="row">
		            <label>Задержка появления:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtDelayAppearOnClosing" AutoCompleteType="Disabled" ClientIDMode="Static" onkeyup="UpdateFormCode()" EmptyMessage="" MinValue="1" Value="10" CssClass="input-text" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                    </telerik:RadNumericTextBox>
                </div>
                <div class="row">
		            <label>Размер поля:</label>
                    <telerik:RadNumericTextBox runat="server" ID="txtSizeFieldOnClosing" AutoCompleteType="Disabled" ClientIDMode="Static" onkeyup="UpdateFormCode()" EmptyMessage="" MinValue="1" Value="50" CssClass="input-text" Type="Number">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                    </telerik:RadNumericTextBox>
                </div>
            </div>
            <div class="clear"></div>
            <h3>Код для подстановки</h3>
            <div class="warning">
                <p><b>ВАЖНО:</b> код вызова формы должен быть размещен на странице сайта после <asp:HyperLink ID="hlSettings" Target="_blank" runat="server">кода счетчика</asp:HyperLink>!</p>
            </div>
            <table id="formCode" class="tbl-counter-code">
                <tr>
                    <td><pre><code><asp:Literal runat="server" ID="lrlCounterCode"></asp:Literal></code></pre></td>
                </tr>
            </table>    
        </div>
    </form>
</body>
</html>