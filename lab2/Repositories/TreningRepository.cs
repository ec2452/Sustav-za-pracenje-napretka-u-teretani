using Sustavzapracenjenapretkauteretani.Models;
using System.Collections.Generic;
using System.Linq;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Repositories
{
    public class TreningRepository
    {
        private readonly List<Korisnik> _korisnici;

        public TreningRepository(List<Korisnik> korisnici)
        {
            _korisnici = korisnici;
        }

        public IEnumerable<Trening> GetAll()
        {
            return _korisnici.SelectMany(k => k.Treninzi);
        }

        public Trening? GetById(int id)
        {
            return _korisnici.SelectMany(k => k.Treninzi).FirstOrDefault(t => t.Id == id);
        }
    }
}
