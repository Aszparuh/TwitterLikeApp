namespace Twitter.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddTweet : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 300),
                        ApplicationUserId = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.IsDeleted);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.Tweets", "ApplicationUserId", "dbo.AspNetUsers");
            this.DropIndex("dbo.Tweets", new[] { "IsDeleted" });
            this.DropIndex("dbo.Tweets", new[] { "ApplicationUserId" });
            this.DropTable("dbo.Tweets");
        }
    }
}
