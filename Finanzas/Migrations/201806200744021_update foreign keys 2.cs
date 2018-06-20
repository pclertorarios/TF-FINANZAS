namespace Finanzas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateforeignkeys2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resultadoes", "Id", "dbo.Bonoes");
            DropForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes");
            DropIndex("dbo.Resultadoes", new[] { "Id" });
            DropPrimaryKey("dbo.Resultadoes");
            AddColumn("dbo.Bonoes", "Resultado_Id", c => c.Int());
            AlterColumn("dbo.Resultadoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Resultadoes", "Id");
            CreateIndex("dbo.Bonoes", "Resultado_Id");
            AddForeignKey("dbo.Bonoes", "Resultado_Id", "dbo.Resultadoes", "Id");
            AddForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes");
            DropForeignKey("dbo.Bonoes", "Resultado_Id", "dbo.Resultadoes");
            DropIndex("dbo.Bonoes", new[] { "Resultado_Id" });
            DropPrimaryKey("dbo.Resultadoes");
            AlterColumn("dbo.Resultadoes", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Bonoes", "Resultado_Id");
            AddPrimaryKey("dbo.Resultadoes", "Id");
            CreateIndex("dbo.Resultadoes", "Id");
            AddForeignKey("dbo.Periodoes", "Resultado_Id", "dbo.Resultadoes", "Id");
            AddForeignKey("dbo.Resultadoes", "Id", "dbo.Bonoes", "Id");
        }
    }
}
