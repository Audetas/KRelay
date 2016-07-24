using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.GameData.DataStructures {
	public interface IDataStructure<IDType> {
		string Name { get; }
		IDType ID { get; }
	}
}
