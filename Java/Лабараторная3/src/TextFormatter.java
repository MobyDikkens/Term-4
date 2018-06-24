/*Contain all logic
*
*
*
* Mihail Sitnik IP-63
 */


public class TextFormatter {

    private StringBuilder text = null;


    //returns array of all question sentences
    private StringBuilder[] getQuestion(){

        int count = 0;

        StringBuilder clearText = this.deleteRepeatings(this.text, '?');

        for(int i = 0; i < clearText.length(); i++){
            if(clearText.charAt(i) == '?'){
                count++;
            }
        }

        if(count > 0){

            StringBuilder[] questions = new StringBuilder[count];

            int previosPosition = 0;
            int currentPosition = 0;

            for(int i = 0; i < questions.length; i++){
                currentPosition = clearText.indexOf("?", previosPosition);
                questions[i] = new StringBuilder(
                        clearText.substring(endSentencePosition(clearText, currentPosition), currentPosition));
                previosPosition = currentPosition + 1;
            }

            return questions;
        }

        return null;

    }

    // Finds previos sign( . , ! , ? ) before startIndex
    private int endSentencePosition(StringBuilder text, int startIndex){

        if((text != null)
                && (!text.equals(""))) {
            if (startIndex < text.length()) {
                for (int i = startIndex - 1; i >= 0; i--) {
                    if ((text.charAt(i) == '.')
                            || (text.charAt(i) == '!')
                            || (text.charAt(i) == '?')) {
                        return i + 1;
                    }
                }
            }
        }

        return 0;
    }

    //delete repeating symbols
    private StringBuilder deleteRepeatings(StringBuilder text, char symbol){
        if((text != null)
                && (!text.equals(""))) {
            StringBuilder result = text;


            for (int i = 0; i < result.length() - 1; i++) {
                if ((result.charAt(i) == symbol)
                        && (result.charAt(i + 1) == symbol)) {
                    result.delete(i, i + 1);
                    //Dec because we deleted one character and lenth becomed lenth--
                    i--;
                }
            }

            return result;
        } else {
            return  null;
        }

    }



    //constructor
    public TextFormatter(StringBuilder text){
        if((text == null)
            || (text.equals(""))){
            System.out.println("Cannot process a null");
            System.exit(1);
        }

        this.text = text;
    }

    //find words in the line
    private StringBuilder[] makeWords(StringBuilder line){

        if(line != null){

            // delete spaces at the end and start
            if(line.charAt(line.length() - 1) == ' '){
                line.delete(line.length() - 1, line.length());
            }

            if(line.charAt(0) == ' '){
                line.delete(0, 1);
            }
            //




            int count = 1;

            for(int i = 0; i < line.length(); i++){
                if(line.charAt(i) == ' '){
                    count++;
                }
            }


            if(count > 0) {
                StringBuilder[] words = new StringBuilder[count];

                int previous = 0;

                for(int i = 0; i < count; i++){
                    words[i] = new StringBuilder();
                    for(int j = previous; j < line.length(); j++){
                        if(line.charAt(j) == ' '){
                            previous = j + 1;
                            break;
                        }
                        words[i].append(line.charAt(j));
                    }

                }

                return words;



            }


        }

        return null;

    }


    //print the words without repeatings
    private void printWithoutRepeatings(StringBuilder[] words, int lenth){
        if((words != null)
                &&(words.length != 0)) {
            boolean isPrinted = false;

            //filter the repeatings
            for (int i = 0; i < words.length; i++) {
                if (words[i].length() == lenth) {
                    for (int j = i - 1; j >= 0; j--) {
                        if (words[i].toString().equals(words[j].toString())) {
                            isPrinted = true;
                            break;
                        }
                    }

                    if (!isPrinted) {
                        System.out.println(words[i]);
                    }

                }


                isPrinted = false;

            }
        }

    }

    //return result
    public void getResult(int lenth){
        this.text = deleteRepeatings(this.text, ' ');
        if(this.text != null) {
            StringBuilder[] questions = this.getQuestion();

            if(questions != null) {

                StringBuilder[] words = null;

                for (int i = 0; i < questions.length; i++) {

                    if((!questions[i].toString().isEmpty())
                            && (!questions[i].toString().equals(" ".toString()))) {

                        words = makeWords(questions[i]);

                        if (words != null) {
                            printWithoutRepeatings(words, lenth);
                        } else {
                            System.out.println("Error");
                            return;
                        }
                    }
                }
            }
            return;
        }
        System.out.println("Error");
    }


}
