namespace Pacha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrandCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Products", "New", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "Brand_BrandId", c => c.Int());
            AddColumn("dbo.Products", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Products", "Brand_BrandId");
            CreateIndex("dbo.Products", "Category_CategoryId");
            AddForeignKey("dbo.Products", "Brand_BrandId", "dbo.Brands", "BrandId");
            AddForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories", "CategoryId");
            DropColumn("dbo.Products", "ProductCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductCategory", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "Brand_BrandId", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "Category_CategoryId" });
            DropIndex("dbo.Products", new[] { "Brand_BrandId" });
            DropColumn("dbo.Products", "Category_CategoryId");
            DropColumn("dbo.Products", "Brand_BrandId");
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.Products", "New");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
        }
    }
}
