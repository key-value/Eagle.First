using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface ILetterServices : IAppServices
    {
        void SendLetter(UpdateLetter updateLetter);
        List<ShowLetter> GetLetters(int pageNum);
        ShowLetter GetLetter(Guid id);
        void ReplyLetter(UpdateLetter updateLetter);
        void Delete(List<Guid> letterIdList);
        List<ShowLetter> GetLetter(DateTime beginTime, DateTime endTime);
        void SendLetterWcf(UpdateLetter updateLetter);
    }
}