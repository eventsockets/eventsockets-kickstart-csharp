using System.Runtime.Serialization;

namespace EventSockets.Kickstart.Model
{
	[DataContract(Name = "dummyobject")]
	public class DummyObject
	{
		[DataMember(Name = "message")]
		public string Message;
	}
}