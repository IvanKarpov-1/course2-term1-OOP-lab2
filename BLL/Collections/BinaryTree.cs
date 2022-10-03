using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL
{
    public class BinaryTree<T> : IEnumerable<T>, IMyCollection<T> where T : class, IComparable<T>, IComparable
    {
        public TreeNode<T> RootNode { get; private set; }
        public int Count { get; private set; } = 0;

        public void Add(T data)
        {
            Add(new TreeNode<T>(data));
        }

        private void Add(TreeNode<T> node, TreeNode<T> currentNode = null)
        {
            while (true)
            {
                if (RootNode == null)
                {
                    node.ParentNode = null;
                    RootNode = node;
                    break;
                }

                currentNode = currentNode ?? RootNode;
                node.ParentNode = currentNode;
                var result = node.Data.CompareTo(currentNode.Data);

                if (result < 0)
                    if (currentNode.LeftNode == null)
                    {
                        currentNode.LeftNode = node;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.LeftNode;
                        continue;
                    }

                if (currentNode.RightNode == null)
                {
                    currentNode.RightNode = node;
                    break;
                }
                currentNode = currentNode.RightNode;
            }

            Count++;
        }

        public T Find(T data)
        {
            return FindNode(data).Data;
        }

        public TreeNode<T> FindNode(T data, TreeNode<T> startWithNode = null)
        {
            while (true)
            {
                startWithNode = startWithNode ?? RootNode;
                var result = data.CompareTo(startWithNode.Data);

                if (result == 0) return startWithNode;
                
                if (result < 0)
                {
                    if (startWithNode.LeftNode == null) return null;

                    startWithNode = startWithNode.LeftNode;
                    continue;
                }

                if (startWithNode.RightNode == null) return null;

                startWithNode = startWithNode.RightNode;
            }
        }

        public void Delete()
        {
            Delete(RootNode);
        }

        public void Delete(T data)
        {
            var foundNode = FindNode(data);
            Delete(foundNode);
        }

        public void Delete(TreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            var currentNodeSide = node.NodeSide;

            if (node.ParentNode == null)
            {
                if (node.LeftNode != null && node.RightNode != null)
                {
                    var bufLeft = node.LeftNode;
                    var bufRightLeft = node.RightNode.LeftNode;
                    var bufRightRight = node.RightNode.RightNode;
                    node.Data = node.RightNode.Data;
                    node.RightNode = bufRightRight;
                    node.LeftNode = bufRightLeft;
                    Add(bufLeft, node);
                    Count--;
                }
                else if (node.LeftNode != null)
                {
                    var bufLeft = node.LeftNode;
                    node.LeftNode.ParentNode = null;
                    node.LeftNode = null;
                    RootNode = bufLeft;
                }
                else if (node.RightNode != null)
                {
                    var bufRight = node.RightNode;
                    node.RightNode.ParentNode = null;
                    node.RightNode = null;
                    RootNode = bufRight;
                }
                else
                {
                    RootNode = null;
                }
            }
            else if (node.LeftNode == null && node.RightNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = null;
                }
                else if (currentNodeSide == Side.Right)
                {
                    node.ParentNode.RightNode = null;
                }
                else
                {
                    RootNode = null;
                }
            }
            else if (node.LeftNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.RightNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.RightNode;
                }

                node.RightNode.ParentNode = node.ParentNode;
            }
            else if (node.RightNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.LeftNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.LeftNode;
                }

                node.LeftNode.ParentNode = node.ParentNode;
            }
            else
            {
                switch (currentNodeSide)
                {
                    case Side.Left:
                    {
                        node.ParentNode.LeftNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        Count--;
                        break;
                    }
                    case Side.Right:
                    {
                        node.ParentNode.RightNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        Count--;
                        break;
                    }
                    default:
                    {
                        var bufLeft = node.LeftNode;
                        var bufRightLeft = node.RightNode.LeftNode;
                        var bufRightRight = node.RightNode.RightNode;
                        node.Data = node.RightNode.Data;
                        node.RightNode = bufRightRight;
                        node.LeftNode = bufRightLeft;
                        Add(bufLeft, node);
                        Count--;
                        break;
                    }
                }
            }

            Count--;
        }

        public IEnumerable<T> PostOrder()
        {
            if (RootNode == null) yield break;

            var stack = new Stack<TreeNode<T>>();
            var node = RootNode;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.RightNode == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.RightNode;
                    }
                    else
                    {
                        yield return node.Data; 
                        node = null;
                    }
                }
                else
                {
                    if (node.RightNode != null) stack.Push(node.RightNode);
                    stack.Push(node);
                    node = node.LeftNode;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return PostOrder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
