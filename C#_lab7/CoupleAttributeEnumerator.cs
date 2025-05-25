using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_7;

public class CoupleAttributeEnumerator : IEnumerator<CoupleAttribute>
{
    private readonly CoupleAttribute[] _attributes;
    private int _position = -1;

    public CoupleAttributeEnumerator(Type type)
    {
        var attributes = type.GetCustomAttributes(typeof(CoupleAttribute), false);
        _attributes = new CoupleAttribute[attributes.Length];
        for (int i = 0; i < attributes.Length; i++)
        {
            _attributes[i] = (CoupleAttribute)attributes[i];
        }
    }

    public CoupleAttribute Current
    {
        get
        {
            if (_position < 0 || _position >= _attributes.Length)
                throw new InvalidOperationException();
            return _attributes[_position];
        }
    }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        _position++;
        return _position < _attributes.Length;
    }

    public void Reset()
    {
        _position = -1;
    }

    public void Dispose()
    {
    }
}