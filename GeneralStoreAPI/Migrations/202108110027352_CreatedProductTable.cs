﻿namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        SKU = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Cost = c.Double(nullable: false),
                        NumberInInventory = c.Int(nullable: false),
                        IsInStock = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SKU);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
