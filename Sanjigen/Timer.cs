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
