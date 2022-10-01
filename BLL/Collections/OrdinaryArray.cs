using DAL;
using System;
using System.Collections;
using System.Linq;

namespace BLL
{
    public class OrdinaryArray : IMyCollection, IRefresh
    {
        private Rectangle[] _rectangles;

        public OrdinaryArray()
        {
            _rectangles = Array.Empty<Rectangle>();
        }

        public void Add(Rectangle rectangle)
        {
            _rectangles = _rectangles.Append(rectangle).ToArray();
        }

        public void Delete()
        {
            _rectangles = _rectangles.Take(_rectangles.Length - 1).ToArray();
        }

        public void Refresh()
        {
            Array.Sort(_rectangles);
        }

        public Rectangle Find(Rectangle rectangle)
        {
            var temp = Array.IndexOf(_rectangles, rectangle);
            return temp == -1 ? null : _rectangles[temp];
        }

        public IEnumerator GetEnumerator()
        {
            return new OrdinaryArrayEnumerator(this);
        }

        private class OrdinaryArrayEnumerator : IEnumerator
        {
            private readonly OrdinaryArray _self;
            private int _currentIndex = -1;
            
            public OrdinaryArrayEnumerator(OrdinaryArray self)
            {
                _self = self;
            }

            public bool MoveNext()
            {
                if (_currentIndex >= _self._rectangles.Length - 1) return false;
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
                    if (_currentIndex == -1 || _currentIndex > _self._rectangles.Length)
                    {
                        throw new ArgumentException();
                    }
                    return _self._rectangles[_currentIndex];
                }
            }
        }
    }
}
