public class Main {

    public static void main(String[] args) {

        UserCollection<Integer> collection = new UserCollection<Integer>();
        collection.add(10);
        collection.add(10);
        collection.add(10);
        collection.add(10);collection.add(10);
        collection.add(10);
        collection.add(10);
        collection.add(10);
        collection.add(10);
        collection.add(10);
        collection.add(10);


        collection.convert("T.txt");
        UserCollection<Integer> test = new UserCollection<>();
        test.unconvert("T.txt");
        for(Object i : test.toArray()){
            System.out.println(i);
        }


        //System.out.println(collection.size());

    }
}
