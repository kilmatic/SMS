//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Timetable
    {
        public string timetable_id { get; set; }
        public Nullable<System.DateTime> day_of_week { get; set; }
        public Nullable<System.TimeSpan> hour_of_day { get; set; }
    }
}
