/*
 * MeetupManager:
 * Copyright (C) 2013 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Android.App;
using Android.OS;
using Android.Widget;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Droid.Views
{
    [Activity(Label = "EventView", Icon = "@drawable/ic_launcher")]
    public class EventView : BaseView
	{
		private EventViewModel viewModel;
		private new EventViewModel ViewModel 
		{
			get { return viewModel ?? (viewModel = base.ViewModel as EventViewModel); }
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            Tag = "Event";
			SetContentView(Resource.Layout.view_event);
            FindViewById<GridView>(Resource.Id.grid).SetOnScrollListener(this);
		    SupportActionBar.Title = ViewModel.EventName;
		}

		public override bool OnCreateOptionsMenu (Android.Views.IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.menu_event, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected (Android.Views.IMenuItem item)
		{
            if(ViewModel.IsBusy)
                return base.OnOptionsItemSelected(item);
			switch (item.ItemId) {
			case Resource.Id.menu_select_winner:
				ViewModel.SelectWinnerCommand.Execute (null);
				return true;
            case Resource.Id.menu_refresh:
                ViewModel.RefreshCommand.Execute(null);
			    return true;
            case Resource.Id.menu_add_new_member:
                ViewModel.AddNewUserCommand.Execute(null);
                return true;
			}
			return base.OnOptionsItemSelected (item);
		}
	}
}

