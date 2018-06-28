
/// <summary>
/// 数学行列
/// </summary>
public struct Matrix
{
    /// <summary>
    /// 全ての要素が0の行列
    /// </summary>
    public static Matrix Zero(int dim1, int dim2) => new Matrix(dim1, dim2);

    /// <summary>
    /// ランダムな数値を持つ行列を作成
    /// </summary>
    public static Matrix Random(int dim1, int dim2)
    {
        var rand = new System.Random();
        Matrix mat = new Matrix(dim1, dim2);
        for (int x = 0; x < dim1; x++)
        {
            for (int y = 0; y < dim2; y++)
            {
                mat.Values[x][y] = rand.NextDouble();
            }
        }
        return mat;
    }

    /// <summary>
    /// 行列のよこサイズ
    /// </summary>
    public int Dim1 => m_Dim1;

    /// <summary>
    /// 行列のたてサイズ
    /// </summary>
    public int Dim2 => m_Dim2;

    /// <summary>
    /// 行列の数値
    /// </summary>
    public double[][] Values => m_Values;

    int m_Dim1;
    int m_Dim2;

    /// <summary>
    /// 行列の各要素
    /// </summary>
    double[][] m_Values;

    /// <summary>
    /// 行列の作成
    /// </summary>
    /// <param name="dim1">行列のよこサイズ</param>
    /// <param name="dim2">行列のたてサイズ</param>
    public Matrix(int dim1, int dim2)
    {
        m_Dim1 = dim1;
        m_Dim2 = dim2;

        m_Values = new double[dim1][];
        for (int i = 0; i < dim1; i++)
        {
            m_Values[i] = new double[dim2];
        }
    }

    public Matrix(double[][] values)
    {
        m_Dim1 = values[0].Length;
        m_Dim2 = values.Length;

        m_Values = new double[m_Dim1][];
        for (int x = 0; x < m_Dim1; x++)
        {
            m_Values[x] = new double[m_Dim2];
            for (int y = 0; y < m_Dim2; y++)
            {
                m_Values[x][y] = values[y][x];
            }
        }
    }

    /// <summary>
    /// 行列の積をもとめる
    /// </summary>
    /// <param name="mat1"></param>
    /// <param name="mat2"></param>
    public static Matrix Dot(Matrix mat1, Matrix mat2)
    {
        if (mat1.Dim1 != mat2.Dim2) // 横のサイズと縦のサイズが違う
        {
            UnityEngine.Debug.LogError($"not match size\nmat 1 : ({mat1.m_Dim1}, {mat1.Dim2})\nvmat 2: ({mat2.m_Dim1}, {mat2.Dim2}}");
            return mat1;
        }

        Matrix newMat = new Matrix(mat2.Dim1, mat1.Dim2);

        // 行列の積の計算
        double sum = 0f;
        for (int x = 0; x < newMat.Dim1; x++)
        {
            for (int y = 0; y < newMat.Dim2; y++)
            {
                // (x,y)の位置にある要素の計算
                sum = 0f;
                for (int k = 0; k < mat1.Dim1; k++)
                {
                    sum += mat1.Values[k][y] * mat2.Values[x][k];
                }
                newMat.m_Values[x][y] = sum;
            }
        }

        return newMat;
    }

    public static Vector Dot(Matrix mat1, Vector v)
    {
        Matrix mat2 = new Matrix(1, v.Size);
        for (int y = 0; y < v.Size; y++)
        {
            mat2.Values[0][y] = v.Values[y];
        }
        mat2 = Matrix.Dot(mat1, mat2);

        Vector newV = new Vector(mat2.Dim2);
        for (int y = 0; y < newV.Size; y++)
        {
            newV.Values[y] = mat2.Values[0][y];
        }
        return newV;
    }

    public static Vector Dot(Vector v, Matrix mat2)
    {
        Matrix mat1 = new Matrix(v.Size, 1);
        for (int x = 0; x < v.Size; x++)
        {
            mat1.Values[x][0] = v.Values[x];
        }
        mat1 = Matrix.Dot(mat1, mat2);

        Vector newV = new Vector(mat1.Dim1);
        for (int x = 0; x < newV.Size; x++)
        {
            newV.Values[x] = mat1.Values[x][0];
        }
        return newV;
    }

    /// <summary>
    /// *演算の定義
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    public static Matrix operator*(Matrix mat, double value)
    {
        Matrix newMat = new Matrix(mat.Dim1, mat.Dim2);
        for (int x = 0; x < mat.Dim1; x++)
        {
            for (int y = 0; y < mat.Dim2; y++)
            {
                newMat.Values[x][y] = mat.Values[x][y] * value;
            }
        }
        return newMat;
    }

    /// <summary>
    /// +演算の定義
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    public static Matrix operator +(Matrix mat, double value)
    {
        Matrix newMat = new Matrix(mat.Dim1, mat.Dim2);
        for (int x = 0; x < mat.Dim1; x++)
        {
            for (int y = 0; y < mat.Dim2; y++)
            {
                newMat.Values[x][y] = mat.Values[x][y] + value;
            }
        }
        return newMat;
    }

    /// <summary>
    /// +演算の定義
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    public static Matrix operator +(Matrix mat1, Matrix mat2)
    {
        Matrix newMat = new Matrix(mat1.Dim1, mat1.Dim2);
        for (int x = 0; x < mat1.Dim1; x++)
        {
            for (int y = 0; y < mat1.Dim2; y++)
            {
                newMat.Values[x][y] = mat1.Values[x][y] + mat2.Values[x][y];
            }
        }
        return newMat;
    }

    /// <summary>
    /// -演算の定義
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    public static Matrix operator -(Matrix mat, double value)
    {
        return mat - value;
    }

    /// <summary>
    /// -演算の定義
    /// </summary>
    /// <param name="m1"></param>
    /// <param name="m2"></param>
    public static Matrix operator -(Matrix mat1, Matrix mat2)
    {
        return mat1 + (mat2 * -1.0);
    }

    public override string ToString()
    {
        var s = new System.Text.StringBuilder();
        s.Append("[");
        for (int y = 0; y < m_Dim2; y++)
        {
            s.Append("[");
            for (int x = 0; x < m_Dim1; x++)
            {
                s.AppendFormat("{0}, ", m_Values[x][y]);
            }
            s.Append("],\n");
        }
        s.Append("]");
        return s.ToString();
    }
}
