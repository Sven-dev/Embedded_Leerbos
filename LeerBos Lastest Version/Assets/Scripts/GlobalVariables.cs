public class GlobalVariables
{
    public int _mainSquareState = 0;   //0 = default, 1 = introduced, 2 = game completed
    public int _townState = 0;         //0 = default, 1 = introduced, 2 = area completed
    public int _bakeryState = 0;       //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _marketState = 0;       //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _forestState = 0;       //0 = default, 1 = introduced, 2 = area completed
    public int _towerState = 0;        //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _mazeState = 0;         //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _shedState = 0;         //0 = default, 1 = introduced, 2 = area completed
    public int _musicState = 0;    //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _craftingState = 0; //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _mountainState = 0;     //0 = default, 1 = introduced, 2 = area completed
    public int _readingState = 0;      //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
    public int _flowerState = 0;       //0 = default, 1 = game 1 completed, 2 = game 2 completed, 3 = icon in kart
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

    public static int ForestState
    {
        get { return Instance._forestState; }
        set
        {
            Instance._forestState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }
    public static int TowerState
    {
        get { return Instance._towerState; }
        set
        {
            Instance._towerState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int MazeState
    {
        get { return Instance._mazeState; }
        set
        {
            Instance._mazeState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int ShedState
    {
        get { return Instance._shedState; }
        set
        {
            Instance._forestState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }
    public static int MusicState
    {
        get { return Instance._musicState; }
        set
        {
            Instance._towerState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int CraftingState
    {
        get { return Instance._craftingState; }
        set
        {
            Instance._mazeState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int MountainState
    {
        get { return Instance._shedState; }
        set
        {
            Instance._mountainState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }
    public static int ReadingState
    {
        get { return Instance._musicState; }
        set
        {
            Instance._readingState = value;
            ClassToXmlFileIO.Save("Leerbos", "Save", _instance);
        }
    }

    public static int FlowerState
    {
        get { return Instance._craftingState; }
        set
        {
            Instance._flowerState = value;
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
        ForestState = 0;
        TowerState = 0;
        MazeState = 0;
        ShedState = 0;
        MusicState = 0;
        CraftingState = 0;
        MountainState = 0;
        ReadingState = 0;
        FlowerState = 0;
        Standalone = false;
    }

    //Checks if every game area has been completed
    private static void CheckVictory()
    {
        if (TownState == 2 && ForestState ==2)
        {
            MainSquareState = 2;
        }
    }
}