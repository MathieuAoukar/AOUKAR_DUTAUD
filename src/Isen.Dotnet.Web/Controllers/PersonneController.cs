using System;
using System.Linq;
using Isen.Dotnet.Library.Context;
using Isen.Dotnet.Library.Model;
using Isen.Dotnet.Library.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Isen.Dotnet.Web.Controllers
{
    public class PersonneController : BaseController<Personne>

    {
        public PersonneController(
            ILogger<PersonneController> logger,
            ApplicationDbContext context) : base(logger, context)
        {
        }               


        // Exemple d'override de la query : liste les personnes

        protected override IQueryable<Personne> BaseQuery() =>

            base.BaseQuery()
                // Inclure BirthCity lors d'une requête faite sur une ville
                .Include(p => p.Date_anniversaire)
                .Include(p => p.TypeService)
                .Include(p => p.TypeRole)
                // Filtrer sur les villes qui commencent par Toul
                //.Where(p => p.BirthCity.StartsWith("Toul"))
                // Trier par ordre alpha des villes
                .OrderBy(p => p.TypeRole.NomRole);

    }

}