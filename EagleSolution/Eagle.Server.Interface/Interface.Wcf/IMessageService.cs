using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using Eagle.ViewModel;

namespace Eagle.Server.Interface.Wcf
{
    [ServiceContract(Namespace = "http://First.eagle.com")]
    public interface IMessageService
    {
        //List<ShowLetter> GetLetter();

        [OperationContract]
        void GetTest(UpdateLetter updateLetter);

        [OperationContract]
        void Entrance(Dictionary<string, string> param);
    }
}