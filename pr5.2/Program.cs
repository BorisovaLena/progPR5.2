using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace pr5._2
{
    struct Lenght //структура с колонками из файла
    {
        public string km;
        public string m;
        public string mili;
        public string fyt;
        public string yard;
        public string duim;
        public string versta;
        public void show() // метод для вывода в консоль экземпляра структуры
        {
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15} {6,-15} ", km, m, mili, fyt, yard, duim, versta);
        }
    }

    struct Write //структура для вывода в newfile.csv
    {
        public string str;

        public string concat()
        {
            return str;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Lenght> lenght = new List<Lenght>();
            List<Write> write = new List<Write>(); // список для вывода в файл newfile.csv
            getData("dataLenght.csv", lenght); // чтение файла
            printData(lenght); // вывод данных из файла
            perevod(lenght, write); // перевод в другие единицы измерения
            inputData("newfile.csv", write); // запись в newfile.csv
        }
        static void getData(string path, List<Lenght> L) // чтение файла
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                string[] array = sr.ReadLine().Split(';');
                    L.Add(new Lenght()
                    {
                        km = array[0],
                        m =  array[1],
                        mili = array[2],
                        fyt = array[3],
                        yard = array[4],
                        duim = array[5],
                        versta = array[6]
                    });
                }
            }
        }
        static void printData(List<Lenght> L) // метод для вывода данных в консоль
        {
            foreach (Lenght u in L)
            {
                u.show();
            }
        }

        static void perevod(List<Lenght> L, List<Write> W) // перевод в другие единицы измерения
        {
            foreach (Lenght l in L)
            {
                int km = 0;
                int m = 0;
                double mili = 0;
                double fyt = 0;
                double yard = 0;
                double duim = 0;
                double versta = 0;

                try // проверка корректности данных
                {
                    km = Convert.ToInt32(l.km);
                }
                catch { }

                try
                {
                    m = Convert.ToInt32(l.m);
                }
                catch { }

                if (km!=0 || m!= 0) // если есть км и м
                {
                    double rezMili = (km * 1000 + m) * 0.000621371; // подсчет миль
                    Console.WriteLine(km + " км "+ m +" м = "+ rezMili+ " миль"); // вывод на экран результата
                    Write write = new Write() {str = km + " км " + m + " м = " + rezMili + " миль" }; // создание записи
                    W.Add(write); // добавление записи в список
                    double rezfyt = (km * 1000 + m) * 3.28084;
                    Console.WriteLine(km + " км " + m + " м = " + rezfyt + " футов");
                    Write write1 = new Write() { str = km + " км " + m + " м = " + rezfyt + " футов" };
                    W.Add(write1);
                    double rezyard = (km * 1000 + m) * 1.09361;
                    Console.WriteLine(km + " км " + m + " м = " + rezyard + " ярд");
                    Write write2 = new Write() { str = km + " км " + m + " м = " + rezyard + " ярд" };
                    W.Add(write2);
                    double rezduim = (km * 1000 + m) * 39.3701;
                    Console.WriteLine(km + " км " + m + " м = " + rezduim + " дюймов");
                    Write write3 = new Write() { str = km + " км " + m + " м = " + rezduim + " дюймов" };
                    W.Add(write3);
                    double rezversta = (km * 1000 + m) * 0.000937383;
                    Console.WriteLine(km + " км " + m + " м = " + rezversta + " верст");
                    Write write4 = new Write() { str = km + " км " + m + " м = " + rezversta + " верст" };
                    W.Add(write4);
                }

                try
                {
                    mili = Convert.ToDouble(l.mili);
                }
                catch { }

                if (mili!=0)
                {
                    string str = "миль";
                    double kolM = mili * 1609.34;
                    podsch(kolM, str, mili, W); // выражение км из метров и добавление записи в список
                }

                try
                {
                    fyt = Convert.ToDouble(l.fyt);
                }
                catch { }

                if (fyt!=0)
                {
                    string str = "фут";
                    double kolM = fyt * 0.3048;
                    podsch(kolM, str, fyt, W);
                }

                try
                {
                    yard = Convert.ToDouble(l.yard);
                }
                catch { }

                if (yard!=0)
                {
                    string str = "ярд";
                    double kolM = yard * 0.9144;
                    podsch(kolM, str, yard, W);
                }

                try
                {
                    duim = Convert.ToDouble(l.duim);
                }
                catch { }

                if (duim!=0)
                {
                    string str = "дюймов";
                    double kolM = duim * 0.0254;
                    podsch(kolM, str, duim, W);
                }

                try
                {
                    versta = Convert.ToDouble(l.versta);
                }
                catch { }

                if (versta != 0)
                {
                    string str = "верст";
                    double kolM = versta * 1066.8;
                    podsch(kolM, str, versta, W);
                }
            }
        }

        public static void podsch(double kolM, string str, double lens, List<Write> W) // выражение км из метров и добавление записи в список
        {
            int kolKm = 0;
            while (kolM > 999)
            {
                kolKm++;
                kolM -= 1000;
            }
            Console.WriteLine(lens + " " + str + " = " + kolKm + " км " + kolM + " м");
            Write write = new Write() { str = lens + " " + str + " = " + kolKm + " km " + kolM + " m" };
            W.Add(write);
        }
        
        static void inputData(string path, List<Write> L) // запись списка в newfile.csv
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8)) 
            {
                foreach (Write u in L)
                {
                    sw.WriteLine(u.concat());
                }
            }
        }
    }
}
