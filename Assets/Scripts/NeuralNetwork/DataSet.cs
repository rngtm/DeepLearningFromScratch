/// <summary>
/// 学習に使うデータセット
/// </summary>
public class DataSet
{
    // And演算をここで定義
    public readonly Data[] Values = new Data[]
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
            x = new Vector(0, 0), // 入力 : (true, true)
            t = new Vector(0, 1)  // 正解 : trueに分類 (true && true は trueになる)
        },
    };
}
