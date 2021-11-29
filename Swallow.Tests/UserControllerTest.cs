
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using Swallow.Controllers;
using Swallow.Models;

namespace Swallow.Tests;

public class UserControllerTest
{

    private readonly UserController _controller = new UserController();

    [Fact]
    public void Test1()
    {
        var newVar = 10;
        Assert.Equal(4, 2+2);
    }

    [Fact]
    public void GetAll_OK()
    {
        //Act
        var response = _controller.GetAll();
        
        //Assert
        var result = Assert.IsType<ActionResult<List<User>>>(response);

        Assert.IsType<List<User>>(result.Value);
    }

    [Fact]
    public void GetById_OK()
    {
        //Arrange
        
        //Act
        
        //Assert
    }

    [Fact]
    public void GetById_NotFound()
    {
    //Given
    
    //When
    
    //Then
    }

    // Create User tests
    [Fact]
    public void CreateUser_Created()
    {
    //Given
    
    //When
    
    //Then
    }

    [Fact]
    public void CreateUser_BadRequest()
    {
    //Given
    
    //When
    
    //Then
    }

    
    // Update user Tests
    [Fact]
    public void UpdateUser_NoContent()
    {
    //Given
    
    //When
    
    //Then
    }

    [Fact]
    public void UpdateUser_BadRequest()
    {
    //Given
    
    //When
    
    //Then
    }

    [Fact]
    public void UpdateUser_NotFound()
    {
    //Given
    
    //When
    
    //Then
    }


    // Delete user test
    [Fact]
    public void DeleteUser_NoContent()
    {
    //Given
    
    //When
    
    //Then
    }

    [Fact]
    public void DeleteUser_NotFound()
    {
    //Given
    
    //When
    
    //Then
    }

}