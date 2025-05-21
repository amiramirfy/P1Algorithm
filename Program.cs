using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestManager
{
    class NodeM
    {
      
        public int ID;
        public int priority;
       
        public NodeM(int data, int pri)
        {

          
            this.ID = data;
            this.priority = pri;
        }
    }
    class NodeT
    {
        public NodeT parent;
        public NodeT leftChild;
        public NodeT rightChild;
        public int ID;
        public string name;
        public NodeT()
        {
            parent = null;
            leftChild = null;
            rightChild = null;
            ID = 0;
        }
        public NodeT(int data,string name)
        {

            parent = null;
            leftChild = null;
            rightChild = null;
            this.ID = data;
            this.name = name;
        }
    }

///****************
    class Tree
    {
        public NodeT root;
       
      
        public Tree()
        {
          
            root = null;
        }
        public Tree(NodeT root)
        {
           
            this.root = root;
        }
        public void InsertRequest(NodeT r)
        {
            if (root == null)
                root = r;
            else
            {
                NodeT q = root;
                while (true)
                {
                    if (q.ID > r.ID)
                    {
                        if (q.leftChild == null)
                        {
                            q.leftChild = r;
                            r.parent = q;
                            break;
                        }
                        else
                            q = q.leftChild;
                    }
                    else
                    {
                        if (q.rightChild == null)
                        {
                            q.rightChild = r;
                            r.parent = q;
                            break;
                        }
                        else
                            q = q.rightChild;
                    }
                }


            }
        }

     
        
        // preorder print
        public void PreorderPrint(NodeT p)
        {
            if (p == null)
                return;
            PreorderPrint(p.leftChild);
            PreorderPrint(p.rightChild);
            Console.WriteLine(p.name+": "+p.ID);
          
        }
      
        // minimum in tree 
        public NodeT Minimum(NodeT p)
        {
            while (p.leftChild != null)
            {
                p = p.leftChild;
            }
            return p;
        }
        // search node
        public NodeT SearchRequest(int x)
        {
            NodeT p = root;
            while (p != null)
            {
                if (p.ID > x)
                {
                    p = p.leftChild;
                }
                else if (p.ID < x)
                {
                    p = p.rightChild;
                }
                else
                    return p;
            }
            return null;
        }
      

        //transplan
        public void TransPlant(NodeT p, NodeT n)
        {
            if (p.parent == null)
                root = n;
            else if (p == p.parent.rightChild)
                p.parent.rightChild = n;
            else
                p.parent.leftChild = n;
            if (n != null)
                n.parent = p.parent;
        }
        // delete a node from tree and max
        public void DeleteRequest(NodeT x,MaxHeap m)
        {
            m.DeleteRequestMaxHeap(x.ID);
            if (x.leftChild == null)
                TransPlant(x, x.rightChild);
            if (x.rightChild == null)
                TransPlant(x, x.leftChild);
            else
            {
                NodeT y = Minimum(x.rightChild);
                if (y.parent != x)
                {
                    TransPlant(y, y.rightChild);
                    y.rightChild = x.rightChild;
                    y.rightChild.parent = y;
                }
                TransPlant(x, y);
                y.leftChild = x.leftChild;
                y.leftChild.parent = y;
            }

        }

       
       
       
    }
    //**************
    class MaxHeap
    {
        public List<NodeM> maxHeap = new List<NodeM>();
        public int heapSize = 0;
        public MaxHeap()
        {
            maxHeap.Add(new NodeM(-1,-1));
            heapSize=0;
        }
        //
        public void IncerecePriority(int id, int key)
        {
            int count = 0;
            foreach(var item in maxHeap)
            {
                if(item.ID==id)
                {
                    break;
                }
                else
                {
                    count++;
                }
            }
            if(count>heapSize)
            {
                ;
            }
            else
            {
                if (key < maxHeap[count].priority)
                {
                    //print an error
                    Console.WriteLine("This is decrecing not increce next time read before selecting!");
                }
                maxHeap[count].priority = key;
                while (count > 1 && maxHeap[MaxHeapParentIndex(count)].priority < maxHeap[count].priority)
                {
                    NodeM temp = maxHeap[count];
                    maxHeap[count] = maxHeap[MaxHeapParentIndex(count)];
                    maxHeap[MaxHeapParentIndex(count)] = temp;
                    count = MaxHeapParentIndex(count);
                }
            }
           
        }
        //
        public void DeleteRequestMaxHeap(int id)
        {
            int count = 0;
            foreach (var item in maxHeap)
            {
                if (item.ID == id)
                {
                    break;
                }
                else
                {
                    count++;
                }
            }
            if (count > heapSize)
            {
                ;
            }
            else
            {
                maxHeap[count].priority = -1000;
                MaxHeapify(count);
                int count2 = 0;
                foreach (var item in maxHeap)
                {
                    if (item.ID == id)
                    {
                        break;
                    }
                    else
                    {
                        count2++;
                    }
                }
                maxHeap.RemoveAt(count2);
                heapSize--;
               
            }
           
           
        }
        
        //
        public NodeM HighestPriority(Tree t,MaxHeap m)
        {
            if(heapSize<1)
            {
                Console.WriteLine("hichi nist");
                return null;
            }
            else
            {
                NodeM max = m.maxHeap[1];
                m.maxHeap[1] = m.maxHeap[heapSize];
                heapSize--;
                MaxHeapify(1);
                NodeT p = new NodeT(max.ID, "not important");

                t.DeleteRequest(p,m);
                return max;
            }
        }
        //
        public void IncereceKey(int i, int key)
        {
            if (key < maxHeap[i].priority)
            {
                //print an error
                Console.WriteLine("This is decrecing not increce next time read before selecting!!!!!");
            }
            maxHeap[i].priority = key;
            while (i > 1 && maxHeap[MaxHeapParentIndex(i)].priority < maxHeap[i].priority)
            {
                NodeM temp = maxHeap[i];
                maxHeap[i] = maxHeap[MaxHeapParentIndex(i)];
                maxHeap[MaxHeapParentIndex(i)] = temp;
                i = MaxHeapParentIndex(i);
            }
        }
        public void InsertHeap(int key,int ID)
        { 
            heapSize++;
            maxHeap.Add(new NodeM(ID,-1000));
            IncereceKey(heapSize, key);

        }
        //
        public void PrintMaxHeap()
        {
            //i is level count
            double i = 1;
            int count = 1;
            Console.Write( maxHeap[(int)i].priority+",");
            for ( count=1 ; count<heapSize;count++)
            {
                if (2*count > heapSize)
                    break;
                i = 2 * count;
                Console.Write( maxHeap[(int)i].priority+",");
               
                i = (2 * count )+ 1;
                if (i > heapSize)
                    break;
                Console.Write( maxHeap[(int)i].priority+",");


            }
        }
        private int MaxHeapParentIndex(int i)
        {
            return i / 2;
        }
        private int MaxHeapLeftChilde(int i)
        {
            return 2 * i;
        }
        private int MaxHeapRightChilde(int i)
        {
            return (2 * i) + 1;
        }
        private void MaxHeapify(int i)
        {
            int l = MaxHeapLeftChilde(i);
            int r = MaxHeapRightChilde(i);
            int largest = 0;
            if (heapSize >= l && (maxHeap[l].priority > maxHeap[i].priority))
                largest = l;
            else
                largest = i;
            if (heapSize >= r && (maxHeap[r].priority > maxHeap[i].priority))
            {
                if (maxHeap[r].priority > maxHeap[largest].priority)
                    largest = r;
            }
            else
                largest = i;
            if (largest != i)
            {
                NodeM temp = maxHeap[i];
                maxHeap[i] = maxHeap[largest];
                maxHeap[largest] = temp;
                MaxHeapify(largest);
            }

        }
       
    }
    class Program
    {
        static void Main(string[] args)
        {
             
           
         
            Tree tree = new Tree();
            MaxHeap maxH = new MaxHeap();
            while (true)
            {
               
                Console.WriteLine("please choose your command: ");
                Console.WriteLine("1: build Tree ");
                Console.WriteLine("2: Insert to Tree and MaxHeap ");
                Console.WriteLine("3: Delete a node from Tree ");
                Console.WriteLine("4:print Maxheap ");
                  Console.WriteLine("5:increace priority by ID ");
                Console.WriteLine("6:search tree by ID ");
                  Console.WriteLine("7:Print Tree preorder "); 
                Console.WriteLine("8: Quit");
              
                int key = Convert.ToInt32(Console.ReadLine());
                if (key == 8)
                    break;
                else if (key == 1)
                {
                    int n;
                    Console.WriteLine("how many nodes do you want?");
                    n = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < n; i++)
                    {
                        int c;
                           int pri;
                        string name;
                        Console.WriteLine("Enter the Id");
                        c = Convert.ToInt32(Console.ReadLine());
                         Console.WriteLine("Enter the name");
                        name = Console.ReadLine();
                         Console.WriteLine("Enter priority");
                        pri = Convert.ToInt32(Console.ReadLine());
                      NodeT p = new NodeT(c,name);
                        tree.InsertRequest(p);
                        maxH.InsertHeap(pri,c);
                    }
                    Console.WriteLine("Success!");
                }
                else if (key == 2)
                {
                   int c;
                     int pri;
                  string name;
                Console.WriteLine("Enter the Id");
                c = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the name");
                name = Console.ReadLine();
                    Console.WriteLine("Enter priority");
                pri = Convert.ToInt32(Console.ReadLine());
                NodeT p = new NodeT(c,name);
                tree.InsertRequest(p);
                maxH.InsertHeap(pri,c);
                }
                else if (key == 3)
                {
                    int c;
                    Console.WriteLine("Enter the ID ");
                    c = Convert.ToInt32(Console.ReadLine());
                    NodeT p = tree.SearchRequest(c);
                    tree.DeleteRequest(p,maxH);
                  
                }
                else if (key == 4)
                {
                   maxH.PrintMaxHeap();
                }
                else if (key == 5)
                {
                    int c;
                    Console.WriteLine("Enter the increase value");
                    c = Convert.ToInt32(Console.ReadLine());
                     int d;
                    Console.WriteLine("Enter the ID");
                    d = Convert.ToInt32(Console.ReadLine());
                    maxH.IncerecePriority(d,c);
                   

                }
                else if (key == 6)
                {
                     int c;
                    Console.WriteLine("Enter the ID value");
                    c = Convert.ToInt32(Console.ReadLine());
                    
                    NodeT find =tree.SearchRequest(c);
                    Console.WriteLine("the name of id: "+find.name);
                }
                else if (key == 7)
                {
                    NodeT p = tree.root;
                   tree.PreorderPrint(p);
                }
               

            }
        }
        }
    }

