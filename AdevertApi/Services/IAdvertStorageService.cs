﻿using AdvertApi.Models;
using System.Threading.Tasks;

namespace AdevertApi.Services
{
	public interface IAdvertStorageService
	{
		Task<string> Add(AdvertModel model);
		Task Confirm(ConfirmAdvertModel model);
		Task<bool> CheckHealthAsync();
	}
}
