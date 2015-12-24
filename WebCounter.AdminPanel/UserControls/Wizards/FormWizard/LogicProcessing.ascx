<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogicProcessing.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.FormWizard.LogicProcessing" %>

<div class="wizard-step">
    <asp:Panel runat="server" ID="plNotFeedBack">
        <div class="row">
		    <label>Формат вывода полей:</label>
		    <asp:DropDownList ID="ddlOutputFormatFields" CssClass="select-text" runat="server" />
	    </div>
        <h3>Дополнительные поля ввода данных</h3>    
        <table>
            <tr>
                <td>
                    <h4>Все поля ввода данных</h4>
                    <br/>
                    <telerik:RadListBox runat="server" ID="rlbSource"
                        Skin="Windows7"  
                        Height="300px"              
                        Width="300px"
                        AllowTransfer="true" 
                        AllowTransferOnDoubleClick="true"
                        TransferToID="rlbDestination" 
                        TransferMode="Move"
                        AllowTransferDuplicates="false"   
                        SelectionMode="Single"                                                                      
                        AutoPostBackOnTransfer="true"
                        EnableDragAndDrop="true"                  
                        >
                            <ButtonSettings TransferButtons="All" ShowTransferAll="true" />                                                
                        </telerik:RadListBox>            
                </td>
                <td>
                    <h4>Выбранные поля ввода данных</h4>
                    <br/>
                    <telerik:RadListBox runat="server" ID="rlbDestination" AllowTransferDuplicates="false"
                            Skin="Windows7" AllowTransfer="false" Height="300px" Width="300px" AllowReorder="true" EnableDragAndDrop="true" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="plFeedBack" Visible="false">
        <div class="row">
            <label>Режим подачи заявки:</label>
            <asp:RadioButtonList runat="server" ID="rblStep" RepeatLayout="UnorderedList" CssClass="rlb-center" />
        </div>
        <div class="row">
            <label>База знаний:</label>
            <asp:RadioButtonList runat="server" ID="rblKnowledgeBase" RepeatLayout="UnorderedList" CssClass="rlb-center" />
        </div>
        <div class="row">
            <label>Типы обращений:</label>                    
            <asp:CheckBoxList runat="server" ID="chxPublicationType" RepeatLayout="UnorderedList" CssClass="rlb-center" />
        </div>
    </asp:Panel>
    <br/>
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="LogicProcessing"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>