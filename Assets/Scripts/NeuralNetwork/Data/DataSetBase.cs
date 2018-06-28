public abstract class DataSetBase
{
    // 訓練用のデータ取得
    public virtual Data[] GetTrainDatas() => new Data[0];
    

    // テスト用のデータ取得
    public virtual Vector[] GetTestValues() => new Vector[0];
}
