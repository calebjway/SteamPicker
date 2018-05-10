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
using System.Windows.Navigation;
using System.Windows.Shapes;

/// <summary>
/// Added these imports to make methods and variables work
/// </summary>

using System.Xml.Linq;
using System.Xml.XPath;
using System.Net;
using System.IO;
using System.Collections;

namespace FinalTest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected string rootPath = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
        protected bool pullUser = false, pullOne = false, pullTwo = false, pullThree = false;
        protected bool idNotCustom = false;
        protected bool sendUser = false, sendOne = false, sendTwo = false, sendThree = false;
        protected string userBox = "User Profile", oneBox = "Player 1 Profile", twoBox = "Player 2 Profile", threeBox = "Player 3 Profile";
        protected string xml, html;
        protected List<Game> play1Games = new List<Game>(), play2Games = new List<Game>(), play3Games = new List<Game>(), play4Games = new List<Game>();


        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists(rootPath + "\\UserProfile"))
            {
                Directory.CreateDirectory(rootPath + "\\UserProfile");
            }

            if (!Directory.Exists(rootPath + "\\Game"))
            {
                Directory.CreateDirectory(rootPath + "\\Game");
            }

        }

        private void getProfileButton(object sender, RoutedEventArgs e)
        {
            

            if (pullUser)
            {
                GetProfile(userProfileText.Text.ToString(), userBox, pullUser);
                
            }
            if (pullOne)
            {
                GetProfile(playerOneText.Text.ToString(), oneBox, pullOne);
            }
            if (pullTwo)
            {
                GetProfile(playerTwoText.Text.ToString(), twoBox, pullTwo);
            }
            if (pullThree)
            {
                GetProfile(playerThreeText.Text.ToString(), threeBox, pullThree);
            }

            Display display = new Display(this,play1Games,play2Games,play3Games,play4Games,sendUser,sendOne,sendTwo,sendThree); 

            this.Hide();

            display.Show();

        }

        private void play3On(object sender, RoutedEventArgs e)
        {
            pullThree = true;
        }

        private void play2On(object sender, RoutedEventArgs e)
        {
            pullTwo = true;
        }

        private void play1On(object sender, RoutedEventArgs e)
        {
            pullOne = true;
        }

        private void userOn(object sender, RoutedEventArgs e)
        {
            pullUser = true;
        }

        protected void downWrite(String name, String boxName, bool statement)
        {
            try {
                TextWriter twt = new StreamWriter(rootPath + "\\UserProfile\\" + name + ".wtf");

                XElement x = XElement.Parse(xml);

                twt.WriteLine("<profile>");

                twt.WriteLine("<userName>" + x.Element("steamID").Value + "</userName>");

                var ele = x.Descendants("game");

                foreach (XElement current in ele)
                {
                    //MessageBox.Show("Write profile game stuff");

                    twt.WriteLine("<game>");

                    twt.WriteLine("<appNum>" + current.Element("appID").Value + "</appNum>");
                    #region gameWrite
                    try
                    {
                        #region GameFilesFromWeb
                        if (!File.Exists(rootPath + "\\Game\\" + current.Element("appID").Value + ".wtf"))
                        {
                            File.Create(rootPath + "\\Game\\" + current.Element("appID").Value + ".wtf").Dispose();

                            Game currentGame = new Game();
                            currentGame.AppNum = current.Element("appID").Value;

                            TextWriter gameTW = new StreamWriter(rootPath + "\\Game\\" + current.Element("appID").Value + ".wtf");
                            String html = new WebClient().DownloadString("http://store.steampowered.com/api/appdetails/?appids=" + current.Element("appID").Value);

                            gameTW.WriteLine(current.Element("name").Value);
                            currentGame.Title = (current.Element("name").Value);
                            if (html.Contains("\"id\":\"2\""))
                            {
                                gameTW.WriteLine("Single-player");
                                currentGame.Single = true;
                            }
                            if (html.Contains("\"id\":\"1\""))
                            {
                                gameTW.WriteLine("Multiplayer");
                                currentGame.Multi = true;
                            }
                            if (html.Contains("\"id\":\"9\""))
                            {
                                gameTW.WriteLine("Online Co-op");
                                currentGame.Online = true;
                            }
                            if (html.Contains("\"id\":\"24\""))
                            {
                                gameTW.WriteLine("Local Co-op");
                                currentGame.Local = true;
                            }
                            if (html.Contains("\"id\":\"18\""))
                            {
                                gameTW.WriteLine("Partial Controller Support");
                                currentGame.Partial = true;
                            }
                            if (html.Contains("\"id\":\"28\""))
                            {
                                gameTW.WriteLine("Full Controller Support");
                                currentGame.Full = true;
                            }
                            if(html.Contains("\"id\":\"21\"")){
                                gameTW.WriteLine("Downloadable Content");
                                currentGame.DLC = true;
                            }

                            gameTW.Close();

                            if (boxName == userBox && currentGame.DLC == false)
                            {
                                play1Games.Add(currentGame);
                            }
                            else if (boxName == oneBox && currentGame.DLC == false)
                            {
                                play2Games.Add(currentGame);
                            }
                            else if (boxName == twoBox && currentGame.DLC == false)
                            {
                                play3Games.Add(currentGame);
                            }
                            else if (boxName == threeBox && currentGame.DLC == false)
                            {
                                play4Games.Add(currentGame);
                            }
                        }
                        #endregion
                        #region GameInfoFromFile
                        else
                        {
                            TextReader gamereader = new StreamReader(rootPath + "\\Game\\" + current.Element("appID").Value + ".wtf");

                            Game currentGame = new Game();

                            currentGame.AppNum = current.Element("appID").Value;

                            currentGame.Title = gamereader.ReadLine();

                            string document = gamereader.ReadToEnd();

                            if (document.Contains("Single-player"))
                            {
                                currentGame.Single = true;
                            }
                            if (document.Contains("Multiplayer"))
                            {
                                currentGame.Multi = true;
                            }
                            if (document.Contains("Online Co-op"))
                            {
                                currentGame.Online = true;
                            }
                            if (document.Contains("Local Co-op"))
                            {
                                currentGame.Local = true;
                            }
                            if (document.Contains("Partial Controller Support"))
                            {
                                currentGame.Partial = true;
                            }
                            if (document.Contains("Full Controller Support"))
                            {
                                currentGame.Full = true;
                            }
                            if(document.Contains("Downloadable Content")){
                                currentGame.DLC = true;
                            }

                            if (boxName == userBox && currentGame.DLC == false)
                            {
                                play1Games.Add(currentGame);
                            }
                            else if (boxName == oneBox && currentGame.DLC == false)
                            {
                                play2Games.Add(currentGame);
                            }
                            else if (boxName == twoBox && currentGame.DLC == false)
                            {
                                play3Games.Add(currentGame);
                            }
                            else if (boxName == threeBox && currentGame.DLC == false)
                            {
                                play4Games.Add(currentGame);
                            }

                        }
                        #endregion

                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(e.Message.ToString());
                    }
                    #endregion
                    twt.WriteLine("<gameName>" + current.Element("name").Value + "</gameName>");

                    twt.WriteLine("</game>");
                  }  
                twt.WriteLine("</profile>");
                twt.Close();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Broken?");

            }
        }

        protected void GetProfile(String name, String boxName, bool statement)
        {
            if (!name.Equals("") && !name.Equals(null) && !name.Equals(boxName) && statement == true)
            {
                try
                {
                    xml = new WebClient().DownloadString("http://www.steamcommunity.com/id/" + name + "/games?xml=1");
                    
                    
                    if (xml.Contains("error"))
                    {
                        idNotCustom = true;
                    }
                   
                    if (idNotCustom == true)
                    {

                        xml = new WebClient().DownloadString("http://www.steamcommunity.com/profiles/" + name + "/games?xml=1");
                        idNotCustom = false;
                        throw new Exception("fail");
                    }
                    

                }
                catch (Exception)
                {
                    

                    MessageBox.Show("Name/ID entered for User profile failed to load");
                }
                

                #region ifFilenotThere
                if (!File.Exists(rootPath + "\\UserProfile\\" + name + ".wtf"))
                {

                    try
                    {
                        File.Create(rootPath + "\\UserProfile\\" + name + ".wtf").Dispose();

                        downWrite(name, boxName, statement);
                        if (boxName == userBox)
                        {
                            sendUser = true;
                        }
                        if (boxName == oneBox)
                        {
                            sendOne = true;
                        }
                        if (boxName == twoBox)
                        {
                            sendTwo = true;
                        }
                        if (boxName == threeBox)
                        {
                            sendThree = true;
                        }
                        

                    }
                    catch (Exception)
                    {


                    }
                }
                #endregion
                #region elseexists
                else
                {
                    try
                    {
                        downWrite(name, boxName, statement);
                        if (boxName == userBox)
                        {
                            sendUser = true;
                        }
                        if (boxName == oneBox)
                        {
                            sendOne = true;
                        }
                        if (boxName == twoBox)
                        {
                            sendTwo = true;
                        }
                        if (boxName == threeBox)
                        {
                            sendThree = true;
                        }
                    }
                    catch
                    {

                    }
                } 
                #endregion
            }


        }

        private void clearProfileClick(object sender, RoutedEventArgs e)
        {
            string directoryPath = rootPath +"\\UserProfile";
            Directory.GetFiles(directoryPath).ToList().ForEach(File.Delete);
            Directory.GetDirectories(directoryPath).ToList().ForEach(Directory.Delete);
        }

        private void clearGameClick(object sender, RoutedEventArgs e)
        {
            string directoryPath = rootPath + "\\Game";
            Directory.GetFiles(directoryPath).ToList().ForEach(File.Delete);
            Directory.GetDirectories(directoryPath).ToList().ForEach(Directory.Delete);
        }
    }
}