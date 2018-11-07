public static class GlobalVariables
{
    public static bool MainSquareIntroduced = false;

    #region Town
    public static bool TownIntroduced = false;

    public static bool TownCompleted
    {
        get
        {
            if (BakeryCompleted && MarketCompleted)
            {
                return true;
            }

            return false;
        }
    }

    #region Bakery
    public static bool Bakery1Introduced = false;
    public static bool Bakery1Completed = false;
    public static bool Bakery2Introduced = false;
    public static bool Bakery2Completed = false;

    public static bool BakeryCompleted
    {
        get
        {
            if (Bakery1Completed && Bakery2Completed)
            {
                return true;
            }

            return false;
        }
    }
    #endregion

    #region Market
    public static bool Market1Introduced = false;
    public static bool Market1Completed = false;
    public static bool Market2Introduced = false;
    public static bool Market2Completed = false;

    public static bool MarketCompleted
    {
        get
        {
            if (Market1Completed && Market2Completed)
            {
                return true;
            }

            return false;
        }
    }
    #endregion

    #endregion
}