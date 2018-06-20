namespace Finanzas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Periodoes", "plazoGracia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Periodoes", "plazoGracia");
        }
    }
}
