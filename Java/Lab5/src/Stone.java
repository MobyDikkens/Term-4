/*  Stone
*
*   Provide abstranct stone functionallity
*
*   Version 1.0
*
*   Copyright Dima Sheludko IP-63 2018
 */


public abstract class Stone {
    private String name = null;
    private int weight = 0;
    private int transperency = 0;
    private int carat = 0;
    private int cost = 0;
    public abstract boolean isPrecious();

    public int weight(){
        return this.weight;
    }

    public String name(){
        return this.name;
    }


    public int transperency(){
        return this.transperency;
    }

    public int carat(){
        return this.carat;
    }

    public int cost(){
        return this.cost;
    }

    public Stone(){
        this.weight = 0;
        this.transperency = 0;
        this.carat = 0;
        this.cost = 0;
        this.name = null;
    }

    public Stone(String name,int weight, int transperency, int carat, int cost) throws StoneException {
        if((weight > 0) && (carat > 0)
                && (transperency > 0)
                && (cost > 0)
                && (name != null)
                && (!name.isEmpty())){
            this.weight = weight;
            this.carat = carat;
            this.transperency = transperency;
            this.cost = cost;
            this.name = name;
        } else {
            throw new StoneException("Error while creating a Stone instance");
        }
    }

    @Override
    public boolean equals(Object o){
        Stone other = (Stone)o;
        if(other == null) return false;

        boolean n =  name.equals(other.name);
        boolean w = (weight == other.weight);
        boolean t = (other.transperency == transperency);
        boolean c = ( other.carat == carat);

        return n && w && t && c;
    }

}
