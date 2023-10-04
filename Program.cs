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

    static void KezdoEntitasok()
    {
        for (int i = 0; i < maxNyulak; i++)
        {
            int x = veletlen.Next(racsMeret);
            int y = veletlen.Next(racsMeret);
            nyulak.Add(new Nyul(x, y));
        }

        for (int i = 0; i < maxRokak; i++)
        {
            int x = veletlen.Next(racsMeret);
            int y = veletlen.Next(racsMeret);
            rokak.Add(new Roka(x, y));
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

    static void EntitasokFrissitese()
    {
        List<Nyul> ujNyulak = new List<Nyul>();
        List<Roka> ujRokak = new List<Roka>();

        foreach (var nyul in nyulak)
        {
            nyul.Mozgas();
            nyul.Eves(racs);
            if (nyul.SzaporodasKepezes())
            {
                ujNyulak.Add(new Nyul(nyul.X, nyul.Y));
            }
            nyul.EhessegCsokkentes();
        }

        foreach (var roka in rokak)
        {
            roka.Mozgas();
            roka.Vadaszat(nyulak);
            if (roka.SzaporodasKepezes())
            {
                ujRokak.Add(new Roka(roka.X, roka.Y));
            }
            roka.EhessegCsokkentes();
        }

        nyulak.AddRange(ujNyulak);
        rokak.AddRange(ujRokak);

        nyulak.RemoveAll(n => n.Ehesseg <= 0);
        rokak.RemoveAll(r => r.Ehesseg <= 0);
    }
}

class Nyul
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Ehesseg { get; private set; }

    public Nyul(int x, int y)
    {
        X = x;
        Y = y;
        Ehesseg = 5; 
    }


    public void EhessegCsokkentes()
    {
        Ehesseg--;
    }
}

class Roka
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Ehesseg { get; private set; }

    public Roka(int x, int y)
    {
        X = x;
        Y = y;
        Ehesseg = 10; 
    }

    public void EhessegCsokkentes()
    {
        Ehesseg--;
    }
}
