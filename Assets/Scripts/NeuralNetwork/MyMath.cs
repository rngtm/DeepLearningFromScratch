using Debug = UnityEngine.Debug;

/// <summary>
/// 数学関数の定義
/// </summary>
public static class MyMath
{
    /// <summary>
    /// シグモイド関数
    /// </summary>
    public static double Sigmoid(double x)
    {
        return 1.0 / (1.0 + System.Math.Exp(-x));
    }

    /// <summary>
    /// シグモイド関数
    /// </summary>
    public static Vector Sigmoid(Vector z)
    {
        Vector newV = new Vector(z.Size);
        for (int i = 0; i < z.Size; i++)
        {
            newV.Values[i] = Sigmoid(z.Values[i]);
        }
        return newV;
    }

    /// <summary>
    /// ソフトマックス関数
    /// </summary>
    public static Vector Softmax(Vector a)
    {
        // aの各要素vをexp(v)に置き換えたもの
        Vector exp_a = new Vector(a.Size);
        for (int i = 0; i < a.Size; i++)
        {
            exp_a.Values[i] = System.Math.Exp(a.Values[i]);
        }

        double sum_exp_a = 0f;
        for (int i = 0; i < a.Size; i++)
        {
            sum_exp_a += exp_a.Values[i];
        }

        // 合計値で割る
        for (int i = 0; i < a.Size; i++)
        {
            exp_a.Values[i] /= sum_exp_a;
        }

        return exp_a;
    }

    /// <summary>
    /// 交差エントロピー誤差
    /// </summary>
    /// <param name="y">ニューラルネットの出力データ</param>
    /// <param name="t">教師データ</param>
    public static double CrossEntropyError(Vector y, Vector t)
    {
        double sum = 0f;
        for (int i = 0; i < y.Size; i++)
        {
            sum -= t.Values[i] * System.Math.Log(y.Values[i]);
        }
        return sum;
    }

    /// <summary>
    /// x=x0に関するf(x)の勾配を計算する
    /// </summary>
    /// <param name="f">数学関数f</param>
    /// <param name="x0">微分点</param>
    /// <returns>x=x0に関するf(x)の勾配</returns>
    public static Vector CalcGradient(System.Func<Vector, double> f, Vector x0)
    {
        Vector grad = new Vector(x0.Size); // 勾配

        double h = 1e-4;
        for (int i = 0; i < x0.Size; i++)
        {
            double tmp = x0.Values[i];

            // f(x + h)
            x0.Values[i] = tmp + h;
            double fL = f(x0);

            // f(x - h)
            x0.Values[i] = tmp - h;
            double fR = f(x0);

            x0.Values[i] = tmp; // 元に戻す

            grad.Values[i] = (fL - fR) / (2.0 * h); // 偏微分
        }

        return grad;
    }

    /// <summary>
    /// x=x0に関するf(x)の勾配を計算する
    /// </summary>
    /// <param name="f">数学関数f</param>
    /// <param name="x0">微分点</param>
    /// <returns>x=x0に関するf(x)の勾配</returns>
    public static Matrix CalcGradient(System.Func<Matrix, double> f, Matrix x0)
    {
        Matrix grad = new Matrix(x0.Dim1, x0.Dim2); // 勾配用のデータ作成

        double h = 1e-4;
        for (int x = 0; x < x0.Dim1; x++)
        {
            for (int y = 0; y < x0.Dim2; y++)
            {
                double tmp = x0.Values[x][y];

                // f(x + h)
                x0.Values[x][y] = tmp + h;
                double fL = f(x0);
               
                // f(x - h)
                x0.Values[x][y] = tmp - h;
                double fR = f(x0); 

                x0.Values[x][y] = tmp; // 元に戻す

                grad.Values[x][y] = (fL - fR) / (2.0 * h); // 偏微分
                //Debug.Log($"fL:{fL}\nfR:{fR}\nf:{f(x0)}\ngrad:{grad.Values[x][y]}\nh:{h}\n\nx0{x0}");
            }
        }
        return grad;
    }
}
