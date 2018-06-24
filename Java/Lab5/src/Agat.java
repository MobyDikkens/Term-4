/*  Agat
*
*   Class reflect agat
*
*   Version 1.0
*
*   Copyright Dima Sheludko IP - 63 2018
 */


public class Agat extends Semiprecious {
    private static final int coeff = 5;

    public Agat(int weight, int transperency, int carat) throws StoneException {
        super("Agat",weight,transperency,carat,weight*transperency*carat*coeff);
    }
}
