/*  Pearl
 *
 *   Class reflect pearl
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */



public class Pearl extends Semiprecious {
    private static final int coeff = 12;

    public Pearl(int weight, int transperency, int carat) throws StoneException {
        super("Pearl",weight,transperency,carat,weight*transperency*carat*coeff);
    }
}
