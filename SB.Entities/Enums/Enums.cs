namespace SB.Entities
{
    public class Enums
    {
        public enum LoggingEvents
        {
            DbUpdateException = 1000,
            OwnException = 1001,
            Fatal = 1002
        }

        public enum Messages
        {
            DbError = 1010,
            FatalError = 2010,
            AdminError = 2020,
            UnAuthorized = 2030
        }

        public enum ErrorCodes
        {
            NotValidVersion = 100,
            NotNegativeValue = 110,
            NotNullName = 120,
            NotEmptyName = 130,
            InvalidEmailFormat = 140,
            InvalidModel = 150,
            NotFoundEntity = 160,
            MaxLenght=170,
            NotEmptyCountry = 180,
            NotEmptyState = 190,
            NotEmptyCity = 200,
            MinimumDate = 210
        }

        public enum DocumentType
        {
            TI,
            CC,
            PP
        }

        public enum CatalogType
        {
            Paises,
            Ciudades,
            Departamentos
        }
    }
}
