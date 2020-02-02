using System.Text;

namespace Isen.Dotnet.Library.Model
{
    public class Role : BaseEntity
    {
        public string NomRole {get;set;}
        
        public override string ToString() => $"{NomRole}";

        /*
        public MyCollection<RelationRolePersonne> RelationRolePersonne {get;set;}
        public override string ToString() => NomRole;

         public string Personne() {
            var sb = new StringBuilder();
            sb.Append($"{RelationRolePersonne[0]?.Personne?.Prenom} {RelationRolePersonne[0]?.Personne?.Nom}");
            for(var i = 1; i < RelationRolePersonne?.Count; i++)
            {
                sb.Append(", ");
                sb.Append($"{RelationRolePersonne[i]?.Personne?.Prenom} {RelationRolePersonne[i]?.Personne?.Nom}");
            }
            return sb.ToString();
        }*/

    }
}