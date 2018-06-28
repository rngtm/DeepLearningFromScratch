/// <summary>
/// And演算を定義したデータセット
/// </summary>
public class DataSet_And : DataSetBase
{
    // 訓練用のデータ
    public override Data[] GetTrainDatas() => new Data[]
    {
        new Data {
            x = new Vector(0, 0), // 入力 : (false, false)
            t = new Vector(0, 1)  // 正解 : falseに分類 (false && false は falseになる)
        },
        new Data {
            x = new Vector(1, 0), // 入力 : (true, false)
            t = new Vector(0, 1)  // 正解 : falseに分類 (true && false は falseになる)
        },
        new Data {
            x = new Vector(0, 1), // 入力 : (false, true)
            t = new Vector(0, 1)  // 正解 : falseに分類 (false && true は falseになる)
        },
        new Data {
            x = new Vector(1, 1), // 入力 : (true, true)
            t = new Vector(1, 0)  // 正解 : trueに分類 (true && true は trueになる)
        },
    };

    // テスト用のデータ
    public override Vector[] GetTestValues() => new Vector[]
    {
        new Vector (0, 0),
        new Vector (1, 0),
        new Vector (0, 1),
        new Vector (1, 1),
    };
}
