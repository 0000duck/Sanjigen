using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

using Caltron.Input.Joystick;
using Caltron.Input.Keyboard;
using Caltron.Input.Mouse;

namespace Caltron
{
    public class Window : IControlContainer
    {
        private static Dictionary<Window, int> windowHandles = new Dictionary<Window, int>();
        private static Dictionary<int, Window> handleWindows = new Dictionary<int, Window>();

		public IControlContainer Parent
		{
			get { return null; }
		}

        /// <summary>
        /// Gets the native handle of this FREEGLUT window.
        /// </summary>
        /// <exception cref="System.PlatformNotSupportedException">The platform is not supported.</exception>
        public IntPtr NativeHandle
        {
            get
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.MacOSX:
                    {
                        break;
                    }
                    case PlatformID.Unix:
                    {
                        break;
                    }
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                    {
                        return Internal.System.Windows.Methods.FindWindow("FREEGLUT", mvarText);
                    }
                }
                throw new PlatformNotSupportedException();
            }
        }

        private IntPtr mvarIconHandle = IntPtr.Zero;
        public IntPtr IconHandle
        {
            get { return mvarIconHandle; }
            set
            {
                mvarIconHandle = value;
                UpdateIcon();
            }
        }

        private void UpdateIcon()
        {
            if (mvarIconHandle != IntPtr.Zero)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.MacOSX:
                    {
                        break;
                    }
                    case PlatformID.Unix:
                    {
                        break;
                    }
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                    {
                        Internal.System.Windows.Methods.SendMessage(NativeHandle, Internal.System.Windows.Constants.WM_SETICON, new IntPtr(Internal.System.Windows.Constants.ICON_BIG), mvarIconHandle);
                        break;
                    }
                }
            }
        }

        private static Window mvarLastFullscreenWindow = null;
        private static int mvarFullscreenWindowHandle = 0;

        private Color mvarBackgroundColor = Color.Empty;
        public Color BackgroundColor { get { return mvarBackgroundColor; } set { mvarBackgroundColor = value; } }

        private Canvas mvarCanvas = null;
        public Canvas Canvas { get { return mvarCanvas; } }

        public int Left
        {
            get { return Internal.FreeGLUT.Methods.glutGet(Internal.FreeGLUT.Constants.GlutStates.WindowX) - System.Windows.Forms.SystemInformation.FrameBorderSize.Width; }
        }
        public int Top
        {
            get { return Internal.FreeGLUT.Methods.glutGet(Internal.FreeGLUT.Constants.GlutStates.WindowY) - System.Windows.Forms.SystemInformation.FrameBorderSize.Height - System.Windows.Forms.SystemInformation.CaptionButtonSize.Height; }
        }
        public int Width
        {
            get { return Internal.FreeGLUT.Methods.glutGet(Internal.FreeGLUT.Constants.GlutStates.WindowWidth) + (System.Windows.Forms.SystemInformation.FrameBorderSize.Width * 2); }
            set
            {
                Internal.FreeGLUT.Methods.glutReshapeWindow(value, Height);
            }
        }
        public int Height
        {
            get { return Internal.FreeGLUT.Methods.glutGet(Internal.FreeGLUT.Constants.GlutStates.WindowHeight) + System.Windows.Forms.SystemInformation.FrameBorderSize.Height + System.Windows.Forms.SystemInformation.CaptionButtonSize.Height; }
            set
            {
                Internal.FreeGLUT.Methods.glutReshapeWindow(Width, value);
            }
        }
        public int Right
        {
            get { return Left + Width; }
        }
        public int Bottom
        {
            get { return Top + Height; }
        }

        // the width and height of the window before moving fullscreen
        private int fsW = 0, fsH = 0;

        private bool mvarFullScreen = false;
        public bool FullScreen
        {
            get { return mvarFullScreen; }
            set
            {
                if (value == mvarFullScreen) return;

                if (value)
                {
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.MacOSX:
                        case PlatformID.Unix:
                        {
                            break;
                        }
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            fsW = Width;
                            fsH = Height;

                            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromHandle(NativeHandle);

                            Internal.System.Windows.Constants.WindowStyles style = (Internal.System.Windows.Constants.WindowStyles)Internal.System.Windows.Methods.GetWindowLong(NativeHandle, Internal.System.Windows.Constants.WindowLongIndex.WindowStyle);
                            style &= ~Internal.System.Windows.Constants.WindowStyles.OverlappedWindow;
                            Internal.System.Windows.Methods.SetWindowLong(NativeHandle, Internal.System.Windows.Constants.WindowLongIndex.WindowStyle, (int)style);

                            Internal.System.Windows.Methods.SetWindowPos(NativeHandle, IntPtr.Zero, screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height, Internal.System.Windows.Constants.SetWindowPosFlags.RetainCurrentZOrder);
                            break;
                        }
                        case PlatformID.Xbox:
                        {
                            break;
                        }
                    }
                }
                else
                {
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.MacOSX:
                        case PlatformID.Unix:
                        {
                            break;
                        }
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            Internal.System.Windows.Methods.ShowWindow(NativeHandle, 10);

                            Internal.System.Windows.Constants.WindowStyles style = (Internal.System.Windows.Constants.WindowStyles)Internal.System.Windows.Methods.GetWindowLong(NativeHandle, Internal.System.Windows.Constants.WindowLongIndex.WindowStyle);
                            style |= Internal.System.Windows.Constants.WindowStyles.OverlappedWindow;
                            Internal.System.Windows.Methods.SetWindowLong(NativeHandle, Internal.System.Windows.Constants.WindowLongIndex.WindowStyle, (int)style);

                            Width = fsW;
                            Height = fsH;
                            break;
                        }
                        case PlatformID.Xbox:
                        {
                            break;
                        }
                    }
                }

                mvarFullScreen = value;
                /*
                if (value)
                {
                    if (mvarLastFullscreenWindow == this)
                    {
                        return;
                    }
                    else if (mvarLastFullscreenWindow != null)
                    {
                        throw new InvalidOperationException("Cannot set a window fullscreen while another window is already fullscreen");
                    }

                    mvarLastFullscreenWindow = this;

                    Internal.FreeGLUT.Methods.glutDestroyWindow(windowHandles[this]);
                    handleWindows.Remove(windowHandles[this]);
                    windowHandles.Remove(this);

                    // Internal.FreeGLUT.Methods.glutGameModeString("width>=1024 height>=768 bpp=32 hertz=60");
                    mvarFullscreenWindowHandle = Internal.FreeGLUT.Methods.glutEnterGameMode();

                    InitWindow();
                }
                else
                {
                    Internal.FreeGLUT.Methods.glutLeaveGameMode();
                    mvarLastFullscreenWindow = null;

                    CreateHandle();
                    Internal.FreeGLUT.Methods.glutShowWindow();
                }
                mvarFullScreen = value;
                */
            }
        }

        private bool mvarAlwaysRender = false;
        public bool AlwaysRender
        {
            get { return mvarAlwaysRender; }
            set
            {
                mvarAlwaysRender = value;
                if (mvarAlwaysRender)
                {
                    Internal.FreeGLUT.Methods.glutIdleFunc(_OnIdleCallback);
                }
                else
                {
                    Internal.FreeGLUT.Methods.glutIdleFunc(null);
                }
            }
        }
		
		private Control.ControlCollection mvarControls = null;
		public Control.ControlCollection Controls
		{
			get { return mvarControls; }
		}
        
        public Window()
        {
            mvarControls = new Control.ControlCollection(this);
            CreateHandle();
        }

        protected virtual void OnCreated()
        {
        }

        private string mvarText = String.Empty;
        public string Text
        {
            get { return mvarText; }
            set
            {
                if (mvarLastFullscreenWindow == null)
                {
                    mvarText = value;
                    if (windowHandles.ContainsKey(this))
                    {
                        Internal.FreeGLUT.Methods.glutSetWindow(windowHandles[this]);
                        Internal.FreeGLUT.Methods.glutSetWindowTitle(mvarText);
                    }
                }
            }
        }

        private bool mvarIsCreated = false;

        private void CreateHandle()
        {
            int hwnd = Internal.FreeGLUT.Methods.glutCreateWindow(mvarText);
            
            windowHandles.Add(this, hwnd);
            handleWindows.Add(hwnd, this);

            InitWindow();
            mvarCanvas = new Canvas(hwnd);

            Internal.FreeGLUT.Methods.glutReshapeWindow((int)mvarSize.Width, (int)mvarSize.Height);

            UpdateIcon();

            if (!mvarIsCreated)
            {
                OnCreated();
            }
            mvarIsCreated = true;
        }

        private void InitWindow()
        {
            Internal.FreeGLUT.Methods.glutDisplayFunc(_OnRenderCallback);
            // Internal.FreeGLUT.Methods.glutIdleFunc(_OnIdleCallback);

            Internal.FreeGLUT.Methods.glutKeyboardFunc(_OnKeyboardCallback);
            Internal.FreeGLUT.Methods.glutSpecialFunc(_OnSpecialCallback);
            Internal.FreeGLUT.Methods.glutKeyboardUpFunc(_OnKeyboardUpCallback);
            Internal.FreeGLUT.Methods.glutSpecialUpFunc(_OnSpecialUpCallback);
            Internal.FreeGLUT.Methods.glutMouseFunc(_OnMouseCallback);
            Internal.FreeGLUT.Methods.glutMotionFunc(_OnMotionCallback);
            Internal.FreeGLUT.Methods.glutPassiveMotionFunc(_OnPassiveMotionCallback);
            Internal.FreeGLUT.Methods.glutReshapeFunc(_OnReshapeCallback);
        }

        #region Event Handlers

        #region Render
        public event BeforeRenderEventHandler BeforeRender;
        protected virtual void OnBeforeRender(BeforeRenderEventArgs e)
        {
            if (BeforeRender != null) BeforeRender(this, e);
        }
        public event RenderEventHandler AfterRender;
        protected virtual void OnAfterRender(RenderEventArgs e)
        {
            if (AfterRender != null) AfterRender(this, e);
        }
        private static Internal.FreeGLUT.Delegates.DisplayCallback _OnRenderCallback = new Internal.FreeGLUT.Delegates.DisplayCallback(_OnRender);
        private static Internal.FreeGLUT.Delegates.IdleCallback _OnIdleCallback = new Internal.FreeGLUT.Delegates.IdleCallback(_OnRender);
        
        private static void _OnRender()
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }

			BeforeRenderEventArgs bre = new BeforeRenderEventArgs(w.Canvas);
			RenderEventArgs re = new RenderEventArgs(w.Canvas);

            Internal.OpenGL.Methods.glMatrixMode(MatrixMode.Projection);
            Internal.OpenGL.Methods.glLoadIdentity();
            Internal.OpenGL.Methods.glViewport(0, 0, w.Width, w.Height);
            Internal.OpenGL.Methods.glOrtho(0, w.Width, w.Height, 0, -1, 1);

            Internal.OpenGL.Methods.glMatrixMode(MatrixMode.ModelView);
			Internal.OpenGL.Methods.glLoadIdentity();

			Internal.OpenGL.Methods.glClearColor(w.BackgroundColor.Red, w.BackgroundColor.Green, w.BackgroundColor.Blue, w.BackgroundColor.Alpha);
			Internal.OpenGL.Methods.glClear(Internal.OpenGL.Constants.GL_COLOR_BUFFER_BIT | Internal.OpenGL.Constants.GL_DEPTH_BUFFER_BIT);

            w.OnBeforeRender(bre);

            if (bre.Cancel) return;

            bool depthtest = Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.DepthTesting);
            bool lighting = Internal.OpenGL.Methods.glIsEnabled(Internal.OpenGL.Constants.GLCapabilities.Lighting);

            double ofsx = 1.0, ofsy = 11.0;
            if (w.FullScreen)
            {
                ofsx = 0;
                ofsy = -10;
            }

            foreach (Control ctl in w.Controls)
            {
                if (ctl.Visible)
                {
                    if (ctl is Control2D)
                    {
                        // set up for 2D rendering
                        Caltron.Internal.OpenGL.Methods.glViewport(0, 0, w.Width, w.Height);

                        Control2D c2d = (ctl as Control2D);
                        Internal.OpenGL.Methods.glMatrixMode(MatrixMode.ModelView);
                        Internal.OpenGL.Methods.glLoadIdentity();
                        Internal.OpenGL.Methods.glTranslated((c2d.Position.X + ofsx), (c2d.Position.Y + ofsy), 0);

                        if (depthtest) Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.DepthTesting);
                        if (lighting) Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.Lighting);
                    }

                    ctl.OnBeforeRender(bre);
					if (!bre.Cancel)
					{
						ctl.OnRender(re);
						ctl.OnAfterRender(re);
					}

                    if (ctl is Control2D)
                    {
                        Control2D c2d = (ctl as Control2D);
                        Internal.OpenGL.Methods.glMatrixMode(MatrixMode.ModelView);
                        Internal.OpenGL.Methods.glTranslated(-(c2d.Position.X + ofsx), -(c2d.Position.Y + ofsy), 0);

                        if (depthtest) Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.DepthTesting);
                        if (lighting) Internal.OpenGL.Methods.glEnable(Internal.OpenGL.Constants.GLCapabilities.Lighting);
                    }
                }
            }

            w.OnAfterRender(re);
            Internal.FreeGLUT.Methods.glutSwapBuffers();
        }
        #endregion
        #region Mouse
        public event MouseEventHandler MouseDown;
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null) MouseDown(this, e);
        }
        public event MouseEventHandler MouseMove;
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null) MouseMove(this, e);
        }
        public event MouseEventHandler MouseUp;
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null) MouseUp(this, e);
        }
        public event MouseEventHandler MouseWheel;
        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            if (MouseWheel != null) MouseWheel(this, e);
        }

        private static Control mvarFocusedControl = null;

        private static Internal.FreeGLUT.Delegates.MouseCallback _OnMouseCallback = new Internal.FreeGLUT.Delegates.MouseCallback(_OnMouse);
        private static void _OnMouse(int button, int state, int x, int y)
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }

            MouseButton buttons = MouseButton.None;
            switch (button)
            {
                case 0:
                {
                    buttons = MouseButton.Primary;
                    break;
                }
                case 1:
                {
                    buttons = MouseButton.Wheel;
                    break;
                }
                case 2:
                {
                    buttons = MouseButton.Secondary;
                    break;
                }
                case 3:
                {
                    // Wheel up
                    if (state == 0)
                    {
                        state = 2;
                    }
                    else if (state == 1)
                    {
                        return;
                    }
                    break;
                }
                case 4:
                {
                    // 	Wheel down
                    if (state == 0)
                    {
                        state = 2;
                    }
                    else if (state == 1)
                    {
                        return;
                    }
                    break;
                }
                case 7:
                {
                    buttons = MouseButton.XButton1;
                    break;
                }
                case 8:
                {
                    buttons = MouseButton.XButton2;
                    break;
                }
            }
            MouseEventArgs args = new MouseEventArgs(buttons, x, y);
            /*
            if (w.FullScreen)
            {
                y += 25;
                x -= 5;
            }
            */
            switch (state)
            {
                case 0:
                {
                    Control2D c2d = w.HitTest(x, y);
                    if (c2d != null)
                    {
                        args = new MouseEventArgs(buttons, (int)(x - c2d.Position.X), (int)(y - c2d.Position.Y));
                        w._motionCtl = c2d;

                        if (mvarFocusedControl != c2d)
                        {
                            if (mvarFocusedControl != null)
                            {
                                mvarFocusedControl.HasFocus = false;
                                mvarFocusedControl.OnLostFocus(EventArgs.Empty);
                            }
                            mvarFocusedControl = c2d;
                            c2d.HasFocus = true;
                            c2d.OnGotFocus(EventArgs.Empty);
                        }

                        c2d.OnMouseDown(args);
                        return;
                    }
                    else if (mvarFocusedControl != null)
                    {
                        mvarFocusedControl.HasFocus = false;
                        mvarFocusedControl.OnLostFocus(EventArgs.Empty);
                        mvarFocusedControl = null;
                    }
                    w.OnMouseDown(args);
                    break;
                }
                case 1:
                {
                    Control2D c2d = (w._motionCtl as Control2D);
                    if (c2d != null)
                    {
                        args = new MouseEventArgs(buttons, (int)(x - c2d.Position.X), (int)(y - c2d.Position.Y));
                        w._motionCtl.OnMouseUp(args);
                        w._motionCtl = null;
                        return;
                    }
                    w.OnMouseUp(args);
                    break;
                }
                case 2:
                {
                    Control2D c2d = w.HitTest(x, y);
                    if (c2d != null)
                    {
                        args = new MouseEventArgs(buttons, (int)(x - 4 - c2d.Position.X), (int)(y + 12 - c2d.Position.Y));
                        c2d.OnMouseWheel(args);
                        return;
                    }
                    w.OnMouseWheel(args);
                    break;
                }
            }
        }

        public Control2D HitTest(double x, double y)
        {
            for (int i = mvarControls.Count - 1; i >= 0; i--)
            {
                Control ctl = mvarControls[i];

                if (!ctl.Visible) continue;
                if (ctl is Control2D)
                {
                    Control2D c2d = (ctl as Control2D);
                    if (x >= c2d.Position.X && y >= c2d.Position.Y && x <= c2d.Position.X + c2d.Size.Width && y <= c2d.Position.Y + c2d.Size.Height)
                    {
                        return c2d;
                    }
                }
            }
            return null;
        }

        private static Internal.FreeGLUT.Delegates.MotionCallback _OnMotionCallback = new Internal.FreeGLUT.Delegates.MotionCallback(_OnMotion);
        private static Internal.FreeGLUT.Delegates.PassiveMotionCallback _OnPassiveMotionCallback = new Internal.FreeGLUT.Delegates.PassiveMotionCallback(_OnMotion);

        private Control _motionCtl = null;

        private static void _OnMotion(int x, int y)
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }
            MouseEventArgs args = new MouseEventArgs(MouseButton.None, x, y);
            if (w._motionCtl != null)
            {
                w._motionCtl.OnMouseMove(args);
            }
            else
            {
                Control2D ctl = w.HitTest(x, y);
                if (ctl != null)
                {
                    args = new MouseEventArgs(MouseButton.None, (int)(x - ctl.Position.X), (int)(y - ctl.Position.Y));
                    ctl.OnMouseMove(args);
                }
                else
                {
                    w.OnMouseMove(args);
                }
            }
        }
        #endregion
        #region Keyboard
		public event KeyboardEventHandler KeyDown;
		protected virtual void OnKeyDown(KeyboardEventArgs e)
		{
			if (KeyDown != null) KeyDown(this, e);
		}
		public event KeyboardEventHandler KeyUp;
		protected virtual void OnKeyUp(KeyboardEventArgs e)
		{
			if (KeyUp != null) KeyUp(this, e);
		}


        private static Internal.FreeGLUT.Delegates.KeyboardCallback _OnKeyboardCallback = new Internal.FreeGLUT.Delegates.KeyboardCallback(_OnKeyboard);
        public static void _OnKeyboard(byte key, int x, int y)
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
                            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }
			
			int modifiers = Internal.FreeGLUT.Methods.glutGetModifiers();
			
			KeyboardKey keys = GetKeyboardKey(key);

			KeyboardModifierKey modifierKeys = (KeyboardModifierKey)modifiers;
			
			KeyboardEventArgs e = new KeyboardEventArgs(keys, modifierKeys);
			w.OnKeyDown(e);

			foreach (Control ctl in w.Controls)
			{
				// if (ctl == w.ActiveControl)
				{
					ctl.OnKeyDown(e);
				}
			}
		}
        private static Internal.FreeGLUT.Delegates.KeyboardUpCallback _OnKeyboardUpCallback = new Internal.FreeGLUT.Delegates.KeyboardUpCallback(_OnKeyboardUp);
        public static void _OnKeyboardUp(byte key, int x, int y)
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
                            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }
			
			int modifiers = Internal.FreeGLUT.Methods.glutGetModifiers();
			
			KeyboardKey keys = GetKeyboardKey(key);
			KeyboardModifierKey modifierKeys = KeyboardModifierKey.None;
			
			KeyboardEventArgs e = new KeyboardEventArgs(keys, modifierKeys);
			w.OnKeyUp(e);
		}


        private static Internal.FreeGLUT.Delegates.SpecialCallback _OnSpecialCallback = new Internal.FreeGLUT.Delegates.SpecialCallback(_OnSpecial);
        public static void _OnSpecial(int key, int x, int y)
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
                            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }
			
			int modifiers = Internal.FreeGLUT.Methods.glutGetModifiers();
			
			KeyboardKey keys = GetSpecialKeyboardKey(key);
			KeyboardModifierKey modifierKeys = (KeyboardModifierKey)modifiers;
			
			KeyboardEventArgs e = new KeyboardEventArgs(keys, modifierKeys);
			w.OnKeyDown(e);

			foreach (Control ctl in w.Controls)
			{
				ctl.OnKeyDown(e);
			}
		}
        private static Internal.FreeGLUT.Delegates.SpecialUpCallback _OnSpecialUpCallback = new Internal.FreeGLUT.Delegates.SpecialUpCallback(_OnSpecialUp);
        public static void _OnSpecialUp(int key, int x, int y)
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
                            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }
			
			int modifiers = Internal.FreeGLUT.Methods.glutGetModifiers();
			
			KeyboardKey keys = GetSpecialKeyboardKey(key);
			KeyboardModifierKey modifierKeys = KeyboardModifierKey.None;
			
			KeyboardEventArgs e = new KeyboardEventArgs(keys, modifierKeys);
			w.OnKeyUp(e);
		}

		private static KeyboardKey GetSpecialKeyboardKey(int key)
		{
			switch (key)
			{
				case 100: return KeyboardKey.ArrowLeft;
				case 101: return KeyboardKey.ArrowUp;
				case 102: return KeyboardKey.ArrowRight;
				case 103: return KeyboardKey.ArrowDown;
			}
			return KeyboardKey.Unknown;
		}
        private static KeyboardKey GetKeyboardKey(int key)
        {
            switch (key)
            {
                default:
                {
                    if (key >= 65 && key <= 90) return (KeyboardKey)(key + 32);
                    if (key >= 97 && key <= 122) return (KeyboardKey)key;
                    break;
                }
            }
            return (KeyboardKey)key;
        }
        #endregion
        #region Resize
        private static Internal.FreeGLUT.Delegates.ReshapeCallback _OnReshapeCallback = new Internal.FreeGLUT.Delegates.ReshapeCallback(_OnReshape);
        private static void _OnReshape(int width, int height)
        {
            Window w = null;
            if (mvarLastFullscreenWindow != null)
            {
                w = mvarLastFullscreenWindow;
            }
            else
            {
                int handle = Internal.FreeGLUT.Methods.glutGetWindow();
                w = handleWindows[handle];
            }
            ResizeEventArgs e = new ResizeEventArgs(w.Size, new Dimension2D(width, height));
            w.OnResizing(e);
        }

        protected virtual void OnResizing(ResizeEventArgs e)
        {
        }
        #endregion
        #endregion

        #region Window Visibility
        private bool mvarVisible = false;
        public bool Visible
        {
            get { return mvarVisible; }
            set
            {
                switch (value)
                {
                    case true:
                        {
                            Show();
                            break;
                        }
                    case false:
                        {
                            Hide();
                            break;
                        }
                }
            }
        }
        public void Show()
        {
            if (mvarLastFullscreenWindow != null) return;

            if (!windowHandles.ContainsKey(this))
            {
                throw new InvalidOperationException("The window handle is invalid");
            }

            Internal.FreeGLUT.Methods.glutSetWindow(windowHandles[this]);
            Internal.FreeGLUT.Methods.glutShowWindow();

            mvarVisible = true;
        }
        public void Hide()
        {
            if (mvarLastFullscreenWindow != null) return;

            if (!windowHandles.ContainsKey(this))
            {
                throw new InvalidOperationException("The window handle is invalid");
            }

            Internal.FreeGLUT.Methods.glutSetWindow(windowHandles[this]);
            Internal.FreeGLUT.Methods.glutHideWindow();

            mvarVisible = false;
        }
        #endregion

        private Dimension2D mvarSize = new Dimension2D(800, 450);
        public Dimension2D Size
        {
            get { return mvarSize; }
            set
            {
                mvarSize = value;

                if (mvarLastFullscreenWindow != null) return;
                if (!windowHandles.ContainsKey(this)) return;
                Internal.FreeGLUT.Methods.glutSetWindow(windowHandles[this]);
                Internal.FreeGLUT.Methods.glutReshapeWindow((int)mvarSize.Width, (int)mvarSize.Height);
            }
        }

        public void Refresh()
        {
            // Don't bother refreshing if we're set to always render, since the app will do that
            // anyway
            if (mvarAlwaysRender) return;

            if (mvarLastFullscreenWindow == null)
            {
                if (windowHandles.ContainsKey(this))
                {
                    Internal.FreeGLUT.Methods.glutSetWindow(windowHandles[this]);
                }
            }
			Internal.FreeGLUT.Methods.glutSwapBuffers();
            Internal.FreeGLUT.Methods.glutPostRedisplay();
        }
	}
}
