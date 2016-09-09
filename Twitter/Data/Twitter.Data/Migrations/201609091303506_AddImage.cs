namespace Twitter.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.IsDeleted);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.Images", "Id", "dbo.AspNetUsers");
            this.DropIndex("dbo.Images", new[] { "IsDeleted" });
            this.DropIndex("dbo.Images", new[] { "Id" });
            this.DropTable("dbo.Images");
        }
    }
}
