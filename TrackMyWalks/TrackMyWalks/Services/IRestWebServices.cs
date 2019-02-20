using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMyWalks.Models;

namespace TrackMyWalks.Services
{
    public interface IRestWebServices
    {
		Task<List<WalkDataModel>> GetWalkEntries();

		Task SaveWalkEntry(WalkDataModel item, bool isAdding);

		Task DeleteWalkEntry(string id);
    }
}
