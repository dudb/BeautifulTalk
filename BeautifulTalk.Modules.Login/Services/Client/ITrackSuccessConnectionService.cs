using BeautifulTalk.Modules.Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalk.Modules.Login.Services.Client
{
    public interface ITrackSuccessConnectionService
    {
        Identifications GetListOfSuccessConnections();
    }
}
