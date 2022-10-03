using DAL;
using System.Collections;

namespace BLL
{
    public class CollectionsWorker
    {
        private readonly NonGenericCollection _nonGenericCollection;
        private readonly GenericCollection _genericCollection;
        private readonly OrdinaryArray _ordinaryArray;
        private readonly BinaryTree<Rectangle> _binaryTree;
        private Rectangle _rectangleToFind;


        public CollectionsWorker(NonGenericCollection nonGenericCollection, GenericCollection genericCollection, OrdinaryArray ordinaryArray, BinaryTree<Rectangle> binaryTree)
        {
            _nonGenericCollection = nonGenericCollection;
            _genericCollection = genericCollection;
            _ordinaryArray = ordinaryArray;
            _binaryTree = binaryTree;
            _rectangleToFind = new Rectangle("#FFAB34", "#123456", 10, 5.3);
        }

        #region Get

        public string GetNonGenericCollection()
        {
            return GetCollection(_nonGenericCollection);
        }

        public string GetGenericCollection()
        {
            return GetCollection(_genericCollection);
        }

        public string GetOrdinaryArray()
        {
            return GetCollection(_ordinaryArray);
        }

        public string GetBinaryTree()
        {
            return GetCollection(_binaryTree);
        }

        private string GetCollection(IEnumerable myCollection)
        {
            var str = "";

            foreach (var element in myCollection)
            {
                str += $"{element}\n";
            }

            return str;
        }

        #endregion

        #region Fill

        public void FillNonGenericCollection(int countOfElementsToFilling = -1)
        {
            Fill(_nonGenericCollection, countOfElementsToFilling);
        }

        public void FillGenericCollection(int countOfElementsToFilling = -1)
        {
            Fill(_genericCollection, countOfElementsToFilling);
        }

        public void FillOrdinaryArray(int countOfElementsToFilling = -1)
        {
            Fill(_ordinaryArray, countOfElementsToFilling);
        }

        public void FillBinaryTree(int countOfElementsToFilling = -1)
        {
            if (countOfElementsToFilling == -1) countOfElementsToFilling = 5;

            for (var i = 0; i < countOfElementsToFilling; i++)
            {
                _binaryTree.Add(new Rectangle());
            }
        }

        public void FillAllCollections(int countOfElementsToFilling = -1)
        {
            Fill(_nonGenericCollection, countOfElementsToFilling);
            Fill(_genericCollection, countOfElementsToFilling);
            Fill(_ordinaryArray, countOfElementsToFilling);
        }

        private void Fill(IMyCollection myCollection, int countOfElementsToFilling = -1)
        {
            if (countOfElementsToFilling == -1) countOfElementsToFilling = 5;

            for (var i = 0; i < countOfElementsToFilling; i++)
            {
                myCollection.Add(new Rectangle());
            }
        }

        #endregion

        #region Delete

        public void DeleteInNonGenericCollectionAt(int index)
        {
            DeleteAt(_nonGenericCollection, index);
        }

        public void DeleteInGenericCollectionAt(int index)
        {
            DeleteAt(_genericCollection, index);
        }

        public void DeleteInOrdinaryArrayAt(int index)
        {
            DeleteAt(_ordinaryArray, index);
        }

        private void DeleteAt(IMyCollection collection, int index)
        {
            collection.DeleteAt(index);
        }

        public void DeleteLastInNonGenericCollection()
        {
            DeleteLast(_nonGenericCollection);
        }

        public void DeleteLastInGenericCollection()
        {
            DeleteLast(_genericCollection);
        }

        public void DeleteLastInOrdinaryArray()
        {
            DeleteLast(_ordinaryArray);
        }

        public void DeleteLastInAllCollections()
        {
            DeleteLast(_nonGenericCollection);
            DeleteLast(_genericCollection);
            DeleteLast(_ordinaryArray);
        }

        private void DeleteLast(IMyCollection myCollection)
        {
            myCollection.Delete();
        }

        public void DeleteInTree()
        {
            _binaryTree.Delete();
        }

        #endregion

        #region Refresh

        public void RefreshAll()
        {
            Refresh(_nonGenericCollection);
            Refresh(_genericCollection);
            Refresh(_ordinaryArray);
        }

        private void Refresh(IRefresh myCollection)
        {
            myCollection.Refresh();
        }

        #endregion

        #region Find

        public string FindInNonGenericCollection(Rectangle rectangleToFind = null)
        {
            return Find(_nonGenericCollection, rectangleToFind);
        }

        public string FindInGenericCollection(Rectangle rectangleToFind = null)
        {
            return Find(_genericCollection, rectangleToFind);
        }

        public string FindInOrdinaryArray(Rectangle rectangleToFind = null)
        {
            return Find(_ordinaryArray, rectangleToFind);
        }

        public string FindInBinaryTree(Rectangle rectangleToFind = null)
        {
            _rectangleToFind = rectangleToFind ?? _rectangleToFind;
            return _binaryTree.Find(_rectangleToFind)?.ToString() ?? "Елемент не знайдено";
        }

        private string Find(IMyCollection collection, Rectangle rectangleToFind = null)
        {
            _rectangleToFind = rectangleToFind ?? _rectangleToFind;
            return collection.Find(_rectangleToFind)?.ToString() ?? "Елемент не знайдено";
        }

        #endregion
    }
}
