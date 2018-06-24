/*  Necklace
 *
 *   Special containier
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */


import java.io.*;
import java.util.Scanner;

public class Necklace {
    ISerializeble serializeble = new StoneSerializer()
            ;
    private Stone[] stones = null;
    private int size = 5;
    private int position = 0;

    //region Constructors

    public Necklace(){
        stones = new Stone[this.size];
    }

    public Necklace(int size){
        if(size > 0){
            this.size = size;
        }
        stones = new Stone[this.size];
    }

    //endregion

    //region Methods

    public void show(){
        for(int i = 0; i < position; i++){
            serializeble = new StoneSerializer();
            System.out.print(serializeble.serialize(stones[i]));
        }
    }


    public void add(Stone stone){

        if(stone == null) throw new StoneException("stone instance cannot be null - referenced");

        if(stone != null){
            if(position != size){
                stones[position] = stone;
                position++;
            } else {
                realloc();
                add(stone);
            }
        }
    }

    public int cost(){
        int summ = 0;
        for(int i = 0; i < position; i++){
            summ += stones[i].cost();
        }
        return summ;
    }

    public int weight(){
        int summ = 0;
        for(int i = 0; i < position; i++){
            summ += stones[i].weight();
        }
        return summ;
    }

    public int carat(){
        int summ = 0;
        for(int i = 0; i < position; i++){
            summ += stones[i].carat();
        }
        return summ;
    }

    public void sort(){
        for(int i = 0; i < position; i++){
            int max = i;
            for(int j = i; j < position; j++){
                if(stones[max].cost() < stones[j].cost()){
                    max = j;
                }
            }
            swap(max,i);
        }
    }

    public Stone search(int start, int end){

        if(end < start) throw new StoneException("start cannot be less than end");

        for(int i = 0; i < position; i++){
            if((stones[i].transperency() < end)
                    && stones[i].transperency() > start){
                return stones[i];
            }
        }
        return null;
    }


    //endregion\

    //region Serialization

    public void serialize(String fileName) throws IOException {
        if(fileName == null || fileName.isEmpty()) throw new ExceptionInInitializerError("fileName");
        try {
            BufferedWriter writer = new BufferedWriter(new FileWriter(fileName));
            for (int i = 0; i < position; i++) {
                writer.write(serializeble.serialize(stones[i]));
            }
            writer.close();
        } catch (Exception ex){

        }
    }

    public void desirialize(String fileName){
        if(fileName == null || fileName.isEmpty()) throw new ExceptionInInitializerError("fileName");
        try {

            String all = readFile(fileName);

            String[] globalSplitted = all.split("\r\n\r\n");
            String[] localSplitted;

            for(int i = 0; i < globalSplitted.length; i++){
                Stone obj = (Stone)serializeble.desirialize(globalSplitted[i]);

                add(obj);
            }


        } catch (Exception ex){

        }
    }

    private String readFile(String pathname) throws IOException {

        File file = new File(pathname);
        StringBuilder fileContents = new StringBuilder((int)file.length());
        Scanner scanner = new Scanner(file);
        String lineSeparator = System.getProperty("line.separator");

        try {
            while(scanner.hasNextLine()) {
                fileContents.append(scanner.nextLine() + lineSeparator);
            }
            return fileContents.toString();
        } finally {
            scanner.close();
        }
    }

    public boolean contains(Stone stone){
        for(int i = 0; i < position; i++){
            if(stone.equals(stones[i])) return true;
        }
        return false;
    }

    public boolean contains(Necklace necklace){
        for(int i = 0; i < necklace.position; i++){
            if(!contains(necklace.stones[i])) return false;
        }
        return true;
    }



    //endregion

    //region Helpers
    private void realloc(){
        Stone[] old = stones;
        size *= 2;

        stones = new Stone[size];

        for(int i = 0; i < position; i++){
            stones[i] = old[i];
        }

    }

    private void swap(int i, int j){
        Stone tmp = stones[i];
        stones[i] = stones[j];
        stones[j] = tmp;
    }
    //endregion

}
