using DAL;
using System;
using System.Collections;

namespace BLL
{
    public class NonGenericCollection : IMyCollection, IRefresh
    {
        private readonly ArrayList _rectangles = new ArrayList();

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
            var temp = _rectangles.IndexOf(rectangle);
            return temp == -1 ? null : (Rectangle)_rectangles[temp];
        }

        public IEnumerator GetEnumerator()
        {
            return new NonGenericCollectionEnumerator(this);
        }

        private class NonGenericCollectionEnumerator : IEnumerator
        {
            private readonly NonGenericCollection _self;
            private int _currentIndex = -1;

            public NonGenericCollectionEnumerator(NonGenericCollection self)
            {
                _self = self;
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

            public object Current
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
        }
    }
}
