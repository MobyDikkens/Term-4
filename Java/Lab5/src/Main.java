/*  Main
 *
 *   Entry point
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */



public class Main {

    private static final int student  = 6329;
    private static final int C17 = student % 17; // 5

    public static void main(String[] args) {
        Necklace necklace = new Necklace();

        try {
            //int weight, int transperency, int carat
            necklace.desirialize("Test.txt");

        } catch (Exception ex){
            System.out.println(ex);
        }




        necklace.show();





    }
}
