﻿using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PublicationMarkType
    {
        [Description("Нравиться")]
        Like = 0,
        [Description("Оценка")]
        Mark = 1,
        [Description("Архив")]
        Archive = 2
    }
}