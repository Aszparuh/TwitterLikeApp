namespace Twitter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTag : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 12),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.TweetTags",
                c => new
                    {
                        Tweet_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tweet_Id, t.Tag_Id })
                .ForeignKey("dbo.Tweets", t => t.Tweet_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Tweet_Id)
                .Index(t => t.Tag_Id);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.TweetTags", "Tag_Id", "dbo.Tags");
            this.DropForeignKey("dbo.TweetTags", "Tweet_Id", "dbo.Tweets");
            this.DropIndex("dbo.TweetTags", new[] { "Tag_Id" });
            this.DropIndex("dbo.TweetTags", new[] { "Tweet_Id" });
            this.DropIndex("dbo.Tags", new[] { "IsDeleted" });
            this.DropTable("dbo.TweetTags");
            this.DropTable("dbo.Tags");
        }
    }
}
