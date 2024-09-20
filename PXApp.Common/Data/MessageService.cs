using Microsoft.Extensions.Configuration;
using PXApp.Common.Contracts;
using PXApp.Common.Data.Entity;

namespace PXApp.Common.Data;

public class MessageService(IConfiguration configuration) : BaseApiService<Message>(configuration, "messages"), IApiService<Message>;