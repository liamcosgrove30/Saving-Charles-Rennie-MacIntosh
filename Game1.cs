using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace SavingCharlesRennieMacintosh
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pressStartImage, newGameImage, newGameSelectedImage, achievImage, achievSelectedImage, optionsImage, optionsSelectedImage, exitImage, exitSelectedImage;
        Texture2D bruCharacterSlecImage, cooCharacterSelecImage, nessieCharacterSelecImage, tamCharacterSelecImage, character2Image, character3Image, volumeBlankImage, volumeFullImage, mainMenuImage, startMenuImage, bagpipePlayerImage;
        Texture2D standOnImage, shortbreadRoadImage, tabletPlatformImage, enemyImage, enemyAttackImage, playerBulletImage, p1UiImage, playerHealthImage, playerHealthRedImage, bruHeadImage;
        Texture2D arrows1Image, cooHeadImage, nessieHead, tamHead, thistleHead, thistleCharacterSelecImage, bulletsAndEnergyImage, irnBruWavesImage, groundImage, energyCollectImage, coinImage;
        Texture2D coinSingleImage, villagerImage, p2UiImage, parchmentImage, jumpImage, villagerBigImage, speechBubble1Image, speechBubble2Image, speechBubble3Image, speechBubble4Image;
        Texture2D bulletCollectImage, speechBubble5Image, speechBubble6Image, speechBubble7Image, monsterImage, lasorImage, flyingEnemiesImage, spaceBackgroundImage, shortBreadTextImage, shower1Image;
        Texture2D shower2Image, shower3Image, shower4Image, bossBulletImage, bossDeathImage, crmImage, bruHudImage, cooHudImage, nessieHudImage, tamHudImage, thistleHudImage, roseCrackImage;
        Texture2D exploImage, rose1Image, rose2Image, bossHudImage, achi1Image, achi1CompImage, achi2Image, achi2CompImage, achi3Image, achi3CompImage, achi4Image, achi4CompImage, achi5Image, achi5CompImage;
        Texture2D achi6Image, achi6CompImage, optionsTextImage, soundsTextImage, optionsRectangleImage, highscoreImage, highscoreSelectedImage, Aimage, Bimage, controllerImage, player2StartImage, readyImage;
        ScrollingLayer backGround = new ScrollingLayer();
        ScrollingLayer midGround = new ScrollingLayer();
        float shade = 1;
        float spaceShade = 0;
        float textShade = 1;
        bool shadeSwitch, player2, textSwitch;
        enum Gamestate { pressStartMenu, mainMenu, characterSelection, optionsMenu, achievMenu, inGame, inGameLevel2, highScoreMenu, controllerMenu }; //different menus
        Gamestate currentGameState = Gamestate.pressStartMenu;
        GamePadState currentButton, prevButton, currentButton2, prevButton2;
        //counter to move the main menu options
        int menuMover = 1;
        //counter to move the choice of characters in the main menu
        int characterMover = 1;
        int characterMover2 = 1;
        int volumeAmount = 10; //selects the amount of full volume bars to draw
        int volumeSoundAmount = 10; //selects the volume for the sound effects 
        int screenWidth = 1920; //width of the screen
        int screenHeight = 1080; //height of the screen
        int characterChoice, characterChoice2; //decides what character will be used to play in the game. Go to player class
        int inGameSpriteChange = 0; //changes the begin call for the draw method
        int gunCoolDown = 0, gunCoolDown2 = 0;
        int timesBossHasDied = 0;
        int bulletsFired = 0;
        int timesCRMSaved = 0;
        int timesPlayerDied = 0;
        int timesPlayerJumps = 0;
        int totalEnemiesKilled = 0;
        bool CRMon = true;
        int volumeMover = 0;
        float soundEffectVolume = 1f;
        int mainMenuCounter = 0;
        bool player2Ready = false;

        const int MaxGunCoolDown = 100, MaxGunCoolDown2 = 100;
        Song testMusic;
        //bagpipe Animation
        GameSprite bagpipePlayer = new GameSprite();
        //different characters
        GameSprite villagerBig = new GameSprite();
        GameSprite characterSelec = new GameSprite();
        GameSprite monster = new GameSprite(); //monster for when you fall into the irn bru lake 
        GameSprite monster2 = new GameSprite(); //monster for when you fall into the irn bru lake player 2 
        GameSprite characterSelec2 = new GameSprite();
        GameSprite speech = new GameSprite();
        GameSprite p1Heads = new GameSprite();
        GameSprite p2Heads = new GameSprite();
        GameSprite playersBulletsAndEnergy = new GameSprite(); //player 1
        GameSprite playersBulletsAndEnergy2 = new GameSprite(); //player 2
        GameSprite coin = new GameSprite(); //the coin that sits unnder the players UI
        GameSprite coin2 = new GameSprite(); //the coin that sits under player 2 UI
        GameSprite parchment = new GameSprite(); //the background for the score 
        GameSprite explo = new GameSprite(); //the explosion for the last part of the game 
        GameSprite bossHud = new GameSprite(); //the boss hud to hold the health 
        Player p1 = new Player(); //player 1       
        PlayerTwo p2 = new PlayerTwo(); //player 2
        PlayerFlying p1Flying = new PlayerFlying(); //player 1 for level 2
        PlayerFlying2 p2Flying = new PlayerFlying2(); //player 2 for level 2
        GameSprite highscore = new GameSprite(); //highscore menu button
        Boss boss = new Boss(); //final boss
        Camera gameCamera;
        List<GameSprite> transPlatforms = new List<GameSprite>(); // what the player actually stands on
        List<GameSprite> tabPlatforms = new List<GameSprite>(); // tablet platforms
        List<GameSprite> irnBruWaves = new List<GameSprite>(); //irn bru waves 
        List<GameSprite> irnBruWaves2 = new List<GameSprite>(); //irn bru waves that the player will fall between 
        List<Enemy> enemies = new List<Enemy>();
        List<GameSprite> energyCollect = new List<GameSprite>(); //energy collectable 
        List<GameSprite> bulletCollect = new List<GameSprite>(); //bullet collectable 
        List<GameSprite> playerHealths = new List<GameSprite>(); //player 1 green health
        List<GameSprite> playerHealths2 = new List<GameSprite>(); //player 2 green health
        List<GameSprite> bossHealths = new List<GameSprite>(); //boss health
        List<GameSprite> coins = new List<GameSprite>(); //coins
        GameSprite flyingCoin = new GameSprite(); //coins for the flying level
        GameSprite playerBullet = new GameSprite(); //player 1 bullets
        GameSprite player2Bullet = new GameSprite(); //player 2 bullets 
        GameSprite p1Ui = new GameSprite(); //player 1 UI
        GameSprite p2Ui = new GameSprite(); //player 2 UI
        GameSprite redHealth = new GameSprite(); //player 1
        GameSprite redHealth2 = new GameSprite(); //player 2
        GameSprite redHealth3 = new GameSprite(); //boss
        GameSprite villager = new GameSprite(); //talking villager at the start of the game
        GameSprite crm = new GameSprite(); //charles rennie mackintosh himself!!!
        GameSprite charHuds = new GameSprite(); //all of the charater huds for player 1
        GameSprite charHuds2 = new GameSprite(); //all of the character huds for player 2
        GameSprite roseCrack = new GameSprite(); //the cracking rose at the end of the game 
        GameSprite rose1 = new GameSprite(); //first part of the rose to collect
        GameSprite rose2 = new GameSprite(); //second part of the rose to collect
        GameSprite achi1 = new GameSprite(); //first achievement 
        GameSprite achi2 = new GameSprite(); //second achievement 
        GameSprite achi3 = new GameSprite(); //third achievement 
        GameSprite achi4 = new GameSprite(); //fourth achievement
        GameSprite achi5 = new GameSprite(); //fith achievement
        GameSprite achi6 = new GameSprite(); //sixth achievment
        List<GameSprite> shower1 = new List<GameSprite>(); //part of the shortbread shower 
        List<GameSprite> shower2 = new List<GameSprite>(); //part of the shortbread shower
        List<GameSprite> shower3 = new List<GameSprite>(); //part of the shortbread shower
        List<GameSprite> shower4 = new List<GameSprite>(); //part of the shortbread shower
        SpriteFont font, fontWhite, font2;
        Vector2 fontPosition, fontPosition2;
        Vector2 fontPositionEnergy, fontPositionEnergy2;
        Vector2 coinCollectedPostion, coinCollectedPostion2, spacePosition;
        Vector2 p1ScorePosition, p2ScorePosition, p1ScoreFlyingPosition, p2ScoreFlyingPosition, shortbreadTextPosition;
        int bulletNumber = 9, energyNumber = 9, coinsCollected = 0;
        int bulletNumber2 = 9, energyNumber2 = 9, coinsCollected2 = 0;
        int p1Score = 0;
        int p2Score = 0;
        int coinDelay = 0;
        int bossHealth = 24;
        bool isPlayerDead;
        bool isPlayerShootingRight;
        bool isPlayerAttacking;
        bool isPlayerShootingRight2;
        bool isPlayerAttacking2;
        bool isPlayerDead2;
        bool flySwitch = false, flySwitch2 = false;
        float playerUp, playerUp2;
        const int flyingMaxGunCoolDown = 500, flyingMaxGunCoolDown2 = 500;
        int flyingGunCoolDown = 0, flyingGunCoolDown2 = 0;
        const int MaxTalkingReset = 1000, MaxTalkingReset2 = 1000;
        int talkingReset = 0, talkingReset2 = 0;

        SoundEffect tamName, tamHurt, tamJump, tamMelee, tamDeath;
        SoundEffect nessieName, nessieHurt, nessieJump, nessieMelee, nessieDeath;
        SoundEffect bruName, bruHurt, bruJump, bruMelee, bruDeath;
        SoundEffect cooName, cooHurt, cooJump, cooMelee, cooDeath;
        SoundEffect thistleName, thistleHurt, thistleJump, thistleMelee, thistleDeath;
        SoundEffect warning, coinCollectedSound, energySound, bulletSound, shootSound, midgeSound;
        SoundEffect bossBulletSound, bossInPainSound, bossDeathSound, bossMeleeSound, coinCountSound, exploSound, guitarRiff, blipSound;
        SoundEffectInstance warningInst, coinInst, shootInst, midgeInst, bossAttackInst, bossPainInst, bossDeathInst, bossMeleeInst, coinCountInst, exploInst, guitarInst;
        SoundEffectInstance bruHurtInst, cooHurtInst, nessieHurtInst, tamHurtInst, thistleHurtInst;


        Random rng = new Random();
        int stage2Counter = 30000, endGameCounter = 5000;
        bool stage1 = true, stage2 = false;
        bool playerHit = false, playerHit2 = false;
        //bullets for the flying level
        List<GameSprite> flyingBullets = new List<GameSprite>();
        List<GameSprite> flyingEnemies = new List<GameSprite>();
        List<GameSprite> bossBullet = new List<GameSprite>();
        bool exploStart = false; //bool to start the explosion 
        bool roseCrackStop = false; //stops the rose constantly cracking
        bool attackedRose = false;
        bool bossKilled = false;
        bool midgeSoundOnce = true;
        bool coinsCanBeAdded = false;
        bool bossDeathSoundOnce = true;

        const int numberofhighscores = 10;                              // Number of high scores to store
        int[] highscores = new int[numberofhighscores];                 // Array of high scores
        string[] highscorenames = new string[numberofhighscores];       // Array of high score names
        const int maxnamelength = 30;   // Maximum name length for high score table
        int lasthighscore = numberofhighscores - 1;
        float keycounter = 0;           // Counter for delay between key strokes
        const float keystrokedelay = 200;   // Delay between key strokes in milliseconds
        Boolean keyboardreleased = true;
        KeyboardState keys;                             // Variable to hold keyboard state
        KeyboardState lastkeystate;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            Window.Position = new Point(0, 0);

            //player 1 start point
            p1.Initialize(0, 0, Content);
            p1.collisionArea.Width = 93;

            //player 2 start point 
            p2.Initialize(0, 0, Content);
            p2.collisionArea.Width = 93;

            //p1 flying start point 
            p1Flying.Initialize(0, 500, Content);
            p1Flying.collisionArea.Width = 93;

            //p2 flying start point 
            p2Flying.Initialize(0, 500, Content);
            p2Flying.collisionArea.Width = 93;

            //boss
            boss.Initialize(1920, 140, Content);
            boss.collisionArea.Width = 93;

            //placement of the scrolling background 
            backGround.scrollAmount.Y = 1670;
            midGround.scrollAmount.Y = 1890;

            monster.position = new Vector2(-1000, -1000);
            monster2.position = new Vector2(-1000, -1000);

            //screen size
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            spacePosition = new Vector2(0, -1080);
            shortbreadTextPosition = new Vector2(0, -500);

            //camera
            gameCamera = new Camera(new Vector2(0, screenHeight / 2), new Rectangle(0, 0, screenWidth, screenHeight));

            //loading in the achievements 
            string inputFile = "achi1.txt";
            if (File.Exists(inputFile))
            {
                StreamReader inputFileStream = new StreamReader(inputFile);
                string inputText = inputFileStream.ReadLine();
                timesBossHasDied = Convert.ToInt32(inputText);
                inputFileStream.Close();
            }
            string inputFile2 = "achi2.txt";
            if (File.Exists(inputFile2))
            {
                StreamReader inputFileStream = new StreamReader(inputFile2);
                string inputText = inputFileStream.ReadLine();
                bulletsFired = Convert.ToInt32(inputText);
                inputFileStream.Close();
            }
            string inputFile3 = "achi3.txt";
            if (File.Exists(inputFile3))
            {
                StreamReader inputFileStream = new StreamReader(inputFile3);
                string inputText = inputFileStream.ReadLine();
                timesCRMSaved = Convert.ToInt32(inputText);
                inputFileStream.Close();
            }
            string inputFile4 = "achi4.txt";
            if (File.Exists(inputFile4))
            {
                StreamReader inputFileStream = new StreamReader(inputFile4);
                string inputText = inputFileStream.ReadLine();
                timesPlayerDied = Convert.ToInt32(inputText);
                inputFileStream.Close();
            }
            string inputFile5 = "achi5.txt";
            if (File.Exists(inputFile5))
            {
                StreamReader inputFileStream = new StreamReader(inputFile5);
                string inputText = inputFileStream.ReadLine();
                timesPlayerJumps = Convert.ToInt32(inputText);
                inputFileStream.Close();
            }
            string inputFile6 = "achi6.txt";
            if (File.Exists(inputFile6))
            {
                StreamReader inputFileStream = new StreamReader(inputFile6);
                string inputText = inputFileStream.ReadLine();
                totalEnemiesKilled = Convert.ToInt32(inputText);
                inputFileStream.Close();
            }
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            pressStartImage = Content.Load<Texture2D>("pressStart");
            newGameImage = Content.Load<Texture2D>("newGame");
            newGameSelectedImage = Content.Load<Texture2D>("newGameSelected");
            achievImage = Content.Load<Texture2D>("achievments");
            achievSelectedImage = Content.Load<Texture2D>("achievmentsSelected");
            optionsImage = Content.Load<Texture2D>("options");
            optionsSelectedImage = Content.Load<Texture2D>("optionsSelected");
            exitImage = Content.Load<Texture2D>("exit");
            exitSelectedImage = Content.Load<Texture2D>("exitSelected");
            testMusic = Content.Load<Song>("froggerBackgroundMusic");
            volumeBlankImage = Content.Load<Texture2D>("volumeBarBlank");
            volumeFullImage = Content.Load<Texture2D>("volumeBarFull");
            mainMenuImage = Content.Load<Texture2D>("mainMenuFace");
            startMenuImage = Content.Load<Texture2D>("startMenu");
            bagpipePlayerImage = Content.Load<Texture2D>("bagpipePlayerSpritesheet");
            playerBulletImage = Content.Load<Texture2D>("ZNKMIX");
            bruCharacterSlecImage = Content.Load<Texture2D>("bruIdle");
            cooCharacterSelecImage = Content.Load<Texture2D>("cooIdle");
            nessieCharacterSelecImage = Content.Load<Texture2D>("nessieIdle");
            tamCharacterSelecImage = Content.Load<Texture2D>("tamIdle");
            thistleCharacterSelecImage = Content.Load<Texture2D>("thistleIdle1");
            character2Image = Content.Load<Texture2D>("character2still");
            character3Image = Content.Load<Texture2D>("character3still");
            standOnImage = Content.Load<Texture2D>("roadRectangle");
            shortbreadRoadImage = Content.Load<Texture2D>("road");
            groundImage = Content.Load<Texture2D>("ground");
            tabletPlatformImage = Content.Load<Texture2D>("platform");
            enemyImage = Content.Load<Texture2D>("enemyWalking");
            enemyAttackImage = Content.Load<Texture2D>("enemyAttacking");
            p1UiImage = Content.Load<Texture2D>("player1UI");
            p2UiImage = Content.Load<Texture2D>("player2UI");
            playerHealthImage = Content.Load<Texture2D>("health");
            playerHealthRedImage = Content.Load<Texture2D>("healthRed");
            bruHeadImage = Content.Load<Texture2D>("bruHead");
            cooHeadImage = Content.Load<Texture2D>("CooHead");
            nessieHead = Content.Load<Texture2D>("NessieHead");
            tamHead = Content.Load<Texture2D>("tamHead");
            thistleHead = Content.Load<Texture2D>("thistleHead1");
            font = Content.Load<SpriteFont>("Font1");
            font2 = Content.Load<SpriteFont>("font2");
            fontWhite = Content.Load<SpriteFont>("Font1White");
            irnBruWavesImage = Content.Load<Texture2D>("irnBruLake");
            energyCollectImage = Content.Load<Texture2D>("energyCollect1");
            bulletCollectImage = Content.Load<Texture2D>("bulletCollect");
            coinImage = Content.Load<Texture2D>("coinCollect1");
            arrows1Image = Content.Load<Texture2D>("arrows");
            bulletsAndEnergyImage = Content.Load<Texture2D>("UI");
            coinSingleImage = Content.Load<Texture2D>("coinCount");
            villagerImage = Content.Load<Texture2D>("villager");
            parchmentImage = Content.Load<Texture2D>("parchment");
            jumpImage = Content.Load<Texture2D>("jump");
            villagerBigImage = Content.Load<Texture2D>("villagerBig");
            speechBubble1Image = Content.Load<Texture2D>("Part1");
            speechBubble2Image = Content.Load<Texture2D>("Part2");
            speechBubble3Image = Content.Load<Texture2D>("Part3");
            speechBubble4Image = Content.Load<Texture2D>("Part4");
            speechBubble5Image = Content.Load<Texture2D>("Part5");
            speechBubble6Image = Content.Load<Texture2D>("Part6");
            speechBubble7Image = Content.Load<Texture2D>("Part7");
            monsterImage = Content.Load<Texture2D>("monster");
            lasorImage = Content.Load<Texture2D>("laser");
            flyingEnemiesImage = Content.Load<Texture2D>("enemyFlyingLevel2");
            spaceBackgroundImage = Content.Load<Texture2D>("space");
            shortBreadTextImage = Content.Load<Texture2D>("shortbreadText");
            shower1Image = Content.Load<Texture2D>("shower1");
            shower2Image = Content.Load<Texture2D>("shower2");
            shower3Image = Content.Load<Texture2D>("shower3");
            shower4Image = Content.Load<Texture2D>("shower5");
            bossBulletImage = Content.Load<Texture2D>("bossBullet");
            bossDeathImage = Content.Load<Texture2D>("bossFlyingDeathPicture");
            crmImage = Content.Load<Texture2D>("crmDance");
            bruHudImage = Content.Load<Texture2D>("bruHud");
            cooHudImage = Content.Load<Texture2D>("cooHud");
            nessieHudImage = Content.Load<Texture2D>("nessieHud");
            tamHudImage = Content.Load<Texture2D>("tamHud");
            thistleHudImage = Content.Load<Texture2D>("thistleHud");
            roseCrackImage = Content.Load<Texture2D>("roseCracking");
            exploImage = Content.Load<Texture2D>("explosion");
            rose1Image = Content.Load<Texture2D>("rosePart1");
            rose2Image = Content.Load<Texture2D>("rosePart2");
            bossHudImage = Content.Load<Texture2D>("bossHUD");
            achi1CompImage = Content.Load<Texture2D>("achi1True");
            achi1Image = Content.Load<Texture2D>("achi1False");
            achi2CompImage = Content.Load<Texture2D>("achi2True");
            achi2Image = Content.Load<Texture2D>("achi2False");
            achi3CompImage = Content.Load<Texture2D>("achi3True");
            achi3Image = Content.Load<Texture2D>("achi3False");
            achi4CompImage = Content.Load<Texture2D>("achi4True");
            achi4Image = Content.Load<Texture2D>("achi4False");
            achi5CompImage = Content.Load<Texture2D>("achi5True");
            achi5Image = Content.Load<Texture2D>("achi5False");
            achi6CompImage = Content.Load<Texture2D>("achi6True");
            achi6Image = Content.Load<Texture2D>("achi6False");
            optionsTextImage = Content.Load<Texture2D>("optionsText");
            soundsTextImage = Content.Load<Texture2D>("optionsText2");
            optionsRectangleImage = Content.Load<Texture2D>("optionsRectangle");
            highscoreImage = Content.Load<Texture2D>("highscoresText");
            highscoreSelectedImage = Content.Load<Texture2D>("highscoresTextSelected");
            Aimage = Content.Load<Texture2D>("A");
            Bimage = Content.Load<Texture2D>("B");
            controllerImage = Content.Load<Texture2D>("ControllerLayout");
            player2StartImage = Content.Load<Texture2D>("player2PressStart");
            readyImage = Content.Load<Texture2D>("readyText");

            //sound effects 
            tamName = Content.Load<SoundEffect>("tamNameSound");
            tamHurt = Content.Load<SoundEffect>("tamHurtSound");
            tamJump = Content.Load<SoundEffect>("tamJumpSound");
            tamMelee = Content.Load<SoundEffect>("tamMeleeSound");
            tamDeath = Content.Load<SoundEffect>("tamDeathSound");
            nessieName = Content.Load<SoundEffect>("nessieNameSoundNew");
            nessieHurt = Content.Load<SoundEffect>("nessieHurtSoundNew");
            nessieJump = Content.Load<SoundEffect>("nessieJumpSoundNew");
            nessieMelee = Content.Load<SoundEffect>("nessieMeleeSoundNew");
            nessieDeath = Content.Load<SoundEffect>("nessieDeathSoundNew");
            bruName = Content.Load<SoundEffect>("bruNameSound");
            bruHurt = Content.Load<SoundEffect>("bruHurtSound");
            bruJump = Content.Load<SoundEffect>("bruJumpSound");
            bruMelee = Content.Load<SoundEffect>("bruMeleeSound");
            bruDeath = Content.Load<SoundEffect>("bruDeathSound");
            cooName = Content.Load<SoundEffect>("cooNameSoundNew");
            cooHurt = Content.Load<SoundEffect>("cooHurtSoundNew");
            cooJump = Content.Load<SoundEffect>("cooJumpSoundNew");
            cooMelee = Content.Load<SoundEffect>("cooMeleeSoundNew");
            cooDeath = Content.Load<SoundEffect>("cooDeathSoundNew");
            thistleName = Content.Load<SoundEffect>("thistleNameSoundNew");
            thistleHurt = Content.Load<SoundEffect>("thistleHurtSoundNew");
            thistleJump = Content.Load<SoundEffect>("thistleJumpSoundNew");
            thistleMelee = Content.Load<SoundEffect>("thistleMeleeSoundNew");
            thistleDeath = Content.Load<SoundEffect>("thistleDeathSoundNew");
            warning = Content.Load<SoundEffect>("shortbreadSound");
            coinCollectedSound = Content.Load<SoundEffect>("coin-collect");
            energySound = Content.Load<SoundEffect>("energy-pick-up");
            bulletSound = Content.Load<SoundEffect>("bullet-pickup");
            shootSound = Content.Load<SoundEffect>("fireBullet");
            midgeSound = Content.Load<SoundEffect>("MidgeSound");
            bossBulletSound = Content.Load<SoundEffect>("bossAttackSound");
            bossInPainSound = Content.Load<SoundEffect>("bossHurtSound");
            bossDeathSound = Content.Load<SoundEffect>("bossDeathSound");
            bossMeleeSound = Content.Load<SoundEffect>("bossMeleeSound");
            coinCountSound = Content.Load<SoundEffect>("coinCountingSound");
            exploSound = Content.Load<SoundEffect>("explosionSound");
            guitarRiff = Content.Load<SoundEffect>("guitar-riff");
            blipSound = Content.Load<SoundEffect>("blipSound");

            guitarInst = guitarRiff.CreateInstance();
            exploInst = exploSound.CreateInstance();
            coinCountInst = coinCountSound.CreateInstance();
            bossMeleeInst = bossMeleeSound.CreateInstance();
            bossDeathInst = bossDeathSound.CreateInstance();
            warningInst = warning.CreateInstance();
            coinInst = coinCollectedSound.CreateInstance();
            shootInst = shootSound.CreateInstance();
            midgeInst = midgeSound.CreateInstance();
            bossAttackInst = bossBulletSound.CreateInstance();
            bossPainInst = bossInPainSound.CreateInstance();
            bruHurtInst = bruHurt.CreateInstance();
            cooHurtInst = cooHurt.CreateInstance();
            nessieHurtInst = nessieHurt.CreateInstance();
            tamHurtInst = tamHurt.CreateInstance();
            thistleHurtInst = thistleHurt.CreateInstance();
            

            //scrolling layers
            midGround.image = Content.Load<Texture2D>("midGround");
            backGround.image = Content.Load<Texture2D>("backgroundSky3");

            //achievements 
            achi1.AddAnimation(achi1Image, 1, 1);
            achi1.AddAnimation(achi1CompImage, 1, 1);
            achi1.position = new Vector2(0, 0);
            achi2.AddAnimation(achi2Image, 1, 1);
            achi2.AddAnimation(achi2CompImage, 1, 1);
            achi2.position = new Vector2(0, 300);
            achi3.AddAnimation(achi3Image, 1, 1);
            achi3.AddAnimation(achi3CompImage, 1, 1);
            achi3.position = new Vector2(0, 600);
            achi4.AddAnimation(achi4Image, 1, 1);
            achi4.AddAnimation(achi4CompImage, 1, 1);
            achi4.position = new Vector2(1200, 0);
            achi5.AddAnimation(achi5Image, 1, 1);
            achi5.AddAnimation(achi5CompImage, 1, 1);
            achi5.position = new Vector2(1200, 300);
            achi6.AddAnimation(achi6Image, 1, 1);
            achi6.AddAnimation(achi6CompImage, 1, 1);
            achi6.position = new Vector2(1200, 600);

            //highscore button
            highscore.AddAnimation(highscoreImage, 1, 1);
            highscore.AddAnimation(highscoreSelectedImage, 1, 1);


            //boss hud
            bossHud.AddAnimation(bossHudImage, 1, 1);
            bossHud.position = new Vector2(650, 1080);

            //rose pieces 
            rose1.AddAnimation(rose2Image, 27, 1000);
            rose1.position = new Vector2(19200, 800);

            rose2.AddAnimation(rose1Image, 27, 1000);
            rose2.position = new Vector2(2100, 300);

            //rose crack position 
            roseCrack.AddAnimation(roseCrackImage, 17, 1);
            roseCrack.AddAnimation(roseCrackImage, 17, 2000);          
            roseCrack.position = new Vector2(-21000, 620);

            //explostion for the last level
            explo.AddAnimation(exploImage, 38, 1000);
            explo.position = new Vector2(-20950, -2000);

            //flying coins for level 2
            flyingCoin.AddAnimation(coinImage, 60, 1500);
            flyingCoinReset();

            //character hud
            charHuds.AddAnimation(bruHudImage, 1, 1);
            charHuds.AddAnimation(cooHudImage, 1, 1);
            charHuds.AddAnimation(nessieHudImage, 1, 1);
            charHuds.AddAnimation(tamHudImage, 1, 1);
            charHuds.AddAnimation(thistleHudImage, 1, 1);
            charHuds.position = new Vector2(150, 50);

            //character hud for player 2
            charHuds2.AddAnimation(bruHudImage, 1, 1);
            charHuds2.AddAnimation(cooHudImage, 1, 1);
            charHuds2.AddAnimation(nessieHudImage, 1, 1);
            charHuds2.AddAnimation(tamHudImage, 1, 1);
            charHuds2.AddAnimation(thistleHudImage, 1, 1);
            charHuds2.position = new Vector2(1525, 50);

            //crm
            crm.AddAnimation(crmImage, 55, 4500);
            crm.position = new Vector2(-20900, 680);

            //monster 
            monster.AddAnimation(monsterImage, 17, 1200);

            monster2.AddAnimation(monsterImage, 17, 1200);
            //speech 
            speech.AddAnimation(speechBubble1Image, 1, 1);
            speech.AddAnimation(speechBubble2Image, 1, 1);
            speech.AddAnimation(speechBubble3Image, 1, 1);
            speech.AddAnimation(speechBubble4Image, 1, 1);
            speech.AddAnimation(speechBubble5Image, 1, 1);
            speech.AddAnimation(speechBubble6Image, 1, 1);
            speech.AddAnimation(speechBubble7Image, 1, 1);

            //big villager
            villagerBig.AddAnimation(villagerBigImage, 1, 1);

            //parchment 
            parchment.AddAnimation(parchmentImage, 1, 1);

            //villager 
            villager.AddAnimation(villagerImage, 16, 1500);

            //coin that sits under the players UI
            coin.AddAnimation(coinSingleImage, 53, 1500);
            coin2.AddAnimation(coinSingleImage, 53, 1500);

            //animation bagpipe
            bagpipePlayer.AddAnimation(bagpipePlayerImage, 28, 1000);

            //player bullets
            playerBullet.AddAnimation(playerBulletImage, 1, 1);
            player2Bullet.AddAnimation(playerBulletImage, 1, 1);

            //animation characters player 1
            characterSelec.AddAnimation(bruCharacterSlecImage, 7, 600);
            characterSelec.AddAnimation(cooCharacterSelecImage, 7, 600);
            characterSelec.AddAnimation(nessieCharacterSelecImage, 7, 600);
            characterSelec.AddAnimation(tamCharacterSelecImage, 7, 600);
            characterSelec.AddAnimation(thistleCharacterSelecImage, 7, 600);

            //animation characters player 2
            characterSelec2.AddAnimation(bruCharacterSlecImage, 7, 600);
            characterSelec2.AddAnimation(cooCharacterSelecImage, 7, 600);
            characterSelec2.AddAnimation(nessieCharacterSelecImage, 7, 600);
            characterSelec2.AddAnimation(tamCharacterSelecImage, 7, 600);
            characterSelec2.AddAnimation(thistleCharacterSelecImage, 7, 600);

            //player 1 UI
            p1Ui.AddAnimation(p1UiImage, 1, 1);

            //player 2 UI
            p2Ui.AddAnimation(p2UiImage, 1, 1);

            //red health
            redHealth.AddAnimation(playerHealthRedImage, 1, 1);
            redHealth2.AddAnimation(playerHealthRedImage, 1, 1);
            redHealth3.AddAnimation(playerHealthRedImage, 1, 1);

            redHealth3.position = new Vector2(850, 1175);

            //all the player heads that get displayed on the UI
            p1Heads.AddAnimation(bruHeadImage, 1, 1);
            p1Heads.AddAnimation(cooHeadImage, 1, 1);
            p1Heads.AddAnimation(nessieHead, 1, 1);
            p1Heads.AddAnimation(tamHead, 1, 1);
            p1Heads.AddAnimation(thistleHead, 1, 1);
            p2Heads.AddAnimation(bruHeadImage, 1, 1);
            p2Heads.AddAnimation(cooHeadImage, 1, 1);
            p2Heads.AddAnimation(nessieHead, 1, 1);
            p2Heads.AddAnimation(tamHead, 1, 1);
            p2Heads.AddAnimation(thistleHead, 1, 1);

            //players UI
            playersBulletsAndEnergy.AddAnimation(bulletsAndEnergyImage, 1, 1);
            playersBulletsAndEnergy2.AddAnimation(bulletsAndEnergyImage, 1, 1);

            //original positions
            //bagpipe position
            bagpipePlayer.position = new Vector2(-10, 500);
            //player bullets initial position
            playerBullet.position = new Vector2(-10000, -10000);
            player2Bullet.position = new Vector2(-100, -100);
            //character position
            characterSelec.position = new Vector2(200, screenHeight / 4);
            characterSelec2.position = new Vector2(912, 470);
            //background music
            MediaPlayer.Play(testMusic);
            //allows the music to repeat
            MediaPlayer.IsRepeating = true;
            //volume that it plays at
            MediaPlayer.Volume = 1f;

            //shortbread shower

            for (int counter = 0; counter < 6; counter++)
            {
                shower1.Add(new GameSprite());

            }
            foreach (GameSprite s1 in shower1)
            {
                s1.AddAnimation(shower1Image, 15, 1000);
                s1Positions();
            }

            for (int counter = 0; counter < 2; counter++)
            {
                shower2.Add(new GameSprite());
            }
            foreach (GameSprite s2 in shower2)
            {
                s2.AddAnimation(shower2Image, 15, 1000);
                s2Positions();
            }

            for (int counter = 0; counter < 3; counter++)
            {
                shower3.Add(new GameSprite());
            }
            foreach (GameSprite s3 in shower3)
            {
                s3.AddAnimation(shower3Image, 15, 1000);
                s3Positions();
            }

            for (int counter = 0; counter < 4; counter++)
            {
                shower4.Add(new GameSprite());
            }
            foreach (GameSprite s4 in shower4)
            {
                s4.AddAnimation(shower4Image, 15, 1000);
                s4Positions();

            }

            //creates the amount of coins 
            for (int counter = 0; counter < 64; counter++)
            {
                coins.Add(new GameSprite());
            }

            foreach (GameSprite c in coins)
            {
                c.AddAnimation(coinImage, 60, 1500);
                coinPositions();
            }


            //creates the amount of bullet collectables 
            for (int counter = 0; counter < 8; counter++)
            {
                bulletCollect.Add(new GameSprite());
            }
            foreach (GameSprite bCollect in bulletCollect)
            {
                bCollect.AddAnimation(bulletCollectImage, 48, 1500);
                bulletCollectPositions();

            }


            //creates the amount of energy collectables 
            for (int counter = 0; counter < 7; counter++)
            {
                energyCollect.Add(new GameSprite());
            }

            foreach (GameSprite eCollect in energyCollect)
            {
                eCollect.AddAnimation(energyCollectImage, 48, 1000);

                energyCollectPositions();

            }


            //creates the amount of iron bru waves 
            for (int counter = 0; counter < 60; counter++)
            {
                irnBruWaves.Add(new GameSprite());
            }

            //creates the amount of irn bru waves the player will fall bwteen
            for (int counter = 0; counter < 30; counter++)
            {
                irnBruWaves2.Add(new GameSprite());
            }

            foreach (GameSprite fallBetweenWaves in irnBruWaves2)
            {
                fallBetweenWaves.AddAnimation(irnBruWavesImage, 29, 1500);

                fallBetweenWavesPositions();
            }




            foreach (GameSprite wave in irnBruWaves)
            {
                wave.AddAnimation(irnBruWavesImage, 29, 1500);

                //top wave 
                irnBruWavePositions();

            }




            //creates the amount of player health
            for (int counter = 0; counter < 24; counter++)
            {
                playerHealths.Add(new GameSprite());
                playerHealths2.Add(new GameSprite());
                bossHealths.Add(new GameSprite());
            }


            //creates the amount of transparent platforms 
            for (int counter = 0; counter < 8; counter++)
            {
                transPlatforms.Add(new GameSprite());
            }

            //creates the amount of tablet platforms
            for (int counter = 0; counter < 33; counter++)
            {
                tabPlatforms.Add(new GameSprite());
            }

            //creates the amount of enemies 
            for (int counter = 0; counter < 11; counter++)
            {
                enemies.Add(new Enemy());
            }

            //each health gets given its image
            foreach (GameSprite health in playerHealths)
            {
                health.AddAnimation(playerHealthImage, 1, 1);
            }
            foreach (GameSprite health2 in playerHealths2)
            {
                health2.AddAnimation(playerHealthImage, 1, 1);
            }
            foreach (GameSprite bossH in bossHealths)
            {
                bossH.AddAnimation(playerHealthImage, 1, 1);
            }

            //positions of each enemy also velocity 
            foreach (Enemy enem in enemies)
            {
                enem.AddAnimation(enemyImage, 26, 1000);
                enem.AddAnimation(enemyAttackImage, 26, 1000);

                //enemy velocity
                enem.velocity = new Vector2(1, 0);
                enemyPositions();

            }



            //positions of each tablet platforms
            foreach (GameSprite tab in tabPlatforms)
            {
                tab.AddAnimation(tabletPlatformImage, 1, 1);
                tabletPlatformPositions();
            }



            //positions of each transparent platform 
            foreach (GameSprite trans in transPlatforms)
            {
                trans.AddAnimation(standOnImage, 1, 1);

                //before parkour platforms
                transPlatformsPositions(0, 0, 940);
                transPlatformsPositions(1, 1786, 940);
                transPlatformsPositions(2, 3572, 940);
                transPlatformsPositions(3, 5358, 940);
                transPlatformsPositions(4, 7114, 940);
                transPlatformsPositions(5, 8930, 940);
                //after parkour platforms 
                transPlatformsPositions(6, 17500, 940);
                //after you kill the boss
                transPlatformsPositions(7, -22500, 940);
            }


            // Load in high scores
            if (File.Exists(@"highscore.txt")) // This checks to see if the file exists
            {
                StreamReader sr = new StreamReader(@"highscore.txt");	// Open the file

                String line;		// Create a string variable to read each line into
                for (int i = 0; i < numberofhighscores && !sr.EndOfStream; i++)
                {
                    line = sr.ReadLine();	// Read the first line in the text file
                    highscorenames[i] = line.Trim(); // Read high score name

                    if (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();	// Read the first line in the text file
                        line = line.Trim(); 	// This trims spaces from either side of the text
                        highscores[i] = Convert.ToInt32(line);	// This converts line to numeric
                    }
                }
                sr.Close();			// Close the file
            }
            // SORT HIGH SCORE TABLE
            Array.Sort(highscores, highscorenames);
            Array.Reverse(highscores);
            Array.Reverse(highscorenames);



        }

        private void enemyPositions()
        {
            enemies[0].position = new Vector2(2400, 410);
            enemies[1].position = new Vector2(4910, 410);
            enemies[2].position = new Vector2(5200, 410);
            enemies[3].position = new Vector2(5100, 120);
            enemies[4].position = new Vector2(8200, 50);
            enemies[5].position = new Vector2(8400, 50);
            enemies[6].position = new Vector2(8600, 50);
            enemies[7].position = new Vector2(8600, 410);
            enemies[8].position = new Vector2(13400, 260);
            enemies[9].position = new Vector2(14100, 260);
            enemies[10].position = new Vector2(14800, 260);
        }

        private void energyCollectPositions()
        {
            energyCollect[0].position = new Vector2(2345, 550);
            energyCollect[1].position = new Vector2(2445, 550);
            energyCollect[2].position = new Vector2(5050, 260);
            energyCollect[3].position = new Vector2(5150, 260);
            energyCollect[4].position = new Vector2(8600, 550);
            energyCollect[5].position = new Vector2(13230, 400);
            energyCollect[6].position = new Vector2(13550, 400);
        }

        private void bulletCollectPositions()
        {
            bulletCollect[0].position = new Vector2(5100, 550);
            bulletCollect[1].position = new Vector2(5050, 550);
            bulletCollect[2].position = new Vector2(5150, 550);
            bulletCollect[3].position = new Vector2(8550, 190);
            bulletCollect[4].position = new Vector2(8600, 190);
            bulletCollect[5].position = new Vector2(8650, 190);
            bulletCollect[6].position = new Vector2(14630, 400);
            bulletCollect[7].position = new Vector2(14950, 400);
        }

        private void s4Positions()
        {
            shower4[0].position = new Vector2(5900, 0);
            shower4[1].position = new Vector2(5900, 200);
            shower4[2].position = new Vector2(5900, 400);
            shower4[3].position = new Vector2(5900, 600);
        }

        private void s3Positions()
        {
            shower3[0].position = new Vector2(4800, 0);
            shower3[1].position = new Vector2(4900, 200);
            shower3[2].position = new Vector2(5000, 400);
        }

        private void s2Positions()
        {
            shower2[0].position = new Vector2(3000, 400);
            shower2[1].position = new Vector2(4000, 200);
        }

        private void s1Positions()
        {
            shower1[0].position = new Vector2(2000, 0);
            shower1[1].position = new Vector2(2300, 800);
            shower1[2].position = new Vector2(4300, 800);
            shower1[3].position = new Vector2(7200, 0);
            shower1[4].position = new Vector2(7200, 800);
            shower1[5].position = new Vector2(8200, 400);
        }

        private void flyingCoinReset()
        {
            flyingCoin.position = new Vector2(rng.Next(2500, 3500), rng.Next(100, 900));
        }

        private void coinPositions()
        {
            //first platform part
            coins[0].position = new Vector2(2000, 550);
            coins[1].position = new Vector2(2100, 550);
            coins[2].position = new Vector2(2200, 550);
            coins[3].position = new Vector2(2550, 550);
            coins[4].position = new Vector2(2650, 550);
            coins[5].position = new Vector2(2750, 550);

            //pyramid 
            coins[6].position = new Vector2(4600, 550);
            coins[7].position = new Vector2(4700, 550);
            coins[8].position = new Vector2(4800, 550);
            coins[9].position = new Vector2(5400, 550);
            coins[10].position = new Vector2(5500, 550);
            coins[11].position = new Vector2(5600, 550);

            //floating in air
            coins[12].position = new Vector2(6350, 550);
            coins[13].position = new Vector2(6450, 550);
            coins[14].position = new Vector2(6550, 550);
            coins[15].position = new Vector2(6650, 550);
            coins[16].position = new Vector2(6750, 550);
            coins[17].position = new Vector2(6350, 450);
            coins[18].position = new Vector2(6450, 450);
            coins[19].position = new Vector2(6550, 450);
            coins[20].position = new Vector2(6650, 450);
            coins[21].position = new Vector2(6750, 450);

            //trapeez
            coins[22].position = new Vector2(7425, 550);
            coins[23].position = new Vector2(7525, 490);
            coins[24].position = new Vector2(7625, 430);
            coins[25].position = new Vector2(7725, 370);
            coins[26].position = new Vector2(7825, 310);
            coins[27].position = new Vector2(7925, 250);
            coins[28].position = new Vector2(9250, 250);
            coins[29].position = new Vector2(9350, 310);
            coins[30].position = new Vector2(9450, 370);
            coins[31].position = new Vector2(9550, 430);
            coins[32].position = new Vector2(9650, 490);
            coins[33].position = new Vector2(9750, 550);

            //jumps
            coins[34].position = new Vector2(11875, 600);
            coins[35].position = new Vector2(11975, 600);
            coins[36].position = new Vector2(12075, 600);
            coins[37].position = new Vector2(13975, 400);
            coins[38].position = new Vector2(14075, 400);
            coins[39].position = new Vector2(14175, 400);
            coins[40].position = new Vector2(16075, 600);
            coins[41].position = new Vector2(16175, 600);
            coins[42].position = new Vector2(16275, 600);

            //end
            coins[43].position = new Vector2(18100, 550);
            coins[44].position = new Vector2(18200, 550);
            coins[45].position = new Vector2(18300, 550);
            coins[46].position = new Vector2(18400, 550);
            coins[47].position = new Vector2(18500, 550);
            coins[48].position = new Vector2(18600, 550);
            coins[49].position = new Vector2(18700, 550);

            coins[50].position = new Vector2(18100, 450);
            coins[51].position = new Vector2(18200, 450);
            coins[52].position = new Vector2(18300, 450);
            coins[53].position = new Vector2(18400, 450);
            coins[54].position = new Vector2(18500, 450);
            coins[55].position = new Vector2(18600, 450);
            coins[56].position = new Vector2(18700, 450);

            coins[57].position = new Vector2(18100, 350);
            coins[58].position = new Vector2(18200, 350);
            coins[59].position = new Vector2(18300, 350);
            coins[60].position = new Vector2(18400, 350);
            coins[61].position = new Vector2(18500, 350);
            coins[62].position = new Vector2(18600, 350);
            coins[63].position = new Vector2(18700, 350);
        }

        private void fallBetweenWavesPositions()
        {
            //top wave 
            irnBruWaves2[0].position = new Vector2(10848, 970);
            irnBruWaves2[1].position = new Vector2(11100, 970);
            irnBruWaves2[2].position = new Vector2(11350, 970);
            irnBruWaves2[3].position = new Vector2(11600, 970);
            irnBruWaves2[4].position = new Vector2(11850, 970);
            irnBruWaves2[5].position = new Vector2(12050, 970);
            irnBruWaves2[6].position = new Vector2(12200, 970);
            irnBruWaves2[7].position = new Vector2(12450, 970);
            irnBruWaves2[8].position = new Vector2(12650, 970);
            irnBruWaves2[9].position = new Vector2(12850, 970);
            irnBruWaves2[10].position = new Vector2(13050, 970);
            irnBruWaves2[11].position = new Vector2(13250, 970);
            irnBruWaves2[12].position = new Vector2(13450, 970);
            irnBruWaves2[13].position = new Vector2(13650, 970);
            irnBruWaves2[14].position = new Vector2(13850, 970);
            irnBruWaves2[15].position = new Vector2(14050, 970);
            irnBruWaves2[16].position = new Vector2(14250, 970);
            irnBruWaves2[17].position = new Vector2(14500, 970);
            irnBruWaves2[18].position = new Vector2(14750, 970);
            irnBruWaves2[19].position = new Vector2(15000, 970);
            irnBruWaves2[20].position = new Vector2(15250, 970);
            irnBruWaves2[21].position = new Vector2(15500, 970);
            irnBruWaves2[22].position = new Vector2(15750, 970);
            irnBruWaves2[23].position = new Vector2(16000, 970);
            irnBruWaves2[24].position = new Vector2(16200, 970);
            irnBruWaves2[25].position = new Vector2(16400, 970);
            irnBruWaves2[26].position = new Vector2(16600, 970);
            irnBruWaves2[27].position = new Vector2(16850, 970);
            irnBruWaves2[28].position = new Vector2(17050, 970);
            irnBruWaves2[29].position = new Vector2(17250, 970);
        }

        private void irnBruWavePositions()
        {
            irnBruWaves[0].position = new Vector2(10848, 870);
            irnBruWaves[1].position = new Vector2(11100, 870);
            irnBruWaves[2].position = new Vector2(11350, 870);
            irnBruWaves[3].position = new Vector2(11600, 870);
            irnBruWaves[4].position = new Vector2(11850, 870);
            irnBruWaves[5].position = new Vector2(12050, 870);
            irnBruWaves[6].position = new Vector2(12200, 870);
            irnBruWaves[7].position = new Vector2(12450, 870);
            irnBruWaves[8].position = new Vector2(12650, 870);
            irnBruWaves[9].position = new Vector2(12850, 870);
            irnBruWaves[10].position = new Vector2(13050, 870);
            irnBruWaves[11].position = new Vector2(13250, 870);
            irnBruWaves[12].position = new Vector2(13450, 870);
            irnBruWaves[13].position = new Vector2(13650, 870);
            irnBruWaves[14].position = new Vector2(13850, 870);
            irnBruWaves[15].position = new Vector2(14050, 870);
            irnBruWaves[16].position = new Vector2(14250, 870);
            irnBruWaves[17].position = new Vector2(14500, 870);
            irnBruWaves[18].position = new Vector2(14750, 870);
            irnBruWaves[19].position = new Vector2(15000, 870);
            irnBruWaves[20].position = new Vector2(15250, 870);
            irnBruWaves[21].position = new Vector2(15500, 870);
            irnBruWaves[22].position = new Vector2(15750, 870);
            irnBruWaves[23].position = new Vector2(16000, 870);
            irnBruWaves[24].position = new Vector2(16200, 870);
            irnBruWaves[25].position = new Vector2(16400, 870);
            irnBruWaves[26].position = new Vector2(16600, 870);
            irnBruWaves[27].position = new Vector2(16850, 870);
            irnBruWaves[28].position = new Vector2(17050, 870);
            irnBruWaves[29].position = new Vector2(17250, 920);
            irnBruWaves[30].position = new Vector2(17250, 870);

            //middle wave
            irnBruWaves[31].position = new Vector2(10848, 920);
            irnBruWaves[32].position = new Vector2(11108, 910);
            irnBruWaves[33].position = new Vector2(11350, 920);
            irnBruWaves[34].position = new Vector2(11600, 920);
            irnBruWaves[35].position = new Vector2(11856, 920);
            irnBruWaves[36].position = new Vector2(12050, 910);
            irnBruWaves[37].position = new Vector2(12200, 920);
            irnBruWaves[38].position = new Vector2(12450, 920);
            irnBruWaves[39].position = new Vector2(12650, 920);
            irnBruWaves[40].position = new Vector2(12850, 920);
            irnBruWaves[41].position = new Vector2(13055, 910);
            irnBruWaves[42].position = new Vector2(13250, 910);
            irnBruWaves[43].position = new Vector2(13450, 920);
            irnBruWaves[44].position = new Vector2(13650, 920);
            irnBruWaves[45].position = new Vector2(13850, 920);
            irnBruWaves[46].position = new Vector2(14050, 920);
            irnBruWaves[47].position = new Vector2(14250, 920);
            irnBruWaves[48].position = new Vector2(14508, 920);
            irnBruWaves[49].position = new Vector2(14750, 910);
            irnBruWaves[50].position = new Vector2(15000, 920);
            irnBruWaves[51].position = new Vector2(15250, 920);
            irnBruWaves[52].position = new Vector2(15500, 920);
            irnBruWaves[53].position = new Vector2(15750, 920);
            irnBruWaves[54].position = new Vector2(16000, 920);
            irnBruWaves[55].position = new Vector2(16201, 910);
            irnBruWaves[56].position = new Vector2(16409, 920);
            irnBruWaves[57].position = new Vector2(16600, 920);
            irnBruWaves[58].position = new Vector2(16850, 910);
            irnBruWaves[59].position = new Vector2(17059, 910);

        }

        private void tabletPlatformPositions()
        {
            //first two
            tabletPlatformPositions(0, 2000, 650);
            tabletPlatformPositions(1, 2410, 650);

            //pyramid
            tabletPlatformPositions(2, 4500, 650);
            tabletPlatformPositions(3, 4910, 650);
            tabletPlatformPositions(4, 5320, 650);
            tabletPlatformPositions(5, 4705, 360);
            tabletPlatformPositions(6, 5115, 360);
            tabletPlatformPositions(7, 4910, 70);

            //trapeez
            tabletPlatformPositions(8, 7400, 650);
            tabletPlatformPositions(9, 7500, 590);
            tabletPlatformPositions(10, 7600, 530);
            tabletPlatformPositions(11, 7700, 470);
            tabletPlatformPositions(12, 7800, 410);
            tabletPlatformPositions(13, 7900, 350);
            tabletPlatformPositions(14, 8000, 290);
            tabletPlatformPositions(15, 8410, 290);
            tabletPlatformPositions(16, 8820, 290);
            tabletPlatformPositions(17, 8920, 350);
            tabletPlatformPositions(18, 9020, 410);
            tabletPlatformPositions(19, 9120, 470);
            tabletPlatformPositions(20, 9220, 530);
            tabletPlatformPositions(21, 9320, 590);
            tabletPlatformPositions(22, 9420, 650);
            tabletPlatformPositions(23, 8410, 650);

            //platformer 
            tabletPlatformPositions(24, 11100, 800);
            tabletPlatformPositions(25, 11800, 700);
            tabletPlatformPositions(26, 12500, 600);
            tabletPlatformPositions(27, 13200, 500);
            tabletPlatformPositions(28, 13900, 500);
            tabletPlatformPositions(29, 14600, 500);
            tabletPlatformPositions(30, 15300, 600);
            tabletPlatformPositions(31, 16000, 700);
            tabletPlatformPositions(32, 16700, 800);
        }

        private void tabletPlatformPositions(int platformNumber, int platformPositionX, int platformPositionY)
        {
            tabPlatforms[platformNumber].position = new Vector2(platformPositionX, platformPositionY);
        }

        private void transPlatformsPositions(int platformNumber, int positionOnScreenX, int positionOnScreenY)
        {
            transPlatforms[platformNumber].position = new Vector2(positionOnScreenX, positionOnScreenY);
        }



        protected override void UnloadContent()
        {
            // Save high scores
            StreamWriter sw = new StreamWriter(@"highscore.txt");
            for (int i = 0; i < numberofhighscores; i++)
            {
                sw.WriteLine(highscorenames[i]);
                sw.WriteLine(highscores[i].ToString());
            }
            sw.Close();
        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            keyboardreleased = (keys != lastkeystate);      // Has keyboard input changed            

            float timebetweenupdates = (float)gameTime.ElapsedGameTime.TotalMilliseconds; // Time between updates

            if (currentButton.IsButtonDown(Buttons.Back) && currentGameState == Gamestate.inGame || currentButton.IsButtonDown(Buttons.Back) && currentGameState == Gamestate.inGameLevel2)
            {
                mainMenuCounter += gameTime.ElapsedGameTime.Milliseconds;

                if (mainMenuCounter >= 2000)
                {
                    inGameSpriteChange = 0;
                    currentGameState = Gamestate.mainMenu;

                    //saving the achievement
                    string outputFile = "achi1.txt";
                    StreamWriter outputFileStream = new StreamWriter(outputFile);
                    outputFileStream.WriteLine(timesBossHasDied);
                    outputFileStream.Close();
                    //achi 2
                    string outputFile2 = "achi2.txt";
                    StreamWriter outputFileStream2 = new StreamWriter(outputFile2);
                    outputFileStream2.WriteLine(bulletsFired);
                    outputFileStream2.Close();
                    //achi 3
                    string outputFile3 = "achi3.txt";
                    StreamWriter outputFileStream3 = new StreamWriter(outputFile3);
                    outputFileStream3.WriteLine(timesCRMSaved);
                    outputFileStream3.Close();
                    //achi 4
                    string outputFile4 = "achi4.txt";
                    StreamWriter outputFileStream4 = new StreamWriter(outputFile4);
                    outputFileStream4.WriteLine(timesPlayerDied);
                    outputFileStream4.Close();
                    //achi 5
                    string outputFile5 = "achi5.txt";
                    StreamWriter outputFileStream5 = new StreamWriter(outputFile5);
                    outputFileStream5.WriteLine(timesPlayerJumps);
                    outputFileStream5.Close();
                    //achi 6
                    string outputFile6 = "achi6.txt";
                    StreamWriter outputFileStream6 = new StreamWriter(outputFile6);
                    outputFileStream6.WriteLine(totalEnemiesKilled);
                    outputFileStream6.Close();                   
                }
            }
            else mainMenuCounter = 0;

            currentButton = GamePad.GetState(PlayerIndex.One);
            currentButton2 = GamePad.GetState(PlayerIndex.Two);

            //allows for the sound effects volume to be customizable 
            guitarInst.Volume = soundEffectVolume;
            exploInst.Volume = soundEffectVolume;
            coinCountInst.Volume = soundEffectVolume;
            bossMeleeInst.Volume = soundEffectVolume;
            bossDeathInst.Volume = soundEffectVolume;
            warningInst.Volume = soundEffectVolume;
            coinInst.Volume = soundEffectVolume;
            shootInst.Volume = soundEffectVolume;
            midgeInst.Volume = soundEffectVolume;
            bossAttackInst.Volume = soundEffectVolume; 
            bossPainInst.Volume = soundEffectVolume;
            bruHurtInst.Volume = soundEffectVolume;
            cooHurtInst.Volume = soundEffectVolume;
            nessieHurtInst.Volume = soundEffectVolume;
            tamHurtInst.Volume = soundEffectVolume;
            thistleHurtInst.Volume = soundEffectVolume;

            //switches between the different screens for update
            switch (currentGameState)
            {
                //the first screen you see when you load the game 
                case Gamestate.pressStartMenu:
                    updatePressStartMenu(gameTime);
                    break;
                //the main menu screen, options, new game, achievments, exit
                case Gamestate.mainMenu:
                    updateMainMenu(gameTime);
                    break;
                //options menu in the main screen
                case Gamestate.optionsMenu:
                    updateOptionsMenu(gameTime);
                    break;
                //the character selection screen
                case Gamestate.characterSelection:
                    updateCharacterSelection(gameTime);
                    break;
                //achievements selection
                case Gamestate.achievMenu:
                    updateAchieveMenu(gameTime);
                    break;
                //inGame screen
                case Gamestate.inGame:
                    updateInGame(gameTime);
                    break;
                //level 2 screen
                case Gamestate.inGameLevel2:
                    updateLevel2(gameTime);
                    break;
                //highscore screen
                case Gamestate.highScoreMenu:
                    keys = updateHighscore(keys, timebetweenupdates, gameTime);
                    break;
                //controller menu
                case Gamestate.controllerMenu:
                    updateControllerMenu(gameTime);
                    break;
            }

            //player 1 controller
            prevButton = currentButton;
            //player 2 controller
            prevButton2 = currentButton2;

            lastkeystate = keys; // Read keyboard

            base.Update(gameTime);
        }

        private void updateControllerMenu(GameTime gameTime)
        {
            //bagpipe player animation 
            bagpipePlayer.Update(gameTime.ElapsedGameTime.Milliseconds);

            if (currentButton.IsButtonDown(Buttons.B) && !prevButton.IsButtonDown(Buttons.B)) currentGameState = Gamestate.characterSelection;

            //moves onto the ingame screen 
            if (currentButton.IsButtonDown(Buttons.Start) && !prevButton.IsButtonDown(Buttons.Start))
            {
                inGameSpriteChange = 1;
                currentGameState = Gamestate.inGame;
                resetEverything(gameTime); //resets everything in the game so that its good to go again
            }

            //timer to make the start flash in and out of transparency
            if (shade >= 1) shadeSwitch = true;
            if (shade <= 0) shadeSwitch = false;
            if (shadeSwitch) shade -= 0.01f;
            if (!shadeSwitch) shade += 0.01f;
        }

        private void resetEverything(GameTime gameTime)
        {
            coinDelay = 0;
            menuMover = 1;
            gunCoolDown = 0;
            gunCoolDown2 = 0;
            CRMon = true;
            volumeMover = 0;
            mainMenuCounter = 0;
            bulletNumber = 9;
            energyNumber = 9;
            coinsCollected = 0;
            bulletNumber2 = 9;
            energyNumber2 = 9;
            coinsCollected2 = 0;
            p1Score = 0;
            p2Score = 0;
            bossHealth = 24;
            isPlayerDead = false;
            isPlayerShootingRight = false;
            isPlayerAttacking = false;
            isPlayerShootingRight2 = false;
            isPlayerAttacking2 = false;
            isPlayerDead2 = false;
            flySwitch = false;
            flySwitch2 = false;
            playerUp = 0;
            playerUp2 = 0;
            flyingGunCoolDown = 0;
            flyingGunCoolDown2 = 0;
            talkingReset = 0;
            talkingReset2 = 0;
            stage2Counter = 30000;
            endGameCounter = 5000;
            stage1 = true;
            stage2 = false;
            playerHit = false;
            playerHit2 = false;
            exploStart = false;
            roseCrackStop = false;
            attackedRose = false;
            bossKilled = false;
            midgeSoundOnce = true;
            coinsCanBeAdded = false;
            bossDeathSoundOnce = true;

            shade = 1;
            spaceShade = 0;
            textShade = 1;
            shadeSwitch = false;
            textSwitch = false;

            p1.Initialize(0, 0, Content);
            p1.collisionArea.Width = 93;
            p2.Initialize(0, 0, Content);
            p2.collisionArea.Width = 93;
            p1Flying.Initialize(0, 500, Content);
            p1Flying.collisionArea.Width = 93;
            p2Flying.Initialize(0, 500, Content);
            p2Flying.collisionArea.Width = 93;
            boss.Initialize(1920, 140, Content);
            boss.collisionArea.Width = 93;
            backGround.scrollAmount.Y = 1670;
            midGround.scrollAmount.Y = 1890;
            monster.position = new Vector2(-1000, -1000);
            monster2.position = new Vector2(-1000, -1000);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            spacePosition = new Vector2(0, -1080);
            shortbreadTextPosition = new Vector2(0, -500);
            gameCamera = new Camera(new Vector2(0, screenHeight / 2), new Rectangle(0, 0, screenWidth, screenHeight));

            bossHud.position = new Vector2(650, 1080);
            rose1.position = new Vector2(19200, 800);
            rose2.position = new Vector2(2100, 300);
            roseCrack.position = new Vector2(-21000, 620);
            explo.position = new Vector2(-20950, -2000);
            flyingCoinReset();
            charHuds.position = new Vector2(150, 50);
            charHuds2.position = new Vector2(1525, 50);
            crm.position = new Vector2(-20900, 680);
            redHealth3.position = new Vector2(850, 1175);
            playerBullet.position = new Vector2(-10000, -10000);
            player2Bullet.position = new Vector2(-100, -100);
            s1Positions();
            s2Positions();
            s3Positions();
            s4Positions();
            coinPositions();
            bulletCollectPositions();
            energyCollectPositions();
            fallBetweenWavesPositions();
            irnBruWavePositions();
            enemyPositions();
            speech.currentAnimation = 0;

            boss.ySwitch = false;
            boss.xSwitch = false;
            boss.bossIdle = true;
            boss.bossAttack = false;
            boss.bossMelee = false;
            boss.bossGun = false;

            foreach (Enemy enem in enemies) enem.enemyHealth = 2;

            p1.playerHealth = 24;
            p1Flying.playerFlyingHealth = 24;
            p2.playerHealth2 = 24;
            p2Flying.playerFlyingHealth2 = 24;
            p1.animations[p1.currentAnimation].currentAnimationFrame = 0;
            p2.animations[p2.currentAnimation].currentAnimationFrame = 0;
            flyingEnemies.Clear();
            bossBullet.Clear();
            flyingBullets.Clear();

            //background music
            MediaPlayer.Play(testMusic);
            //allows the music to repeat
            MediaPlayer.IsRepeating = true;

            roseCrack.currentAnimation = 0;
            crm.animations[crm.currentAnimation].currentAnimationFrame = 0;
            roseCrack.animations[crm.currentAnimation].currentAnimationFrame = 0;
            explo.animations[explo.currentAnimation].currentAnimationFrame = 0;
        }

        private KeyboardState updateHighscore(KeyboardState keys, float timebetweenupdates, GameTime gameTime)
        {
            
            // Game is over
            if (p1Score + p2Score > highscores[lasthighscore])
            {
                keycounter -= timebetweenupdates; // Counter to delay between keys of the same value being entered
                if (keyboardreleased)
                {
                    if (keys.IsKeyDown(Keys.Back) && highscorenames[lasthighscore].Length > 0)
                    {
                        highscorenames[lasthighscore] = highscorenames[lasthighscore].Substring(0, highscorenames[lasthighscore].Length - 1);
                    }
                    else
                    {
                        char nextchar = sfunctions2d.getnextkey();
                        char lastchar = '!';
                        if (highscorenames[lasthighscore].Length > 0)
                            lastchar = Convert.ToChar(highscorenames[lasthighscore].Substring(highscorenames[lasthighscore].Length - 1, 1));

                        if (nextchar != '!' && (nextchar != lastchar || keycounter < 0))
                        {
                            keycounter = keystrokedelay;
                            highscorenames[lasthighscore] += nextchar;
                            if (highscorenames[lasthighscore].Length > maxnamelength)
                                highscorenames[lasthighscore] = highscorenames[lasthighscore].Substring(0, maxnamelength);
                        }
                    }
                }

            }

            // Allow game to return to the main menu
            if (currentButton.IsButtonDown(Buttons.B) && !prevButton.IsButtonDown(Buttons.B) || keys.IsKeyDown(Keys.Enter))
            {
                if (p1Score + p2Score > highscores[lasthighscore])
                {
                    highscores[lasthighscore] = p1Score + p2Score;
                }

                // Sort the high score table
                Array.Sort(highscores, highscorenames);
                Array.Reverse(highscores);
                Array.Reverse(highscorenames);

                currentGameState = Gamestate.mainMenu;
            }

            //bagpipe animation
            bagpipePlayer.Update(gameTime.ElapsedGameTime.Milliseconds);

            //bagpipe player movement
            bagpipePlayer.velocity.X = 0.5f;
            //bagpipe player reaches the edge of the screen reset him
            if (bagpipePlayer.position.X > screenWidth)
            {
                bagpipePlayer.position.X = -80;
                bagpipePlayer.position.Y = 500;
            }
            //bagpipe player stays ontop of the black outline
            if (bagpipePlayer.position.X > screenWidth / 2) bagpipePlayer.position.Y += 0.01f;

            return keys;
        }

        private void updateLevel2(GameTime gameTime)
        {
            //brings the boss hud onto the screen when the boss comes onto the screen
            if (boss.position.X < 1600)
            {
                if (bossHud.position.Y > 870)
                {
                    bossHud.position.Y -= 5;
                    redHealth3.position.Y -= 5;
                    
                }
            }

            foreach (GameSprite bossH in bossHealths)
            {
                bossH.position.Y -= 5;
            }

            //rose part 2
            rose2.Update(gameTime.ElapsedGameTime.Milliseconds);
            rose2.position.X -= 2;
            if (rose2.position.X < -100 && rose2.position.Y > 0) rose2.position = new Vector2(2100, rng.Next(100, 900));
            if (rose2.collision(p1Flying) || rose2.collision(p2Flying) && player2)
            {
                rose2.position = new Vector2(0, -500);
                coinCollectedSound.Play(soundEffectVolume, 0, 0);
            }

            //moves onto the highscore screen if the player dies 
            //moves to the highscore menu if only player 1
            if (p1Flying.playerFlyingHealth <= 0 && !player2)
            {
                if (p1Flying.position.Y > 1500)
                {
                    currentGameState = Gamestate.highScoreMenu;
                    if (p1Score + p2Score > highscores[lasthighscore])
                        highscorenames[lasthighscore] = "";
                    inGameSpriteChange = 0;
                    timesPlayerDied += 1;                  
                }
            }           

            //moves to the highscore menu with both players
            if (p1Flying.playerFlyingHealth <= 0 && p2Flying.playerFlyingHealth2 <= 0)
            {
                if (p1Flying.position.Y > 1500 && p2Flying.position.Y > 1500)
                {
                    currentGameState = Gamestate.highScoreMenu;
                    if (p1Score + p2Score > highscores[lasthighscore])
                        highscorenames[lasthighscore] = "";
                    inGameSpriteChange = 0;
                    timesPlayerDied += 2;
                }
            }

            //boss being killed
            if (boss.position.Y > 1500)
            {
                flySwitch = false; //brings the player back to normal mode away from the flying mode
                flySwitch2 = false;
                p1.position = new Vector2(-22500, -100);
                if (player2) p2.position = new Vector2(-22500, -100);
                isPlayerDead = false;
                isPlayerDead2 = false;
                p1.playerHealth = 24;
                p2.playerHealth2 = 24;
                bossKilled = true;
                currentGameState = Gamestate.inGame;
                timesBossHasDied += 1;
            }

            //resets the flying coin also the collision where it adds on to the players total collection
            flyingCoin.Update(gameTime.ElapsedGameTime.Milliseconds);
            flyingCoin.position.X -= 3;
            if (flyingCoin.position.X < -50) flyingCoinReset();
            if (p1Flying.collision(flyingCoin))
            {
                flyingCoinReset();
                coinsCollected += 1;
                coinCollectedSound.Play(soundEffectVolume, 0, 0); //coin sound effect
            }

            //coin position the flashing one
            coin.position = new Vector2(screenWidth / 2 - 780, 10);
            coin.Update(gameTime.ElapsedGameTime.Milliseconds);
            //coin position the flashing one
            coin2.position = new Vector2(screenWidth / 2 + 730, 10);
            coin2.Update(gameTime.ElapsedGameTime.Milliseconds);

            //the position of the font that keeps count of how many coins the player has collected 
            coinCollectedPostion = new Vector2(screenWidth / 2 - 740, 10);
            if (coinsCollected2 < 10) coinCollectedPostion2 = new Vector2(screenWidth / 2 + 650, 10); //keeps player2 coin count in the right position 
            if (coinsCollected2 >= 10 && coinsCollected2 < 100) coinCollectedPostion2 = new Vector2(screenWidth / 2 + 600, 10);
            if (coinsCollected2 >= 100 && coinsCollected2 < 1000) coinCollectedPostion2 = new Vector2(screenWidth / 2 + 550, 10);

            //players ui
            p1Ui.position = new Vector2(0, 0);

            //player2 ui
            if (player2) p2Ui.position = new Vector2(0, 0);

            //player heads
            playerHeadPos();

            //red health for the player ui
            redHealth.position = new Vector2(screenWidth / 2 - 770, 95);
            redHealth2.position = new Vector2(screenWidth / 2 + 400, 95);

            //player health
            EverythingToDoWithPlayerHealth();


            if (stage2Counter < -12500)
            {
                //animation update
                foreach (GameSprite s1 in shower1)
                {
                    s1.Update(gameTime.ElapsedGameTime.Milliseconds);
                    s1.position.X -= 5;

                    //collision between the player and shortbread
                    if (s1.collision(p1Flying))
                    {
                        s1.position = new Vector2(0, -2000);
                        p1Flying.playerFlyingHealth -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                    if (s1.collision(p2Flying) && player2)
                    {
                        s1.position = new Vector2(0, -2000);
                        p2Flying.playerFlyingHealth2 -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                }
                foreach (GameSprite s2 in shower2)
                {
                    s2.Update(gameTime.ElapsedGameTime.Milliseconds);
                    s2.position.X -= 5;

                    if (s2.collision(p1Flying))
                    {
                        s2.position = new Vector2(0, -2000);
                        p1Flying.playerFlyingHealth -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                    if (s2.collision(p2Flying) && player2)
                    {
                        s2.position = new Vector2(0, -2000);
                        p2Flying.playerFlyingHealth2 -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                }
                foreach (GameSprite s3 in shower3)
                {
                    s3.Update(gameTime.ElapsedGameTime.Milliseconds);
                    s3.position.X -= 5;

                    if (s3.collision(p1Flying))
                    {
                        s3.position = new Vector2(0, -2000);
                        p1Flying.playerFlyingHealth -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                    if (s3.collision(p2Flying) && player2)
                    {
                        s3.position = new Vector2(0, -2000);
                        p2Flying.playerFlyingHealth2 -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                }
                foreach (GameSprite s4 in shower4)
                {
                    s4.Update(gameTime.ElapsedGameTime.Milliseconds);
                    s4.position.X -= 5;

                    if (s4.collision(p1Flying))
                    {
                        s4.position = new Vector2(0, -2000);
                        p1Flying.playerFlyingHealth -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                    if (s4.collision(p2Flying) && player2)
                    {
                        s4.position = new Vector2(0, -2000);
                        p2Flying.playerFlyingHealth2 -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }
                }
            }

            //the chance of an enemy spawning into the game 
            float spawnChance = 10f;

            //spawns in enemies 
            if (rng.Next(0, 1000) < spawnChance && stage1 == true)
            {
                flyingEnemies.Add(new GameSprite());

                foreach (GameSprite flyingEnem in flyingEnemies)
                {
                    if (flyingEnem.position.X < 2101 && flyingEnem.position.X > 0) flyingEnem.position.X -= 10;
                    else
                        flyingEnem.position = new Vector2(2100, rng.Next(100, 900));

                }
            }



            //enemy colliding with the player
            foreach (GameSprite flyingEnem in flyingEnemies)
            {
                flyingEnem.Update(gameTime.ElapsedGameTime.Milliseconds); //flying animation
                if (flyingEnem.collision(p1Flying))
                {
                    flyingEnem.position = new Vector2(-100, -1000);
                    //hurt sound plays for each player 
                    if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                }
                if (flyingEnem.collision(p2Flying) && player2)
                {
                    flyingEnem.position = new Vector2(-100, -1000);
                    //hurt sound plays for each player 
                    if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                }
            }



            //background scroll
            backGround.scrollAmount.X -= 3;
            backGround.ParallaxRation = 0.9f;

            //allows the player to fire 
            flyingGunCoolDown -= gameTime.ElapsedGameTime.Milliseconds;




            //player 1 flying animation 
            p1Flying.Update(gameTime.ElapsedGameTime.Milliseconds, characterChoice);

            if (p1Flying.playerFlyingHealth > 0)
            {
                //keeps the player on the screen 
                if (p1Flying.position.X < 0) p1Flying.velocity.X += 1.5f;
                if (p1Flying.position.X > 700) p1Flying.velocity.X -= 1;
                if (p1Flying.position.X > 960) p1Flying.velocity.X = -1.5f;
                if (p1Flying.position.Y < 0) p1Flying.velocity.Y += 1.5f;
                if (p1Flying.position.Y > 950) p1Flying.velocity.Y -= 1.5f;
            }

            //adds player 1 bullets to the game 
            if (currentButton.IsButtonDown(Buttons.B) && p1Flying.animations[p1Flying.currentAnimation].currentAnimationFrame == 5 && flyingGunCoolDown < 0 && p1Flying.flipHorizontally == false)
            {
                flyingBullets.Add(new GameSprite());

                foreach (GameSprite fly in flyingBullets)
                {
                    if (fly.position.X > p1Flying.position.X) fly.position.X += 10;
                    else
                        fly.position = new Vector2(p1Flying.position.X + 100, p1Flying.position.Y + 65);


                }

                flyingGunCoolDown = flyingMaxGunCoolDown; //resets the timer
            }

            //everything to do with the player 1 bullets, positions, collsion 
            for (int c = 0; c < flyingBullets.Count; c++)
            {
                flyingBullets[c].AddAnimation(lasorImage, 1, 1);
                flyingBullets[c].position.X += 10;

                if (flyingBullets[c].position.X > 3000)
                {
                    flyingBullets.RemoveAt(c);
                }


            }



            //everything to do with the enemies. Removes them when the get to the edge of the screen
            for (int counter = 0; counter < flyingEnemies.Count; counter++)
            {
                flyingEnemies[counter].AddAnimation(flyingEnemiesImage, 40, 1000);
                flyingEnemies[counter].position.X -= 5;

                //enemies go towards the player 
                if (flyingEnemies[counter].position.X < p1Flying.position.X + 500 && flyingEnemies[counter].position.X > p1Flying.position.X)
                {
                    if (p1Flying.position.Y < 1200) //enemies go towards the player
                    {
                        if (flyingEnemies[counter].position.Y > p1Flying.position.Y) flyingEnemies[counter].position.Y -= 2;
                        if (flyingEnemies[counter].position.Y < p1Flying.position.Y) flyingEnemies[counter].position.Y += 2;
                    }

                    if (flyingEnemies[counter].collision(p1Flying))
                    {
                        p1Flying.playerFlyingHealth -= 2; //takes away player health if an anemy collides with them
                    }
                }

                //enemies go towards the player 
                if (flyingEnemies[counter].position.X < p2Flying.position.X + 500 && flyingEnemies[counter].position.X > p2Flying.position.X && player2)
                {
                    if (p2Flying.position.Y < 1200) //enemies go towards the player
                    {
                        if (flyingEnemies[counter].position.Y > p2Flying.position.Y) flyingEnemies[counter].position.Y -= 2;
                        if (flyingEnemies[counter].position.Y < p2Flying.position.Y) flyingEnemies[counter].position.Y += 2;
                    }

                    if (flyingEnemies[counter].collision(p2Flying))
                    {
                        p2Flying.playerFlyingHealth2 -= 2; //takes away player health if an anemy collides with them
                    }
                }

                //removes an enemy 
                if (flyingEnemies[counter].position.X < -300)
                {
                    flyingEnemies.RemoveAt(counter);
                }




            }

            //collsion between the enemies and the player bullets 
            foreach (GameSprite fly in flyingBullets)
            {
                foreach (GameSprite flyingEnem in flyingEnemies)
                {
                    if (fly.collision(flyingEnem))
                    {
                        fly.position = new Vector2(0, 2000);
                        flyingEnem.position = new Vector2(-300, -100);
                        p1Score += 50;
                        if (player2) p2Score += 50;
                        totalEnemiesKilled += 1;
                    }
                }
            }

            //position of the background for the score
            parchment.position = new Vector2(768, 0);

            //p1 score if only player 1 is playing 
            if (!player2)
            {
                if (p1Score < 10) p1ScoreFlyingPosition = new Vector2(screenWidth / 2 - 25, 60);
                if (p1Score >= 10 && p1Score < 100) p1ScoreFlyingPosition = new Vector2(screenWidth / 2 - 45, 60);
                if (p1Score >= 100 && p1Score < 1000) p1ScoreFlyingPosition = new Vector2(screenWidth / 2 - 65, 60);
                if (p1Score >= 1000 && p1Score < 10000) p1ScoreFlyingPosition = new Vector2(screenWidth / 2 - 85, 60);
                if (p1Score >= 10000 && p1Score < 100000) p1ScoreFlyingPosition = new Vector2(screenWidth / 2 - 105, 60);
            }

            //position of score with both players 
            if (player2)
            {
                p1ScoreFlyingPosition = new Vector2(screenWidth / 2 - 170, 40);

                if (p2Score < 10) p2ScoreFlyingPosition = new Vector2(screenWidth / 2 + 120, 80);
                if (p2Score >= 10 && p2Score < 100) p2ScoreFlyingPosition = new Vector2(screenWidth / 2 + 85, 80);
                if (p2Score >= 100 && p2Score < 1000) p2ScoreFlyingPosition = new Vector2(screenWidth / 2 + 45, 80);
                if (p2Score >= 1000 && p2Score < 10000) p2ScoreFlyingPosition = new Vector2(screenWidth / 2 + 5, 80);
                if (p2Score >= 10000 && p2Score < 100000) p2ScoreFlyingPosition = new Vector2(screenWidth / 2 - 35, 80);
            }

            //counts down before stage 2 happens 
            stage2Counter -= gameTime.ElapsedGameTime.Milliseconds;

            //ends stage 1 of level 2
            if (stage2Counter < 0) stage1 = false;

            if (stage2Counter < -5000) stage2 = true;



            if (stage2)
            {
                //takes the background down to reveal the space background 
                backGround.scrollAmount.Y += 10;

                //brings the space background down 
                if (spacePosition.Y >= 0)
                {
                    spacePosition.Y -= 0;
                }
                else
                    spacePosition.Y += 10;

                //changes the transparency of the backgorund 
                spaceShade += 0.01f;

                //brings up the warning text 
                if (stage2Counter < -8000 && stage2Counter > -12500)
                {
                    shortbreadTextPosition = new Vector2(400, 300);

                    //warning speech 
                    warningInst.Play();

                    //timer to make the text flash in and out of transparency
                    if (textShade >= 1) textSwitch = true;
                    if (textShade <= 0) textSwitch = false;
                    if (textSwitch) textShade -= 0.01f;
                    if (!textSwitch) textShade += 0.01f;
                }
            }

            //when the boss appears
            if (stage2Counter < -40000)
            {

                //boss animation
                boss.Update(gameTime.ElapsedGameTime.Milliseconds, bossHealth, playerHit, playerHit2);


                if (boss.position.X > p1Flying.position.X - 10) //stops the player losing lots of health if the player gets infront of the boss
                {

                    //boss collision with the palyer when he is meleeing
                    if (boss.collision(p1Flying))
                    {
                        playerHit = true;
                        //hurt sound plays for each player 
                        if (characterChoice == 0) bruHurtInst.Play();
                        if (characterChoice == 1) cooHurtInst.Play();
                        if (characterChoice == 2) nessieHurtInst.Play();
                        if (characterChoice == 3) tamHurtInst.Play();
                        if (characterChoice == 4) thistleHurtInst.Play();
                        if (boss.position.X > p1Flying.position.X) playerHit = false;
                        if (playerHit)
                            p1Flying.playerFlyingHealth -= 2;
                    }


                }

                if (boss.position.X > p2Flying.position.X - 10) //stops the player losing lots of health if the player gets infront of the boss
                {
                    if (player2)
                    {
                        if (boss.collision(p2Flying))
                        {
                            playerHit2 = true;
                            //hurt sound plays for each player 
                            if (characterChoice2 == 0) bruHurtInst.Play();
                            if (characterChoice2 == 1) cooHurtInst.Play();
                            if (characterChoice2 == 2) nessieHurtInst.Play();
                            if (characterChoice2 == 3) tamHurtInst.Play();
                            if (characterChoice2 == 4) thistleHurtInst.Play();
                            if (boss.position.X > p2Flying.position.X) playerHit2 = false;
                            if (playerHit2)
                                p2Flying.playerFlyingHealth2 -= 2;
                        }
                    }
                }

                //if the boss does a bullet attack
                if (boss.animations[boss.currentAnimation].currentAnimationFrame == 12 && boss.currentAnimation == 2)
                {
                    bossBullet.Add(new GameSprite()); //add a bullet
                    bossAttackInst.Play(); //sound effect for the boss bullet 

                    foreach (GameSprite bossB in bossBullet)
                    {
                        if (bossB.position.X < boss.position.X + 1 && bossB.position.X > 0) bossB.position.X -= 10;
                        else
                            bossB.position = new Vector2(boss.position.X, boss.position.Y + 150);



                    }
                }

                for (int counter = 0; counter < bossBullet.Count; counter++)
                {
                    bossBullet[counter].AddAnimation(bossBulletImage, 1, 1);
                    bossBullet[counter].position.X -= 10;

                    if (bossBullet[counter].collision(p1Flying)) //boss bullet hitting the player
                    {
                        p1Flying.playerFlyingHealth -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }

                    if (bossBullet[counter].collision(p2Flying) && player2) //boss bullet hitting the player
                    {
                        p2Flying.playerFlyingHealth2 -= 2;
                        //hurt sound plays for each player 
                        if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);
                    }

                    if (bossBullet[counter].position.X < 0 || bossBullet[counter].collision(p1Flying) || bossBullet[counter].collision(p2Flying))
                    {
                        bossBullet.RemoveAt(counter); //reaches the edge of the screen
                        counter--;
                    }


                }

                for (int c = 0; c < flyingBullets.Count; c++)
                {
                    if (flyingBullets[c].collision(boss)) //player bullets colliding with the boss
                    {
                        bossHealth -= 1;
                        flyingBullets.RemoveAt(c);
                        c--;
                        if (bossHealth >= 1) bossPainInst.Play(); //boss has been hit by a player bullet                        
                    }
                }

                if (boss.position.X <= 1498 && boss.position.X >= 1400 && boss.xSwitch == false) bossMeleeInst.Play(); //boss melee sound               
            }

            if (bossHealth < 2 && boss.position.Y > 700 && bossDeathSoundOnce) //when the boss dies 
            {
                bossDeathInst.Play();
                bossDeathSoundOnce = false;
            }

            if (player2)
            {
                //player 2 animation 
                p2Flying.Update(gameTime.ElapsedGameTime.Milliseconds, characterChoice2);

                //allows the player to fire at a steady rate 
                flyingGunCoolDown2 -= gameTime.ElapsedGameTime.Milliseconds;

                //if player 2 collects a flying coin 
                if (p2Flying.collision(flyingCoin))
                {
                    flyingCoinReset();
                    coinsCollected2 += 1;
                    coinCollectedSound.Play(soundEffectVolume, 0, 0); //coin sound effect
                }

                //keeps player 2 on the screen
                if (p2Flying.playerFlyingHealth2 > 0)
                {
                    if (p2Flying.position.X < 0) p2Flying.velocity.X += 1.5f;
                    if (p2Flying.position.X > 700) p2Flying.velocity.X -= 1;
                    if (p2Flying.position.X > 960) p2Flying.velocity.X = -1.5f;
                    if (p2Flying.position.Y < 0) p2Flying.velocity.Y += 1.5f;
                    if (p2Flying.position.Y > 950) p2Flying.velocity.Y -= 1.5f;
                }

                //player 2 bullets              
                if (currentButton2.IsButtonDown(Buttons.B) && p2Flying.animations[p2Flying.currentAnimation].currentAnimationFrame == 5 && flyingGunCoolDown2 < 0 && p2Flying.flipHorizontally == false)
                {
                    flyingBullets.Add(new GameSprite());

                    foreach (GameSprite fly in flyingBullets)
                    {
                        if (fly.position.X > p2Flying.position.X) fly.position.X += 10;
                        else
                            fly.position = new Vector2(p2Flying.position.X + 100, p2Flying.position.Y + 65);
                    }
                    flyingGunCoolDown2 = flyingMaxGunCoolDown2; //resets the timer
                }



            }
        }

        private void playerHeadPos()
        {
            //the place the heads get displayed for player 1           
            if (characterChoice == 0) //bru
            {
                p1Heads.position = new Vector2(screenWidth / 2 - 940, 40);

                p1Heads.currentAnimation = 0;
            }

            if (characterChoice == 1) //coo
            {
                p1Heads.currentAnimation = 1;

                p1Heads.position = new Vector2(screenWidth / 2 - 920, 0);

            }

            if (characterChoice == 2) //nessie
            {
                p1Heads.currentAnimation = 2;
                p1Heads.position = new Vector2(screenWidth / 2 - 940, 25);

            }

            if (characterChoice == 3) //tam
            {
                p1Heads.currentAnimation = 3;
                p1Heads.position = new Vector2(screenWidth / 2 - 925, 10);

            }

            if (characterChoice == 4) //thistle
            {
                p1Heads.currentAnimation = 4;
                p1Heads.position = new Vector2(screenWidth / 2 - 915, 5);

            }
            //the place the heads get displayed for player 2
            if (characterChoice2 == 0) //bru
            {

                p2Heads.position = new Vector2(screenWidth / 2 + 790, 40);

                p2Heads.currentAnimation = 0;
            }

            if (characterChoice2 == 1) //coo
            {
                p2Heads.currentAnimation = 1;

                p2Heads.position = new Vector2(screenWidth / 2 + 800, 0);
            }

            if (characterChoice2 == 2) //nessie
            {
                p2Heads.currentAnimation = 2;

                p2Heads.position = new Vector2(screenWidth / 2 + 790, 25);
            }

            if (characterChoice2 == 3) //tam
            {
                p2Heads.currentAnimation = 3;

                p2Heads.position = new Vector2(screenWidth / 2 + 810, 10);
            }

            if (characterChoice2 == 4) //thistle
            {
                p2Heads.currentAnimation = 4;

                p2Heads.position = new Vector2(screenWidth / 2 + 810, 5);
            }
        }

        private void updateInGame(GameTime gameTime)
        {
            roseCrack.Update(gameTime.ElapsedGameTime.Milliseconds);

            //player jump counter for the achievement 
            if (currentButton.IsButtonDown(Buttons.A) && !prevButton.IsButtonDown(Buttons.A) && p1.onGround == true)
            {
                timesPlayerJumps += 1;
            }
            if (currentButton2.IsButtonDown(Buttons.A) && !prevButton2.IsButtonDown(Buttons.A) && p2.onGround == true && player2)
            {
                timesPlayerJumps += 1;
            }

            //rose piece part 1 
            rose1.Update(gameTime.ElapsedGameTime.Milliseconds);
            if (p1.collision(rose1) || rose1.collision(p2) && player2)
            {
                rose1.position = new Vector2(0, -500);
                coinCollectedSound.Play(soundEffectVolume, 0, 0);
            }

            //stops the player being able to fly back after they have jumped off of the edge and changed to riding the duck
            if (p1.position.X < 19000 && flySwitch)
            {
                p1.velocity.X += 10;
            }
            if (p2.position.X < 19000 && flySwitch2)
            {
                p2.velocity.X += 10;
            }

            //resets the player if they jump off at the start of the game 
            if (p1.position.X < 500 && p1.position.X > -2000 && p1.position.Y > 1200)
            {
                p1.position = new Vector2(0, 0);
            }
            if (p2.position.X < 500 && p2.position.X > -2000 && p2.position.Y > 1200)
            {
                p2.position = new Vector2(0, 0);
            }

            //resets the player if the jump off the screen at the rose ending 
            if (p1.position.X < -10000 && p1.position.Y > 1200)
            {
                p1.position = new Vector2(-22500, 0);
            }
            if (p2.position.X < -10000 && p2.position.Y > 1200)
            {
                p2.position = new Vector2(-22500, 0);
            }

            //stops both player being able to collect too much energy or bullets 
            if (energyNumber > 9) energyNumber = 9;
            if (bulletNumber > 9) bulletNumber = 9;
            if (energyNumber2 > 9) energyNumber2 = 9;
            if (bulletNumber2 > 9) bulletNumber2 = 9;

            //if the player attacks the rose
            if (playerBullet.collision(roseCrack) || roseCrack.collision(p1) && isPlayerAttacking == true || player2Bullet.collision(roseCrack) || roseCrack.collision(p2) && isPlayerAttacking2 == true)
            {
                attackedRose = true;
                playerBullet.position = new Vector2(0, -200);
                player2Bullet.position = new Vector2(0, -200);
            }

            if (attackedRose)
            {
                if (roseCrackStop == false)
                {
                    roseCrack.currentAnimation = 1;
                    exploInst.Play(); //explosion sound effect being played 
                }
            }
            if (roseCrack.animations[roseCrack.currentAnimation].currentAnimationFrame == 16 && roseCrack.currentAnimation == 1)
            {
                roseCrack.position = new Vector2(0, -2000);
                exploStart = true;
            }
            if (exploStart)
            {
                explo.position = new Vector2(-20950, 650);
                explo.Update(gameTime.ElapsedGameTime.Milliseconds);
                if (CRMon)
                {
                    timesCRMSaved += 1;
                    CRMon = false;
                }
            }
            if (explo.animations[explo.currentAnimation].currentAnimationFrame == 37)
            {
                exploStart = false;
                roseCrackStop = true;
                explo.position = new Vector2(-20950, -6500);
                crm.Update(gameTime.ElapsedGameTime.Milliseconds);
                guitarInst.Play(); //guitar music
                MediaPlayer.Stop();

                if (roseCrackStop == true)
                    roseCrack.animations[roseCrack.currentAnimation].currentAnimationFrame = 7;
            }

            if (coinsCanBeAdded)
            {
                coinDelay += gameTime.ElapsedGameTime.Milliseconds;

                if (coinDelay >= 3000)
                {
                    if (coinsCollected > 0) //adds the coins that have been collected throughout the game to the plauyers score
                    {
                        coinsCollected -= 1;
                        p1Score += 10;
                        coinCountInst.Play(); //coins counting sound
                    }

                    //if player 2 is playing the game
                    if (player2)
                    {
                        if (coinsCollected2 > 0)
                        {
                            coinsCollected2 -= 1;
                            p2Score += 10;
                        }
                    }
                }
            }

            //ends the game and moves to the high score menu if player 1
            if (coinsCollected == 0 && !player2 && attackedRose)
            {
                endGameCounter -= gameTime.ElapsedGameTime.Milliseconds;
                if (endGameCounter < 0)
                {
                    currentGameState = Gamestate.highScoreMenu;
                    if (p1Score + p2Score > highscores[lasthighscore])
                        highscorenames[lasthighscore] = "";
                    inGameSpriteChange = 0;
                }
            }
            //ends the game and moves to the high score menu if player 1 and player 2
            if (coinsCollected == 0 && coinsCollected2 == 0 && player2 && attackedRose)
            {
                endGameCounter -= gameTime.ElapsedGameTime.Milliseconds;
                if (endGameCounter < 0)
                {
                    currentGameState = Gamestate.highScoreMenu;
                    if (p1Score + p2Score > highscores[lasthighscore])
                        highscorenames[lasthighscore] = "";
                    inGameSpriteChange = 0;
                }
            }

            if (crm.animations[crm.currentAnimation].currentAnimationFrame == 54) coinsCanBeAdded = true;

            //moves to the highscore menu if only player 1
            if (p1.playerHealth <= 0 && !player2 || p1Flying.playerFlyingHealth <= 0 && !player2)
            {
                if (isPlayerDead && p1.animations[p1.currentAnimation].currentAnimationFrame == 17)
                {
                    currentGameState = Gamestate.highScoreMenu;
                    if (p1Score + p2Score > highscores[lasthighscore])
                        highscorenames[lasthighscore] = "";
                    inGameSpriteChange = 0;
                    timesPlayerDied += 1;
                }
            }

            //moves to the highscore menu with both players
            if (p1.playerHealth <= 0 && p2.playerHealth2 <= 0 || p1Flying.playerFlyingHealth <= 0 && p2Flying.playerFlyingHealth2 <= 0)
            { 
                    currentGameState = Gamestate.highScoreMenu;
                    if (p1Score + p2Score > highscores[lasthighscore])
                        highscorenames[lasthighscore] = "";
                    inGameSpriteChange = 0;
                    timesPlayerDied += 2;                
            }

            talkingReset -= gameTime.ElapsedGameTime.Milliseconds;
            talkingReset2 -= gameTime.ElapsedGameTime.Milliseconds;

            //talk
            if (currentButton.IsButtonDown(Buttons.A) && !prevButton.IsButtonDown(Buttons.A))
            {
                if (characterChoice == 4 && p1.onGround) thistleJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 3 && p1.onGround) tamJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 2 && p1.onGround) nessieJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 1 && p1.onGround) cooJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 0 && p1.onGround) bruJump.Play(soundEffectVolume, 0, 0);
            }
            //talk
            if (currentButton2.IsButtonDown(Buttons.A) && !prevButton2.IsButtonDown(Buttons.A) && player2)
            {
                if (characterChoice2 == 4 && p1.onGround) thistleJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 3 && p1.onGround) tamJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 2 && p1.onGround) nessieJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 1 && p1.onGround) cooJump.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 0 && p1.onGround) bruJump.Play(soundEffectVolume, 0, 0);
            }

            //talk
            if (p1.animations[p1.currentAnimation].currentAnimationFrame == 4 && isPlayerAttacking == true)
            {
                if (characterChoice == 4) thistleMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 3) tamMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 2) nessieMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 1) cooMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice == 0) bruMelee.Play(soundEffectVolume, 0, 0);
            }
            //talk
            if (p2.animations[p2.currentAnimation].currentAnimationFrame == 4 && isPlayerAttacking2 == true && player2)
            {
                if (characterChoice2 == 4) thistleMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 3) tamMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 2) nessieMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 1) cooMelee.Play(soundEffectVolume, 0, 0);
                if (characterChoice2 == 0) bruMelee.Play(soundEffectVolume, 0, 0);
            }


            //talk
            if (p1.animations[p1.currentAnimation].currentAnimationFrame == 3 && isPlayerDead == true)
            {
                if (talkingReset < 0)
                {
                    if (characterChoice == 4) thistleDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 3) tamDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 2) nessieDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 1) cooDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 0) bruDeath.Play(soundEffectVolume, 0, 0);
                }
                talkingReset = MaxTalkingReset;
            }
            //talk
            if (p2.animations[p2.currentAnimation].currentAnimationFrame == 3 && isPlayerDead2 == true && player2)
            {
                if (talkingReset2 < 0)
                {
                    if (characterChoice2 == 4) thistleDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 3) tamDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 2) nessieDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 1) cooDeath.Play(soundEffectVolume, 0, 0);
                    if (characterChoice2 == 0) bruDeath.Play(soundEffectVolume, 0, 0);
                }
                talkingReset2 = MaxTalkingReset2;                
            }

            //keeps player 2 on the screen unless either player dies to which it doesnt 
            if (p2.position.X > p1.position.X + 860 && !isPlayerDead && !isPlayerDead2)
            {
                p2.velocity.X -= 5;
            }
            if (p2.position.X < p1.position.X - 910 && !isPlayerDead && !isPlayerDead2)
            {
                p2.velocity.X += 5;
            }

            if (p1.position.X > 22000 && !player2) currentGameState = Gamestate.inGameLevel2;
            if (player2 && p1.position.X > 22000 || isPlayerDead && p2.position.X > 22000) currentGameState = Gamestate.inGameLevel2;


            //background scroll
            backGround.scrollAmount.X -= 1;
            backGround.ParallaxRation = 0.9f;

            //villager animation 
            villager.Update(gameTime.ElapsedGameTime.Milliseconds);
            villager.position = new Vector2(1000, 780);
            villagerBig.position = new Vector2(p1.position.X - 1000, 500);

            //speech for the villager 
            speech.position = new Vector2(p1.position.X - 600, 400);

            //goes through the player speech
            if (p1.position.X > 800 && p1.position.X < 1200)
            {
                if (currentButton.IsButtonDown(Buttons.A) && !prevButton.IsButtonDown(Buttons.A)) speech.currentAnimation += 1;
            }

            //if just player 1 is playing 
            if (!player2)
            {
                if (p1Score < 10) p1ScorePosition = new Vector2(p1.position.X - 25, 60);
                if (p1Score >= 10 && p1Score < 100) p1ScorePosition = new Vector2(p1.position.X - 45, 60);
                if (p1Score >= 100 && p1Score < 1000) p1ScorePosition = new Vector2(p1.position.X - 65, 60);
                if (p1Score >= 1000 && p1Score < 10000) p1ScorePosition = new Vector2(p1.position.X - 85, 60);
                if (p1Score >= 10000 && p1Score < 100000) p1ScorePosition = new Vector2(p1.position.X - 105, 60);
            }

            //position of score with both players 
            if (!isPlayerDead && player2)
            {
                p1ScorePosition = new Vector2(p1.position.X - 170, 40);

                if (p2Score < 10) p2ScorePosition = new Vector2(p1.position.X + 120, 80);
                if (p2Score >= 10 && p2Score < 100) p2ScorePosition = new Vector2(p1.position.X + 85, 80);
                if (p2Score >= 100 && p2Score < 1000) p2ScorePosition = new Vector2(p1.position.X + 45, 80);
                if (p2Score >= 1000 && p2Score < 10000) p2ScorePosition = new Vector2(p1.position.X + 5, 80);
                if (p2Score >= 10000 && p2Score < 100000) p2ScorePosition = new Vector2(p1.position.X - 35, 80);
            }

            //position of score with both players when player 1 dies
            if (isPlayerDead && player2)
            {
                p1ScorePosition = new Vector2(p2.position.X - 170, 40);

                if (p2Score < 10) p2ScorePosition = new Vector2(p2.position.X + 120, 80);
                if (p2Score >= 10 && p2Score < 100) p2ScorePosition = new Vector2(p2.position.X + 85, 80);
                if (p2Score >= 100 && p2Score < 1000) p2ScorePosition = new Vector2(p2.position.X + 45, 80);
                if (p2Score >= 1000 && p2Score < 10000) p2ScorePosition = new Vector2(p2.position.X + 5, 80);
                if (p2Score >= 10000 && p2Score < 100000) p2ScorePosition = new Vector2(p2.position.X - 35, 80);
            }


            //player energy and bullets
            if (characterChoice == 0)
            {
                playersBulletsAndEnergy.position = new Vector2(p1.position.X + 10, p1.position.Y - 40);

                //postion of the amount of energy and bullets the player has left
                fontPosition = new Vector2(p1.position.X + 45, p1.position.Y - 39);
                fontPositionEnergy = new Vector2(p1.position.X + 110, p1.position.Y - 39);
            }

            if (characterChoice == 1)
            {
                playersBulletsAndEnergy.position = new Vector2(p1.position.X, p1.position.Y - 40);

                //postion of the amount of energy and bullets the player has left
                fontPosition = new Vector2(p1.position.X + 35, p1.position.Y - 39);
                fontPositionEnergy = new Vector2(p1.position.X + 100, p1.position.Y - 39);
            }

            if (characterChoice == 2)
            {
                playersBulletsAndEnergy.position = new Vector2(p1.position.X + 10, p1.position.Y - 40);

                //postion of the amount of energy and bullets the player has left
                fontPosition = new Vector2(p1.position.X + 45, p1.position.Y - 39);
                fontPositionEnergy = new Vector2(p1.position.X + 110, p1.position.Y - 39);
            }

            if (characterChoice == 3)
            {
                playersBulletsAndEnergy.position = new Vector2(p1.position.X, p1.position.Y - 40);

                //postion of the amount of energy and bullets the player has left
                fontPosition = new Vector2(p1.position.X + 35, p1.position.Y - 39);
                fontPositionEnergy = new Vector2(p1.position.X + 100, p1.position.Y - 39);
            }

            if (characterChoice == 4)
            {
                playersBulletsAndEnergy.position = new Vector2(p1.position.X, p1.position.Y - 40);

                //postion of the amount of energy and bullets the player has left
                fontPosition = new Vector2(p1.position.X + 35, p1.position.Y - 39);
                fontPositionEnergy = new Vector2(p1.position.X + 100, p1.position.Y - 39);
            }



            //player health
            EverythingToDoWithPlayerHealth();

            //p1 ui follows the player so that it stays on screen
            if (!isPlayerDead) p1Ui.position = new Vector2(p1.position.X - 960, p1.position.Y - p1.position.Y);
            if (isPlayerDead && player2) p1Ui.position = new Vector2(p2.position.X - 960, p2.position.Y - p2.position.Y);

            //red health so that it stays on screem
            if (!isPlayerDead)
            {
                redHealth.position = new Vector2(p1.position.X - 770, 95);
                redHealth2.position = new Vector2(p1.position.X + 380, 95);
            }
            if (isPlayerDead && player2)
            {
                redHealth.position = new Vector2(p2.position.X - 770, 95);
                redHealth2.position = new Vector2(p2.position.X + 380, 95);
            }

            //the place the heads get displayed for player 1           
            if (characterChoice == 0) //bru
            {
                if (!isPlayerDead) p1Heads.position = new Vector2(p1.position.X - 940, 40);
                if (isPlayerDead && player2) p1Heads.position = new Vector2(p2.position.X - 940, 40);
                p1Heads.currentAnimation = 0;
            }

            if (characterChoice == 1) //coo
            {
                p1Heads.currentAnimation = 1;

                if (!isPlayerDead) p1Heads.position = new Vector2(p1.position.X - 920, 0);
                if (isPlayerDead && player2) p1Heads.position = new Vector2(p2.position.X - 920, 0);
            }

            if (characterChoice == 2) //nessie
            {
                p1Heads.currentAnimation = 2;
                if (!isPlayerDead) p1Heads.position = new Vector2(p1.position.X - 940, 25);
                if (isPlayerDead && player2) p1Heads.position = new Vector2(p2.position.X - 940, 25);
            }

            if (characterChoice == 3) //tam
            {
                p1Heads.currentAnimation = 3;
                if (!isPlayerDead) p1Heads.position = new Vector2(p1.position.X - 925, 10);
                if (isPlayerDead && player2) p1Heads.position = new Vector2(p2.position.X - 925, 10);
            }

            if (characterChoice == 4) //thistle
            {
                p1Heads.currentAnimation = 4;
                if (!isPlayerDead) p1Heads.position = new Vector2(p1.position.X - 915, 5);
                if (isPlayerDead && player2) p1Heads.position = new Vector2(p2.position.X - 915, 5);
            }

            //player attacking 
            if (gunCoolDown <= 0 && bulletNumber > 0)//stops the player from firing constantly & means the player can fire a bullet
            {
                if (currentButton.IsButtonDown(Buttons.B) && p1.animations[p1.currentAnimation].currentAnimationFrame == 6)

                {
                    if (p1.currentAnimation == 4 || p1.currentAnimation == 10 || p1.currentAnimation == 16 || p1.currentAnimation == 22 || p1.currentAnimation == 28) // stops the player being able to fire bullets in mid air 
                    {
                        gunCoolDown = MaxGunCoolDown; //resets the cool down so the player doesnt constantly fire bullets




                        if (isPlayerShootingRight == true) //fires to the right
                        {
                            p1.animations[p1.currentAnimation].currentAnimationFrame = 0;
                            //where the bullet fires from if the player is facing right
                            playerBullet.position.X = p1.position.X + 150;
                            playerBullet.position.Y = p1.position.Y + 80;
                            bulletNumber -= 1;
                            shootInst.Play(); //player shooting sound effect
                            bulletsFired += 1; //total amount of bullets the player has fired for the achievment
                        }
                        if (isPlayerShootingRight == false) //fires to the left
                        {
                            //where the bullet fires from if the player is facing right
                            playerBullet.position.X = p1.position.X - 50;
                            playerBullet.position.Y = p1.position.Y + 80;
                            bulletNumber -= 1;
                            shootInst.Play(); //player shooting sound effect
                            bulletsFired += 1; //total amount of bullets the player has fired for the achievment
                        }
                    }
                }
            }





            gunCoolDown -= gameTime.ElapsedGameTime.Milliseconds;
            gunCoolDown2 -= gameTime.ElapsedGameTime.Milliseconds;

            if (currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) || //stick movement or dpad
                currentButton.IsButtonDown(Buttons.DPadLeft))
            {
                isPlayerShootingRight = false;
            }


            if (currentButton.IsButtonDown(Buttons.LeftThumbstickRight) || //stick movement or dpad
                 currentButton.IsButtonDown(Buttons.DPadRight))
            {
                isPlayerShootingRight = true;
            }

            //speed the player bullet moves at
            if (playerBullet.position.X > p1.position.X) playerBullet.position.X += 18;
            if (playerBullet.position.X < p1.position.X) playerBullet.position.X -= 18;



            //moves the bullet of screen
            if (playerBullet.position.X > p1.position.X + 1000 || playerBullet.position.X < p1.position.X - 1000)
            {
                playerBullet.position = new Vector2(-10000, -10000);
            }




            if (currentButton.IsButtonDown(Buttons.X) && energyNumber > 0 && p1.onGround == true) isPlayerAttacking = true;//player will remove enemies from the screen
            if (currentButton.IsButtonUp(Buttons.X) || energyNumber <= 0) isPlayerAttacking = false;//player will be thrown back by the enemies and take damage

            //coin positions that have been collected 
            if (!isPlayerDead)
            {
                coinCollectedPostion = new Vector2(p1.position.X - 740, 10);
                if (coinsCollected2 < 10) coinCollectedPostion2 = new Vector2(p1.position.X + 650, 10); //keeps player2 coin count in the right position 
                if (coinsCollected2 >= 10 && coinsCollected2 < 100) coinCollectedPostion2 = new Vector2(p1.position.X + 600, 10);
                if (coinsCollected2 >= 100 && coinsCollected2 < 1000) coinCollectedPostion2 = new Vector2(p1.position.X + 550, 10);
            }

            //position of the parchment depending on who is alive 
            if (!isPlayerDead) parchment.position = new Vector2(p1.position.X - 192, 0);
            if (isPlayerDead && player2) parchment.position = new Vector2(p2.position.X - 192, 0);

            //if player 2 is playing 
            if (player2)
            {
                //coins colected that have been collected and get added to the text beside the flashing coin  
                if (isPlayerDead)
                {
                    coinCollectedPostion = new Vector2(p2.position.X - 740, 10);
                    if (coinsCollected2 < 10) coinCollectedPostion2 = new Vector2(p2.position.X + 650, 10);
                    if (coinsCollected2 >= 10 && coinsCollected2 < 100) coinCollectedPostion2 = new Vector2(p2.position.X + 600, 10); //keeps player2 coin count in the right position
                    if (coinsCollected2 >= 100 && coinsCollected2 < 1000) coinCollectedPostion2 = new Vector2(p2.position.X + 550, 10);
                }

                //p2 ui follows the player so that it stays on screen
                if (!isPlayerDead) p2Ui.position = new Vector2(p1.position.X - 960, 0);
                if (isPlayerDead) p2Ui.position = new Vector2(p2.position.X - 960, 0);

                //the place the heads get displayed for player 2
                if (characterChoice2 == 0) //bru
                {
                    if (!isPlayerDead) p2Heads.position = new Vector2(p1.position.X + 790, 40);
                    if (isPlayerDead) p2Heads.position = new Vector2(p2.position.X + 790, 40);

                    p2Heads.currentAnimation = 0;
                }

                if (characterChoice2 == 1) //coo
                {
                    p2Heads.currentAnimation = 1;
                    if (!isPlayerDead) p2Heads.position = new Vector2(p1.position.X + 800, 0);
                    if (isPlayerDead) p2Heads.position = new Vector2(p2.position.X + 800, 0);
                }

                if (characterChoice2 == 2) //nessie
                {
                    p2Heads.currentAnimation = 2;
                    if (!isPlayerDead) p2Heads.position = new Vector2(p1.position.X + 790, 25);
                    if (isPlayerDead) p2Heads.position = new Vector2(p2.position.X + 790, 25);
                }

                if (characterChoice2 == 3) //tam
                {
                    p2Heads.currentAnimation = 3;
                    if (!isPlayerDead) p2Heads.position = new Vector2(p1.position.X + 810, 10);
                    if (isPlayerDead) p2Heads.position = new Vector2(p2.position.X + 810, 10);
                }

                if (characterChoice2 == 4) //thistle
                {
                    p2Heads.currentAnimation = 4;
                    if (!isPlayerDead) p2Heads.position = new Vector2(p1.position.X + 810, 5);
                    if (isPlayerDead) p2Heads.position = new Vector2(p2.position.X + 810, 5);
                }

                //stops the player looping through the death animation
                if (isPlayerDead2 && p2.animations[p2.currentAnimation].currentAnimationFrame == 17) p2.position.X += 0;
                else
                {
                    //player 2 into the game 
                    p2.Update(gameTime.ElapsedGameTime.Milliseconds, characterChoice2, isPlayerAttacking2, isPlayerShootingRight2, energyNumber2, flySwitch2);
                }

                //how fast the player falls back to the ground 
                p2.velocity.Y += 2.5f;

                //player 2 moving back to the edge of platforms ect.
                foreach (GameSprite trans in transPlatforms)
                {
                    if (trans.collision(p2))
                    {
                        p2.moveBackToEdge(trans);
                    }
                }



                p2Heads.flipHorizontally = true;



                //player 2 attacking 
                if (gunCoolDown2 <= 0 && bulletNumber2 > 0)//stops the player from firing constantly & means the player can fire a bullet
                {
                    if (currentButton2.IsButtonDown(Buttons.B) && p2.animations[p2.currentAnimation].currentAnimationFrame == 6)

                    {
                        if (p2.currentAnimation == 4 || p2.currentAnimation == 10 || p2.currentAnimation == 16 || p2.currentAnimation == 22 || p2.currentAnimation == 28) // stops the player being able to fire bullets in mid air 
                        {
                            gunCoolDown2 = MaxGunCoolDown2; //resets the cool down so the player doesnt constantly fire bullets

                            if (isPlayerShootingRight2 == true) //fires to the right
                            {
                                //where the bullet fires from if the player is facing right
                                player2Bullet.position.X = p2.position.X + 150;
                                player2Bullet.position.Y = p2.position.Y + 80;
                                bulletNumber2 -= 1;
                                shootInst.Play(); //player shooting sound effect
                                bulletsFired += 1; //total amount of bullets the player has fired for the achievment
                            }
                            if (isPlayerShootingRight2 == false) //fires to the left
                            {
                                //where the bullet fires from if the player is facing right
                                player2Bullet.position.X = p2.position.X - 50;
                                player2Bullet.position.Y = p2.position.Y + 80;
                                bulletNumber2 -= 1;
                                shootInst.Play();  //player shooting sound effect
                                bulletsFired += 1; //total amount of bullets the player has fired for the achievment
                            }
                        }
                    }
                }

                if (currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) || //stick movement or dpad
                currentButton2.IsButtonDown(Buttons.DPadLeft))
                {
                    isPlayerShootingRight2 = false;
                }


                if (currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) || //stick movement or dpad
                     currentButton2.IsButtonDown(Buttons.DPadRight))
                {
                    isPlayerShootingRight2 = true;
                }

                //speed the player bullet moves at
                if (player2Bullet.position.X > p2.position.X) player2Bullet.position.X += 18;
                if (player2Bullet.position.X < p2.position.X) player2Bullet.position.X -= 18;

                //moves the bullet of screen
                if (player2Bullet.position.X > p2.position.X + 1000 || player2Bullet.position.X < p2.position.X - 1000)
                {
                    player2Bullet.position = new Vector2(-10000, -10000);
                }

                //an if loop that means the if the player is dead they wont be effected by enemies 
                if (p2.playerHealth2 <= 0) isPlayerDead2 = true;
                if (p2.playerHealth2 > 0) isPlayerDead2 = false;


                //if an enemy attacks player 2 
                foreach (Enemy enem in enemies)
                {
                    if (enem.collision(p2) && p2.position.X < enem.position.X && isPlayerAttacking2 == false && isPlayerDead2 == false) //attacked to the left
                    {
                        p2.velocity.X -= 100;
                        p2.velocity.Y -= 100;
                        p2.playerHealth2 -= 2; // how much health the player loses

                        if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);//talk
                        if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);//talk
                    }
                    if (enem.collision(p2) && p2.position.X > enem.position.X && isPlayerAttacking2 == false && isPlayerDead2 == false) //attacked to the right 
                    {
                        p2.velocity.X += 100;
                        p2.velocity.Y -= 100;
                        p2.playerHealth2 -= 2; // how much health the player loses

                        if (characterChoice2 == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                        if (characterChoice2 == 3) tamHurt.Play(soundEffectVolume, 0, 0);//talk
                        if (characterChoice2 == 4) thistleHurt.Play(soundEffectVolume, 0, 0);//talk
                    }

                    //player attacking an enemy
                    if (enem.collision(p2) && isPlayerAttacking2 == true && energyNumber2 > 0 && p2.onGround == true)
                    {
                        enem.enemyHealth -= 2;
                        p2Score += 100;
                        if (enem.enemyHealth <= 0)
                        {
                            enem.position = new Vector2(-5000, -5000); //moves the enemy of screen
                            totalEnemiesKilled += 1;
                        }
                    }

                    if (enem.collision(player2Bullet)) //resets the bullet, takes away 1 enemy health and if enemy has 0 health, moves the off screen
                    {
                        player2Bullet.position = new Vector2(-10000, -10000);
                        enem.enemyHealth -= 1;

                        if (enem.enemyHealth <= 0)
                        {
                            enem.position = new Vector2(-5000, -5000);
                            p2Score += 200;
                            totalEnemiesKilled += 1;
                            break;
                        }
                    }

                    if (isPlayerAttacking2 == true && energyNumber2 > 0) //if the player is attacking and has enough energy to do so
                    {
                        if (p2.currentAnimation == 3 || p2.currentAnimation == 9 || p2.currentAnimation == 15 || p2.currentAnimation == 21 || p2.currentAnimation == 27)
                        {
                            if (p2.animations[p2.currentAnimation].currentAnimationFrame == 9)
                            {
                                energyNumber2 -= 1;
                                break;
                            }
                        }
                    }

                    //allows for the midge sound to only play once
                    if (p2.position.X < enem.position.X)
                    {
                        if (p2.position.X - enem.position.X < 0 && p2.position.X - enem.position.X > -800 && enem.currentAnimation == 1 && enem.position.Y > 0 && midgeSoundOnce == false)
                        {
                            midgeInst.Play();
                        }
                    }
                    if (p2.position.X > enem.position.X)
                    {
                        if (enem.position.X - p2.position.X < 0 && p2.position.X - enem.position.X > -800 && enem.currentAnimation == 1 && enem.position.Y > 0 && midgeSoundOnce == false)
                        {
                            midgeInst.Play();
                        }
                    }
                }

                //player 2 collecting bullets
                foreach (GameSprite bCollect in bulletCollect)
                {
                    if (p2.collision(bCollect) && bulletNumber2 <= 8)
                    {
                        bCollect.position = new Vector2(-2000, 0);
                        bulletNumber2 += 1;
                        bulletNumber += 1;
                        bulletSound.Play(soundEffectVolume, 0, 0); //bullet sound plays when the player picks up a bullet collectable
                    }
                }

                //player 2 moving back to the edge of platforms ect.
                foreach (GameSprite trans in transPlatforms)
                {
                    if (trans.collision(p2))
                    {
                        p2.moveBackToEdge(trans);
                    }
                }

                //energy collectable into the game also the player picking it up and adding on energy to the players amount 
                foreach (GameSprite eCollect in energyCollect)
                {
                    if (p2.collision(eCollect) && energyNumber2 <= 8)
                    {
                        eCollect.position = new Vector2(-2000, 0);
                        energyNumber += 1;
                        energyNumber2 += 2;
                        energySound.Play(soundEffectVolume, 0, 0); //energy sound when the player picks up energy
                    }
                }

                //coins animation into the game 
                foreach (GameSprite c in coins)
                {
                    if (p2.collision(c)) // collecting the coins 
                    {
                        c.position = new Vector2(-1000, 0);
                        coinsCollected2 += 1;
                        p2Score += 10;
                        coinCollectedSound.Play(soundEffectVolume, 0, 0); //coin sound effect
                    }
                }

                //player 2 moving back to edge of platforms ect.
                foreach (GameSprite tab in tabPlatforms)
                {
                    if (tab.collision(p2))
                    {
                        p2.moveBackToEdge(tab);
                    }
                }

                //brings player back if they fall between platforms into the irn bru lake. Also slows the palyer           
                if (p2.position.Y > 900 && p2.position.X > 10800 && p2.position.X < 17500)
                {
                    p2.velocity.Y = 1;
                    p2.velocity.X = 1;

                    if (p2.position.Y > 1300)
                    {
                        p2.playerHealth2 -= 6;
                        if (!isPlayerDead)
                        p1.position = new Vector2(10000, 0);
                        if (!isPlayerDead2)
                        p2.position = new Vector2(10000, 0);
                    }
                }

                if (currentButton2.IsButtonDown(Buttons.X) && energyNumber2 > 0 && p2.onGround == true) isPlayerAttacking2 = true;//player will remove enemies from the screen
                if (currentButton2.IsButtonUp(Buttons.X) || energyNumber2 <= 0) isPlayerAttacking2 = false;//player will be thrown back by the enemies and take damage

                //player energy and bullets
                if (characterChoice2 == 0)
                {
                    playersBulletsAndEnergy2.position = new Vector2(p2.position.X + 10, p2.position.Y - 40);

                    //postion of the amount of energy and bullets the player has left
                    fontPosition2 = new Vector2(p2.position.X + 45, p2.position.Y - 39);
                    fontPositionEnergy2 = new Vector2(p2.position.X + 110, p2.position.Y - 39);
                }

                if (characterChoice2 == 1)
                {
                    playersBulletsAndEnergy2.position = new Vector2(p2.position.X, p2.position.Y - 40);

                    //postion of the amount of energy and bullets the player has left
                    fontPosition2 = new Vector2(p2.position.X + 35, p2.position.Y - 39);
                    fontPositionEnergy2 = new Vector2(p2.position.X + 100, p2.position.Y - 39);
                }

                if (characterChoice2 == 2)
                {
                    playersBulletsAndEnergy2.position = new Vector2(p2.position.X + 10, p2.position.Y - 40);

                    //postion of the amount of energy and bullets the player has left
                    fontPosition2 = new Vector2(p2.position.X + 45, p2.position.Y - 39);
                    fontPositionEnergy2 = new Vector2(p2.position.X + 110, p2.position.Y - 39);
                }

                if (characterChoice2 == 3)
                {
                    playersBulletsAndEnergy2.position = new Vector2(p2.position.X, p2.position.Y - 40);

                    //postion of the amount of energy and bullets the player has left
                    fontPosition2 = new Vector2(p2.position.X + 35, p2.position.Y - 39);
                    fontPositionEnergy2 = new Vector2(p2.position.X + 100, p2.position.Y - 39);
                }

                if (characterChoice2 == 4)
                {
                    playersBulletsAndEnergy2.position = new Vector2(p2.position.X, p2.position.Y - 40);

                    //postion of the amount of energy and bullets the player has left
                    fontPosition2 = new Vector2(p2.position.X + 35, p2.position.Y - 39);
                    fontPositionEnergy2 = new Vector2(p2.position.X + 100, p2.position.Y - 39);
                }
            }

            //stops the player looping through the death animation
            if (isPlayerDead && p1.animations[p1.currentAnimation].currentAnimationFrame == 17) p1.position.X += 0;
            else
            {
                //player into the game
                p1.Update(gameTime.ElapsedGameTime.Milliseconds, characterChoice, isPlayerAttacking, isPlayerShootingRight, energyNumber, flySwitch);
            }

            //coin flashing into the game
            coin.Update(gameTime.ElapsedGameTime.Milliseconds);
            coin2.Update(gameTime.ElapsedGameTime.Milliseconds);

            //how fast the player falls back to the ground 
            p1.velocity.Y += 2.5f;

            //an if loop that means the if the player is dead they wont be effected by enemies 
            if (p1.playerHealth <= 0) isPlayerDead = true;
            if (p1.playerHealth > 0) isPlayerDead = false;

            //if an enemy attack a player 
            foreach (Enemy enem in enemies)
            {
                if (enem.collision(p1) && p1.position.X < enem.position.X && isPlayerAttacking == false && isPlayerDead == false) //attacked to the left
                {
                    p1.velocity.X -= 100;
                    p1.velocity.Y -= 100;
                    p1.playerHealth -= 2; // how much health the player loses

                    if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);//talk
                    if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);//talk
                }
                if (enem.collision(p1) && p1.position.X > enem.position.X && isPlayerAttacking == false && isPlayerDead == false) //attacked to the right 
                {
                    p1.velocity.X += 100;
                    p1.velocity.Y -= 100;
                    p1.playerHealth -= 2; // how much health the player loses

                    if (characterChoice == 0) bruHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 1) cooHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 2) nessieHurt.Play(soundEffectVolume, 0, 0);
                    if (characterChoice == 3) tamHurt.Play(soundEffectVolume, 0, 0);//talk
                    if (characterChoice == 4) thistleHurt.Play(soundEffectVolume, 0, 0);//talk
                }

                //player attacking an enemy
                if (enem.collision(p1) && isPlayerAttacking == true && energyNumber > 0 && p1.onGround == true)
                {
                    enem.enemyHealth -= 2;
                    p1Score += 100;
                    totalEnemiesKilled += 1;
                    if (enem.enemyHealth <= 0)
                    {
                        enem.position = new Vector2(-5000, -5000); //moves the enemy of screen
                    }
                }



                if (enem.collision(playerBullet)) //resets the bullet, takes away 1 enemy health and if enemy has 0 health, moves the off screen
                {
                    playerBullet.position = new Vector2(-10000, -10000);
                    enem.enemyHealth -= 1;

                    if (enem.enemyHealth <= 0)
                    {
                        enem.position = new Vector2(-5000, -5000);
                        p1Score += 200;
                        totalEnemiesKilled += 1;
                        break;
                    }
                }

                if (isPlayerAttacking == true && energyNumber > 0) //if the player is attacking and has enough energy to do so
                {
                    if (p1.currentAnimation == 3 || p1.currentAnimation == 9 || p1.currentAnimation == 15 || p1.currentAnimation == 21 || p1.currentAnimation == 27)
                    {
                        if (p1.animations[p1.currentAnimation].currentAnimationFrame == 9)
                        {
                            energyNumber -= 1;
                            break;
                        }
                    }
                }


                //allows for the midge sound to only play once
                if (p1.position.X < enem.position.X)
                {
                    if (p1.position.X - enem.position.X < 0 && p1.position.X - enem.position.X > -800 && enem.currentAnimation == 1 && enem.position.Y > 0)
                    {
                        midgeInst.Play();
                        midgeSoundOnce = true;
                    }
                    else midgeSoundOnce = false;
                }

                if (p1.position.X > enem.position.X)
                {
                    if (enem.position.X - p1.position.X < 0 && p1.position.X - enem.position.X > -800 && enem.currentAnimation == 1 && enem.position.Y > 0)
                    {
                        midgeInst.Play();
                        midgeSoundOnce = true;
                    }
                    else midgeSoundOnce = false;
                }
            }

            foreach (GameSprite bCollect in bulletCollect)
            {
                bCollect.Update(gameTime.ElapsedGameTime.Milliseconds);

                if (p1.collision(bCollect) && bulletNumber <= 8)
                {
                    bCollect.position = new Vector2(-2000, 0);
                    bulletNumber += 1;
                    bulletNumber2 += 1;
                    bulletSound.Play(soundEffectVolume, 0, 0); //bullet sound plays when the player picks up a bullet collectable
                }
            }


            //energy collectable into the game also the player picking it up and adding on energy to the players amount 
            foreach (GameSprite eCollect in energyCollect)
            {
                eCollect.Update(gameTime.ElapsedGameTime.Milliseconds);

                if (p1.collision(eCollect) && energyNumber <= 8)
                {
                    eCollect.position = new Vector2(-2000, 0);
                    energyNumber += 1;
                    energyNumber2 += 1;
                    energySound.Play(soundEffectVolume, 0, 0); //energy sound when the player picks up energy
                }
            }



            //irn bru waves into the game
            foreach (GameSprite waves in irnBruWaves)
            {
                waves.Update(gameTime.ElapsedGameTime.Milliseconds);
            }

            //irn bru waves into the game player fall between these
            foreach (GameSprite fallBetweenWaves in irnBruWaves2)
            {
                fallBetweenWaves.Update(gameTime.ElapsedGameTime.Milliseconds);
            }

            //enemies into the game
            foreach (Enemy enem in enemies)
            {
                enem.Update(gameTime.ElapsedGameTime.Milliseconds);

            }

            //coins animation into the game 
            foreach (GameSprite c in coins)
            {
                c.Update(gameTime.ElapsedGameTime.Milliseconds);

                if (p1.collision(c)) // collecting the coins 
                {
                    c.position = new Vector2(-1000, 0);
                    coinsCollected += 1;
                    p1Score += 10;
                    coinCollectedSound.Play(soundEffectVolume, 0, 0); //coin sound effect
                }
            }

            //player 1 moving back to the edge of platforms ect.
            foreach (GameSprite trans in transPlatforms)
            {
                if (trans.collision(p1))
                {
                    p1.moveBackToEdge(trans);


                }
            }


            //player 1 moving back to edge of platforms ect.
            foreach (GameSprite tab in tabPlatforms)
            {
                if (tab.collision(p1))
                {
                    p1.moveBackToEdge(tab);
                }
            }








            //timer to make the jump image flash in and out of transparency
            if (shade >= 1) shadeSwitch = true;
            if (shade <= 0) shadeSwitch = false;
            if (shadeSwitch) shade -= 0.01f;
            if (!shadeSwitch) shade += 0.01f;

            //player 2 turning into the flying animation 
            if (player2 && p2.position.X > 19000 && p2.position.Y > 1500)
            {
                flySwitch2 = true;
            }

            if (flySwitch2)
            {
                playerUp2 += gameTime.ElapsedGameTime.Milliseconds;
                if (playerUp2 < 3500) p2.velocity.Y -= 5;
            }

            //player turning into the flying animation 
            if (p1.position.X > 19000 && p1.position.Y > 1500)
            {
                flySwitch = true;
            }

            if (flySwitch)
            {


                playerUp += gameTime.ElapsedGameTime.Milliseconds;
                if (playerUp < 3500) p1.velocity.Y -= 5;
            }

            //brings player back if they fall between platforms into the irn bru lake. Also slows the palyer
            if (p1.position.Y > 900 && p1.position.X > 10800 && p1.position.X < 17500)
            {
                p1.velocity.Y = 1;
                p1.velocity.X = 1;

                if (p1.position.Y > 1300)
                {
                    p1.playerHealth -= 6;
                    if (!isPlayerDead)
                    p1.position = new Vector2(10000, 0);
                    if(!isPlayerDead2)
                    p2.position = new Vector2(10000, 0);
                }
            }

            //monster attacking the player 2 
            if (player2 && p2.position.Y > 950 && p2.position.X > 10800 && p2.position.X < 17500)
            {
                monster2.Update(gameTime.ElapsedGameTime.Milliseconds);

                if (p2.position.X < 14200)
                {
                    monster2.position = new Vector2(p2.position.X - 110, 743);
                    monster.flipHorizontally = false;
                }
                if (p2.position.X > 14200)
                {
                    monster2.position = new Vector2(p2.position.X - 290, 743);
                    monster2.flipHorizontally = true;
                }

                if (p2.position.Y > 1000)
                {
                    p2.velocity.Y += 10;
                }
            }
            else
            {
                if (monster2.animations[monster2.currentAnimation].currentAnimationFrame > 0)
                    monster2.Update(gameTime.ElapsedGameTime.Milliseconds);

            }



            //monster attacking the player 
            if (p1.position.Y > 950 && p1.position.X > 10800 && p1.position.X < 17500)
            {
                monster.Update(gameTime.ElapsedGameTime.Milliseconds);
                if (p1.position.X < 14200)
                {
                    monster.position = new Vector2(p1.position.X - 110, 743);
                    monster.flipHorizontally = false;
                }

                if (p1.position.X > 14200)
                {
                    monster.position = new Vector2(p1.position.X - 290, 743);
                    monster.flipHorizontally = true;
                }

                if (p1.position.Y > 1000)
                {
                    p1.velocity.Y += 10;
                }
            }
            else
            {
                if (monster.animations[monster.currentAnimation].currentAnimationFrame > 0)
                    monster.Update(gameTime.ElapsedGameTime.Milliseconds);

            }


            //everything to do with enemies. When to attack, when to change direction ect.
            enemyAttacksAndBoundaries(1900, 2800, 500, 210, 0, 2000, 2650);//0

            enemyAttacksAndBoundaries(4400, 5710, 500, 210, 1, 4500, 5570);//1
            enemyAttacksAndBoundaries(4400, 5710, 500, 210, 2, 4500, 5570);//2

            enemyAttacksAndBoundaries(4600, 5500, 210, -80, 3, 4700, 5360);//3

            enemyAttacksAndBoundaries(7900, 9210, 140, -150, 4, 8000, 9070);//4
            enemyAttacksAndBoundaries(7900, 9210, 140, -150, 5, 8000, 9070);//5
            enemyAttacksAndBoundaries(7900, 9210, 140, -150, 6, 8000, 9070);//6

            enemyAttacksAndBoundaries(8310, 8800, 500, 210, 7, 8410, 8660);//7

            enemyAttacksAndBoundaries(13100, 13590, 350, 60, 8, 13200, 13450);//8
            enemyAttacksAndBoundaries(13800, 14290, 350, 60, 9, 13900, 14150);//9
            enemyAttacksAndBoundaries(14500, 14990, 350, 60, 10, 14600, 14850);//10


        }

        private void EverythingToDoWithPlayerHealth()
        {
            if (player2)
            {
                foreach (GameSprite health2 in playerHealths2)
                {
                    if (!isPlayerDead)
                    {
                        playerHealths2[0].position = new Vector2(p1.position.X + 735, 95);
                        playerHealths2[1].position = new Vector2(p1.position.X + 720, 95);
                        playerHealths2[2].position = new Vector2(p1.position.X + 705, 95);
                        playerHealths2[3].position = new Vector2(p1.position.X + 690, 95);
                        playerHealths2[4].position = new Vector2(p1.position.X + 675, 95);
                        playerHealths2[5].position = new Vector2(p1.position.X + 660, 95);
                        playerHealths2[6].position = new Vector2(p1.position.X + 645, 95);
                        playerHealths2[7].position = new Vector2(p1.position.X + 630, 95);
                        playerHealths2[8].position = new Vector2(p1.position.X + 615, 95);
                        playerHealths2[9].position = new Vector2(p1.position.X + 600, 95);
                        playerHealths2[10].position = new Vector2(p1.position.X + 585, 95);
                        playerHealths2[11].position = new Vector2(p1.position.X + 570, 95);
                        playerHealths2[12].position = new Vector2(p1.position.X + 555, 95);
                        playerHealths2[13].position = new Vector2(p1.position.X + 540, 95);
                        playerHealths2[14].position = new Vector2(p1.position.X + 525, 95);
                        playerHealths2[15].position = new Vector2(p1.position.X + 510, 95);
                        playerHealths2[16].position = new Vector2(p1.position.X + 495, 95);
                        playerHealths2[17].position = new Vector2(p1.position.X + 480, 95);
                        playerHealths2[18].position = new Vector2(p1.position.X + 465, 95);
                        playerHealths2[19].position = new Vector2(p1.position.X + 450, 95);
                        playerHealths2[20].position = new Vector2(p1.position.X + 435, 95);
                        playerHealths2[21].position = new Vector2(p1.position.X + 420, 95);
                        playerHealths2[22].position = new Vector2(p1.position.X + 405, 95);
                        playerHealths2[23].position = new Vector2(p1.position.X + 390, 95);
                    }

                    if (isPlayerDead && player2)
                    {
                        playerHealths2[0].position = new Vector2(p2.position.X + 735, 95);
                        playerHealths2[1].position = new Vector2(p2.position.X + 720, 95);
                        playerHealths2[2].position = new Vector2(p2.position.X + 705, 95);
                        playerHealths2[3].position = new Vector2(p2.position.X + 690, 95);
                        playerHealths2[4].position = new Vector2(p2.position.X + 675, 95);
                        playerHealths2[5].position = new Vector2(p2.position.X + 660, 95);
                        playerHealths2[6].position = new Vector2(p2.position.X + 645, 95);
                        playerHealths2[7].position = new Vector2(p2.position.X + 630, 95);
                        playerHealths2[8].position = new Vector2(p2.position.X + 615, 95);
                        playerHealths2[9].position = new Vector2(p2.position.X + 600, 95);
                        playerHealths2[10].position = new Vector2(p2.position.X + 585, 95);
                        playerHealths2[11].position = new Vector2(p2.position.X + 570, 95);
                        playerHealths2[12].position = new Vector2(p2.position.X + 555, 95);
                        playerHealths2[13].position = new Vector2(p2.position.X + 540, 95);
                        playerHealths2[14].position = new Vector2(p2.position.X + 525, 95);
                        playerHealths2[15].position = new Vector2(p2.position.X + 510, 95);
                        playerHealths2[16].position = new Vector2(p2.position.X + 495, 95);
                        playerHealths2[17].position = new Vector2(p2.position.X + 480, 95);
                        playerHealths2[18].position = new Vector2(p2.position.X + 465, 95);
                        playerHealths2[19].position = new Vector2(p2.position.X + 450, 95);
                        playerHealths2[20].position = new Vector2(p2.position.X + 435, 95);
                        playerHealths2[21].position = new Vector2(p2.position.X + 420, 95);
                        playerHealths2[22].position = new Vector2(p2.position.X + 405, 95);
                        playerHealths2[23].position = new Vector2(p2.position.X + 390, 95);
                    }
                    //what happens when the player loses health
                    if (p2.playerHealth2 == 22)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22);

                    }
                    if (p2.playerHealth2 == 20)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                    }
                    if (p2.playerHealth2 == 18)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18);
                    }
                    if (p2.playerHealth2 == 16)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                    }
                    if (p2.playerHealth2 == 14)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14);
                    }
                    if (p2.playerHealth2 == 12)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                    }
                    if (p2.playerHealth2 == 10)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10);
                    }
                    if (p2.playerHealth2 == 8)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                    }
                    if (p2.playerHealth2 == 6)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6);

                    }
                    if (p2.playerHealth2 == 4)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6); healthLossPlacement2(5); healthLossPlacement2(4);
                    }
                    if (p2.playerHealth2 == 2)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6); healthLossPlacement2(5); healthLossPlacement2(4);
                        healthLossPlacement2(3); healthLossPlacement2(2);
                    }
                    if (p2.playerHealth2 == 0)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6); healthLossPlacement2(5); healthLossPlacement2(4);
                        healthLossPlacement2(3); healthLossPlacement2(2); healthLossPlacement2(1); healthLossPlacement2(0);
                    }

                    if (p2.playerHealth2 < 0) p2.playerHealth2 = 0;
                }
            }
            //position of each health bar
            foreach (GameSprite health in playerHealths)
            {
                if (currentGameState == Gamestate.inGame) //first level
                {

                    if (!isPlayerDead)
                    {
                        playerHealths[0].position = new Vector2(p1.position.X - 760, 95);
                        playerHealths[1].position = new Vector2(p1.position.X - 745, 95);
                        playerHealths[2].position = new Vector2(p1.position.X - 730, 95);
                        playerHealths[3].position = new Vector2(p1.position.X - 715, 95);
                        playerHealths[4].position = new Vector2(p1.position.X - 700, 95);
                        playerHealths[5].position = new Vector2(p1.position.X - 685, 95);
                        playerHealths[6].position = new Vector2(p1.position.X - 670, 95);
                        playerHealths[7].position = new Vector2(p1.position.X - 655, 95);
                        playerHealths[8].position = new Vector2(p1.position.X - 640, 95);
                        playerHealths[9].position = new Vector2(p1.position.X - 625, 95);
                        playerHealths[10].position = new Vector2(p1.position.X - 610, 95);
                        playerHealths[11].position = new Vector2(p1.position.X - 595, 95);
                        playerHealths[12].position = new Vector2(p1.position.X - 580, 95);
                        playerHealths[13].position = new Vector2(p1.position.X - 565, 95);
                        playerHealths[14].position = new Vector2(p1.position.X - 550, 95);
                        playerHealths[15].position = new Vector2(p1.position.X - 535, 95);
                        playerHealths[16].position = new Vector2(p1.position.X - 520, 95);
                        playerHealths[17].position = new Vector2(p1.position.X - 505, 95);
                        playerHealths[18].position = new Vector2(p1.position.X - 490, 95);
                        playerHealths[19].position = new Vector2(p1.position.X - 475, 95);
                        playerHealths[20].position = new Vector2(p1.position.X - 460, 95);
                        playerHealths[21].position = new Vector2(p1.position.X - 445, 95);
                        playerHealths[22].position = new Vector2(p1.position.X - 430, 95);
                        playerHealths[23].position = new Vector2(p1.position.X - 415, 95);
                    }
                    if (isPlayerDead && player2)
                    {
                        playerHealths[0].position = new Vector2(p2.position.X - 760, 95);
                        playerHealths[1].position = new Vector2(p2.position.X - 745, 95);
                        playerHealths[2].position = new Vector2(p2.position.X - 730, 95);
                        playerHealths[3].position = new Vector2(p2.position.X - 715, 95);
                        playerHealths[4].position = new Vector2(p2.position.X - 700, 95);
                        playerHealths[5].position = new Vector2(p2.position.X - 685, 95);
                        playerHealths[6].position = new Vector2(p2.position.X - 670, 95);
                        playerHealths[7].position = new Vector2(p2.position.X - 655, 95);
                        playerHealths[8].position = new Vector2(p2.position.X - 640, 95);
                        playerHealths[9].position = new Vector2(p2.position.X - 625, 95);
                        playerHealths[10].position = new Vector2(p2.position.X - 610, 95);
                        playerHealths[11].position = new Vector2(p2.position.X - 595, 95);
                        playerHealths[12].position = new Vector2(p2.position.X - 580, 95);
                        playerHealths[13].position = new Vector2(p2.position.X - 565, 95);
                        playerHealths[14].position = new Vector2(p2.position.X - 550, 95);
                        playerHealths[15].position = new Vector2(p2.position.X - 535, 95);
                        playerHealths[16].position = new Vector2(p2.position.X - 520, 95);
                        playerHealths[17].position = new Vector2(p2.position.X - 505, 95);
                        playerHealths[18].position = new Vector2(p2.position.X - 490, 95);
                        playerHealths[19].position = new Vector2(p2.position.X - 475, 95);
                        playerHealths[20].position = new Vector2(p2.position.X - 460, 95);
                        playerHealths[21].position = new Vector2(p2.position.X - 445, 95);
                        playerHealths[22].position = new Vector2(p2.position.X - 430, 95);
                        playerHealths[23].position = new Vector2(p2.position.X - 415, 95);
                    }

                    //what happens when the player loses health and level 1 is playing 
                    if (p1.playerHealth == 22)
                    {
                        healthLossPlacement(23); healthLossPlacement(22);

                    }
                    if (p1.playerHealth == 20)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                    }
                    if (p1.playerHealth == 18)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18);
                    }
                    if (p1.playerHealth == 16)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                    }
                    if (p1.playerHealth == 14)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14);
                    }
                    if (p1.playerHealth == 12)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                    }
                    if (p1.playerHealth == 10)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10);
                    }
                    if (p1.playerHealth == 8)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                    }
                    if (p1.playerHealth == 6)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6);

                    }
                    if (p1.playerHealth == 4)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6); healthLossPlacement(5); healthLossPlacement(4);
                    }
                    if (p1.playerHealth == 2)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6); healthLossPlacement(5); healthLossPlacement(4);
                        healthLossPlacement(3); healthLossPlacement(2);
                    }
                    if (p1.playerHealth == 0)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6); healthLossPlacement(5); healthLossPlacement(4);
                        healthLossPlacement(3); healthLossPlacement(2); healthLossPlacement(1); healthLossPlacement(0);
                    }

                    if (p1.playerHealth < 0) p1.playerHealth = 0;
                }

                if (currentGameState == Gamestate.inGameLevel2) // second level
                {
                    playerHealths[0].position = new Vector2(screenWidth / 2 - 760, 95);
                    playerHealths[1].position = new Vector2(screenWidth / 2 - 745, 95);
                    playerHealths[2].position = new Vector2(screenWidth / 2 - 730, 95);
                    playerHealths[3].position = new Vector2(screenWidth / 2 - 715, 95);
                    playerHealths[4].position = new Vector2(screenWidth / 2 - 700, 95);
                    playerHealths[5].position = new Vector2(screenWidth / 2 - 685, 95);
                    playerHealths[6].position = new Vector2(screenWidth / 2 - 670, 95);
                    playerHealths[7].position = new Vector2(screenWidth / 2 - 655, 95);
                    playerHealths[8].position = new Vector2(screenWidth / 2 - 640, 95);
                    playerHealths[9].position = new Vector2(screenWidth / 2 - 625, 95);
                    playerHealths[10].position = new Vector2(screenWidth / 2 - 610, 95);
                    playerHealths[11].position = new Vector2(screenWidth / 2 - 595, 95);
                    playerHealths[12].position = new Vector2(screenWidth / 2 - 580, 95);
                    playerHealths[13].position = new Vector2(screenWidth / 2 - 565, 95);
                    playerHealths[14].position = new Vector2(screenWidth / 2 - 550, 95);
                    playerHealths[15].position = new Vector2(screenWidth / 2 - 535, 95);
                    playerHealths[16].position = new Vector2(screenWidth / 2 - 520, 95);
                    playerHealths[17].position = new Vector2(screenWidth / 2 - 505, 95);
                    playerHealths[18].position = new Vector2(screenWidth / 2 - 490, 95);
                    playerHealths[19].position = new Vector2(screenWidth / 2 - 475, 95);
                    playerHealths[20].position = new Vector2(screenWidth / 2 - 460, 95);
                    playerHealths[21].position = new Vector2(screenWidth / 2 - 445, 95);
                    playerHealths[22].position = new Vector2(screenWidth / 2 - 430, 95);
                    playerHealths[23].position = new Vector2(screenWidth / 2 - 415, 95);

                    bossHealths[0].position = new Vector2(850, 970);
                    bossHealths[1].position = new Vector2(865, 970);
                    bossHealths[2].position = new Vector2(880, 970);
                    bossHealths[3].position = new Vector2(895, 970);
                    bossHealths[4].position = new Vector2(910, 970);
                    bossHealths[5].position = new Vector2(925, 970);
                    bossHealths[6].position = new Vector2(940, 970);
                    bossHealths[7].position = new Vector2(955, 970);
                    bossHealths[8].position = new Vector2(970, 970);
                    bossHealths[9].position = new Vector2(985, 970);
                    bossHealths[10].position = new Vector2(1000, 970);
                    bossHealths[11].position = new Vector2(1015, 970);
                    bossHealths[12].position = new Vector2(1030, 970);
                    bossHealths[13].position = new Vector2(1045, 970);
                    bossHealths[14].position = new Vector2(1060, 970);
                    bossHealths[15].position = new Vector2(1075, 970);
                    bossHealths[16].position = new Vector2(1090, 970);
                    bossHealths[17].position = new Vector2(1105, 970);
                    bossHealths[18].position = new Vector2(1120, 970);
                    bossHealths[19].position = new Vector2(1135, 970);
                    bossHealths[20].position = new Vector2(1150, 970);
                    bossHealths[21].position = new Vector2(1165, 970);
                    bossHealths[22].position = new Vector2(1180, 970);
                    bossHealths[23].position = new Vector2(1195, 970);

                    //what happens when the boss loses health
                    if (bossHealth == 22 || bossHealth == 21)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22);

                    }
                    if (bossHealth == 20 || bossHealth == 19)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                    }
                    if (bossHealth == 18 || bossHealth == 17)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18);
                    }
                    if (bossHealth == 16 || bossHealth == 15)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                    }
                    if (bossHealth == 14 || bossHealth == 13)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14);
                    }
                    if (bossHealth == 12 || bossHealth == 11)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14); healthLossPlacement3(13); healthLossPlacement3(12);
                    }
                    if (bossHealth == 10 || bossHealth == 9)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14); healthLossPlacement3(13); healthLossPlacement3(12);
                        healthLossPlacement3(11); healthLossPlacement3(10);
                    }
                    if (bossHealth == 8 || bossHealth == 7)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14); healthLossPlacement3(13); healthLossPlacement3(12);
                        healthLossPlacement3(11); healthLossPlacement3(10); healthLossPlacement3(9); healthLossPlacement3(8);
                    }
                    if (bossHealth == 6 || bossHealth == 5)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14); healthLossPlacement3(13); healthLossPlacement3(12);
                        healthLossPlacement3(11); healthLossPlacement3(10); healthLossPlacement3(9); healthLossPlacement3(8);
                        healthLossPlacement3(7); healthLossPlacement3(6);

                    }
                    if (bossHealth == 4 || bossHealth == 3)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14); healthLossPlacement3(13); healthLossPlacement3(12);
                        healthLossPlacement3(11); healthLossPlacement3(10); healthLossPlacement3(9); healthLossPlacement3(8);
                        healthLossPlacement3(7); healthLossPlacement3(6); healthLossPlacement3(5); healthLossPlacement3(4);
                    }
                    if (bossHealth == 2 || bossHealth == 1)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14); healthLossPlacement3(13); healthLossPlacement3(12);
                        healthLossPlacement3(11); healthLossPlacement3(10); healthLossPlacement3(9); healthLossPlacement3(8);
                        healthLossPlacement3(7); healthLossPlacement3(6); healthLossPlacement3(5); healthLossPlacement3(4);
                        healthLossPlacement3(3); healthLossPlacement3(2);
                    }
                    if (bossHealth == 0)
                    {
                        healthLossPlacement3(23); healthLossPlacement3(22); healthLossPlacement3(21); healthLossPlacement3(20);
                        healthLossPlacement3(19); healthLossPlacement3(18); healthLossPlacement3(17); healthLossPlacement3(16);
                        healthLossPlacement3(15); healthLossPlacement3(14); healthLossPlacement3(13); healthLossPlacement3(12);
                        healthLossPlacement3(11); healthLossPlacement3(10); healthLossPlacement3(9); healthLossPlacement3(8);
                        healthLossPlacement3(7); healthLossPlacement3(6); healthLossPlacement3(5); healthLossPlacement3(4);
                        healthLossPlacement3(3); healthLossPlacement3(2); healthLossPlacement3(1); healthLossPlacement3(0);
                    }

                    if (bossHealth < 0) bossHealth = 0;

                    if (player2)
                    {
                        playerHealths2[0].position = new Vector2(screenWidth / 2 + 735, 95);
                        playerHealths2[1].position = new Vector2(screenWidth / 2 + 720, 95);
                        playerHealths2[2].position = new Vector2(screenWidth / 2 + 705, 95);
                        playerHealths2[3].position = new Vector2(screenWidth / 2 + 690, 95);
                        playerHealths2[4].position = new Vector2(screenWidth / 2 + 675, 95);
                        playerHealths2[5].position = new Vector2(screenWidth / 2 + 660, 95);
                        playerHealths2[6].position = new Vector2(screenWidth / 2 + 645, 95);
                        playerHealths2[7].position = new Vector2(screenWidth / 2 + 630, 95);
                        playerHealths2[8].position = new Vector2(screenWidth / 2 + 615, 95);
                        playerHealths2[9].position = new Vector2(screenWidth / 2 + 600, 95);
                        playerHealths2[10].position = new Vector2(screenWidth / 2 + 585, 95);
                        playerHealths2[11].position = new Vector2(screenWidth / 2 + 570, 95);
                        playerHealths2[12].position = new Vector2(screenWidth / 2 + 555, 95);
                        playerHealths2[13].position = new Vector2(screenWidth / 2 + 540, 95);
                        playerHealths2[14].position = new Vector2(screenWidth / 2 + 525, 95);
                        playerHealths2[15].position = new Vector2(screenWidth / 2 + 510, 95);
                        playerHealths2[16].position = new Vector2(screenWidth / 2 + 495, 95);
                        playerHealths2[17].position = new Vector2(screenWidth / 2 + 480, 95);
                        playerHealths2[18].position = new Vector2(screenWidth / 2 + 465, 95);
                        playerHealths2[19].position = new Vector2(screenWidth / 2 + 450, 95);
                        playerHealths2[20].position = new Vector2(screenWidth / 2 + 435, 95);
                        playerHealths2[21].position = new Vector2(screenWidth / 2 + 420, 95);
                        playerHealths2[22].position = new Vector2(screenWidth / 2 + 405, 95);
                        playerHealths2[23].position = new Vector2(screenWidth / 2 + 390, 95);
                    }

                    //what happens when the player loses health
                    if (p1Flying.playerFlyingHealth == 22)
                    {
                        healthLossPlacement(23); healthLossPlacement(22);

                    }
                    if (p1Flying.playerFlyingHealth == 20)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                    }
                    if (p1Flying.playerFlyingHealth == 18)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18);
                    }
                    if (p1Flying.playerFlyingHealth == 16)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                    }
                    if (p1Flying.playerFlyingHealth == 14)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14);
                    }
                    if (p1Flying.playerFlyingHealth == 12)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                    }
                    if (p1Flying.playerFlyingHealth == 10)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10);
                    }
                    if (p1Flying.playerFlyingHealth == 8)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                    }
                    if (p1Flying.playerFlyingHealth == 6)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6);

                    }
                    if (p1Flying.playerFlyingHealth == 4)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6); healthLossPlacement(5); healthLossPlacement(4);
                    }
                    if (p1Flying.playerFlyingHealth == 2)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6); healthLossPlacement(5); healthLossPlacement(4);
                        healthLossPlacement(3); healthLossPlacement(2);
                    }
                    if (p1Flying.playerFlyingHealth == 0)
                    {
                        healthLossPlacement(23); healthLossPlacement(22); healthLossPlacement(21); healthLossPlacement(20);
                        healthLossPlacement(19); healthLossPlacement(18); healthLossPlacement(17); healthLossPlacement(16);
                        healthLossPlacement(15); healthLossPlacement(14); healthLossPlacement(13); healthLossPlacement(12);
                        healthLossPlacement(11); healthLossPlacement(10); healthLossPlacement(9); healthLossPlacement(8);
                        healthLossPlacement(7); healthLossPlacement(6); healthLossPlacement(5); healthLossPlacement(4);
                        healthLossPlacement(3); healthLossPlacement(2); healthLossPlacement(1); healthLossPlacement(0);
                    }

                    //what happens when the player loses health player 2
                    if (p2Flying.playerFlyingHealth2 == 22)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22);

                    }
                    if (p2Flying.playerFlyingHealth2 == 20)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                    }
                    if (p2Flying.playerFlyingHealth2 == 18)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18);
                    }
                    if (p2Flying.playerFlyingHealth2 == 16)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                    }
                    if (p2Flying.playerFlyingHealth2 == 14)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14);
                    }
                    if (p2Flying.playerFlyingHealth2 == 12)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                    }
                    if (p2Flying.playerFlyingHealth2 == 10)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10);
                    }
                    if (p2Flying.playerFlyingHealth2 == 8)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                    }
                    if (p2Flying.playerFlyingHealth2 == 6)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6);

                    }
                    if (p2Flying.playerFlyingHealth2 == 4)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6); healthLossPlacement2(5); healthLossPlacement2(4);
                    }
                    if (p2Flying.playerFlyingHealth2 == 2)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6); healthLossPlacement2(5); healthLossPlacement2(4);
                        healthLossPlacement2(3); healthLossPlacement2(2);
                    }
                    if (p2Flying.playerFlyingHealth2 == 0)
                    {
                        healthLossPlacement2(23); healthLossPlacement2(22); healthLossPlacement2(21); healthLossPlacement2(20);
                        healthLossPlacement2(19); healthLossPlacement2(18); healthLossPlacement2(17); healthLossPlacement2(16);
                        healthLossPlacement2(15); healthLossPlacement2(14); healthLossPlacement2(13); healthLossPlacement2(12);
                        healthLossPlacement2(11); healthLossPlacement2(10); healthLossPlacement2(9); healthLossPlacement2(8);
                        healthLossPlacement2(7); healthLossPlacement2(6); healthLossPlacement2(5); healthLossPlacement2(4);
                        healthLossPlacement2(3); healthLossPlacement2(2); healthLossPlacement2(1); healthLossPlacement2(0);
                    }

                    if (p2Flying.playerFlyingHealth2 < 0) p2Flying.playerFlyingHealth2 = 0;

                    if (p1Flying.playerFlyingHealth < 0) p1Flying.playerFlyingHealth = 0;
                }



            }
        }

        private void healthLossPlacement(int healthNumber)
        {
            playerHealths[healthNumber].position = new Vector2(-500, -500);
        }
        private void healthLossPlacement2(int healthNumber2)
        {
            playerHealths2[healthNumber2].position = new Vector2(-500, -500);
        }
        private void healthLossPlacement3(int healthNumber3)
        {
            bossHealths[healthNumber3].position = new Vector2(-500, -500);
        }

        private void enemyAttacksAndBoundaries(int playerLeftBoundary, int playerRightBoundary, int playerHeightBoundaryBottom, int playerHeightBoundaryTop, int whatEnemy, int enemyLeftBoundary, int enemyRightBoundary)
        {

            //enemy movements 
            bool changeDirection = false;
            //velocity
            enemies[whatEnemy].position += enemies[whatEnemy].velocity;
            //boundaries 
            if (enemies[whatEnemy].position.X > enemyRightBoundary)
            {
                changeDirection = true;
                enemies[whatEnemy].position.X -= 5;

            }
            if (enemies[whatEnemy].position.X < enemyLeftBoundary)
            {
                changeDirection = true;
                enemies[whatEnemy].position.X += 5;
            }


            //what enemy changes direction when 
            if (changeDirection) enemies[whatEnemy].velocity = -enemies[whatEnemy].velocity;



            //enemies attacking player
            if (p1.position.X > playerLeftBoundary & p1.position.X < playerRightBoundary & p1.position.Y < playerHeightBoundaryBottom & p1.position.Y > playerHeightBoundaryTop
                || p2.position.X > playerLeftBoundary & p2.position.X < playerRightBoundary & p2.position.Y < playerHeightBoundaryBottom & p2.position.Y > playerHeightBoundaryTop)
            {
                enemies[whatEnemy].currentAnimation = 1; //attack animation
                if (enemies[whatEnemy].velocity.X == 1) enemies[whatEnemy].velocity = new Vector2(5, 0);
                if (enemies[whatEnemy].velocity.X == -1) enemies[whatEnemy].velocity = new Vector2(-5, 0);
            }
            else
            {
                enemies[whatEnemy].currentAnimation = 0;
                if (enemies[whatEnemy].velocity.X == 5) enemies[whatEnemy].velocity = new Vector2(1, 0);
                if (enemies[whatEnemy].velocity.X == -5) enemies[whatEnemy].velocity = new Vector2(-1, 0);

            }
        }

        private void updateAchieveMenu(GameTime gameTime)
        {
            //takes the player back to the main menu
            if (currentButton.IsButtonDown(Buttons.B) && !prevButton.IsButtonDown(Buttons.B)) currentGameState = Gamestate.mainMenu;

            //bagpipe animation
            bagpipePlayer.Update(gameTime.ElapsedGameTime.Milliseconds);
            //bagpipe player movement
            bagpipePlayer.velocity.X = 0.5f;
            //bagpipe player reaches the edge of the screen reset him
            if (bagpipePlayer.position.X > screenWidth)
            {
                bagpipePlayer.position.X = -80;
                bagpipePlayer.position.Y = 500;
            }
            //bagpipe player stays ontop of the black outline
            if (bagpipePlayer.position.X > screenWidth / 2) bagpipePlayer.position.Y += 0.01f;
        }

        private void updateOptionsMenu(GameTime gameTime)
        {
            if (volumeMover == 0)
            {
                //allows for the volums to be changed 
                if (currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) || currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft)) //stick left or dpad left
                {
                    if (volumeAmount >= 2 && volumeAmount <= 10) //
                    {
                        MediaPlayer.Volume -= 0.1f; //the volume amount that is playing
                        volumeAmount -= 1; //takes away 1 volume bar
                    }
                }
                //allows for the volume to be changed
                if (currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) || currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight)) //stick right or dpad right
                {
                    if (volumeAmount >= 0 && volumeAmount <= 9)
                    {
                        MediaPlayer.Volume += 0.1f; //the volume amount that is playing
                        volumeAmount += 1; //adds one volume bar
                    }
                }

                //moves onto the other option 
                if (currentButton.IsButtonDown(Buttons.A) && !prevButton.IsButtonDown(Buttons.A)) volumeMover += 1;
            }

            if (volumeMover == 1) //on the sound effect volume 
            {
                //allows for the volums to be changed 
                if (currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) || currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft)) //stick left or dpad left
                {
                    if (volumeSoundAmount >= 2 && volumeSoundAmount <= 10) //
                    {
                        soundEffectVolume -= 0.1f;             
                        volumeSoundAmount -= 1; //takes away 1 volume bar
                        tamMelee.Play(soundEffectVolume, 0, 0);
                    }
                }
                //allows for the volume to be changed
                if (currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) || currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight)) //stick right or dpad right
                {
                    if (volumeSoundAmount >= 0 && volumeSoundAmount <= 9)
                    {
                        soundEffectVolume += 0.1f;
                        volumeSoundAmount += 1; //adds one volume bar
                        thistleMelee.Play(soundEffectVolume, 0, 0);
                    }
                }
            }
            //takes the player back to the main menu
            if (currentButton.IsButtonDown(Buttons.B) && !prevButton.IsButtonDown(Buttons.B))
            {
                currentGameState = Gamestate.mainMenu;
                volumeMover = 0;
            }

            //bagpipe animation
            bagpipePlayer.Update(gameTime.ElapsedGameTime.Milliseconds);

            //bagpipe player movement
            bagpipePlayer.velocity.X = 0.5f;
            //bagpipe player reaches the edge of the screen reset him
            if (bagpipePlayer.position.X > screenWidth)
            {
                bagpipePlayer.position.X = -80;
                bagpipePlayer.position.Y = 500;
            }
            //bagpipe player stays ontop of the black outline
            if (bagpipePlayer.position.X > screenWidth / 2) bagpipePlayer.position.Y += 0.01f;
        }

        private void updateCharacterSelection(GameTime gameTime)
        {
            //moves between the characters
            if (currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) || //stick movement or dpad
                currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight))
                characterMover += 1;
            if (currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) || //stick movement or dpad
                currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft))
                characterMover -= 1;

            //allows for the charcters to be looped
            if (characterMover > 5) characterMover = 1;
            if (characterMover < 1) characterMover = 5;

            //talk
            if (characterMover == 5 && currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) ||
                characterMover == 5 && currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight))
                thistleName.Play(soundEffectVolume, 0, 0);
            if (characterMover == 5 && currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) ||
               characterMover == 5 && currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft))
                thistleName.Play(soundEffectVolume, 0, 0);

            if (characterMover == 4 && currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) ||
                characterMover == 4 && currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight))
                tamName.Play(soundEffectVolume, 0, 0);
            if (characterMover == 4 && currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) ||
               characterMover == 4 && currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft))
                tamName.Play(soundEffectVolume, 0, 0);

            if (characterMover == 3 && currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) ||
               characterMover == 3 && currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight))
                nessieName.Play(soundEffectVolume, 0, 0);
            if (characterMover == 3 && currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) ||
               characterMover == 3 && currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft))
                nessieName.Play(soundEffectVolume, 0, 0);

            if (characterMover == 2 && currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) ||
               characterMover == 2 && currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight))
                cooName.Play(soundEffectVolume, 0, 0);
            if (characterMover == 2 && currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) ||
               characterMover == 2 && currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft))
                cooName.Play(soundEffectVolume, 0, 0);

            if (characterMover == 1 && currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight) ||
               characterMover == 1 && currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight))
                bruName.Play(soundEffectVolume, 0, 0);
            if (characterMover == 1 && currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft) ||
               characterMover == 1 && currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft))
                bruName.Play(soundEffectVolume, 0, 0);



            //allows player 2 to start in the game
            if (currentButton2.IsButtonDown(Buttons.Start) && !prevButton2.IsButtonDown(Buttons.Start)) player2 = true;

            //allows player 2 to take themselves out of the game
            if (currentButton2.IsButtonDown(Buttons.B) && !prevButton2.IsButtonDown(Buttons.B) && player2 == true && player2Ready == false) player2 = false;


            if (player2 == true && !player2Ready)
            {
                //player 2 charcter selection 
                if (currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickRight)) characterMover2 += 1;
                if (currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickLeft)) characterMover2 -= 1;
                if (currentButton2.IsButtonDown(Buttons.DPadRight) && !prevButton2.IsButtonDown(Buttons.DPadRight)) characterMover2 += 1;
                if (currentButton2.IsButtonDown(Buttons.DPadLeft) && !prevButton2.IsButtonDown(Buttons.DPadLeft)) characterMover2 -= 1;
                if (characterMover2 > 5) characterMover2 = 1;
                if (characterMover2 < 1) characterMover2 = 5;

                //talk
                if (characterMover2 == 5 && currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickRight) ||
                    characterMover2 == 5 && currentButton2.IsButtonDown(Buttons.DPadRight) && !prevButton2.IsButtonDown(Buttons.DPadRight))
                    thistleName.Play(soundEffectVolume, 0, 0);
                if (characterMover2 == 5 && currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                   characterMover2 == 5 && currentButton2.IsButtonDown(Buttons.DPadLeft) && !prevButton2.IsButtonDown(Buttons.DPadLeft))
                    thistleName.Play(soundEffectVolume, 0, 0);

                if (characterMover2 == 4 && currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickRight) ||
                    characterMover2 == 4 && currentButton2.IsButtonDown(Buttons.DPadRight) && !prevButton2.IsButtonDown(Buttons.DPadRight))
                    tamName.Play(soundEffectVolume, 0, 0);
                if (characterMover2 == 4 && currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                   characterMover2 == 4 && currentButton2.IsButtonDown(Buttons.DPadLeft) && !prevButton2.IsButtonDown(Buttons.DPadLeft))
                    tamName.Play(soundEffectVolume, 0, 0);

                if (characterMover2 == 3 && currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickRight) ||
                   characterMover2 == 3 && currentButton2.IsButtonDown(Buttons.DPadRight) && !prevButton2.IsButtonDown(Buttons.DPadRight))
                    nessieName.Play(soundEffectVolume, 0, 0);
                if (characterMover2 == 3 && currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                   characterMover2 == 3 && currentButton2.IsButtonDown(Buttons.DPadLeft) && !prevButton2.IsButtonDown(Buttons.DPadLeft))
                    nessieName.Play(soundEffectVolume, 0, 0);

                if (characterMover2 == 2 && currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickRight) ||
                   characterMover2 == 2 && currentButton2.IsButtonDown(Buttons.DPadRight) && !prevButton2.IsButtonDown(Buttons.DPadRight))
                    cooName.Play(soundEffectVolume, 0, 0);
                if (characterMover2 == 2 && currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                   characterMover2 == 2 && currentButton2.IsButtonDown(Buttons.DPadLeft) && !prevButton2.IsButtonDown(Buttons.DPadLeft))
                    cooName.Play(soundEffectVolume, 0, 0);

                if (characterMover2 == 1 && currentButton2.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickRight) ||
                   characterMover2 == 1 && currentButton2.IsButtonDown(Buttons.DPadRight) && !prevButton2.IsButtonDown(Buttons.DPadRight))
                    bruName.Play(soundEffectVolume, 0, 0);
                if (characterMover2 == 1 && currentButton2.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton2.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                   characterMover2 == 1 && currentButton2.IsButtonDown(Buttons.DPadLeft) && !prevButton2.IsButtonDown(Buttons.DPadLeft))
                    bruName.Play(soundEffectVolume, 0, 0);               
            }

            //alows player two to become ready and for player one to start the game 
            if (currentButton2.IsButtonDown(Buttons.A) && !prevButton2.IsButtonDown(Buttons.A) && player2) player2Ready = true;

                //if (currentButton2.IsButtonDown(Buttons.A) && !prevButton2.IsButtonDown(Buttons.A) && player2) player2Ready = true;
                if (currentButton2.IsButtonDown(Buttons.B) && !prevButton2.IsButtonDown(Buttons.B) && player2)
            {
                player2Ready = false;
            }

            //takes the player back to the main menu and resets player 2
            if (currentButton.IsButtonDown(Buttons.B) && !prevButton.IsButtonDown(Buttons.B))
            {
                player2 = false;
                currentGameState = Gamestate.mainMenu;
            }
            //character selection animation 
            characterSelec.Update(gameTime.ElapsedGameTime.Milliseconds);
            characterSelec2.Update(gameTime.ElapsedGameTime.Milliseconds);

            //charcater selection for player 1
            if (characterMover == 1)
            {
                characterSelec.currentAnimation = 0;
                characterSelec.position = new Vector2(200, screenHeight / 4);
                characterChoice = 0; //character 1 will be used in game 
                charHuds.currentAnimation = 0;
            }
            if (characterMover == 2)
            {
                characterSelec.currentAnimation = 1;
                characterSelec.position = new Vector2(225, screenHeight / 4);
                characterChoice = 1; //character 2 will be used in game 
                charHuds.currentAnimation = 1;
            }
            if (characterMover == 3)
            {
                characterSelec.currentAnimation = 2;
                characterSelec.position = new Vector2(210, screenHeight / 4);
                characterChoice = 2; //character 3 will be used in game 
                charHuds.currentAnimation = 2;
            }
            if (characterMover == 4)
            {
                characterSelec.currentAnimation = 3;
                characterSelec.position = new Vector2(220, screenHeight / 4);
                characterChoice = 3; //character 4 will be used in game 
                charHuds.currentAnimation = 3;

            }
            if (characterMover == 5)
            {
                characterSelec.currentAnimation = 4;
                characterSelec.position = new Vector2(220, screenHeight / 4);
                characterChoice = 4; //character 5 will be used in game 
                charHuds.currentAnimation = 4;
            }

            if (player2 == true)
            {
                //player 2 character selection
                if (characterMover2 == 1)
                {
                    characterSelec2.currentAnimation = 0;
                    characterSelec2.position = new Vector2(screenWidth / 2 + 610, screenHeight / 4);
                    characterChoice2 = 0;
                    charHuds2.currentAnimation = 0;
                }
                if (characterMover2 == 2)
                {
                    characterSelec2.currentAnimation = 1;
                    characterSelec2.position = new Vector2(screenWidth / 2 + 635, screenHeight / 4);
                    characterChoice2 = 1;
                    charHuds2.currentAnimation = 1;
                }
                if (characterMover2 == 3)
                {
                    characterSelec2.currentAnimation = 2;
                    characterSelec2.position = new Vector2(screenWidth / 2 + 620, screenHeight / 4);
                    characterChoice2 = 2;
                    charHuds2.currentAnimation = 2;
                }
                if (characterMover2 == 4)
                {
                    characterSelec2.currentAnimation = 3;
                    characterSelec2.position = new Vector2(screenWidth / 2 + 630, screenHeight / 4);
                    characterChoice2 = 3;
                    charHuds2.currentAnimation = 3;
                }
                if (characterMover2 == 5)
                {
                    characterSelec2.currentAnimation = 4;
                    characterSelec2.position = new Vector2(screenWidth / 2 + 630, screenHeight / 4);
                    characterChoice2 = 4;
                    charHuds2.currentAnimation = 4;
                }
            }

            //bagpipe animation
            bagpipePlayer.Update(gameTime.ElapsedGameTime.Milliseconds);

            //bagpipe player movement
            bagpipePlayer.velocity.X = 0.5f;
            //bagpipe player reaches the edge of the screen reset him
            if (bagpipePlayer.position.X > screenWidth)
            {
                bagpipePlayer.position.X = -80;
                bagpipePlayer.position.Y = 500;
            }
            //bagpipe player stays ontop of the black outline
            if (bagpipePlayer.position.X > screenWidth / 2) bagpipePlayer.position.Y += 0.01f;

            //timer to make the start flash in and out of transparency
            if (shade >= 1) shadeSwitch = true;
            if (shade <= 0) shadeSwitch = false;
            if (shadeSwitch) shade -= 0.01f;
            if (!shadeSwitch) shade += 0.01f;

            //starts the game and changed the draw method
            if (currentButton.IsButtonDown(Buttons.A) && !prevButton.IsButtonDown(Buttons.A) && !player2)
            {                             
                currentGameState = Gamestate.controllerMenu;              
            }

            //starts the game and changed the draw method
            if (currentButton.IsButtonDown(Buttons.A) && !prevButton.IsButtonDown(Buttons.A) && player2 && player2Ready)
            {
                currentGameState = Gamestate.controllerMenu;
            }
        }

        private void updateMainMenu(GameTime gameTime)
        {
            //highscore menu being selected 
            if (highscore.currentAnimation == 1 && currentButton.IsButtonDown(Buttons.A) && !prevButton.IsButtonDown(Buttons.A)) currentGameState = Gamestate.highScoreMenu;

            player2 = false;
            player2Ready = false;

            p1Score = 0;
            p2Score = 0;
      
            highscore.position = new Vector2(500, 0);

            if (highscore.currentAnimation == 0)
            {
                //moves the selection, eg from new game to options
                if (currentButton.IsButtonDown(Buttons.LeftThumbstickRight) && !prevButton.IsButtonDown(Buttons.LeftThumbstickRight))
                {
                    menuMover += 1;
                    blipSound.Play(soundEffectVolume, 0, 0);
                }
                if (currentButton.IsButtonDown(Buttons.LeftThumbstickLeft) && !prevButton.IsButtonDown(Buttons.LeftThumbstickLeft))
                {
                    menuMover -= 1;
                    blipSound.Play(soundEffectVolume, 0, 0);
                }
                if (currentButton.IsButtonDown(Buttons.DPadRight) && !prevButton.IsButtonDown(Buttons.DPadRight))
                {
                    menuMover += 1;
                    blipSound.Play(soundEffectVolume, 0, 0);
                }
                if (currentButton.IsButtonDown(Buttons.DPadLeft) && !prevButton.IsButtonDown(Buttons.DPadLeft))
                {
                    menuMover -= 1;
                    blipSound.Play(soundEffectVolume, 0, 0);
                }
                }

            if (currentButton.IsButtonDown(Buttons.LeftThumbstickUp) && !prevButton.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                highscore.currentAnimation = 1;
                blipSound.Play(soundEffectVolume, 0, 0);
            }
            if (currentButton.IsButtonDown(Buttons.DPadUp) && !prevButton.IsButtonDown(Buttons.DPadUp))
            {
                highscore.currentAnimation = 1;
                blipSound.Play(soundEffectVolume, 0, 0);
            }
            if (currentButton.IsButtonDown(Buttons.LeftThumbstickDown) && !prevButton.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                highscore.currentAnimation = 0;
                blipSound.Play(soundEffectVolume, 0, 0);
            }
            if (currentButton.IsButtonDown(Buttons.DPadDown) && !prevButton.IsButtonDown(Buttons.DPadDown))
            {
                highscore.currentAnimation = 0;
                blipSound.Play(soundEffectVolume, 0, 0);
            }

                //resets the mover so that the selections are looped
                if (menuMover > 3) menuMover = 0;
            if (menuMover < 0) menuMover = 3;

            if (highscore.currentAnimation == 0)
            {
                //exits the game if the exit option is selected
                if (menuMover == 3 && GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                {
                    //saving the achievement
                    string outputFile = "achi1.txt";
                    StreamWriter outputFileStream = new StreamWriter(outputFile);
                    outputFileStream.WriteLine(timesBossHasDied);
                    outputFileStream.Close();
                    //achi 2
                    string outputFile2 = "achi2.txt";
                    StreamWriter outputFileStream2 = new StreamWriter(outputFile2);
                    outputFileStream2.WriteLine(bulletsFired);
                    outputFileStream2.Close();
                    //achi 3
                    string outputFile3 = "achi3.txt";
                    StreamWriter outputFileStream3 = new StreamWriter(outputFile3);
                    outputFileStream3.WriteLine(timesCRMSaved);
                    outputFileStream3.Close();
                    //achi 4
                    string outputFile4 = "achi4.txt";
                    StreamWriter outputFileStream4 = new StreamWriter(outputFile4);
                    outputFileStream4.WriteLine(timesPlayerDied);
                    outputFileStream4.Close();
                    //achi 5
                    string outputFile5 = "achi5.txt";
                    StreamWriter outputFileStream5 = new StreamWriter(outputFile5);
                    outputFileStream5.WriteLine(timesPlayerJumps);
                    outputFileStream5.Close();
                    //achi 6
                    string outputFile6 = "achi6.txt";
                    StreamWriter outputFileStream6 = new StreamWriter(outputFile6);
                    outputFileStream6.WriteLine(totalEnemiesKilled);
                    outputFileStream6.Close();
                    //exits the game
                    this.Exit();
                }
                //moves the screen onto the character selection
                if (menuMover == 1 && GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)) currentGameState = Gamestate.characterSelection;
                //moves the screen to the options menu
                if (menuMover == 0 && GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)) currentGameState = Gamestate.optionsMenu;
                //moves the screen to the achievements menu
                if (menuMover == 2 && GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)) currentGameState = Gamestate.achievMenu;
                //takes the player back to the start menu
                if (currentButton.IsButtonDown(Buttons.B) && !prevButton.IsButtonDown(Buttons.B)) currentGameState = Gamestate.pressStartMenu;
            }

            //bagpipe animation
            bagpipePlayer.Update(gameTime.ElapsedGameTime.Milliseconds);
            //bagpipe player movement
            bagpipePlayer.velocity.X = 0.5f;
            //bagpipe player reaches the edge of the screen reset him
            if (bagpipePlayer.position.X > screenWidth)
            {
                bagpipePlayer.position.X = -80;
                bagpipePlayer.position.Y = 500;
            }
            //bagpipe player stays ontop of the black outline
            if (bagpipePlayer.position.X > screenWidth / 2) bagpipePlayer.position.Y += 0.01f;


        }

        private void updatePressStartMenu(GameTime gameTime)
        {
            //timer to make the start flash in and out of transparency
            if (shade >= 1) shadeSwitch = true;
            if (shade <= 0) shadeSwitch = false;
            if (shadeSwitch) shade -= 0.01f;
            if (!shadeSwitch) shade += 0.01f;
            //allows for the next screen to become active(main menu screen)
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start)) currentGameState = Gamestate.mainMenu;
        }

        protected override void Draw(GameTime gameTime)
        {
            //different begin methods 
            if (inGameSpriteChange == 0)
                spriteBatch.Begin();
            //camera

            //what the camera follows
            //is player 1 dies it follows player 2
            if (!isPlayerDead) gameCamera.cameraPos.X = p1.position.X;
            if (isPlayerDead && player2) gameCamera.cameraPos.X = p2.position.X;
            if (p1.position.X > 21000) gameCamera.cameraPos.X = 21000;
            if (isPlayerDead && p2.position.X > 21000) gameCamera.cameraPos.X = 21000;
            if (currentGameState == Gamestate.inGameLevel2) gameCamera.cameraPos.X = 960;

            if (inGameSpriteChange == 1)
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, gameCamera.getMatrix());

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //switches between the different draws
            switch (currentGameState)
            {
                //draws the first screen you see
                case Gamestate.pressStartMenu:
                    drawPressStartMenu();
                    break;
                //draws the main menu eg new game, options, achievments ect.
                case Gamestate.mainMenu:
                    drawMainMenu();
                    break;
                //draws the options menu
                case Gamestate.optionsMenu:
                    drawOptionsMenu();
                    break;
                //draws the character selection screen
                case Gamestate.characterSelection:
                    drawCharacterSelection();
                    break;
                //draws the achievments screen
                case Gamestate.achievMenu:
                    drawAchievements();
                    break;
                //draws the inGame screen
                case Gamestate.inGame:
                    drawInGame();
                    break;
                //draws the highscore menu 
                case Gamestate.highScoreMenu:
                    drawHighscore();
                    break;
                //draws level 2 screen 
                case Gamestate.inGameLevel2:
                    drawLevel2();
                    break;
                //draws the controller scheme
                case Gamestate.controllerMenu:
                    drawControllerMenu();

                    break;

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void drawControllerMenu()
        {
            //charles face
            spriteBatch.Draw(mainMenuImage, new Vector2(0, 0), Color.White);

            //b
            spriteBatch.Draw(Bimage, new Vector2(1800, 1020));
            //a
            spriteBatch.Draw(Aimage, new Vector2(30, 1020));

            //controller
            spriteBatch.Draw(controllerImage, new Vector2(-50, 0), Color.White);
            if (player2) spriteBatch.Draw(controllerImage, new Vector2(1170, 0), Color.White);

            //bagpipe player animation
            bagpipePlayer.Draw(spriteBatch);

            Color starTrans = Color.White * shade;
            spriteBatch.Draw(pressStartImage, new Vector2(screenWidth / 2 - pressStartImage.Width / 2, 1000), starTrans);
        }

        private void drawAchievements()
        {
            //charles face
            spriteBatch.Draw(mainMenuImage, new Vector2(0, 0), Color.White);

            //b
            spriteBatch.Draw(Bimage, new Vector2(1800, 1020));
            //a
            spriteBatch.Draw(Aimage, new Vector2(30, 1020));

            //placement of the options eg new game ect
            spriteBatch.Draw(optionsImage, new Vector2(0, 900), Color.White);
            spriteBatch.Draw(newGameImage, new Vector2(300, 900), Color.White);
            spriteBatch.Draw(achievImage, new Vector2(1620 - achievImage.Width, 900), Color.White);
            spriteBatch.Draw(exitImage, new Vector2(screenWidth - exitImage.Width, 900), Color.White);

            //bagpipe player animation
            bagpipePlayer.Draw(spriteBatch);

            //achievements and there completed versions 
            if (timesBossHasDied > 0) achi1.currentAnimation = 1;
            if (bulletsFired > 19) achi2.currentAnimation = 1;
            if (timesCRMSaved > 0) achi3.currentAnimation = 1;
            if (timesPlayerDied > 19) achi4.currentAnimation = 1;
            if (timesPlayerJumps > 49) achi5.currentAnimation = 1;
            if (totalEnemiesKilled > 19) achi6.currentAnimation = 1;
            achi1.Draw(spriteBatch);
            achi2.Draw(spriteBatch);
            achi3.Draw(spriteBatch);
            achi4.Draw(spriteBatch);
            achi5.Draw(spriteBatch);
            achi6.Draw(spriteBatch);
        }

        private void drawLevel2()
        {
            //background aka the sky 
            backGround.Draw(spriteBatch, gameCamera);

            //alllows for the space background to come from being transparent to not
            Color spaceTrans = Color.White * spaceShade;

            //space background 
            spriteBatch.Draw(spaceBackgroundImage, spacePosition, spaceTrans);

            Color shortTrans = Color.White * textShade;

            //text for the shortbraed shower
            if (stage2Counter > -12500) spriteBatch.Draw(shortBreadTextImage, shortbreadTextPosition, shortTrans);

            //background for the score
            parchment.Draw(spriteBatch);

            //red health
            redHealth.Draw(spriteBatch);
            if (player2) redHealth2.Draw(spriteBatch);

            //green health
            //each health bar
            foreach (GameSprite health in playerHealths)
            {
                health.Draw(spriteBatch);
            }
            if (player2)
                foreach (GameSprite health2 in playerHealths2)
                {
                    health2.Draw(spriteBatch);
                }

            //player ui
            p1Ui.Draw(spriteBatch);
            if (player2) p2Ui.Draw(spriteBatch);

            //player heads for the ui
            p1Heads.Draw(spriteBatch);
            if (player2) p2Heads.Draw(spriteBatch);

            //red health for the boss
            redHealth3.Draw(spriteBatch);
            //green health for the boss
            if (bossHud.position.Y == 870)
            {
                foreach (GameSprite bossH in bossHealths)
                {
                    bossH.Draw(spriteBatch);
                }
            }
            //boss ui 
            bossHud.Draw(spriteBatch);
            

            //player 1 bullets
            for (int c = 0; c < flyingBullets.Count; c++)
            {
                flyingBullets[c].Draw(spriteBatch);
            }

            //shortbread shower
            foreach (GameSprite s1 in shower1)
            {
                s1.Draw(spriteBatch);
            }
            foreach (GameSprite s2 in shower2)
            {
                s2.Draw(spriteBatch);
            }
            foreach (GameSprite s3 in shower3)
            {
                s3.Draw(spriteBatch);
            }
            foreach (GameSprite s4 in shower4)
            {
                s4.Draw(spriteBatch);
            }



            //player 1 score
            spriteBatch.DrawString(font, "" + p1Score, p1ScoreFlyingPosition, Color.White);
            if (player2) spriteBatch.DrawString(font, "" + p2Score, p2ScoreFlyingPosition, Color.White);





            //coin that sits under the players ui the fashing one 
            coin.Draw(spriteBatch);
            if (player2) coin2.Draw(spriteBatch);

            //number of coins players have collected that goes above their ui
            spriteBatch.DrawString(fontWhite, "X" + coinsCollected, coinCollectedPostion, Color.White);
            if (player2)
                spriteBatch.DrawString(fontWhite, coinsCollected2 + "X", coinCollectedPostion2, Color.White);

            //player 1 flying character 
            p1Flying.Draw(spriteBatch);

            //player 2 flying character 
            if (player2) p2Flying.Draw(spriteBatch);

            //flying enemies
            foreach (GameSprite flyingEnem in flyingEnemies)
            {
                flyingEnem.Draw(spriteBatch);
            }

            //flying coin 
            flyingCoin.Draw(spriteBatch);

            //rose collectable part 2
            rose2.Draw(spriteBatch);

            //boss bullets
            foreach (GameSprite bossB in bossBullet)
            {
                bossB.Draw(spriteBatch);
            }

            //boss
            boss.Draw(spriteBatch);
        }

        private void drawHighscore()
        {
            //charles face
            spriteBatch.Draw(mainMenuImage, new Vector2(0, 0), Color.White);

            //b
            spriteBatch.Draw(Bimage, new Vector2(1800, 1020));
            //a
            spriteBatch.Draw(Aimage, new Vector2(30, 1020));

            //bagpipe player animation
            bagpipePlayer.Draw(spriteBatch);

            // Draw top ten high scores
            for (int i = 0; i < numberofhighscores; i++)
            {
                if (highscorenames[i].Length >= 24)
                    spriteBatch.DrawString(font2, (i + 1).ToString("0") + ". " + highscorenames[i].Substring(0, 24), new Vector2(60, 100 + (i * 30)),
                        Color.White, MathHelper.ToRadians(0), new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                else
                    spriteBatch.DrawString(font2, (i + 1).ToString("0") + ". " + highscorenames[i], new Vector2(60, 100 + (i * 30)),
                        Color.White, MathHelper.ToRadians(0), new Vector2(0, 0), 1f, SpriteEffects.None, 0);

                spriteBatch.DrawString(font2, highscores[i].ToString("0"), new Vector2(screenWidth - 180, 100 + (i * 30)),
                    Color.White, MathHelper.ToRadians(0), new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }

            if (p1Score + p2Score > highscores[numberofhighscores - 1])
            {
                spriteBatch.DrawString(font2, "New High Score Enter Name", new Vector2(screenWidth / 2 - (int)(font2.MeasureString("New High Score Enter Name").X * (1f / 2f)), 600),
                        Color.Blue, MathHelper.ToRadians(0), new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font2, highscorenames[numberofhighscores - 1], new Vector2(screenWidth / 2 - (int)(font2.MeasureString("New High Score Enter Name").X * (1f / 2f)), 640),
                        Color.AliceBlue, MathHelper.ToRadians(0), new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            }
        }

        private void drawInGame()
        {
            //sky
            backGround.Draw(spriteBatch, gameCamera);
            //hills
            midGround.Draw(spriteBatch, gameCamera);

            //red health nehind the UI
            redHealth.Draw(spriteBatch);
            if (player2) redHealth2.Draw(spriteBatch);
            //each health bar
            foreach (GameSprite health in playerHealths)
            {
                health.Draw(spriteBatch);
            }

            if (player2)
                foreach (GameSprite health2 in playerHealths2)
                {
                    health2.Draw(spriteBatch);
                }


            //p1 UI
            p1Ui.Draw(spriteBatch);

            //p2 UI
            if (player2) p2Ui.Draw(spriteBatch);

            //player heads
            p1Heads.Draw(spriteBatch);
            if (player2) p2Heads.Draw(spriteBatch);

            //parchment background for the score to sit on 
            parchment.Draw(spriteBatch);

            //player 1 score
            spriteBatch.DrawString(font, "" + p1Score, p1ScorePosition, Color.White);
            //player 2 score
            if (player2) spriteBatch.DrawString(font, p2Score + "", p2ScorePosition, Color.White);

            //tablet platforms 
            foreach (GameSprite tab in tabPlatforms)
            {
                tab.Draw(spriteBatch);
            }



            //waves
            foreach (GameSprite waves in irnBruWaves)
            {
                waves.Draw(spriteBatch);
            }

            //ground before lake
            for (int counter = 0; counter < 6; counter++)
            {
                spriteBatch.Draw(groundImage, new Vector2(1786 * counter, screenHeight / 2 + 460), Color.White);
            }



            //shortbread road before parkour platforms 
            for (int counter = 0; counter < 6; counter++)
            {
                spriteBatch.Draw(shortbreadRoadImage, new Vector2(1786 * counter, screenHeight / 2 + 350), Color.White);

            }



            //shortbread road after parkour platforms 
            for (int counter = 0; counter < 1; counter++)
            {
                spriteBatch.Draw(shortbreadRoadImage, new Vector2(1786 * counter + 17500, screenHeight / 2 + 350), Color.White);

            }

            //ground after you kill the boss          
            for (int counter = 0; counter < 3; counter++)
            {
                spriteBatch.Draw(groundImage, new Vector2(-22500 * counter, screenHeight / 2 + 460), Color.White);
            }

            if (bossKilled)
                //shortbread road after you kill the boss         
                for (int counter = 0; counter < 3; counter++)
                {
                    spriteBatch.Draw(shortbreadRoadImage, new Vector2(-20000 * counter + 17500, screenHeight / 2 + 350), Color.White);

                }

            //villager
            villager.Draw(spriteBatch);


            //brings up the villager speach
            if (p1.position.X > 800 && p1.position.X < 1200)
            {
                villagerBig.Draw(spriteBatch);
                speech.Draw(spriteBatch);
            }

            //monster 
            monster.Draw(spriteBatch);
            //monster 2
            monster2.Draw(spriteBatch);

            //crm
            crm.Draw(spriteBatch);
            //rose cracking 
            roseCrack.Draw(spriteBatch);
            //explosion
            explo.Draw(spriteBatch);
            //player 1
            p1.Draw(spriteBatch);
            //player 2 
            if (player2) p2.Draw(spriteBatch);
            //boss that has been killed 
            spriteBatch.Draw(bossDeathImage, new Vector2(-22000, 750), Color.White);

            //player bullets and energy
            playersBulletsAndEnergy.Draw(spriteBatch);
            if (player2) playersBulletsAndEnergy2.Draw(spriteBatch);
            //amount of bullets each player has
            spriteBatch.DrawString(font2, "" + bulletNumber, fontPosition, Color.White);
            spriteBatch.DrawString(font2, "" + energyNumber, fontPositionEnergy, Color.White);
            //player 2
            if (player2)
            {
                spriteBatch.DrawString(font2, "" + bulletNumber2, fontPosition2, Color.White);
                spriteBatch.DrawString(font2, "" + energyNumber2, fontPositionEnergy2, Color.White);
            }

            //the waves that the player falls betwen
            foreach (GameSprite fallBetweenWaves in irnBruWaves2)
            {
                fallBetweenWaves.Draw(spriteBatch);
            }

            //ground after lake
            for (int counter = 0; counter < 1; counter++)
            {
                spriteBatch.Draw(groundImage, new Vector2(1786 * counter + 17500, screenHeight / 2 + 460), Color.White);
            }









            //players bullets
            playerBullet.Draw(spriteBatch);
            if (player2) player2Bullet.Draw(spriteBatch);

            //energy collectable
            foreach (GameSprite eCollect in energyCollect)
            {
                eCollect.Draw(spriteBatch);
            }

            //bullets collectable
            foreach (GameSprite bCollect in bulletCollect)
            {
                bCollect.Draw(spriteBatch);
            }

            //coins 
            foreach (GameSprite c in coins)
            {
                c.Draw(spriteBatch);
            }

            //rose to collect part 1
            rose1.Draw(spriteBatch);

            //coin that sits under the players ui
            coin.Draw(spriteBatch);
            if (player2) coin2.Draw(spriteBatch);

            //position the flashing coin stays at (above the player 1, 2 ui)
            if (!isPlayerDead)
            {
                coin.position = new Vector2(p1.position.X - 780, 10);
                coin2.position = new Vector2(p1.position.X + 730, 10);
            }
            if (isPlayerDead && player2)
            {
                coin.position = new Vector2(p2.position.X - 780, 10);
                coin2.position = new Vector2(p2.position.X + 730, 10);
            }



            //number of coins players have collected that goes above their ui
            spriteBatch.DrawString(fontWhite, "X" + coinsCollected, coinCollectedPostion, Color.White);
            if (player2) spriteBatch.DrawString(fontWhite, coinsCollected2 + "X", coinCollectedPostion2, Color.White);




            //enemies 
            foreach (Enemy enem in enemies)
            {
                enem.Draw(spriteBatch);
            }

            //allows the color to become transparent 
            Color redtrans = Color.White * shade;
            spriteBatch.Draw(jumpImage, new Vector2(19500, screenHeight / 2), redtrans);

            //should be commented out unless you want to see what the player is walking on 
            //foreach (GameSprite trans in transPlatforms)
            //{
            //    trans.Draw(spriteBatch);
            //}


        }

        private void drawOptionsMenu()
        {
            //charles face
            spriteBatch.Draw(mainMenuImage, new Vector2(0, 0), Color.White);

            //b
            spriteBatch.Draw(Bimage, new Vector2(1800, 1020));
            //a
            spriteBatch.Draw(Aimage, new Vector2(30, 1020));

            //placement of the options eg new game ect
            spriteBatch.Draw(optionsImage, new Vector2(0, 900), Color.White);
            spriteBatch.Draw(newGameImage, new Vector2(300, 900), Color.White);
            spriteBatch.Draw(achievImage, new Vector2(1620 - achievImage.Width, 900), Color.White);
            spriteBatch.Draw(exitImage, new Vector2(screenWidth - exitImage.Width, 900), Color.White);

            //bagpipe player animation
            bagpipePlayer.Draw(spriteBatch);

            //volume that isnt selected
            for (int volumeBlankCounter = 0; volumeBlankCounter < 10; volumeBlankCounter++)
            {
                spriteBatch.Draw(volumeBlankImage, new Vector2(50 * volumeBlankCounter + 50, 300), Color.White);
            }
            //volume that is selected
            for (int volumeFullCounter = 0; volumeFullCounter < volumeAmount; volumeFullCounter++)
            {
                spriteBatch.Draw(volumeFullImage, new Vector2(50 * volumeFullCounter + 50, 300), Color.White);
            }
            //volume that isnt selected
            for (int volumeBlankCounter = 0; volumeBlankCounter < 10; volumeBlankCounter++)
            {
                spriteBatch.Draw(volumeBlankImage, new Vector2(50 * volumeBlankCounter + 1325, 300), Color.White);
            }
            //volume that is selected
            for (int volumeFullCounter = 0; volumeFullCounter < volumeSoundAmount; volumeFullCounter++)
            {
                spriteBatch.Draw(volumeFullImage, new Vector2(50 * volumeFullCounter + 1325, 300), Color.White);
            }

            if (volumeMover == 0)
                spriteBatch.Draw(optionsRectangleImage, new Vector2(48, 301), Color.White);
            else
                spriteBatch.Draw(optionsRectangleImage, new Vector2(1323, 301), Color.White);

            spriteBatch.Draw(optionsTextImage, new Vector2(40, 100), Color.White);
            spriteBatch.Draw(soundsTextImage, new Vector2(1250, 100), Color.White);
        }

        private void drawCharacterSelection()
        {
            //charles face
            spriteBatch.Draw(mainMenuImage, new Vector2(0, 0), Color.White);

            //b
            spriteBatch.Draw(Bimage, new Vector2(1800, 1020));
            //a
            spriteBatch.Draw(Aimage, new Vector2(30, 1020));

            //bagpipe player animation
            bagpipePlayer.Draw(spriteBatch);

            Color startTrans = Color.White * shade;
            if (!player2) spriteBatch.Draw(player2StartImage, new Vector2(1450, 300), startTrans);

            //placement of the options eg new game ect
            spriteBatch.Draw(optionsImage, new Vector2(0, 900), Color.White);
            spriteBatch.Draw(newGameImage, new Vector2(300, 900), Color.White);
            spriteBatch.Draw(achievImage, new Vector2(1620 - achievImage.Width, 900), Color.White);
            spriteBatch.Draw(exitImage, new Vector2(screenWidth - exitImage.Width, 900), Color.White);

            //different characters for selection
            characterSelec.Draw(spriteBatch);

            //character huds
            charHuds.Draw(spriteBatch);
            if (player2) charHuds2.Draw(spriteBatch);

            //player 2
            if (player2 == true)
            {
                characterSelec2.Draw(spriteBatch);
                //player 2 arrows 
                spriteBatch.Draw(arrows1Image, new Vector2(screenWidth / 2 + 500, 340), Color.White);
            }


            //arrows beside the player 
            //player 1 arrows 
            spriteBatch.Draw(arrows1Image, new Vector2(90, 340), Color.White);

            if (player2Ready)
            spriteBatch.Draw(readyImage, new Vector2(1500, 200), Color.White);

            

        }

        private void drawMainMenu()
        {
            //charles face
            spriteBatch.Draw(mainMenuImage, new Vector2(0, 0), Color.White);

            //b
            spriteBatch.Draw(Bimage, new Vector2(1800, 1020));
            //a
            spriteBatch.Draw(Aimage, new Vector2(30, 1020));

            //options in the main menu that havent been selected 
            spriteBatch.Draw(optionsImage, new Vector2(0, 900), Color.White);
            spriteBatch.Draw(newGameImage, new Vector2(300, 900), Color.White);
            spriteBatch.Draw(achievImage, new Vector2(1620 - achievImage.Width, 900), Color.White);
            spriteBatch.Draw(exitImage, new Vector2(screenWidth - exitImage.Width, 900), Color.White);
            highscore.Draw(spriteBatch);

            if (highscore.currentAnimation == 0)
            {
                //main menu options that have been selected
                //options
                if (menuMover == 0)
                    spriteBatch.Draw(optionsSelectedImage, new Vector2(0, 900), Color.White);
                //new game
                if (menuMover == 1)
                    spriteBatch.Draw(newGameSelectedImage, new Vector2(300, 900), Color.White);
                //achievments
                if (menuMover == 2)
                    spriteBatch.Draw(achievSelectedImage, new Vector2(1620 - achievImage.Width, 900), Color.White);
                //exit
                if (menuMover == 3)
                    spriteBatch.Draw(exitSelectedImage, new Vector2(screenWidth - exitImage.Width, 900), Color.White);
            }

            //bagpipe player animation
            bagpipePlayer.Draw(spriteBatch);
        }

        private void drawPressStartMenu()
        {
            //allows the color to become transparent 
            Color whitetrans = Color.White * shade;
            spriteBatch.Draw(startMenuImage, new Vector2(0, 0), Color.White); //start menu image Rose
            spriteBatch.Draw(pressStartImage, new Vector2(screenWidth / 2 - pressStartImage.Width / 2, 1000), whitetrans);
        }
    }
}
