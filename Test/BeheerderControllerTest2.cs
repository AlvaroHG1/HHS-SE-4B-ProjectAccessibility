using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;
using ProjectAccessibility.Controllers;
using ProjectAccessibility.Context;
using ProjectAccessibility.Models;

namespace ProjectAccessibility.Test;

public class BeheerderControllerTest { // deze kan ik niet runnen (?)
    
    private  Mock<GebruikerContext> _mockContext;
    private  BeheerderController _controller;
    private  Mock<DbSet<Beheerder>> _mockSet;

    public void BeheerderControllerTests()
    {
        _mockContext = new Mock<GebruikerContext>();
        _mockSet = new Mock<DbSet<Beheerder>>();
        var beheerderData = new List<Beheerder>
        {
        
        }.AsQueryable();

        _mockSet.As<IQueryable<Beheerder>>().Setup(m => m.Provider).Returns(beheerderData.Provider);
        _mockSet.As<IQueryable<Beheerder>>().Setup(m => m.Expression).Returns(beheerderData.Expression);
        _mockSet.As<IQueryable<Beheerder>>().Setup(m => m.ElementType).Returns(beheerderData.ElementType);
        _mockSet.As<IQueryable<Beheerder>>().Setup(m => m.GetEnumerator()).Returns(beheerderData.GetEnumerator());

        
        _mockSet.Setup(m => m.Add(It.IsAny<Beheerder>())).Callback<Beheerder>((s) => beheerderData.ToList().Add(s));
        _mockContext.Setup(m => m.Beheerders).Returns(_mockSet.Object);

        _controller = new BeheerderController(_mockContext.Object);
    }
}