using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum Skin
    {
        [Description("По умолчанию")]
        Default = 0,
        [Description("Black")]
        Black = 1,
        [Description("Forest")]
        Forest = 2,
        [Description("Hay")]
        Hay = 3,
        [Description("Metro")]
        Metro = 4,
        [Description("Office 2007")]
        Office2007 = 5,
        [Description("Office Black")]
        Office2010Black = 6,
        [Description("Office Blue")]
        Office2010Blue = 7,
        [Description("Office Silver")]
        Office2010Silver = 8,
        [Description("Outlook")]
        Outlook = 9,
        [Description("Simple")]
        Simple = 10,
        [Description("Sitefinity")]
        Sitefinity = 11,
        [Description("Sunset")]
        Sunset = 12,
        [Description("Telerik")]
        Telerik = 13,
        [Description("Transparent")]
        Transparent = 14,
        [Description("Vista")]
        Vista = 15,
        [Description("Web 2.0")]
        Web20 = 16,
        [Description("Web Blue")]
        WebBlue = 17,
        [Description("Windows 7")]
        Windows7 = 18
    }
}
