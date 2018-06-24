import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class NecklaceTest {

    private Necklace necklace;
    private int expectedCost = 1;

    @Before
    public void setUp() {
        necklace = new Necklace();
        necklace.add(new Agat(1, 2, 3));
        necklace.add(new Diamond(1, 2, 3));
        necklace.add(new Emerald(1, 2, 3));
        necklace.add(new Pearl(1, 2, 3));
        necklace.add(new Ruby(1, 2, 3));
        expectedCost = (int)Math.pow(1 * 2 * 3, 5);
        expectedCost *= 5;
        expectedCost *= 15;
        expectedCost *= 12;
        expectedCost *= 12;
        expectedCost *= 10;
    }

    @Test
    public void cost() {
        assertEquals(expectedCost, necklace.cost());
    }

    @Test
    public void weight() {
        int expectedWeight = 1 * 5;
        assertEquals(expectedWeight, necklace.weight());
    }

    @Test
    public void carat() {
        int expectedCarat = 2 * 5;
        assertEquals(expectedCarat, necklace.carat());
    }

    @Test
    public void search() {
        Stone toSearch = new Diamond(11,12,111);
        necklace.add(toSearch);
        assertEquals(necklace.search(110,115), toSearch);
    }

    @Test
    public void serializeDesirializeTest(){
        try {
            necklace.serialize("serializeDesirializeTest.txt");

            Necklace test = new Necklace();
            test.desirialize("serializeDesirializeTest.txt");

            assertTrue(test.contains(necklace));


        } catch (Exception ex) {

        }
    }
}