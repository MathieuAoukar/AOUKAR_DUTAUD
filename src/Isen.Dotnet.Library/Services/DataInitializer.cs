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
            "Vu Dinh"
        };

        private List<string> _mail => new List<string>
        {
            "gmail.com",
            "yahoo.com",
            "live.fr",
            "hotmailS.com",
            "orange.fr"
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
        private string RandomFirstName => _firstNames[_random.Next(_firstNames.Count)];
        // Générateur de nom
        private string RandomLastName => _lastNames[_random.Next(_lastNames.Count)];

        private string RandomTelephone => "06" + _random.Next(10000000, 99999999);

        private string RandomMail => _mail[_random.Next(_mail.Count)];
        
        // Générateur aléatoire de role
        private Role RandomRole
        {
            get
            {
                var roles = _context.RoleCollection.ToList();
                return roles[_random.Next(roles.Count)];
            }
        }

        // Générateur aléatoire de services
        private Service RandomService
        {
            get
            {
                var services = _context.ServiceCollection.ToList();
                return services[_random.Next(services.Count)];
            }
        }

        // Générateur aléatoire de dates
        private DateTime RandomDate =>
            new DateTime(_random.Next(1980, 2010), 1, 1)
                .AddDays(_random.Next(0, 365));

        // Afin de générer des personnes 
         private Personne RandomPersonne
        {
            get 
            {
                var prenom = RandomFirstName;
                var nom = RandomLastName;
                var personne = new Personne() {
                    Prenom = prenom,
                    Nom = nom,
                    Date_anniversaire = RandomDate,
                    Telephone = RandomTelephone,
                    Mail = $"{prenom}.{nom}@{RandomMail}",
                    TypeRole = RandomRole,
                    TypeService = RandomService
                };

                return personne;
            }
            
        }

        public List<Personne> GetPersonnes(int size)
        {
            var personnes = new List<Personne>();
            for(var i = 0 ; i < size ; i++)
            {
                personnes.Add(RandomPersonne);
            }
            return personnes;
        }

        public List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { NomRole = "Utilisateur" },
                new Role { NomRole = "Manager" },
                new Role { NomRole = "Administrateur" },
                new Role { NomRole = "PDG" },
                new Role { NomRole = "Technicien" },
                new Role { NomRole = "Ingenieur" },
                new Role { NomRole = "Client" }
            };
        }

        public List<Service> GetServices()
        {
            return new List<Service>
            {
                new Service { NomService = "Marketing" },
                new Service { NomService = "Ressources Humaines"},
                new Service { NomService = "Production"},
                new Service { NomService = "Management"},
                new Service { NomService = "Direction"},
                new Service { NomService = "Technique"},
                new Service { NomService = "Développement"}
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
            _logger.LogWarning("Adding personnes...");
            if (_context.PersonneCollection.Any()) return;   //on vérifie si la personne existe déjà dans la bdd
            var personnes = GetPersonnes(50);   // on génère cette personne
            _context.AddRange(personnes);   // On l'ajoute au context
            _context.SaveChanges(); // on sauvegarde
        }

        public void AddRoles()
        {
            _logger.LogWarning("Adding roles...");
            if (_context.RoleCollection.Any()) return;  
            var roles = GetRoles();
            _context.AddRange(roles);
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
    }
}