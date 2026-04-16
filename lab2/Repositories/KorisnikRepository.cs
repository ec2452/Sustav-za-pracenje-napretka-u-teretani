using Sustavzapracenjenapretkauteretani.Models;
using System.Collections.Generic;
using System.Linq;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Repositories
{
    public class KorisnikRepository
    {
        private readonly List<Korisnik> _korisnici;

        public KorisnikRepository(List<Korisnik> korisnici)
        {
            _korisnici = korisnici;
        }

        public IEnumerable<Korisnik> GetAll()
        {
            return _korisnici;
        }

        public Korisnik? GetById(int id)
        {
            return _korisnici.FirstOrDefault(k => k.Id == id);
        }
    }
}
