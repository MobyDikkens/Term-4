/*  Salate
*
*   A special container intended
*   for Vegatables collection
*
*   Version 1.0
*   Copyright Mish Sitnik 2018
 */


import java.awt.*;
import java.io.*;

public class Salate  {

    private Vegetable[] vegetables = null;
    private int size = 5;
    private int pointer = 0;
    private VegetableConverter converter = new VegetableConverter();

    //region Constructors
    public Salate(){
        this.vegetables = new Vegetable[this.size];
    }

    public Salate(int size){

        if(size < 0) throw new SalateException("the size value is incorrect");

        this.size = size;
        this.vegetables = new Vegetable[this.size];

    }

    //endregion

    //region Helpers

    //  Helps reallocate internal memory
    private void reallocate(){
        Vegetable[] tmp = vegetables;
        this.size *= 2;
        this.vegetables = new Vegetable[this.size];
        for(int i = 0; i < this.pointer; i++){
            this.vegetables[i] = tmp[i];
        }
    }

    private void swap(int i, int j){
        Vegetable tmp = vegetables[i];
        vegetables[i] = vegetables[j];
        vegetables[j] = tmp;
    }

    //endregion

    public void convert(String path){

        String result = "";
        for(Vegetable v : vegetables){
            if(v != null)
                result += converter.convert(v);
        }
        writeToFile(path, result);
    }

    public void unconvert(String path){
        try {
            String text = readFromFile(path);

            String[] splitted = text.split("\r\n\r\n");

            Vegetable vegetable = null;

            for(String curr : splitted){
                if(curr != null) {
                    vegetable = converter.unconvert(curr);
                    add(vegetable);
                }
            }


        } catch (Exception ex){

        }
    }

    private void writeToFile(String path, String value){
        Writer writer = null;

        try {
            writer = new BufferedWriter(new OutputStreamWriter(
                    new FileOutputStream(path), "utf-8"));
            writer.write(value);
        } catch (IOException ex) {
            // Report
        } finally {
            try {writer.close();} catch (Exception ex) {/*ignore*/}
        }
    }

    private String readFromFile(String path) throws IOException {
        BufferedReader br = new BufferedReader(new FileReader(path));
        try {
            StringBuilder sb = new StringBuilder();
            String line = br.readLine();

            while (line != null) {
                sb.append(line);
                sb.append(System.lineSeparator());
                line = br.readLine();
            }
            String everything = sb.toString();
            return everything;
        } catch (Exception ex){
            return null;
        } finally {
            br.close();
        }
    }

    //region Methods

    public void add(Vegetable vegetable){
        if(vegetable != null) {
            if (pointer < size) {
                this.vegetables[pointer] = vegetable;
                this.pointer++;
            } else {
                reallocate();
                add(vegetable);
            }
        }
    }

    public void show(){
        for(int i = 0; i < this.pointer; i++){
            System.out.println(this.vegetables[i].Name()+":"+this.vegetables[i].kKal());
        }
    }




    public Vegetable search(int startkKal, int endkKal){

        if(startkKal > endkKal) throw new SalateException("end value should be bigger than start value");

        for(int i = 0; i < this.pointer; i++){
            int tmp = vegetables[i].kKal();
            if((tmp > startkKal) && (tmp < endkKal)
                    && (vegetables[i] != null)){
                return vegetables[i];
            }
        }
        return null;
    }


    public void sort(){
       for(int i = 0; i < pointer; i++){
           int max = i;
           for(int j = i; j < pointer; j++){
               if(vegetables[max].hashCode() < vegetables[j].hashCode()){
                   max = j;
               }
           }
           swap(max,i);
       }

    }

    public int kKal(){

        int summ = 0;

        for(int i = 0; i < pointer; i++){
            summ += this.vegetables[i].kKal();
        }

        return summ;
    }

    //endregion


}
