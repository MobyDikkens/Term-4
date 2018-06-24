/*  ISerializeble
 *
 *   Interface intended to provide
 *   servicies to serialize data
 *
 *   Version 1.0
 *
 *   Copyright Dima Sheludko IP - 63 2018
 */


public interface ISerializeble {
    String serialize(Object object);
    Object desirialize(String value);
}
