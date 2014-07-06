using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
	public class Timer
	{
		public Timer()
		{
			_tickDelegate = new Internal.FreeGLUT.Delegates.TimerCallback(_tick);
		}
		public Timer(int interval) : this()
		{
			mvarInterval = interval;
		}

		private static List<Internal.FreeGLUT.Delegates.TimerCallback> _TimerCallbacks = new List<Internal.FreeGLUT.Delegates.TimerCallback>();
		public static void SetTimeout(int timeout, EventHandler callback, EventArgs e = null)
		{
			if (e == null) e = EventArgs.Empty;

			Internal.FreeGLUT.Delegates.TimerCallback callback1 = delegate(int val)
			{
				callback(null, e);
			};
			_TimerCallbacks.Add(callback1);

			Caltron.Internal.FreeGLUT.Methods.glutTimerFunc(timeout, callback1, 0);
		}

		private int mvarInterval = 0;
		public int Interval { get { return mvarInterval; } set { mvarInterval = value; } }

		private Internal.FreeGLUT.Delegates.TimerCallback _tickDelegate = null;
		private void _tick(int value)
		{
			OnTick(EventArgs.Empty);
			if (mvarEnabled)
			{
				Caltron.Internal.FreeGLUT.Methods.glutTimerFunc(mvarInterval, _tickDelegate, 0);
			}
		}
		
		public event EventHandler Tick;
		protected virtual void OnTick(EventArgs e)
		{
			if (Tick != null) Tick(this, e);
		}

		private bool mvarEnabled = false;
		public bool Enabled
		{
			get { return mvarEnabled; }
			set
			{
				bool e = mvarEnabled;
				mvarEnabled = value;
				
				if (e == false && value == true)
				{
					Caltron.Internal.FreeGLUT.Methods.glutTimerFunc(mvarInterval, _tickDelegate, 0);
				}
			}
		}
	}
}
