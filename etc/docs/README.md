# Warikan

## Goal

The aim is to make this.  
https://gist.github.com/j5ik2o/842f12a2cfc008ef564fe8d1e0c0259a


## Summary

１. DDD with F#
- 関数的ドメインモデリング
- ワークフローを意識する
  - 各ステップの状態を型で表現する
  - 関数で前の状態から次の状態へ変換していく
- なるべくパイプでつなぐ

２. [Components](./components.md)
- 基本は F# でつくる
- 副作用を扱うところは C# で実装する

３. [Domain Layer](./domain.md)
- 主要な構造体
- 計算ロジック

４. Service Layer
- ユースケースを表現する
- 可変状態を制御する
  - 設定値を変える，人数を増やすなど

５. Datasource
- 永続化など，アプリケーションの外部とのやりとり
- C# -> F# の呼び出しが煩わしいのでアダプターを挟む

６. Controller / 
- [Web API](./api.md) または Console App
- 各ライブラリを使って，振る舞いは共通化しつつ，  
  アプリケーションの種類は柔軟に変更可能にする


## Featured types

１. PositiveAmount
  - Simple Type
    - PositiveAmount of decimal
  - `Zero`, `(+)` を実装して直接 `Seq.sum` を扱えるようにする

２. ExtraOrShortage
  - Union Case
    - | Extra    of PositiveAmount
    - | Shortage of NegativeAmount
  - TotalBilledAmount と TotalPaymentAmount から生成
    - 自動的にどちらかのケースになる

３. ReportedPaymentClass
  - Union Case
    - | ReportedOrganizerPaymentClass of OrganizerPaymentClass
    - | ReportedGuestPaymentClass     of GuestPaymentClass
  - OrganizerPaymentClass と GuestPaymentClass をまとめて I/F を共通化
  - 継承やインターフェイスと違って対象タイプには影響を与えない


## References
- [Domain Modeling Made Functional](https://www.amazon.co.jp/dp/B07B44BPFB)
