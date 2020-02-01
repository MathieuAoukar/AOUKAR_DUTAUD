using System.Text;

namespace Isen.Dotnet.Library.Model
{
    public class Role : BaseEntity
    {
        public string NomRole {get;set;}
        public Personne Personne {get;set;}
        public int? PersonneId {get;set;}

    }
}