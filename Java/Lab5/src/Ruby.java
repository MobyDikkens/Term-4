/*  Ruby
 *
 *   Class reflect ruby
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */



public class Ruby extends Precious {
    private static final int coeff = 10;

    public Ruby(int weight, int transperency, int carat) throws StoneException {
        super("Ruby",weight,transperency,carat,weight*transperency*carat*coeff);
    }
}
