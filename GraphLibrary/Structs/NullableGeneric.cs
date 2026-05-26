using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Structs;

public record struct NullableGeneric<T>(bool IsNull, T? Value) : IEquatable<NullableGeneric<T>> where T : IEquatable<T>
{
    public NullableGeneric(T value) : this(false, value)
    {
    }

    public static implicit operator NullableGeneric<T>(T value) => new NullableGeneric<T>(value);

    public static implicit operator T(NullableGeneric<T> nullable) {
        if(nullable.IsNull)
        {
            throw new NullReferenceException("Nullable believes value is null.");
        }
        return nullable.Value!;
    }
}