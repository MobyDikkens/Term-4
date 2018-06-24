/*  Text
 *
 *   Descrybes a text entity
 *
 *
 *   Version 1.0
 *
 *   Copyright Sheludko Dmitriy Maksimovich 2018
 */

package kpi.fiot.java.text.entities.objective;

import kpi.fiot.java.text.entities.abstracts.IDisplayble;
import kpi.fiot.java.text.entities.abstracts.ILexema;
import kpi.fiot.java.text.entities.abstracts.ISymbolic;
import kpi.fiot.java.text.exceptions.*;

public class Text implements ISymbolic {

    Sentence[] text = null;

    private static String[] punctuations = {",",":",";"};

    private static String[] endOfSentence = {".","?","!"};



    public Text(String text){
        try {
            if((text != null)
                    && (!text.isEmpty())){
                text = removeRepeatings(text, endOfSentence);

                Lexer(text);


            }
        } catch (Exception ex){
            System.out.println(ex);
        }
    }

    private void Lexer(String text){
        try {
            if ((text != null)
                    && (!text.isEmpty())) {
                String[] splited = split(text);

                if ((splited != null)
                        && (splited.length > 0)) {
                    this.text = new Sentence[splited.length];

                    for (int i = 0; i < splited.length; i++) {
                        this.text[i] = new Sentence(splited[i]);
                    }
                }
            }
        } catch (Exception ex){
            System.out.println(ex);
        }
    }

    //region Helpers
    public static String removeRepeatings(String sentence, String[] toDelete){
        if((sentence != null)
                && (!sentence.isEmpty())
                && (toDelete != null)){

            String tmp = sentence;
            try {
                for (int i = 0; i < toDelete.length; i++)
                    tmp = tmp.trim().replaceAll(toDelete[i] + "+", toDelete[i]);


                return tmp;
            } catch (Exception ex){
                return sentence;
            }
        }

        return null;
    }

    private String[] split(String text){
        if((text != null)
                && (!text.isEmpty())){

            int previousPosition = 0;


            int count = 0;

            for(int i = 0; i < text.length(); i++){
                for(int j = 0; j < endOfSentence.length; j++){
                    if(endOfSentence[j].contains(""+text.charAt(i))){
                        count ++;
                    }
                }
            }

            String[] splited = null;

            if(count > 0){
                splited = new String[count];


                count = 0;

                for(int i = 0; i < text.length(); i++){
                    for(int j = 0; j < endOfSentence.length; j++){
                        if(endOfSentence[j].contains(""+text.charAt(i))) {
                            splited[count] = text.substring(previousPosition, i);
                            previousPosition = i + 1;
                            count++;
                        }
                    }
                }



            }

            return splited;

        } else {
            return null;
        }
    }

    //endregion


    //region ISymbolic implementation

    public String getValue() {
        return null;
    }

    public void print(IDisplayble destination){
        if(destination != null){
            if((this.text != null)
                    && (this.text.length > 0)){
                for(int i = 0; i < text.length; i++){
                    text[i].print(destination);
                }
            }
        }
    }

    //endregion


    //region Sentence Class

    public class Sentence implements ISymbolic {

        private ILexema[] lexems = null;

        private char delimiter = ' ';


        //region Constructors

        public Sentence(){
            lexems = null;
        }

        public Sentence(String sentence) throws SentenceException {
            if((sentence != null)
                    && (!sentence.isEmpty())){
                Lexer(sentence);
            } else {
                throw new SentenceException("Cannot create Sentence via input value");
            }
        }

        public Sentence(String sentence, char delimiter) throws SentenceException {
            if((sentence != null)
                    && (!sentence.isEmpty())){
                    this.delimiter = delimiter;

            } else {
                throw new SentenceException("Cannot create Sentence via input value");
            }
        }

        //endregion


        //region Helpers

        private Word[] bubleSort(Word[] array){
            try {
                if (array != null) {
                    String[] sortedArray = null;
                    for (int i = 0; i < array.length; i++) {
                        for (int j = i + 1; j < array.length; j++) {
                            if (Character.toLowerCase(array[i].getValue().charAt(0))
                                    > Character.toLowerCase(array[j].getValue().charAt(0))) {
                                String tmp = array[i].getValue();
                                array[i] = array[j];
                                array[j] = new Word(tmp);
                            }
                        }
                    }
                    return array;
                } else {
                    return null;
                }
            }catch (Exception ex){
                return null;
            }
        }

        private void Lexer(String sentence) throws SentenceException {
            if((sentence != null)
                    && (!sentence.isEmpty())){


                //  Remove repeating punctuations and end of sentences
                String normalized = removeRepeatings(sentence, punctuations);

                //  type of curr position
                int type = type(normalized.charAt(0));

                //  Process next situations
                //  Hello,_ -> Hello_,_
                //  Hello_,world -> Hello_,_world
                String str = "";
                for(int i = 0; i < normalized.length() - 3; i++){
                    str = normalized.substring(i,i+3);

                    switch (needDelimiter(str)){
                        case 1:{
                            normalized = insertCharInto(normalized,delimiter, i+1);
                            break;
                        }
                        case 2:{
                            normalized = insertCharInto(normalized,delimiter, i+2);
                            break;
                        }
                        default:{
                            break;
                        }
                    }
                }


                int lexemCounts = 1;

                String[] splitted = normalized.split(""+delimiter);

                if((splitted != null)
                        && (splitted.length > 0)){

                    this.lexems = new ILexema[splitted.length];

                    try {


                        for (int i = 0; i < splitted.length; i++) {

                            if (Character.isLetter(splitted[i].charAt(0))) {
                                lexems[i] = new Word(splitted[i]);


                            } else {
                                if (Character.isDigit(splitted[i].charAt(0))) {
                                    lexems[i] = new Number(splitted[i]);
                                } else {
                                    lexems[i] = new Sign(splitted[i].charAt(0));//it is a sign
                                }

                            }

                        }
                    } catch (Exception ex){ // if we had unexpected execption
                        System.out.println(ex);
                    }

                } else {
                    throw new SentenceException("Unexpected format");
                }

            }
        }


        //  return 1 if letter+punctuation+delimiter or 2 if delimiter+punctuation+letter
        private byte needDelimiter(String str){
            if((str != null)
                    && (!str.isEmpty())
                    && (str.length() == 3)){
                for(int i = 0; i < punctuations.length; i++) {
                    if (punctuations[i].contains(Character.toString(str.charAt(1)))) {
                        if(Character.isLetter(str.charAt(0))){
                            if(str.charAt(2) == delimiter){
                                return 1;
                            }
                        } else if (Character.isLetter(str.charAt(2))){
                            if(str.charAt(0) == delimiter){
                                return 2;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        //  Inserts value intp position
        private String insertCharInto(String source, char value, int position){


            if((source != null)
                    && (!source.isEmpty())
                    && (source.length() > position)){

                String result = "";

                for(int i = 0; i < source.length(); i++){
                    if(i == position){
                        result += value;
                    }
                    result += source.charAt(i);
                }
                return result;
            } else {
                return null;
            }
        }

        //  Detect the type of value
        private int type(char value){
            int res = 2;//punctuations

            if(Character.isDigit(value))
                res = 1;
            if(Character.isLetter(value))
                res = 0;

            return res;
        }




        //endregion




        //region ISymbolic implementation

        public String getValue(){
            return null;
        }

        public void print(IDisplayble destination){
            if(destination != null){

                Word[] words = null;

                int count = 0;

                for(int i = 0; i < lexems.length; i++){
                    if(lexems[i].isWord()){
                        count++;
                    }
                }

                if(count > 0) {

                    int index = 0;
                    words = new Word[count];

                    for (int i = 0; i < lexems.length; i++) {
                        if (lexems[i].isWord()) {
                            words[index] = (Word)lexems[i];
                            index++;
                        }
                    }



                    Word[] sorted = bubleSort(words);

                    //  Delete repeatings
                    for(int i = 0; i < sorted.length; i++){
                        for(int j = i + 1; j < sorted.length; j++){
                            if((sorted[i] != null)
                                    && (sorted[j] != null)
                                    && (sorted[i].getValue().equals(sorted[j].getValue()))){
                                sorted[j] = null;
                            }
                        }
                    }

                    if(sorted != null){
                        for(int i = 0; i < sorted.length; i++){
                            if(sorted[i] != null)
                                sorted[i].print(destination);
                        }
                    }

                }
            }
        }

        //endregion







        //region Number Class

        /*  Number
         *
         *   Descrybes a number entity
         *
         *
         *   Version 1.0
         *
         *   Copyright Sheludko Dmitriy Maksimovich 2018
         */



        public class Number implements ILexema {

            private String number = null;


            //region Constructor

            public Number(){
                this.number = null;
            }


            public Number(String number) throws NumberException {
                if((number != null)
                        && (!number.isEmpty())){
                    this.number = "";
                    for(int i = 0; i < number.length(); i++){
                        if(Character.isDigit(number.charAt(i))){
                            this.number += number.charAt(i);
                        }
                    }
                } else {
                    throw new NumberException("Cannot convert input value to the Number");
                }
            }

            //endregion


            //region ILexema implementation

            public boolean isNumber(){
                return true;
            }
            public boolean isPunctuation(String[] punctuations){
                return false;
            }
            public boolean isWord(){
                return false;
            }

            public boolean isEndOfSentence(String[] endSymbols){
                return false;
            }

            //endregion

            //region ISymbolic implementation

            public String getValue(){
                return this.number;
            }

            public void print(IDisplayble destination){
                if(destination != null){
                    destination.print(this.number);
                }
            }

            //endregion

        }


        //endregion


        //region Sign Class

        /*  Sign
         *
         *   Descrybes a sign entity
         *
         *
         *   Version 1.0
         *
         *   Copyright Sheludko Dmitriy Maksimovich 2018
         */




        public class Sign implements ILexema {

            private char sign = ' ';

            //region Constructors

            public Sign(){
                this.sign = ' ';
            }

            public Sign(char value) throws SignException {
                if(((!Character.isLetter(value)))
                        && (!Character.isDigit(value))){
                    this.sign = value;
                } else {
                    throw new SignException("Cannot create Sign via degit or letter");
                }
            }

            //endregion

            //region Helpers



            private boolean contains(String[] values){
                if((values != null)
                        && (values.length > 0)) {
                    for (int i = 0; i < values.length; i++) {
                        if ((values[i] != null)
                                && (!values[i].isEmpty())) {
                            if (values[i].equals(sign))
                                return true;
                        }
                    }
                }
                return false;

            }

            //endregion

            //region ILexema implementation

            public boolean isNumber(){
                return false;
            }

            public boolean isPunctuation(String[] punctuatios){
                if(this.contains(punctuatios)){
                    return true;
                }

                return false;
            }

            public boolean isEndOfSentence(String[] endSymbols){
                if(this.contains(endSymbols)){
                    return true;
                }

                return false;

            }
            public boolean isWord(){
                return false;
            }


            //endregion

            //region ISymbolic realization

            public String getValue(){
                return ""+this.sign;
            }

            public void print(IDisplayble destination){
                if(destination != null){
                    destination.print(""+this.sign);
                }
            }

            //endregion
        }


        //endregion


        //region Word Class

        /*  Word
         *
         *   Descrybes a word entity
         *
         *
         *   Version 1.0
         *
         *   Copyright Sheludko Dmitriy Maksimovich 2018
         */



        public class Word implements ILexema {

            private Letter[] word = null;

            //region Constructors

            public Word(){
                this.word = null;
            }

            public Word(String value) throws WordException {
                if((value != null)
                        && (!value.isEmpty())){

                    int count = 0;

                    for(int i = 0; i < value.length(); i++){
                        if(Character.isLetter(value.charAt(i)))
                            count++;
                    }

                    //  If the input was 555 the count equals 0
                    if(count > 0) {
                        word = new Letter[count];

                        int index = 0;

                        for(int i = 0; i < value.length(); i++){
                            if(Character.isLetter(value.charAt(i))){

                                try {
                                    word[index] = new Letter(value.charAt(i));
                                    index++;
                                } catch (LetterException ex){
                                    throw new WordException("Unexpected value exception");
                                }

                            }
                        }


                    } else {
                        throw new WordException("Cannot convert number to Word");
                    }


                } else {
                    throw new WordException("Cannot implicitly convert input to a Word");
                }
            }

            //endregion

            //region ILexema implementation

            public boolean isNumber(){
                return false;
            }

            public boolean isPunctuation(String[] punctuatios){
                return false;
            }

            public boolean isEndOfSentence(String[] endSymbols){
                return false;
            }
            public boolean isWord(){
                return true;
            }


            //endregion

            //region ISymbolic implementaion

            public String getValue(){

                if(word == null)
                    return null;

                String value = "";

                for(int i = 0; i < word.length; i++){
                    value += word[i].getValue();
                }

                return value;

            }

            public void print(IDisplayble destination){

                if(word == null)
                    return;


                if(destination != null){

                    String value = "";

                    for(int i = 0; i < word.length; i++){
                        value += word[i].getValue();
                    }

                    destination.print(value);
                }
            }

            //endregion

            //region Letter Class

            /*  Letter
             *
             *  Describes basic behavior
             *  for all text letter
             *
             *
             *   Version 1.0
             *
             *   Copyright Sheludko Dmitriy Maksimovich 2018
             */


            public class Letter implements ISymbolic {

                private char value;

                //region Constructos
                public Letter(){
                    this.value = ' ';
                }

                public Letter(char value) throws LetterException {
                    if(Character.isLetter(value)){
                        this.value = value;
                    } else {
                        throw new LetterException("Cannot implitly convert input to Letter type");
                    }

                }

                //endregion

                //region ISymbolic realization

                public String getValue(){
                    return ""+this.value;
                }

                public void print(IDisplayble destination){
                    if(destination != null){
                        destination.print(""+this.value);
                    }
                }

                //endregion

            }

            //endregion
        }

        //endregion


    }

    //endregion

}
