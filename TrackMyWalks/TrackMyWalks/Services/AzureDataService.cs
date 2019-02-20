using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using System.Linq;
using Xamarin.Forms;
using TrackMyWalks.Services;
using Plugin.Connectivity;
using System.Diagnostics;

[assembly: Dependency(typeof(AzureDataService))]
namespace TrackMyWalks.Services
{
    public class AzureDataService
    {
		public MobileServiceClient Client { get; set; }
		IMobileServiceSyncTable<WalkDataModel> WalkEntryTable;

		public AzureDataService()
		{
			Initialize();
		}

		public async Task Initialize()
		{
			if (Client?.SyncContext?.IsInitialized ?? false)
				return;

			var appUrl = "http://trackwalks2.azurewebsites.net";

			Client = new MobileServiceClient(appUrl);

			const string path = "syncstore.db";
			//setup our local sqlite store and intialize our table
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<WalkDataModel>();
			await Client.SyncContext.InitializeAsync(store);

			//Get our sync table that will call out to azure
			WalkEntryTable = Client.GetSyncTable<WalkDataModel>();
		}

		public async Task<List<WalkDataModel>> GetWalkEntries()
		{
			await Initialize();
			await SyncWalkEntries();

			return await WalkEntryTable.ToListAsync();
		}

		public async Task AddWalkEntires(WalkDataModel walk)
		{
			await WalkEntryTable.InsertAsync(walk);

			await SyncWalkEntries();
		}

		public async Task SyncWalkEntries()
		{
			if (!CrossConnectivity.Current.IsConnected)
				return;

			try
			{
				await WalkEntryTable.PullAsync("WalkEntries", WalkEntryTable.CreateQuery());
				await Client.SyncContext.PushAsync();
			} catch ( Exception ex )
			{
				Debug.WriteLine("Error syncing: " + ex);
			}
		}
	}
}
