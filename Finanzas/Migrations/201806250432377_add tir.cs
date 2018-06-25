namespace Finanzas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtir : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resultado", "rentabilidad_Id", c => c.Int());
            CreateIndex("dbo.Resultado", "rentabilidad_Id");
            AddForeignKey("dbo.Resultado", "rentabilidad_Id", "dbo.Rentabilidad", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resultado", "rentabilidad_Id", "dbo.Rentabilidad");
            DropIndex("dbo.Resultado", new[] { "rentabilidad_Id" });
            DropColumn("dbo.Resultado", "rentabilidad_Id");
        }
    }
}
