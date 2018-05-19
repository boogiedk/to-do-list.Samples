namespace TrelloAlt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditActListModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActListModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ActListModels", new[] { "User_Id" });
            DropTable("dbo.ActListModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ActListModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ActListModels", "User_Id");
            AddForeignKey("dbo.ActListModels", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
