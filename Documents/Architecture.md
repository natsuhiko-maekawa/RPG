# アーキテクチャ

## MV(R)P

　MVPに加え、R3のイベントを使用している。  
　特別な用語は使用していないので、詳細は読み飛ばしても構わない。  

### Models

　DDDを参考に設計を行った。  
　`Models`は、  
1. `Entity`クラス、`ValueObject`クラスもしくは構造体からなる`Domain`層
2. `Service`クラスとそのFacadeである`UseCase`クラスがある`UseCases`層
3. `Repository`クラス、`Factory`クラスからなる`DataAccess`層

　の3層で構成される。  
　`Domain`層と`UseCases`層はUnityEngineに依存していない。  
　`Service`クラスは状態を持たないが、`Repository`への参照を保持しているため、
`Repository`の状態によって返す値が変化することがある。よって、完全な参照透過性はない。
また、`Repository`の状態を更新することもある。  
　`DataAccess`層にはScriptableObjectを利用したクラスがある。  

### Views

　`Views`は、
- コントローラの入力を受け付ける`InputAction`クラス
- 画面の出力を担う`View`クラスと`ViewModel`クラスもしくは構造体

　からなる。  
　コントローラの入力を`Presenter`を介さずに即時画面に出力するために、
`InputAction`クラスと`View`クラスの両方の性質を持つクラスも存在する。  
　アニメーションはUnityの`Animation`機能で実装し、コルーチンやasync/awaitを使用しない。  
　`View`は自作コンポーネント (`GameObject`) のコンポジションによって実装する。  

### Presenters

　`Presenters`は`Views`からの入力を`Models`に反映し、`Models`の変更を`Views`に出力する役割を持つ。  
　まず、`Views`の`InputAction`クラスがインタフェースを介して`StateMachine`クラスを操作する。
なお、`StateMachine`クラスは`State`パターンと`Memento`パターンで構成される。  
　次に、`StateMachine`クラスが`Context`クラスを通して`State`クラスを操作する。  
　`State`クラスは`Models`の`UseCase`クラスを介して`Repository`を操作し、
`Repository`の変更に応じて`Presenter`クラスのFacadeである`PresenterFacade`クラスを操作する。  
　最後に、`PresenterFacade`クラスを介して操作された`Presenter`クラスが`View`に`ViewModel`を返す。  
　ReactivePropertyを使用している場合もほぼ同様である。  

## 有限オートマトン

　`StateMachine`はの状態遷移は一方通行となっている。  
(`State`の循環参照がおこらないようになっている)  
