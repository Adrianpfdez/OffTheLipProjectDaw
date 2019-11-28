using OffTheLipProject.Migrations;
using OffTheLipProject.Models.ModelOTL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models
{
    public class Documentary 
    {
        public Documentary()
        {
            Surfers = new HashSet<Surfer>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string UrlRedirect { get; set; }
        public int Like { get; set; }
        public virtual ICollection<Surfer> Surfers { get; set; }
        public virtual ICollection<CommentDocumentary> CommentsDocumentary { get; set; }
    }

    public class DocumentaryCollection : IEnumerable
    {
        private Documentary[] _Documentary;
        public DocumentaryCollection(Documentary[] pArray)
        {
            _Documentary = new Documentary[pArray.Length];
            for (int i = 0; i < pArray.Length; i++)
            {
                _Documentary[i] = pArray[i];
            }
        }
        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public CollectionEnum GetEnumerator()
        {
            return new CollectionEnum(_Documentary);
        }
    }

    public class CollectionEnum : IEnumerator
    {
        public Documentary[] _Documentary;
        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        public CollectionEnum(Documentary[] list)
        {
            _Documentary = list;
        }
        public bool MoveNext()
        {
            position++;
            return (position < _Documentary.Length);
        }
        public void Reset()
        {
            position = -1;
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public Documentary Current
        {
            get
            {
                try
                {
                    return _Documentary[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}

