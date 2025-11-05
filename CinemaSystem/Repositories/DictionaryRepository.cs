namespace CinemaSystem.Repositories
{
   
   

    public class DictionaryRepository<T>  where T : Entity
    {
        //private ApplicationDbContext _context = new();

        public Dictionary<Func<T, object>, object> ToDictionary(
         IEnumerable<Func<T, object>> selectors,
         T item
 )
        {
            var result = selectors.ToDictionary(
                selector => selector,
                selector => selector(item)
            );

            return result;
        }

        public List<Dictionary<Func<T, object>, object>> ToDictionaryList(
           IEnumerable<Func<T, object>> selectors,
           IEnumerable<T> items)
        {
            return items.Select(item => ToDictionary(selectors, item)).ToList();
        }
        public Dictionary<int, T> ToIdDictionary(IEnumerable<T> items)
        {
            return items.ToDictionary(item => item.EId);
        }

    }


}