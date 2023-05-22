using System;


namespace QuestionAttempts
{
    public class Class2
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
            public AVLNode root;

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

                if (balance > 1 && value < node.left.value)  // sola ağırlık var ise yani sol-sol durumu varsa sağa döndürüyoruz
                {
                    return RotateRight(node);
                }

                if (balance > 1 && value > node.left.value)  // sola ağırlık var ve sol alt ağaç sağa ağırlıklı(sol-sağ durumu) varsa önce sola sonra sağa döndürüyoruz
                {
                    node.left = RotateLeft(node.left);
                    return RotateRight(node);
                }

                if (balance < -1 && value > node.right.value) // sağa ağırlık var ise yani sağ-sağ durumu varsa sola döndürüyoruz
                {
                    return RotateLeft(node);
                }

                if (balance < -1 && value < node.right.value) // sağa ağırlık var ve sağ alt ağaç sola ağırlıklı(sağ-sol durumu) varsa önce sağa sonra sola döndürüyoruz
                {
                    node.right = RotateRight(node.right);
                    return RotateLeft(node);
                }

                return node;
            }

            public AVLNode Delete(AVLNode node, AVLNode delNode)
            {
                if (node == null)
                {
                    return node;
                }

                if (delNode.value < node.value)
                {
                    node.left = Delete(node.left, delNode);
                }
                else if (delNode.value > node.value)
                {
                    node.right = Delete(node.right, delNode);
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
                        AVLNode temp = MinValueNode(node.right);    // silmek istediğimiz node u en küçükle yer değiştirip
                        node.value = temp.value;                    // node u uçtan siliyoruz
                        node.right = Delete(node.right, temp);
                    }
                }

                if (node == null)
                {
                    return node;
                }

                node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

                int balance = GetBalance(node);

                if (balance > 1 && GetBalance(node.left) >= 0) // sola ağırlık var ise yani sol-sol durumu varsa sağa döndürüyoruz
                {
                    return RotateRight(node);
                }

                if (balance > 1 && GetBalance(node.left) < 0)  // sola ağırlık var ve sol alt ağaç sağa ağırlıklı(sol-sağ durumu) varsa önce sola sonra sağa döndürüyoruz
                {
                    node.left = RotateLeft(node.left);
                    return RotateRight(node);
                }

                if (balance < -1 && GetBalance(node.right) <= 0) // sağa ağırlık var ise yani sağ-sağ durumu varsa sola döndürüyoruz
                {
                    return RotateLeft(node);
                }

                if (balance < -1 && GetBalance(node.right) > 0)  // sağa ağırlık var ve sağ alt ağaç sola ağırlıklı(sağ-sol durumu) varsa önce sağa sonra sola döndürüyoruz
                {
                    node.right = RotateRight(node.right);
                    return RotateLeft(node);
                }

                return node; // ağacın son halini döndürüyoruz
            }

            private AVLNode MinValueNode(AVLNode node)  // silme işlemini yapmak için ağacın en küçük değerli düğümünü buluyoruz
            {
                AVLNode current = node;

                while (current.left != null)    // küçük değerliler sol tarafta olduğu için sola gidiyoruz
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

            private int GetBalance(AVLNode node)    // yükseklikleri kullanarak silme ve ekleme işlemlerinde
            {                                       // dengesizlik olup olmadığını kontrol etmek için aradaki farkı alıyoruz
                if (node == null)
                {
                    return 0;
                }

                return GetHeight(node.left) - GetHeight(node.right);
            }

            private AVLNode RotateRight(AVLNode node) // sağ sağ durumunda sola döndürme || ağacın o sırada incelenen kısmında sağa doğru eğilim varsa yapıyoruz
            {
                AVLNode newRoot = node.left;          // kökü güncelleyerek yeni kökü belirleme
                AVLNode temp = newRoot.right;         // yeni kökün sağ çocuğunu tempte saklama

                newRoot.right = node;                
                node.left = temp;                     

                node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));           // eski kökün yüksekliğini güncelleme
                newRoot.height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));  // yeni kökün yüksekliğini güncelleme

                return newRoot;
            }

            private AVLNode RotateLeft(AVLNode node) // sol sol durumunda sağa döndürme || ağacın o sırada incelenen kısmında sola doğru eğilim varsa yapıyoruz
            {
                AVLNode newRoot = node.right;        // kökü güncelleyerek yeni kökü belirleme
                AVLNode temp = newRoot.left;        

                newRoot.left = node;               
                node.right = temp;                  

                node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));           // eski kökün yüksekliğini güncelleme
                newRoot.height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));  // yeni kökün yüksekliğini güncelleme

                return newRoot;
            }

            public void InOrder(AVLNode node)
            {
                if (node != null)
                {
                    InOrder(node.left);
                    Console.Write(node.value + " ");
                    InOrder(node.right);
                }
            }

            public bool IsBalanced(AVLNode root)    // ağacın sırayla sol ve sağ çocuklarının yüksekliğine bakarak dengeli olup olmadığını kontrol ediyoruz
            {
                if (root == null)
                {
                    return true;
                }

                int leftHeight = Height(root.left);
                int rightHeight = Height(root.right);

                if (Math.Abs(leftHeight - rightHeight) <= 1 &&
                    IsBalanced(root.left) && IsBalanced(root.right))
                {
                    return true;
                }

                return false;
            }
            public int Height(AVLNode node)         // Sağ ve sol çocukkların yükseklik farkı birden fazla mı diye yükseklik buluyoruz
            {
                if (node == null)
                {
                    return 0;
                }

                return 1 + Math.Max(Height(node.left), Height(node.right));
            }
            
            public void TraverseForDeleteWeak(AVLNode node) // Bütün ağacı inorder gezerken gezilen nodeların değerlerini kontrol edip weak prime ise silme işlemi yapıyoruz
            {
                if (node != null)
                {
                    TraverseForDeleteWeak(node.left);
                    IfWeakDelete(node);
                    TraverseForDeleteWeak(node.right);
                }
            }

            public void IfWeakDelete(AVLNode node)      
            {
                if (IsWeakPrime(node.value))
                {
                    Delete(this.root, node);
                }
            }
            public void IfWeakPrint(AVLNode node)
            {
                if(node != null)
                {
                    IfWeakPrint(node.left);
                    if (IsWeakPrime(node.value))
                    {
                        Console.Write(node.value + " ");
                    }
                    IfWeakPrint(node.right);
                }
            }
            public bool IsPrime(int num)
            {
                if (num < 2)
                {
                    return false;
                }

                for (int i = 2; i <= Math.Sqrt(num); i++)
                {
                    if (num % i == 0)
                        return false;
                }

                return true;
            }

            public bool IsWeakPrime(int num)
            {
                if (!IsPrime(num) || num == 2)
                {
                    return false;
                }

                int previousPrime = num - 1;
                int nextPrime = num + 1;

                while(!IsPrime(previousPrime)) // Burada elimizdeki asaldan önceki asalı bulana kadar değer küçültüyoruz
                {
                    previousPrime--;
                }

                while (!IsPrime(nextPrime))    // Burada ise aynı işlemi değer büyüterek, büyük asalı bulmak için yapıyoruz
                {
                    nextPrime++;
                }

                int control = (nextPrime + previousPrime) / 2;  // Zayıf asal formülü
                if(control < num)
                {
                    return false;
                }
                return true;
            }
        }

        //public static void Main()
        //{
        //    Console.WriteLine("Kaç sayı gireceğinizi yazın:");
        //    int n = Convert.ToInt32(Console.ReadLine());
        //    AVLTree tree = new AVLTree();
        //    Console.WriteLine("Sayıları girin:");
        //    for (int i = 0; i < n; i++)
        //    {
        //        int sayi = Convert.ToInt32(Console.ReadLine());
        //        tree.root = tree.Insert(tree.root, sayi);
        //    }

        //    Console.Write("\nInorder düzende ağaç: ");
        //    tree.InOrder(tree.root);
        //    Console.WriteLine("\nAğaç dengeli mi? " + tree.IsBalanced(tree.root));
        //    Console.Write("\nAğaçtaki zayıf asallar:");
        //    tree.IfWeakPrint(tree.root);
        //    Console.Write("\n\nZayıf asallar silindikten sonra inorder düzende ağaç: ");
        //    tree.TraverseForDeleteWeak(tree.root);
        //    tree.InOrder(tree.root);
        //    Console.WriteLine("\nAğaç dengeli mi? " + tree.IsBalanced(tree.root));
        //}
    }
}
