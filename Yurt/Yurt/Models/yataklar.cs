//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yurt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class yataklar
    {
        public int yat_id { get; set; }
        public string yat_no { get; set; }
        public string yat_durum { get; set; }
        public Nullable<int> oda_id { get; set; }
    
        public virtual odalar odalar { get; set; }
    }
}
