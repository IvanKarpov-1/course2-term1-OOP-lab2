using System;

namespace BLL
{
    public enum Side
    {
        Left,
        Right
    }

    public class TreeNode <T> where T : IComparable<T>, IComparable
    {
        public T Data { get; set; }
        public TreeNode<T> ParentNode { get;  set; }
        public TreeNode<T> LeftNode { get;  set; }
        public TreeNode<T> RightNode { get;  set; }

        public TreeNode(T data)
        {
            Data = data;
        }

        public Side? NodeSide
        {
            get
            {
                if (ParentNode == null)
                    return (Side?)null;
                return ParentNode.LeftNode == this ? Side.Left : Side.Right;
            }
        }
    }
}
