using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace Assets
{
    public static class PowerPoints
    {
        private static TextMeshProUGUI _Filag1;
        private static TextMeshProUGUI _Filad1;
        private static TextMeshProUGUI _Filaa1;

        private static TextMeshProUGUI _Filag2;
        private static TextMeshProUGUI _Filaa2;
        private static TextMeshProUGUI _Filad2;

        public static int filag1 = 0;
        public static int filad1 = 0;
        public static int filaa1 = 0;

        public static int filag2 = 0;
        public static int filad2 = 0;
        public static int filaa2 = 0;

        private static TextMeshProUGUI _AllCount1;
        private static TextMeshProUGUI _AllCount2;

        public static int allCount1 = 0;    
        public static int allCount2 = 0;    
       

        public static void Load(TextMeshProUGUI Filag1, TextMeshProUGUI Filaa1,TextMeshProUGUI Filad1,TextMeshProUGUI Filag2,TextMeshProUGUI Filaa2,TextMeshProUGUI Filad2,TextMeshProUGUI AllCount1,TextMeshProUGUI AllCount2)
        {
            _Filag1 = Filag1;
            _Filad1 = Filad1;
            _Filaa1= Filaa1;
            _Filag2 = Filag2;
            _Filag2= Filag2;
            _Filad2 = Filad2;
            _Filaa2 = Filaa2;
            _AllCount1 = AllCount1;
            _AllCount2 = AllCount2;
        }

        public static void addPointToWarrior1(int points)
        {
            filag1 += points;
            _Filag1.text = filag1.ToString();
            allCount1 += points;

        }
        public static void addPointToasedio1(int points)
        {
            filaa1 += points;
            _Filaa1.text= filaa1.ToString();    
            allCount1 += points;
        }
        
        public static void addPointTodistance1(int points)
        {
            filad1 += points;
            _Filad1.text = filad1.ToString();
            allCount1 += points;

        }

        public static void addPointotal1(int points)
        {
            allCount1 += points;
            _AllCount1.text = allCount1.ToString();
         

        }

        public static void addPointToWarrior2(int points)
        {
            filag2 += points;
            _Filag2.text = filag2.ToString();
            allCount2 += points;

        }
        public static void addPointToasedio2(int points)
        {
            filaa2 += points;
            _Filaa1.text = filaa2.ToString();
            allCount2 += points;
        }
        public static void addPointTodistance2(int points)
        {
            filad2 += points;
            _Filad2.text = filag2.ToString();
            allCount2 += points;

        }

        public static void addPointotal2(int points)
        {
            allCount2 += points;
            _AllCount2.text = allCount2.ToString();


        }

        public static void Actualizar1() 
        {
            _AllCount1.text = allCount1.ToString(); 
        }
        public static void Actualizar2()
        {
            _AllCount2.text = allCount2.ToString();
        }


       
    }
}
