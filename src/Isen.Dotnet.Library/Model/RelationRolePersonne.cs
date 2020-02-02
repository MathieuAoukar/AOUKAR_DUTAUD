namespace Isen.Dotnet.Library.Model
{
    
    // cette classe a été créée afin de faire la laison m-m
    // nous avons essayer cette relation mais plusiseurs erreurs sont survenues 
    // on a préféré continuer tout en laissant une trace de notre travail 

    public class RelationRolePersonne : BaseEntity
    {
        public Personne Personne { get;set; }
        public int PersonneId { get;set; }
        public Role Role { get;set; }
        public int RoleId { get;set; }
    }
}