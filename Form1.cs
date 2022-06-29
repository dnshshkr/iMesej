using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace iMesej
{
    public partial class Form1 : Form
    {
        public class Role
        {
            public IPAddress LocalAddress { get; set; }
            public Int32 PortNumber { get; set; }
            public string role { get; set; }
            public Role(string Role,string PortNumber)
            {
                role = Role;
                LocalAddress = IPAddress.Parse(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString());
                this.PortNumber = Int32.Parse(PortNumber);
            }
            public Role(string Role,string LocalAddress,string PortNumber)
            {
                role = Role;
                this.LocalAddress = IPAddress.Parse(LocalAddress);
                this.PortNumber = Int32.Parse(PortNumber);
            }
        }
        int CurrentY = 0, TextOverflowPaddingY = 13, TimeOverflowPaddingY = 17;
        const byte TextPadingY = 5, TextOverflowDefaultPaddingY = 13, TimeOverflowDefaultPaddingY = 17;
        static bool CreateRoomButtonState = false, JoinRoomButtonState = false, FirstTime = true;
        string ToRead, ToWrite, Side;
        List<string> Messages = new List<string>();
        TcpClient client = null;
        TcpListener server = null;
        Thread ClientThread = null, ServerThread = null;
        Role ClientRole = null;
        Role ServerRole = null;
        public Form1()
        {
            InitializeComponent();
            RefreshRateTimer.Interval = 500;
            RefreshRateTimer.Start();
            RoomIP.ReadOnly = true;
            JoinRoomButton.Enabled = false;
            CreateRoomButton.Enabled = false;
            MessageBox.Enabled = false;
            SendButton.Enabled = false;
        }
        private void SendButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }
        private void MessageBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendMessage();
        }
        private void ExportChatButton_Click(object sender, EventArgs e)
        {
            string projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string fileName = "Chat log.txt";
            string fullPath = projectPath + "\\" + fileName;
            if (!File.Exists(fullPath))
                File.Create(fullPath);
            else
            {
                foreach (string message in Messages)
                {
                    Console.Write("Writing {0}", message);
                    using(System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath, true))
                    {
                        file.WriteLine(message);
                    }
                }
            }
            System.Windows.Forms.MessageBox.Show("Chat log has been exported to " + projectPath, "Export chat");
        }
        private void CreateRoomButton_Click(object sender, EventArgs e)
        {
            CreateRoomButtonState = !CreateRoomButtonState;
            if(CreateRoomButtonState)
            {
                ServerRole = new Role("server", RoomPort.Text);
                RefreshRateTimer.Enabled = true;
                try
                {
                    server = new TcpListener(ServerRole.LocalAddress, ServerRole.PortNumber);
                    server.Start();
                }
                catch
                {
                    new Thread(()=>
                    {
                        StatusText.BeginInvoke(new Action(() => StatusText.Text = "Failed to create room"));
                        StatusText.BeginInvoke(new Action(() => StatusText.ForeColor = Color.Red));
                        Thread.Sleep(2000);
                        StatusText.BeginInvoke(new Action(() => StatusText.Text = "Disconnected"));
                    }).Start();
                    CreateRoomButtonState = false;
                    return;
                }
                CreateRoomButton.Text = "Shutdown";
                RoomPort.ReadOnly = true;
                JoinRoomLabel.Enabled = false;
                ClientIPLabel.Enabled = false;
                ClientPortLabel.Enabled = false;
                ClientIP.Enabled = false;
                ClientPort.Enabled = false;
                JoinRoomButton.Enabled = false;
                RoomIP.Text = ServerRole.LocalAddress.ToString();
                StatusText.Text = "Room is online";
                StatusText.ForeColor = Color.Green;
                MessageBox.Enabled = true;
                SendButton.Enabled = true;
            }
            else
            {
                RefreshRateTimer.Enabled = false;
                if (client != null)
                    client.Close();
                server.Stop();
                ServerThread.IsBackground = false;
                ServerThread.Abort();
                CreateRoomButton.Text = "Create";
                RoomPort.ReadOnly = false;
                JoinRoomLabel.Enabled = true;
                ClientIPLabel.Enabled = true;
                ClientPortLabel.Enabled = true;
                ClientIP.Enabled = true;
                ClientPort.Enabled = true;
                JoinRoomButton.Enabled = true;
                RoomIP.Text = string.Empty;
                StatusText.Text = "Disconnected";
                StatusText.ForeColor = Color.Red;
                MessageBox.Enabled = false;
                SendButton.Enabled = false;
            }
        }
        private void ClientPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (ClientPort.Text.Length >= 4)
                e.SuppressKeyPress = true;
            VerifyInput(e);
        }
        private void RefreshRateTimer_Tick(object sender, EventArgs e)
        {
            if(CreateRoomButtonState)
            {
                ServerThread = new Thread(Server);
                ServerThread.IsBackground = true;
                ServerThread.Start();
            }
            if(JoinRoomButtonState)
            {
                ClientThread = new Thread(ListenToServer);
                ClientThread.IsBackground = true;
                ClientThread.Start();
            }
        }
        private void RoomPort_TextChanged(object sender, EventArgs e)
        {
            if (RoomPort.Text.Length != 0)
                CreateRoomButton.Enabled = true;
            else
                CreateRoomButton.Enabled = false;
        }
        private void ClientIP_TextChanged(object sender, EventArgs e)
        {
            if (ClientIP.Text.Length != 0 && ClientPort.Text.Length != 0)
                JoinRoomButton.Enabled = true;
            else
                JoinRoomButton.Enabled = false;
        }
        private void JoinRoomButton_Click(object sender, EventArgs e)
        {
            JoinRoomButtonState = !JoinRoomButtonState;
            if(JoinRoomButtonState)
            {
                ClientRole = new Role("client", ClientIP.Text, ClientPort.Text);
                RefreshRateTimer.Enabled = true;
                try
                {
                    client = new TcpClient(ClientRole.LocalAddress.ToString(), ClientRole.PortNumber);
                }
                catch
                {
                    new Thread(() =>
                    {
                        StatusText.BeginInvoke(new Action(() => StatusText.Text = "Failed to connect"));
                        Thread.Sleep(1000);
                        StatusText.BeginInvoke(new Action(() => StatusText.Text = "Disconnected"));
                    }).Start();
                    JoinRoomButtonState = false;
                    return;
                }
                CreateRoomLabel.Enabled = false;
                CreateRoomButton.Enabled = false;
                RoomIPLabel.Enabled = false;
                RoomPortLabel.Enabled = false;
                RoomIP.Enabled = false;
                RoomPort.Enabled = false;
                ClientIP.ReadOnly = true;
                ClientPort.ReadOnly = true;
                JoinRoomButton.Text = "Leave";
                StatusText.Text = "Connected";
                StatusText.ForeColor = Color.Green;
                MessageBox.Enabled = true;
                SendButton.Enabled = true;
            }
            else
            {
                RefreshRateTimer.Enabled = false;
                CreateRoomLabel.Enabled = true;
                CreateRoomButton.Enabled = true;
                RoomIPLabel.Enabled = true;
                RoomPortLabel.Enabled = true;
                RoomIP.Enabled = true;
                RoomPort.Enabled = true;
                ClientIP.ReadOnly = false;
                ClientPort.ReadOnly = false;
                client.Close();
                ClientThread.IsBackground = false;
                ClientThread.Abort();
                JoinRoomButton.Text = "Join";
                StatusText.Text = "Disonnected";
                StatusText.ForeColor = Color.Red;
                MessageBox.Enabled = false;
                SendButton.Enabled = false;
            }
        }
        private void RoomPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (RoomPort.Text.Length >= 4)
                e.SuppressKeyPress = true;
            VerifyInput(e);
        }
        private void ClearChatButton_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Are you sure you want to clear chat history?", "Yes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageField.Controls.Clear();
                CurrentY = 0;
                Messages.Clear();
                TextOverflowPaddingY = TextOverflowDefaultPaddingY;
                TimeOverflowPaddingY = TimeOverflowDefaultPaddingY;
                FirstTime = true;
            }
        }
        private void ClientPort_TextChanged(object sender, EventArgs e)
        {
            if (ClientIP.Text.Length != 0 && ClientPort.Text.Length != 0)
                JoinRoomButton.Enabled = true;
            else
                JoinRoomButton.Enabled = false;
        }
        void VerifyInput(KeyEventArgs e)
        {
            bool IsControlPressed = false, IsAPressed = false;
            int result;
            if (e.Modifiers == Keys.Control)
                IsControlPressed = true;
            if (e.KeyCode == Keys.A)
                IsAPressed = true;
            int.TryParse(e.KeyValue.ToString(), out result);
            if ((result < 48 || result > 57) && e.KeyCode != Keys.NumPad0 && e.KeyCode != Keys.NumPad1 && e.KeyCode != Keys.NumPad2 && e.KeyCode != Keys.NumPad3 && e.KeyCode != Keys.NumPad4 && e.KeyCode != Keys.NumPad5 && e.KeyCode != Keys.NumPad6 && e.KeyCode != Keys.NumPad7 && e.KeyCode != Keys.NumPad8 && e.KeyCode != Keys.NumPad9)
                e.SuppressKeyPress = true;
            if ((IsControlPressed && e.KeyCode == Keys.A) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Insert || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
                e.SuppressKeyPress = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (CreateRoomButtonState)
                {
                    CreateRoomButton.PerformClick();
                    ServerThread.Abort();
                }  
                if (JoinRoomButtonState)
                    ClientThread.Abort();
                RefreshRateTimer.Stop();
            }
            catch { }
        }
        void SendMessage()
        {
            if (MessageBox.Text.Trim().Length == 0) return;
            TextBubble(MessageBox.Text, "right", DateTime.Now.ToString("h.mm tt"));
            ToWrite = MessageBox.Text;
            if(JoinRoomButtonState)
            {
                new Thread(() =>
                {
                    Client();
                }).Start();
            }
            else if (CreateRoomButtonState)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] message = System.Text.Encoding.ASCII.GetBytes(ToWrite);
                    ToWrite = null;
                    stream.Write(message, 0, message.Length);
                    Console.WriteLine("Sent {0}", message);
                    stream.Close();
                }
                catch
                {
                    Console.WriteLine("Failed to connect");
                }
            }
            MessageBox.Text = string.Empty;
        }
        void TextBubble(string message, string side, string time)
        {
            string logRole = null;
            if (CreateRoomButtonState)
            {
                if (side == "right")
                    logRole = "Server";
                else if (side == "left")
                    logRole = "Client";
            }
            else if (JoinRoomButtonState)
            {
                if (side == "right")
                    logRole = "Client";
                else if (side == "left")
                    logRole = "Server";
            }
            Messages.Add("[" + time + "] " + logRole + ": " + message);
            int timePaddingY = 0;
            Label textBox = new Label();
            Label timeLabel = new Label();
            timeLabel.Text = time;
            timeLabel.Font = new Font("Microsoft Sans Serif", 5);
            timeLabel.ForeColor = Color.Black;
            timeLabel.BackColor = Color.Transparent;
            int MessageLength = 0, limit = 45;
            string[] word = null;
            if (message.Length > limit)
            {
                message = message.Insert(message.Length, " ");
                word = message.Split(' ');
                foreach (string word2 in word)
                    MessageLength += word2.Length;
                string tempMessage = null;
                int i = 0;
                bool firstLoop = true;
                List<string> tempMessageList = new List<string>();
                while (true)
                {
                    if(i == word.Length-1)
                    {
                        tempMessageList.Add(tempMessage);
                        break;
                    }
                    if (firstLoop)
                    {
                        tempMessage = word[i];
                        firstLoop = false;
                    }
                    if ((tempMessage + " " + word[i+1]).Length <= limit)
                    {
                        tempMessage += " " + word[i + 1];
                        i++;
                    }
                    else
                    {
                        tempMessageList.Add(tempMessage);
                        tempMessage = null;
                        firstLoop = true;
                        i++;
                        continue;
                    }
                }
                message = null;
                message += String.Join("\n", tempMessageList);
            }
            textBox.Text = message;
            Size textSize = TextRenderer.MeasureText(message, MessageBox.Font), timeSize = TextRenderer.MeasureText(time, MessageBox.Font);
            textBox.Width = textSize.Width + 10;
            textBox.Height = textSize.Height + 10;
            textBox.Padding = new Padding(5);
            textBox.TextAlign = ContentAlignment.MiddleLeft;
            timeLabel.Width = timeSize.Width;
            timeLabel.Height = timeSize.Height;
            Console.WriteLine("text font size: {0}, {1}", MessageBox.Font.Size, MessageBox.Font.SizeInPoints);
            Console.WriteLine("field height: {0}", MessageField.Size.Height);
            Console.WriteLine("expexted CurrentY: {0}", CurrentY + textBox.Height + TextPadingY + timeLabel.Height + timePaddingY);
            Console.WriteLine("CurrentY: {0}", CurrentY);
            Console.WriteLine("textBox height: {0}, {1}", textBox.Height, textBox.Size.Height);
            Console.WriteLine(11 * (message.Count(f => f == '\n') + 1));
            if (CurrentY + textBox.Height + TextPadingY + timeLabel.Height + timePaddingY >= MessageField.Size.Height)
            {
                Console.WriteLine("triggered");
                if (FirstTime)
                {
                    Console.WriteLine(FirstTime);
                    TextOverflowPaddingY += 12;//satu line 12
                    TimeOverflowPaddingY += -15;//satu line -15
                    FirstTime = false;
                }
                else
                {
                    TextOverflowPaddingY = TextOverflowDefaultPaddingY;
                    TimeOverflowPaddingY = TimeOverflowDefaultPaddingY;
                }
                if (side == "right")
                {
                    textBox.Location = new Point(MessageField.Size.Width - textSize.Width - 25, MessageField.Size.Height - TextOverflowPaddingY);
                    textBox.BackColor = Color.PaleGreen;
                    timeLabel.Location = new Point(MessageField.Size.Width - timeSize.Width - 15, MessageField.Size.Height - TimeOverflowPaddingY);
                    timeLabel.TextAlign = ContentAlignment.MiddleRight;
                }
                else if (side == "left")
                {
                    textBox.Location = new Point(0, MessageField.Size.Height - TextOverflowPaddingY);
                    textBox.BackColor = Color.LightBlue;
                    timeLabel.Location = new Point(0, MessageField.Size.Height - TimeOverflowPaddingY);
                    timeLabel.TextAlign = ContentAlignment.MiddleLeft;
                }
            }
            else if (CurrentY < MessageField.Size.Height)
            {
                Console.WriteLine("sinsi");
                if (side == "right")
                {
                    textBox.Location = new Point(MessageField.Size.Width - textSize.Width - 25, CurrentY);
                    textBox.BackColor = Color.PaleGreen;
                    timeLabel.Location = new Point(MessageField.Size.Width - timeSize.Width - 15, CurrentY + textBox.Height + timePaddingY);
                    timeLabel.TextAlign = ContentAlignment.MiddleRight;
                }
                else if (side == "left")
                {
                    textBox.Location = new Point(0, CurrentY);
                    textBox.BackColor = Color.LightBlue;
                    timeLabel.Location = new Point(0, CurrentY + textBox.Height + timePaddingY);
                    timeLabel.TextAlign = ContentAlignment.MiddleLeft;
                }
            }
            CurrentY += textBox.Height + TextPadingY + timeLabel.Height + timePaddingY;
            if (CreateRoomButtonState || JoinRoomButtonState)
            {
                new Thread(() =>
                {
                    MessageField.BeginInvoke(new Action(() => MessageField.AutoScrollPosition = new Point(0, MessageField.VerticalScroll.Maximum)));
                    MessageField.BeginInvoke(new Action(() => MessageField.Controls.Add(textBox)));
                    MessageField.BeginInvoke(new Action(() => MessageField.AutoScrollPosition = new Point(0, MessageField.VerticalScroll.Maximum)));
                    MessageField.BeginInvoke(new Action(() => MessageField.Controls.Add(timeLabel)));
                }).Start();
            }
        }
        /////////////////////////////////////////////////////////////server//////////////////////////////////////////////////////////////////////
        void Server()
        {
            Byte[] bytes = new Byte[256];
            ToRead = null;
            try
            {
                int i;
                client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    ToRead = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received {0}", ToRead);
                if (ToRead != null)
                {
                    new Thread(() =>
                    {
                        TextBubble(ToRead, "left", DateTime.Now.ToString("h.mm tt"));
                        ToRead = null;
                    }).Start();
                }
                stream.Close();
                //client.Close();
            }
            catch
            {
                FirstTime = true;
                if (client != null)
                    client.Close();
                Console.WriteLine("Connection failed");
            }
        }
        /// <summary>
        /// /////////////////////////////////////////////////////////client//////////////////////////////////////////////////////////////////////
        /// </summary>
        void ListenToServer()
        {
            Console.WriteLine("listening to server");
            try
            {
                client = new TcpClient(ClientIP.Text, Int32.Parse(ClientPort.Text));
                NetworkStream stream = client.GetStream();
                Byte[] data = new Byte[256];
                Int32 bytes = stream.Read(data, 0, data.Length);
                ToRead = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received {0}", ToRead);
                if (ToRead.Length != 0)
                    new Thread(() => TextBubble(ToRead, "left", DateTime.Now.ToString("h.mm tt"))).Start();
                stream.Close();
                client.Close();
            }
            catch
            {
                if(JoinRoomButtonState)
                {
                    Console.WriteLine("Connection lost");
                    client.Close();
                    StatusText.BeginInvoke(new Action(() => StatusText.Text = "Connection lost"));
                    StatusText.BeginInvoke(new Action(() => StatusText.ForeColor = Color.Red));
                    Thread.Sleep(1000);
                    StatusText.BeginInvoke(new Action(() => StatusText.Text = "Disconnected"));
                    RefreshRateTimer.Enabled = false;
                    CreateRoomLabel.BeginInvoke(new Action(() => CreateRoomLabel.Enabled = true));
                    CreateRoomButton.BeginInvoke(new Action(() => CreateRoomButton.Enabled = true));
                    RoomIPLabel.BeginInvoke(new Action(() => RoomIPLabel.Enabled = true));
                    RoomPortLabel.BeginInvoke(new Action(() => RoomPortLabel.Enabled = true));
                    RoomIP.BeginInvoke(new Action(() => RoomIP.Enabled = true));
                    RoomPort.BeginInvoke(new Action(() => RoomPort.Enabled = true));
                    ClientIP.BeginInvoke(new Action(() => ClientIP.ReadOnly = false));
                    ClientPort.BeginInvoke(new Action(() => ClientPort.ReadOnly = false));
                    ClientThread.IsBackground = false;
                    ClientThread.Abort();
                    Console.WriteLine("ClientThread aborted");
                    JoinRoomButton.BeginInvoke(new Action(() => JoinRoomButton.Text = "Join"));
                    StatusText.BeginInvoke(new Action(() => StatusText.Text = "Disonnected"));
                    StatusText.BeginInvoke(new Action(() => StatusText.ForeColor = Color.Red));
                    MessageBox.BeginInvoke(new Action(() => MessageBox.Enabled = false));
                    SendButton.BeginInvoke(new Action(() => SendButton.Enabled = false));
                    JoinRoomButtonState = false;
                    return;
                }
            }
        }
        void Client()
        {
            try
            {
                TcpClient client = new TcpClient(ClientIP.Text, Int32.Parse(ClientPort.Text));
                NetworkStream stream = client.GetStream();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(ToWrite);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent {0}", data);
                stream.Close();
                client.Close();
            }
            catch
            {
                Console.WriteLine("Failed to connect to server");
                client.Close();
                new Thread(() => JoinRoomButton.BeginInvoke(new Action(() => JoinRoomButton.PerformClick()))).Start();
            }
        }
    }
}
