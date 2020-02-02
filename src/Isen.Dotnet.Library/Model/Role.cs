using System.Text;

namespace Isen.Dotnet.Library.Model
{
    public class Role : BaseEntity
    {
        public string NomRole {get;set;}
        
        /*public Personne Personne {get;set;}
        public int? PersonneId {get;set;}*/

        public MyCollection<RelationRolePersonne> RelationRolePersonne {get;set;}

         public string Persons() {
            var sb = new StringBuilder();
            sb.Append($"{RelationRolePersonne[0]?.Personne?.Prenom} {RelationRolePersonne[0]?.Personne?.Nom}");
            for(var i = 1; i < RelationRolePersonne?.Count; i++)
            {
                sb.Append(", ");
                sb.Append($"{RelationRolePersonne[i]?.Personne?.Prenom} {RelationRolePersonne[i]?.Personne?.Nom}");
            }
            return sb.ToString();
        }

    }
}