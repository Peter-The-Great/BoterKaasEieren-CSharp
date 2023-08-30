using System;
namespace BoterKaasEieren
{
    class Program
    {
        static void Main(string[] args)
        {
            Spel spel = new Spel();
            spel.Start();
        }
    }

    class Spel
    {
        private Veld veld;
        private Speler spelerX;
        private Speler spelerO;
        private Speler huidigeSpeler;

        public Spel()
        {
            veld = new Veld();
            spelerX = new Speler('X');
            spelerO = new Speler('O');
            huidigeSpeler = spelerX;
        }

        public void Start()
        {
            while (!veld.IsVol() && !veld.HeeftIemandGewonnen())
            {
                Console.Clear();
                veld.Toon();
                Console.WriteLine($"Huidige speler: {huidigeSpeler.GetSymbool()}");

                Console.Write("Geef de x-coördinaat (1-3): ");
                int x = int.Parse(Console.ReadLine()) - 1;

                Console.Write("Geef de y-coördinaat (1-3): ");
                int y = int.Parse(Console.ReadLine()) - 1;

                Zet zet = new Zet(huidigeSpeler, new Coördinaat(x, y));
                if (veld.IsZetGeldig(zet))
                {
                    veld.VoerZetUit(zet);
                    huidigeSpeler = (huidigeSpeler == spelerX) ? spelerO : spelerX;
                }
                else
                {
                    Console.WriteLine("Ongeldige zet. Probeer opnieuw.");
                    Console.ReadLine();
                }
            }

            Console.Clear();
            veld.Toon();
            if (veld.HeeftIemandGewonnen())
            {
                Speler winnendeSpeler = (huidigeSpeler == spelerX) ? spelerO : spelerX;
                Console.WriteLine($"Speler {winnendeSpeler.GetSymbool()} heeft gewonnen!");
            }
            else
            {
                Console.WriteLine("Gelijkspel!");
            }
        }
    }

    class Veld
    {
        private char[,] data;

        public Veld()
        {
            data = new char[,] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        }

        public void Toon()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(data[i, j]);
                    if (j < 2) Console.Write(" | ");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("---------");
            }
        }

        public bool IsZetGeldig(Zet zet)
        {
            int x = zet.Coördinaat.GetX();
            int y = zet.Coördinaat.GetY();
            return x >= 0 && x < 3 && y >= 0 && y < 3 && data[y, x] == ' ';
        }

        public void VoerZetUit(Zet zet)
        {
            int x = zet.Coördinaat.GetX();
            int y = zet.Coördinaat.GetY();
            data[y, x] = zet.Speler.GetSymbool();
        }

        public bool IsVol()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (data[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool HeeftIemandGewonnen()
        {
            // Controleer horizontale en verticale lijnen
            for (int i = 0; i < 3; i++)
            {
                if (data[i, 0] != ' ' && data[i, 0] == data[i, 1] && data[i, 1] == data[i, 2])
                {
                    return true;
                }

                if (data[0, i] != ' ' && data[0, i] == data[1, i] && data[1, i] == data[2, i])
                {
                    return true;
                }
            }

            // Controleer diagonale lijnen
            if (data[0, 0] != ' ' && data[0, 0] == data[1, 1] && data[1, 1] == data[2, 2])
            {
                return true;
            }

            if (data[0, 2] != ' ' && data[0, 2] == data[1, 1] && data[1, 1] == data[2, 0])
            {
                return true;
            }

            return false;
        }

    }

    class Speler
    {
        private char symbool;

        public Speler(char symbool)
        {
            this.symbool = symbool;
        }

        public char GetSymbool()
        {
            return symbool;
        }
    }

    class Zet
    {
        public Speler Speler { get; }
        public Coördinaat Coördinaat { get; }

        public Zet(Speler speler, Coördinaat coördinaat)
        {
            Speler = speler;
            Coördinaat = coördinaat;
        }
    }

    class Coördinaat
    {
        private int x;
        private int y;

        public Coördinaat(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}