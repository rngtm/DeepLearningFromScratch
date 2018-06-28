using UnityEngine;

public class MainProgram : MonoBehaviour
{
    /// <summary>
    /// 学習の繰り返し回数
    /// </summary>
    const int iter = 10000;

    /// <summary>
    /// 学習率η
    /// </summary>
    const double eta = 0.1;

    void Start()
    {
        // ニューラルネットの作成とデータセットの用意
        var net = new NeuralNetwork(2, 3, 2); var dataSet = new DataSet_And();
        // var net = new NeuralNetwork(3, 3, 3); var dataSet = new DataSet_RGB();

        // 訓練データ
        var trainDatas = dataSet.GetTrainDatas();

        // ニューラルネットの出力の確認
        ShowOutput(net, trainDatas);

        Debug.Log($"# 学習を開始します\niter = {iter}, eta = {eta}");

        // 勾配法でニューラルネットを学習させる
        NNGradient grad;
        for (int i = 0; i < iter; i++)
        {
            for (int di = 0; di < trainDatas.Length; di++)
            {
                var data = trainDatas[di];
                grad = net.Gradient(data.x, data.t);
                net.W1 -= grad.W1 * eta;
                net.W2 -= grad.W2 * eta;
                net.B1 -= grad.B1 * eta;
                net.B2 -= grad.B2 * eta;
            }
        }

        Debug.Log("# 学習が完了しました。");

        // ニューラルネットの出力の確認
        ShowOutput(net, dataSet.GetTestValues()); 
    }
    

    /// <summary>
    /// ニューラルネットの出力の確認
    /// </summary>
    void ShowOutput(NeuralNetwork net, Vector[] xs)
    {
        var sb = new System.Text.StringBuilder();

        foreach (var x in xs)
        {
            sb.Length = 0;

            sb.Append($"入力:{x}\n"); // 入力データ
            sb.Append($"出力:{net.Predict(x)}\n");

            Debug.Log(sb.ToString());
        }
    }

    /// <summary>
    /// ニューラルネットの出力の確認
    /// </summary>
    void ShowOutput(NeuralNetwork net, Data[] datas)
    {
        var sb = new System.Text.StringBuilder();

        //Debug.Log($"W1:{net.W1}\nW2:{net.W2}\nB1:{net.B1}\nB2:{net.B2}"); // ニューラルネットの各パラメータ

        foreach (var data in datas)
        {
            sb.Length = 0;

            var x = data.x;
            var t = data.t;

            sb.Append($"入力:{x} "); // 入力データ
            sb.Append($"出力:{net.Predict(x)} ");
            sb.Append($"正解:{t}"); // 教師データ

            //sb.Append($"loss = {net.Loss(x, t)}, "); // 損失
            // NNGradient grad = net.Gradient(x, t); // 勾配
            //sb.Append($"grad = {grad}");

            Debug.Log(sb.ToString());
        }
    }
}
