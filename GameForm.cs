using System;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Diagnostics;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
//using System.Security.Cryptography.X509Certificates;

public enum EnumMove
{
   Open,
   Block,
   Peek,
   LeftHook,
   RightHook,
   LeftBody,
   RightBody,
   Hit
}

[ServiceContract]
public interface ICloudBoxingService
{
   [OperationContract(IsOneWay=true)]
   void OpponentMove(EnumMove opponentMove);

   [OperationContract(IsOneWay=true)]
   void Join(string player, string playerName);
}

public interface ICloudBoxingChannel : ICloudBoxingService, IClientChannel { }

partial class GameForm : Form, ICloudBoxingService
{
   ServiceHost _host = null;
   ICloudBoxingChannel _proxy = null;
   EnumMove _playerMove = EnumMove.Open;
   bool _playerBlockingMove = false;
   EnumMove _opponentMove = EnumMove.Open;
   int _playerHitCount = 0;

   // Primary screen buffer references
   
   private Bitmap _doubleBuffer = null;
   private Graphics _graphics = null;
   private Graphics _screen = null;

   public GameForm()
   {
      InitializeComponent();
      //SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
      //SetStyle(ControlStyles.DoubleBuffer, false);

   }

   public void Join(string player, string playerName)
   {
      //Create proxy to Player2
      TransportClientEndpointBehavior relayCredentials = new TransportClientEndpointBehavior();
      relayCredentials.CredentialType = TransportClientCredentialType.UserNamePassword;
      relayCredentials.Credentials.UserName.UserName = "Outreal";
      relayCredentials.Credentials.UserName.Password = "abc123";
      
      Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", "Outreal", "CloudBoxing/Player2");
      ChannelFactory<ICloudBoxingChannel> channelFactory = new ChannelFactory<ICloudBoxingChannel>("ClientEndpoint", new EndpointAddress(serviceUri));
      channelFactory.Endpoint.Behaviors.Add(relayCredentials);
      _proxy = channelFactory.CreateChannel();
   }

   public void OpponentMove(EnumMove opponentMove)
   {
      switch(opponentMove)
      {
         case EnumMove.Block:      
            _opponentMove = EnumMove.Block;
            break;
         case EnumMove.Peek:
            _opponentMove = EnumMove.Peek;
            break;
         case EnumMove.LeftBody:
            _opponentMove = EnumMove.LeftBody;
            break;
         case EnumMove.LeftHook:
            _opponentMove = EnumMove.LeftHook;
            break;
         case EnumMove.RightBody:
            _opponentMove = EnumMove.RightBody;
            break;
         case EnumMove.RightHook:
            _opponentMove = EnumMove.RightHook;
            break;
         case EnumMove.Hit:
            break;
         default:
            _opponentMove = EnumMove.Open;
            break;
      }

      DrawOpponent();
   }

   private void DrawOpponent()
   {
      Bitmap image = null;
      Point point = new Point(0, 0);

      switch(_opponentMove)
      {
         case EnumMove.Peek: 
            image = CloudBoxing.Properties.Resources.OpponentBlockPeek;
            point = new Point(50,50);
            break;
         case EnumMove.Block:
            image = CloudBoxing.Properties.Resources.OpponentBlock;
            point = new Point(50,50);
            break;
         case EnumMove.LeftBody:
            image = CloudBoxing.Properties.Resources.OpponentLeftBody;
            point = new Point(10,100);
            break;
         case EnumMove.LeftHook:
            image = CloudBoxing.Properties.Resources.OpponentLeftHook;
            point = new Point(10,50);
            break;
         case EnumMove.RightBody:
            image = CloudBoxing.Properties.Resources.OpponentRightBody;
            point = new Point(100,10);
            break;
         case EnumMove.RightHook:
            image = CloudBoxing.Properties.Resources.OpponentRightHook;
            point = new Point(100,50);
            break;
         default:
            image = CloudBoxing.Properties.Resources.OpponentOpen;
            point = new Point(0,0);
            break;
      }

      image.MakeTransparent(Color.FromArgb(255, 255, 255));
       _graphics.DrawImage(image, point);
   }

   private void DrawPlayer()
   {
      switch(_playerMove)
      {
         case EnumMove.Peek: 
            DrawImage(CloudBoxing.Properties.Resources.PlayerLeftBlock, new Point(50,100));
            DrawImage(CloudBoxing.Properties.Resources.PlayerRightBlock, new Point(100,100));
            break;
         case EnumMove.Block:
            DrawImage(CloudBoxing.Properties.Resources.PlayerLeftBlock, new Point(50,100));
            DrawImage(CloudBoxing.Properties.Resources.PlayerRightBlock, new Point(260,100));
            break;
         case EnumMove.LeftBody:
            DrawImage(CloudBoxing.Properties.Resources.PlayerLeftBody, new Point(60,300));
            break;
         case EnumMove.LeftHook:
            DrawImage(CloudBoxing.Properties.Resources.PlayerLeftHook, new Point(80,50));
            break;
         case EnumMove.RightBody:
            DrawImage(CloudBoxing.Properties.Resources.PlayerRightBody, new Point(300,300));
            break;
         case EnumMove.RightHook:
            DrawImage(CloudBoxing.Properties.Resources.PlayerRightHook, new Point(250,50));
            break;
         default:
            DrawImage(CloudBoxing.Properties.Resources.PlayerLeftBlock, new Point(20,280));
            DrawImage(CloudBoxing.Properties.Resources.PlayerRightBlock, new Point(300,280));
            break;
      }
   }
   
   private void DrawImage(Bitmap image, Point point)
   {     
      image.MakeTransparent(Color.FromArgb(255, 255, 255));
      _graphics.DrawImage(image, point);
   }

   void OnClosed(object sender,FormClosedEventArgs e)
   {
      _proxy.Close();
   }

   private void GameForm_Load(object sender,EventArgs e)
   {
      _doubleBuffer = new Bitmap(550, 700);
      _graphics = Graphics.FromImage(_doubleBuffer);
      _screen = this.CreateGraphics();

      //Enable double buffering ensuring minimal flicker
      //SetStyle(ControlStyles.DoubleBuffer,true);
      //SetStyle(ControlStyles.AllPaintingInWmPaint, true);

      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, false);
   }

   private void Draw()
   {
      //DrawOpponent();
      DrawPlayer();
      //_screen.DrawImage(_doubleBuffer, new Point(0,0));

      Bitmap image = CloudBoxing.Properties.Resources.OpponentBlockPeek;

      _screen.DrawImage(image, new Point(0,0));
   }

   private void OpponentHit(PictureBox image, EnumMove playerMove)
   {
      bool headHit = false;
      bool bodyHit = false;

      headHit = (_opponentMove != EnumMove.Block) && (playerMove == EnumMove.LeftHook || playerMove == EnumMove.RightHook);
      bodyHit = (_opponentMove != EnumMove.Open ) && (playerMove == EnumMove.LeftBody || playerMove == EnumMove.RightBody);

      if(headHit || bodyHit)
      {
         _playerHitCount++;

         image.Image = HitImage();

         if (_proxy != null)
            _proxy.OpponentMove(EnumMove.Hit);

         _playerHitCount = 0;
      }
   }

   private Bitmap HitImage()
   {
      Bitmap hitImage = null;

      int hit = new Random().Next(1,4);

      switch(hit)
      {
         case 1:
            hitImage = CloudBoxing.Properties.Resources.biff;
            break;
         case 2:
            hitImage = CloudBoxing.Properties.Resources.Zap;
            break;
         case 3:
            hitImage = CloudBoxing.Properties.Resources.Kapow;
            break;
         case 4:
            hitImage = CloudBoxing.Properties.Resources.Argh;
            break;
      }

      return hitImage;
   }

   private void timer1_Tick(object sender,EventArgs e)
   {
      if(_playerBlockingMove)
      {
         if(_proxy != null)
         {
            int i = new Random().Next(1, 4);
            bool peek = (i > 2);

            if(peek)
            {
               _playerMove = EnumMove.Peek;
            }
            else
            {
               _playerMove = EnumMove.Block;
            }

            //if(_playerMove == EnumMove.Block)
            //   _playerMove = EnumMove.Peek;
            //else
            //   _playerMove  = EnumMove.Block;
            
            _proxy.OpponentMove(_playerMove);
         }
      }

      if(_opponentMove != EnumMove.Block && _opponentMove != EnumMove.Peek)
         _opponentMove= EnumMove.Open;
      else
      {
         if(_opponentMove == EnumMove.Block)
            _opponentMove = EnumMove.Block;
         else
            _opponentMove = EnumMove.Peek;
      }
   }

   #region Player Controls

   private void LeftBody()
   {
      if (_proxy!=null)
         _proxy.OpponentMove(EnumMove.LeftBody);

      _playerMove = EnumMove.LeftBody;
   }

   private void RightBody()
   {
      if (_proxy!=null)
         _proxy.OpponentMove(EnumMove.RightBody);

      _playerMove = EnumMove.RightBody;
   }

   private void BlockOpen()
   {
      if(_playerMove != EnumMove.Block && _playerMove != EnumMove.Peek)
         _playerMove = EnumMove.Block;
      else
         _playerMove = EnumMove.Open;

      if(_proxy != null)
         _proxy.OpponentMove(_playerMove);

      if(_playerMove == EnumMove.Block || _playerMove == EnumMove.Peek)
         _playerBlockingMove = true;
      else
         _playerBlockingMove = false;
   }

   private void LeftHook()
   {
      if (_proxy!=null)
         _proxy.OpponentMove(EnumMove.LeftHook);

      _playerMove = EnumMove.LeftHook;
   }

   private void RightHook()
   {
      if (_proxy!=null)
         _proxy.OpponentMove(EnumMove.RightHook);

      _playerMove = EnumMove.RightHook;
   }

   #endregion

   private void GameForm_KeyDown(object sender,KeyEventArgs e)
   {
      switch(e.KeyCode)
      {
         case Keys.Space: //Guard
            BlockOpen();

            break;
         case Keys.Q: //Left Hook
            LeftHook();

            break;
         case Keys.W: //Right Hook
            RightHook();

            break;
         case Keys.A: //Left Body
            LeftBody();

            break;
         case Keys.S: //Right Body
            RightBody();

            break;
         case Keys.Left: //Left Dodge
            break;
         case Keys.Right: //Right Dodge
            break;
      }

      Draw();
   }

   protected override CreateParams CreateParams 
   { 
      get 
      { 
        CreateParams cp = base.CreateParams; 
        cp.ExStyle |= 0x02000000; 
        return cp; 
      } 
    } 

   private void cToolStripMenuItem_Click(object sender,EventArgs e)
   {
      TransportClientEndpointBehavior relayCredentials = new TransportClientEndpointBehavior();
      relayCredentials.CredentialType = TransportClientCredentialType.UserNamePassword;
      relayCredentials.Credentials.UserName.UserName = "Outreal";
      relayCredentials.Credentials.UserName.Password = "abc123";

      Uri baseAddress = ServiceBusEnvironment.CreateServiceUri("sb", "Outreal", "CloudBoxing/Player1");

      _host = new ServiceHost(typeof(CloudBoxingService), baseAddress);
      _host.Description.Endpoints[0].Behaviors.Add(relayCredentials);
      _host.Open();
   }

   private void joinToolStripMenuItem_Click(object sender,EventArgs e)
   {
      //Create host

      TransportClientEndpointBehavior relayCredentials = new TransportClientEndpointBehavior();
      relayCredentials.CredentialType = TransportClientCredentialType.UserNamePassword;
      relayCredentials.Credentials.UserName.UserName = "Outreal";
      relayCredentials.Credentials.UserName.Password = "abc123";

      Uri baseAddress = ServiceBusEnvironment.CreateServiceUri("sb", "Outreal", "CloudBoxing/Player2");

      _host = new ServiceHost(typeof(CloudBoxingService), baseAddress);
      _host.Description.Endpoints[0].Behaviors.Add(relayCredentials);
      _host.Open();

      Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", "Outreal", "CloudBoxing/Player1");
      ChannelFactory<ICloudBoxingChannel> channelFactory = new ChannelFactory<ICloudBoxingChannel>("ClientEndpoint", new EndpointAddress(serviceUri));
      channelFactory.Endpoint.Behaviors.Add(relayCredentials);
      _proxy = channelFactory.CreateChannel();
      _proxy.Open();
      _proxy.Join("Player2","Stu");

      
      //Join server game
      _proxy.Join("p", "t");
   }

   private void GameForm_Paint(object sender,PaintEventArgs e)
   {
      Draw();
   }

   private void GameForm_FormClosed(object sender,FormClosedEventArgs e)
   {
      _screen.Dispose();
      _doubleBuffer.Dispose();
   }
}