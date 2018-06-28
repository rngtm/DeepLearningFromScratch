/// <summary>
/// RGBの判定を定義したデータセット
/// </summary>
public class DataSet_RGB : DataSetBase
{
    // 訓練用のデータ
    public override Data[] GetTrainDatas() => new Data[]
    {
        new Data {
            x = new Vector(1, 0, 0), // 入力 : (R,G,B) = (1, 0, 0)
            t = new Vector(1, 0, 0)  // 正解 : 赤へ分類
        },
        new Data {
            x = new Vector(0, 1, 0), // 入力 : (R,G,B) = (0, 1, 0)
            t = new Vector(0, 1, 0)  // 正解 : 緑へ分類
        },
        new Data {
            x = new Vector(0, 0, 1), // 入力 : (R,G,B) = (0, 0, 1)
            t = new Vector(0, 0, 1)  // 正解 : 青へ分類
        },
    };

    // テスト用のデータ
    public override Vector[] GetTestValues() => new Vector []
    {
        new Vector(1f, 0.5f, 0.0f),
        new Vector(0f, 1f, 0.0f),
        new Vector(0f, 0f, 1f),
        new Vector(1f, 0.5f, 0.0f),
        new Vector(1f, 0.0f, 0.5f),
        new Vector(0f, 1f, 0.5f),
        new Vector(0.5f, 1f, 0f),
        new Vector(0.5f, 0f, 1f),
        new Vector(0f, 0.5, 1f),
    };
}
