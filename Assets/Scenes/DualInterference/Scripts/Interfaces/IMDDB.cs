using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKeyTuple<KeyA, KeyB> {
    KeyA A { get; }
    KeyB B { get; }
}

public interface IMDDB<KeyA, KeyB, KeyTuple, Value> 
    where KeyTuple : IKeyTuple<KeyA, KeyB> {

    void Add(KeyTuple k, Value v);
    void Remove(KeyTuple k, Value v);

    IEnumerable<Value> this[KeyA a] { get; }
    IEnumerable<Value> this[KeyB b] { get; }
    IEnumerable<Value> this[KeyA a, KeyB b] { get; }
    IEnumerable<Value> this[KeyTuple k] { get; }
}

