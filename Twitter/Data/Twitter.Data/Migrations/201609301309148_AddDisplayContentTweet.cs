namespace Twitter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddDisplayContentTweet : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Tweets", "OriginalContent", c => c.String(nullable: false, maxLength: 300));
            this.AddColumn("dbo.Tweets", "DisplayContent", c => c.String(maxLength: 1000));
            this.DropColumn("dbo.Tweets", "Content");
        }

        public override void Down()
        {
            this.AddColumn("dbo.Tweets", "Content", c => c.String(nullable: false, maxLength: 300));
            this.DropColumn("dbo.Tweets", "DisplayContent");
            this.DropColumn("dbo.Tweets", "OriginalContent");
        }
    }
}
