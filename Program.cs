using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProje3
{
    // Düğüm Sınıfı
    class TreeNode
    {
        public Mahalle data;
        public TreeNode leftChild;   //bir düğümün tuttuğu değişkenlerin tanımlanması
        public TreeNode rightChild;
                
        public TreeNode(Mahalle data)  //constructor
        {
            this.data = data;
            leftChild = null;   //düğümün değişkenlerinin ilk değerlerini alması
            rightChild = null;
        }

        public void displayNode() {  //mahalle nesnesinin içeriğinin yazdırılması
            int sayac = 1;
            Console.WriteLine("\nMahalle Adı: "+data.mahalleAdi+" mahallesi\n");
            foreach(List<Yemek> liste in data.siparislerListesi) //mahallenin içerdiği siparişler listesinin dolaşılması
            {
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("{0}.sipariş",sayac);
                foreach(Yemek yemekNesne in liste) //siparişler listesinin içerdiği her bir siparişin dolaşılması
                {
                    Console.WriteLine(yemekNesne.ToString());  //her bir sipaişteki yemek nesnesi bilgilerinin yazdırılması
                }
                sayac++;
            }
            Console.WriteLine("\n*******************************************************************************");
        }
    }

    // Agaç Sınıfı
    class Tree
    {
        private TreeNode root;
        //variables for traverse statistics
        public int maxDepth; //ağacın derinlik değişkeni
        public ArrayList toplamArananYemek = new ArrayList();   //bir yemeğin ağaç boyunca her mahallede kaç adet olarak bulunduğunu tutan liste
        public Tree() { root = null; }

        public TreeNode getRoot()
        { return root; }

        // Agacın inOrder Dolasılması
        public void inOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.displayNode();
                inOrder(localRoot.rightChild);
            }
        }

        public void adetBulma(TreeNode localRoot, int toplamAdet, string arananYemek) //bir yemeğin toplam kaç adet sipariş edildiğini bulduran metod
        {
            if (localRoot != null)
            {
                adetBulma(localRoot.leftChild, toplamAdet, arananYemek);
                toplamAdet = 0;
                foreach (List<Yemek> liste in localRoot.data.siparislerListesi)
                {
                    foreach (Yemek yemekNesne in liste)
                    {
                        //yemeğin ağaçtaki her bir yemek nesnesi ile karşılaştırılması
                        if (string.Compare(yemekNesne.yemekAdi, arananYemek, StringComparison.OrdinalIgnoreCase) == 0)  
                        {
                            toplamAdet += yemekNesne.yemekAdet;
                            yemekNesne.yemekBirimFiyat *= 0.9; //yemeğin geçtiği her listede yemeğin birim fiyatına %10 indirim uygulanması
                        } 
                    }
                }            
                toplamArananYemek.Add(toplamAdet); //yemeğin her bir mahalledeki toplam sipariş adedinin listeye eklenmesi
                adetBulma(localRoot.rightChild, toplamAdet, arananYemek);
            }             
        }

        // Agaca bir dügüm eklemeyi saglayan metot
        public void insert(Mahalle newData)
        {
            TreeNode newNode = new TreeNode(newData);
            
            if (root == null)  //ağacın boş olduğu durum
                root = newNode;
            else //ağaçta kök bulunduğu durum
            {
                TreeNode current = root;
                TreeNode parent;
                while (true)
                {
                    parent = current;
                    if (String.Compare(newData.mahalleAdi,current.data.mahalleAdi)==-1) //gelen node'un current node'dan küçük olması durumu
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;  // ağacın soluna ekleme yapma
                            return;
                        }
                    }
                    else  //gelen node'un current node'dan büyük olması durumu
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;  // ağacın sağına ekleme yapma
                            return;
                        }
                    }
                } // end while
            } // end else not root
        } // end insert()


        private void traverseTreeForInfo(TreeNode node, int depth)  //ağacın maksimum derinliğini bulduran metod
        {
            if (node != null)
            {
                depth++;

                if (depth > maxDepth)
                    maxDepth = depth; //max derinliği güncelleme

                traverseTreeForInfo(node.leftChild, depth); //traverse left sub-tree
                traverseTreeForInfo(node.rightChild, depth); //traverse right sub-tree
            }
        }
        public void findTreeInfo(TreeNode rootNode) //root'tan -1 derinlik alarak başlayıp ağacın maksimum derinliğini bulan metod
        {
            traverseTreeForInfo(rootNode, -1);
            Console.WriteLine("\nDepth of the tree: " + maxDepth);
        }

        public TreeNode find(string aranan) //ağaçta aranan bir mahalleyi bulduran metod (küçük harf büyük harf dikkate alınmaz)
        {
            TreeNode etkin = root;
            while (String.Compare(etkin.data.mahalleAdi, aranan, StringComparison.OrdinalIgnoreCase) !=0)  
            {
                if (String.Compare(aranan, etkin.data.mahalleAdi)==-1) //aranan mahalle adının etkin node'dakinden küçük olduğu durum
                    etkin = etkin.leftChild;
                else  //aranan mahalle adının etkin node'dakinden büyük olduğu durum
                    etkin = etkin.rightChild;
                if (etkin == null) return null;
                
            }
            return etkin;
        }
    }

    class HeapNode
    {
        public int nufus;
        public string mahalleAdi;                      //heap değişkenlerinin tanımlanması
        public HeapNode(int nufus, string mahalleAdi) //constructor
        {
            this.nufus = nufus;               //heap nesnesi değişkenlerinin ilk değerlerini alması
            this.mahalleAdi = mahalleAdi;
        }

        public override string ToString()  //heap'teki mahalle bilgilerini (mahalle adı, nüfus) yazdıran metod
        {
            string data = string.Format("Mahalle Adı: {0} , Nüfus: {1}", mahalleAdi, nufus);
            return data;
        }

    }
    class Heap
    {
        private HeapNode[] nodes;      //heap node'larını tutan nodes isimli bir dizi tanımlanması
        int sıra = 0;                //heap'in o anki boyutunu belirtir
        public Heap(int boyut)   //constructor
        {
            nodes = new HeapNode[boyut];  //gelen "boyut" değeri boyutunda nodes dizisi açılması
        }
        public void updateInsert(int index) //heap'in güncel boyutunu parametre alarak ekleme işleminin yapıldığı metod
        {
            int parent = (index - 1) / 2;
            HeapNode bottom = nodes[index];
            while (index > 0 &&
            nodes[parent].nufus < bottom.nufus)
            {
                nodes[index] = nodes[parent]; // aşağı at
                index = parent; // indeksi arttır
                parent = (parent - 1) / 2; // bir üst ebeveyni seç
            }
            nodes[index] = bottom;
        }
        public void insert(int nufus, string mahalleAdi) //heap'in önceki boyutunu 1 arttırıp updateInsert metodunu çağırarak ekleme yapılan metod
        {
            HeapNode ekle = new HeapNode(nufus, mahalleAdi);
            nodes[sıra] = ekle;
            updateInsert(sıra++);
        }
        public void updateRemove(int index) //heap'in güncel boyutunu parametre alarak ekleme işleminin yapıldığı metod
        {
            int largerChild;  //nüfusu en büyük olan node'un indexi
            HeapNode top = nodes[index];
            while (index < sıra / 2)
            {
                int leftChild = 2 * index + 1;  //leftchild'ın nodes dizisindeki güncel yerinin bulunması
                int rightChild = leftChild + 1; //rightchild'ın nodes dizisindeki güncel yerinin bulunması

                if (rightChild < sıra && nodes[leftChild].nufus < nodes[rightChild].nufus) //leftchild'ın nüfusunun rightchild'ın nüfusundan küçük olduğu durum
                {
                    largerChild = rightChild;
                }
                else //leftchild'ın nüfusunun rightchild'ın nüfusundan küçük olmadığı durum
                {
                    largerChild = leftChild;
                }
                if (top.nufus >= nodes[largerChild].nufus)
                {
                    break;
                }
                nodes[index] = nodes[largerChild]; 
                index = largerChild;
            }
            nodes[index] = top;
        }

        // max nüfusa sahip mahalleyi silme
        public HeapNode remove()  //heap'in önceki boyutunu 1 azaltıp updateRemove metodunu çağırarak silme yapılan metod
        {
            HeapNode root = nodes[0];
            nodes[0] = nodes[--sıra];
            updateRemove(0);
            return root;
        }

        public void displayHeap()  //heap'in bilgilerinin yazdırılması
        {
            Console.WriteLine("Nüfuslarına göre Bornova’daki 10 mahalle"); // array format
            Console.WriteLine("----------------------------------------");
            for (int m = 0; m < sıra; m++)  //heap'in dolaşılması
            {
                if (nodes[m] != null)
                    Console.WriteLine(nodes[m].ToString() + "");
                else
                    Console.WriteLine("--");
            }
        }
    }

    class Program
    {

        // Advanced Sorting-QuickSort
        private static void Quick_Sort(int[] arr, int left, int right)  //integer tipinde veri tutan bir dizi, sol indis ve sağ indis alan sıralama metodu
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);  //dizinin pivot elemanının buldurulması

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1); //pivotun solunda en az 2 elemanın bulunduğu durum için pivotun solu ve sağı için
                                                      //tekrardan sıralama metodunun çağırılması
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right); //pivotun sağında en az 2 elemanın bulunduğu durum için pivotun solu ve sağı için
                                                       //tekrardan sıralama metodunun çağırılması
                }
            }
        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {                                           
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;

                }
                else
                {
                    return right;
                }
            }
        }

        static Random random = new Random();  // Rastgele sayı üretebilmek için Random sınıfından bir nesne oluşturuldu.

        static void Main(string[] args)
        {
            // Gerekli diziler oluşturuldu
            string[] mahalleAdlari = { "Evka3", "Özkanlar", "Atatürk", "Erzene", "Kazımdirik" };
            string[] yemekMenusu = { "Türlü", "Pilav","Ayran" ,"Pizza","Hamburger","Köfte","Makarna","Tantuni",
                "Baklava","Kola","Kek"};
            int[] yemekFiyatlar = {15,10,3,16,14,10,7,8,6,4,5 };

            Tree yemekSiparisAgaci = new Tree();  // Yemek Sipariş Ağacının oluşturulması 
            Yemek yemek;
            List<Yemek> siparisBilgileri;
            ArrayList siparislerListesi;
            Mahalle mahalle;                     

            for(int i=0; i<mahalleAdlari.Length; i++)  //mahalle sayısı kadar dönen döngü
            {
                int randomSiparisSayisi = random.Next(5, 11);  //bir mahalle için random sipariş sayısı belirleme (5-10)
                siparislerListesi = new ArrayList();                

                for (int j = 0; j < randomSiparisSayisi; j++)  //sipariş sayısı kadar dönen döngü
                {
                    int randomYemekSayisi = random.Next(3,6); //bir sipariş için random yemek sayısı belirleme (3-5)
                    siparisBilgileri = new List<Yemek>();
                    int[] randomIndex = new int[5];
                    for (int k = 0; k < randomYemekSayisi; k++)  //yemek sayısı kadar dönen döngü
                    {
                        int randomYemekAdet = random.Next(1, 9); //random yemek adedi seçilmesi (1-8)
                        
                        int randomMenu = random.Next(11);  //yemek listesinden random yemek seçilmesi
                        while (randomIndex.Contains(randomMenu))  //bir siparişe 2 kere aynı ürünün engellenmesini kontrol eden döngü
                        {
                            randomMenu = random.Next(11);
                        }
                        randomIndex[k] = randomMenu;

                        yemek = new Yemek(yemekMenusu[randomMenu],randomYemekAdet,yemekFiyatlar[randomMenu]); //seçilen yemek bilgileri ile yemek nesnesi oluşturma
                        siparisBilgileri.Add(yemek); //yemeğin siparişe eklenmesi
                    }
                    siparislerListesi.Add(siparisBilgileri); //siparişin siparişler listesine eklenmesi 
                }
                mahalle = new Mahalle(mahalleAdlari[i], siparislerListesi); //oluşan siparişler listesini alarak mahalle nesnesi oluşturma
                yemekSiparisAgaci.insert(mahalle); //oluşan mahalleyi ağaca ekleme
            }
            
            yemekSiparisAgaci.inOrder(yemekSiparisAgaci.getRoot());  // ağaçtaki tüm bilgileri ekrana listeleyen metodun çağrımı
            yemekSiparisAgaci.findTreeInfo(yemekSiparisAgaci.getRoot());  // ağacın derinliğini bulduran metodun çağrımı

            Console.Write("\nLütfen mahalle adı giriniz: ");
            string arananMahalle = Console.ReadLine();      //aranan mahalleyi girdi olarak almak
            Console.WriteLine();

            TreeNode tempNode = yemekSiparisAgaci.find(arananMahalle);  //girilen mahallenin ağaçta olup olmadığının kontrolü

            while (tempNode == null)  //aranan mahalle ağaçta yoksa tekrardan girdi istenmesi için kontrol
            {
                Console.WriteLine("Aradığınız mahalle bulunamadı");
                Console.Write("Lütfen mahalle adı giriniz: ");
                arananMahalle = Console.ReadLine();
                tempNode = yemekSiparisAgaci.find(arananMahalle);
            }

            foreach (List<Yemek> liste in tempNode.data.siparislerListesi)
            {
                double siparisTutar = 0;
                foreach (Yemek yemekNesne in liste)
                {
                    siparisTutar += yemekNesne.yemekAdet * yemekNesne.yemekBirimFiyat;  //her bir siparişlerlistesi elemanı için sipariş tutarı hesaplama
                }
                
                if (siparisTutar > 150) //sipariş tutarının 150'den büyük olduğu siparişleri belirleme
                {
                    foreach (Yemek yemekNesne in liste)
                    {
                        Console.WriteLine(yemekNesne.ToString());
                        
                    }
                    Console.WriteLine("--------------------------------------------------------------------");
                }
            }

            Console.Write("\nLütfen yiyecek/içecek adı giriniz: ");
            string arananYemek = Console.ReadLine();  //aranan yemeği girdi olarak almak
            Console.WriteLine();

            int toplamAdet = 0;
            yemekSiparisAgaci.adetBulma(yemekSiparisAgaci.getRoot(), toplamAdet, arananYemek);  //aranan yemeğin ağaçta olup olmadığının kontrolü

            int toplamYemekAdet = 0;
            foreach(int i in yemekSiparisAgaci.toplamArananYemek)  
            {
                toplamYemekAdet += i;  // Adı verilen bir yiyecek/içeceğin tüm ağaçta kaç adet sipariş verildiğinin bulunması
            }

            Console.WriteLine("Toplam {0} Adedi: {1}",arananYemek,toplamYemekAdet);
            Console.WriteLine("\n-------------------------------------------------");

            string[] mahalleBilgileri = { "Erzene,35135", "Kazımdirik,33934", "Atatürk,28912",
            "Evka3,20445", "Kızılay,15795", "Kemalpaşa,11742", "Naldöken,9892",
            "Karacaoğlan,8818", "Tuna,7607", "Egemenlik,2518"};

            Hashtable hashtable = new Hashtable(mahalleBilgileri.Length);  // hashtable oluşturma
            Heap heap = new Heap(mahalleBilgileri.Length);  // heap oluşturma
            string[] split = null;

            foreach (string m in mahalleBilgileri)
            {
                split = m.Split(',');
                int x = Int32.Parse(split[1]);   //mahalle bilgileri listesindeki her bir ikili elemanın mahalle adı ve nüfusu şeklinde ayrılıp
                                                 //iki farklı değişkene atanması
                hashtable.Add(split[0], x);  // hashtable eleman ekleme
                heap.insert(x, split[0]);  // heap eleman ekleme
            }

            Console.Write("\nLütfen bir harf giriniz: ");
            string arananHarf = Console.ReadLine();  //aranan harfi girdi olarak almak
            Console.WriteLine();

            string[] mahalleAdi = new string[hashtable.Count];
            int[] nufus = new int[hashtable.Count];
            int sayac = 0;
            foreach (DictionaryEntry entry in hashtable)  // hashtable dolaşılması
            {
                mahalleAdi[sayac] = (string)entry.Key;  // hashtable'ın her bir key'inin diziye aktarılması
                nufus[sayac] = (int)entry.Value;  // hashtable'ın her bir value'sinin diziye aktarılması
                sayac++;
            }

            for(int x=0; x < hashtable.Count; x++)  //hastable'ın tüm elemanlarının gezilmesi için döngü
            {
                string mahalleIlkHarf = mahalleAdi[x].Substring(0, 1);
                if (string.Compare(arananHarf, mahalleIlkHarf, StringComparison.OrdinalIgnoreCase) == 0) //mahalle adının ilk harfinin aranan harf olup olmadığının kontrolü
                {
                    nufus[x] += 1;  //mahalle adı, aranan harf ile başlayan mahallelerin nüfusunun 1 arttırılması
                }
                hashtable[mahalleAdi[x]] = nufus[x];  // hashtable güncelleme
            }

            Console.WriteLine("Güncel Hashtable ");
            Console.WriteLine("------------------");
            foreach (DictionaryEntry item in hashtable)
            {
                Console.WriteLine("{0}, {1}", item.Key, item.Value);
            }

            Console.WriteLine();
            heap.displayHeap();
            Console.WriteLine();
            Console.WriteLine("Nüfusu en fazla olan 3 mahalle");
            Console.WriteLine("------------------------------");

            // Nüfusu en fazla olan 3 mahalleyi sıra ile Heap’ten çekerek ilgili mahalle adı ve nüfus bilgilerini listeleyen döngü
            for (int i = 0; i < 3; i++)
            {
                HeapNode root = heap.remove();
                Console.WriteLine(root.ToString());
            }           

            int[] arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };

            Console.WriteLine("\n\nQuicksort: ");
            Console.WriteLine("-----------------");
            Console.WriteLine("Original array : ");
            foreach (var item in arr) //dizinin yazdırılması
            {
                Console.Write(" " + item);
            }
            Console.WriteLine();

            Quick_Sort(arr, 0, arr.Length - 1); //dizinin küçükten büyüğe sıralanması
            
            Console.WriteLine();
            Console.WriteLine("Sorted array : ");

            foreach (var item in arr) //sıralanan dizinin yazdırılması
            {
                Console.Write(" " + item);
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
