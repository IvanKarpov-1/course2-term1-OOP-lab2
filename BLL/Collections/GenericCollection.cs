using DAL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL
{
    public class GenericCollection : IEnumerable<Rectangle>, IMyCollection, IRefresh
    {
        private readonly List<Rectangle> _rectangles = new List<Rectangle>();

        public void Add(Rectangle rectangle)
        {
            _rectangles.Add(rectangle);
        }

        public void Delete()
        {
            _rectangles.RemoveAt(_rectangles.Count - 1);
        }

        public void Delete(int index)
        {
            if (index >= _rectangles.Count) return;

            _rectangles.RemoveAt(index);
        }
        public void Refresh()
        {
            _rectangles.Sort();
        }

        public Rectangle Find(Rectangle rectangle)
        {
            return _rectangles.Find(x => x.Equals(rectangle));
        }

        public IEnumerator<Rectangle> GetEnumerator()
        {
            return new GenericCollectionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class GenericCollectionEnumerator : IEnumerator<Rectangle>
        {
            private readonly GenericCollection _self;
            private int _currentIndex = -1;

            public GenericCollectionEnumerator(GenericCollection self)
            {
                _self = self;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_currentIndex >= _self._rectangles.Count - 1) return false;
                _currentIndex++;
                return true;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            public Rectangle Current
            {
                get
                {
                    if (_currentIndex == -1 || _currentIndex > _self._rectangles.Count)
                    {
                        throw new ArgumentException();
                    }
                    return _self._rectangles[_currentIndex];
                }
            }

            object IEnumerator.Current => Current;
        }
    }
}
