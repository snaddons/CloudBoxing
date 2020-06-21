using System;
using System.Windows.Forms;
using System.ServiceModel;

class CloudBoxingService : ICloudBoxingService
{
   GameForm _cloudBoxing = Application.OpenForms[0] as GameForm;

   public void Join(string player,string playerName)
   {
      _cloudBoxing.Join(player,playerName);
   }

   public void OpponentMove(EnumMove opponentMove)
   {
      _cloudBoxing.OpponentMove(opponentMove);
   }
}