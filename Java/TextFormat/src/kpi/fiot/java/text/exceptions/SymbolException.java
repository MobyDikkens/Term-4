/*  SymbolException
 *
 *  Describes basic abstract symbol
 *  exceptions behaviour
 *
 *
 *   Version 1.0
 *
 *   Copyright Sheludko Dmitriy Maksimovich 2018
 */


package kpi.fiot.java.text.exceptions;

public abstract class SymbolException extends Exception {
    public SymbolException(String message){
        super(message);
    }
}
