public class UserCollectionConverted<T> {
    private String _source;
    private int _size;

    public String convert(UserCollection<T> collection){
        if(collection == null){
            throw new UserCollectionException("bad parametr");
        }

        _source = "";

        append("Size", "" + collection.size());
        _source += ",\r\n";
        append("Current", "" + collection.getCurrent());
        _source += ",\r\n";


        for (Object o : collection.toArray()){
            T tmp = (T)o;
            if(tmp != null){
                append("Element", "" + tmp);
                _source += ",\r\n";
            }
        }

        _source += "\r\n\r\n";


        return _source;
    }


    public T[] unconvert(String source){
        _source = source;

        T[] array = (T[])new Object[_size + 1];

        for(int i =0 ; i<= array.length; i++){
            T t = getElement();
            if(t != null)
                array[i] = t;
        }


        return array;
    }

    private T getElement(){
        try {
            String splitter = "\"Element\":";

            int start = _source.indexOf(splitter) + splitter.length() + 1;
            int end = _source.indexOf(",", start) - 1;

            String sub = _source.substring(start, end);

            _source = _source.substring(end + 4);

            T value = (T) sub;

            return value;
        } catch (Exception ex){
            return null;
        }
    }

    public int getSize(String source){

        String splitter = "\"Size\":";

        int start = source.indexOf(splitter) + splitter.length() + 1;
        int end = source.indexOf(",", start) - 1;

        String sub = source.substring(start, end);

        int value = Integer.parseInt(sub);

        return value;
    }

    public int getCurrent(String source){
        String splitter = "\"Current\":";

        int start = source.indexOf(splitter) + splitter.length() + 1;
        int end = source.indexOf(",", start) - 1;

        String sub = source.substring(start, end);

        int value = Integer.parseInt(sub);

        _size = value;

        return value;
    }

    private void append(String atribute, String value){
        _source += "\"" + atribute + "\"";
        _source += ":";
        _source += "\"" + value + "\"";
    }


}
