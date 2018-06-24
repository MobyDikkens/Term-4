import kpi.fiot.java.text.entities.abstracts.IDisplayble;
import kpi.fiot.java.text.entities.objective.Text;

public class Main {

    private static final int studentNumber = 6329;
    private static final int C3 = studentNumber % 3;//2 String
    private static final int C13 = studentNumber % 17;//5 Words without reapeatings
    //sorted by first letter


    public static void main(String[] args) {




        Text txt = new Text("Hello world!Hi.Whats, ,up?");
        IDisplayble printer =  (String s) -> System.out.println(s);
        txt.print(printer);
    }
}
