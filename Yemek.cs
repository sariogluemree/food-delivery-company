using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProje3
{
    class Yemek
    {
        public string yemekAdi;  
        public int yemekAdet;                 //yemek nesnesinin tutacağı değişkenlerin tanımlanması
        public double yemekBirimFiyat;

        public Yemek(string yemekAdi, int yemekAdet, double yemekBirimFiyat) //constructor
        {
            this.yemekAdi = yemekAdi;
            this.yemekAdet = yemekAdet;      //yemek nesnesinin değişkenlerinin ilk değerlerini alması
            this.yemekBirimFiyat = yemekBirimFiyat;
        }
                
        public override string ToString()   //bir yemek nesnesinin içeriğinin yazdırılması
        {
            string data = String.Format("Yemek Adı: {0} , Yemek Adedi: {1}, Yemek Birim Fiyatı: {2}", yemekAdi, yemekAdet, yemekBirimFiyat);
            return data;
        }
    }
}
