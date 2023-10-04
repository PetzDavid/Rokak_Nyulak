using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random veletlen = new Random();
    static int racsMeret = 10;
    static int maxNyulak = 20;
    static int maxRokak = 10;

    static int[,] racs = new int[racsMeret, racsMeret];
    static List<Nyul> nyulak = new List<Nyul>();
    static List<Roka> rokak = new List<Roka>();

    static void Main()
    {
        KezdoAllapot();
        KezdoEntitasok();

        int kor = 0;

        while (nyulak.Count > 0)
        {
            kor++;
            Console.WriteLine($"Kör {kor}");
            RacsFrissitese();
            RacsKirajzolasa();
            EntitasokFrissitese();
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000); 
        }

        Console.WriteLine("Minden nyúl elpusztult. A szimuláció véget ér.");
    }

    static void KezdoAllapot()
    {
        for (int x = 0; x < racsMeret; x++)
        {
            for (int y = 0; y < racsMeret; y++)
            {
                racs[x, y] = veletlen.Next(3); 
            }
        }
    }


    static void RacsFrissitese()
    {
        
        for (int x = 0; x < racsMeret; x++)
        {
            for (int y = 0; y < racsMeret; y++)
            {
                if (racs[x, y] == 0 && !nyulak.Any(n => n.X == x && n.Y == y))
                {
                    racs[x, y] = 1; 
                }
                else if (racs[x, y] == 1 && !nyulak.Any(n => n.X == x && n.Y == y))
                {
                    racs[x, y] = 2; 
                }
            }
        }
    }

    static void RacsKirajzolasa()
    {
        for (int x = 0; x < racsMeret; x++)
        {
            for (int y = 0; y < racsMeret; y++)
            {
                if (nyulak.Any(n => n.X == x && n.Y == y))
                {
                    Console.Write("N ");
                }
                else if (rokak.Any(r => r.X == x && r.Y == y))
                {
                    Console.Write("R ");
                }
                else
                {
                    Console.Write(racs[x, y] + " ");
                }
            }
            Console.WriteLine();
        }
    }


}
