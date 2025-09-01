using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PKHeX_Raid_Plugin
{
    public enum PopupShape
    {
        Ellipse,
        RoundedRectangle
    }

    public class BubblePopup : Form
    {
        private readonly Timer _lifetimeTimer = new();
        private readonly int _cornerRadius = 16;
        private readonly int _shadowSize = 2;
        private readonly int _displayTimeMs;
        private readonly Control _anchorControl;
        private readonly string _text;
        private readonly Padding _textPadding = new(12, 8, 12, 8);
        private readonly int _maxTextWidth = 300;
        private readonly Timer _fadeTimer = new();
        private bool _fadingIn = true;
        private bool _dragging = false;
        private Point _dragOffset = Point.Empty;
        private PopupShape _shape;

        /// <summary>
        /// Gets or sets the size of the text.
        /// </summary>
        [Category("Appearance")]
        [Description("The size of the font for the popup text.")]
        [DefaultValue(9f)]
        public float FontSize { get; set; } = 9f;

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        [Category("Appearance")]
        [Description("The color of the thumb.")]
        [DefaultValue(typeof(Color), "Black")]
        public Color FontColor { get; set; } = Color.Black;

        public BubblePopup(Control anchor, PopupShape shape, string text, int displayTimeMs = 3000)
        {
            _anchorControl = anchor;
            _text = text;
            _displayTimeMs = displayTimeMs;
            _shape = shape;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.White;
            DoubleBuffered = true;
            TopMost = true;
            Opacity = 0;
            Width = 180;
            Height = 100;

            _lifetimeTimer.Interval = _displayTimeMs;
            _lifetimeTimer.Tick += OnLifetimeElapsed;
            _fadeTimer.Tick += OnFadeTick;

            Shown += OnFirstShown;
            Load += OnLoadAdjustAndPosition;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;

        }

        protected override bool ShowWithoutActivation => true;

        private void OnFirstShown(object s, EventArgs e)
        {
            _fadingIn = true;
            _fadeTimer.Start();
        }


        private void OnLifetimeElapsed(object s, EventArgs e)
        {
            _lifetimeTimer.Stop();
            _fadingIn = false;
            _fadeTimer.Start();
        }

        private void OnFadeTick(object s, EventArgs e)
        {
            if (_fadingIn)
            {
                Opacity += 0.1;
                if (Opacity >= 1)
                {
                    Opacity = 1.00;
                    _fadeTimer.Stop();
                    _lifetimeTimer.Start();
                }
            }
            else
            {
                Opacity -= 0.1;
                if (Opacity <= 0)
                {
                    _fadeTimer.Stop();
                    Close();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using var shadowBrush = new SolidBrush(Color.FromArgb(50, Color.Black));
            var shadowRect = new Rectangle(
                _shadowSize,
                _shadowSize,
               Width + _shadowSize,
               Height + _shadowSize
            );
            using var shadowPath = new GraphicsPath();

            if (_shape == PopupShape.Ellipse)
                shadowPath.AddEllipse(shadowRect);
            else
                shadowPath.AddPath(GetRoundedPath(shadowRect, _cornerRadius), false);

            g.FillPath(shadowBrush, shadowPath);

            using var bubbleBrush = new SolidBrush(BackColor);
            using var bubblePath = new GraphicsPath();
            var bubbleRect = new Rectangle();

            if (_shape == PopupShape.Ellipse)
            {
                (bubbleRect.Width, bubbleRect.Height, bubbleRect.X, bubbleRect.Y) = (Width - _shadowSize, Height - _shadowSize, 0, 0);
                bubblePath.AddEllipse(bubbleRect);
            }
            else
            {
                (bubbleRect.Width, bubbleRect.Height, bubbleRect.X, bubbleRect.Y) = (Width - _shadowSize, Height - _shadowSize, 0, 0);
                bubblePath.AddPath(GetRoundedPath(bubbleRect, _cornerRadius), false);
            }
            g.FillPath(bubbleBrush, bubblePath);

            using var font = new Font(Font.FontFamily, FontSize);
            using var textBrush = new SolidBrush(FontColor);
            using var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var textRect = new Rectangle();
            if (_shape == PopupShape.Ellipse)
            {
                var width = Width - _textPadding.Left - _textPadding.Right - _shadowSize;
                var height = Height - _textPadding.Top - _textPadding.Bottom - _shadowSize;
                textRect = new Rectangle(_textPadding.Left, _textPadding.Top, width, height);
            }
            else
                textRect = new Rectangle(10, 10, Width - 20, Height - 20);

            g.DrawString(_text, font, textBrush, textRect, sf);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            using var regionPath = new GraphicsPath();
            if (_shape == PopupShape.Ellipse)
                regionPath.AddEllipse(ClientRectangle);
            else
                regionPath.AddPath(GetRoundedPath(ClientRectangle, _cornerRadius), false);
            Region = new Region(regionPath);
        }

        private void PositionNearAnchor()
        {
            if (_anchorControl == null) return;

            var screenPos = _anchorControl.PointToScreen(Point.Empty);
            var x = screenPos.X + (_anchorControl.Width - Width) / 2;
            var y = screenPos.Y - Height - 10;

            Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

        public static void ShowBubble(Control anchor, PopupShape shape, string text, int displayTimeMs = 3000)
        {
            var popup = new BubblePopup(anchor, shape, text, displayTimeMs);
            popup.Show();
        }

        private void OnLoadAdjustAndPosition(object sender, EventArgs e)
        {
            AdjustSize();
            PositionNearAnchor();
        }

        private void AdjustSize()
        {
            using var g = CreateGraphics();
            using var font = new Font(Font.FontFamily, FontSize);
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
            var layoutWidth = _maxTextWidth - _textPadding.Left - _textPadding.Right;
            var measured = g.MeasureString(_text, font, layoutWidth, sf);

            var finalWidth = (int)Math.Ceiling(measured.Width)
                             + _textPadding.Left
                             + _textPadding.Right
                             + _shadowSize;
            var finalHeight = (int)Math.Ceiling(measured.Height)
                              + _textPadding.Top
                              + _textPadding.Bottom
                              + _shadowSize;

            Size = new Size(finalWidth, finalHeight);
        }

        private void OnMouseDown(object s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _dragging = true;
            _dragOffset = e.Location;
        }

        private void OnMouseMove(object s, MouseEventArgs e)
        {
            if (!_dragging) return;
            var screen = PointToScreen(e.Location);
            Location = new Point(
                screen.X - _dragOffset.X,
                screen.Y - _dragOffset.Y
            );
        }

        private void OnMouseUp(object s, MouseEventArgs e)
        {
            if (!_dragging) return;
            _dragging = false;
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(rect.Left, rect.Top, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Top, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}

