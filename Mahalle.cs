using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProje3
{
    class Mahalle
    {
        public string mahalleAdi;           //mahalle sınıfı nesnesinin değişkenlerinin tanımlanması
        public ArrayList siparislerListesi;

        public Mahalle(string mahalleAdi, ArrayList siparislerListesi) //constructor
        {
            this.mahalleAdi = mahalleAdi;
            this.siparislerListesi = siparislerListesi;   //mahalle nesnesinin değişkenlerinin ilk değerlerini alması
        }
    }
}
