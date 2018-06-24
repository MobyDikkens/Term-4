/*Main class that contain main working thread
*
*
*
* Copyright 2018 Dima Sheludko Maksimovich IP-63
 */



public class Main {

    private static final int studentNumber = 6329;
    private static final int C3 = studentNumber % 3;//2   String
    private static final int C13 = studentNumber % 17;//5  Words without reapeatings sorted by first letter




    public static void main(String[] args) {
        try {
            StringHandler sh = new StringHandler("Hi my name is Dima Dima my My Hi Hi    Hi");
            sh.print();
        } catch (Exception ex){
            System.out.println(ex);
        }
    }
}
