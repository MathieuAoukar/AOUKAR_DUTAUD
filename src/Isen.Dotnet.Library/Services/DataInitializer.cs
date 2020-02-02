using System;
using System.Collections.Generic;
using System.Linq;
using Isen.Dotnet.Library.Context;
using Isen.Dotnet.Library.Model;
using Microsoft.Extensions.Logging;

namespace Isen.Dotnet.Library.Services
{
    public class DataInitializer : IDataInitializer
    {
        private List<string> _firstNames => new List<string>
        {
            "Sang", 
            "Anne",
            "Boris",
            "Pierre",
            "Laura",
            "Hadrien",
            "Camille",
            "Louis",
            "Alicia"
        };

        private List<string> _lastNames => new List<string>
        {
            "Schuck",
            "Arbousset",
            "Lopasso",
            "Jubert",
            "Lebrun",
            "Dutaud",
            "Sarrazin",
            "Vu Dinh",
            "Aoukar"
        };

        private List<string> _atMail => new List<string>
        {
            "gmail.com",
            "caramail.com",
            "hotmail.fr",
            "isen.yncrea.fr",
            "live.com",
            "soprasteria.com",
            "yncrea.fr",
            "yahoo.com"
        };

        // Générateur aléatoire
        private readonly Random _random;

        // DI de ApplicationDbContext
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(
            ILogger<DataInitializer> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _random = new Random();
        }

        // Générateur de prénom
        private string RandomFirstName => 
            _firstNames[_random.Next(_firstNames.Count)];

        // Générateur de nom
        private string RandomLastName => 
            _lastNames[_random.Next(_lastNames.Count)];
    
        private Service RandomService
        {
            get
            {
                var services = _context.ServiceCollection.ToList();
                return services[_random.Next(services.Count)];

           }
        }

        private List<Role> RandomRoleSequence
        {
            get
            {
                var roles = _context.RoleCollection.ToList();
                roles.OrderBy(x => _random.Next()).ToList();
                List<Role> roleSequence = roles.OrderBy(x => _random.Next()).ToList().GetRange(0, _random.Next(roles.Count-1)+1);
                return roleSequence;
            }
        }

        private string RandomPhoneNumber => "06" + _random.Next(10000000, 99999999);
        private string RandomMail =>  _atMail[_random.Next(_atMail.Count)];

        // Générateur de date
        private DateTime RandomDate =>
            new DateTime(_random.Next(1980, 2010), 1, 1)
                .AddDays(_random.Next(0, 365));
        // Générateur de personne
        private Personne RandomPerson
        {
            get
            {
                var firstName = RandomFirstName;
                var lastName = RandomLastName;
                var person =  new Personne() {
                    Prenom = firstName,
                    Nom = lastName,
                    Date_anniversaire = RandomDate,
                    Telephone = RandomPhoneNumber,
                    Mail = $"{firstName}.{lastName}@{RandomMail}",
                    TypeService = RandomService,
                    RelationRolePersonne = new MyCollection<RelationRolePersonne>()
                };
                
                var roles = RandomRoleSequence;
                foreach (var role in roles)
                {
                    var rolePerson = new RelationRolePersonne() { 
                        Personne = person,
                        Role = role
                    };
                    person.RelationRolePersonne.Add(rolePerson);
                    role.RelationRolePersonne.Add(rolePerson);
                }
                
                return person;
            }
        }
        // Générateur de personnes
        public List<Personne> GetPersonnes(int size)
        {
            var persons = new List<Personne>();
            for(var i = 0 ; i < size ; i++)
            {
                persons.Add(RandomPerson);
            }
            return persons;
        }
        public List<Service> GetServices()
        {
            return new List<Service>
            {
                new Service { NomService = "Marketing"},
                new Service { NomService = "Production"},
                new Service { NomService = "Technique"},
                new Service { NomService = "Direction"},
                new Service { NomService = "Ressources humaines"},
                new Service { NomService = "Recherche"},
                new Service { NomService = "Design"}
            };
        }

        public List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { NomRole = "Utilisateur", RelationRolePersonne = new MyCollection<RelationRolePersonne>() },
                new Role { NomRole = "Administrateur", RelationRolePersonne = new MyCollection<RelationRolePersonne>() },
                new Role { NomRole = "Manager", RelationRolePersonne = new MyCollection<RelationRolePersonne>() },
                new Role { NomRole = "Invité", RelationRolePersonne = new MyCollection<RelationRolePersonne>() }
            };
        }

        public void DropDatabase()
        {
            _logger.LogWarning("Dropping database");
            _context.Database.EnsureDeleted();
        }
            

        public void CreateDatabase()
        {
            _logger.LogWarning("Creating database");
            _context.Database.EnsureCreated();
        }

        public void AddPersonnes()
        {
            _logger.LogWarning("Adding persons...");
            // S'il y a déjà des personnes dans la base -> ne rien faire
            if (_context.PersonneCollection.Any()) return;
            // Générer des personnes
            var persons = GetPersonnes(50);
            // Les ajouter au contexte
            _context.AddRange(persons);
            // Sauvegarder le contexte
            _context.SaveChanges();
        }

        public void AddServices()
        {
            _logger.LogWarning("Adding services...");
            if (_context.ServiceCollection.Any()) return;
            var services = GetServices();
            _context.AddRange(services);
            _context.SaveChanges();
        }

        public void AddRoles()
        {
            _logger.LogWarning("Adding roles...");
            if (_context.RoleCollection.Any()) return;
            var roles = GetRoles();
            _context.AddRange(roles);
            _context.SaveChanges();
        }
    }
}