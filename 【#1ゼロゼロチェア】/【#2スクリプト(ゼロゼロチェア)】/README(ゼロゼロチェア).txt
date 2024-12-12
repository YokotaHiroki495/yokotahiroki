📂スクリプトデータ
├📂Enemys		敵に関するスクリプトフォルダ
│├📂EnemyCreate      敵の生成に関するスクリプトフォルダ
││├ EnemySpawner.cs     	敵の生成に関する処理
││└ SingltonEnemyPool.cs   敵のオブジェクトプール
││
│├ EnemyAttackArea.cs    	敵の攻撃範囲に関する処理
│├ EnemyController.cs     	敵の行動に関する処理
│├ EnemyData.asset			敵のデータに関するアセット
│└ EnemyData.cs				敵のデータに関する処理
│
├📂FadeSystem			シーン遷移に関するスクリプトフォルダ
│└ SimpleFadeSceneTransitionSystem.cs		シーン遷移時の演出に関する処理
│
├📂GameSystem			ゲーム全体に関するスクリプトフォルダ
│├ GameManager.cs      ゲーム全体に関する処理
│└ ExitGame.cs			ゲームを終了するための処理
│
├📂ObjectPool		オブジェクトのプールに関するスクリプトフォルダ
│├ BulletPool.cs			GameObjectPoolクラスを継承したBulletPoolクラス
│├ EnemyPool.cs      		GameObjectPoolクラスを継承したEnemyPoolクラス
│├ GameObjectPool.cs      	SingltonGameObjectPoolクラスを継承したBulletとEnemyのオブジェクトプール
│└ SingltonGameObjectPool.cs   GameObjectPoolをシングルトンパターンで管理するためのクラス
│
├📂Players		プレイヤーに関するスクリプトフォルダ
│├📂Bullets		攻撃に関するスクリプトフォルダ
││├ Bullet.cs      攻撃用の弾に関する処理
││├ BulletData		攻撃用の弾に関するアセット
││└ BulletData.cs  攻撃用の弾に関する処理
││
│├ FollowCamera.cs      カメラに関する処理
│├ Player.cs      		プレイヤーに関する処理
│└ PlayerSingleton		Playerをシングルトンパターンで管理するためのクラス
│
├📂Result			リザルトシーンに関するスクリプトフォルダ
│└ MoveStageSelect.cs      リザルトシーンに関する処理
│
├📂StageGimmick			ステージギミックに関するスクリプトフォルダ
│├📂Battle      バトルシーンのギミック関する処理
││└ DamageWall.cs      ダメージ判定のある壁に関する処理
││
│└📂Escape			脱出ステージシーンのギミック関するスクリプトフォルダ
│ ├📂Respawn			チェックポイントに関するスクリプトフォルダ
│ │├ CheckPoint.cs      チェックポイント更新に関する処理
│ │└ PlayerRespawn.cs      プレイヤーのリスポーンに関する処理
│ │
│ ├📂Traps			脱出ステージシーンの罠に関するスクリプトフォルダ
│ │├ MoveBlock.cs      動く箱の罠に関する処理
│ │└ RevolvingTrap.cs      回転扉の罠に関する処理
│ └ Elevator.cs      ゴールであるエレベーターに関する処理
│
├📂StageSelect			ステージセレクトシーンに関するスクリプトフォルダ
│└ ExplanationTextDisplay.cs      各ステージの画像と説明に関する処理
│
├📂Title			タイトルシーンに関するスクリプトフォルダ
│└ TitleUI.cs      タイトルシーンに関する処理
│
├📂Tutorial		チュートリアルシーンに関するスクリプトフォルダ
│├ EnemySpawnCollider.cs	チュートリアルシーンでの敵の生成イベントの処理
│├ ExitArea.cs     			 脱出エリアに関する処理
│└ TutorialController.cs    チュートリアルシーン全体に関する処理
└📂README(ゼロゼロチェア)
