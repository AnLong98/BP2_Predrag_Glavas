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
    
    public partial class Payment
    {
        public int paymentID { get; set; }
        public double moneyPaid { get; set; }
        public string type { get; set; }
        public System.Guid Invoice_invoiceCode { get; set; }
    
        public virtual Invoice Invoice { get; set; }
    }
}