/*  VegetableConverter
 *
 *   Converts to file
 *
 *   Version 1.0
 *   Copyright Misha Sitnik 2018
 */



public class VegetableConverter {

    private String converted = "";

    public String convert(Vegetable vegetable){
        if(vegetable == null){
            throw new VegetableException("in convert vegetable parametr is unexpected");
        }

        converted = "";

        append("Name", vegetable.Name());
        converted += ",\r\n";
        append("kKal", ""+vegetable.kKal());
        converted += ",\r\n";
        append("Color", "" + vegetable.Color());
        converted += "\r\n\r\n";

        return converted;
    }

    public Vegetable unconvert(String converted){
        Vegetable result = null;

        String name = getName(converted);

        switch (name){
            case "Tomato":{
                result = new Tomato();
                break;
            }
            case "Cucumber":{
                result = new Cucumber();
                break;
            }
            case "Corn":{
                result = new Corn();
                break;
            }
            case "Potato":{
                result = new Potato();
                break;
            }
            case "Peas":{
                result = new Peas();
                break;
            }
        }

        return result;
    }

    private String getName(String value){
        String splitter = "\"Name\":";
        //  +- 1 to trim the quotes
        int start = value.indexOf(splitter) + splitter.length() + 1;
        int end = value.indexOf(",", start) - 1;

        String result = value.substring(start, end);
        return result;
    }


    private void append(String atribute, String value){
        converted += "\"" + atribute + "\"";
        converted += ":";
        converted += "\"" + value + "\"";
    }

}
