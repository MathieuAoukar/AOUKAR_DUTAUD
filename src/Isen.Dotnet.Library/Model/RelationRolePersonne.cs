namespace Isen.Dotnet.Library.Model
{
    // cette classe a été créée afin de faire la laison m-m
    public class RelationRolePersonne : BaseEntity
    {
        public Personne Personne { get;set; }
        public int PersonneId { get;set; }
        public Role NomRole { get;set; }
        public int RoleId { get;set; }
    }
}