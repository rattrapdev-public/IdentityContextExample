﻿using System;

namespace RattrapDev.Identity.Domain.Clients
{
	public class ClientDetails : IEquatable<ClientDetails>
	{
		private string name;

		public ClientDetails (string clientName, ClientStatus status)
		{
			this.Name = clientName;
			this.Status = status;
		}

		public string Name 
		{
			get 
			{
				return name;
			}
			private set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("The name cannot be null or empty!");
				}

				name = value;
			}
		}

		public ClientStatus Status { get; set; }

		#region IEquatable implementation

		public bool Equals (ClientDetails other)
		{
			if (other == null)
				return false;
			return other.Name.Equals (this.Name) && other.Status.Equals (this.Status);
		}

		#endregion
	}
}

