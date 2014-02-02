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
using System.Threading.Tasks;
using MeetupManager.Portable.Services;
using MeetupManager.Portable.Services.Responses;
using MeetupManager.Portable.Models;

namespace MeetupManager.Portable.Interfaces
{
	public interface IMeetupService
	{
		Task<EventsRootObject> GetEvents(string groupId, int skip);
		Task<RSVPsRootObject> GetRSVPs (string eventId, int skip);
	    Task<GroupsRootObject> GetGroups(string memberId, int skip);
	    Task<MeetupService.RequestTokenObject> GetToken(string code);
		Task<bool> RenewAccessToken ();

		Task<LoggedInUser> GetCurrentMember ();
	}
}

