﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace Model
{

    using System;

    public partial class OrdersDetail
    {

        public string Id { get; set; }

        public string OrdersId { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Num { get; set; }

        public decimal Price { get; set; }

        public Nullable<System.DateTime> CreateTime { get; set; }

        public string CreatePerson { get; set; }

        public Nullable<System.DateTime> UpdateTime { get; set; }

        public string UpdatePerson { get; set; }

    }

}
