using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;


namespace PL
{
    public class UserInterface
    {
        private readonly BinaryTreePresenter _binaryTreePresenter;
        private readonly CollectionsWorker _collectionsWorker;

        public UserInterface(BinaryTreePresenter binaryTreePresenter, CollectionsWorker collectionsWorker)
        {
            _binaryTreePresenter = binaryTreePresenter;
            _collectionsWorker = collectionsWorker;
        }

        public void Start()
        {
            ConsoleWorker.WriteItem("Демонстрація основних операцій з колекціями та масивом:\n");
            ConsoleWorker.WriteItem("Наразі вони пусті, тому додамо один елемент:\n", ConsoleColor.Green);
            _collectionsWorker.FillAllCollections(1);
            ShowAllCollections();
            ConsoleWorker.ReadKey();

            ConsoleWorker.WriteItem("\nДодамо ще 4 елементи:\n", ConsoleColor.Green);
            _collectionsWorker.FillAllCollections(4);
            ShowAllCollections();
            ConsoleWorker.ReadKey();

            ConsoleWorker.WriteItem("\nОновимо списки та масив (сортування):\n", ConsoleColor.Green);
            _collectionsWorker.RefreshAll();
            ShowAllCollections();
            ConsoleWorker.ReadKey();

            ConsoleWorker.WriteItem("\nВидалимо останній елемент:\n", ConsoleColor.Green);
            _collectionsWorker.DeleteLastInAllCollections();
            ShowAllCollections();
            ConsoleWorker.ReadKey();

            ConsoleWorker.WriteItem("\nПошук заданого елементу в звичайному масиві:\n", ConsoleColor.Green);
            ConsoleWorker.WriteItem(_collectionsWorker.FindInOrdinaryArray());
            ConsoleWorker.ReadKey();

            ConsoleWorker.WriteItem("\nПрохід по спискам та масиву відбувався раніше.\n", ConsoleColor.Green);
            ConsoleWorker.ReadKey();

            ConsoleWorker.WriteItem("Демонстрація роботи з бінарним деревом:\n");
            ConsoleWorker.WriteItem("Заповнимо дерево та виведемо його (прохід по дереву відбувається в зворотньому порядку):", ConsoleColor.Green);
            _collectionsWorker.FillBinaryTree(5);
            ConsoleWorker.WriteItem(_collectionsWorker.GetBinaryTree());

            ConsoleWorker.ReadKey();

            RunForm();
        }

        public void ShowAllCollections()
        {
            ConsoleWorker.WriteItem("Не узагальнена колекція:", ConsoleColor.Yellow);
            ConsoleWorker.WriteItem(_collectionsWorker.GetNonGenericCollection());
            ConsoleWorker.WriteItem("Узагальнена колекція:", ConsoleColor.Yellow);
            ConsoleWorker.WriteItem(_collectionsWorker.GetGenericCollection());
            ConsoleWorker.WriteItem("Звичайний масив:", ConsoleColor.Yellow);
            ConsoleWorker.WriteItem(_collectionsWorker.GetOrdinaryArray());
        }

        public async void RunForm()
        {
            var binaryTreePresenter = _binaryTreePresenter;

            await Task.Run(() =>
            {
                Application.Run(binaryTreePresenter);
            });
        }
    }
}
