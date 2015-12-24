using System.Web;

namespace WebCounter.BusinessLogicLayer.Configuration
{
    public class PortalTemplates
    {
        public static string HeaderTemplate
        {
            get
            {                
                var header = string.Format(@"
<div style='width: 100%;background-color:#f3f3ef;'>
    <div style='width:980px;margin: 0 auto;padding: 8px 0px'>
        <table border='0' cellspacing='0' celpadding='0'>
            <tr>
                <td width='155px'>
                    <a href='#'>
                        <img src='{0}' />
                    </a>
                </td>
                <td width='545px'></td>
                <td width='280px' valign='bottom'>
                    <p style='font-size: 12px; color: #555;text-align:right'>Система коммуникаций и поддержки клиентов</p>
                </td>
            </tr>
        </table>
    </div>
</div>", VirtualPathUtility.ToAbsolute("~/App_Themes/Default/images/DefaultPortalLogo.png"));

                return header;
            }
        }
    }
}
