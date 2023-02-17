namespace CRUD_DBFirst.Models
{
    public class ConnectionString
    {
        private static string constr = "Data Source=VNMPCNHMI2; Initial Catalog=FileAndFolderDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
        public static string Constr
        {
            get => constr;
        }
    }
}
