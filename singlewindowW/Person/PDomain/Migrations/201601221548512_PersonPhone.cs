namespace PDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonPhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhoneNumbers", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PhoneNumbers", "PhoneNumber", c => c.String());
        }
    }
}
