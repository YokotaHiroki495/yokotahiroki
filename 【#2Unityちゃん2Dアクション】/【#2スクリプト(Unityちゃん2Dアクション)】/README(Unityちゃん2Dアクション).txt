
📂スクリプトデータ
├📂GameScenes
│├📂Enemys
││├📂ArcherEnemy
│││├ ArcherEnemy.cs ..................... 弓を持っている敵の処理の関する処理 
│││└ SpawnArcherEnemyAttack.cs .......... 弓を持っている敵の攻撃オブジェクトの関する処理 
│││
││├📂Boss
│││├📂Attacks
││││├📂BossFireAttacks
│││││├ BossFireAttackPool.cs ............ ボス敵の通常攻撃のオブジェクトプール
│││││└ BossFireAttackScript.cs .......... ボス敵の通常攻撃の処理
│││││
││││└📂BossNwayAttacks
││││  ├📂NWayBullets
││││  │├ BossNwayAttack.cs .............. ボス敵のNway攻撃の弾のグループの処理
││││  │└ BossNwayAttackPool.cs .......... ボス敵のNway攻撃の弾のグループのオブジェクトプール
││││  │
││││  └📂NWayBulletScripts
││││    ├ BossNwayAttackBullet.cs ........ ボス敵のNway攻撃の攻撃オブジェクトの処理
││││    └ BossNWayBulletPool.cs .......... ボス敵のNway攻撃の弾自体のオブジェクトプール
││││
│││├📂UI
││││└ BossHPBarScript.cs ................. ボス敵の体力ゲージの処理
││││
│││├ BossController.cs .................... ボス敵自体の処理
│││└ BossWeakPointScript.cs ............... ボス敵の弱点(ダメージ)の処理
│││
││├📂EnemyManagers
│││├ EnemyObjectPool.cs ................... 敵を生成するオブジェクトプール
│││└ SpawnEnemy.cs ........................ 敵を生成する処理
│││
││└📂SwordEnemy
││  ├ SpawnSwordEnemyAttack.cs ............. 剣を持っている敵の攻撃オブジェクトの関する処理 
││  └ SwordEnemy.cs ........................ 剣を持っている敵の処理の関する処理 

││
│├📂Items
││├ HPRecoveryItemScript.cs ................ プレイヤーの体力回復アイテムの処理
││└ StaminaRecoveryItemScript.cs ........... プレイヤーのスタミナ回復アイテムの処理
││
│├📂Player
││├📂UI
│││├ Player StaminaVar Script.cs .......... プレイヤーのスタミナゲージの処理
│││└ PlayerHPBarScript.cs ................. プレイヤーの体力ゲージの処理
│││
││├ CameraScript.cs ........................ プレイヤーのカメラの処理
││├ PlayerAttackScript.cs .................. プレイヤーの攻撃オブジェクトの処理
││└ PlayerScript.cs ........................ スレイヤーの処理
││
│├📂StageSystem
││├ FloarScript.cs ......................... ステージの浮いている床の処理
││└ MoveFloarScript.cs ..................... ボスステージの動く床の処理
││
│├ GameManager.cs ........................... シーン移動、スコア表示の処理
│└ SoundManager.cs .......................... サウンド関連の処理
│
├📂ResultScene
│└ ResultSceneScript.cs ..................... リザルトシーンでの処理
│
└📂TitleScenes
  └ TitleSceneScript.cs ...................... タイトルシーンでの処理


