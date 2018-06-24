/*  SalateTest
 *
 *   A special unit test to
 *   controll Salate functionallity
 *
 *   Version 1.0
 *   Copyright Mish Sitnik 2018
 */


import org.junit.Test;

import static org.junit.Assert.*;

public class SalateTest {

    private Salate salate;

    private Vegetable[] vegetables;

    @org.junit.Before
    public void setUp() {
        salate = new Salate();
        vegetables = new Vegetable[5];

        vegetables[0] = new Corn();
        vegetables[1] = new Peas();
        vegetables[2] = new Potato();
        vegetables[3] = new Tomato();
        vegetables[4] = new Cucumber();

        for(Vegetable v : vegetables){
            salate.add(v);
        }
    }

    @org.junit.Test
    public void add() {
        Vegetable test = new Potato();

        Salate testSalate = new Salate();
        testSalate.add(test);

        assertTrue(testSalate.search(test.kKal() - 1, test.kKal() + 1).equals(test));
    }

    @org.junit.Test
    public void search() {
        boolean excpected = true;
        for(Vegetable v : vegetables){
            excpected = excpected && salate.search(v.kKal() - 1, v.kKal() + 1).equals(v);
        }
        assertTrue(excpected);
    }

    @org.junit.Test
    public void kKal() {
        int expected = 0;
        for(Vegetable v : vegetables){
            expected += v.kKal();
        }
        assertEquals(expected, salate.kKal());
    }

    @Test
    public void convertUnconvert(){
        salate.convert("1.txt");

        Salate test = new Salate();
        test.unconvert("1.txt");

        test.show();

        boolean b = true;

        for(Vegetable v : vegetables){
            if(v != null){
                b = b && (test.search(v.kKal() - 1, v.kKal() + 1) != null);
            }
        }

        assertTrue(b);
    }
}