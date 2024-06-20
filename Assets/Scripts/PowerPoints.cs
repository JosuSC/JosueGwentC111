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

        public static TextMeshProUGUI _Rondas1;
        public static TextMeshProUGUI _Rondas2;  

        public static int rondas1 = 0;
        public static int rondas2 = 0;

        public static void Load(TextMeshProUGUI Filag1, TextMeshProUGUI Filaa1,TextMeshProUGUI Filad1,TextMeshProUGUI Filag2,TextMeshProUGUI Filaa2,TextMeshProUGUI Filad2,TextMeshProUGUI AllCount1,TextMeshProUGUI AllCount2,TextMeshProUGUI Rondas1,TextMeshProUGUI Rondas2)
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
            _Rondas1 = Rondas1;
            _Rondas2 = Rondas2;
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
            _Filaa2.text = filaa2.ToString();
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

        public static void ReiniciarCount()
        {
            _Filag1.text = "0";
            _Filad1.text = "0";
            _Filaa1.text = "0";
            _Filag2.text = "0";
            _Filag2.text = "0";
            _Filad2.text = "0";
            _Filaa2.text = "0";
            _AllCount1.text = "0";
            _AllCount2.text = "0";
            filag1 = 0;
            filad1 = 0;
            filaa1 = 0;

            filag2 = 0;
            filad2 = 0;
            filaa2 = 0;
            allCount1 = 0;
            allCount2 = 0;


        

        }

        public static void AddPointToPlayer1() 
        {
            rondas1 += 1;
            _Rondas1.text = rondas1.ToString();
        }

        public static void AddPointToPlayer2()
        {
            rondas2 += 1;
            _Rondas2.text = rondas2.ToString();
        }

    }
}
