using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Subscriber.Graphics
{
public class Player : ControlSprite
   {
      #region AI constants and variables

      //private const float _lookDistance = 300f;   //How close is the ball to the bat before the AI kicks in
      //private float _estimatedLocation = -1;      //A 'guess' at the Y position where the ball will hit the bat
      
      #endregion

      #region Look and feel
      
      public int Width {get;set;}
      public int Height {get;set;}

      public Bitmap PlayerImage {get;set;}

      public Bitmap LeftOpenImage {get;set;}
      public Bitmap LeftBodyImage {get;set;}
      public Bitmap LeftHookImage {get;set;}
      public Bitmap LeftBlockImage {get;set;}
      public Bitmap LeftPeekImage {get;set;}

      public Bitmap RightOpenImage {get;set;}
      public Bitmap RightBodyImage {get;set;}
      public Bitmap RightHookImage {get;set;}
      public Bitmap RightBlockImage {get;set;}
      public Bitmap RightPeekImage {get;set;}
   
      #endregion

      #region Keyboard controls and keyboard state for those keys

      private Keys _upKey;
      private Keys _downKey;
      private Keys _leftHookKey;
      private Keys _leftBodyKey;
      private Keys _rightHookKey;
      private Keys _rightBodyKey;
      private Keys _blockKey;

      private bool _isUpKeyPressed;
      private bool _isDownKeyPressed;
      private bool _leftHookKeyPressed;
      private bool _leftBodyKeyPressed;
      private bool _rightHookKeyPressed;
      private bool _rightBodyKeyPressed;
      private bool _BlockKeyPressed;

      #endregion

      private bool _isHuman;
      private static Random _random = new Random();
      public bool ActionRequired {get;set;}

      //This constructor is used to make a computer controlled bat
      public Player(Control control, int x, int y, int w, int h) : base(control)
      {
         initialize(control, x, y, w, h);

         _isHuman = false;
      }

      //This constructor is used to make a human controlled bat
      public Player(Control control,int x, int y, int w, int h, Keys up, Keys down, Keys block, Keys leftBody, Keys leftHook, Keys rightBody, Keys rightHook) : base(control)
      {
         initialize(control, x, y, w, h);

         //Keys to control this sprite 
         _upKey = up;
         _downKey = down;
         _leftBodyKey = leftBody;
         _leftHookKey = leftHook;
         _blockKey = block;
         _rightBodyKey = rightBody;
         _rightHookKey = rightHook;

         _isHuman = true;
      }

      //Shared construction code
      private void initialize(Control control, int x, int y, int w, int h)
      {
         //The x position is constant for each bat
         Location.X = x;
         Location.Y = y;

         //Look & feel
         control.Width = w;
         control.Height = h;

         setHeightWidth();
      }

      /// <summary>
      /// Moves the bat based on keyboard (for humans) or AI (for computer)
      /// </summary>
      /// <param name="gameTime">current time since start of game</param>
      /// <param name="elapsedTime">elapsed time since last frame</param>
      public override void Update()
      {
         if(_isHuman)
            humanMove();
         else
            computerMove();

         //Perform any animation
         base.Update();
      }

      private void computerMove()
      {
         ////Use this line to play perfectly. It moves the bat in sync with the ball
         ////Location.Y = Ball.Location.Y - Size.Height / 2;

         ////Use to AI to make the computer imperfect
         ////Only care if the ball is moving towards us
         ////NOTE: This AI assumes the computer is always the player on the right hand side of the screen
         //if(Ball.Velocity.X > 0)
         //{
         //   //Only care if the ball is close enough to use
         //   if(Location.X - Ball.Location.X < _lookDistance)
         //   {
         //      //Are we already moving toward a guessed location?
         //      if(_estimatedLocation > -1)
         //      {
         //         //Yes - Are we there yet
         //         if((Math.Sign(Velocity.Y) > 0 && Location.Y >= _estimatedLocation)
         //                   || (Math.Sign(Velocity.Y) < 0 && Location.Y <= _estimatedLocation))
         //         {
         //            //Stop moving and next time round we can find a new estimated location
         //            Velocity.Y = 0;
         //            _estimatedLocation = -1;
         //         }
         //         //else just keep on moving
         //      }
         //      else
         //      {
         //         //Don't allow correction when we are very close
         //         if(Location.X - Ball.Location.X > 50)
         //         {
         //            //Create an estimated location to head for and set the velocity to move us in that direction
         //            //Notice that this guess doesn't take into account bounces and has a random factor
         //            _estimatedLocation = Ball.Location.Y + (Location.X - Ball.Location.X) * Math.Sign(Ball.Velocity.Y) + _random.Next(100) - 50;

         //            if(_estimatedLocation > MaxPosition - Height)
         //               _estimatedLocation = MaxPosition - Height;
         //            if(_estimatedLocation < MinPosition)
         //               _estimatedLocation = MinPosition;

         //            if(_estimatedLocation > Location.Y)
         //            {
         //               Velocity.Y = (float)_speed;
         //            }
         //            else
         //            {
         //               Velocity.Y = -(float)_speed;
         //            }
         //         }
         //      }
         //   }
         //   else
         //   {
         //      //Else don't move
         //      Velocity.Y = 0;
         //      _estimatedLocation = -1;
         //   }

         //}
         //else
         //{
         //   //Else don't move
         //   Velocity.Y = 0;
         //   _estimatedLocation = -1;
         //}
      }

      private void humanMove()
      {
         //Set the velocity of the sprite based on which keys are pressed
         if(_isUpKeyPressed)
         {
            //velocity += -_speed;
         }
         
         if(_isDownKeyPressed)
         {
            //velocity += _speed;
         }

         if(_leftBodyKeyPressed)
         {
         }

         if(_leftHookKeyPressed)
         {
         }

         if(_BlockKeyPressed)
         {
         }

         if(_rightBodyKeyPressed)
         {
         }

         if(_rightHookKeyPressed)
         {
         }


      }

      public void KeyDown(Keys keys)
      {
         //If the key from the key down event matches the up or down key for this bat
         //then set the keyboard state to indicate that this key is currently being held down
         if(keys == _upKey)
            _isUpKeyPressed = true;

         if(keys == _downKey)
            _isDownKeyPressed = true;

         if(keys == _blockKey)
            _BlockKeyPressed = true;

         if(keys == _leftBodyKey)
            _leftBodyKeyPressed = true;

         if(keys == _leftHookKey)
            _leftHookKeyPressed = true;

         if(keys == _rightBodyKey)
            _rightBodyKeyPressed = true;

         if(keys == _rightHookKey)
            _rightHookKeyPressed = true;
      }

      public void KeyUp(Keys keys)
      {
         //If the key from the key down event matches the up or down key for this bat
         //then set the keyboard state to indicate that this key has been released
         if(keys == _upKey)
            _isUpKeyPressed = false;

         if(keys == _downKey)
            _isDownKeyPressed = false;

         if(keys == _blockKey)
            _BlockKeyPressed = false;

         if(keys == _leftBodyKey)
            _leftBodyKeyPressed = false;

         if(keys == _leftHookKey)
            _leftHookKeyPressed = false;

         if(keys == _rightBodyKey)
            _rightBodyKeyPressed = false;

         if(keys == _rightHookKey)
            _rightHookKeyPressed = false;
      }
   }
}
