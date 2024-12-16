# 計算式

## ダメージ

$$
\begin{equation}
\begin{split}
ダメージ量&=攻撃者の攻撃力\\
&\times \frac{攻撃者の攻撃力 \times 攻撃者の攻撃力のバフの倍率}{攻撃対象の防御力 \times 攻撃対象の防御力のバフの倍率 }\\
&\times 2^{n(攻撃の属性 \cap 攻撃対象の弱点)}\\
&\times (1 - 腕損傷数 \times 0.5)\\
&\times 攻撃対象の強化\\
&\times スキル倍率\\
&\times 定数\\
&+乱数
\end{split}
\end{equation}
$$