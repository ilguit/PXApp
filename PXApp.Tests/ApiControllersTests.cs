using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using PXApp.API.Controllers;
using PXApp.API.Contracts.Service;
using PXApp.API.Entity.Message;
using PXApp.API.Mapping;
using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using PXApp.Core.Db.Entity;

namespace PXApp.Tests;

public class ApiControllersTests
{
    private IServiceMapper _serviceMapper;
    
    [OneTimeTearDown]
    public void Teardown()
    {
        // 
    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        SetupMapper();
    }

    private void SetupMapper()
    {

        var mapperProfile = new MessageProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
        var mapper = new Mapper(configuration);
        
        mapper.ConfigurationProvider.AssertConfigurationIsValid();

        _serviceMapper = new ServiceMapper(mapper);
    }

    [Test]
    public void GetMessages()
    {
        var serviceMock = new Mock<IService<TableMessage>>();
        
        serviceMock.Setup(x => x.CountAsync(null)).ReturnsAsync(1);
        var obj = new TableMessage()
        {
            Body = "test",
            Id = Guid.NewGuid()
        };
        serviceMock.Setup(x => x.GetAsync(null, null)).ReturnsAsync(new List<TableMessage>()
        {
            obj
        });
        
        var rabbitMock = new Mock<IRabbitMqService>();
        
        var controller = new MessagesController(_serviceMapper, serviceMock.Object, rabbitMock.Object);

        var response = controller.Get(new MessageGetRequest(){Take = 10, Skip = 0});

        var result = response.Result;
        Assert.IsNotNull(result.Value);
        Assert.IsTrue(result.Value.Total == 1);
        Assert.IsNotNull(result.Value.Items);
        Assert.That(obj.Id, Is.EqualTo(result.Value.Items[0].Id));
    }

    [Test]
    public void GetByIdMessages()
    {
        var serviceMock = new Mock<IService<TableMessage>>();
        
        serviceMock.Setup(x => x.CountAsync(null)).ReturnsAsync(1);
        var obj = new TableMessage()
        {
            Body = "test",
            Id = Guid.NewGuid()
        };
        serviceMock.Setup(x => x.GetAsync(obj.Id)).ReturnsAsync(obj);
        
        var rabbitMock = new Mock<IRabbitMqService>();
        
        var controller = new MessagesController(_serviceMapper, serviceMock.Object, rabbitMock.Object);

        var response = controller.GetById(new MessageGetByIdRequest(){Id = obj.Id});

        var result = response.Result;
        Assert.IsNotNull(result.Value);
        Assert.That(obj.Id, Is.EqualTo(result.Value.Id));
    }

    [Test]
    public void PostMessages()
    {
        var serviceMock = new Mock<IService<TableMessage>>();
        
        var obj = new TableMessage()
        {
            Body = "test",
            Id = Guid.NewGuid()
        };
        var post = new MessagePostBody() { Body = "test" };
        
        serviceMock.Setup(x => x.AddAsync(It.IsAny<TableMessage>())).ReturnsAsync(obj);
        
        var rabbitMock = new Mock<IRabbitMqService>();
        
        var controller = new MessagesController(_serviceMapper, serviceMock.Object, rabbitMock.Object);

        var response = controller.Post(new MessagePostRequest(){Body = post});

        var result = response.Result;
        Assert.IsNotNull(result.Value);
        Assert.That(obj.Id, Is.EqualTo(result.Value.Id));
    }

    [Test]
    public void PutMessages()
    {
        var serviceMock = new Mock<IService<TableMessage>>();
        
        var obj = new TableMessage()
        {
            Body = "test",
            Id = Guid.NewGuid()
        };
        var post = new MessagePutBody() { Body = "test" };
        
        serviceMock.Setup(x => x.GetAsync(obj.Id)).ReturnsAsync(obj);
        serviceMock.Setup(x => x.UpdateAsync(It.IsAny<TableMessage>())).ReturnsAsync(obj);
        
        var rabbitMock = new Mock<IRabbitMqService>();
        
        var controller = new MessagesController(_serviceMapper, serviceMock.Object, rabbitMock.Object);

        var response = controller.Put(new MessagePutRequest(){Body = post, Id = obj.Id});

        var result = response.Result;
        Assert.IsNotNull(result.Value);
        Assert.That(obj.Id, Is.EqualTo(result.Value.Id));
    }

    [Test]
    public void DeleteMessages()
    {
        var serviceMock = new Mock<IService<TableMessage>>();
        
        var obj = new TableMessage()
        {
            Body = "test",
            Id = Guid.NewGuid()
        };
        
        serviceMock.Setup(x => x.DeleteAsync(obj.Id));
        
        var rabbitMock = new Mock<IRabbitMqService>();
        
        var controller = new MessagesController(_serviceMapper, serviceMock.Object, rabbitMock.Object);

        var response = controller.Delete(new MessageDeleteRequest(){Id = obj.Id});

        var result = response.Result;
        var okResult = result as IStatusCodeActionResult;
        Assert.That(okResult?.StatusCode, Is.EqualTo(200));
    }
}