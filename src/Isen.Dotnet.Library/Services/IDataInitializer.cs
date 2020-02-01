  
using System.Collections.Generic;
using Isen.Dotnet.Library.Model;

namespace Isen.Dotnet.Library.Services
{
    public interface IDataInitializer
    {
         List<Personne> GetPersonnes(int size);
         void DropDatabase();
         void CreateDatabase();
         void AddPersonnes();
         void AddRoles();
         void AddServices();
    }
}