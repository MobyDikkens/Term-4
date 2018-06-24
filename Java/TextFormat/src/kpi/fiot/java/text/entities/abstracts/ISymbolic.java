/*  ISymbolic
 *
 *  Describes basic behavior
 *  for all text entities
 *
 *
 *   Version 1.0
 *
 *   Copyright Sheludko Dmitriy Maksimovich 2018
 */


package kpi.fiot.java.text.entities.abstracts;

public interface ISymbolic {
    void print(IDisplayble destionation);
    String getValue();
}
