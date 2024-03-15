namespace AppUnipsico.Models
{
    public class Paginacao<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public Paginacao(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage {
            get {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage {
            get {
                return (PageIndex < TotalPages);
            }
        }

        public static Paginacao<T> CreateAsync(List<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count;
            int skip = (pageIndex - 1) * pageSize;
            List<T> items = source.Skip(skip).Take(pageSize).ToList();
            return new Paginacao<T>(items, count, pageIndex, pageSize);
        }
    }
}
