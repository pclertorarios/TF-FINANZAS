namespace Finanzas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        vnominal = c.Double(nullable: false),
                        vcomercial = c.Double(nullable: false),
                        años = c.Int(nullable: false),
                        frecuencia = c.Int(nullable: false),
                        diasAño = c.Int(nullable: false),
                        tipoInteres = c.String(nullable: false),
                        capitalizacion = c.Int(),
                        tasaInteres = c.Double(nullable: false),
                        tasaDescuento = c.Double(nullable: false),
                        impuestoRenta = c.Double(nullable: false),
                        fechaEmision = c.DateTime(nullable: false),
                        pPrima = c.Double(nullable: false),
                        pEstructura = c.Double(nullable: false),
                        pColoca = c.Double(nullable: false),
                        pFlota = c.Double(nullable: false),
                        pCAVALI = c.Double(nullable: false),
                        UsuarioID = c.Int(nullable: false),
                        nombre = c.String(nullable: false),
                        tipoActor = c.String(nullable: false),
                        Resultado_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resultado", t => t.Resultado_Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID, cascadeDelete: true)
                .Index(t => t.UsuarioID)
                .Index(t => t.Resultado_Id);
            
            CreateTable(
                "dbo.Resultado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        estructura_Id = c.Int(),
                        ratios_Id = c.Int(),
                        utilidad_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estructuracion", t => t.estructura_Id)
                .ForeignKey("dbo.RatiosDesicion", t => t.ratios_Id)
                .ForeignKey("dbo.Utilidad", t => t.utilidad_Id)
                .Index(t => t.estructura_Id)
                .Index(t => t.ratios_Id)
                .Index(t => t.utilidad_Id);
            
            CreateTable(
                "dbo.Estructuracion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        totalPeriodos = c.Int(nullable: false),
                        TEA = c.Double(nullable: false),
                        TEP = c.Double(nullable: false),
                        COK = c.Double(nullable: false),
                        costesIniciales = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Periodo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        N = c.Int(nullable: false),
                        plazoGracia = c.String(),
                        bono = c.Double(),
                        cupon = c.Double(),
                        cuota = c.Double(),
                        amortizacion = c.Double(),
                        prima = c.Double(),
                        escudo = c.Double(),
                        flujo = c.Double(nullable: false),
                        flujoEscudo = c.Double(),
                        flujoActivo = c.Double(),
                        flujoActivoPlazo = c.Double(),
                        factorConvexidad = c.Double(),
                        Resultado_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resultado", t => t.Resultado_Id)
                .Index(t => t.Resultado_Id);
            
            CreateTable(
                "dbo.RatiosDesicion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        duracion = c.Double(nullable: false),
                        convexidad = c.Double(nullable: false),
                        total = c.Double(nullable: false),
                        duracionModificada = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Utilidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        precioActual = c.Double(nullable: false),
                        utilidad = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        nombre = c.String(),
                        apellido = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rentabilidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TCEA = c.Double(),
                        TREA = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bono", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Bono", "Resultado_Id", "dbo.Resultado");
            DropForeignKey("dbo.Resultado", "utilidad_Id", "dbo.Utilidad");
            DropForeignKey("dbo.Resultado", "ratios_Id", "dbo.RatiosDesicion");
            DropForeignKey("dbo.Periodo", "Resultado_Id", "dbo.Resultado");
            DropForeignKey("dbo.Resultado", "estructura_Id", "dbo.Estructuracion");
            DropIndex("dbo.Periodo", new[] { "Resultado_Id" });
            DropIndex("dbo.Resultado", new[] { "utilidad_Id" });
            DropIndex("dbo.Resultado", new[] { "ratios_Id" });
            DropIndex("dbo.Resultado", new[] { "estructura_Id" });
            DropIndex("dbo.Bono", new[] { "Resultado_Id" });
            DropIndex("dbo.Bono", new[] { "UsuarioID" });
            DropTable("dbo.Rentabilidad");
            DropTable("dbo.Usuario");
            DropTable("dbo.Utilidad");
            DropTable("dbo.RatiosDesicion");
            DropTable("dbo.Periodo");
            DropTable("dbo.Estructuracion");
            DropTable("dbo.Resultado");
            DropTable("dbo.Bono");
        }
    }
}
