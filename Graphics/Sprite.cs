using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Subscriber.Graphics
{
   public class Sprite
   {
      public PointF Location;
      public SizeF Size;

      public Sprite()
      {
      }

      public Sprite(float x,float y,float width,float height)
      {
         Location.X = x;
         Location.Y = y;
         Size.Width = width;
         Size.Height = height;
      }

      public virtual void Update()
      {
         //Move the sprite based on the velocity
         //Location.X += Velocity.X * (float)elapsedTime;
         //Location.Y += Velocity.Y * (float)elapsedTime;
      }

      public virtual void Draw()
      {
         //Default behaviour right now is to do nothing. Inherited classes will do their own drawing
      }
   }
}
