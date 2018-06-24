

public class Calculator
{
    //    Constants
    private static final int _id = 6329;

    private final int _c = this._c3;

    //  Non constant variaables
    private int _c2 = 1;
    private int _c3 = 2;
    private int _c5 = 1;
    private int _c7 = 1;



    private int _n = 0;
    private int _m = 0;


    //    Constructors


    /*public Calculator()
    {
        this._c2 = this._id % 2;
        this._c3 = this._id % 3;
        this._c5 = this._id % 5;
        this._c7 = this._id % 7;

    }*/

    public Calculator(int n,int m)
    {

        this._n = n;
        this._m = m;
    }


    //   Methods



    public int GetN()
    {
        return this._m;
    }

    public int GetM()
    {
        return this._m;
    }


    public int Calculate()
    {
        int S = 0;

        try
        {

            int up = 0, down = 0;

            for (short i = 0; i < this._n; i++)
            {
                for (short j = 0; j < this._m; j++)
                {
                    if(j != 0)
                    {
                        up = i/j;
                        down = i - this._c;


                        if(down != 0)
                        {
                            S += up/down;
                        }

                    }


                }
            }

        }
        catch (Exception ex)
        {
            System.out.print(ex);
        }


        return  S;
    }


}
