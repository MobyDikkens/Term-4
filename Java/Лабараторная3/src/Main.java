/*Main class
*
*
*
*Mihail Sitnik IP-63
 */



public class Main {

    private static final int C3 = 0; // StringBuilder
    private static final int C17 = 3; // Question sentences




    public static void main(String[] args) {

        try {
            StringBuilder text = new StringBuilder("Hi. How   How   are u??sds     d????     ???re        rerwer");

            TextFormatter formatter = new TextFormatter(text);
            formatter.getResult(3);
        } catch (Exception e){
            System.out.println(e);
        }
    }
}
