using SB.Entities;
using System.Linq;
using static SB.Entities.Enums;

namespace SB.Data
{
    public class DbInitializer
    {
        public static void Initialize(SqlServerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Catalog.Any()) return;

            context.Catalog.Add(new Catalog() { Code = "COL", Name = "Colombia", Type = CatalogType.Paises });

            context.SaveChanges();

            var countryId = context.Catalog.First(x => x.Code.Equals("COL")).Id;
            
            context.Catalog.Add(new Catalog() { Code = "CUD", Name = "Cundinamarca", Type = CatalogType.Departamentos, ParentId = countryId });

            context.Catalog.Add(new Catalog() { Code = "ANT", Name = "Antioquia", Type = CatalogType.Departamentos, ParentId = countryId });

            context.SaveChanges();

            var stateId = context.Catalog.First(x => x.Code.Equals("CUD")).Id;

            var stateId2 = context.Catalog.First(x => x.Code.Equals("ANT")).Id;

            context.Catalog.Add(new Catalog() { Code = "BOG", Name = "Bogotá", Type = CatalogType.Ciudades, ParentId = stateId });

            context.Catalog.Add(new Catalog() { Code = "GIR", Name = "Girardot", Type = CatalogType.Ciudades, ParentId = stateId });

            context.Catalog.Add(new Catalog() { Code = "ZIP", Name = "Zipaquira", Type = CatalogType.Ciudades, ParentId = stateId });

            context.Catalog.Add(new Catalog() { Code = "MOS", Name = "Mosquera", Type = CatalogType.Ciudades, ParentId = stateId });

            context.Catalog.Add(new Catalog() { Code = "ABE", Name = "Abejorral", Type = CatalogType.Ciudades, ParentId = stateId2 });

            context.Catalog.Add(new Catalog() { Code = "MED", Name = "Medellin", Type = CatalogType.Ciudades, ParentId = stateId2 });

            context.SaveChanges();
        }
    }
}
