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
        private readonly Rectangle _rectangleToFind;


        public CollectionsWorker(NonGenericCollection nonGenericCollection, GenericCollection genericCollection, OrdinaryArray ordinaryArray, BinaryTree<Rectangle> binaryTree)
        {
            _nonGenericCollection = nonGenericCollection;
            _genericCollection = genericCollection;
            _ordinaryArray = ordinaryArray;
            _binaryTree = binaryTree;
            _rectangleToFind = new Rectangle("#FFAB34", "#123456", 10, 5.3);
        }

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

        public string FindInNonGenericCollection()
        {
            return Find(_nonGenericCollection);
        }

        public string FindInGenericCollection()
        {
            return Find(_genericCollection);
        }

        public string FindInOrdinaryArray()
        {
            return Find(_ordinaryArray);
        }

        public string FindInBinaryTree()
        {
            return _binaryTree.Find(_rectangleToFind)?.ToString() ?? "Елемент не знайдено";
        }

        private string Find(IMyCollection collection)
        {
            return collection.Find(_rectangleToFind)?.ToString() ?? "Елемент не знайдено";
        }
    }
}
