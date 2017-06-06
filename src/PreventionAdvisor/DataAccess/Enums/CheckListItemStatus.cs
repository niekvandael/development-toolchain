using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreventionAdvisor.Enums
{
    public enum CheckListItemStatus
    {
        NOT_FILLED_IN = 0,
        OK = 1,
        NVT = 2,
        NOT_OK = 3
    }
}