using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voam.Server.Contracts;
using Voam.Server.Models;

namespace Voam.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly VoamDbContext context;

        public ProductService(VoamDbContext _context)
        {
            context = _context;
        }

        //Implement all methods from the controller here
        //All methods must to be async!!!
    }
}
