﻿using StreamDeckLib;
using StreamDeckLib.Messages;
using System.Dynamic;
using System.Threading.Tasks;

namespace SamplePlugin
{
	internal class MySamplePlugin : BaseStreamDeckPlugin
	{

		// Cheer 200 kevin_downs Jan 11, 2019

		private static int _Counter = 0;

		public override async Task OnKeyUp(StreamDeckEventPayload args)
		{
			_Counter++;
			await Manager.SetTitleAsync(args.context, _Counter.ToString());

            if (_Counter % 10 == 0)
            {
                await Manager.ShowAlertAsync(args.context);
            }
            else if (_Counter % 15 == 0)
            {
                await Manager.OpenUrlAsync(args.context, "https://www.bing.com");
            }
            else if (_Counter % 3 == 0)
            {
                await Manager.ShowOkAsync(args.context);
            } 
        }

        public override async Task OnWillAppear(StreamDeckEventPayload args)
        {
            if(args.payload!= null && args.payload.settings != null && args.payload.settings.counter != null)
            {
                _Counter = args.payload.settings.counter;
            }
            await Manager.SetTitleAsync(args.context, _Counter.ToString());
        }

        public override async Task OnWillDisappear(StreamDeckEventPayload args)
        {
            dynamic settings = new ExpandoObject();
            settings.counter = _Counter;
            await Manager.SetSettingsAsync(args.context, settings);
        }

    }
}