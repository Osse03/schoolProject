using System;
using System.Collections;
using System.Reflection;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace schoolProject
{
    internal class Program
    {
       
        
        public static bilInfo[] bilLista; 
        
        public class bilInfo

        {

            public string farger;
            public string regNu;
            public string bilMarke;

           
        }

        static void Main(string[] args)
        {

           

            string svar;
            int val;

            //En stor loop för inkludera hela meny 
            do
            {

                do//loop för själva meny
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine(" //      \\ \\______");
                    Console.WriteLine("||  ====  ||  ===  \\");
                    Console.WriteLine(" \\\\______/________  ");

                    Console.WriteLine("------------------------------");
                    Console.WriteLine("|  Välkommen till bilregister |");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("|   1. Lägg till ett bil.     |");
                    Console.WriteLine("|   2. Se alla bilar.         |");
                    Console.WriteLine("|   3. Ta bort din bil.       |");
                    Console.WriteLine("|   4. Söka efter märke.      |");
                    Console.WriteLine("|   5. Avsluta programmet.    |");
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Ange vilket altinativ vill köra.");
                   
                    while (!int.TryParse(Console.ReadLine(), out val)); //för att kontrollera att inamatning är korrekt
                    {
                       
                    }

                    //switch gör det möjligt att användare mellan olika fall. 
                    switch (val)
                    {
                        case 1:
                            läggBil();
                            break;

                        case 2:
                            Console.Clear();
                            seBil();
                            break;
                        
                        case 3:
                            TaBortBil();
                            break;

                        case 4:
                            sökaBil();
                            break;

                        case 5:
                            
                            Environment.Exit(0); // för att stäng ner programmet
                            Console.WriteLine("-------------------");
                            Console.WriteLine("Tack för din besök.");
                            Console.WriteLine("-------------------");
                            break;

                        default:
                            Console.WriteLine("Din inmatning är fel.");
                            break;
                    }

                } while (val != 1 && val != 2 && val != 3); // för att bryta loopen

                //Denna do loop för att kolla om användare vill se meny igen.
                do
                {


                    Console.WriteLine("\nVill du se meny igen.");

                    svar = Console.ReadLine().ToUpper();

                    if (svar != "JA" && svar != "NEJ") // kontrollera i fall inmatningen är fel. 
                    {
                        Console.WriteLine("Din inmatning är icke korrekt");
                    }
                    


                } while (svar != "JA" && svar != "NEJ"); // för att bryta loopen






            } while (svar != "NEJ"); // för att bryta loopen

            Console.Clear();


            Console.WriteLine("-------------------");
            Console.WriteLine("Tack för din besök.");
            Console.WriteLine("-------------------");

        }
       public static void hämtadata()

        {


            StreamReader visaData = new StreamReader("bilar.txt"); //för att läsa rader från filen
            
            int antalBilar = File.ReadLines("bilar.txt").Count(); //för att räkna rader utan att jag sätter gräns på antalet
            bilLista = new bilInfo[antalBilar];

            int i = 0;
            string line;
            
            while ((line = visaData.ReadLine()) != null) //denna loop gör att det läser alla rader tills filen är tom.
            {
                bilInfo bil = new bilInfo(); 
                string[] del = line.Split('\t');
                bil.bilMarke = del[0];
                bil.regNu = del[1];
                bil.farger = del[2];

                bilLista[i] = bil;
                i++;
            }
            visaData.Close();
           

        }
        static void seBil()
        {
            hämtadata();

            int index = 1; // för att börja från 1 och inte 0
            Array.Sort(bilLista, (x, y) => x.bilMarke.CompareTo(y.bilMarke)); //jag valde att den sortera bilmärken. 
            
            foreach (bilInfo bil in bilLista) // gå genom allt innehåll i filen
            {

                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine($"Fordons nummer {index}: Bilen är: {bil.bilMarke}|| Regnum är: {bil.regNu}|| Färgen är: {bil.farger}||");
                Console.WriteLine("----------------------------------------------------------------------");
                
                index++;
            }

            
        }
        
       
        static void läggBil()
        {

           StreamWriter läggData = new StreamWriter("bilar.txt", true); //för att skriva in i filen.

            string bilMarke, regNu, farger;


            Console.Write("Vad är din bils märke: ");
           bilMarke = Console.ReadLine();
             
 

           Console.Write("\nVad har din bil för regnummer: ");
           regNu = Console.ReadLine();
            
           Console.Write("\nVad har din bil för färge: ");
           farger = Console.ReadLine();
            
    
           Console.WriteLine("Din bil är nu registrera.");
 
           läggData.WriteLine($"{bilMarke}\t{regNu}\t{farger}");

            
            läggData.Close(); // här spara man data som användare mattar in. 
        
        
        }

        static void TaBortBil()
        {
            Console.WriteLine("Ange index på bilen du vill ta bort.");
            int bilToRemove;
            
            bool rättInMatning = int.TryParse(Console.ReadLine(), out bilToRemove); // kontrollerar i fall inmatningen är rätt 

            if (!rättInMatning || bilToRemove < 0 || bilToRemove >= bilLista.Length) //Här kontrolleras om inmatningen är rätt, dvs om bilToRemove är mindre än 0 eller större eller lika med billista.
            {
                Console.WriteLine("Ogiltigt index. Försök igen.");
                return;
            }

            bilInfo[] nyBilLista = new bilInfo[bilLista.Length - 1]; //för att ta bort index

            int index = 0;

            //loopen är för att spara det i listan
            for (int i = 0; i < bilLista.Length; i++)
            {
                if (i != bilToRemove)
                {
                    nyBilLista[index] = bilLista[i];
                    index++;
                }
            }

            bilLista = nyBilLista;
            Sparadata();
        }

        static void Sparadata() //metoden är för spara nydata efter att man ta bort något bil.
        {
            using (StreamWriter nyData = new StreamWriter("bilar.txt", false)) //false är för innehållet i filen att rensas innan ny data skrivs till den. 
            {
                foreach (bilInfo enBil in bilLista)
                {
                    nyData.WriteLine($"{enBil.bilMarke}\t{enBil.regNu}\t{enBil.regNu}"); // spara ny data i denna ordning. 
                }
            }

            Console.WriteLine("Data har uppddaterats.");
        }

  
        static void sökaBil()
        {

            hämtadata();

            Console.Write("Vilka bilmärke vill du se? ");
            string söktMärke = Console.ReadLine().ToUpper();

            bool hittad = false;

           for (int i = 0; i < bilLista.Length; i++)//loop för att gå genom alla märke och genmföra det med den sökta 
            {

                if (bilLista[i].bilMarke.ToUpper() == söktMärke) // för att kontrollera om bilmärket stämmer ed sökt bil. 
                {
                    Array.Sort(bilLista, (x, y) => x.regNu.CompareTo(y.regNu));
                    Console.WriteLine("Bilerna är sortrade på regnummer.");
                    Console.WriteLine("------------------------------------------------------------------");
                    Console.WriteLine($"Bilen är: {bilLista[i].bilMarke}|| Regnum är: {bilLista[i].regNu}|| Färgen är: {bilLista[i].farger}||");
                    Console.WriteLine("------------------------------------------------------------------");
                    hittad = true; // om den hittar matchning så blir det true 

                }

            }

            if (!hittad) // i fall märket inte hittas 
            {
               
                Console.WriteLine("Inga bilar av det sökta märket hittades.");

            }

        }


    }


}
