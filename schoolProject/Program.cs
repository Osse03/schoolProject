using System;
using System.Collections;
using System.Security;
using System.Text.RegularExpressions;

namespace schoolProject
{
    internal class Program
    {

        public static bilInfo[] bilLista;
        public struct bilar
        {
            public string bilMarke;
            public string regNu;     
            public string farger;
            

        }
        
        static void Main(string[] args)
        {


            string svar;
            int val;

            //En stor loop att inkludera meny 
            do
            {

                do
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("|Välkommen till bilregister|");
                    Console.WriteLine("----------------------------");
                    Console.Write("Ange vilket altinativ vill köra.");

                    Console.WriteLine("\n\n1. Se bilar.");
                    Console.WriteLine("2. Lägg till din bil.");
                    Console.WriteLine("3. Ta bort din bil");
                    Console.WriteLine("4. Avsluta programmet.");

                    while (!int.TryParse(Console.ReadLine(), out val));
                    {
                        Console.WriteLine("Ange korrekt val.");
                    }

                    //switch gör det möjligt att användare mellan olika fall. 
                    switch (val)
                    {
                        case 1:
                            Console.Clear();
                            seBil();
                            break;

                        case 2:
                            Console.Clear();
                            läggBil();
                            break;

                        case 3:

                            break;

                        case 4:
                            Environment.Exit(0);
                            Console.WriteLine("Tack för din besök");
                            break;

                        default:
                            Console.WriteLine("Din inmatning är fel.");
                            break;
                    }

                } while (val != 1 && val != 2 && val != 3);

                //Denna do loop för att kolla om användare vill se meny igen.
                do
                {


                    Console.WriteLine("\nVill du se meny igen.");

                    svar = Console.ReadLine().ToUpper();

                    if (svar != "JA" && svar != "NEJ")
                    {
                        Console.WriteLine("Din inmatning är icke korrekt");
                    }

                } while (svar != "JA" && svar != "NEJ");





            } while (svar != "NEJ");
            Console.Clear();


            Console.WriteLine("-------------------");
            Console.WriteLine("Tack för din besök.");
            Console.WriteLine("-------------------");

        }
        static void seBil()

        {


            StreamReader visaData = new StreamReader("bilar.txt");

            bilLista = new bilInfo[3];

            string line;
            while ((line = visaData.ReadLine()) != null)
            {
                bilInfo bil = new bilInfo(); 
                string[] tab = line.Split('\t');
                bil.bilMarke = tab[0];
                bil.regNu = tab[1];
                bil.farger = tab[2];
            }
            visaData.Close();


        }
        static void läggBil()
        {

           StreamWriter läggData = new StreamWriter("bilar.txt", true);

            string bilMarke, regNu, farger;


           Console.Write("Vad är din bils märke: ");
           bilMarke = Console.ReadLine();
             
 

           Console.Write("\nVad har din bil för regnummer: ");
           regNu = Console.ReadLine();
            
           Console.Write("\nVad har din bil för färge: ");
           farger = Console.ReadLine();
            
    
           Console.WriteLine("Din bil är nu registrera.");
 
           läggData.WriteLine($"{bilMarke}\t{regNu}\t{farger}");

            

            läggData.Close();

        }

               
        
    }

    public class bilInfo
    {
    }
}
