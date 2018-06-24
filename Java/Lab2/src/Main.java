/*
* Main thread
* Intended to provide operations with array
*
*
* Copyright Dima Sheludko IP - 63 2018
*
 */


public class Main {


    private static final int C5 = 6329 % 5;//4

    private static final int C7 = 6329 % 7;//1

    private static final int C11 = 6329 % 11;//4


    private static void Print(byte[][] matrix){

        if(matrix != null) {

            for (int i = 0; i < matrix.length; i++) {

                for (int j = 0; j < matrix[0].length; j++) {
                    System.out.print(matrix[i][j] + ";");
                }

                System.out.println();

            }
        }

    }



    public static void main(String[] args) {

        byte[][] A = {
                {1,2,3},
                {4,5,6},
                {7,8,9}
        };


        byte[][] B = {
                {-5,2,1},
                {2,-5,2},
                {8,5,5}
        };

        try {

            // Condition to mulpiply matrixes

            if (A.length == B[0].length) {

                byte[][] C = new byte[A.length][B[0].length];

                for (int i = 0; i < C.length; i++) {

                    for (int j = 0; j < C[0].length; j++) {
                        C[i][j] = 0;
                    }

                }

                int tmp = 0;

                for (int line = 0; line < B.length; line++) {

                    for (int column = 0; column < A[0].length; column++) {

                        for (int row = 0; row < A[0].length; row++) {

                            tmp = A[line][row] * B[row][column];

                            if (tmp > Byte.MIN_VALUE && tmp < Byte.MAX_VALUE) {
                                C[line][column] += tmp;
                            }

                        }

                    }

                }

                System.out.println("Result of first operation:");

                // Show the resulting matrix
                Main.Print(C);


                int summ = 0;
                int lim = 0;

                for (int i = 0; i < C.length; i++) {

                    lim = C[i][0];

                    for (int j = 0; j < C[i].length; j++) {

                        if (i % 2 == 0) {

                            if (C[i][j] > lim) {
                                lim = C[i][j];
                            }

                        } else {
                            if (C[i][j] < lim) {
                                lim = C[i][j];
                            }
                        }


                    }

                    summ += lim;

                }


                System.out.println("\nResult of second operation:");
                System.out.println(summ);


            } else {
                System.out.println("Sorry, but matrixes is incorrect to make a multiply");
            }
        } catch (Exception ex){
            System.out.println(ex);
        }





    }
}
