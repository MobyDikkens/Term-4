/*  UserCollectionTest
 *
 *   A unit tets to chech
 *   the collection
 *
 *   Version 1.0
 *   Copyright Misha Sitnik 2018
 */




import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class UserCollectionTest {

    private UserCollection<Integer> collection;
    Integer[] array;

    @Before
    public void setUp(){
        collection = new UserCollection<>();
        array = new Integer[5];

        for (int i =0 ; i < array.length; i++) {
            array[i] = i;
            collection.add(i);
        }
    }

    @Test
    public void size() {
        int expected = 10;
        assertEquals(expected, collection.size());
    }

    @Test
    public void isEmpty() {
        assertTrue(new UserCollection<Integer>().isEmpty());
    }

    @Test
    public void isNotEmpty() {
        UserCollection<Integer> test = new UserCollection();
        test.add(1);
        assertFalse(test.isEmpty());
    }

    @Test
    public void contains() {
        UserCollection<Integer> test = new UserCollection<>();
        Integer i = new Integer(10);
        test.add(i);

        assertTrue(test.contains(i));
    }

    @Test
    public void toArray() {
        Object[] res = collection.toArray();
        Integer[] test = new Integer[res.length];

        for(int i = 0; i < test.length; i++){

            test[i] = (int)res[i];
        }

        boolean expected = true;

        if(array.length != test.length) {
            expected = false;
        } else {
            for(int i = 0; i < array.length; i++){
                if(!test[i].equals(array[i])) {
                    expected = false;
                    break;
                }
            }
        }

        assertTrue(expected);


    }

    @Test
    public void add() {
        boolean expected = true;
        for(Integer i : array){
            expected = expected && collection.contains(i);
        }
        assertTrue(expected);
    }

    @Test
    public void remove() {
        int i = 15;
        collection.add(i);
        collection.remove(i);
        assertFalse(collection.contains(i));
    }

    @Test
    public void clear() {
        collection.clear();
        assertTrue(collection.isEmpty());
    }

    @Test
    public void convertUnconvert(){
        collection.convert("1.txt");
        UserCollection<Integer> test = new UserCollection<>();
        test.unconvert("1.txt");

        boolean b = true;

        for(Object o : collection.toArray()){
            if(o != null){
                b = b && test.contains(o);
            }
        }
        assertTrue(b);
    }

}