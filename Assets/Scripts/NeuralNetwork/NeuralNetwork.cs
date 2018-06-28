using Debug = UnityEngine.Debug;

/// <summary>
/// ２層のニューラルネットワーク
/// </summary>
public class NeuralNetwork
{
    public Matrix W1 { get; set; } // 重み(入力層->隠れ層) (学習によってこの値に更新をかけ、損失値を減らしていく)
    public Matrix W2 { get; set; } // 重み(隠れ層→出力層) (学習によってこの値に更新をかけ、損失値を減らしていく)
    public Vector B1 { get; set; } // バイアス(隠れ層) (学習によってこの値に更新をかけ、損失値を減らしていく)
    public Vector B2 { get; set; } // バイアス(出力層) (学習によってこの値に更新をかけ、損失値を減らしていく)

    /// <summary>
    /// ニューラルネットの初期化
    /// </summary>
    /// <param name="inputSize">入力層のサイズ</param>
    /// <param name="hiddenSize">隠れ層のサイズ</param>
    /// <param name="outputSize">出力層のサイズ</param>
    public NeuralNetwork(int inputSize, int hiddenSize, int outputSize)
    {
        double initialWeight = 0.01;

        W1 = Matrix.Random(inputSize, hiddenSize) * initialWeight; //  重み(入力層->隠れ層)の行列の作成
        B1 = Vector.Zero(hiddenSize); // バイアス(隠れ層)

        W2 = Matrix.Random(hiddenSize, outputSize) * initialWeight; //  重み(隠れ層->出力層)の行列の作成
        B2 = Vector.Zero(outputSize); // バイアス(出力層)
    }

    /// <summary>
    /// ニューラルネットの出力値の計算
    /// </summary>
    /// <param name="x">ニューラルネットへの入力データ</param>
    /// <returns>ニューラルネットからの出力</returns>
    public Vector Predict(Vector x)
    {
        //Debug.Log("x: " + x);
        //Debug.Log("W1: " + m_W1);
        //Debug.Log("B1: " + m_B1);   
        Vector a1 = Matrix.Dot(W1, x) + B1; // 入力に重みをかける

        //Debug.Log("a1:" + a1);
        Vector z1 = MyMath.Sigmoid(a1); // 活性化関数としてシグモイド関数を適用

        //Debug.Log("z1:" + z1);
        //Debug.Log("B2: " + m_B2);
        Vector a2 = Matrix.Dot(W2, z1) + B2; // 重みをかける

        //Debug.Log("a2:" + a2);
        Vector y = MyMath.Softmax(a2); // 活性化関数としてソフトマックス関数を適用 (結果は確率になる)

        //Debug.Log("y:" + y);
        return y;
    }

    /// <summary>
    /// 損失の計算
    /// </summary>
    /// <param name="x">入力データ</param>
    /// <param name="t">教師データ</param>
    /// <returns>損失関数の計算結果</returns>
    public double Loss(Vector x, Vector t)
    {
        Vector y = Predict(x); // ニューラルネットの出力を計算
        return MyMath.CrossEntropyError(y, t); // 損失の計算
    }


    /// <summary>
    /// 勾配を計算する
    /// </summary>
    /// <param name="xs">入力データの集まり</param>
    /// <param name="ts">教師データの集まり</param>
    /// <returns>勾配</returns>
    public NNGradient Gradient(Vector x, Vector t)
    {
        System.Func<Matrix, double> FM = X => Loss(x, t); // 損失関数FM
        System.Func<Vector, double> FV = X => Loss(x, t); // 損失関数FV

        // 勾配の計算
        var grad = new NNGradient();
        grad.W1 = MyMath.CalcGradient(FM, W1); // X=W1でFM(X)を微分して勾配を算出する
        grad.W2 = MyMath.CalcGradient(FM, W2); // X=W2でFM(X)を微分して勾配を算出する
        grad.B1 = MyMath.CalcGradient(FV, B1); // X=B1でFV(X)を微分して勾配を算出する
        grad.B2 = MyMath.CalcGradient(FV, B2); // X=B2でFV(X)を微分して勾配を算出する

        return grad; 
    }
}
