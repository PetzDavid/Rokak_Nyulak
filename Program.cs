using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        int width = 20;
        int height = 10;
        Random random = new Random();

        Erdo erdo = new Erdo(width, height);
        erdo.KezdoAllatokGeneralasa(random);
        erdo.Megjelenit();

        while (true)
        {
            Thread.Sleep(1000);
            Console.Clear();
            erdo.EgyNapSzimulacioja(random);
            erdo.Megjelenit();
        }
    }
}

class Allat
{
    public int x, y;
    public char jel;

    public Allat(int x, int y, char jel)
    {
        this.x = x;
        this.y = y;
        this.jel = jel;
    }

    public int X { get { return x; } }
    public int Y { get { return y; } }

    public void Mozgas(int newX, int newY)
    {
        x = newX;
        y = newY;
    }
}

class Nyul : Allat
{
    public Nyul(int x, int y) : base(x, y, 'N') { }
}

class Roka : Allat
{
    public Roka(int x, int y) : base(x, y, 'R') { }
}

class Erdo
{
    private int szelesseg, magassag;
    private char[,] matrix;
    private List<Nyul> nyulak;
    private List<Roka> rokak;

    public Erdo(int szelesseg, int magassag)
    {
        this.szelesseg = szelesseg;
        this.magassag = magassag;
        matrix = new char[szelesseg, magassag];
        nyulak = new List<Nyul>();
        rokak = new List<Roka>();
    }

    public void KezdoAllatokGeneralasa(Random random)
    {
        for (int i = 0; i < 5; i++)
        {
            int x = random.Next(szelesseg);
            int y = random.Next(magassag);
            Nyul nyul = new Nyul(x, y);
            nyulak.Add(nyul);
        }

        for (int i = 0; i < 3; i++)
        {
            int x = random.Next(szelesseg);
            int y = random.Next(magassag);
            Roka roka = new Roka(x, y);
            rokak.Add(roka);
        }
    }

    public void EgyNapSzimulacioja(Random random)
    {
        foreach (Roka roka in rokak)
        {
            int newX = roka.X + random.Next(-1, 2);
            int newY = roka.Y + random.Next(-1, 2);
            newX = Math.Max(0, Math.Min(szelesseg - 1, newX));
            newY = Math.Max(0, Math.Min(magassag - 1, newY));
            roka.Mozgas(newX, newY);

            foreach (Nyul nyul in nyulak)
            {
                if (roka.X == nyul.X && roka.Y == nyul.Y)
                {
                    Console.WriteLine("Egy róka megevett egy nyulat!");
                    nyulak.Remove(nyul);
                    break;
                }
            }
        }

        foreach (Nyul nyul in nyulak)
        {
            int newX = nyul.X + random.Next(-1, 2);
            int newY = nyul.Y + random.Next(-1, 2);
            newX = Math.Max(0, Math.Min(szelesseg - 1, newX));
            newY = Math.Max(0, Math.Min(magassag - 1, newY));
            nyul.Mozgas(newX, newY);
        }

        if (random.Next(0, 101) <= 10)
        {
            int x = random.Next(szelesseg);
            int y = random.Next(magassag);
            Nyul ujNyul = new Nyul(x, y);
            nyulak.Add(ujNyul);
            Console.WriteLine("Egy új nyúl született!");
        }
    }

    public void Megjelenit()
    {
        for (int y = 0; y < magassag; y++)
        {
            for (int x = 0; x < szelesseg; x++)
            {
                bool allatKiir = false;
                foreach (Nyul nyul in nyulak)
                {
                    if (nyul.X == x && nyul.Y == y)
                    {
                        Console.Write(nyul.jel);
                        allatKiir = true;
                        break;
                    }
                }

                if (!allatKiir)
                {
                    foreach (Roka roka in rokak)
                    {
                        if (roka.X == x && roka.Y == y)
                        {
                            Console.Write(roka.jel);
                            allatKiir = true;
                            break;
                        }
                    }
                }

                if (!allatKiir)
                {
                    Console.Write("-");
                }
            }
            Console.WriteLine();
        }
    }
}
