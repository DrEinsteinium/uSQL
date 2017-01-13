using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SQLJoinType {
    NONE,
    INNER,
    CROSS,
    LEFT

}
public static class SQLJoinTypeExtensions
{
    public static string GetJoinTypeString(this SQLJoinType type)
    {
        if (type == SQLJoinType.NONE) return string.Empty;
        else return Enum.GetName(typeof(SQLJoinType), type);
    }
}
