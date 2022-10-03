using System;
using System.Windows.Forms;
using BLL;
using DAL;


namespace PL
{
    public partial class BinaryTreePresenter : Form
    {
        private readonly TreePainter _treePainter;
        private readonly BinaryTree<Rectangle> _binaryTree;

        public BinaryTreePresenter(BinaryTree<Rectangle> binaryTree)
        {
            _treePainter = new TreePainter();
            _binaryTree = binaryTree;
            InitializeComponent();
        }

        private void PaintTree(object sender, EventArgs e)
        {
            PaintTree();
            ShowCount();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            var count = _binaryTree.Count;

            for (var i = 0; i < count; i++)
            {
                _binaryTree.Delete();
            }
            for (var i = 0; i < 5; i++)
            {
                _binaryTree.Add(new Rectangle());
            }

            PaintTree();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            _binaryTree.Delete();
            PaintTree();
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            _binaryTree.Add(new Rectangle());
            PaintTree();
        }

        private void PaintTree()
        {
            Image.Image = _treePainter.Draw(out _, _binaryTree.RootNode);
            Image.Refresh();
            ShowCount();
        }

        private void ShowCount()
        {
            count.Text = _binaryTree.Count.ToString();
        }
    }
}
