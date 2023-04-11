using System.Collections.Generic;
using Godot.Collections;
public static class ArrayExtensions{
    public static IEnumerable<T> ToEnumerable<T>(this Godot.Collections.Array array){
        for(int i = 0; i < array.Count; i++){
            yield return (T)array[i];
        }
    }
    public static T[] ToArray<T>(this Godot.Collections.Array array){
        var result = new T[array.Count];
        for(int i = 0; i < array.Count; i++){
            result[i] = (T)array[i];
        }
        return result;
    }
}