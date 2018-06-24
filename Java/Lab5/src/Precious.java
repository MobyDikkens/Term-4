/*  Precious
 *
 *   Provide abstranct Precious functionallity
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP-63 2018
 */

public abstract class Precious extends Stone {
    @Override
    public boolean isPrecious() {
        return true;
    }

    public Precious(String name,int weight, int transperency, int carat, int cost) throws StoneException {
            super(name,weight,transperency,carat,cost);
    }
}
