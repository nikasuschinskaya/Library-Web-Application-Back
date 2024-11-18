﻿namespace Library.Domain.Extentions;


[AttributeUsage(AttributeTargets.Field)]
public sealed class StringValueAttribute : Attribute
{
    public string Value { get; }

    public StringValueAttribute(string value) => Value = value;
}
