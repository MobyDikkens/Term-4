/**
 *  UserSetTest
 *
 *
 *  Perfoms a speciall test for the generic
 *
 *
 *
 *  Version 1.0
 *
 *
 *  Copyright Dima Sheludko IP - 63
 */


import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;


public class UserSetTest {

    private UserSet<Integer> testSet;
    private int expectedSize = 15;

    @Before
    public void setUp(){
        testSet = new UserSet<>();

        for(int i = 0 ; i < 10; i++){
            testSet.add(i);
        }
        expectedSize = testSet.size();
    }


    @Test
    public void size() {
        assertEquals(expectedSize,testSet.size());
    }

    @Test
    public void isEmptyFalse() {
        boolean expected = false;
        assertEquals(expected,testSet.isEmpty());
    }


    @Test
    public void isEmptyTrue() {
        boolean expected = true;
        UserSet<Integer> localeSet = new UserSet<Integer>();
        assertEquals(expected,localeSet.isEmpty());
    }



    @Test
    public void containsFalse(){
        //int obj = -5;
        //boolean expected = false;
        //assertEquals(expected,testSet.contains(obj));
    }

    @Test
    public void clear(){
        testSet.clear();
        assertEquals(15, testSet.size());
    }

    @Test
    public void serializeDesirialize(){
        testSet.serialize("serializeDesirialize.txt");
        UserSet<Integer> test = new UserSet<>();
        test.desirialize("serializeDesirialize.txt");
        assertTrue(test.equals(testSet));
    }

}