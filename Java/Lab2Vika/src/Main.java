/* Class contain main thread and all buisness logic
*
*
* Victoria Bondar 6302 IP - 63
 */



public class Main {

    private static final int studentsBook = 6302;

    private static final int C5 = studentsBook % 5;// 2 C = A + B

    private static final int C7 = studentsBook % 7;// 2 short

    private static final int C11 = studentsBook % 11;// 10 Avarage


    public static boolean isEqual(short[][] matrix1, short[][] matrix2){

        if ((matrix1 != null)
               && (matrix2 != null)
               && (matrix1.length == matrix2.length)) {

            for (int i = 0; i < matrix1.length; i++) {
                if (matrix1[i].length != matrix2[i].length) {
                    return false;
                }
            }

            return true;
        }else{
            return false;
        }

    }

    public static void printMatrix(short[][] matrix){
        if(matrix != null){
            for(int i = 0; i < matrix.length; i++){
                for(int j = 0; j < matrix[i].length; j++){
                    System.out.print(matrix[i][j] + ";");
                }
                System.out.println();
            }
        }
    }


    public static void main(String[] args) {

        short[][] A = {
                {1,2,3},
                {5,-2,1},
                {22,1,5},
                {1,2,3}
        };

        short[][] B = {
                {2,6,8},
                {14,-22,1},
                {1,-3,5},
                {2,5,8}
        };


        short[][] C = null;


        try{

            // C = A + B , so A must be equal B

            if((A != null)
                    && (B != null)
                    && (isEqual(A, B))){

                C = new short[A.length][A[0].length];

                int summ = 0;

                // Calculating C matrix
                for(int i = 0; i < A.length; i++){
                    for(int j = 0; j < A[i].length; j++){

                        summ = A[i][j] + B[i][j];

                        if((summ < Short.MAX_VALUE)
                                && (summ > Short.MIN_VALUE)){
                            C[i][j] = (short)summ;
                        }else{
                            System.out.println("Short buffer was been overflowed");
                            return;
                        }

                    }
                }

                System.out.println("C:");
                printMatrix(C);

                int count = 0;
                double avarage = 0f;


                // Calculating avarage in C matrix
                for(int i = 0; i < C.length; i++) {
                    for (int j = 0; j < C[i].length; j++) {
                        avarage += C[i][j];
                        count ++;
                    }
                }

                if(count != 0){
                    avarage /= count;
                }else {
                    System.out.println("Divizion by zero!");
                    return;
                }


                System.out.println("\nAvarage element in C matrix:\n" + avarage);


            }else{
                System.out.println("An unexpected matrix size!");
            }



        }catch (Exception e){
            System.out.println(e.toString());
        }


    }
}

















