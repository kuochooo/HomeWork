namespace HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶數量資訊MetaData))]
    public partial class 客戶數量資訊
    {
        public int 帳戶數量 { get; set; }
        public int 聯絡人數量 { get; set; }
    }
    
    public partial class 客戶數量資訊MetaData
    {
        [Required]
        public int 客戶ID { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 帳戶ID { get; set; }
        public Nullable<int> 聯絡人ID { get; set; }
        [Required]
        public bool 客戶刪除 { get; set; }
        public Nullable<bool> 帳戶刪除 { get; set; }
        public Nullable<bool> 聯絡人刪除 { get; set; }
    }
}
