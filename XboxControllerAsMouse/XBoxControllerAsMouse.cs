using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading;
using WindowsInput;
using SharpDX.XInput;
using System.Web.Mvc;

namespace XBoxAsMouse
{
	public class XBoxControllerAsMouse
	{
		private const int RefreshRate = 60;

		private System.Timers.Timer _timer;
		private SharpDX.XInput.Controller _controller;

		public XBoxControllerAsMouse()
		{
			_controller = new SharpDX.XInput.Controller(UserIndex.One);
			_timer = new System.Timers.Timer(1000 / RefreshRate);
			_timer.Elapsed += (s, e) => { Update(); };
			_timer.AutoReset = false;
		}

		public void Start()
		{
			_timer.Start();
		}

		private void Update()
		{
			_controller.GetState(out var state);
			GamepadInputManager.Update(state);
			_timer.Start();
		}
	}
}