# 質問一覧

## 技術的な細かい質問

### 否定演算子`!`と三項演算子の使用

　Unity/C#を使用している現場で否定演算子や三項演算子を使用するとレビューが通らなかったりするのでしょうか。  
　主にJavaの界隈で、真偽値を反転する場合は否定演算子`!`を使用せず`== False`と記述せよ
と言われているのですが、私自身は素直に否定演算子を使用すべきだと思っています。
ただ、いくら検索しても否定演算子を否定する記事しかヒットしないため、自信がなくなってきました。  
　三項演算子に関しても、Javaの界隈では可読性を下げるから使用するなと言われているのですが、
シンプルな三項演算子ならば可読性を下げるとは思えません。  
　実際のところどうなんでしょうか。


### アロケーション回避のための空リスト生成

　Unityのマニュアル「ガベージコレクションのベストプラクティス」に以下の記述がある。  

> 基本的に、長さ 0 の配列をメソッドから返す場合は、空配列を繰り返し生成するよりも、
> 事前に割り当てられた長さ 0 の静的インスタンスを返す方が効率的です。

[空配列の再利用](https://docs.unity3d.com/ja/2022.1/Manual/performance-garbage-collection-best-practices.html#emptyarray)  

　戻り値がIReadOnlyList型の場合、
ImmutableList.Emptyで生成した空のリストを返せば目的が達成できるか。  
　あるいは独自に定義した以下のクラスで目的が達成できるか。

```csharp
public static class MyList<T>
{
    public static IReadOnlyList<T> Empty => new List<T>();
}
```

　ImmutableList.Emptyと独自に定義したMyList.Emptyのどちらでも目的が達成できる場合、
どちらのほうが好ましいか。
また、よりよい方法はある場合、知りたい。

### null非許容時の参照型のデフォルト値

　前提として、null許容参照型を使用している。  
　コンストラクタ等の参照型引数にデフォルト値を与えたい。  
　この場合、引数はnull許容参照型とするのが正しいか。  

```csharp
// フィールドではnullを許容したくない
private readonly IReadOnlyList<foobar> _fooberList = new();

// 引数を省略できるようにデフォルト値を設定する
// 参照型のデフォルト値はnullなので、null許容型とするしかないか。
public Foo(Bar bar, IReadOnlyList<foobar>? foobarList = null)
{
    _foobarList = foobarList ?? new List<foobarList>();
}
```
