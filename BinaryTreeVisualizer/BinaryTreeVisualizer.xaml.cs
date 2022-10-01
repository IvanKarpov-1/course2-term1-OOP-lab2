using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BinaryTreeVisualizer
{
    /// <summary>
    /// Логика взаимодействия для BinaryTreePresenter.xaml
    /// </summary>
    public partial class BinaryTreePresenter : UserControl
    {
        public BinaryTreePresenter()
        {
            InitializeComponent();
            LeftHost.DataContextChanged += OnDataContextChanged;
            RightHost.DataContextChanged += OnDataContextChanged;
        }

        void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var host = (Decorator)sender;
            if (host.DataContext is IBinaryTree)
                host.Child ??= new BinaryTreePresenter();
            else
                host.Child = null;
        }
    }
}
