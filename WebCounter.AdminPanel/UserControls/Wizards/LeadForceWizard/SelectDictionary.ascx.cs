using System;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard
{
    public partial class SelectDictionary : LeadForceWizardStep
    {        
        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();
            
            BindDictionaryList(CurrentSiteTemplate);
        }



        /// <summary>
        /// Binds the dictionary list.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        protected void BindDictionaryList(Guid siteId)
        {
            rtvSource.Nodes.Clear();

            if (siteId == Guid.Empty)
                return;

            var dictionaries = new DataManager().Dictionary.SelectAll((int)AccessLevel.User);

            Guid? accessProfileId = DataManager.Sites.SelectById(siteId).AccessProfileID;

            if (accessProfileId.HasValue)
            {
                var accessProfileModules = DataManager.AccessProfileModule.SelectByAccessProfileID((Guid)accessProfileId).Where(apm => apm.Read && apm.Write);
                if (accessProfileModules.Any())
                {
                    var availableModules = accessProfileModules.Select(apr => apr.ModuleID).ToList();
                    dictionaries =
                        dictionaries.Where(
                            d => (
                                d.tbl_DictionaryGroup != null && d.tbl_DictionaryGroup.ModuleID.HasValue &&
                            availableModules.Contains((Guid)d.tbl_DictionaryGroup.ModuleID))
                            || d.tbl_DictionaryGroup == null
                            || !d.tbl_DictionaryGroup.ModuleID.HasValue);
                }
            }
            else if (CurrentUser.Instance.AccessLevelID != (int)AccessLevel.SystemAdministrator && CurrentUser.Instance.AccessLevelID != (int)AccessLevel.Administrator)
            {
                dictionaries = dictionaries.Where(d => (d.tbl_DictionaryGroup == null || !d.tbl_DictionaryGroup.ModuleID.HasValue));
            }

            dictionaries = dictionaries.OrderBy(a => a.tbl_DictionaryGroup.Title);

            foreach (var dictionaryItem in dictionaries)
            {
                if (dictionaryItem.DictionaryGroupID.HasValue && rtvSource.Nodes.FindNodeByValue(dictionaryItem.DictionaryGroupID.ToString()) == null)
                {
                    var groupRadTreeNode = new RadTreeNode
                    {
                        Value = dictionaryItem.DictionaryGroupID.ToString(),
                        Text = dictionaryItem.tbl_DictionaryGroup.Title,
                        PostBack = false,
                        Checkable = false
                    };

                    groupRadTreeNode.Attributes.Add("IsGroup", "true");

                    rtvSource.Nodes.Add(groupRadTreeNode);
                }
            }

            dictionaries = dictionaries.OrderBy(a => a.Title);

            foreach (var dictionaryItem in dictionaries)
            {
                var dictionaryRadTreeNode = new RadTreeNode
                {
                    Value = dictionaryItem.DataSet,
                    Text = dictionaryItem.Title,
                    PostBack = true
                };

                if (rtvSource.Nodes.FindNodeByValue(dictionaryItem.DictionaryGroupID.ToString()) != null)
                {
                    dictionaryRadTreeNode.Attributes.Add("GroupId", dictionaryItem.DictionaryGroupID.ToString());
                    rtvSource.Nodes.FindNodeByValue(dictionaryItem.DictionaryGroupID.ToString()).Nodes.Add(
                        dictionaryRadTreeNode);
                }
                else
                    rtvSource.Nodes.Add(dictionaryRadTreeNode);
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ucSiteTemplateComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSiteTemplateComboBox_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindDictionaryList((Guid)ucSiteTemplateComboBox.SelectedValue);
        }



        /// <summary>
        /// Handles the OnNodeDrop event of the rtvDestination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeDragDropEventArgs"/> instance containing the event data.</param>
        protected void rtvDestination_OnNodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            if ((!string.IsNullOrEmpty(e.HtmlElementID) && e.HtmlElementID == rtvDestination.ClientID) || e.DestDragNode != null)
            {
                foreach (RadTreeNode node in e.DraggedNodes)
                {
                    var toInsertNode = node.Clone();                    

                    if (IsExist(toInsertNode.Value))
                        continue;

                    if (toInsertNode.Attributes["GroupId"] != null)
                    {
                        toInsertNode.Text = string.Format("Справочник '{0}' из шаблона '{1}'", toInsertNode.Text, ucSiteTemplateComboBox.SelectedText);
                        toInsertNode.Attributes.Add("SiteId", ucSiteTemplateComboBox.SelectedValue.ToString());
                    }
                    else
                    {
                        foreach (RadTreeNode n in toInsertNode.Nodes)
                        {
                            n.Text = string.Format("Справочник '{0}' из шаблона '{1}'", n.Text, ucSiteTemplateComboBox.SelectedText);
                            n.Attributes.Add("SiteId", ucSiteTemplateComboBox.SelectedValue.ToString());
                        }
                    }

                    if (e.DestDragNode == null)
                    {
                        if (toInsertNode.Attributes["GroupId"] == null)
                            rtvDestination.Nodes.Add(toInsertNode);                        
                        else
                        {
                            if (rtvDestination.Nodes.FindNodeByValue(toInsertNode.Attributes["GroupId"]) == null)
                            {
                                var parentNode = node.ParentNode.Clone();
                                parentNode.Nodes.Clear();
                                parentNode.Nodes.Add(toInsertNode);
                                rtvDestination.Nodes.Add(parentNode);
                            }
                            else                            
                                rtvDestination.Nodes.FindNodeByValue(node.Attributes["GroupId"]).Nodes.Add(toInsertNode);                            
                        }
                    }
                    else if (toInsertNode.Attributes["IsGroup"] == null)
                    {
                        if (e.DestDragNode.Level > 0)
                        {
                            if (e.DestDragNode.ParentNode.Value == toInsertNode.Attributes["GroupId"])
                                e.DestDragNode.ParentNode.Nodes.Add(toInsertNode);
                        }
                        else if (e.DestDragNode.Value == toInsertNode.Attributes["GroupId"])
                            e.DestDragNode.Nodes.Add(toInsertNode);                        
                    }
                }
            }
        }



        /// <summary>
        /// Determines whether the specified value is exist.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is exist; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsExist(string value)
        {
            if (rtvDestination.Nodes.FindNodeByValue(value) != null)
                return true;

            foreach (RadTreeNode node in rtvDestination.Nodes)
            {
                if (node.Nodes.FindNodeByValue(value) != null)
                    return true;
            }

            return false;
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnRemove_OnClick(object sender, EventArgs e)
        {
            if (rtvDestination.SelectedNode != null)
                rtvDestination.SelectedNode.Remove();
        }
    }
}