/*  Vegetable
 *
 *   Contain abstract model
 *   of vegetable with all
 *   helpfull methods
 *
 *   Version 1.0
 *   Copyright Misha Sitnik 2018
 */



//  import color class, we can sort vegebles by color...
import java.awt.*;

public abstract class Vegetable {
    private String name = "Vegetable";
    private int kkal = 0;
    private Color color = Color.white;


    public Vegetable(){

    }

    public Vegetable(String name, int kkal, Color color){

        if(name == null || name.isEmpty()) throw new VegetableException("cannot assign vegetable with name");

        this.name = name;
        this.color = color;
        this.kkal = kkal;
    }


    public String Name(){
        return this.name;
    }

    public int kKal(){
        return this.kkal;
    }

    public Color Color(){
        return  this.color;
    }

    @Override
    public int hashCode(){
        return this.kkal;
    }

    @Override
    public boolean equals(Object o){
        Vegetable other = (Vegetable)o;
        if(other == null) return false;

        return other.kkal == kkal && other.color == color && other.name == name;
    }
}
