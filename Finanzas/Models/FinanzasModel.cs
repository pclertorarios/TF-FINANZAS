namespace Finanzas.Models
{
    using Resultados;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FinanzasModel : DbContext
    {
        // Your context has been configured to use a 'FinanzasModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Finanzas.Models.FinanzasModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FinanzasModel' 
        // connection string in the application configuration file.
        public FinanzasModel()
            : base("name=FinanzasModel")
        {
        }
        public virtual DbSet<Bono> Bono { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<Estructuracion> Estructuracion { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<RatiosDesicion> RatiosDesicion { get; set; }
        public virtual DbSet<Rentabilidad> Rentabilidad { get; set; }
        public virtual DbSet<Utilidad> Utilidad { get; set; }

        public virtual DbSet<Resultado> Resultado { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}