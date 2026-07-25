using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data_structure_404
{
    // nodes
    public enum Color
    {
        Red,
        Black
    }
    public class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }
    public class Node_BinaryTree
    {
        public int val;
        public Node_BinaryTree left;
        public Node_BinaryTree right;
        public int Height { get; set; } = 1;
        public Color color;
        public Node_BinaryTree parent; // just for red and black tree
        public Node_BinaryTree(int val)
        {
            this.val = val;
            left = null;
            right = null;
            Height = 1;
            this.color = Color.Red;
        }
    }

    class Node_DoublyLinkedList
    {
        public int Data;
        public Node_DoublyLinkedList Next;
        public Node_DoublyLinkedList Prev;
        public Node_DoublyLinkedList(int d)
        {
            Data = d;
            Next = null;
        }
    }

    // node for general tree
    public class GeneralNode
    {
        public int Data { get; set; }
        public List<GeneralNode> Children { get; set; } = new List<GeneralNode>();

        public GeneralNode(int data)
        {
            Data = data;
        }
    }

    // node for hashing
    class HashNode
    {
        public int key;
        public int value;
        public bool isDeleted;

        public HashNode(int key, int value)
        {
            this.key = key;
            this.value = value;
            this.isDeleted = false;
        }
    }

    // -------------------------------------------------------------------------------------------------
    // -------------------------------------------- Lessons --------------------------------------------
    // -------------------------------------------------------------------------------------------------

    // Node and LinkedList

    // az node aval code estefade shode
    public class LinkedList
    {
        private Node head;
        private Node tail;
        private int size;

        public LinkedList()
        {
            head = null;
            tail = null;
            size = 0;
        }

        //// in code az teta n hast va behine nist

        //public void Insert_last(int data)
        //{
        //    Node newNode = new Node(data);
        //    if (head == null)
        //    {
        //        head = newNode;
        //        return;
        //    }

        //    Node current = head;
        //    while (current.Next != null)
        //    {
        //        current = current.Next;
        //    }
        //    current.Next = newNode;
        //}
        public void Insert_last(int data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
                size++;
                return;
            }

            tail.Next = newNode;
            tail = newNode;
            size++;
        }

        public void Insert_first(int data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
                size++;
                return;
            }
            newNode.Next = head;
            head = newNode;
            size++;
        }

        public bool Insert_after(int key, int data)
        {
            Node current = FindNode(key);
            if (current == null)
                return false;

            Node newNode = new Node(data);
            newNode.Next = current.Next;
            current.Next = newNode;

            if (current == tail)
            {
                tail = newNode;
            }
            size++;
            return true;
        }

        public bool Delete(int data)
        {
            if (head == null)
                return false;

            if (head.Data == data)
            {
                head = head.Next;
                if (head == null)
                {
                    tail = null;
                }
                size--;
                return true;
            }

            Node current = head;
            while (current.Next != null)
            {
                if (current.Next.Data == data)
                {
                    current.Next = current.Next.Next;
                    if (current.Next == null)
                    {
                        tail = current;
                    }
                    size--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        //// in code az teta n hast va behine nist 

        //public bool Find(int data)
        //{
        //    Node current = head;
        //    while (current != null)
        //    {
        //        if (current.Data == data)
        //            return true;
        //        current = current.Next;
        //    }
        //    return false;
        //}

        public bool Find(int data)
        {
            return FindNode(data) != null;
        }

        public Node FindNode(int data)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data == data)
                    return current;
                current = current.Next;
            }
            return null;
        }

        public void Display()
        {
            Node current = head;
            Console.Write("list: ");
            while (current != null)
            {
                Console.Write(current.Data + " -> ");
                current = current.Next;
            }
            Console.WriteLine("null");
        }

        //// in code az teta n hast va behine nist

        //public int Get_size()
        //{
        //    int count = 0;
        //    Node current = head;
        //    while (current != null)
        //    {
        //        count++;
        //        current = current.Next;
        //    }
        //    return count;
        //}
        public int Get_size()
        {
            return size;
        }
    }

    // -------------------------------------------------------------------------------------------------------------------------
    //General conversion to binary

    // az node binary dar aval code estefade shode

    public class TreeConverter
    {
        public static Node_BinaryTree ConvertToBinary(GeneralNode genRoot)
        {
            if (genRoot == null)
                return null;

            var binNode = new Node_BinaryTree(genRoot.Data);

            if (genRoot.Children != null && genRoot.Children.Count > 0)
            {
                // اولین فرزند → left
                binNode.left = ConvertToBinary(genRoot.Children[0]);

                // بقیه فرزندان → زنجیره right
                Node_BinaryTree current = binNode.left;

                for (int i = 1; i < genRoot.Children.Count; i++)
                {
                    current.right = ConvertToBinary(genRoot.Children[i]);
                    current = current.right;
                }
            }

            return binNode;
        }
    }

        // -------------------------------------------------------------------------------------------------------------------------
        //traversal

        // az node binary dar aval code estefade shode
    public class Traversal
    {

        public static Node_BinaryTree BuildExampleTree()
        {
            Node_BinaryTree root = new Node_BinaryTree(10);
            root.left = new Node_BinaryTree(5);
            root.right = new Node_BinaryTree(15);
            root.left.left = new Node_BinaryTree(3);
            root.left.right = new Node_BinaryTree(7);
            root.right.left = new Node_BinaryTree(12);
            root.right.right = new Node_BinaryTree(18);
            return root;
        }
        public void PreOrder(Node_BinaryTree root)
        {
            if (root != null)
            {
                Console.Write(root.val + " ");
                PreOrder(root.left);
                PreOrder(root.right);
            }
        }

        public void InOrder(Node_BinaryTree root)
        {
            if (root != null)
            {
                InOrder(root.left);
                Console.Write(root.val + " ");
                InOrder(root.right);
            }
        }

        public void PostOrder(Node_BinaryTree root)
        {
            if (root != null)
            {
                PostOrder(root.left);
                PostOrder(root.right);
                Console.Write(root.val + " ");
            }
        }

        public void LevelOrder(Node_BinaryTree root)
        {
            if (root == null) return;

            Queue<Node_BinaryTree> queue = new Queue<Node_BinaryTree>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node_BinaryTree current = queue.Dequeue();
                Console.Write(current.val + " ");

                if (current.left != null)
                    queue.Enqueue(current.left);
                if (current.right != null)
                    queue.Enqueue(current.right);
            }
        }
    }

    // ----------------------------------------------------------------------------------------
    // BST

    // az node binary dar aval code va class traversal estefade shode
    public class Gfc_BST
    {
        public static Node_BinaryTree Search(Node_BinaryTree root, int key)
        {
            if (root == null || root.val == key)
                return root;

            if (key < root.val)
                return Search(root.left, key);
            else
                return Search(root.right, key);
        }

        public static Node_BinaryTree Insert(Node_BinaryTree root, int data)
        {
            if (root == null)
            {
                root = new Node_BinaryTree(data);
                return root;
            }

            if (data < root.val)
            {
                root.left = Insert(root.left, data);
            }
            else if (data > root.val)
            {
                root.right = Insert(root.right, data);
            }

            return root;
        }

        public static Node_BinaryTree Delete(Node_BinaryTree root, int data)
        {
            if (root == null)
                return root;

            if (data < root.val)
            {
                root.left = Delete(root.left, data);
            }
            else if (data > root.val)
            {
                root.right = Delete(root.right, data);
            }
            else
            {
                if (root.left == null)
                {
                    return root.right;
                }
                else if (root.right == null)
                {
                    return root.left;
                }

                Node_BinaryTree successor = FindMin(root.right);
                root.val = successor.val;
                root.right = Delete(root.right, successor.val);
            }
            return root;
        }

        public static Node_BinaryTree FindMin(Node_BinaryTree root)
        {
            while (root != null && root.left != null)
            {
                root = root.left;
            }
            return root;
        }


        public static Node_BinaryTree FindMax(Node_BinaryTree root)
        {
            while (root != null && root.right != null)
            {
                root = root.right;
            }
            return root;
        }

        public static int Floor(Node_BinaryTree root, int key)
        {
            int floorVal = int.MinValue;
            Node_BinaryTree current = root;
            while (current != null)
            {
                if (current.val == key)
                {
                    return key;
                }
                else if (current.val > key)
                {
                    current = current.left;
                }
                else
                {
                    floorVal = current.val;
                    current = current.right;
                }
            }
            return floorVal;
        }

        public static int Ceil(Node_BinaryTree root, int key)
        {
            int ceilVal = int.MaxValue;
            Node_BinaryTree current = root;
            while (current != null)
            {
                if (current.val == key)
                {
                    return key;
                }
                else if (current.val < key)
                {
                    current = current.right;
                }
                else
                {
                    ceilVal = current.val;
                    current = current.left;
                }
            }
            return ceilVal;
        }

        public static Node_BinaryTree BuildBST()
        {
            Node_BinaryTree root = null;
            int[] values = { 10, 5, 15, 3, 7, 12, 18 };
            foreach (int val in values)
            {
                root = Insert(root, val);
            }
            return root;
        }
    }

    // -----------------------------------------------------------------------------------------
    // AVL

    public class AVL
    {
        public Node_BinaryTree root_AVL;

        private int Height(Node_BinaryTree node)
        {
            if (node == null)
                return 0;
            return node.Height;
        }

        private int BalanceFactor(Node_BinaryTree node)
        {
            if (node == null)
                return 0;

            return Height(node.left) - Height(node.right);
        }

        private void UpdateHeight(Node_BinaryTree node)
        {
            if (node != null)
            {
                node.Height = 1 + Math.Max(Height(node.left), Height(node.right));
            }
        }

        private Node_BinaryTree RightRotate(Node_BinaryTree y)
        {
            Node_BinaryTree x = y.left;
            Node_BinaryTree T2 = x.right;

            x.right = y;
            y.left = T2;

            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        private Node_BinaryTree LeftRotate(Node_BinaryTree x)
        {
            Node_BinaryTree y = x.right;
            Node_BinaryTree T2 = y.left;

            y.left = x;
            x.right = T2;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        public void Insert(int value)
        {
            root_AVL = InsertRec(root_AVL, value);
        }

        private Node_BinaryTree InsertRec(Node_BinaryTree node, int value)
        {
            if (node == null)
                return new Node_BinaryTree(value);

            if (value < node.val)
                node.left = InsertRec(node.left, value);
            else if (value > node.val)
                node.right = InsertRec(node.right, value);
            else
                return node;

            UpdateHeight(node);

            int balance = BalanceFactor(node);

            // Left Left
            if (balance > 1 && value < node.left?.val)
                return RightRotate(node);

            // Right Right
            if (balance < -1 && value > node.right?.val)
                return LeftRotate(node);

            // Left Right
            if (balance > 1 && value > node.left?.val)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }

            // Right Left
            if (balance < -1 && value < node.right?.val)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
            }

            return node;
        }

        public bool Search(int value)
        {
            return SearchRec(root_AVL, value) != null;
        }

        private Node_BinaryTree SearchRec(Node_BinaryTree node, int value)
        {
            if (node == null || node.val == value)
                return node;

            if (value < node.val)
                return SearchRec(node.left, value);

            return SearchRec(node.right, value);
        }

        public void Delete(int value)
        {
            root_AVL = DeleteRec(root_AVL, value);
        }

        private Node_BinaryTree DeleteRec(Node_BinaryTree node, int value)
        {
            if (node == null)
                return node;

            if (value < node.val)
                node.left = DeleteRec(node.left, value);
            else if (value > node.val)
                node.right = DeleteRec(node.right, value);
            else
            {
                if (node.left == null)
                    return node.right;
                else if (node.right == null)
                    return node.left;

                node.val = MinValue(node.right);
                node.right = DeleteRec(node.right, node.val);
            }

            if (node == null)
                return node;

            UpdateHeight(node);

            int balance = BalanceFactor(node);

            // Left Left
            if (balance > 1 && BalanceFactor(node.left) >= 0)
                return RightRotate(node);

            // Left Right
            if (balance > 1 && BalanceFactor(node.left) < 0)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }

            // Right Right
            if (balance < -1 && BalanceFactor(node.right) <= 0)
                return LeftRotate(node);

            // Right Left
            if (balance < -1 && BalanceFactor(node.right) > 0)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
            }

            return node;
        }

        private int MinValue(Node_BinaryTree node)
        {
            int minv = node.val;
            while (node.left != null)
            {
                minv = node.left.val;
                node = node.left;
            }
            return minv;
        }

        public int GetHeight()
        {
            return Height(root_AVL);
        }

        public int GetBalance()
        {
            return BalanceFactor(root_AVL);
        }
    }

    // -----------------------------------------------------------------------------------------
    // Red and Black

    public class RedBlackTree
    {
        private Node_BinaryTree root_RedBlack;

        private bool IsRed(Node_BinaryTree node)
        {
            if (node == null)
                return false;

            return node.color == Color.Red;
        }

        private bool IsBlack(Node_BinaryTree node)
        {
            if (node == null)
                return true;

            return node.color == Color.Black;
        }

        private void FlipColors(Node_BinaryTree node)
        {
            if (node == null)
                return;

            if (node.color == Color.Red)
                node.color = Color.Black;
            else
                node.color = Color.Red;

            if (node.left != null)
            {
                if (node.left.color == Color.Red)
                    node.left.color = Color.Black;
                else
                    node.left.color = Color.Red;
            }

            if (node.right != null)
            {
                if (node.right.color == Color.Red)
                    node.right.color = Color.Black;
                else
                    node.right.color = Color.Red;
            }
        }

        // چرخش به چپ
        private Node_BinaryTree RotateLeft(Node_BinaryTree node)
        {
            Node_BinaryTree y = node.right;
            node.right = y.left;
            y.left = node;

            Color temp = node.color;
            node.color = y.color;
            y.color = temp;

            return y;
        }

        // چرخش به راست
        private Node_BinaryTree RotateRight(Node_BinaryTree node)
        {
            Node_BinaryTree y = node.left;
            node.left = y.right;
            y.right = node;

            Color temp = node.color;
            node.color = y.color;
            y.color = temp;

            return y;
        }

        public void Insert(int value)
        {
            root_RedBlack = InsertRec(root_RedBlack, value);

            if (root_RedBlack != null)
                root_RedBlack.color = Color.Black;
        }

        private Node_BinaryTree InsertRec(Node_BinaryTree node, int value)
        {
            if (node == null)
                return new Node_BinaryTree(value);

            if (value < node.val)
                node.left = InsertRec(node.left, value);
            else if (value > node.val)
                node.right = InsertRec(node.right, value);
            else
                return node; 

            return FixInsert(node);
        }

        private Node_BinaryTree FixInsert(Node_BinaryTree node)
        {
            if (IsRed(node.left) && IsRed(node.right))
            {
                FlipColors(node);
                return node;
            }

            if (IsRed(node.left))
            {
                if (IsRed(node.left.left))
                {
                    node = RotateRight(node);
                }
                else if (IsRed(node.left.right))
                {
                    node.left = RotateLeft(node.left);
                    node = RotateRight(node);
                }
            }
            else if (IsRed(node.right))
            {
                if (IsRed(node.right.right))
                {
                    node = RotateLeft(node);
                }
                else if (IsRed(node.right.left))
                {
                    node.right = RotateRight(node.right);
                    node = RotateLeft(node);
                }
            }

            return node;
        }


        public bool Search(int value)
        {
            Node_BinaryTree current = root_RedBlack;

            while (current != null)
            {
                if (value == current.val)
                    return true;

                if (value < current.val)
                    current = current.left;
                else
                    current = current.right;
            }

            return false;
        }

        // delete without balance color

        public void DeleteSimple(int value)
        {
            root_RedBlack = DeleteRec(root_RedBlack, value);
        }

        private Node_BinaryTree DeleteRec(Node_BinaryTree node, int value)
        {
            if (node == null)
                return null;

            if (value < node.val)
                node.left = DeleteRec(node.left, value);
            else if (value > node.val)
                node.right = DeleteRec(node.right, value);
            else
            {
                if (node.left == null)
                    return node.right;

                if (node.right == null)
                    return node.left;

                Node_BinaryTree min = MinNode(node.right);
                node.val = min.val;
                node.right = DeleteRec(node.right, min.val);
            }

            return node;
        }


        public void Delete(int value)
        {
            root_RedBlack = DeleteRec(root_RedBlack, value);
            if (root_RedBlack != null)
                root_RedBlack.color = Color.Black;
        }

        private Node_BinaryTree DeleteRec(Node_BinaryTree z)
        {
            if (z == null) return null;

            Node_BinaryTree x, y;
            Color yOriginalColor = z.color;

            if (z.left == null)
            {
                x = z.right;
                Transplant(z, z.right);
            }
            else if (z.right == null)
            {
                x = z.left;
                Transplant(z, z.left);
            }
            else
            {
                y = MinNode(z.right);
                yOriginalColor = y.color;
                x = y.right;

                if (y.parent == z)
                    x.parent = y;
                else
                {
                    Transplant(y, y.right);
                    y.right = z.right;
                    y.right.parent = y;
                }

                Transplant(z, y);
                y.left = z.left;
                y.left.parent = y;
                y.color = z.color;
            }

            if (yOriginalColor == Color.Black)
                DeleteFixUp(x);

            return root_RedBlack;
        }

        private void Transplant(Node_BinaryTree u, Node_BinaryTree v)
        {
            if (u.parent == null)
                root_RedBlack = v;
            else if (u == u.parent.left)
                u.parent.left = v;
            else
                u.parent.right = v;

            if (v != null)
                v.parent = u.parent;
        }

        // need parent field
        private void DeleteFixUp(Node_BinaryTree x)
        {
            while (x != root_RedBlack && IsBlack(x))
            {
                if (x == x.parent.left)
                {
                    Node_BinaryTree w = x.parent.right;

                    if (IsRed(w))
                    {
                        w.color = Color.Black;
                        x.parent.color = Color.Red;
                        x.parent = RotateLeft(x.parent);
                        w = x.parent.right;
                    }

                    if (IsBlack(w.left) && IsBlack(w.right))
                    {
                        w.color = Color.Red;
                        x = x.parent;
                    }
                    else
                    {
                        if (IsBlack(w.right))
                        {
                            w.left.color = Color.Black;
                            w.color = Color.Red;
                            w = RotateRight(w);
                        }

                        w.color = x.parent.color;
                        x.parent.color = Color.Black;
                        w.right.color = Color.Black;
                        x.parent = RotateLeft(x.parent);
                        x = root_RedBlack;
                    }
                }
                else
                {
                    Node_BinaryTree w = x.parent.left;

                    if (IsRed(w))
                    {
                        w.color = Color.Black;
                        x.parent.color = Color.Red;
                        x.parent = RotateRight(x.parent);
                        w = x.parent.left;
                    }

                    if (IsBlack(w.right) && IsBlack(w.left))
                    {
                        w.color = Color.Red;
                        x = x.parent;
                    }
                    else
                    {
                        if (IsBlack(w.left))
                        {
                            w.right.color = Color.Black;
                            w.color = Color.Red;
                            w = RotateLeft(w);
                        }

                        w.color = x.parent.color;
                        x.parent.color = Color.Black;
                        w.left.color = Color.Black;
                        x.parent = RotateRight(x.parent);
                        x = root_RedBlack;
                    }
                }
            }

            if (x != null)
                x.color = Color.Black;
        }


        private Node_BinaryTree MinNode(Node_BinaryTree node)
        {
            while (node.left != null)
                node = node.left;

            return node;
        }


        public Node_BinaryTree Root
        {
            get { return root_RedBlack; }
        }
    }

    // -----------------------------------------------------------------------------------------
    // Heap

    public class MinHeap
    {
        private List<int> heap;
        public MinHeap()
        {
            heap = new List<int>();
        }
        public MinHeap(int[] initialValues)
        {
            heap = new List<int>(initialValues);
            BuildHeap();
        }
        public int Size => heap.Count;
        public bool IsEmpty => heap.Count == 0;
        public int Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Heap is empty");
            return heap[0];
        }
        public void Insert(int value)
        {
            heap.Add(value);
            SiftUp(heap.Count - 1);
        }
        public int ExtractMin()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Heap is empty");
            int min = heap[0];
            int last = heap[heap.Count - 1];
            heap[0] = last;
            heap.RemoveAt(heap.Count - 1);
            if (!IsEmpty)
                SiftDown(0);
            return min;
        }
        public int Min => Peek();
        public bool Contains(int value)
        {
            return heap.Contains(value);
        }
        public void Print()
        {
            Console.WriteLine("Heap: " + string.Join(" ", heap));
        }
        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[parent] <= heap[index])
                    break;
                Swap(parent, index);
                index = parent;
            }
        }
        private void SiftDown(int index)
        {
            int minIndex = index;
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            if (left < heap.Count && heap[left] < heap[minIndex])
                minIndex = left;
            if (right < heap.Count && heap[right] < heap[minIndex])
                minIndex = right;
            if (minIndex != index)
            {
                Swap(index, minIndex);
                SiftDown(minIndex);
            }
        }
        private void BuildHeap()
        {
            for (int i = heap.Count / 2 - 1; i >= 0; i--)
            {
                SiftDown(i);
            }
        }
        private void Swap(int i, int j)
        {
            (heap[i], heap[j]) = (heap[j], heap[i]);
        }
    }
    // -----------------------------------------------------------------------------------------
    public class MaxHeap
    {
        private List<int> heap;
        public MaxHeap()
        {
            heap = new List<int>();
        }
        public MaxHeap(int[] initialValues)
        {
            heap = new List<int>(initialValues);
            BuildHeap();
        }
        public int Size => heap.Count;
        public bool IsEmpty => heap.Count == 0;
        public int Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Heap is empty");
            return heap[0];
        }
        public void Insert(int value)
        {
            heap.Add(value);
            SiftUp(heap.Count - 1);
        }
        public int ExtractMax()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Heap is empty");
            int max = heap[0];
            int last = heap[heap.Count - 1];
            heap[0] = last;
            heap.RemoveAt(heap.Count - 1);
            if (!IsEmpty)
                SiftDown(0);
            return max;
        }
        public int Max => Peek();
        public bool Contains(int value)
        {
            return heap.Contains(value);
        }
        public void Print()
        {
            Console.WriteLine("Max-Heap: " + string.Join(" ", heap));
        }
        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[parent] >= heap[index])
                    break;
                Swap(parent, index);
                index = parent;
            }
        }
        private void SiftDown(int index)
        {
            int maxIndex = index;
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            if (left < heap.Count && heap[left] > heap[maxIndex])
                maxIndex = left;
            if (right < heap.Count && heap[right] > heap[maxIndex])
                maxIndex = right;
            if (maxIndex != index)
            {
                Swap(index, maxIndex);
                SiftDown(maxIndex);
            }
        }
        private void BuildHeap()
        {
            for (int i = heap.Count / 2 - 1; i >= 0; i--)
            {
                SiftDown(i);
            }
        }
        private void Swap(int i, int j)
        {
            (heap[i], heap[j]) = (heap[j], heap[i]);
        }
    }

    // -----------------------------------------------------------------------------------------
    // sorting

    public static class SelectionSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIdx])
                        minIdx = j;
                }
                (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
            }
        }
    }


    public static class BubbleSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }
    }


    public static class InsertionSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }
    }


    public static class QuickSort
    {
        public static void Sort(int[] arr)
        {
            QuickSortHelper(arr, 0, arr.Length - 1);
        }

        private static void QuickSortHelper(int[] arr, int low, int high)
        {
            if (low >= high) return;

            int pivotIndex = Partition(arr, low, high);
            QuickSortHelper(arr, low, pivotIndex - 1);
            QuickSortHelper(arr, pivotIndex + 1, high);
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return i + 1;
        }
    }


    public static class MergeSort
    {
        public static void Sort(int[] arr)
        {
            int[] temp = new int[arr.Length];
            MergeSortHelper(arr, temp, 0, arr.Length - 1);
        }

        private static void MergeSortHelper(int[] arr, int[] temp, int left, int right)
        {
            if (left >= right) return;

            int mid = left + (right - left) / 2;
            MergeSortHelper(arr, temp, left, mid);
            MergeSortHelper(arr, temp, mid + 1, right);
            Merge(arr, temp, left, mid, right);
        }

        private static void Merge(int[] arr, int[] temp, int left, int mid, int right)
        {
            for (int i = left; i <= right; i++)
                temp[i] = arr[i];

            int iLeft = left;
            int iRight = mid + 1;
            int current = left;

            while (iLeft <= mid && iRight <= right)
            {
                if (temp[iLeft] <= temp[iRight])
                    arr[current++] = temp[iLeft++];
                else
                    arr[current++] = temp[iRight++];
            }

            while (iLeft <= mid)
                arr[current++] = temp[iLeft++];
        }
    }



    // az Heap dar bala estefade shod
    public static class HeapSort
    {
        public static void Sort(int[] arr)
        {
            // ساخت Max-Heap از آرایه
            MaxHeap heap = new MaxHeap(arr);  // ← از constructor شما استفاده می‌شود

            // استخراج تک‌تک بزرگ‌ترین عناصر
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                arr[i] = heap.ExtractMax();
            }
        }
    }



    public static class TreeSort
    {
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length == 0) return;

            Node_BinaryTree root = null;
            foreach (int val in arr)
            {
                root = Insert(root, val);
            }

            int index = 0;
            InOrder(root, arr, ref index);
        }

        private static Node_BinaryTree Insert(Node_BinaryTree node, int val)
        {
            if (node == null) return new Node_BinaryTree(val);

            if (val < node.val)
                node.left = Insert(node.left, val);
            else
                node.right = Insert(node.right, val);

            return node;
        }

        private static void InOrder(Node_BinaryTree node, int[] arr, ref int index)
        {
            if (node == null) return;

            InOrder(node.left, arr, ref index);
            arr[index++] = node.val;
            InOrder(node.right, arr, ref index);
        }
    }


    // ------------------------------------------------------------------------------------------------------------
    // Hashing
    // just kavosh khati

    class HashTable
    {
        private HashNode[] arr;
        private int capacity;
        private int size;
        private const int EMPTY = -1;

        public HashTable(int capacity = 10)
        {
            this.capacity = capacity;
            this.size = 0;
            arr = new HashNode[capacity];

            for (int i = 0; i < capacity; i++)
            {
                arr[i] = null;
            }
        }

        private int HashCode(int key)
        {
            return Math.Abs(key) % capacity;
        }
        public bool IsFull()
        {
            return size == capacity;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public bool InsertNode(int key, int value)
        {
            if (IsFull())
            {
                Console.WriteLine($"eror: chart is full! cant add {key}.");
                return false;
            }

            int hashIndex = HashCode(key);
            int originalIndex = hashIndex;
            int probeCount = 0;

            while (probeCount < capacity)
            {
                if (arr[hashIndex] == null || arr[hashIndex].isDeleted)
                {
                    arr[hashIndex] = new HashNode(key, value);
                    size++;
                    Console.WriteLine($"key {key} with value {value} in index {hashIndex} aded.");
                    return true;
                }
                else if (arr[hashIndex].key == key)
                {
                    Console.WriteLine($"key {key} exist. value from {arr[hashIndex].value} to {value} changed.");
                    arr[hashIndex].value = value;
                    return true;
                }

                probeCount++;
                hashIndex = (originalIndex + probeCount) % capacity;
            }

            Console.WriteLine($"eror: free place for {key} dont find.");
            return false;
        }

        public bool DeleteNode(int key)
        {
            if (IsEmpty())
            {
                Console.WriteLine($"eror: chet is empty! cant delete key {key} .");
                return false;
            }

            int hashIndex = HashCode(key);
            int originalIndex = hashIndex;
            int probeCount = 0;

            while (probeCount < capacity)
            {
                if (arr[hashIndex] == null)
                {
                    break;
                }
                else if (arr[hashIndex].key == key && !arr[hashIndex].isDeleted)
                {
                    arr[hashIndex].isDeleted = true;
                    size--;
                    Console.WriteLine($"key {key} from index {hashIndex} deleted.");
                    return true;
                }

                probeCount++;
                hashIndex = (originalIndex + probeCount) % capacity;
            }

            Console.WriteLine($"eror: key {key} dont find.");
            return false;
        }

        public int GetValue(int key)
        {
            if (IsEmpty())
            {
                Console.WriteLine($"eror: chart is empty! key {key} dont find.");
                return -1;
            }

            int hashIndex = HashCode(key);
            int originalIndex = hashIndex;
            int probeCount = 0;

            while (probeCount < capacity)
            {
                if (arr[hashIndex] == null)
                {
                    break;
                }
                else if (arr[hashIndex].key == key && !arr[hashIndex].isDeleted)
                {
                    Console.WriteLine($"key {key} in index {hashIndex} find. value: {arr[hashIndex].value}");
                    return arr[hashIndex].value;
                }

                probeCount++;
                hashIndex = (originalIndex + probeCount) % capacity;
            }

            Console.WriteLine($"eror: key {key} dont find.");
            return -1;
        }

        public void Print()
        {
            if (IsEmpty())
            {
                Console.WriteLine("chart is empty!");
                return;
            }

            Console.WriteLine("\n========== stutus ==========");
            Console.WriteLine($"capasity: {capacity}");
            Console.WriteLine($"number of values: {size}");
            Console.WriteLine("-----------------------------------");

            for (int i = 0; i < capacity; i++)
            {
                if (arr[i] != null && !arr[i].isDeleted)
                {
                    Console.WriteLine($"index [{i}]: key = {arr[i].key}, value = {arr[i].value}");
                }
                else if (arr[i] != null && arr[i].isDeleted)
                {
                    Console.WriteLine($"index [{i}]: <deleted>");
                }
                else
                {
                    Console.WriteLine($"index [{i}]: <empty>");
                }
            }
            Console.WriteLine("===================================\n");
        }

        public void GetStatistics()
        {
            int occupied = 0;
            int deleted = 0;
            int empty = 0;

            for (int i = 0; i < capacity; i++)
            {
                if (arr[i] == null)
                    empty++;
                else if (arr[i].isDeleted)
                    deleted++;
                else
                    occupied++;
            }

            double loadFactor = (double)size / capacity;

            Console.WriteLine("\n======= stutus =======");
            Console.WriteLine($"capacity: {capacity}");
            Console.WriteLine($"high value: {occupied}");
            Console.WriteLine($"deleted value: {deleted}");
            Console.WriteLine($"empty plase: {empty}");
            Console.WriteLine($"factor: {loadFactor:P2}");
            Console.WriteLine("============================\n");
        }
    }

    // ------------------------------------------------------------------------------------------------
    // ------------------------------------ Exercises -----------------------------------
    // ------------------------------------------------------------------------------------------------

    // DoublyLinkedList

    // az node aval code estefade shode

    class DoublyLinkedList
    {
        Node_DoublyLinkedList Head;
        Node_DoublyLinkedList Tail;
        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
        }

        public void insertFirst_DoublyLinkedList(int data)
        {
            Node_DoublyLinkedList newNode = new Node_DoublyLinkedList(data);
            if (Head == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head.Prev = newNode;
                Head = newNode;
            }
        }
        public void insertLast_DoublyLinkedList(int data)
        {
            Node_DoublyLinkedList newNode = new Node_DoublyLinkedList(data);
            if (Head == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
        }

        public void insertAfter_DoublyLinkedList(Node_DoublyLinkedList node, int data)
        {
            if (node == null) return;

            Node_DoublyLinkedList newNode = new Node_DoublyLinkedList(data);
            newNode.Prev = node;
            newNode.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = newNode;
            else
                Tail = newNode;

            node.Next = newNode;
        }
        public void insertBefore_DoublyLinkedList(Node_DoublyLinkedList node, int data)
        {
            if (node == null) return;

            Node_DoublyLinkedList newNode = new Node_DoublyLinkedList(data);
            newNode.Next = node;
            newNode.Prev = node.Prev;

            if (node.Prev != null)
                node.Prev.Next = newNode;
            else
                Head = newNode;

            node.Prev = newNode;
        }
        public void deleteNodeByKey_DoublyLinkedList(int key)
        {
            Node_DoublyLinkedList x = Find_DoublyLinkedList(key);
            if (x == null)
            {
                Console.WriteLine("not exist...");
                return;
            }
            Node_DoublyLinkedList current = Head;
            while (current != null)
            {
                if (current.Data == key)
                {
                    if (current.Prev != null)
                        current.Prev.Next = current.Next;
                    else
                        Head = current.Next;

                    if (current.Next != null)
                        current.Next.Prev = current.Prev;
                    else
                        Tail = current.Prev;

                    return;
                }
                current = current.Next;
            }
        }
        public void reverseList_DoublyLinkedList()
        {
            if (Head == null || Head.Next == null)
                return;

            Node_DoublyLinkedList current = Head;
            while (current != null)
            {
                Node_DoublyLinkedList temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;
                current = current.Prev;
            }

            Node_DoublyLinkedList tempHeadTail = Head;
            Head = Tail;
            Tail = tempHeadTail;
        }
        public void printList_DoublyLinkedList()
        {
            Node_DoublyLinkedList current = Head;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
        public Node_DoublyLinkedList Find_DoublyLinkedList(int data)
        {
            Node_DoublyLinkedList current = Head;
            while (current != null)
            {
                if (current.Data == data)
                    return current;
                current = current.Next;
            }
            return null;
        }
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // CircularQueue
    public class CircularQueue
    {
        private int[] queue;
        private int front;
        private int rear;
        private int capacity;

        public CircularQueue(int size)
        {
            capacity = size;
            queue = new int[capacity];
            front = 0;
            rear = 0;
        }


        public void Enqueue(int data)
        {
            if (IsFull())
            {
                Console.WriteLine("queueu is full");
                return;
            }

            queue[rear] = data;
            rear = (rear + 1) % capacity;
        }

        public int Dequeue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("queue is empty");
                return -1;
            }

            int data = queue[front];
            front = (front + 1) % capacity;
            return data;
        }

        // چک کردن پر بودن
        private bool IsFull()
        {
            return (rear + 1) % capacity == front;
        }


        private bool IsEmpty()
        {
            return front == rear;
        }

        public int Size()
        {
            return (rear - front + capacity) % capacity;
        }
    }

    // ----------------------------------------------------------------------------------------------------------
    // Stack_and_Queue_with_LinkedList

    public class Stack_LinkedList
    {
        private Node head;

        public Stack_LinkedList()
        {
            head = null;
        }
        public void Push(int data)
        {
            Node newNode = new Node(data);
            newNode.Next = head;
            head = newNode;
        }
        public void Pop()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty! Cannot pop.");
                return;
            }
            int poppedData = head.Data;
            head = head.Next;
            Console.WriteLine(poppedData);
        }
        public void Peek()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty! Cannot peek.");
                return;
            }
            Console.WriteLine(head.Data);
        }
        public bool IsEmpty()
        {
            return head == null;
        }
    }


    public class Queue_LinkedList
    {
        private Node front;
        private Node rear;
        public Queue_LinkedList()
        {
            front = null;
            rear = null;
        }
        public void Enqueue(int data)
        {
            Node newNode = new Node(data);
            if (IsEmpty())
            {
                front = rear = newNode;
            }
            else
            {
                rear.Next = newNode;
                rear = newNode;
            }

        }
        public void Dequeue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Queue is empty! Cannot dequeue.");
                return;
            }
            int dequeuedData = front.Data;
            front = front.Next;
            if (front == null)
            {
                rear = null;
            }
            Console.WriteLine(dequeuedData);
        }
        public bool IsEmpty()
        {
            return front == null;
        }
    }

    // -----------------------------------------------------------------------------------------------------------
    // TwoStacks in one arrey

    public class TwoStacks_one_arrey
    {
        private int[] arr;
        private int top1;
        private int top2;
        private int size;

        public TwoStacks_one_arrey(int n)
        {
            arr = new int[n];
            top1 = -1;
            top2 = n;
            size = n;

            for (int i = 0; i < n; i++)
            {
                arr[i] = -1;
            }
        }

        public void Push1(int x)
        {
            if (top1 + 1 == top2)
            {
                Console.WriteLine("Stack 1 is full!");
                return;
            }
            top1++;
            arr[top1] = x;
            Console.WriteLine($"Push1: {x} pushed to stack 1..."); // Array: [{string.Join(", ", arr)}]
        }

        public int Pop1()
        {
            if (top1 == -1)
            {
                Console.WriteLine("Stack 1 is empty!");
                return -1;
            }
            int x = arr[top1];
            arr[top1] = -1;
            top1--;
            Console.WriteLine($"Pop1: {x} popped from stack 1..."); // Array: [{string.Join(", ", arr)}]
            return x;
        }

        public void Push2(int x)
        {
            if (top2 - 1 == top1)
            {
                Console.WriteLine("Stack 2 is full!");
                return;
            }
            top2--;
            arr[top2] = x;
            Console.WriteLine($"Push2: {x} pushed to stack 2..."); // Array: [{string.Join(", ", arr)}]
        }

        public int Pop2()
        {
            if (top2 == size)
            {
                Console.WriteLine("Stack 2 is empty!");
                return -1;
            }
            int x = arr[top2];
            arr[top2] = -1;
            top2++;
            Console.WriteLine($"Pop2: {x} popped from stack 2..."); // Array: [{string.Join(", ", arr)}]
            return x;
        }

        public bool IsEmpty1()
        {
            return top1 == -1;
        }

        public bool IsFull1()
        {
            return top1 + 1 == top2;
        }

        public bool IsEmpty2()
        {
            return top2 == size;
        }

        public bool IsFull2()
        {
            return top2 - 1 == top1;
        }

        public void PrintFinalStatus()
        {
            Console.WriteLine("final array: [{0}]", string.Join(", ", arr));
            Console.WriteLine($"top1: {top1}"); //, value = {arr[top1]}
            Console.WriteLine($"top2: {top2}"); // , value = {arr[top2]}

            if (top1 >= 0)
                Console.WriteLine($"value in top1: {arr[top1]}");
            else
                Console.WriteLine("stack 1 is empty...");
            if (top2 < size)
                Console.WriteLine($"value in top2: {arr[top2]}");
            else
                Console.WriteLine("stack 2 is empty...");
        }
    }

    // ------------------------------------------------------------------------------------------------------------------------
    // BFS

    // dar in cod az node binary dar aval code estafade shode
    public class BinaryTree_BFS
    {
            // در این کد اگه فرزندانی ( که خواهر برادری داشته باشن ویا اینکه خواهر برادرشون فرزند داشته باشه ) وجود نداشته باشن اون هارو با -1 نمایش میدیم
        public static List<int> BFS(Node_BinaryTree root)
        {
            if (root == null) return new List<int>();
            Queue<Node_BinaryTree> queue = new Queue<Node_BinaryTree>(); queue.Enqueue(root);
            List<int> result = new List<int>();
            while (queue.Count > 0)
            {
                Node_BinaryTree node = queue.Dequeue();

                if (node == null)
                {
                    result.Add(-1);
                }
            else
                {
                    result.Add(node.val);

                    if (node.left != null || node.right != null)
                    {
                        queue.Enqueue(node.left);
                        queue.Enqueue(node.right);
                    }
                }
            }
            return result;
        }

        // اینجا یک درخت فرضی ساختم برای تست کد
        public static Node_BinaryTree BuildExampleTree()
        {
            Node_BinaryTree root = new Node_BinaryTree(10);
            root.left = new Node_BinaryTree(5);
            root.right = new Node_BinaryTree(15);
            root.left.left = new Node_BinaryTree(3);
            root.left.right = new Node_BinaryTree(7);
            root.right.left = new Node_BinaryTree(12);
            root.right.right = new Node_BinaryTree(18);
            return root;
        }
    }

    // ------------------------------------------------------------------------------------------------------------
    // pre and in order Non-returnable

    // dar in cod az node binary dar aval code estafade shode
    public class BinaryTree_Non_returnable
    {

        public Node_BinaryTree Root { get; set; }

        public BinaryTree_Non_returnable()
        {
            Root = null;
        }

        public void Insert(int value)
        {
            Node_BinaryTree newNode = new Node_BinaryTree(value);
            if (Root == null)
            {
                Root = newNode;
                return;
            }

            Node_BinaryTree current = Root;
            while (true)
            {
                if (value < current.val)
                {
                    if (current.left == null)
                    {
                        current.left = newNode;
                        return;
                    }
                    current = current.left;
                }
                else if (value > current.val)
                {
                    if (current.right == null)
                    {
                        current.right = newNode;
                        return;
                    }
                    current = current.right;
                }
                else
                {
                    return;
                }
            }
        }


        public void InOrderTraversal(Node_BinaryTree root)
        {
            if (root == null) return;

            Stack<Node_BinaryTree> stack = new Stack<Node_BinaryTree>();
            Node_BinaryTree current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.left;
                }

                current = stack.Pop();
                Console.Write(current.val + " ");

                current = current.right;
            }
        }


        public void PreOrderTraversal(Node_BinaryTree root)
        {
            if (root == null) return;

            Stack<Node_BinaryTree> stack = new Stack<Node_BinaryTree>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                Node_BinaryTree current = stack.Pop();
                Console.Write(current.val + " ");

                if (current.right != null)
                    stack.Push(current.right);

                if (current.left != null)
                    stack.Push(current.left);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //// -------------------------------------------------------------------------------------------------
            //// -------------------------------------------- Lessons --------------------------------------------
            //// -------------------------------------------------------------------------------------------------
            //Console.WriteLine("-------------------------------------------------------------------------------------------------");
            //Console.WriteLine("-------------------------------------------- Lessons --------------------------------------------");
            //Console.WriteLine("-------------------------------------------------------------------------------------------------");

            //// ----------------------------------------- test for LinkedList --------------------------------------------
            //Console.WriteLine("----------------------------------------- test for LinkedList --------------------------------------------");

            //// be tor dasti dar list adad vared shod vali mi tavan dar consol ba kami taghir adad vared kard

            //LinkedList list = new LinkedList();

            //list.Insert_last(10);
            //list.Insert_last(20);
            //list.Insert_first(5);
            //list.Insert_last(30);

            //list.Display();


            //Console.WriteLine("exist 20? " + list.Find(20));
            //Console.WriteLine("exist 50? " + list.Find(50));

            //Console.WriteLine("delet 10... ");
            //list.Delete(10);
            //list.Display();

            //Console.WriteLine("add 25 after 20...");
            //list.Insert_after(20, 25);
            //list.Display();

            //Console.WriteLine("number of data's (size): " + list.Get_size());

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ----------------------------------------- test for General conversion to binary --------------------------------------------
            //Console.WriteLine("----------------------------------------- test for General conversion to binary --------------------------------------------");

            //var root_General = new GeneralNode(1);

            //var n2 = new GeneralNode(2);
            //var n3 = new GeneralNode(3);
            //var n4 = new GeneralNode(4);

            //root_General.Children.Add(n2);
            //root_General.Children.Add(n3);
            //root_General.Children.Add(n4);

            //n2.Children.Add(new GeneralNode(5));
            //n2.Children.Add(new GeneralNode(6));

            //n3.Children.Add(new GeneralNode(7));

            //var binary = TreeConverter.ConvertToBinary(root_General);
            //Console.WriteLine("converting");
            //new Traversal().PreOrder(binary);

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ----------------------------------------- test for Traversal --------------------------------------------
            //Console.WriteLine("----------------------------------------- test for Traversal --------------------------------------------");

            //// yek derakht pishfarz darim

            //Traversal traversal = new Traversal();
            //Node_BinaryTree root_Traversal = Traversal.BuildExampleTree();

            //Console.Write("Preorder: ");
            //traversal.PreOrder(root_Traversal);
            //Console.WriteLine();

            //Console.Write("InOrder: ");
            //traversal.InOrder(root_Traversal);
            //Console.WriteLine();

            //Console.Write("PostOrder: ");
            //traversal.PostOrder(root_Traversal);
            //Console.WriteLine();

            //Console.Write("LevelOrder: ");
            //traversal.LevelOrder(root_Traversal);

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ----------------------------------------------- test for BST ------------------------------------------------
            //Console.WriteLine("------------------------------------------------ test for BST ------------------------------------------------");

            ////dar in code adad ha dar yek function pish farz(dar class asli ke kar an fghat yek while baray ezafe kardan adad hast) sakhte shode

            //Node_BinaryTree bstRoot = Gfc_BST.BuildBST();
            //Console.WriteLine("BST made by : 10,5,15,3,7,12,18");

            //Node_BinaryTree found = Gfc_BST.Search(bstRoot, 7);
            //if (found != null)
            //{
            //    Console.WriteLine("Search 7: " + found.val);
            //}
            //else
            //{
            //    Console.WriteLine("Search 7: Not Found");
            //}


            //Console.WriteLine("add 20...");
            //Console.WriteLine("add 7...");
            //Console.WriteLine("7 is duplicated! ");
            //bstRoot = Gfc_BST.Insert(bstRoot, 20);
            //bstRoot = Gfc_BST.Insert(bstRoot, 7);
            //Console.Write("InOrder: ");
            //new Traversal().InOrder(bstRoot);
            //Console.WriteLine();

            //Console.WriteLine("delete 5...");
            //bstRoot = Gfc_BST.Delete(bstRoot, 5);
            //Console.Write("InOrder: ");
            //new Traversal().InOrder(bstRoot);
            //Console.WriteLine();

            //Node_BinaryTree minNode = Gfc_BST.FindMin(bstRoot);
            //Node_BinaryTree maxNode = Gfc_BST.FindMax(bstRoot);
            //Console.WriteLine("Min: " + minNode.val + ", Max: " + maxNode.val);

            //Console.WriteLine("floor and ceil of 6: ");
            //Console.WriteLine("Floor of 6: " + Gfc_BST.Floor(bstRoot, 6));
            //Console.WriteLine("Ceil of 6: " + Gfc_BST.Ceil(bstRoot, 6));

            //Traversal traversal_BST = new Traversal();
            //Console.WriteLine("PreOrder BST: ");
            //traversal_BST.PreOrder(bstRoot);
            //Console.WriteLine();
            //Console.WriteLine("InOrder BST: ");
            //traversal_BST.InOrder(bstRoot);
            //Console.WriteLine();
            //Console.WriteLine("PostOrder BST: ");
            //traversal_BST.PostOrder(bstRoot);
            //Console.WriteLine();
            //Console.WriteLine("LevelOrder BST: ");
            //traversal_BST.LevelOrder(bstRoot);
            //Console.WriteLine();

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ----------------------------------------------- test for AVL ------------------------------------------------
            //Console.WriteLine("------------------------------------------------ test for AVL ------------------------------------------------");

            //AVL tree_AVL = new AVL();

            //int[] values_AVL = { 50, 30, 70, 20, 40, 60, 80, 10, 25, 35, 45, 65 };

            //Console.WriteLine("Add...:");
            //foreach (int val in values_AVL)
            //{
            //    tree_AVL.Insert(val);
            //    Console.Write(val + " ");
            //}
            //Console.WriteLine("\n");

            //Traversal traverser = new Traversal();

            //Console.Write("In-order: ");
            //traverser.InOrder(tree_AVL.root_AVL);
            //Console.WriteLine();

            //Console.Write("Pre-order : ");
            //traverser.PreOrder(tree_AVL.root_AVL);
            //Console.WriteLine();

            //Console.Write("Post-order: ");
            //traverser.PostOrder(tree_AVL.root_AVL);
            //Console.WriteLine();

            //Console.Write("Level-order: ");
            //traverser.LevelOrder(tree_AVL.root_AVL);
            //Console.WriteLine();

            //Console.WriteLine($"\nHeight: {tree_AVL.GetHeight()}");
            //Console.WriteLine($"Balance Factor: {tree_AVL.GetBalance()}");

            //Console.WriteLine("\nSearch:");
            //Console.WriteLine($"Search 40 → {tree_AVL.Search(40)}");
            //Console.WriteLine($"Search 100 → {tree_AVL.Search(100)}");

            //Console.WriteLine("\nDeleting 20,70,40: ");
            //tree_AVL.Delete(20);
            //tree_AVL.Delete(70);
            //tree_AVL.Delete(40);

            //Console.Write("In-order: ");
            //traverser.InOrder(tree_AVL.root_AVL);
            //Console.WriteLine();

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();


            //// ----------------------------------------------- test for RedBlack ------------------------------------------------
            //Console.WriteLine("------------------------------------------------ test for RedBlack ------------------------------------------------");

            //RedBlackTree rbt = new RedBlackTree();

            //int[] numbers = { 40, 20, 60, 10, 30, 50, 70, 5, 15, 25, 35, 55 };

            //Console.WriteLine("Inserting values:");
            //foreach (int n_AVL in numbers)
            //{
            //    rbt.Insert(n_AVL);
            //    Console.Write(n_AVL + " ");
            //}
            //Console.WriteLine("\n");

            //Traversal t = new Traversal();

            //Console.WriteLine("Tree after insertion (In-order):");
            //t.InOrder(rbt.Root);
            //Console.WriteLine("\n");

            //Console.WriteLine("Tree after insertion (Pre-order):");
            //t.PreOrder(rbt.Root);
            //Console.WriteLine("\n");

            //Console.WriteLine("Tree after insertion (Level-order):");
            //t.LevelOrder(rbt.Root);
            //Console.WriteLine("\n\n");

            //Console.WriteLine("Search results:");
            //Console.WriteLine("  Does 30 exist? " + rbt.Search(30));
            //Console.WriteLine("  Does 999 exist? " + rbt.Search(999));
            //Console.WriteLine();

            //Console.WriteLine("Deleting some nodes...");

            //Console.WriteLine("Simple deletion (DeleteSimple) of node 20:");
            //rbt.DeleteSimple(20);
            //Console.Write("In-order after DeleteSimple(20): ");
            //t.InOrder(rbt.Root);
            //Console.WriteLine("\n");

            //Console.WriteLine("Full deletion (Delete) of node 60:");
            //rbt.Delete(60);
            //Console.Write("In-order after Delete(60): ");
            //t.InOrder(rbt.Root);
            //Console.WriteLine("\n");

            //Console.WriteLine("Final Level-order:");
            //t.LevelOrder(rbt.Root);
            //Console.WriteLine();

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ----------------------------------------------- test for Heap ------------------------------------------------
            //Console.WriteLine("------------------------------------------------ test for Heap ------------------------------------------------");

            //int[] data_Heap = { 48, 12, 35, 5, 99, 27, 18, 61, 3, 44, 76, 19 };

            //Console.WriteLine("=== MinHeap vs MaxHeap Comparison Test ===\n");

            //Console.WriteLine("Input values (insertion order):");
            //Console.WriteLine(string.Join(" ", data_Heap));
            //Console.WriteLine(new string('-', 60) + "\n");

            //// ────────────────────── MinHeap Section ──────────────────────
            //Console.WriteLine("1) MIN-HEAP");
            //MinHeap minHeap = new MinHeap();

            //Console.WriteLine("Inserting elements...");
            //foreach (int val in data_Heap)
            //{
            //    minHeap.Insert(val);
            //}

            //Console.Write("Internal MinHeap structure (Level order): ");
            //minHeap.Print();

            //Console.WriteLine($"Size          : {minHeap.Size}");
            //Console.WriteLine($"Smallest      : {minHeap.Peek()}");
            //Console.WriteLine();

            //Console.WriteLine("Extracting smallest elements (ExtractMin):");
            //for (int i = 1; i <= 5; i++)
            //{
            //    int extracted = minHeap.ExtractMin();
            //    Console.WriteLine($"{i,2}) Extracted: {extracted,3}   →  Remaining: {minHeap.Size}");
            //}
            //Console.WriteLine();

            //Console.Write("MinHeap after 5 extractions: ");
            //minHeap.Print();
            //Console.WriteLine(new string('-', 60) + "\n");

            //// ────────────────────── MaxHeap Section ──────────────────────
            //Console.WriteLine("2) MAX-HEAP");
            //MaxHeap maxHeap = new MaxHeap();

            //Console.WriteLine("Inserting elements...");
            //foreach (int val in data_Heap)
            //{
            //    maxHeap.Insert(val);
            //}

            //Console.Write("Internal MaxHeap structure (Level order): ");
            //maxHeap.Print();

            //Console.WriteLine($"Size          : {maxHeap.Size}");
            //Console.WriteLine($"Largest       : {maxHeap.Peek()}");
            //Console.WriteLine();

            //Console.WriteLine("Extracting largest elements (ExtractMax):");
            //for (int i = 1; i <= 5; i++)
            //{
            //    int extracted = maxHeap.ExtractMax();
            //    Console.WriteLine($"{i,2}) Extracted: {extracted,3}   →  Remaining: {maxHeap.Size}");
            //}
            //Console.WriteLine();

            //Console.Write("MaxHeap after 5 extractions: ");
            //maxHeap.Print();

            //Console.WriteLine("\n" + new string('=', 60));

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();


            //// ----------------------------------------------- test for Sorting ------------------------------------------------
            //Console.WriteLine("------------------------------------------------ test for Sorting ------------------------------------------------");

            //int[] original = new int[] {
            // 127, -143, -188, 179, -60, -75, -86, -129, 177, -148,
            // 146, 179, 79, -156, 102, 16, -184, -185, -153, -89,
            // -81, 58, 108, -187, 87, -99, 166, 132, 159, 79,
            // 14, -88, 29, 101, -58, -197, 188, -119, 157, 16,
            // -26, -58, -121, -90, 190, -28, -148, -153, -6, -151
            // };
            //Console.Write("Datas:  ");

            //foreach (int x in original)
            //{
            //    Console.Write(x + "  ");
            //}
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("=== Sorting Algorithms Test - 50 elements ===\n");
            //Console.WriteLine("Original array size: 50");
            //Console.WriteLine(new string('-', 65) + "\n");

            //var sorters = new (string Name, Action<int[]> Sort)[]
            //{
            // ("Selection Sort", SelectionSort.Sort),
            // ("Bubble Sort",    BubbleSort.Sort),
            // ("Insertion Sort", InsertionSort.Sort),
            // ("Quick Sort",     QuickSort.Sort),
            // ("Merge Sort",     MergeSort.Sort),
            // ("Heap Sort",      HeapSort.Sort),
            // ("Tree Sort",      TreeSort.Sort)
            //};

            //foreach (var (name, sorter) in sorters)
            //{
            //    int[] copy = (int[])original.Clone();

            //    var stopwatch = Stopwatch.StartNew();
            //    sorter(copy);
            //    stopwatch.Stop();

            //    Console.WriteLine($"{name,-14} | Time: {stopwatch.ElapsedMilliseconds,4} ms   | {stopwatch.ElapsedTicks,10} ticks");
            //    Console.WriteLine("Sorted: " + string.Join(" ", copy));
            //    Console.WriteLine(new string('-', 65));
            //}

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ----------------------------------------------- test for Hashing ------------------------------------------------
            //Console.WriteLine("------------------------------------------------ test for Hashing ------------------------------------------------");

            //Console.WriteLine("=== test hash node ===\n");

            //HashTable hashTable = new HashTable(7);

            //Console.WriteLine("1. add:");
            //Console.WriteLine("---------------");
            //hashTable.InsertNode(10, 100);
            //hashTable.InsertNode(20, 200);
            //hashTable.InsertNode(30, 300);
            //hashTable.InsertNode(17, 170);
            //hashTable.InsertNode(24, 240);
            //hashTable.InsertNode(31, 310);
            //hashTable.InsertNode(45, 450);

            //hashTable.Print();
            //hashTable.GetStatistics();

            //Console.WriteLine("\n2. test add in repeated:");
            //Console.WriteLine("----------------------------");
            //hashTable.InsertNode(20, 250);
            //hashTable.Print();

            //Console.WriteLine("\n3. search:");
            //Console.WriteLine("-----------------");
            //int value1 = hashTable.GetValue(10);
            //int value2 = hashTable.GetValue(17);
            //int value3 = hashTable.GetValue(99);

            //Console.WriteLine("\n4. delete:");
            //Console.WriteLine("---------------");
            //hashTable.DeleteNode(17);
            //hashTable.DeleteNode(30);
            //hashTable.DeleteNode(99);

            //hashTable.Print();
            //hashTable.GetStatistics();

            //Console.WriteLine("\n5. add after delete:");
            //Console.WriteLine("-------------------");
            //hashTable.InsertNode(50, 500);
            //hashTable.InsertNode(60, 600);

            //hashTable.Print();

            //Console.WriteLine("\n6. search after delete:");
            //Console.WriteLine("---------------------");
            //hashTable.GetValue(17);
            //hashTable.GetValue(50);

            //Console.WriteLine("\n7. test add in full:");
            //Console.WriteLine("-----------------------");
            //hashTable.InsertNode(70, 700);
            //hashTable.InsertNode(80, 800);

            //hashTable.Print();
            //hashTable.GetStatistics();

            //Console.WriteLine("\n8. final position:");
            //Console.WriteLine("----------------------");
            //hashTable.Print();

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ------------------------------------------------------------------------------------------------
            //// ------------------------------------------- Exercises ------------------------------------------
            //// ------------------------------------------------------------------------------------------------
            //Console.WriteLine("------------------------------------------------------------------------------------------------");
            //Console.WriteLine("------------------------------------------ Exercises -------------------------------------------");
            //Console.WriteLine("------------------------------------------------------------------------------------------------");

            //// ---------------------------  test for DoublyLinkedList -----------------------
            //Console.WriteLine("---------------------------  test for DoublyLinkedList -----------------------");
            //DoublyLinkedList list_DoublyLinkedList = new DoublyLinkedList();

            //Console.WriteLine("creating...");
            //for (int i = 1; i <= 100; i++)
            //{
            //    list_DoublyLinkedList.insertLast_DoublyLinkedList(i);
            //}


            //Console.WriteLine("print:");
            //list_DoublyLinkedList.printList_DoublyLinkedList();

            //Console.WriteLine("reversing");
            //list_DoublyLinkedList.reverseList_DoublyLinkedList();

            //Console.WriteLine("print reverse:");
            //list_DoublyLinkedList.printList_DoublyLinkedList();


            //Console.WriteLine("testing other funcs:");
            //Console.WriteLine("________________________________________________________________________");


            //list_DoublyLinkedList.insertFirst_DoublyLinkedList(0);
            //Console.WriteLine("add 0 in first:");
            //list_DoublyLinkedList.printList_DoublyLinkedList();


            //list_DoublyLinkedList.insertLast_DoublyLinkedList(101);
            //Console.WriteLine("add 101 in last:");
            //list_DoublyLinkedList.printList_DoublyLinkedList();


            //Node_DoublyLinkedList node50 = list_DoublyLinkedList.Find_DoublyLinkedList(50);
            //list_DoublyLinkedList.insertAfter_DoublyLinkedList(node50, 51);
            //Console.WriteLine("add 51 after 50:");
            //list_DoublyLinkedList.printList_DoublyLinkedList();


            //list_DoublyLinkedList.insertBefore_DoublyLinkedList(node50, 49);
            //Console.WriteLine("add 49 before 50:");
            //list_DoublyLinkedList.printList_DoublyLinkedList();



            //Console.WriteLine("dealit by key 49:");
            //list_DoublyLinkedList.deleteNodeByKey_DoublyLinkedList(49);
            //list_DoublyLinkedList.printList_DoublyLinkedList();

            //Console.WriteLine("dealit by key 110:");
            //list_DoublyLinkedList.deleteNodeByKey_DoublyLinkedList(110);
            //list_DoublyLinkedList.printList_DoublyLinkedList();

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ------------------------------------------------------- test for CircularQueue -----------------------------------------
            //Console.WriteLine("------------------------------------------------------- test for CircularQueue -----------------------------------------");

            //// در این کد یک ارایه به عنوان تضمین برای پر نشدن صف دایره ای در نظر گرفته شده است

            //// تابع زیر به صورت دستی عدد می گیرد
            ////CircularQueue q = new CircularQueue(6);
            ////q.Enqueue(10);
            ////q.Enqueue(20);
            ////q.Enqueue(30);
            ////q.Enqueue(40);
            ////q.Enqueue(50);

            ////while (q.Size() > 0)
            ////{
            ////    Console.WriteLine(q.Dequeue());
            ////}



            //Random rand = new Random();
            //Console.WriteLine("enter teh size of queue: ");
            //int n = int.Parse(Console.ReadLine());
            //n++;
            //CircularQueue q = new CircularQueue(n);
            //Console.WriteLine("add numbers...");

            //for (int i = 0; i < n - 1; i++)
            //{
            //    int num = rand.Next(1, 101);
            //    q.Enqueue(num);
            //}

            //Console.WriteLine("numbers: ");

            //while (q.Size() > 0)
            //{
            //    Console.WriteLine(q.Dequeue());
            //}

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// -------------------------------------------- test for Stack_and_Queue_with_LinkedList ---------------------------------------------
            //Console.WriteLine("-------------------------------------------- test for Stack_and_Queue_with_LinkedList ---------------------------------------------");

            ////Console.WriteLine("=== test Stack ===");
            ////Stack stack = new Stack();
            ////Console.WriteLine("pushed...");
            ////stack.Push(1);
            ////stack.Push(2);
            ////stack.Push(3);
            ////Console.WriteLine("peek: ");
            ////stack.Peek();  
            ////Console.WriteLine("poped...");
            ////stack.Pop();  
            ////stack.Pop(); 
            ////stack.Pop();  
            ////stack.Pop();  

            ////Console.WriteLine("\n=== test Queue ===");
            ////Queue queue = new Queue();
            ////Console.WriteLine("enqueued...");
            ////queue.Enqueue(1);
            ////queue.Enqueue(2);
            ////queue.Enqueue(3);
            ////Console.WriteLine("dequeued...");
            ////queue.Dequeue(); 
            ////queue.Dequeue(); 
            ////queue.Dequeue();  
            ////queue.Dequeue();  



            //Random random = new Random();
            //Console.Write("enter the size: ");
            //int count = int.Parse(Console.ReadLine());

            //Console.WriteLine("add datas...");
            //Stack_LinkedList stack = new Stack_LinkedList();
            //Queue_LinkedList queue = new Queue_LinkedList();

            //for (int i = 0; i < count; i++)
            //{
            //    int num = random.Next(1, 101);
            //    stack.Push(num);
            //    queue.Enqueue(num);
            //}

            //Console.WriteLine("print stack data's: ");

            //while (!stack.IsEmpty())
            //{
            //    stack.Pop();
            //}

            //Console.WriteLine("print queue data's: ");

            //while (!queue.IsEmpty())
            //{
            //    queue.Dequeue();
            //}

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// -------------------------------------------------------- test for TwoStacks in one arrey ------------------------------------------------
            //Console.WriteLine("-------------------------------------------------------- test for TwoStacks in one arrey ------------------------------------------------");

            ////در این کد اندیس تاپ ها از یکی کمتر و بیشتر شروع می شوند برای اینکه در وسط ارایه به هم برخورد نکنند
            ////همچنین در قسمت چاپ وضعیت نهایی برای هندل کردن ارور برای زمانی که استک ها خالی باشند و تاپ ها به دلیل ویژگی بالا مقداری نداشته باشند از دو شرط استفاده شده

            //TwoStacks_one_arrey stacks = new TwoStacks_one_arrey(5);

            //stacks.Push1(1);
            //stacks.Push2(2);
            //stacks.Push1(3);
            //stacks.Push2(5);
            //stacks.Push1(9);
            //Console.WriteLine("Push2: 11 pushed to stack 2...");
            //stacks.Push2(11);
            //stacks.Pop1();
            //stacks.Pop2();

            //stacks.PrintFinalStatus();

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ----------------------------------------------test for BFS-------------------------------------- -
            //Console.WriteLine("---------------------------------------------- test for BFS ---------------------------------------");

            //// در این کد یک درخت پیش فرض ساخته شده است ولی می توان یک درخت ساخت و این کلاس را روی آن اجرا کرد

            //Node_BinaryTree root_BFS = BinaryTree_BFS.BuildExampleTree();
            //List<int> traversal_BFS = BinaryTree_BFS.BFS(root_BFS);

            //Console.WriteLine("Level Order (BFS): ");
            //foreach (int val in traversal_BFS)
            //{
            //    Console.Write(val + " ");
            //}
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

            //// ---------------------------------------------- test for pre and in order Non-returnable --------------------------------------------
            //Console.WriteLine("---------------------------------------------- test for pre and in order Non-returnable --------------------------------------------");

            //BinaryTree_Non_returnable tree = new BinaryTree_Non_returnable();

            //// این اعداد از 10 تا 90 هستند که به طور به هم ریخته وارد شدند
            //// این تابع یک درخت به اصطلاح ( بی اس تی ) می سازد برای تست کد در انتها
            //int[] values = { 50, 30, 20, 10, 40, 70, 60, 80, 90 };
            //foreach (int val in values)
            //{
            //    tree.Insert(val);
            //}


            //Console.WriteLine("In-Order Traversal: ");
            //tree.InOrderTraversal(tree.Root);  //out put: 10 20 30 40 50 60 70 80 90 (BST)
            //Console.WriteLine();

            //Console.WriteLine("Pre-Order Traversal: ");
            //tree.PreOrderTraversal(tree.Root);  //out put: 50 30 20 10 40 70 60 80 90
            //Console.WriteLine();

            //Console.WriteLine();
            //Console.WriteLine("enter for next...");
            //Console.ReadLine();

        }
    }
}