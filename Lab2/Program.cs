using System;
using System.Text;
using BLL;
using PL;
using DAL;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            Console.InputEncoding = Encoding.Default;
            
            var nonGenericCollection = new NonGenericCollection();
            var genericCollection = new GenericCollection();
            var ordinaryArray = new OrdinaryArray();
            var binaryTree = new BinaryTree<Rectangle>();

            var collectionsWorker = new CollectionsWorker(nonGenericCollection, genericCollection, ordinaryArray, binaryTree);
            var binaryTreePresenter = new BinaryTreePresenter(binaryTree);
            var userInterface = new UserInterface(binaryTreePresenter, collectionsWorker);

            userInterface.Start();

            Console.ReadKey();
        }
    }
}
