using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Isen.Dotnet.Library.Model
{

    public class Personne : BaseEntity
    {
        public string Nom {get;set;}
        public string Prenom {get;set;}
        public DateTime? Date_anniversaire {get;set;}
        public string Telephone {get;set;}
        public string Mail {get;set;}

        public Role TypeRole {get;set;}
        public int? TypeRoleId {get;set;}

        //Relation avec le service
        public Service TypeService {get;set;}
        public int? TypeServiceId {get;set;}

        public MyCollection<RelationRolePersonne> RelationRolePersonne {get;set;}

        [NotMapped] // afin de ne pas générer ce champ dans la bdd
        public int? Age => Date_anniversaire.HasValue ?
            (int)((DateTime.Now - Date_anniversaire.Value).TotalDays / 365) :
            new int?();
        
        public override string ToString() =>
            $"{Nom} {Prenom} | {Date_anniversaire} ({Age}) | {TypeRole} / {TypeService}";

        public string Roles() {
        var sb = new StringBuilder();
        sb.Append(RelationRolePersonne[0]?.Role?.NomRole);
        for(var i = 1; i < RelationRolePersonne?.Count; i++)
        {
            sb.Append(", ");
            sb.Append(RelationRolePersonne[i]?.Role?.NomRole);
        }
        return sb.ToString();
        }
    }
}