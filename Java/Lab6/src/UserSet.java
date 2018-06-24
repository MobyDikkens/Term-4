/*  UserSet
 *
 *   Perform a generic user
 *   collection that implements
 *   Set of T
 *
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */


import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.*;
import java.util.Collection;

/**
 * @param <T>
 */
public class UserSet<T> implements Set<T> {

    private T[] array = null;
    private int size = 15;
    private static final double delta = 0.3;
    private int current = -1;
    private UserSetSerializer<T> serializer = new UserSetSerializer<>();;


    public int current(){
        return current;
    }

    public double delta(){
        return delta;
    }


    //region Serialization Methods

    public void serialize(String fileName){


        serializer.desirialized = this;

        String result = serializer.serialize();
        try {
            writeToFile(fileName, result);
        } catch (Exception ex) {
            System.out.println(ex);
        }

    }

    private void writeToFile(String fileName, String value) throws IOException {
        String str = "World";
        BufferedWriter writer = new BufferedWriter(new FileWriter(fileName, true));
        writer.append(value);

        writer.close();
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


    public void desirialize(String fileName){

        try {
            String file = readFile(fileName);

            serializer.serialized = file;
            CollectionConfigs configs = serializer.getConfigs();
            size = configs.size;
            current = configs.current;
            UserSet<T> des = (UserSet<T>) serializer.desirialize();

            for (int i = 0; i <= des.current; i++) {
                array[i] = des.array[i];
            }
        } catch (Exception ex){

        }
    }


    public void show(){
        System.out.println("Size:" + size);
        System.out.println("Delta:" + delta);
        System.out.println("Current:" + current);

        for(int i = 0; i <= current; i++){
            System.out.println(array[i]);
        }
    }

    //endregion

    //region Constructors

    public UserSet() {
        this.array = (T[]) new Object[this.size];
    }

    public UserSet(T object) {
        this.array = (T[]) new Object[this.size];
        if (object != null) {
            this.current++;
            this.array[this.current] = object;
        }
    }


    public UserSet(ArrayList<T> list) {
        this.array = (T[]) new Object[this.size];

        if (list != null && list.size() > 0) {
            for (T element : list) {
                this.add(element);
            }
        }
    }

    //endregion


    //region Set Methods

    @Override
    public int size() {
        return this.size;
    }

    @Override
    public boolean isEmpty() {
        return this.current <= 0;
    }

    @Override
    public boolean contains(Object o) {

        if(o == null) throw new UserSetArgumentException("cannot resolve null - reference object");

        for(int i =0 ; i <= current; i++){
            String s1 = new String(""+array[i]);
            String  s2 = new String("" + (T)o);
            if(s1.equals(s2)) return true;
        }
        return false;
    }

    @Override
    public Iterator<T> iterator() {
        return Arrays.asList(array).iterator();
    }

    @Override
    public Object[] toArray() {
        return (Object[]) array;
    }

    @Override
    public <T1> T1[] toArray(T1[] a) {
        return (T1[]) array;
    }

    @Override
    public boolean add(T t) {
        if(!t.equals(null)) {
            if (this.current + 1 < this.size) {
                this.current++;
                this.array[this.current] = t;
                return true;
            } else {
                T[] tmp = this.array;
                this.size *= 1 + delta;
                this.array = (T[]) new Object[this.size];
                System.arraycopy(tmp, 0, this.array, 0, tmp.length);
                this.add(t);
                return false;
            }
        } else {
          return false;
        }
    }

    @Override
    public boolean remove (Object o) {

        if(o == null) throw new UserSetArgumentException("cannot remove null - reference object");

        for (int i = 0; i < array.length; i++) {
            if (array[i].equals(o)) {

                for (int j = i; j < array.length - 1; j++) {
                    array[j] = array[j + 1];
                }

                return true;

            }
        }
        return false;
    }

    @Override
    public boolean containsAll(Collection<?> c) {

        if(c == null) throw new UserSetArgumentException("cannot resolve null - reference object");

        for (T elemet : array) {
            if (elemet != null && !c.contains(elemet)) {
                return false;
            }
        }
        return true;
    }

    @Override
    public boolean addAll(Collection<? extends T> c) {

        if(c == null) throw new UserSetArgumentException("cannot resolve null - reference object");

        for (T elemet : c) {
            if (!this.add(elemet)) {
                return false;
            }
        }
        return true;
    }

    @Override
    public boolean retainAll(Collection<?> c) {

        if(c == null) throw new UserSetArgumentException("cannot resolve null - reference object");

        Iterator iterator = c.iterator();
        Object element;
        while (iterator.hasNext()) {
            element = iterator.next();
            if (!this.equals(element)) {
                this.remove(element);
            }
        }
        return true;
    }

    @Override
    public boolean removeAll(Collection<?> c) {

        if(c == null) throw new UserSetArgumentException("cannot resolve null - reference object");

        Iterator iterator = c.iterator();
        while (iterator.hasNext()) {
            if (!this.remove(iterator.next())) {
                return false;
            }
        }
        return true;
    }

    @Override
    public void clear() {
        this.size = 15;
        this.current = -1;
        this.array = (T[]) new Object[this.size];
    }


    @Override
    public boolean equals(Object other){
        UserSet<T> set = (UserSet<T>)other;
        if(other == null) return false;

        for(int i = 0; i <= current; i++){
            if(!set.contains(array[i])) return false;
        }

        return true;

    }
    //endregion


    //region class Serializer


    private class UserSetSerializer<T> implements ISerializable {


        public String serialized = null;

        public UserSet<T> desirialized = null;

        private int current = 0;


        private void append(String name, String value){
            this.serialized += "<" + name + ">" + value + "</" + name + ">\r\n";
        }

        private void append(Object[] set){
            this.serialized += "<Set>\r\n";
            for(int i = 0; i < set.length; i++){
                if(set[i] != null) {
                    this.serialized += set[i];
                    this.serialized += "\r\n";
                }
            }
            this.serialized += "</Set>\r\n\r\n";
        }

        @Override
        public String serialize() {
            if(desirialized != null){
                serialized = "";
                append("Size", "" + desirialized.size());
                append("Delta", "" + desirialized.delta());
                append("Current", "" + desirialized.current());

                append(desirialized.toArray());
            }
            return this.serialized;
        }


        @Override
        public Object desirialize() {
            UserSet<T> collection = new UserSet<>();

            for(int i =0 ; i <= current; i++){
                int start = serialized.indexOf("\n");
                int end = serialized.indexOf("\r", start + 1);
                String s = serialized.substring(start + 1, end);
                T value = (T)s;
                serialized = serialized.substring(end);
                collection.add(value);
            }

            return collection;
        }

        public CollectionConfigs getConfigs(){
            CollectionConfigs conf = new CollectionConfigs();
            conf.size = Integer.parseInt(getNext());
            conf.delta = Double.parseDouble(getNext());
            conf.current = Integer.parseInt(getNext());
            current = conf.current;
            return conf;

        }

        private String getNext(){
            int start = serialized.indexOf(">");
            int end = serialized.indexOf("<",start);
            String result = serialized.substring(start + 1, end);
            serialized = serialized.substring(serialized.indexOf("\n") + 1);
            return result;
        }


    }


    //endregion

}
