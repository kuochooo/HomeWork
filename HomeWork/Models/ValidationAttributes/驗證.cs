using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using HomeWork.Models;

namespace HomeWork.Models.ValidationAttributes
{
    public class 手機驗證 : DataTypeAttribute
    {
        客戶資料Entities db = new 客戶資料Entities();
        public 手機驗證() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            var str = (string)value;
            return Regex.IsMatch(value.ToString(), "/d{4}-/d{6}");
        }
    }                                             
}