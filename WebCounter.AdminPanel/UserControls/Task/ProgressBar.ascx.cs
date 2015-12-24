namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class ProgressBar : System.Web.UI.UserControl
    {        
        /// <summary>
        /// Updates the progress bar.
        /// </summary>
        /// <param name="percents">The percents.</param>
        public void UpdateProgressBar(int percents)
        {
            if (plProgressBar.Attributes["style"] != null)
                plProgressBar.Attributes["style"] = string.Format("width: {0}%", percents);
            else
                plProgressBar.Attributes.Add("style", string.Format("width: {0}%", percents));

            lrlPercents.Text = percents + "%";
        }
    }
}