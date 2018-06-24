/*  ISymbolic
*
*   Describes abstract symbol
*   behaviour
*
*
*   Version 1.0
*
*   Copyright Sheludko Dmitriy Maksimovich 2018
*/

package kpi.fiot.java.text.entities.abstractions;

public interface ISymbolic {

    void print(IDisplayble destination);
    String getValue();

}
