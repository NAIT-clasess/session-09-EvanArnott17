using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Game1 : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450, _BallWidthAndHeight = 21;
    private const int _PlayAreaEdgeLineWidth = 12;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backgroundTexture, _ballTexture, _paddleTexture;

    private Rectangle _playAreaBoundingBox
    {
        get{
            return  new Rectangle(
            _PlayAreaEdgeLineWidth,
            _PlayAreaEdgeLineWidth,
            _WindowWidth- (_PlayAreaEdgeLineWidth*2),
            _WindowHeight - (_PlayAreaEdgeLineWidth * 2)
        );
        }

        set
        {
            
        }
    }

    private Vector2 _ballPosition, _ballDirection;
     private Vector2 _paddlePosition, _paddleDirection, _paddleDimensions, _paddleLeftPostion, _paddleLeftDirection;
     private const float _paddleSpeed = 200;
    private const float _ballSpeed = 160 ;
  //Example of read only Property
    Rectangle _ballRectangle=> new Rectangle((int)_ballPosition.X,(int)_ballPosition.Y,_BallWidthAndHeight,_BallWidthAndHeight); 
    Rectangle _paddleRect=> new Rectangle((int)_paddlePosition.X,(int)_paddlePosition.Y,_paddleWidth,_paddleHeight); 
    Rectangle _paddleLeftRect=> new Rectangle((int)_paddleLeftPostion.X,(int)_paddleLeftPostion.Y,_paddleWidth,_paddleHeight); 

#region  Properties Example
    int score;
    private const int _paddleWidth = 6, _paddleHeight = 55;

    int Score
    {
        set
        {
            score = value;
            ScoreHasUpdated();
        }
        get
        {
            var  result = score * score;
          return result;  
        }
    }
public void ScoreHasUpdated()
    {
            score *=2;
    }

    #endregion
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ballPosition.X = 150;
        _ballPosition.Y = 195;

        _ballDirection.X = -1;
        _ballDirection.Y = -1;
        //Score =30; // proterty assignment sample

        _paddlePosition = new Vector2(690, 198);
        _paddleDirection = Vector2.Zero;
        _paddleDimensions = new Vector2(_paddleWidth, _paddleHeight);

        _paddleLeftPostion = new Vector2(60,198);
        _paddleLeftDirection = Vector2.Zero;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _paddleTexture = Content.Load<Texture2D>("Paddle");
    }

    protected override void Update(GameTime gameTime)
    {
        #region Comented codes
       // _ballPosition += _ballDirection * _ballSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;

// #region Collision Detection Region
//         // bounce ball off left and right sides
//         if((_ballPosition.X- _BallWidthAndHeight*0.5f) <= _playAreaBoundingBox.Left ||
//            (_ballPosition.X + _BallWidthAndHeight*0.5f) >= _playAreaBoundingBox.Right)
//         {
//             _ballDirection.X *= -1;
//         }

//         // bounce ball off top and bottom
//         if((_ballPosition.Y- _BallWidthAndHeight*0.5f) <= _playAreaBoundingBox.Top ||
//            (_ballPosition.Y + _BallWidthAndHeight*0.5f) >= _playAreaBoundingBox.Bottom)
//         {
//             _ballDirection.Y *= -1;
//         }
// #endregion

//          _ballRectangle= new Rectangle((int) _ballPosition.X, (int) _ballPosition.Y, _BallWidthAndHeight, _BallWidthAndHeight);
#endregion

   
#region CollisinDetection
        if(_ballRectangle.Left<=_playAreaBoundingBox.Left || 
           _ballRectangle.Right >=_playAreaBoundingBox.Right) 
        {
            _ballDirection.X *= -1;
        }

         else if(_ballRectangle.Bottom>=_playAreaBoundingBox.Bottom || 
           _ballRectangle.Top<=_playAreaBoundingBox.Top) 
        {
            _ballDirection.Y *= -1;
        }
#endregion


#region  Paddle Controll

    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    KeyboardState kbState = Keyboard.GetState();

        if(kbState.IsKeyDown(Keys.Up))
        {
            _paddleDirection = new Vector2(0, -1);
        }
        else if(kbState.IsKeyDown(Keys.Down))
        {
            _paddleDirection = new Vector2(0, 1);
        }
        else
        {
            _paddleDirection = Vector2.Zero;
        }

        _paddlePosition += _paddleDirection * _paddleSpeed * dt;

        if(_paddlePosition.Y <= _playAreaBoundingBox.Top)
        {
            _paddlePosition.Y = _playAreaBoundingBox.Top;
        }
        else if(_paddlePosition.Y +_paddleDimensions.Y >= _playAreaBoundingBox.Bottom)
        {
            _paddlePosition.Y = _playAreaBoundingBox.Bottom-_paddleDimensions.Y;
        }

        if(kbState.IsKeyDown(Keys.W))
        {
            _paddleLeftDirection = new Vector2(0, -1);
        }
        else if(kbState.IsKeyDown(Keys.S))
        {
            _paddleLeftDirection = new Vector2(0, 1);
        }
        else
        {
            _paddleLeftDirection = Vector2.Zero;
        }

        _paddleLeftPostion += _paddleLeftDirection * _paddleSpeed * dt;

        if(_paddleLeftPostion.Y <= _playAreaBoundingBox.Top)
        {
            _paddleLeftPostion.Y = _playAreaBoundingBox.Top;
        }
        else if(_paddleLeftPostion.Y +_paddleDimensions.Y >= _playAreaBoundingBox.Bottom)
        {
            _paddleLeftPostion.Y = _playAreaBoundingBox.Bottom-_paddleDimensions.Y;
        }


        

#endregion
        _ballPosition+= _ballDirection * _ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);

        _spriteBatch.Draw(_ballTexture, _ballRectangle, Color.White);
        _spriteBatch.Draw(_paddleTexture, _paddleRect, Color.White);

        _spriteBatch.Draw(_paddleTexture, _paddleLeftRect, Color.White);


        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
