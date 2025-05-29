using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IDeepSeekRequestService
{
    Task<string> SendPromptAsync(string prompt);
}