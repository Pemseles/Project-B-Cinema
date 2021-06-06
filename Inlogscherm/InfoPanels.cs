using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace ConsoleApp1
{
    public class Infopanels
    {
        private static int index = 0;

        public string PanelLocation;
        public string PanelVIP;
        public string PanelContact;
        public string PanelReviews;
        public string PanelSearchbar;
        public string PanelPayment;
        public string FAQs;

        public Infopanels()
        {
            // laadt hier de infopanels, niet via json want deze veranderen niet en t zijn dr niet zo veel
            this.PanelLocation = @"
            [   Om te genieten van uw aankopen en onze service kunt u naar een van onze locaties.   ]
            [   Onze locaties zijn als volgt:                                                       ]
            [                                                                                       ]
            [   Prins Hendrikkade 83 Rotterdam                                                      ]
            [   1e Middellandstraat 103 Den haag                                                    ]
            [   Groenedijk 49 Dordrecht                                                             ]
            [   Westelijke Parallelweg 4 Amsterdam                                                  ]
            [                                                                                       ]
            [   Als u meer informatie wilt over onze locaties, bel +31 632 92 66.                   ]
            " + "\n";
            this.PanelVIP = @"
            [   Bij het creëren van uw account heeft u de mogelijkheid te registreren als           ]
            [   VIP gebruiker.                                                                      ]
            [   U kunt ook uw standaardaccount upgraden tot VIP in het betalingsmenu.               ]
            [   Als VIP gebruiker heeft u toegang tot exclusieve privileges en aanbiedingen,        ]
            [   waaronder tot maarliefst wel 50% korting op filmtickets en producten, het mogen     ]
            [   bijwonen van exclusieve filmpremières en toegang tot de meest comfortabele          ]
            [   stoelen in luxe- en gemengde zalen.                                                 ]
            [   Registratie voor filmpremières wordt afgehandeld op onze locaties.                  ]
            [   Als u meer informatie wilt over onze VIP regeling, bel +31 632 92 66.               ]
            " + "\n";
            this.PanelContact = @"
            [   Heeft u nog resterende vragen over ons, onze regelingen of onze service? bel        ]
            [   dan naar +31 632 92 66 of stuur een e-mail naar info@cinscope.nl. U kunt ons        ]
            [   ook bereiken door een review achter te laten met de categorie 'Contact'. U zult     ]
            [   binnen 1-3 werkdagen antwoord ontvangen van onze klantenservicemedewerkers.         ]
            " + "\n";
            this.PanelReviews = @"
            [   Vanaf het hoofdmenu bevindt zich het reviewmenu. Hier kunt u reviews achterlaten    ]
            [   over verscheidene onderwerpen, als specifieke films, onze klantenservice, ons       ]
            [   aanbod of over onze locaties. Daarnaast kunt u de reviews van andere gebruikers     ]
            [   lezen. Wij waarderen uw feedback zeer. Hierom vragen wij u graag een review in te   ]
            [   dienen om ons bedrijf te verbeteren.                                                ]
            [   Als u meer informatie wilt over ons reviewmenu, bel +31 632 92 66.                  ]
            " + "\n";
            this.PanelSearchbar = @"
            [   Vanaf het hoofdmenu bevindt zich het zoekmenu. Het selecteren van deze optie        ]
            [   brengt u naar het zoekmenu, waar er van u wordt gevraagd een input te geven. Als    ]
            [   deze input overeenkomt met de titel, regisseur, genres of leeftijds-kwalificatie    ]
            [   van een film of meerdere films, zal de applicatie de lijst met overeenkomstige      ]
            [   films weergeven. Als er geen films overeenkomen met uw input, zal de applicatie     ]
            [   5 keer opnieuw voor een input vragen, waarna u weer naar het hoofdmenu wordt        ]
            [   gestuurd. Vanuit deze lijst met films kunt u een film selecteren om deze film       ]
            [   toe te voegen aan uw persoonlijke winkelwagen. Dit is nodig om een transactie te    ]
            [   kunnen voltooien, aangezien uw gekozen film wordt behandeld als een filmticket.     ]
            [   Als u meer informatie wilt over ons zoekmenu, bel +31 632 92 66.                    ]
            " + "\n";
            this.PanelPayment = @"
            [   Vanaf het hoofdmenu bevindt zich het betalingsmenu. Hier ziet u uw winkelwagen.     ]
            [   Deze kunt u vullen door in onze zoekfunctie een film te selecteren die u zou        ]
            [   willen zien. Daarnaast zijn er twee optionele keuzes mogelijk. U kunt in de         ]
            [   productenlijst de producten kiezen die u tijdens de film kunt eten en drinken. Ook  ]
            [   is het mogelijk zelf de zaal te kiezen waar de film zich afspeelt. Het is ook       ]
            [   mogelijk een stoel te reserveren. Alleen VIP gebruikers kunnen ‘comfortabele’       ]
            [   stoelen reserveren. Nadat u uw film heeft gekozen en de optionele keuzes heeft      ]
            [   gemaakt, kunt u afrekenen. VIP gebruikers krijgen korting op hun aankopen. U kunt   ]
            [   op 3 verschillende manieren betalen, met iDeal, creditcard of contant. Contant      ]
            [   betalen gebeurt op locatie, iDeal en creditcard worden in de applicatie geregeld.   ]
            [   Als u meer informatie wilt over ons betalingssysteem, bel +31 632 92 66.            ]
            " + "\n";
            this.FAQs = @"
            [   Dit segment is toegewijd aan de meest gestelde vragen die wij hebben ontvangen,     ]
            [   en onze antwoorden op deze vragen.                                                  ]
            [                                                                                       ]
            [   Q.  Wat zijn de voordelen van een VIP account?                                      ]
            [   A.  Er zijn meerdere voordelen inbegrepen met een VIP lidmaatschap account.         ]
            [       VIPs hebben exclusief toegang tot filmpremières, speciale aanbiedingen en       ]
            [       kortingen en de mogelijkheid comfortabele stoelen te reserveren.                ]
            [                                                                                       ]
            [   Q.  Hoe neem ik contact op met de klantenservice van Cinescope?                     ]
            [   A.  U kunt ons bereiken door te bellen naar +31 632 92 66, een e-mail te sturen     ]
            [       naar info@cinscope.nl of door een review achter te laten in ons reviewmenu. U   ]
            [       zult binnen 1-3 werkdagen antwoord ontvangen van onze klantenservicemedewerkers.]
            [                                                                                       ]
            [   Q.  Hoe gaat Cinescope om met kijkwijzer 16+ en 18+?                                ]   
            [   A.  Cinescope neemt de kijkwijzer leeftijdsclassificaties serieus, en zal bij       ]
            [       twijfel vragen om legitimatiebewijs. Geldige identificatie zijn onder andere    ]
            [       identiteitskaart, rijbewijs, verblijfsvergunning en paspoort. Ook onder         ]
            [       toezicht van een individu die wel voldoet aan de leeftijdskeuring mogen         ]
            [       jongeren onder de leeftijdsclassificatie niet de zaal in.                       ]
            " + "\n";
        }

        private string InfoScreenSelect(List<string> items, string SelectedPanel)
        {
            // wordt gecalled in Infoscreen() om ingedrukte toets te bepalen
            Logo.Print();
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.Write("                                                ");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.Write("                                                ");
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
                
            }
            // tekent de infopanels met de SelectedPanel, die alleen verandert als je enter drukt.
            DrawInfo(SelectedPanel);
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index < 7)
                {
                    index++;
                }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index > 0)
                {
                    index--;
                }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                return items[index];
            }
            // else returnt SelectedPanel zodat de infopanel open blijft totdat het verandert
            else
            {
                return SelectedPanel;
            }
            Console.Clear();
            return SelectedPanel;
        }

        public void InfoScreen()
        {
            // deze 3 dingen veranderen afhankelijk van wat SelectedPanel is
            index = 0;
            bool infoPanelBool = true;
            string SelectedPanel = "";

            while (infoPanelBool)
            {
                List<string> infoScreenLayout = new List<string>() {
                "[     Locatie     ]" ,
                "[       VIP       ]" ,
                "[     Contact     ]" ,
                "[     Reviews     ]" ,
                "[   Films zoeken  ]" ,
                "[     Betaling    ]" ,
                "[       FAQ       ]" ,
                "[      Terug      ]"
                };

                SelectedPanel = InfoScreenSelect(infoScreenLayout, SelectedPanel);

                if (SelectedPanel == "[      Terug      ]")
                {
                    infoPanelBool = false;
                }
            }
            Console.Clear();
     
            // Go back to Main Menu - Depending on ID and Clearance Level
            if (Program.Level >= 3)  {
                AdminMenu.Mainmenu();
            } else if(Program.UID == -1) {
                Guest.Mainmenu();
            } else {
                MainMenu.Mainmenu();
            }
        }

        private void DrawInfo(string currentPanel)
        {
            // tekent de infopanels afhankelijk van welke panel geselecteerd is
            if (currentPanel == "[     Locatie     ]")
            {
                Console.Write(this.PanelLocation);
            }
            else if (currentPanel == "[       VIP       ]")
            {
                Console.Write(this.PanelVIP);
            }
            else if (currentPanel == "[     Contact     ]")
            {
                Console.Write(this.PanelContact);
            }
            else if (currentPanel == "[     Reviews     ]")
            {
                Console.Write(this.PanelReviews);
            }
            else if (currentPanel == "[   Films zoeken  ]")
            {
                Console.Write(PanelSearchbar);
            }
            else if (currentPanel == "[     Betaling    ]")
            {
                Console.Write(this.PanelPayment);
            }
            else if (currentPanel == "[       FAQ       ]")
            {
                Console.Write(this.FAQs);
            }
        }
    }
}
