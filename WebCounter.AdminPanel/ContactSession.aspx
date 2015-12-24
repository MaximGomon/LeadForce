<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactSession.aspx.cs" Inherits="WebCounter.AdminPanel.ContactSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="breadcrumbs">Мониторинг посетителей / Контакты / Карточка контакта / Карточка сессии</div>
    <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
        <Tabs>
            <telerik:RadTab Text="Общая информация" />
            <telerik:RadTab Text="Географические данные" />
            <telerik:RadTab Text="Технические данные" />            
        </Tabs>
    </telerik:RadTabStrip>
     <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
        <telerik:RadPageView ID="RadPageView1" runat="server">
            <div class="row">
                <label>Посетитель:</label>
                <asp:HyperLink runat="server" ID="hlContact" />
            </div>
            <div class="row">
                <label>Порядковый номер:</label>
                <asp:Literal runat="server" ID="lrlSessionNumber" />
            </div>
            <div class="row">
                <label>Дата сессии:</label>
                <asp:Literal runat="server" ID="lrlSessionDate" />
            </div>
            <div class="row">
                <label>URL источника:</label>
                <asp:Literal runat="server" ID="lrlRefferURL" />
            </div>
            <div class="row">
                <label>Откуда пришел:</label>
                <asp:Literal runat="server" ID="lrlCameFromURL" />
            </div>
            <div class="row">
                <label>Входная точка:</label>
                <asp:Literal runat="server" ID="lrlEnterPointUrl" />
            </div>                        
            <div class="row">
                <label>Ключевые слова:</label>
                <asp:Literal runat="server" ID="lrlKeywords" />
            </div>            
            <div class="row">
                <label>Содержание:</label>
                <asp:Literal runat="server" ID="lrlContent" />
            </div>
            <div class="row">
				<label>Рекламная площадка:</label>
				<asp:Literal runat="server" ID="lrlAdvertisingPlatform" />
			</div>
            <div class="row">
				<label>Тип рекламы:</label>
				<asp:Literal runat="server" ID="lrlAdvertisingType" />
			</div>
            <div class="row">
				<label>Рекламная кампания:</label>
				<asp:Literal runat="server" ID="lrlAdvertisingCampaign" />
			</div>
            <div class="row">
                <label>Рекомендовал:</label>
                <asp:HyperLink runat="server" ID="hlReffer" Target="_blank" />
            </div>          
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <div class="row">
                <label>IP пользователя:</label>
                <asp:Literal runat="server" ID="lrlUserIP" />
            </div>
            <div class="row">
                <label>Страна:</label>
                <asp:Literal runat="server" ID="lrlCountry" />
            </div>
            <div class="row">
                <label>Регион:</label>
                <asp:Literal runat="server" ID="lrlRegion" />
            </div>
            <div class="row">
                <label>Область:</label>
                <asp:Literal runat="server" ID="lrlDistrict" />
            </div>
            <div class="row">
                <label>Город:</label>
                <asp:Literal runat="server" ID="lrlCity" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server">
            <div class="row">
                <label>Агент пользователя:</label>
                <asp:Literal runat="server" ID="lrlUserAgent" />
            </div>
            <div class="row">
                <label>Браузер:</label>
                <asp:Literal runat="server" ID="lrlBrowser" />
            </div>
            <div class="row">
                <label>Операционная система:</label>
                <asp:Literal runat="server" ID="lrlOperationSystem" />
            </div>
            <div class="row">
                <label>Разрешение экрана:</label>
                <asp:Literal runat="server" ID="lrlResolution" />
            </div>
            <div class="row">
                <label>Мобильное устройство:</label>
                <asp:Literal runat="server" ID="lrlMobileDevice" />
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
