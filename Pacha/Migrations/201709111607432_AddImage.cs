namespace Pacha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ImagePath = c.String(),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Images", new[] { "Product_ProductId" });
            DropTable("dbo.Images");
        }
    }
}
