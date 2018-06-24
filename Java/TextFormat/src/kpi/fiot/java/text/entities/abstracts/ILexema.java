/*  ILexema
 *
 *  Describes basic behavior
 *  for an abstract text entity
 *
 *
 *   Version 1.0
 *
 *   Copyright Sheludko Dmitriy Maksimovich 2018
 */

package kpi.fiot.java.text.entities.abstracts;

public interface ILexema extends ISymbolic {

    boolean isNumber();
    boolean isPunctuation(String[] punctuations);
    boolean isWord();
    boolean isEndOfSentence(String[] endSymbols);

}
