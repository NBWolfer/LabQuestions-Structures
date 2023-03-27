namespace QuestionAttempts
{
    public class Lab3
    {
        public class Node
        {
            public int value;
            public Node left;
            public Node right;
            public Node(int value)
            {
                this.value = value;
                left = null;
                right = null;
            }
        }

        public class Tree
        {
            internal Node root;
        }

        public class Func
        {
            public static Node AddNode(Node node, int val)
            {
                if (node == null)
                {
                    return new Node(val);
                }
                if (node.value > val)
                {
                    node.left = AddNode(node.left, val);
                }
                else
                {
                    node.right = AddNode(node.right, val);
                }
                return node;
            }

            public static Node DeleteNode(Node root, int val)
            {
                if (root == null)
                {
                    return root;
                }

                if (val < root.value)
                {
                    root.left = DeleteNode(root.left, val);
                }
                else if (val > root.value)
                {
                    root.right = DeleteNode(root.right, val);
                }
                else
                {
                    if (root.left == null)      // Önce sol tarafı kontrol ediyoruz çünkü ağacımız ikili arama ağacı
                    {
                        return root.right; // sol alt ağaç yoksa, sağ alt ağaç kök düğüm
                    }
                    else if (root.right == null)
                    {
                        return root.left; // sağ alt ağaç yoksa, sol alt ağaç kök düğüm
                    }

                    Node smallestNode = FindSmallestNode(root.right);   // En küçük değeri silmek istediğimiz düğümün yerine yazıyoruz
                    root.value = smallestNode.value;
                    root.right = DeleteNode(root.right, smallestNode.value);    // Daha sonra sanki en uçtaki düğümü siler gibi en küçük düğümün eski konumunu siliyoruz.
                }

                return root;
            }

            private static Node FindSmallestNode(Node node) // Silerken sildiğimiz değerin yerine o düğümün alt ağaçlarına bakarak en küçük değeri dönüyoruz.
            {
                Node current = node;

                while (current.left != null)
                {
                    current = current.left;
                }

                return current;
            }

            public static void PrintTree(Node root)
            {
                if (root == null)
                {
                    return;
                }
                PrintTree(root.left);
                Console.Write(root.value + " ");
                PrintTree(root.right);
            }
            static int counter=0;
            public static int FindNodeNumInRange(Node root, int min, int max) // Aralıktaki nodeların sayısını buluyor.
            {
                if(root == null)
                {
                    return 0;
                }
                FindNodeNumInRange(root.left, min, max);        // Burada infix dolaşarak her koşulu sağlayan node a denk geldiğinde counter ı 1 arttırıyor.
                if(root.value >= min && root.value <= max)
                {
                    counter++;
                }
                FindNodeNumInRange(root.right, min, max);
                return counter;
            }
           
            public static Node DeleteNodesInRange(Node root,int min,int max) { // Ağacı postfix gezerek en sondan başa dönerken aralık dışı değerleri siliyor
                if (root == null)
                {
                    return root;
                }
                root.left=DeleteNodesInRange(root.left, min, max);
                root.right=DeleteNodesInRange(root.right, min, max);
                if (!(root.value >= min && root.value <= max))              // Burada da her çağrıda o anki root değeri ile DeleteNode fonksiyonunu çağırarak silme işlemi yapıyoruz.
                {
                    root = DeleteNode(root, root.value);
                }
                return root;
            }
        }

        //public static void Main()
        //{
        //    Tree tree = new Tree();
            
        //    // A)
        //    Console.Write("Kaç elemanlı bir dizi istiyorsunuz:");
        //    int[] nums = new int[Convert.ToInt32(Console.ReadLine())];
        //    Random rand = new Random();
        //    int temp;
        //    for(int i=0; i<nums.Length; i++)
        //    {
        //        temp=nums[i] = rand.Next(-1000,1000);
        //        tree.root = Func.AddNode(tree.root, temp);
        //    }
        //    Console.Write("Ağacın son hali: ");
        //    Func.PrintTree(tree.root);

        //    // B)
        //    int max, min;
        //    Console.Write("\nÜst ve Alt sınırları girin;\nMax:");
        //    max = Convert.ToInt32(Console.ReadLine());
        //    Console.Write("Min:");
        //    min = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("Aralık dışında kalan düğümlerin sayısı:"+(nums.Length-Func.FindNodeNumInRange(tree.root,min,max)));

        //    // C)
        //    Console.WriteLine("\nAralık dışındaki düğümler siliniyor...");
        //    tree.root = Func.DeleteNodesInRange(tree.root, min, max);
        //    Console.Write("Ağacın son hali: ");
        //    Func.PrintTree(tree.root);

        //}
    }
}
