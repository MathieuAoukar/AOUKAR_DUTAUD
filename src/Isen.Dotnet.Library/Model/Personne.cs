using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped] // afin de ne pas générer ce champ dans la bdd
        public int? Age => Date_anniversaire.HasValue ?
            // pour connaitre le nombre de jours entre aujourd'hui et le jours de naissance
            (int)((DateTime.Now - Date_anniversaire.Value).TotalDays / 365) :
            // si nous n'avons pas de date de naissance alors: un entier null
            new int?();
        
        public override string ToString() =>
            $"{Nom} {Prenom} | {Date_anniversaire} ({Age}) | {TypeRole} / {TypeService}";
        
    }

}