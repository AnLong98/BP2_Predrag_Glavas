//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Registration
    {
        public int startNumber { get; set; }
        public string Runner_email { get; set; }
        public int Race_raceID { get; set; }
    
        public virtual Runner Runner { get; set; }
        public virtual Race Race { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Result Result { get; set; }
    }
}
