using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

/// <summary>
/// Added these imports to make methods and variables work
/// </summary>

using System.Xml.Linq;
using System.Xml.XPath;
using System.Net;
using System.IO;

namespace FinalTest2
{
    /// <summary>
    /// Interaction logic for Display.xaml
    /// </summary>
    public partial class Display : Window
    {

        protected List<Game> play1Games = new List<Game>(), play2Games = new List<Game>(), play3Games = new List<Game>(), play4Games = new List<Game>();

        protected List<string> sharedTotal = new List<string>();

        protected bool play1 = false, play2 = false, play3 = false, play4 = false;

        protected MainWindow First;

        public Display(MainWindow main, List<Game> Player1, List<Game> Player2, List<Game> Player3, List<Game> Player4,bool PlayerOneOn, bool PlayerTwoOn, bool PlayerThreeOn, bool PlayerFourOn)
        {
            InitializeComponent();
            First = main;
            play1Games = Player1;
            play2Games = Player2;
            play3Games = Player3;
            play4Games = Player4;
            play1 = PlayerOneOn;
            play2 = PlayerTwoOn;
            play3 = PlayerThreeOn;
            play4 = PlayerFourOn;
            //List1.Items.Add("bob"); 
            makePage();
            MessageBox.Show(play1 + " " + play2 + " " + play3 + " " + play4);

        }

        public void makePage()
        {
            foreach(Game current in play1Games)
            {
                List1.Items.Add(current.Title);
            }
            foreach (Game current in play2Games)
            {
                List2.Items.Add(current.Title);
            }
            foreach (Game current in play3Games)
            {
                List3.Items.Add(current.Title);
            }
            foreach (Game current in play4Games)
            {
                List4.Items.Add(current.Title);
            }
            List1.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Ascending));
            List2.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Ascending));
            List3.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Ascending));
            List4.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Ascending));
        }

        public void getSimilar()
        {
            if (play1 && play2 && play3 && play4)
            {
                List<string> play1String = new List<string>();
                foreach (Game current in play1Games)
                {
                    play1String.Add(current.Title);
                }
                List<string> play2String = new List<string>();
                foreach (Game current in play2Games)
                {
                    play2String.Add(current.Title);
                }
                List<string> play3String = new List<string>();
                foreach (Game current in play3Games)
                {
                    play3String.Add(current.Title);
                }
                List<string> play4String = new List<string>();
                foreach (Game current in play4Games)
                {
                    play4String.Add(current.Title);
                }
                foreach (string currentTitle in play1String)
                {
                    foreach (string currentTitle2 in play2String)
                    {
                        foreach (string currentTitle3 in play3String)
                        {
                            foreach (string currentTitle4 in play4String)
                            {
                                if (currentTitle == currentTitle2 && currentTitle == currentTitle3 && currentTitle == currentTitle4)
                                {
                                    sharedTotal.Add(currentTitle);
                                }
                            }
                        }
                    }
                }
            }
            else if (play1 && play2 && play3)
            {
                List<string> play1String = new List<string>();
                foreach (Game current in play1Games)
                {
                    play1String.Add(current.Title);
                }
                List<string> play2String = new List<string>();
                foreach (Game current in play2Games)
                {
                    play2String.Add(current.Title);
                }
                List<string> play3String = new List<string>();
                foreach (Game current in play3Games)
                {
                    play3String.Add(current.Title);
                }
                foreach (string currentTitle in play1String)
                {
                    foreach (string currentTitle2 in play2String)
                    {
                        foreach (string currentTitle3 in play3String)
                        {
                            if (currentTitle == currentTitle2 && currentTitle == currentTitle3)
                            {
                                sharedTotal.Add(currentTitle);
                            }
                        }
                    }
                }
            }
            else if (play1 && play2)
            {
                List<string> play1String = new List<string>();
                foreach (Game current in play1Games)
                {
                    play1String.Add(current.Title);
                }
                List<string> play2String = new List<string>();
                foreach (Game current in play2Games)
                {
                    play2String.Add(current.Title);
                }
                foreach (string currentTitle in play1String)
                {
                    foreach (string currentTitle2 in play2String)
                    {
                        if (currentTitle == currentTitle2)
                        {
                            sharedTotal.Add(currentTitle);
                        }
                    }
                }

            }
            else if (play1)
            {
                foreach (Game current in play1Games)
                {
                    sharedTotal.Add(current.Title);
                }
            }

        }


        private void closeMain(object sender, System.ComponentModel.CancelEventArgs e)
        {
            First.Close();
        }

        private void ShowShared(object sender, RoutedEventArgs e)
        {
            getSimilar();
            showShared shared = new showShared(sharedTotal);
            shared.Show();
        }

        
    }
}
