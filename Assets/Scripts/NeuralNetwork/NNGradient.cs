/// <summary>
/// ニューラルネットが持つ各パラメータの勾配
/// </summary>
public struct NNGradient
{
    public Matrix W1; // 重み
    public Matrix W2; // 重み
    public Vector B1; // バイアス
    public Vector B2; // バイアス

    public override string ToString()
    {
        return $"(W1:{W1}\nW2:{W2}\nB1:{B1}\nB2:{B2})";
    }
}
