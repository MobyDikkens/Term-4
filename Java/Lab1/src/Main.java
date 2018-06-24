

public class Main {



    public static void main(String[] args) {

        try
        {
            int n = 5, m = 5;


            Calculator calculator = new Calculator(n, m);


            System.out.print(calculator.Calculate());
        }
        catch (Exception ex)
        {
            System.out.print(ex);
        }


    }
}
