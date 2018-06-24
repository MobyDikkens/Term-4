/*  Vegetable Test
 *
 *   A unit test to check
 *   a Vegetable class
 *
 *   Version 1.0
 *   Copyright Mish Sitnik 2018
 */



import org.junit.Test;

import java.awt.*;

import static org.junit.Assert.*;

public class VegetableTest {

    @Test
    public void name() {
        String expected = "Potato";
        assertEquals(expected, new Potato().Name());
    }

    @Test
    public void kKal() {
        Potato potato = new Potato();
        int expected = 26;

        assertEquals(expected, potato.kKal());
    }

    @Test
    public void color() {
        Color expected = Color.red;
        assertEquals(expected, new Tomato().Color());
    }

    @Test
    public void hashCodeTest() {
        Potato potato = new Potato();
        int expected = 26;

        assertEquals(expected, potato.hashCode());
    }

    @Test
    public void equals() {
        Potato potato = new Potato();
        assertTrue(potato.equals(potato));
    }
}