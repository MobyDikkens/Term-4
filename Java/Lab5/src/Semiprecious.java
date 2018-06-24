/*  Semiprecious
 *
 *   Provide abstranct Semiprecious functionallity
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP-63 2018
 */


public abstract class Semiprecious extends Stone {
    @Override
    public boolean isPrecious() {
        return false;
    }

    public Semiprecious(String name,int weight, int transperency, int carat, int cost) throws StoneException {
        super(name,weight,transperency,carat,cost);
    }
}
