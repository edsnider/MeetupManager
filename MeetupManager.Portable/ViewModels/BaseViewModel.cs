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
using System;
using System.Threading.Tasks;
using MeetupManager.Portable.Interfaces;
using Cirrious.MvvmCross.ViewModels;

namespace MeetupManager.Portable.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		internal readonly IMeetupService meetupService;
		public BaseViewModel (IMeetupService meetupService)
		{
			this.meetupService = meetupService;
		}

		private bool isBusy = false;
		public bool IsBusy
		{ 
			get { return isBusy; }
			set { 
				isBusy = value; 
				RaisePropertyChanged(() => IsBusy); 
				if (IsBusyChanged != null)
					IsBusyChanged (isBusy);
			}
		}

        private bool canLoadMore = false;
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { canLoadMore = value; RaisePropertyChanged(() => CanLoadMore); }
        }

		public Action<bool> IsBusyChanged { get; set; }

        private MvxCommand loadMoreCommand;

        public IMvxCommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand(async () => ExecuteLoadMoreCommand())); }
        }

	    protected virtual async Task ExecuteLoadMoreCommand()
	    {
	    }
	}
}

