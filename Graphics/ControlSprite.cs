using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Subscriber.Graphics
{
   /// <summary>
   /// ControlSprite is a sprite that uses a windows form control to do its rendering
   /// </summary>
   public class ControlSprite : Sprite
   {

      /// <summary>
      /// The control that this sprite will draw with
      /// </summary>
      public Control Control;

      /// <summary>
      /// Creates a ControlSprite
      /// </summary>
      /// <param name="control">The control to use for rendering</param>
      public ControlSprite(Control control)
      {
         Control = control;
         setHeightWidth();
      }

      public override void Draw()
      {
         //Move the control to the correct location 
         Control.Location = new Point((int)(Location.X + .5f),(int)(Location.Y + .5f));

         //and redraw it
         Control.Refresh();
      }

      /// <summary>
      /// Sets the sprite height and width from the control
      /// </summary>
      protected void setHeightWidth()
      {
         //Need to set the sprite height and width for collisions to work
         //Note that if the control height or width is changed AFTER the sprite is created this will break
         //in that case you would need to hook the Resize event on the control to keep these up to date
         Size.Height = Control.Height;
         Size.Width = Control.Width;
      }

   }
}
