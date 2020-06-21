using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

partial class MyEventsProxy : ClientBase<ICloudBoxingService>, ICloudBoxingService
{
   public MyEventsProxy()
   {
   }

   public MyEventsProxy(string endpointConfigurationName)
         : base(endpointConfigurationName)
   {
   }

   public void Join(string player, string playerName)
   {
      Channel.Join(player,playerName);
   }

   public void OpponentMove(EnumMove opponentMove)
   {
      Channel.OpponentMove(opponentMove);
   }
}


