/*  StoneSerializer
 *
 *   Special servicies to provide
 *   serializeon
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */


import java.lang.reflect.Executable;

public class StoneSerializer implements ISerializeble {

    private Stone _object = null;

    private String _serialized = "";
    private Stone _desirialized = null;


    @Override
    public String serialize(Object object) {
        _serialized = "";
        Stone _object = (Stone)object;

        if(_object == null) return null;

        append("Name", _object.name());
        append("Weight", ""+_object.weight());
        append("Carat", ""+_object.carat());
        append("Transperency", ""+_object.transperency());

        _serialized += "\r\n";

        return _serialized;

    }

    private void append(String name, String value){
        _serialized += "<" + name + ">" + value + "</" + name + ">\r\n";
    }

    private String getNext(){
        int start = _serialized.indexOf(">") + 1;
        int end = _serialized.indexOf("<",_serialized.indexOf(">"));
        String res =  _serialized.substring(start, end);
        _serialized = _serialized.substring(_serialized.indexOf("\n") + 1);
        return res;
    }

    @Override
    public Object desirialize(String value) {
        if(value == null) return null;

        _serialized = value;

        Stone result;

        try {
            String name = getNext();
            int weight = Integer.parseInt(getNext());
            int carat = Integer.parseInt(getNext());
            int transperncy = Integer.parseInt(getNext());

            switch (name){
                case "Diamond" : {_desirialized = new Diamond(weight, transperncy, carat); break;}
                case "Emerald" : {_desirialized = new Emerald(weight, transperncy, carat); break;}
                case "Ruby" : {_desirialized = new Ruby(weight, transperncy, carat); break;}
                case "Pearl" : { _desirialized = new Pearl(weight, transperncy, carat); break;}
                case "Agat" : {_desirialized = new Agat(weight, transperncy, carat); break; }
            }


            return _desirialized;

        } catch (Exception ex) {
            return null;
        }


    }
}
