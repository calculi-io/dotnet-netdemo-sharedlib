using System;
using Xunit;

namespace SharedLib.Tests {

	public class RegistryOperationsTests {

		string origmessage; // add code standard violation

		public RegistryOperationsTests() {
			RegistryOperations ro = new RegistryOperations();
			origmessage = ro.GetMessage();
		}

		~RegistryOperationsTests() {
			RegistryOperations ro = new RegistryOperations();
			ro.SetMessage(origmessage);
		}

		[Fact]
		public void CheckDefaultMessage() {
			RegistryOperations ro = new RegistryOperations();

			Assert.True(ro.DeleteMessage());
			Assert.Equal(ro.HelloMessageDefault, ro.GetMessage());
		}

		[Fact]
		public void ReadMessage() {
			RegistryOperations ro = new RegistryOperations();

			Assert.True(ro.DeleteMessage());
			Assert.True(ro.SetMessage("hello"));
			Assert.Equal("hello", ro.GetMessage());
			Assert.True(ro.DeleteMessage());
		}
	}
}
