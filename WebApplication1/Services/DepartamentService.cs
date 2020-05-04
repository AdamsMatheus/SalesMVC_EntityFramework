using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

namespace SalesWebMvc.Services
{
    public class DepartamentService
    {
        private readonly WebApplication1Context _context;

        public DepartamentService(WebApplication1Context context)
        {
            _context = context;
        }

        public List<Departament> FindAll()
        {
            return _context.Departament.OrderBy(x => x.Name).ToList();
        }
    }
}