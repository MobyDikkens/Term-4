/*  Emerald
 *
 *   Class reflect emerald
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */


public class Emerald extends Precious {
    private static final int coeff = 12;

    public Emerald(int weight, int transperency, int carat) throws StoneException {
        super("Emerald",weight,transperency,carat,weight*transperency*carat*coeff);
    }
}
