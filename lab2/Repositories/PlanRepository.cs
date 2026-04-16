using Sustavzapracenjenapretkauteretani.Models;
using System.Collections.Generic;
using System.Linq;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Repositories
{
    public class PlanRepository
    {
        private readonly List<Plan> _planovi;

        public PlanRepository(List<Plan> planovi)
        {
            _planovi = planovi;
        }

        public IEnumerable<Plan> GetAll()
        {
            return _planovi;
        }

        public Plan? GetById(int id)
        {
            return _planovi.FirstOrDefault(p => p.Id == id);
        }
    }
}
