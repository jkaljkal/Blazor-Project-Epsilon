using BlazorApp.Client.Components;
using Bunit;

namespace BlazorApp.Test.Components
{
    public class LoadingTextComponentTests : TestContext
    {
        [Fact]
        public void LoadingText_DisplaysDefaultText_WhenNoTextParameterProvided()
        {
            // Act
            var cut = RenderComponent<LoadingText>();

            // Assert
            Assert.Contains("Loading...", cut.Markup);
            Assert.Contains("spinner-border", cut.Markup);
        }

        [Fact]
        public void LoadingText_DisplaysCustomText_WhenTextParameterProvided()
        {
            // Arrange
            var customText = "Custom loading message";

            // Act
            var cut = RenderComponent<LoadingText>(parameters => parameters
                .Add(p => p.Text, customText));

            // Assert
            Assert.Contains(customText, cut.Markup);
            Assert.Contains("spinner-border", cut.Markup);
        }
    }
}