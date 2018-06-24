/*  Text
 *
 *   Describes an objective world
 *   letter unit
 *
 *  Provide functions to process
 *  letter
 *
 *   Version 1.0
 *
 *   Copyright Sheludko Dmitriy Maksimovich 2018
 */

package kpi.fiot.java.text.entities;

import kpi.fiot.java.text.entities.abstractions.IDisplayble;
import kpi.fiot.java.text.entities.abstractions.ISymbolic;
import kpi.fiot.java.text.exceptions.LexemaException;
import kpi.fiot.java.text.exceptions.SymbolException;


public class Text {



    //region Sentence Class

    private class Sentence implements ISymbolic {

        //  The input text without processing
        private String plainSentence = null;

        //  The input text after processing
        private String normalizedSentence = null;

        //  The sentence - collection of words
        private Lexema[] sentence = null;

        public Sentence(String sentence) throws SymbolException {

            if ((sentence == null)
                    || (sentence.isEmpty())) {
                throw new SymbolException("Cannot implicitly convert null to Sentence object");
            }

            this.normalizedSentence = Lexer(sentence);

        }

        //region ISymbolic realization

        @Override
        public String getValue(){
            return this.normalizedSentence;
        }

        @Override
        public void print(IDisplayble destination){

        }

        //endregion

        //region Lexer and Parser

        //  Make sentence preprocessing
        private String Lexer(String sentence) {

            if ((sentence == null)
                    || (sentence.isEmpty()))
                return "";

            String normalizedSentence = "";

            //  Delete repeating symbols
            normalizedSentence = deleteRepeatings(sentence, ' ');
            normalizedSentence = deleteRepeatings(normalizedSentence, ',');
            normalizedSentence = deleteRepeatings(normalizedSentence, '.');
            normalizedSentence = deleteRepeatings(normalizedSentence, '!');
            normalizedSentence = deleteRepeatings(normalizedSentence, '?');
            normalizedSentence = deleteRepeatings(normalizedSentence, '-');
            normalizedSentence = deleteRepeatings(normalizedSentence, ':');
            normalizedSentence = deleteRepeatings(normalizedSentence, ';');
            normalizedSentence = deleteRepeatings(normalizedSentence, '\t');


            return normalizedSentence;
        }

        //  Split sentence into a words and signs
        private Lexema[] Parser(String sentence) {

            Lexema[] parsed = null;

            if ((sentence == null)
                    || (sentence.isEmpty()))
                return null;


            int count = 1;

            for (int i = 0; i < sentence.length(); i++) {
                if (sentence.charAt(i) == ' ')
                    count++;
            }

            parsed = new Lexema[count];


            return parsed;
        }

        //endregion

        //region Helper Methods

        //  Provide removing the symbol sequences from sentence
        private String deleteRepeatings(String sentence, char symbol) {
            if ((sentence == null)
                    || (sentence.isEmpty())) {
                return null;
            } else {

                String normilizedSentence = sentence.replaceAll("(" + symbol + ")\\1{1,}", "$1");


                return normilizedSentence;

            }

        }


        //  Find if the value could be a sign
        private boolean IsSign(char value){

            if((value == '.')
                    || (value == ',')
                    || (value == '!')
                    || (value == '?')
                    || (value == ':')
                    || (value == ';')
                    || (value == '-')
                    || (value == ' ')){
                return true;
            }

            String tmp = ""+value;
            if(tmp.matches("[0-9]"))
                return true;


            return false;
        }

        //endregion



        //region Lexema Class


        /*  Lexema
         *
         *   Entity to describe text
         *   minimal independent unit
         *
         *
         *
         *   Version 1.0
         *
         *   Copyright Sheludko Dmitriy Maksimovich 2018
         */



        public abstract class Lexema implements ISymbolic {

            private String lexema = null;

            public Lexema(){
                lexema = "";
            }

            public Lexema(String value) throws LexemaException {
                if((value != null)
                        && (!value.isEmpty())){
                    this.lexema = value;
                } else {
                    throw new LexemaException("Cannot create lexema from an null - referenced value");
                }


            }

            @Override
            public String getValue() {
                return lexema;
            }

            @Override
            public void print(IDisplayble destination){

                if((lexema == null)
                        || (lexema.isEmpty()))
                    return;

                for(int i = 0; i < lexema.length(); i++){
                    destination.print(lexema.charAt(i));
                }
            }



            //region Word Class

            /*  Word
             *
             *   Describes an objective world
             *   word unit
             *
             *  Provide functions to process
             *  word
             *
             *   Version 1.0
             *
             *   Copyright Sheludko Dmitriy Maksimovich 2018
             */


            private class Word extends Lexema {

                //  Word
                private Letter[] word = null;

                //  Constructor
                public Word(String word) throws SymbolException {

                    if(isWord(word)){

                        this.word = new Letter[word.length()];

                        for(int i = 0; i < word.length(); ++i){
                            this.word[i] = new Letter(word.charAt(i));
                        }

                    } else {
                        throw new SymbolException("Cannot implicitly convert value to Word type");
                    }

                }

                //  Displays each one of letter in the word
                public void print(IDisplayble destination){

                    if(destination == null)
                        return;


                    for(int i = 0; i < this.word.length; i++){
                        word[i].print(destination);
                    }
                }

                //  Checks if string input could be word
                private boolean isWord(String word){

                    if((word == null)
                            || (word.isEmpty()))
                        return false;

                    for (int i = 0; i != word.length(); ++i) {
                        if (!Character.isLetter(word.charAt(i))) {
                            return false;
                        }
                    }

                    return true;
                }


                //region Letter Class

                /*  Letter
                 *
                 *   Describes an objective world
                 *   letter unit
                 *
                 *  Provide functions to process
                 *  letter
                 *
                 *   Version 1.0
                 *
                 *   Copyright Sheludko Dmitriy Maksimovich 2018
                 */

                private class Letter extends Symbol {

                    public Letter(char value) throws SymbolException {

                        if(Character.isLetter(value)){
                            this.value = value;
                        } else {
                            throw new SymbolException("Cannot implicitly convert value to Letter type");
                        }

                    }


                }
                //endregion

            }

            //endregion

            //region Sign Class


            /*  Sign
             *
             *   Describes an objective world
             *   sign unit
             *
             *  Provide functions to process
             *  sign
             *
             *   Version 1.0
             *
             *   Copyright Sheludko Dmitriy Maksimovich 2018
             */


            private class Sign extends Lexema {

                public Sign(char value) throws LexemaException {
                    super("" + value);
                }


                private boolean isSign(char value){
                    if((value == '.')
                            || (value == ',')
                            || (value == '!')
                            || (value == '?')
                            || (value == ':')
                            || (value == ';')
                            || (value == '-')
                            || (value == ' ')){
                        return true;
                    }

                    String tmp = ""+value;
                    if(tmp.matches("[0-9]"))
                        return true;


                    return false;

                }

            }

            //endregion

        }


        //endregion


        //region Symbol Class

        /*  Symbol
         *
         *   Entity to describe
         *   symbol behaviour
         *
         *
         *
         *   Version 1.0
         *
         *   Copyright Sheludko Dmitriy Maksimovich 2018
         */


        public class Symbol implements ISymbolic {

            protected char value;

            //  Display the value
            @Override
            public void print(IDisplayble destination){
                destination.print(value);
            }


            //  Get the value
            @Override
            public String getValue(){
                return ""+this.value;
            }

            public Symbol(){
                this.value = ' ';
            }

            public Symbol(char value){
                this.value = value;
            }

        }



        //endregion




    }

    //endregion









}
