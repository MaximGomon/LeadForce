using System;
using System.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer.Configuration
{
    public class ScriptTemplates
    {
        public static string Script(bool includeScriptTag)
        {
            var script = @"
(function () {
    var lf = document.createElement('script'); lf.type = 'text/javascript'; lf.async = true; 
    lf.src = ('https:' == document.location.protocol) ? 'https' : 'http' + '://" + ConfigurationManager.AppSettings["webCounterJSPath"] + @"'; 
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(lf, s);
})();";

            if (includeScriptTag)
                return string.Format("<script type=\"text/javascript\">{0}\r\n</script>", script);
            
            return script;                 
        }



        /// <summary>
        /// Counters the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="includeScriptTag">if set to <c>true</c> [include script tag].</param>
        /// <returns></returns>
        public static string Counter(Guid siteId, bool includeScriptTag = true)
        {
            var script = @"
var _lfq = _lfq || [];";
            var counter = "_lfq.push(['WebCounter.LG_Counter', '" + siteId + "']);";

            if (includeScriptTag)
                return string.Format("<script type=\"text/javascript\">{0}\r\n{1}\r\n</script>", script, counter);

            return Script(false) + counter;
        }




        /// <summary>
        /// Forms the specified site id.
        /// </summary>        
        /// <param name="activityCode">The activity code.</param>
        /// <param name="includeScriptTag">if set to <c>true</c> [include script tag].</param>
        /// <returns></returns>
        public static string Form(Guid siteId, string activityCode, bool includeScriptTag = true)
        {
            //var form = "_lfq.push(['WebCounter.LG_Form', '" + activityCode + "', '$Mode', '$FromVisit', '$Through', '$Period']);";
            var form = "_lfq.push(['WebCounter.LG_Form', '" + activityCode + "', '$Mode', '$FromVisit', '$Through', '$Period'$Parameter]);";                        

            if (includeScriptTag)
                return string.Format("<script type=\"text/javascript\">\r\n{0}\r\n</script>", form);

            return Script(false) + form;
        }



        /// <summary>
        /// Popups the form.
        /// </summary>        
        /// <param name="activityCode">The activity code.</param>
        /// <param name="includeScriptTag">if set to <c>true</c> [include script tag].</param>
        /// <returns></returns>
        public static string PopupForm(Guid siteId, string activityCode, bool includeScriptTag = true)
        {
            //var form = "<a href=\"javascript:WebCounter.LG_Form('" + activityCode + "', '" + (int)FormMode.Popup + "', '$Parameter', '$PopupEffectAppear', '$PopupAlign', '$CallOnClosing', '$DelayAppearOnClosing', '$SizeFieldOnClosing')\">[Текст]</a>";
            var form = "<a href=\"javascript:WebCounter.LG_Form('" + activityCode + "', '" + (int)FormMode.Popup + "', '$Parameter', '$PopupEffectAppear', '$PopupAlign')\">[Текст]</a>";

            if (includeScriptTag)
                return form;

            return Script(false) + form;
        }




        /// <summary>
        /// Integrateds the form.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="activityCode">The activity code.</param>
        /// <param name="includeScriptTag">if set to <c>true</c> [include script tag].</param>
        /// <returns></returns>
        public static string IntegratedForm(Guid siteId, string activityCode, bool includeScriptTag = true)
        {
            var form = "_lfq.push(['WebCounter.LG_Form', '" + activityCode + "', '$Mode', '$FromVisit', '$Through', '$Period', '$Parameter', '$ContactCategory']);";

            if (includeScriptTag)
                return string.Format("<script type=\"text/javascript\">\r\n{0}\r\n</script>", form);

            return Script(false) + form;
        }



        /// <summary>
        /// Autoes the call form.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="activityCode">The activity code.</param>
        /// <param name="includeScriptTag">if set to <c>true</c> [include script tag].</param>
        /// <returns></returns>
        public static string AutoCallForm(Guid siteId, string activityCode, bool includeScriptTag = true)
        {
            var form = "_lfq.push(['WebCounter.LG_Form', '" + activityCode + "', '$Mode', '$FromVisit', '$Through', '$Period', '$Parameter', '$ContactCategory', '$PopupDelayAppear', '$PopupEffectAppear', '$PopupAlign']);";

            if (includeScriptTag)
                return string.Format("<script type=\"text/javascript\">\r\n{0}\r\n</script>", form);

            return Script(false) + form;
        }




        /// <summary>
        /// Floatings the button form.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="activityCode">The activity code.</param>
        /// <param name="includeScriptTag">if set to <c>true</c> [include script tag].</param>
        /// <returns></returns>
        public static string FloatingButtonForm(Guid siteId, string activityCode, bool includeScriptTag = true)
        {
            var form = "_lfq.push(['WebCounter.LG_Form', '" + activityCode + "', '$Mode', '$FromVisit', '$Through', '$Period', '$Parameter', '$ContactCategory', '$PopupEffectAppear', '$PopupAlign', '$FloatingButtonName', '$FloatingButtonIcon', '$FloatingButtonBackground', '$FloatingButtonPosition', '$FloatingButtonMargin']);";

            if (includeScriptTag)
                return string.Format("<script type=\"text/javascript\">\r\n{0}\r\n</script>", form);

            return Script(false) + form;
        }



        /// <summary>
        /// Calls the on closing form.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="activityCode">The activity code.</param>
        /// <param name="includeScriptTag">if set to <c>true</c> [include script tag].</param>
        /// <returns></returns>
        public static string CallOnClosingForm(Guid siteId, string activityCode, bool includeScriptTag = true)
        {
            var form = "_lfq.push(['WebCounter.LG_Form', '" + activityCode + "', '$Mode', '$FromVisit', '$Through', '$Period', '$Parameter', '$ContactCategory', '$PopupEffectAppear', '$PopupAlign', '$DelayAppearOnClosing', '$SizeFieldOnClosing']);";

            if (includeScriptTag)
                return string.Format("<script type=\"text/javascript\">\r\n{0}\r\n</script>", form);

            return Script(false) + form;
        }
    }
}