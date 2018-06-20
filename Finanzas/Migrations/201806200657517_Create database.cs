namespace Finanzas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createdatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes");
            DropPrimaryKey("dbo.Resultadoes");
            AlterColumn("dbo.Resultadoes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Resultadoes", "Id");
            CreateIndex("dbo.Resultadoes", "Id");
            AddForeignKey("dbo.Resultadoes", "Id", "dbo.Bonoes", "Id");
            AddForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes");
            DropForeignKey("dbo.Resultadoes", "Id", "dbo.Bonoes");
            DropIndex("dbo.Resultadoes", new[] { "Id" });
            DropPrimaryKey("dbo.Resultadoes");
            AlterColumn("dbo.Resultadoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Resultadoes", "Id");
            AddForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes", "Id");
        }
    }
}
