using Sustavzapracenjenapretkauteretani.Models;
using System.Collections.Generic;
using System.Linq;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Repositories
{
    public class MjerenjeRepository
    {
        private readonly List<Korisnik> _korisnici;

        public MjerenjeRepository(List<Korisnik> korisnici)
        {
            _korisnici = korisnici;
        }

        public IEnumerable<Mjerenje> GetAll()
        {
            return _korisnici.SelectMany(k => k.Mjerenja);
        }

        public Mjerenje? GetById(int id)
        {
            return _korisnici.SelectMany(k => k.Mjerenja).FirstOrDefault(m => m.Id == id);
        }
    }
}
