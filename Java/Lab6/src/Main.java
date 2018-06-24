/*  Main
 *
 *   Perform an entry point
 *   to the entire project
 *
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */
public class Main {

    private static final int studentNumber  = 6329;
    private static final int c2 = studentNumber % 2;//  1
    private static final int c3 = studentNumber % 3;//  2

    public static void main(String[] args) {

        UserSet<Integer> set = new UserSet<>();

        set.add(21);
        set.add(21);
        set.add(21);

        //set.serialize("Test.txt");
        UserSet<Integer> t = new UserSet<>();
        t.desirialize("Test.txt");

        t.show();

    }
}

/*  Main
 *
 *   Perform an entry point
 *
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */
