# VRFlickInput
![DemoMovie](https://raw.githubusercontent.com/donafudo/VRFlickInput/images/Demo.gif)  
Vive対応のVR向け日本語入力システムです。

スマートフォンのフリック入力と同じ感覚で日本語入力ができます。

unitypackageは[こちら](https://github.com/donafudo/VRFlickInput/releases/tag/v0.1.0)からダウンロードできます。

## 開発環境
Unity 2017.4.1f1  
HTC Vive Controller

## セットアップ
### 1.SteamVR PluginのImport
コードの一部やコントローラーのトラッキングをSteamVR Pluginに依存しているため、AssetStoreからSteamVR PluginをImportします。  
先に`.unitypackage`のImportからするとPrefabの参照関係が壊れます。
### 2.パッケージのダウンロード
最新の[.unitypackage](https://github.com/donafudo/VRFlickInput/releases/tag/v0.1.0)をダウンロードし、プロジェクトにインポートします。

以上を行うとScenesフォルダ内のデモシーンは動作すると思います。

## 新しいSceneで使う
自分で新しいSceneを作って、そこでVRFlickInputを使う場合の手順は　　
### 1.PrefabをSceneに追加
`SteamVR/Prefabs`内の[CameraRig]と`VRFlickInput/Prefabs`内のVRFlickKeyBoardをSceneに追加してください。

### 2.参照の解決
1.[CameraRig]の子になっている左右のControllerの両方に`VRFI_Operater`スクリプトをアタッチします。  
2.ControllerSideをコントローラに合わせて設定します。  
3.`VRFI_Operater`スクリプトのVRFlickKeyBoardに、SceneにあるVRFlickKeyBoardを設定して下さい。  
![SetupOperater](https://raw.githubusercontent.com/donafudo/VRFlickInput/images/VRFI_SetpuOperater.png)  


次に、VRFlickKeyBoardにアタッチされている`VRFI_ControllerInput`の、
RightControllerTracked,　LeftControllerTrackedのそれぞれに、[CameraRig]内のコントローラーについている`SteamVR_TrackedObject`コンポーネントを設定して下さい。  
![SetupControllerInput](https://raw.githubusercontent.com/donafudo/VRFlickInput/images/SetupControllerInput.png)
## 操作方法
Keyにコントローラを近づけ、トリガーを引いた状態で上下左右にフリック(もしくはそのまま)してトリガーを離すと文字入力ができます。

濁音入力はパッドの左を押したままトリガーを離します、同様にして  
半濁音はパッド右  
小書き文字はパッド下を押しながらトリガーを離して入力します。

グリップボタンを押すと一文字削除できます。

## Licence
The MIT License (MIT)

Copyright (c) 2018 donafudo

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
