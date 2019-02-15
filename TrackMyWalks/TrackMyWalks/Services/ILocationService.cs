using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyWalks.Services
{
    public interface ILocationService
    {
		Task<Position> GetCurrentPosition();

		Task StartListening();

		void StopListening();
    }
}
