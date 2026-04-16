using Sustavzapracenjenapretkauteretani.Models;
using System.Collections.Generic;
using System.Linq;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Repositories
{
    public class TreningVjezbaRepository
    {
        private readonly List<Korisnik> _korisnici;

        public TreningVjezbaRepository(List<Korisnik> korisnici)
        {
            _korisnici = korisnici;
        }

        public IEnumerable<TreningVjezba> GetAll()
        {
            return _korisnici.SelectMany(k => k.Treninzi).SelectMany(t => t.TreningVjezbe);
        }

        public TreningVjezba? GetById(int id)
        {
            return _korisnici.SelectMany(k => k.Treninzi).SelectMany(t => t.TreningVjezbe).FirstOrDefault(tv => tv.Id == id);
        }
    }
}
