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
            ";
            this.PanelVIP = @"
            [   Bij het creëren van uw account heeft u de mogelijkheid te registreren als           ]
            [   VIP gebruiker.                                                                      ]
            [   U kunt ook uw standaardaccount upgraden tot VIP in het betalings menu.              ]
            [   Als VIP gebruiker heeft u toegang tot exclusieve privileges en aanbiedingen,        ]
            [   waaronder tot maarliefst wel 50% korting op filmtickets en producten, het mogen     ]
            [   bijwonen van exclusieve filmpremières en toegang tot de meest comfortabele          ]
            [   stoelen in luxe- en gemengde zalen.                                                 ]
            [   Registratie voor filmpremières wordt afgehandeld op onze locaties.                  ]
            [   Als u meer informatie wilt over onze VIP regeling, bel +31 632 92 66.               ]
            ";
            this.PanelContact = @"
            [   Heeft u nog resterende vragen over ons, onze regelingen of onze service? bel        ]
            [   dan naar +31 632 92 66 of stuur een e-mail naar info@cinscope.nl. U kunt ons        ]
            [   ook bereiken door een review achter te laten met de categorie 'Contact'. U zult     ]
            [   binnen 1-3 werkdagen antwoord ontvangen van onze klantenservicemedewerkers.         ]
            ";
            this.PanelReviews = @"
            [   Vanaf het hoofdmenu bevindt zich het review menu. Hier kunt u reviews achterlaten   ]
            [   over verscheidene onderwerpen, als specifieke films, onze klantenservice, ons       ]
            [   aanbod of over onze locaties. Daarnaast kunt u de reviews van andere gebruikers     ]
            [   lezen. Wij waarderen uw feedback zeer. Hierom vragen wij u graag een review in te   ]
            [   dienen om ons bedrijf te verbeteren.                                                ]
            [   Als u meer informatie wilt over ons review menu, bel +31 632 92 66.                 ]
            ";
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
            [   Als u meer informatie wilt over onze zoekmenu, bel +31 632 92 66.                   ]
            ";
            this.PanelPayment = @"
            [   Vanaf het hoofdmenu bevindt zich het betalings menu. Hier ziet u uw winkelwagen.    ]
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
            ";
            this.FAQs = @"
            [   Dit segment is toegewijd aan de meest gestelde vragen die wij hebben ontvangen,     ]
            [   en de antwoorden op deze vragen.                                                    ]
            [                                                                                       ]
            [   Q.  Wat zijn de voordelen van een VIP account?                                      ]
            [   A.  Er zijn meerdere voordelen inbegrepen met een VIP lidmaatschap account.         ]
            [       VIPs hebben exclusief toegang tot filmpremières, speciale aanbiedingen en       ]
            [       kortingen en de mogelijkheid comfortabele stoelen te reserveren.                ]
            [                                                                                       ]
            [   Q.  Hoe neem ik contact op met de klantenservice van Cinescope?                     ]
            [   A.  U kunt ons bereiken door te bellen naar +31 632 92 66, een e-mail te sturen     ]
            [   naar info@cinscope.nl of door een review achter te laten in ons review menu. U      ]
            [   zult binnen 1-3 werkdagen antwoord ontvangen van onze klantenservicemedewerkers.    ]
            [                                                                                       ]
            [   Q.  Hoe gaat Cinescope om met kijkwijzer 16+ en 18+?                                ]   
            [   A.  Cinescope neemt de kijkwijzer leeftijdsclassificaties serieus, en zal bij       ]
            [       twijfel vragen om legitimatiebewijs. Geldige identificatie zijn onder andere    ]
            [       identiteitskaart, rijbewijs, verblijfsvergunning en paspoort. Ook onder         ]
            [       toezicht van een individu die wel voldoet aan de leeftijdskeuring mogen         ]
            [       jongeren onder de leeftijdsclassificatie niet de zaal in.                       ]
            ";
        }

        private static string InfoScreenSelect(List<string> items)
        {
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
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index >= 6)
                {
                    index = index;
                }
                else
                {
                    index++;
                }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = index;
                }
                else
                {
                    index--;
                }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }
            Console.Clear();
            return "";
        }

        public void InfoScreen()
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

            string SelectedPanel = InfoScreenSelect(InfoScreenLayout);
            if (SelectedPanel == "[      Terug      ]")
            {
                Console.Clear();
                Back();
            }
            else if (SelectedPanel == "[     Locatie     ]")
            {
                Console.Clear();
                Console.Write(this.PanelLocation);
            }
            else if (SelectedPanel == "[       VIP       ]")
            {
                Console.Clear();
                Console.Write(this.PanelVIP);
            }
            else if (SelectedPanel == "[     Contact     ]")
            {
                Console.Clear();
                Console.Write(this.PanelContact);
            }
            else if (SelectedPanel == "[     Reviews     ]")
            {
                Console.Clear();
                Console.Write(this.PanelReviews);
            }
            else if (SelectedPanel == "[   Films zoeken  ]")
            {
                Console.Clear();
                Console.Write(this.PanelSearchbar);
            }
            else if (SelectedPanel == "[     Betaling    ]")
            {
                Console.Clear();
                Console.Write(this.PanelPayment);
            }
            else if (SelectedPanel == "[       FAQ       ]")
            {
                Console.Clear();
                Console.Write(this.FAQs);
            }
        }
    }
}
