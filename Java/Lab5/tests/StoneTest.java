import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class StoneTest {

    private Stone stone;

    @Before
    public void setUp(){
        stone = new Diamond(1,2,3);
    }

    @Test
    public void isPreciousTrue() {
        Stone stone = new Diamond(1,2,3);
        assertTrue(stone.isPrecious());
    }

    @Test
    public void isPreciousFalse() {
        Stone stone = new Pearl(1,2,3);
        assertFalse(stone.isPrecious());
    }

    @Test
    public void weight() {
        int expected = 1;
        assertEquals(expected, stone.weight());
    }

    @Test
    public void name() {
        String expected = "Diamond";
        assertEquals(expected, stone.name());
    }

    @Test
    public void transperency() {
        int expected = 3;
        assertEquals(expected, stone.transperency());
    }

    @Test
    public void carat() {
        int expected = 2;
        assertEquals(expected, stone.carat());
    }

    @Test
    public void cost() {
        int expected = 1 * 2 * 3 * 15;
        assertEquals(expected, stone.cost());
    }
}