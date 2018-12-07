public class GlobalVariables
{
    public int _mainSquareState = 0;   //0 = default, 1 = introduced, 2 = game completed
    public int _townState = 0;         //0 = default, 1 = introduced, 2 = area completed
    public int _bakeryState = 0;       //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _marketState = 0;       //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public bool _Standalone = false;

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
            CheckVictory();
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

    public static bool Standalone
    {
        get { return Instance._Standalone; }
        set
        {
            Instance._Standalone = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static void Reset()
    {
        MainSquareState = 0;
        TownState = 0;
        BakeryState = 0;
        MarketState = 0;
        Standalone = false;
    }

    //Checks if every game area has been completed
    private static void CheckVictory()
    {
        if (TownState == 2)
        {
            MainSquareState = 2;
        }
    }
}