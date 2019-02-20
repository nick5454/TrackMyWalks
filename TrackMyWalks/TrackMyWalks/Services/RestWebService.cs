using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;

namespace TrackMyWalks.Services
{
	public class RestWebService : IRestWebServices
	{
		//HttpClient client;
		AzureDataService client;

		//public RestWebService()
		//{
		//	client = new AzureDataService();
		//	client.BaseAddress = new Uri("https://trackwalks2.azurewebsites.net");
		//	client.MaxResponseContentBufferSize = 256000;
		//	client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
		//}

		//public async Task DeleteWalkEntry(string id)
		//{
		//	try
		//	{
		//		var response = await client.DeleteAsync("tables/WalkEntries/" + id);

		//		if(response.IsSuccessStatusCode)
		//		{
		//			Debug.WriteLine("WalkEntry Items was successfully deleted.");
		//		}
		//	} catch ( Exception ex )
		//	{
		//		Debug.WriteLine("An error occurred {0}", ex.Message);
		//	}
		//}

		//public async Task<List<WalkDataModel>> GetWalkEntries()
		//{
		//	var Items = new List<WalkDataModel>();

		//	try
		//	{
		//		var response = await client.GetAsync("tables/WalkEntries");

		//		if(response.IsSuccessStatusCode)
		//		{
		//			var content = await response.Content.ReadAsStringAsync();

		//			Items = JsonConvert.DeserializeObject<List<WalkDataModel>>(content);
		//		}
		//	} catch ( Exception ex )
		//	{
		//		Debug.WriteLine("An error ocurred {0}", ex.Message);
		//	}

		//	return Items;
		//}

		//public async Task SaveWalkEntry(WalkDataModel item, bool isAdding)
		//{
		//	try
		//	{
		//		HttpResponseMessage responseMessage;
		//		var json = JsonConvert.SerializeObject(item);
		//		var content = new StringContent(json, Encoding.UTF8, "application/json");

		//		if(isAdding)
		//		{
		//			responseMessage = await client.PostAsync("/tables/WalkEntries", content);
		//		} else
		//		{
		//			responseMessage = await client.PutAsync("/tables/WalkEntries", content);
		//		}

		//		if(responseMessage.IsSuccessStatusCode)
		//		{
		//			Debug.WriteLine("WalkEntry Item successfully saved.");
		//		}
		//	} catch ( Exception ex )
		//	{
		//		Debug.WriteLine("An error occurred {0}", ex.Message);
		//	}
		//}

		public RestWebService()
		{
			client = new AzureDataService();
		}

		public Task DeleteWalkEntry(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<WalkDataModel>> GetWalkEntries()
		{
			var Items = new List<WalkDataModel>();

			try
			{
				return await client.GetWalkEntries();
				//var response = await client.GetAsync("tables/WalkEntries");

				//if (response.IsSuccessStatusCode)
				//{
				//	var content = await response.Content.ReadAsStringAsync();

				//	Items = JsonConvert.DeserializeObject<List<WalkDataModel>>(content);
				//}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error ocurred {0}", ex.Message);
			}

			return Items;
		}

		public async Task SaveWalkEntry(WalkDataModel item, bool isAdding)
		{
			await client.AddWalkEntires(item);
		}

	}
}

