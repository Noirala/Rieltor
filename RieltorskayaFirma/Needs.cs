//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RieltorskayaFirma
{
    using System;
    using System.Collections.Generic;
    
    public partial class Needs
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        public int ApartmentId { get; set; }
        public Nullable<int> MinPrice { get; set; }
        public Nullable<int> MaxPrice { get; set; }
    
        public virtual Agents Agents { get; set; }
        public virtual Apartments Apartments { get; set; }
        public virtual Clients Clients { get; set; }
    }
}
