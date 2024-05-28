using FluentAssertions;
using TDDLesson;

namespace TDDLesson.Orders.Tests;

[TestClass]
public class MessageRendererTests
{
    [TestMethod]
    public void RenderBody_MessageWithPrompt_RenderedWithCompanyName()
    {
        // Arrange
        var companyName = "Mindbox";
        var template = "MessageTemplate {companyName}";
        var exceptedBody = "MessageTemplate Mindbox";
        
        // Act
        var actualBody = MessageRenderer.RenderBody(companyName, template);
        
        // Assert
        actualBody.Should().Be(exceptedBody);
    }
    
    [TestMethod]
    public void RenderBody_MessageWithoutPrompt_RenderedWithoutCompanyName()
    {
        // Arrange
        var template = "MessageTemplate";
        var exceptedBody = "MessageTemplate";
        
        // Act
        var actualBody = MessageRenderer.RenderBody("companyName", template);
        
        // Assert
        actualBody.Should().Be(exceptedBody);
    }
}