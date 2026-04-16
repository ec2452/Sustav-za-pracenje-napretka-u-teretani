using Sustavzapracenjenapretkauteretani.Models;
using System.Collections.Generic;
using System.Linq;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Repositories
{
    public class VjezbaRepository
    {
        private readonly List<Vjezba> _vjezbe;

        public VjezbaRepository(List<Vjezba> vjezbe)
        {
            _vjezbe = vjezbe;
        }

        public IEnumerable<Vjezba> GetAll()
        {
            return _vjezbe;
        }

        public Vjezba? GetById(int id)
        {
            return _vjezbe.FirstOrDefault(v => v.Id == id);
        }
    }
}
