using System;

public class Game
{
    protected string title, appnum;
   
    protected bool single = false, localCo = false, onlineCo = false, multi = false, partialCon = false, fullCon = false; 
	
    public Game(string Title, string AppNum, bool SinglePlayer, bool Local, bool Online, bool Multi, bool Partial, bool Full)
	{
        title = Title;
        appnum = AppNum;
        single = SinglePlayer;
        localCo = Local;
        onlineCo = Online;
        multi = Multi;
        partialCon = Partial;
        fullCon = Full;
	}
    
    public Game()
    {
        title = "Broken";
    }

    public bool Single
    {
        set{
            single = value;
        }
        get{
            return single;
        }
    }

    public bool Local
    {
        set
        {
            local = value;
        }
        get
        {
            return local;
        }
    }

    public bool Multi
    {
        set
        {
            multi = value;
        }
        get
        {
            return multi;
        }
    }

    public bool Online
    {
        set
        {
            onlineCo = value;
        }
        get
        {
            return onlineCo;
        }
    }

    public bool Partial
    {
        set
        {
            partialCon = value;
        }

        get
        {
            return partialCon;
        }
    }

    public bool Full
    {
        set
        {
            fullCon = value;
        }
        get
        {
            return fullCon;
        }
    }

    public string Title
    {
        set
        {
            title = value;
        }
        get
        {
            return title;
        }
    }

    public string AppNum
    {
        get
        {
            return appnum;
        }
    }

}
