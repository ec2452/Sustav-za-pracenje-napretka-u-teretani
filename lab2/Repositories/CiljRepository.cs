using Sustavzapracenjenapretkauteretani.Models;
using System.Collections.Generic;
using System.Linq;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Repositories
{
    public class CiljRepository
    {
        private readonly List<Korisnik> _korisnici;

        public CiljRepository(List<Korisnik> korisnici)
        {
            _korisnici = korisnici;
        }

        public IEnumerable<Cilj> GetAll()
        {
            return _korisnici.SelectMany(k => k.Ciljevi);
        }

        public Cilj? GetById(int id)
        {
            return _korisnici.SelectMany(k => k.Ciljevi).FirstOrDefault(c => c.Id == id);
        }
    }
}
