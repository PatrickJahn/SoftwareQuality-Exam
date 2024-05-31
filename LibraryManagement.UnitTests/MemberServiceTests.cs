

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Members;
using Moq;
using Xunit;

namespace LibraryManagement.UnitTests;

public class MemberServiceTests
{
    private readonly Mock<IRepository<Member>> _memberRepositoryMock;
    private readonly MemberService _memberService;

    public MemberServiceTests()
    {
        _memberRepositoryMock = new Mock<IRepository<Member>>();
        _memberService = new MemberService(_memberRepositoryMock.Object);
    }

    [Fact]
    public void GetAllMembers_ReturnsAllMembers()
    {
        // Arrange
        var members = new List<Member>
        {
            new Member { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", CprNr = "1234567890" },
            new Member { Id = Guid.NewGuid(), Name = "Jane Doe", Email = "jane@example.com", CprNr = "0987654321" }
        };

        _memberRepositoryMock.Setup(repo => repo.GetAll()).Returns(members);

        // Act
        var result = _memberService.GetAllMembers();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, m => m.Name == "John Doe");
        Assert.Contains(result, m => m.Name == "Jane Doe");
    }

    [Fact]
    public void GetById_ReturnsMember_WhenMemberExists()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, Name = "John Doe", Email = "john@example.com", CprNr = "1234567890" };

        _memberRepositoryMock.Setup(repo => repo.Get(memberId)).Returns(member);

        // Act
        var result = _memberService.GetById(memberId);

        // Assert
        Assert.Equal(memberId, result.Id);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public void GetById_ThrowsException_WhenMemberDoesNotExist()
    {
        // Arrange
        var memberId = Guid.NewGuid();

        _memberRepositoryMock.Setup(repo => repo.Get(memberId)).Returns((Member)null);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _memberService.GetById(memberId));
        Assert.Equal("Member not found", exception.Message);
    }

    [Fact]
    public async Task AddMember_AddsMember_WhenValid()
    {
        // Arrange
        var member = new Member { Id = Guid.NewGuid(), Name = "John Doe", Email = "john@example.com", CprNr = "1234567890" };

        // Act
        var result = await _memberService.AddMember(member);

        // Assert
        _memberRepositoryMock.Verify(repo => repo.Add(member), Times.Once);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task AddMember_ThrowsArgumentException_WhenMissingRequiredFields()
    {
        // Arrange
        var member = new Member { Id = Guid.NewGuid(), Name = "John Doe" }; // Missing Email and CprNr

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _memberService.AddMember(member));
        Assert.Equal("Email, Name, and cpr nr. are required.", exception.Message);
    }

    [Fact]
    public async Task DeleteMember_RemovesMember_WhenMemberExists()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var member = new Member { Id = memberId, Name = "John Doe", Email = "john@example.com", CprNr = "1234567890" };

        _memberRepositoryMock.Setup(repo => repo.Get(memberId)).Returns(member);

        // Act
        await _memberService.DeleteMember(memberId);

        // Assert
        _memberRepositoryMock.Verify(repo => repo.Remove(memberId), Times.Once);
    }

    [Fact]
    public async Task DeleteMember_ThrowsKeyNotFoundException_WhenMemberDoesNotExist()
    {
        // Arrange
        var memberId = Guid.NewGuid();

        _memberRepositoryMock.Setup(repo => repo.Get(memberId)).Returns((Member)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _memberService.DeleteMember(memberId));
        Assert.Equal("Member not found.", exception.Message);
    }
}