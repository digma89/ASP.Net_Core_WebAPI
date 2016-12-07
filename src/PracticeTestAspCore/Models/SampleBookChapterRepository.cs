using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeTestAspCore.Models
{

    public class SampleBookChapterRepository : IBookChaptersRepository
    {
        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters =
          new ConcurrentDictionary<Guid, BookChapter>();

        public void init()
        {
            Add(new BookChapter
            {
                Number = 1,
                Title = "C# Programming",
                Pages = 1223
            });

            Add(new BookChapter
            {
                Number = 2,
                Title = ".NET Core 1.0",
                Pages = 145
            });
        }

        public void Add(BookChapter bookChapter)
        {
            bookChapter.Id = Guid.NewGuid();
            _chapters[bookChapter.Id] = bookChapter;
        }

        public BookChapter Find(Guid id)
        {
            BookChapter chapter;
            _chapters.TryGetValue(id, out chapter);
            return chapter;
        }

        public IEnumerable<BookChapter> GetAll() => _chapters.Values;

        public BookChapter Remove(Guid id)
        {
            BookChapter removed;
            _chapters.TryRemove(id, out removed);
            return removed;
        }

        public void Update(BookChapter bookChapter) => _chapters[bookChapter.Id] = bookChapter;
    }
}
