using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private const int _screenWidth = 750;

    private const int _screenHeight = 450;

    private const int _playerAreaEdgeLineWidth = 12, _ballWidthAndHeight = 21;

    private float _ballSpeed = 10f;

    private Vector2 _ballPosition, _ballDirection;

    private Rectangle _PlayArea;

    //loading the textures
    private Texture2D _background, _ball;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = _screenWidth;
        _graphics.PreferredBackBufferHeight = _screenHeight;
        _graphics.ApplyChanges();

        _ballPosition.X = 150;
        _ballPosition.Y = 195;
        _ballSpeed = 25;

        _ballDirection.X = 1;
        _ballDirection.Y = -1;

        _PlayArea = new(0,0, _screenWidth, _screenHeight);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _background = Content.Load<Texture2D>("Court");
        _ball = Content.Load<Texture2D>("Ball");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _ballPosition += _ballDirection * _ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_ballPosition.X <= _PlayArea.Left || _ballPosition.X >= _PlayArea.Right)
        {
            _ballDirection.X *= -1;
        }
        if(_ballPosition.Y >= _PlayArea.Top || _ballPosition.Y <= _PlayArea.Bottom)
        {
            _ballDirection.Y *= -1;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        var ballRect = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _ballWidthAndHeight, _ballWidthAndHeight);
        _spriteBatch.Draw(_background, _PlayArea, Color.White);
        _spriteBatch.Draw(_ball, ballRect, Color.White);


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
