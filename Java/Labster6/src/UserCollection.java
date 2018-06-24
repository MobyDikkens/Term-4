/*  UserCollection
*
*   A generic collection
*   implements Set of T
*
*   Version 1.0
*   Copyright Misha Sitnik 2018
 */


import java.io.*;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.Set;
import java.lang.reflect.Constructor;
import java.util.Arrays;

public class UserCollection<T> implements Set<T> {

    private int size = 10;

    private int current = -1;

    private T[] array = null;

    private UserCollectionConverted<T> converter = new UserCollectionConverted<>();


    //  region Constructors

    public UserCollection(){
        this.array = (T[])new Object[this.size];
    }

    public UserCollection(T element){
        this.current = 0;
        this.array = (T[])new Object[this.size];
        this.array[this.current] = element;
    }

    public UserCollection(ArrayList<T> array){
        for(T tmp:array){
            add(tmp);
        }
    }


    //  endregion


    //region Set Methods

    public int getCurrent(){
        return current;
    }

    @Override
    public int size() {
        return this.size;
    }

    @Override
    public boolean isEmpty() {
        return this.current < 0;
    }

    @Override
    public boolean contains(Object o) {
        for(Object tmp:array){
            String s1 = "" + o;
            String s2 = "" + tmp;
            if(tmp != null && s1.equals(s2)){
                return true;
            }
        }
        return false;
    }

    @Override
    public Iterator<T> iterator() {
        return Arrays.asList(array).iterator();
    }

    @Override
    public Object[] toArray() {
        if(current < 0) return null;

        Object[] test = new Object[current + 1];

        for(int i = 0; i <= current; i++){
            test[i] = array[i];
        }

        return (Object[])test;
    }

    @Override
    public <T1> T1[] toArray(T1[] a) {
        return (T1[])array;
    }

    @Override
    public boolean add(T t) {
        if(this.current + 1 < this.size){
            this.current++;
            this.array[this.current] = t;
            return true;
        } else {
            T[] tmp = this.array;
            this.size *= 1.5;
            this.array = (T[])new Object[this.size];
            System.arraycopy(tmp,0,this.array,0,tmp.length);
            this.add(t);
            return false;
        }

    }

    @Override
    public boolean remove(Object o) {
        for(int i = 0; i < array.length; i++){
            String s1 = array[i] + "";
            String s2 = o + "";
            if(s1.equals(s2)){

                if(i == array.length - 1){
                    array[i] = null;
                    current--;
                }

                for(int j = i; j < array.length - 1; j++){
                    array[j] = array[j + 1];
                }

                current--;
                return true;

            }
        }
        return false;
    }

    @Override
    public boolean containsAll(Collection<?> c) {

        if(c == null) throw new UserCollectionException("collection is empty");

        for(T tmp:array){
            if(!c.contains(tmp)){
                return false;
            }
        }
        return true;
    }

    @Override
    public boolean addAll(Collection<? extends T> c) {

        if(c == null) throw new UserCollectionException("collection is empty");

        for(T tmp:c){
            if(!this.add(tmp)){
                return false;
            }
        }
        return true;
    }

    @Override
    public boolean retainAll(Collection<?> c) {

        if(c == null) throw new UserCollectionException("collection is empty");

        Iterator iterator = c.iterator();
        Object tmp;
        while (iterator.hasNext()){
            tmp = iterator.next();
            if(!this.equals(tmp)){
                this.remove(tmp);
            }
        }
        return true;
    }

    @Override
    public boolean removeAll(Collection<?> c) {
        Iterator iter = c.iterator();
        while (iter.hasNext()){
            if(!this.remove(iter.next())){
                return false;
            }
        }
        return true;
    }

    @Override
    public void clear() {
        this.size = 10;
        this.current = -1;
        this.array = (T[]) new Object[this.size];
    }
    //endregion

    public void convert(String path){
        String text = converter.convert(this);
        writeToFile(path,text);
    }

    public void unconvert(String path){
        try {
            String text = readFromFile(path);
            int size = converter.getSize(text);
            int current = converter.getCurrent(text);

            this.size =size;
            this.current = current;
            this.array = (T[])new Object[size];

            T[] array = converter.unconvert(text);

            for(int i = 0; i < array.length; i++){
                this.array[i] = array[i];
            }



        } catch (Exception ex){
            System.out.println(ex);
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

}
