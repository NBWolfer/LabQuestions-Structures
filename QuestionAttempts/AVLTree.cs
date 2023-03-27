using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAttempts
{
    public class AVLNode
    {
        public int value;
        public int height;
        public AVLNode left;
        public AVLNode right;

        public AVLNode(int value)
        {
            this.value = value;
            this.height = 1;
            this.left = null;
            this.right = null;
        }
    }

    public class AVLTree
    {
        private AVLNode root;

        public AVLTree()
        {
            this.root = null;
        }

        public AVLNode Insert(AVLNode node, int value)
        {
            if (node == null)
            {
                return new AVLNode(value);
            }

            if (value < node.value)
            {
                node.left = Insert(node.left, value);
            }
            else
            {
                node.right = Insert(node.right, value);
            }

            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

            int balance = GetBalance(node);

            if (balance > 1 && value < node.left.value)
            {
                return RotateRight(node);
            }

            if (balance > 1 && value > node.left.value)
            {
                node.left = RotateLeft(node.left);
                return RotateRight(node);
            }

            if (balance < -1 && value > node.right.value)
            {
                return RotateLeft(node);
            }

            if (balance < -1 && value < node.right.value)
            {
                node.right = RotateRight(node.right);
                return RotateLeft(node);
            }

            return node;
        }

        public AVLNode Delete(AVLNode node, int value)
        {
            if (node == null)
            {
                return node;
            }

            if (value < node.value)
            {
                node.left = Delete(node.left, value);
            }
            else if (value > node.value)
            {
                node.right = Delete(node.right, value);
            }
            else
            {
                if ((node.left == null) || (node.right == null))
                {
                    AVLNode temp = null;
                    if (temp == node.left)
                    {
                        temp = node.right;
                    }
                    else
                    {
                        temp = node.left;
                    }

                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else
                    {
                        node = temp;
                    }
                }
                else
                {
                    AVLNode temp = MinValueNode(node.right);
                    node.value = temp.value;
                    node.right = Delete(node.right, temp.value);
                }
            }

            if (node == null)
            {
                return node;
            }

            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

            int balance = GetBalance(node);

            if (balance > 1 && GetBalance(node.left) >= 0)
            {
                return RotateRight(node);
            }

            if (balance > 1 && GetBalance(node.left) < 0)
            {
                node.left = RotateLeft(node.left);
                return RotateRight(node);
            }

            if (balance < -1 && GetBalance(node.right) <= 0)
            {
                return RotateLeft(node);
            }

            if (balance < -1 && GetBalance(node.right) > 0)
            {
                node.right = RotateRight(node.right);
                return RotateLeft(node);
            }

            return node;
        }

        public AVLNode Search(int value)
        {
            AVLNode current = root;
            while (current != null)
            {
                if (value == current.value)
                {
                    return current;
                }
                else if (value < current.value)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }

            return null;
        }

        private AVLNode MinValueNode(AVLNode node)
        {
            AVLNode current = node;

            while (current.left != null)
            {
                current = current.left;
            }

            return current;
        }

        private int GetHeight(AVLNode node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.height;
        }

        private int GetBalance(AVLNode node)
        {
            if (node == null)
            {
                return 0;
            }

            return GetHeight(node.left) - GetHeight(node.right);
        }

        private AVLNode RotateRight(AVLNode node)
        {
            AVLNode newRoot = node.left;
            AVLNode temp = newRoot.right;

            newRoot.right = node;
            node.left = temp;

            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
            newRoot.height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));

            return newRoot;
        }

        private AVLNode RotateLeft(AVLNode node)
        {
            AVLNode newRoot = node.right;
            AVLNode temp = newRoot.left;

            newRoot.left = node;
            node.right = temp;

            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
            newRoot.height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));

            return newRoot;
        }

    }
}
