<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentServices.aspx.cs" Inherits="WebCounter.AdminPanel.PaymentServices" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        if (typeof (Telerik) != "undefined") {
            Telerik.Web.UI.RadToolTip.prototype._addToolTipToDocument = function (elem) {                
                var form = document.forms[0];
                form.appendChild(this._popupElement);
            };
        }    
        function norm_radio_name() {
            $("[type=radio]").each(function (i) {
                var name = $(this).attr("name");
                var splitted = name.split("$");
                $(this).attr("name", splitted[splitted.length - 1]);
            });            
        };
        $(document).ready(function () {
            norm_radio_name();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            norm_radio_name();            
        });
        $(window).load(function () {            
            $('.rotator-product-item').height($('.rrRelativeWrapper').height()-2);
            SetSelected($("input[@name=Products]:checked"));
        });
        function requestStart(sender, args) {
            var element = $find(args.get_eventTarget().replace(/\$/g, '_'));
            var isTelerikButton = Telerik.Web.UI.RadButton.isInstanceOfType(element);
            if (isTelerikButton == true) { element.set_enabled(false); }            
        }
        function responseEnd(sender, args) {
            var element = $find(args.get_eventTarget().replace(/\$/g, '_'));
            var isTelerikButton = Telerik.Web.UI.RadButton.isInstanceOfType(element);
            if (isTelerikButton == true) { element.set_enabled(true); }
            
            var splitted = name.split("$");
            if (splitted[splitted.length - 1] == "radSliderPriceList")
                return;
            $("ul.price-list li").first().addClass("first");
            $("ul.price-list li").last().addClass("last");
            var width = 0;
            $(".price-list li").each(function () { width += $(this).outerWidth(); });
            $('.price-list-content').width(width);
            $find("<%= radSliderPriceList.ClientID %>").set_width(width - 126);
        }
        function SetSelected(element) {
            $('.rotator-products .rotator-product-item.selected').height($('.rotator-products .rotator-product-item.selected').height() + 2);
            $('.rotator-products .rotator-product-item').removeClass('selected');
            var parentElement;
            if ($(element).is("input")) parentElement = $(element).parent().parent();else parentElement = $(element).parent();            
            $(parentElement).addClass('selected').height($(parentElement).height() - 2).find('input').attr('checked', 'checked');
            $find('<%= radAjaxManager.ClientID %>').ajaxRequest($('input:radio[name=Products]:checked').val());            
        }
    </script>
</telerik:RadScriptBlock>

    <table width="100%">
        <tr>
            <td width="195px" valign="top">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Доступ активен до">
                                <ContentTemplate>
                                    <div class="aside-padding-content">
                                        Активен до: <b><asp:Literal runat="server" ID="lrlActiveUntilDate" /></b>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Expanded="true" Text="Остаток средств">
                                <ContentTemplate>
                                    <div class="aside-padding-content">
                                        Остаток средств: <b><asp:Literal runat="server" ID="lrlBalance" /></b>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar> 
                    <uc:LeftColumn runat="server" />                     
                </div>
            </td>
            <td valign="top">
                <telerik:RadPanelBar runat="server" Width="100%" Skin="Windows7">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Тариф">
                            <ContentTemplate>                                
                                   <telerik:RadRotator ID="rotatorProducts" WrapFrames="false" CssClass="rotator-products"  runat="server" RotatorType="Buttons" ItemWidth="146" FrameDuration="1" ScrollDirection="Left,Right" Skin="Windows7">
                                        <ItemTemplate>
                                            <div class="rotator-product-item">                                                
                                                <a href="javascript:;" onclick="SetSelected(this);"><img src='<%# DataBinder.Eval(Container.DataItem, "ImageUrl") %>' height="65px" alt='<%# DataBinder.Eval(Container.DataItem, "Title")%>' title='<%# DataBinder.Eval(Container.DataItem, "Title")%>' /></a>
                                                <h4><%# DataBinder.Eval(Container.DataItem, "Title")%></h4>
                                                <telerik:RadToolTip ID="rttDescription" ShowEvent="OnClick" ManualClose="true" Skin="Windows7" Width="400px" RelativeTo="Element" runat="server" TargetControlID="showTooltip">
                                                    <p class="product-description"><%# Eval("Description") %></p>
                                                </telerik:RadToolTip>
                                                <a href="javascript:;" runat="server" id="showTooltip" class="description">Подробнее</a>
                                                <div class="bottom-item">
                                                    <span class="price"><%# ((decimal)Eval("Price")).ToString("F") %> руб.</span>                                            
                                                    <asp:RadioButton runat="server" ID="radioButton" Value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' GroupName="Products" Checked='<%# bool.Parse(Eval("IsSelected").ToString()) %>' onclick="SetSelected(this);" />                                                
                                                </div>
                                            </div>
                                        </ItemTemplate>                                    
                                    </telerik:RadRotator>                                
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>  
                <br/>                   
                <telerik:RadPanelBar ID="RadPanelBar2" runat="server" Width="100%" Skin="Windows7">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Период оплаты">
                            <ContentTemplate>
                                <div class="price-list-container">
                                    <h3>Вы выбрали: <span><asp:Literal runat="server" ID="lrlSelectedProduct" /></span></h3>
                                    <div class="price-list-content">                                        
                                        <asp:ListView runat="server" ID="lvPriceList">
                                            <LayoutTemplate>
                                                <ul class="price-list clearfix">
                                                    <li runat="server" id="itemPlaceHolder" />
                                                </ul>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <h4><%# Eval("tbl_PriceList.Title") %></h4>
                                                    <span class="discount">Скидка <%# double.Parse(Eval("Discount").ToString()).ToString("##") %>%</span>
                                                    <span class="marker">&nbsp;</span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                            <telerik:RadSlider ID="radSliderPriceList" AutoPostBack="true" OnValueChanged="radSliderPriceList_OnValueChanged" runat="server" ItemType="None" Width="330px" ShowIncreaseHandle="False" ShowDecreaseHandle="False"
                                                AnimationDuration="400" MaximumValue="2" CssClass="ItemsSlider" ThumbsInteractionMode="Free" Skin="Telerik">
                                            <Items>
                                                <telerik:RadSliderItem Text="" Value="0" />
                                                <telerik:RadSliderItem Text="" Value="1" />
                                                <telerik:RadSliderItem Text="" Value="2" />                                                                                             
                                            </Items>
                                        </telerik:RadSlider>
                                        <div class="totals">
                                            <span>Стоимость за период: <b><asp:Literal runat="server" ID="lrlPrice" /> руб.</b></span>
                                            <br/>
                                            <span>Скидка: <b><asp:Literal runat="server" ID="lrlDiscount" />%</b></span>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                    </telerik:RadPanelBar>
                    <br/>                   
                    <telerik:RadPanelBar ID="RadPanelBar3" runat="server" Width="100%" Skin="Windows7">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Итоги и оплата">
                                <ContentTemplate>
                                    <div class="payment-info">
                                        <div style="width: 600px;margin: 0 auto;text-align: left">
                                            <uc:NotificationMessage ID="ucNotificationMessage" runat="server" MessageType="Warning" />
                                        </div>                                        
                                        <p>Сумма к оплате: <span>
                                        <telerik:RadNumericTextBox runat="server" ID="rntxtPriceForPayment" Width="100px" EmptyMessage="" Value="1" MinValue="1" CssClass="input-text" Type="Number">
			                            	<NumberFormat GroupSeparator="" DecimalDigits="2" />
			                            </telerik:RadNumericTextBox>
			                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" ControlToValidate="rntxtPriceForPayment" CssClass="required" Text="*" ErrorMessage="Вы не ввели сумму к оплате" ValidationGroup="valGroupPayment" runat="server" /> руб.
                                        </span></p>
                                        <telerik:RadButton runat="server" UseSubmitBehavior="false" ID="rbtnPay" Skin="Windows7" Text="Оплатить" OnClick="rbtnPay_OnClick" ValidationGroup="valGroupPayment" />                                        
                                    </div>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                         </Items>
                    </telerik:RadPanelBar>                            
                    <asp:HiddenField runat="server" ID="hfProductId" />
                    <asp:HiddenField runat="server" ID="hfPriceId" />
            </td>
        </tr>
    </table>
</asp:Content>
