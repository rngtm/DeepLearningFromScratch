using Debug = UnityEngine.Debug;

/// <summary>
/// 数学ベクトル
/// </summary>
public struct Vector
{
    public static Vector Zero(int size) => new Vector(size);

    public int Size => m_Size;
    public double[] Values => m_Values;

    int m_Size;
    double[] m_Values;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Vector(int size)
    {
        m_Size = size;
        m_Values = new double[size];
    }

    public Vector(params double[] values)
    {
        m_Size = values.Length;
        m_Values = (double[])values.Clone();
    }

    /// <summary>
    /// +演算の定義
    /// </summary>
    public static Vector operator *(Vector v1, double v2)
    {
        Vector newV = new Vector(v1.Size);
        for (int i = 0; i < v1.Size; i++)
        {
            newV.Values[i] = v1.Values[i] * v2;
        }
        return newV;
    }

    /// <summary>
    /// +演算の定義
    /// </summary>
    public static Vector operator+ (Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size)
        {
            Debug.LogError($"not match size v1 : {v1.Size}, v2 : {v2.Size}");
            return v1;
        }
        Vector newV = new Vector(v1.Size);
        for (int i = 0; i < v1.Size; i++)
        {
            newV.Values[i] = v1.Values[i] + v2.Values[i];
        }
        return newV;
    }

    /// <summary>
    /// -演算の定義
    /// </summary>
    public static Vector operator -(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size)
        {
            Debug.LogError($"not match size v1 : {v1.Size}, v2 : {v2.Size}");
            return v1;
        }

        Vector newV = new Vector(v1.Size);
        for (int i = 0; i < v1.Size; i++)
        {
            newV.Values[i] = v1.Values[i] - v2.Values[i];
        }
        return newV;
    }

    public override string ToString()
    {
        var s = new System.Text.StringBuilder();
        s.Append("[");
        for (int i = 0; i < m_Size; i++)
        {
            s.AppendFormat("{0:0.000}, ", m_Values[i]);
        }
        s.Append("]");
        return s.ToString();
    }
}
