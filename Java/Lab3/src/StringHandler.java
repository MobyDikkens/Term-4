/*Class that contain all buisness logic
*Intended to process the string
*
*print the input text without repeatings
*Sorted alphabetically
*
*
* Copyright 2018 Dima Sheludko Maksimovich IP-63
 */


public class StringHandler {

    private String str = null;

    // Split the input text by input splitter
    private String[] split(String text, String separator){

        if((text == null)
                || (separator == null)
                || (text.isEmpty())
                || (separator.isEmpty())){
            return null;
        } else {
            //Delete reapeating witespaces
            String formatted = text.trim().replaceAll(separator + "+", separator);
            return formatted.split(separator);
        }

    }

    //Sorting alphabetically
    private String[] bubleSort(String[] array){
        if(array != null){

            String[] sortedArray = null;

            for(int i = 0; i < array.length; i++){
                for(int j = i + 1; j < array.length; j++){
                    if(Character.toLowerCase(array[i].charAt(0))
                            > Character.toLowerCase(array[j].charAt(0))){
                        String tmp = array[i];
                        array[i] = array[j];
                        array[j] = tmp;
                    }
                }
            }

            return  array;


        } else {
            System.out.println("Error while sorting alphabetically!");
            return null;
        }
    }


    //class Constructor
    public StringHandler(String str){

        if ((str == null)
                && (str.isEmpty()) ){
            System.out.println("Error string format!");
            System.exit(1);
        }

        this.str = str;
    }

    //Prints the result
    public void print(){

            String[] spltArray = split(this.str, " ");
            String[] sortedArray = bubleSort(spltArray);

            if(sortedArray != null){

                //to delete repeats
                boolean isPrint = true;

                //print result with filter
                //using our previous printings
                for(int i = 0; i < sortedArray.length; i++){
                    for(int j = i - 1; j >= 0; j--){
                        if(sortedArray[i].equals(sortedArray[j])){
                            isPrint = false;
                            break;
                        }
                    }

                    //condition to print without repeatings
                    if(isPrint){
                        System.out.println(sortedArray[i]);
                    }

                    //reset
                    isPrint = true;
                }

            } else {
                System.out.println("Error while splitting the text!");
                System.exit(1);
            }

    }


}
