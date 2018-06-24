/*  Diamond
 *
 *   Class reflect diamond
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */


public class Diamond extends Precious {
    private static final int coeff = 15;

    public Diamond(int weight, int transperency, int carat) throws StoneException {
        super("Diamond",weight,transperency,carat,weight * carat * transperency * coeff);
    }
}
