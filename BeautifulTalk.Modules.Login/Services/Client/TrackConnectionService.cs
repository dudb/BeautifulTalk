using BeautifulTalk.Modules.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public class TrackConnectionService : ITrackSuccessConnectionService
    {
        public Identifications GetListOfSuccessConnections()
        {
            //you must implement Some Logic to get ConnectionHistory from registry.
            throw new NotImplementedException();
        }
    }
}
