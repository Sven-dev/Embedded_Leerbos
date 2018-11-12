using UnityEngine;
public class GlobalVariables
{
    private static GlobalVariables _instance;
    private static GlobalVariables Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = ClassToXmlFileIO.Load<GlobalVariables>("Leerbos", "Save");
                if (_instance == null)
                {
                    _instance = new GlobalVariables();
                    ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
                }
            }

            return _instance;
        }
    }

    public static int MainSquareState
    {
        get{ return Instance._mainSquareState; }
        set
        {
            Instance._mainSquareState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int TownState
    {
        get { return Instance._townState; }
        set
        {
            Instance._townState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int BakeryState
    {
        get { return Instance._bakeryState; }
        set
        {
            Instance._bakeryState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int MarketState
    {
        get { return Instance._marketState; }
        set
        {
            Instance._marketState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public int _mainSquareState = 0;   //0 = default, 1 = introduced, 2 = completed
    public int _townState = 0;         //0 = default, 1 = introduced, 2 = completed
    public int _bakeryState = 0;       //0 = default, 1 = game 1 introduced, 2 = game 1 completed, 3 = game 2 introduced, 4 = game 2 completed
    public int _marketState = 0;       //0 = default, 1 = game 1 introduced, 2 = game 1 completed, 3 = game 2 introduced, 4 = game 2 completed
}